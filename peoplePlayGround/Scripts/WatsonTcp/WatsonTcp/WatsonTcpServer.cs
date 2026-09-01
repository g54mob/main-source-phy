using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WatsonTcp
{
	public class WatsonTcpServer : IDisposable
	{
		private string _Header = "[WatsonTcpServer] ";

		private WatsonTcpServerSettings _Settings = new WatsonTcpServerSettings();

		private WatsonTcpServerEvents _Events = new WatsonTcpServerEvents();

		private WatsonTcpServerCallbacks _Callbacks = new WatsonTcpServerCallbacks();

		private WatsonTcpStatistics _Statistics = new WatsonTcpStatistics();

		private WatsonTcpKeepaliveSettings _Keepalive = new WatsonTcpKeepaliveSettings();

		private WatsonTcpServerSslConfiguration _SslConfiguration = new WatsonTcpServerSslConfiguration();

		private int _Connections;

		private bool _IsListening;

		private Mode _Mode;

		private TlsVersion _TlsVersion;

		private string _ListenerIp;

		private int _ListenerPort;

		private IPAddress _ListenerIpAddress;

		private TcpListener _Listener;

		private X509Certificate2 _SslCertificate;

		private ConcurrentDictionary<string, DateTime> _UnauthenticatedClients = new ConcurrentDictionary<string, DateTime>();

		private ConcurrentDictionary<string, ClientMetadata> _Clients = new ConcurrentDictionary<string, ClientMetadata>();

		private ConcurrentDictionary<string, DateTime> _ClientsLastSeen = new ConcurrentDictionary<string, DateTime>();

		private ConcurrentDictionary<string, DateTime> _ClientsKicked = new ConcurrentDictionary<string, DateTime>();

		private ConcurrentDictionary<string, DateTime> _ClientsTimedout = new ConcurrentDictionary<string, DateTime>();

		private CancellationTokenSource _TokenSource = new CancellationTokenSource();

		private CancellationToken _Token;

		private Task _AcceptConnections;

		private Task _MonitorClients;

		private readonly object _SyncResponseLock = new object();

		public WatsonTcpServerSettings Settings
		{
			get
			{
				return _Settings;
			}
			set
			{
				if (value == null)
				{
					_Settings = new WatsonTcpServerSettings();
				}
				else
				{
					_Settings = value;
				}
			}
		}

		public WatsonTcpServerEvents Events
		{
			get
			{
				return _Events;
			}
			set
			{
				if (value == null)
				{
					_Events = new WatsonTcpServerEvents();
				}
				else
				{
					_Events = value;
				}
			}
		}

		public WatsonTcpServerCallbacks Callbacks
		{
			get
			{
				return _Callbacks;
			}
			set
			{
				if (value == null)
				{
					_Callbacks = new WatsonTcpServerCallbacks();
				}
				else
				{
					_Callbacks = value;
				}
			}
		}

		public WatsonTcpStatistics Statistics => _Statistics;

		public WatsonTcpKeepaliveSettings Keepalive
		{
			get
			{
				return _Keepalive;
			}
			set
			{
				if (value == null)
				{
					_Keepalive = new WatsonTcpKeepaliveSettings();
				}
				else
				{
					_Keepalive = value;
				}
			}
		}

		public WatsonTcpServerSslConfiguration SslConfiguration
		{
			get
			{
				return _SslConfiguration;
			}
			set
			{
				if (value == null)
				{
					_SslConfiguration = new WatsonTcpServerSslConfiguration();
				}
				else
				{
					_SslConfiguration = value;
				}
			}
		}

		public int Connections => _Connections;

		public bool IsListening => _IsListening;

		private event EventHandler<SyncResponseReceivedEventArgs> _SyncResponseReceived;

		public WatsonTcpServer(string listenerIp, int listenerPort)
		{
			if (listenerPort < 1)
			{
				throw new ArgumentOutOfRangeException("listenerPort");
			}
			_Mode = Mode.Tcp;
			if (string.IsNullOrEmpty(listenerIp))
			{
				_ListenerIpAddress = IPAddress.Any;
				_ListenerIp = _ListenerIpAddress.ToString();
			}
			else if (listenerIp.Equals("localhost") || listenerIp.Equals("127.0.0.1") || listenerIp.Equals("::1"))
			{
				_ListenerIpAddress = IPAddress.Loopback;
				_ListenerIp = _ListenerIpAddress.ToString();
			}
			else
			{
				_ListenerIpAddress = IPAddress.Parse(listenerIp);
				_ListenerIp = listenerIp;
			}
			_ListenerPort = listenerPort;
			SerializationHelper.InstantiateConverter();
		}

		public WatsonTcpServer(string listenerIp, int listenerPort, string pfxCertFile, string pfxCertPass, TlsVersion tlsVersion = TlsVersion.Tls12)
		{
			if (listenerPort < 1)
			{
				throw new ArgumentOutOfRangeException("listenerPort");
			}
			if (string.IsNullOrEmpty(pfxCertFile))
			{
				throw new ArgumentNullException("pfxCertFile");
			}
			_Mode = Mode.Ssl;
			_TlsVersion = tlsVersion;
			if (string.IsNullOrEmpty(listenerIp))
			{
				_ListenerIpAddress = IPAddress.Any;
				_ListenerIp = _ListenerIpAddress.ToString();
			}
			else if (listenerIp.Equals("localhost") || listenerIp.Equals("127.0.0.1") || listenerIp.Equals("::1"))
			{
				_ListenerIpAddress = IPAddress.Loopback;
				_ListenerIp = _ListenerIpAddress.ToString();
			}
			else
			{
				_ListenerIpAddress = IPAddress.Parse(listenerIp);
				_ListenerIp = listenerIp;
			}
			_SslCertificate = null;
			if (string.IsNullOrEmpty(pfxCertPass))
			{
				_SslCertificate = new X509Certificate2(pfxCertFile);
			}
			else
			{
				_SslCertificate = new X509Certificate2(pfxCertFile, pfxCertPass);
			}
			_ListenerPort = listenerPort;
			SerializationHelper.InstantiateConverter();
		}

		public WatsonTcpServer(string listenerIp, int listenerPort, X509Certificate2 cert, TlsVersion tlsVersion = TlsVersion.Tls12)
		{
			if (listenerPort < 1)
			{
				throw new ArgumentOutOfRangeException("listenerPort");
			}
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			_Mode = Mode.Ssl;
			_TlsVersion = tlsVersion;
			_SslCertificate = cert;
			if (string.IsNullOrEmpty(listenerIp))
			{
				_ListenerIpAddress = IPAddress.Any;
				_ListenerIp = _ListenerIpAddress.ToString();
			}
			else if (listenerIp.Equals("localhost") || listenerIp.Equals("127.0.0.1") || listenerIp.Equals("::1"))
			{
				_ListenerIpAddress = IPAddress.Loopback;
				_ListenerIp = _ListenerIpAddress.ToString();
			}
			else
			{
				_ListenerIpAddress = IPAddress.Parse(listenerIp);
				_ListenerIp = listenerIp;
			}
			_ListenerPort = listenerPort;
			SerializationHelper.InstantiateConverter();
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public void Start()
		{
			if (_IsListening)
			{
				throw new InvalidOperationException("WatsonTcpServer is already running.");
			}
			if (_UnauthenticatedClients == null)
			{
				_UnauthenticatedClients = new ConcurrentDictionary<string, DateTime>();
			}
			if (_Clients == null)
			{
				_Clients = new ConcurrentDictionary<string, ClientMetadata>();
			}
			if (_ClientsLastSeen == null)
			{
				_ClientsLastSeen = new ConcurrentDictionary<string, DateTime>();
			}
			if (_ClientsKicked == null)
			{
				_ClientsKicked = new ConcurrentDictionary<string, DateTime>();
			}
			if (_ClientsTimedout == null)
			{
				_ClientsTimedout = new ConcurrentDictionary<string, DateTime>();
			}
			_TokenSource = new CancellationTokenSource();
			_Token = _TokenSource.Token;
			_Statistics = new WatsonTcpStatistics();
			_Listener = new TcpListener(_ListenerIpAddress, _ListenerPort);
			if (!_Events.IsUsingMessages && !_Events.IsUsingStreams)
			{
				throw new InvalidOperationException("One of either 'MessageReceived' or 'StreamReceived' events must first be set.");
			}
			if (_Mode == Mode.Tcp)
			{
				_Settings.Logger?.Invoke(Severity.Info, _Header + "starting on " + _ListenerIp + ":" + _ListenerPort);
			}
			else
			{
				if (_Mode != Mode.Ssl)
				{
					throw new ArgumentException("Unknown mode: " + _Mode);
				}
				_Settings.Logger?.Invoke(Severity.Info, _Header + "starting with SSL on " + _ListenerIp + ":" + _ListenerPort);
			}
			_Listener.Start();
			_AcceptConnections = Task.Run(() => AcceptConnections(), _Token);
			_MonitorClients = Task.Run(() => MonitorForIdleClients(), _Token);
			_Events.HandleServerStarted(this, EventArgs.Empty);
		}

		public void Stop()
		{
			if (!_IsListening)
			{
				throw new InvalidOperationException("WatsonTcpServer is not running.");
			}
			try
			{
				_IsListening = false;
				_Listener.Stop();
				_TokenSource.Cancel();
				_Settings.Logger?.Invoke(Severity.Info, _Header + "stopped");
				_Events.HandleServerStopped(this, EventArgs.Empty);
			}
			catch (Exception e)
			{
				_Events.HandleExceptionEncountered(this, new ExceptionEventArgs(e));
				throw;
			}
		}

		public bool Send(string ipPort, string data, Dictionary<object, object> metadata = null)
		{
			if (string.IsNullOrEmpty(ipPort))
			{
				throw new ArgumentNullException("ipPort");
			}
			byte[] data2 = new byte[0];
			if (!string.IsNullOrEmpty(data))
			{
				data2 = Encoding.UTF8.GetBytes(data);
			}
			return Send(ipPort, data2, metadata);
		}

		public bool Send(string ipPort, byte[] data, Dictionary<object, object> metadata = null, int start = 0)
		{
			if (string.IsNullOrEmpty(ipPort))
			{
				throw new ArgumentNullException("ipPort");
			}
			if (!_Clients.TryGetValue(ipPort, out var _))
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "unable to find client " + ipPort);
				return false;
			}
			if (data == null)
			{
				data = new byte[0];
			}
			WatsonCommon.BytesToStream(data, start, out var contentLength, out var stream);
			return Send(ipPort, contentLength, stream, metadata);
		}

		public bool Send(string ipPort, long contentLength, Stream stream, Dictionary<object, object> metadata = null)
		{
			if (contentLength < 0)
			{
				throw new ArgumentException("Content length must be zero or greater.");
			}
			if (string.IsNullOrEmpty(ipPort))
			{
				throw new ArgumentNullException("ipPort");
			}
			if (!_Clients.TryGetValue(ipPort, out var value))
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "unable to find client " + ipPort);
				return false;
			}
			if (stream == null)
			{
				stream = new MemoryStream(new byte[0]);
			}
			WatsonMessage msg = new WatsonMessage(metadata, contentLength, stream, syncRequest: false, syncResponse: false, null, null, _Settings.DebugMessages ? _Settings.Logger : null);
			return SendInternal(value, msg, contentLength, stream);
		}

		public async Task<bool> SendAsync(string ipPort, string data, Dictionary<object, object> metadata = null, int start = 0, CancellationToken token = default(CancellationToken))
		{
			if (string.IsNullOrEmpty(ipPort))
			{
				throw new ArgumentNullException("ipPort");
			}
			if (token == default(CancellationToken))
			{
				token = _Token;
			}
			byte[] data2 = new byte[0];
			if (!string.IsNullOrEmpty(data))
			{
				data2 = Encoding.UTF8.GetBytes(data);
			}
			return await SendAsync(ipPort, data2, metadata, start, token).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task<bool> SendAsync(string ipPort, byte[] data, Dictionary<object, object> metadata = null, int start = 0, CancellationToken token = default(CancellationToken))
		{
			if (string.IsNullOrEmpty(ipPort))
			{
				throw new ArgumentNullException("ipPort");
			}
			if (token == default(CancellationToken))
			{
				token = _Token;
			}
			if (!_Clients.TryGetValue(ipPort, out var _))
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "unable to find client " + ipPort);
				return false;
			}
			if (data == null)
			{
				data = new byte[0];
			}
			WatsonCommon.BytesToStream(data, start, out var contentLength, out var stream);
			return await SendAsync(ipPort, contentLength, stream, metadata, token).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task<bool> SendAsync(string ipPort, long contentLength, Stream stream, Dictionary<object, object> metadata = null, CancellationToken token = default(CancellationToken))
		{
			if (contentLength < 0)
			{
				throw new ArgumentException("Content length must be zero or greater.");
			}
			if (string.IsNullOrEmpty(ipPort))
			{
				throw new ArgumentNullException("ipPort");
			}
			if (token == default(CancellationToken))
			{
				token = _Token;
			}
			if (!_Clients.TryGetValue(ipPort, out var value))
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "unable to find client " + ipPort);
				return false;
			}
			if (stream == null)
			{
				stream = new MemoryStream(new byte[0]);
			}
			WatsonMessage msg = new WatsonMessage(metadata, contentLength, stream, syncRequest: false, syncResponse: false, null, null, _Settings.DebugMessages ? _Settings.Logger : null);
			return await SendInternalAsync(value, msg, contentLength, stream, token).ConfigureAwait(continueOnCapturedContext: false);
		}

		public SyncResponse SendAndWait(int timeoutMs, string ipPort, string data, Dictionary<object, object> metadata = null)
		{
			byte[] data2 = new byte[0];
			if (!string.IsNullOrEmpty(data))
			{
				data2 = Encoding.UTF8.GetBytes(data);
			}
			return SendAndWait(timeoutMs, ipPort, data2, metadata);
		}

		public SyncResponse SendAndWait(int timeoutMs, string ipPort, byte[] data, Dictionary<object, object> metadata = null, int start = 0)
		{
			if (string.IsNullOrEmpty(ipPort))
			{
				throw new ArgumentNullException("ipPort");
			}
			if (timeoutMs < 1000)
			{
				throw new ArgumentException("Timeout milliseconds must be 1000 or greater.");
			}
			if (!_Clients.TryGetValue(ipPort, out var _))
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "unable to find client " + ipPort);
				throw new KeyNotFoundException("Unable to find client " + ipPort + ".");
			}
			if (data == null)
			{
				data = new byte[0];
			}
			WatsonCommon.BytesToStream(data, start, out var contentLength, out var stream);
			return SendAndWait(timeoutMs, ipPort, contentLength, stream, metadata);
		}

		public SyncResponse SendAndWait(int timeoutMs, string ipPort, long contentLength, Stream stream, Dictionary<object, object> metadata = null)
		{
			if (contentLength < 0)
			{
				throw new ArgumentException("Content length must be zero or greater.");
			}
			if (string.IsNullOrEmpty(ipPort))
			{
				throw new ArgumentNullException("ipPort");
			}
			if (timeoutMs < 1000)
			{
				throw new ArgumentException("Timeout milliseconds must be 1000 or greater.");
			}
			if (!_Clients.TryGetValue(ipPort, out var value))
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "unable to find client " + ipPort);
				throw new KeyNotFoundException("Unable to find client " + ipPort + ".");
			}
			if (stream == null)
			{
				stream = new MemoryStream(new byte[0]);
			}
			DateTime value2 = DateTime.Now.AddMilliseconds(timeoutMs);
			WatsonMessage msg = new WatsonMessage(metadata, contentLength, stream, syncRequest: true, syncResponse: false, value2, Guid.NewGuid().ToString(), _Settings.DebugMessages ? _Settings.Logger : null);
			return SendAndWaitInternal(value, msg, timeoutMs, contentLength, stream);
		}

		public bool IsClientConnected(string ipPort)
		{
			if (string.IsNullOrEmpty(ipPort))
			{
				throw new ArgumentNullException("ipPort");
			}
			ClientMetadata value;
			return _Clients.TryGetValue(ipPort, out value);
		}

		public IEnumerable<string> ListClients()
		{
			return _Clients.Keys.ToList();
		}

		public void DisconnectClient(string ipPort, MessageStatus status = MessageStatus.Removed, bool sendNotice = true)
		{
			if (string.IsNullOrEmpty(ipPort))
			{
				throw new ArgumentNullException("ipPort");
			}
			if (!_Clients.TryGetValue(ipPort, out var value))
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "unable to find client " + ipPort);
				return;
			}
			if (!_ClientsTimedout.ContainsKey(ipPort))
			{
				_ClientsKicked.TryAdd(ipPort, DateTime.Now);
			}
			if (sendNotice)
			{
				WatsonMessage watsonMessage = new WatsonMessage();
				watsonMessage.Status = status;
				SendInternal(value, watsonMessage, 0L, null);
			}
			value.Dispose();
			_Clients.TryRemove(ipPort, out var _);
		}

		public void DisconnectClients(MessageStatus status = MessageStatus.Removed, bool sendNotice = true)
		{
			if (_Clients == null || _Clients.Count <= 0)
			{
				return;
			}
			foreach (KeyValuePair<string, ClientMetadata> client in _Clients)
			{
				DisconnectClient(client.Value.IpPort, status, sendNotice);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_Settings.Logger?.Invoke(Severity.Info, _Header + "disposing");
				if (_IsListening)
				{
					Stop();
				}
				DisconnectClients(MessageStatus.Shutdown);
				if (_Listener != null && _Listener.Server != null)
				{
					_Listener.Server.Close();
					_Listener.Server.Dispose();
				}
				_Settings = null;
				_Events = null;
				_Callbacks = null;
				_Statistics = null;
				_Keepalive = null;
				_SslConfiguration = null;
				_ListenerIp = null;
				_ListenerIpAddress = null;
				_Listener = null;
				_SslCertificate = null;
				_UnauthenticatedClients = null;
				_Clients = null;
				_ClientsLastSeen = null;
				_ClientsKicked = null;
				_ClientsTimedout = null;
				_TokenSource = null;
				_AcceptConnections = null;
				_MonitorClients = null;
				_IsListening = false;
			}
		}

		private void EnableKeepalives(TcpClient client)
		{
		}

		private async Task AcceptConnections()
		{
			_IsListening = true;
			while (true)
			{
				try
				{
					if (!_IsListening && _Connections >= _Settings.MaxConnections)
					{
						Task.Delay(100).Wait();
						continue;
					}
					if (!_IsListening)
					{
						_Listener.Start();
						_IsListening = true;
					}
					TcpClient tcpClient = await _Listener.AcceptTcpClientAsync().ConfigureAwait(continueOnCapturedContext: false);
					tcpClient.LingerState.Enabled = false;
					tcpClient.NoDelay = _Settings.NoDelay;
					if (_Keepalive.EnableTcpKeepAlives)
					{
						EnableKeepalives(tcpClient);
					}
					string text = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
					if (_Settings.PermittedIPs.Count > 0 && !_Settings.PermittedIPs.Contains(text))
					{
						_Settings.Logger?.Invoke(Severity.Info, _Header + "rejecting connection from " + text + " (not permitted)");
						tcpClient.Close();
						continue;
					}
					ClientMetadata client = new ClientMetadata(tcpClient);
					client.SendBuffer = new byte[_Settings.StreamBufferSize];
					CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_Token, client.Token);
					Interlocked.Increment(ref _Connections);
					if (_Connections >= _Settings.MaxConnections)
					{
						_Settings.Logger?.Invoke(Severity.Info, _Header + "maximum connections " + _Settings.MaxConnections + " met (currently " + _Connections + " connections), pausing");
						_IsListening = false;
						_Listener.Stop();
					}
					if (_Mode == Mode.Tcp)
					{
						Task.Run(delegate
						{
							FinalizeConnection(client, linkedCts.Token);
						}, linkedCts.Token);
					}
					else
					{
						if (_Mode != Mode.Ssl)
						{
							throw new ArgumentException("Unknown mode: " + _Mode);
						}
						if (_Settings.AcceptInvalidCertificates)
						{
							client.SslStream = new SslStream(client.NetworkStream, leaveInnerStreamOpen: false, _SslConfiguration.ClientCertificateValidationCallback);
						}
						else
						{
							client.SslStream = new SslStream(client.NetworkStream, leaveInnerStreamOpen: false);
						}
						Task.Run(async delegate
						{
							if (await StartTls(client).ConfigureAwait(continueOnCapturedContext: false))
							{
								FinalizeConnection(client, linkedCts.Token);
							}
							else
							{
								client.Dispose();
							}
						}, linkedCts.Token);
					}
					_Settings.Logger?.Invoke(Severity.Debug, _Header + "accepted connection from " + client.IpPort);
				}
				catch (TaskCanceledException)
				{
					break;
				}
				catch (ObjectDisposedException)
				{
					break;
				}
				catch (Exception ex3)
				{
					_Settings.Logger?.Invoke(Severity.Error, _Header + "listener exception: " + Environment.NewLine + SerializationHelper.SerializeJson(ex3, pretty: true) + Environment.NewLine);
					_Events.HandleExceptionEncountered(this, new ExceptionEventArgs(ex3));
					break;
				}
			}
		}

		private async Task<bool> StartTls(ClientMetadata client)
		{
			try
			{
				await client.SslStream.AuthenticateAsServerAsync(_SslCertificate, _SslConfiguration.ClientCertificateRequired, _TlsVersion.ToSslProtocols(), !_Settings.AcceptInvalidCertificates).ConfigureAwait(continueOnCapturedContext: false);
				if (!client.SslStream.IsEncrypted)
				{
					_Settings.Logger?.Invoke(Severity.Error, _Header + "stream from " + client.IpPort + " not encrypted");
					client.Dispose();
					Interlocked.Decrement(ref _Connections);
					return false;
				}
				if (!client.SslStream.IsAuthenticated)
				{
					_Settings.Logger?.Invoke(Severity.Error, _Header + "stream from " + client.IpPort + " not authenticated");
					client.Dispose();
					Interlocked.Decrement(ref _Connections);
					return false;
				}
				if (_Settings.MutuallyAuthenticate && !client.SslStream.IsMutuallyAuthenticated)
				{
					_Settings.Logger?.Invoke(Severity.Error, _Header + $"mutual authentication with {client.IpPort} ({_TlsVersion}) failed");
					client.Dispose();
					Interlocked.Decrement(ref _Connections);
					return false;
				}
			}
			catch (Exception ex)
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + $"disconnected during SSL/TLS establishment with {client.IpPort} ({_TlsVersion}): " + Environment.NewLine + SerializationHelper.SerializeJson(ex, pretty: true));
				_Events.HandleExceptionEncountered(this, new ExceptionEventArgs(ex));
				client.Dispose();
				Interlocked.Decrement(ref _Connections);
				return false;
			}
			return true;
		}

		private void FinalizeConnection(ClientMetadata client, CancellationToken token)
		{
			_Clients.TryAdd(client.IpPort, client);
			_ClientsLastSeen.TryAdd(client.IpPort, DateTime.Now);
			if (!string.IsNullOrEmpty(_Settings.PresharedKey))
			{
				_Settings.Logger?.Invoke(Severity.Debug, _Header + "requesting authentication material from " + client.IpPort);
				_UnauthenticatedClients.TryAdd(client.IpPort, DateTime.Now);
				Encoding.UTF8.GetBytes("Authentication required");
				WatsonMessage watsonMessage = new WatsonMessage();
				watsonMessage.Status = MessageStatus.AuthRequired;
				SendInternal(client, watsonMessage, 0L, null);
			}
			_Settings.Logger?.Invoke(Severity.Debug, _Header + "starting data receiver for " + client.IpPort);
			client.DataReceiver = Task.Run(() => DataReceiver(client, token), token);
			_Events.HandleClientConnected(this, new ConnectionEventArgs(client.IpPort));
		}

		private bool IsConnected(ClientMetadata client)
		{
			if (client != null && client.TcpClient != null)
			{
				if (client.TcpClient.Connected)
				{
					byte[] buffer = new byte[1];
					bool flag = false;
					try
					{
						client.WriteLock.Wait();
						client.TcpClient.Client.Send(buffer, 0, SocketFlags.None);
						flag = true;
					}
					catch (SocketException ex)
					{
						if (ex.NativeErrorCode.Equals(10035))
						{
							flag = true;
						}
					}
					catch (Exception)
					{
					}
					finally
					{
						client?.WriteLock.Release();
					}
					if (flag)
					{
						return true;
					}
					try
					{
						client.WriteLock.Wait();
						if (client.TcpClient.Client.Poll(0, SelectMode.SelectWrite) && !client.TcpClient.Client.Poll(0, SelectMode.SelectError))
						{
							byte[] buffer2 = new byte[1];
							if (client.TcpClient.Client.Receive(buffer2, SocketFlags.Peek) == 0)
							{
								return false;
							}
							return true;
						}
						return false;
					}
					catch (Exception)
					{
						return false;
					}
					finally
					{
						client?.WriteLock.Release();
					}
				}
				return false;
			}
			return false;
		}

		private async Task DataReceiver(ClientMetadata client, CancellationToken token)
		{
			_ = 6;
			DateTime value2;
			while (true)
			{
				try
				{
					if (!IsConnected(client))
					{
						break;
					}
					WatsonMessage msg = new WatsonMessage(client.DataStream, _Settings.DebugMessages ? _Settings.Logger : null);
					if (!(await msg.BuildFromStream(token).ConfigureAwait(continueOnCapturedContext: false)))
					{
						_Settings?.Logger?.Invoke(Severity.Debug, _Header + "disconnect detected for client " + client.IpPort);
						break;
					}
					if (msg == null)
					{
						await Task.Delay(30, token).ConfigureAwait(continueOnCapturedContext: false);
						continue;
					}
					if (!string.IsNullOrEmpty(_Settings.PresharedKey) && _UnauthenticatedClients.ContainsKey(client.IpPort))
					{
						_Settings.Logger?.Invoke(Severity.Debug, _Header + "message received from unauthenticated endpoint " + client.IpPort);
						int contentLength = 0;
						Stream stream = null;
						if (msg.Status == MessageStatus.AuthRequested && msg.PresharedKey != null && msg.PresharedKey.Length != 0)
						{
							string value = Encoding.UTF8.GetString(msg.PresharedKey).Trim();
							if (_Settings.PresharedKey.Trim().Equals(value))
							{
								_Settings.Logger?.Invoke(Severity.Debug, _Header + "accepted authentication for " + client.IpPort);
								_UnauthenticatedClients.TryRemove(client.IpPort, out value2);
								_Events.HandleAuthenticationSucceeded(this, new AuthenticationSucceededEventArgs(client.IpPort));
								WatsonCommon.BytesToStream(Encoding.UTF8.GetBytes("Authentication successful"), 0, out contentLength, out stream);
								WatsonMessage watsonMessage = new WatsonMessage(null, contentLength, stream, syncRequest: false, syncResponse: false, null, null, _Settings.DebugMessages ? _Settings.Logger : null);
								watsonMessage.Status = MessageStatus.AuthSuccess;
								SendInternal(client, watsonMessage, 0L, null);
								continue;
							}
							_Settings.Logger?.Invoke(Severity.Warn, _Header + "declined authentication for " + client.IpPort);
							DisconnectClient(client.IpPort, MessageStatus.AuthFailure);
						}
						else
						{
							_Settings.Logger?.Invoke(Severity.Warn, _Header + "no authentication material for " + client.IpPort);
							DisconnectClient(client.IpPort, MessageStatus.AuthFailure);
						}
						break;
					}
					if (msg.Status == MessageStatus.Shutdown)
					{
						_Settings.Logger?.Invoke(Severity.Debug, _Header + "client " + client.IpPort + " is disconnecting");
						break;
					}
					if (msg.Status == MessageStatus.Removed)
					{
						_Settings.Logger?.Invoke(Severity.Debug, _Header + "sent disconnect notice to " + client.IpPort);
						break;
					}
					if (msg.SyncRequest.HasValue && msg.SyncRequest.Value)
					{
						DateTime expiration = WatsonCommon.GetExpirationTimestamp(msg);
						byte[] data = await WatsonCommon.ReadMessageDataAsync(msg, _Settings.StreamBufferSize).ConfigureAwait(continueOnCapturedContext: false);
						if (DateTime.Now < expiration)
						{
							SyncRequest req = new SyncRequest(client.IpPort, msg.ConversationGuid, msg.Expiration.Value, msg.Metadata, data);
							SyncResponse syncResponse = _Callbacks.HandleSyncRequestReceived(req);
							if (syncResponse != null)
							{
								WatsonCommon.BytesToStream(syncResponse.Data, 0, out var contentLength2, out var stream2);
								WatsonMessage msg2 = new WatsonMessage(syncResponse.Metadata, contentLength2, stream2, syncRequest: false, syncResponse: true, msg.Expiration.Value, msg.ConversationGuid, _Settings.DebugMessages ? _Settings.Logger : null);
								SendInternal(client, msg2, contentLength2, stream2);
							}
						}
						else
						{
							_Settings.Logger?.Invoke(Severity.Debug, _Header + "expired synchronous request received and discarded from " + client.IpPort);
						}
						goto IL_0a7b;
					}
					if (msg.SyncResponse.HasValue && msg.SyncResponse.Value)
					{
						byte[] data2 = await WatsonCommon.ReadMessageDataAsync(msg, _Settings.StreamBufferSize).ConfigureAwait(continueOnCapturedContext: false);
						if (DateTime.Now < msg.Expiration.Value)
						{
							lock (_SyncResponseLock)
							{
								this._SyncResponseReceived?.Invoke(this, new SyncResponseReceivedEventArgs(msg, data2));
							}
						}
						else
						{
							_Settings.Logger?.Invoke(Severity.Debug, _Header + "expired synchronous response received and discarded from " + client.IpPort);
						}
						goto IL_0a7b;
					}
					if (_Events.IsUsingMessages)
					{
						byte[] data3 = await WatsonCommon.ReadMessageDataAsync(msg, _Settings.StreamBufferSize).ConfigureAwait(continueOnCapturedContext: false);
						MessageReceivedEventArgs mr = new MessageReceivedEventArgs(client.IpPort, msg.Metadata, data3);
						await Task.Run(delegate
						{
							_Events.HandleMessageReceived(this, mr);
						}, token);
						goto IL_0a7b;
					}
					if (_Events.IsUsingStreams)
					{
						StreamReceivedEventArgs sr = null;
						if (msg.ContentLength >= _Settings.MaxProxiedStreamSize)
						{
							WatsonStream stream3 = new WatsonStream(msg.ContentLength, msg.DataStream);
							sr = new StreamReceivedEventArgs(client.IpPort, msg.Metadata, msg.ContentLength, stream3);
							_Events.HandleStreamReceived(this, sr);
						}
						else
						{
							MemoryStream stream4 = WatsonCommon.DataStreamToMemoryStream(msg.ContentLength, msg.DataStream, _Settings.StreamBufferSize);
							WatsonStream stream3 = new WatsonStream(msg.ContentLength, stream4);
							sr = new StreamReceivedEventArgs(client.IpPort, msg.Metadata, msg.ContentLength, stream3);
							await Task.Run(delegate
							{
								_Events.HandleStreamReceived(this, sr);
							}, token);
						}
						goto IL_0a7b;
					}
					_Settings.Logger?.Invoke(Severity.Error, _Header + "event handler not set for either MessageReceived or StreamReceived");
					goto end_IL_0035;
					IL_0a7b:
					_Statistics.IncrementReceivedMessages();
					_Statistics.AddReceivedBytes(msg.ContentLength);
					_ClientsLastSeen.AddOrUpdate(client.IpPort, DateTime.Now, (string key, DateTime dateTime) => DateTime.Now);
					continue;
					end_IL_0035:;
				}
				catch (ObjectDisposedException)
				{
				}
				catch (TaskCanceledException)
				{
				}
				catch (OperationCanceledException)
				{
				}
				catch (Exception ex4)
				{
					_Settings?.Logger?.Invoke(Severity.Error, _Header + "data receiver exception for " + client.IpPort + ":" + Environment.NewLine + SerializationHelper.SerializeJson(ex4, pretty: true) + Environment.NewLine);
					_Events?.HandleExceptionEncountered(this, new ExceptionEventArgs(ex4));
				}
				break;
			}
			if (_Settings != null && _Events != null)
			{
				DisconnectionEventArgs args = (_ClientsKicked.ContainsKey(client.IpPort) ? new DisconnectionEventArgs(client.IpPort, DisconnectReason.Removed) : ((!_ClientsTimedout.ContainsKey(client.IpPort)) ? new DisconnectionEventArgs(client.IpPort, DisconnectReason.Normal) : new DisconnectionEventArgs(client.IpPort, DisconnectReason.Timeout)));
				_Events.HandleClientDisconnected(this, args);
				_Clients.TryRemove(client.IpPort, out var _);
				_ClientsLastSeen.TryRemove(client.IpPort, out value2);
				_ClientsKicked.TryRemove(client.IpPort, out value2);
				_ClientsTimedout.TryRemove(client.IpPort, out value2);
				_UnauthenticatedClients.TryRemove(client.IpPort, out value2);
				Interlocked.Decrement(ref _Connections);
				_Settings?.Logger?.Invoke(Severity.Debug, _Header + "client " + client.IpPort + " disconnected");
				client.Dispose();
			}
		}

		private bool SendInternal(ClientMetadata client, WatsonMessage msg, long contentLength, Stream stream)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			if (contentLength > 0 && (stream == null || !stream.CanRead))
			{
				throw new ArgumentException("Cannot read from supplied stream.");
			}
			client.WriteLock.Wait();
			try
			{
				SendHeaders(client, msg);
				SendDataStream(client, contentLength, stream);
				_Statistics.IncrementSentMessages();
				_Statistics.AddSentBytes(contentLength);
				return true;
			}
			catch (TaskCanceledException)
			{
				return false;
			}
			catch (OperationCanceledException)
			{
				return false;
			}
			catch (Exception ex3)
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "failed to write message to " + client.IpPort + ": " + Environment.NewLine + SerializationHelper.SerializeJson(ex3, pretty: true));
				_Events.HandleExceptionEncountered(this, new ExceptionEventArgs(ex3));
				return false;
			}
			finally
			{
				client?.WriteLock.Release();
			}
		}

		private async Task<bool> SendInternalAsync(ClientMetadata client, WatsonMessage msg, long contentLength, Stream stream, CancellationToken token)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			if (contentLength > 0 && (stream == null || !stream.CanRead))
			{
				throw new ArgumentException("Cannot read from supplied stream.");
			}
			if (token == default(CancellationToken))
			{
				CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, _Token);
				token = cancellationTokenSource.Token;
			}
			await client.WriteLock.WaitAsync(token).ConfigureAwait(continueOnCapturedContext: false);
			try
			{
				await SendHeadersAsync(client, msg, token).ConfigureAwait(continueOnCapturedContext: false);
				await SendDataStreamAsync(client, contentLength, stream, token).ConfigureAwait(continueOnCapturedContext: false);
				_Statistics.IncrementSentMessages();
				_Statistics.AddSentBytes(contentLength);
				return true;
			}
			catch (TaskCanceledException)
			{
				return false;
			}
			catch (OperationCanceledException)
			{
				return false;
			}
			catch (Exception ex3)
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "failed to write message to " + client.IpPort + ": " + Environment.NewLine + SerializationHelper.SerializeJson(ex3, pretty: true));
				_Events.HandleExceptionEncountered(this, new ExceptionEventArgs(ex3));
				return false;
			}
			finally
			{
				client?.WriteLock.Release();
			}
		}

		private SyncResponse SendAndWaitInternal(ClientMetadata client, WatsonMessage msg, int timeoutMs, long contentLength, Stream stream)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			if (contentLength > 0 && (stream == null || !stream.CanRead))
			{
				throw new ArgumentException("Cannot read from supplied stream.");
			}
			client.WriteLock.Wait();
			SyncResponse ret = null;
			AutoResetEvent Responded = new AutoResetEvent(initialState: false);
			EventHandler<SyncResponseReceivedEventArgs> value = delegate(object sender, SyncResponseReceivedEventArgs e)
			{
				if (e.Message.ConversationGuid == msg.ConversationGuid)
				{
					ret = new SyncResponse(e.Message.Expiration.Value, e.Message.Metadata, e.Data);
					Responded.Set();
				}
			};
			_SyncResponseReceived += value;
			try
			{
				SendHeaders(client, msg);
				SendDataStream(client, contentLength, stream);
				_Statistics.IncrementSentMessages();
				_Statistics.AddSentBytes(contentLength);
			}
			catch (Exception ex)
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "failed to write message to " + client.IpPort + " due to exception: " + Environment.NewLine + SerializationHelper.SerializeJson(ex, pretty: true));
				_Events.HandleExceptionEncountered(this, new ExceptionEventArgs(ex));
				throw;
			}
			finally
			{
				client?.WriteLock.Release();
			}
			Responded.WaitOne(new TimeSpan(0, 0, 0, 0, timeoutMs));
			_SyncResponseReceived -= value;
			if (ret != null)
			{
				return ret;
			}
			_Settings.Logger?.Invoke(Severity.Error, _Header + "synchronous response not received within the timeout window");
			throw new TimeoutException("A response to a synchronous request was not received within the timeout window.");
		}

		private void SendHeaders(ClientMetadata client, WatsonMessage msg)
		{
			byte[] headerBytes = msg.HeaderBytes;
			client.DataStream.Write(headerBytes, 0, headerBytes.Length);
			client.DataStream.Flush();
		}

		private async Task SendHeadersAsync(ClientMetadata client, WatsonMessage msg, CancellationToken token)
		{
			byte[] headerBytes = msg.HeaderBytes;
			await client.DataStream.WriteAsync(headerBytes, 0, headerBytes.Length, token).ConfigureAwait(continueOnCapturedContext: false);
			await client.DataStream.FlushAsync(token).ConfigureAwait(continueOnCapturedContext: false);
		}

		private void SendDataStream(ClientMetadata client, long contentLength, Stream stream)
		{
			if (contentLength <= 0)
			{
				return;
			}
			long num = contentLength;
			int num2 = 0;
			if (_Settings.StreamBufferSize != client.SendBuffer.Length)
			{
				client.SendBuffer = new byte[_Settings.StreamBufferSize];
			}
			while (num > 0)
			{
				num2 = stream.Read(client.SendBuffer, 0, client.SendBuffer.Length);
				if (num2 > 0)
				{
					client.DataStream.Write(client.SendBuffer, 0, num2);
					num -= num2;
				}
			}
			client.DataStream.Flush();
		}

		private async Task SendDataStreamAsync(ClientMetadata client, long contentLength, Stream stream, CancellationToken token)
		{
			if (contentLength <= 0)
			{
				return;
			}
			long bytesRemaining = contentLength;
			if (_Settings.StreamBufferSize != client.SendBuffer.Length)
			{
				client.SendBuffer = new byte[_Settings.StreamBufferSize];
			}
			while (bytesRemaining > 0)
			{
				int bytesRead = await stream.ReadAsync(client.SendBuffer, 0, client.SendBuffer.Length, token).ConfigureAwait(continueOnCapturedContext: false);
				if (bytesRead > 0)
				{
					await client.DataStream.WriteAsync(client.SendBuffer, 0, bytesRead, token).ConfigureAwait(continueOnCapturedContext: false);
					bytesRemaining -= bytesRead;
				}
			}
			await client.DataStream.FlushAsync(token).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async Task MonitorForIdleClients()
		{
			try
			{
				while (true)
				{
					await Task.Delay(5000, _Token).ConfigureAwait(continueOnCapturedContext: false);
					if (_Settings.IdleClientTimeoutSeconds <= 0 || _ClientsLastSeen.Count <= 0)
					{
						continue;
					}
					DateTime dateTime = DateTime.Now.AddSeconds(-1 * _Settings.IdleClientTimeoutSeconds);
					foreach (KeyValuePair<string, DateTime> item in _ClientsLastSeen)
					{
						if (item.Value < dateTime)
						{
							_ClientsTimedout.TryAdd(item.Key, DateTime.Now);
							_Settings.Logger?.Invoke(Severity.Debug, _Header + "disconnecting client " + item.Key + " due to idle timeout");
							DisconnectClient(item.Key, MessageStatus.Timeout);
						}
					}
				}
			}
			catch (TaskCanceledException)
			{
			}
			catch (OperationCanceledException)
			{
			}
		}
	}
}
