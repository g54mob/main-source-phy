using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WatsonTcp
{
	public class WatsonTcpClient : IDisposable
	{
		private string _Header = "[WatsonTcpClient] ";

		private WatsonTcpClientSettings _Settings = new WatsonTcpClientSettings();

		private WatsonTcpClientEvents _Events = new WatsonTcpClientEvents();

		private WatsonTcpClientCallbacks _Callbacks = new WatsonTcpClientCallbacks();

		private WatsonTcpStatistics _Statistics = new WatsonTcpStatistics();

		private WatsonTcpKeepaliveSettings _Keepalive = new WatsonTcpKeepaliveSettings();

		private WatsonTcpClientSslConfiguration _SslConfiguration = new WatsonTcpClientSslConfiguration();

		private Mode _Mode;

		private TlsVersion _TlsVersion;

		private string _SourceIp;

		private int _SourcePort;

		private string _ServerIp;

		private int _ServerPort;

		private TcpClient _Client;

		private Stream _DataStream;

		private NetworkStream _TcpStream;

		private SslStream _SslStream;

		private X509Certificate2 _SslCertificate;

		private X509Certificate2Collection _SslCertificateCollection;

		private SemaphoreSlim _WriteLock = new SemaphoreSlim(1, 1);

		private SemaphoreSlim _ReadLock = new SemaphoreSlim(1, 1);

		private CancellationTokenSource _TokenSource = new CancellationTokenSource();

		private CancellationToken _Token;

		private Task _DataReceiver;

		private Task _IdleServerMonitor;

		private DateTime _LastActivity = DateTime.Now;

		private bool _IsTimeout;

		private byte[] _SendBuffer = new byte[65536];

		private readonly object _SyncResponseLock = new object();

		public WatsonTcpClientSettings Settings
		{
			get
			{
				return _Settings;
			}
			set
			{
				if (value == null)
				{
					_Settings = new WatsonTcpClientSettings();
				}
				else
				{
					_Settings = value;
				}
			}
		}

		public WatsonTcpClientEvents Events
		{
			get
			{
				return _Events;
			}
			set
			{
				if (value == null)
				{
					_Events = new WatsonTcpClientEvents();
				}
				else
				{
					_Events = value;
				}
			}
		}

		public WatsonTcpClientCallbacks Callbacks
		{
			get
			{
				return _Callbacks;
			}
			set
			{
				if (value == null)
				{
					_Callbacks = new WatsonTcpClientCallbacks();
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

		public WatsonTcpClientSslConfiguration SslConfiguration
		{
			get
			{
				return _SslConfiguration;
			}
			set
			{
				if (value == null)
				{
					_SslConfiguration = new WatsonTcpClientSslConfiguration();
				}
				else
				{
					_SslConfiguration = value;
				}
			}
		}

		public bool Connected { get; private set; }

		private event EventHandler<SyncResponseReceivedEventArgs> _SyncResponseReceived;

		public WatsonTcpClient(string serverIp, int serverPort)
		{
			if (string.IsNullOrEmpty(serverIp))
			{
				throw new ArgumentNullException("serverIp");
			}
			if (serverPort < 0)
			{
				throw new ArgumentOutOfRangeException("serverPort");
			}
			_Mode = Mode.Tcp;
			_ServerIp = serverIp;
			_ServerPort = serverPort;
			_SendBuffer = new byte[_Settings.StreamBufferSize];
			SerializationHelper.InstantiateConverter();
		}

		public WatsonTcpClient(string serverIp, int serverPort, string pfxCertFile, string pfxCertPass, TlsVersion tlsVersion = TlsVersion.Tls12)
		{
			if (string.IsNullOrEmpty(serverIp))
			{
				throw new ArgumentNullException("serverIp");
			}
			if (serverPort < 0)
			{
				throw new ArgumentOutOfRangeException("serverPort");
			}
			_Mode = Mode.Ssl;
			_TlsVersion = tlsVersion;
			_ServerIp = serverIp;
			_ServerPort = serverPort;
			_SendBuffer = new byte[_Settings.StreamBufferSize];
			if (!string.IsNullOrEmpty(pfxCertFile))
			{
				if (string.IsNullOrEmpty(pfxCertPass))
				{
					_SslCertificate = new X509Certificate2(pfxCertFile);
				}
				else
				{
					_SslCertificate = new X509Certificate2(pfxCertFile, pfxCertPass);
				}
				_SslCertificateCollection = new X509Certificate2Collection { _SslCertificate };
			}
			else
			{
				_SslCertificateCollection = new X509Certificate2Collection();
			}
			SerializationHelper.InstantiateConverter();
		}

		public WatsonTcpClient(string serverIp, int serverPort, X509Certificate2 cert, TlsVersion tlsVersion = TlsVersion.Tls12)
		{
			if (string.IsNullOrEmpty(serverIp))
			{
				throw new ArgumentNullException("serverIp");
			}
			if (serverPort < 0)
			{
				throw new ArgumentOutOfRangeException("serverPort");
			}
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			_Mode = Mode.Ssl;
			_TlsVersion = tlsVersion;
			_SslCertificate = cert;
			_ServerIp = serverIp;
			_ServerPort = serverPort;
			_SendBuffer = new byte[_Settings.StreamBufferSize];
			_SslCertificateCollection = new X509Certificate2Collection { _SslCertificate };
			SerializationHelper.InstantiateConverter();
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public void Connect()
		{
			if (Connected)
			{
				throw new InvalidOperationException("Already connected to the server.");
			}
			if (_Settings.LocalPort == 0)
			{
				_Client = new TcpClient();
			}
			else
			{
				IPEndPoint localEP = new IPEndPoint(IPAddress.Any, _Settings.LocalPort);
				_Client = new TcpClient(localEP);
			}
			_Client.NoDelay = _Settings.NoDelay;
			_Statistics = new WatsonTcpStatistics();
			IAsyncResult asyncResult = null;
			WaitHandle waitHandle = null;
			if (!_Events.IsUsingMessages && !_Events.IsUsingStreams)
			{
				throw new InvalidOperationException("One of either 'MessageReceived' or 'StreamReceived' events must first be set.");
			}
			if (_Mode == Mode.Tcp)
			{
				_Settings.Logger?.Invoke(Severity.Info, _Header + "connecting to " + _ServerIp + ":" + _ServerPort);
				_Client.LingerState = new LingerOption(enable: true, 0);
				asyncResult = _Client.BeginConnect(_ServerIp, _ServerPort, null, null);
				waitHandle = asyncResult.AsyncWaitHandle;
				try
				{
					if (!waitHandle.WaitOne(TimeSpan.FromSeconds(_Settings.ConnectTimeoutSeconds), exitContext: false))
					{
						_Client.Close();
						_Settings.Logger?.Invoke(Severity.Error, _Header + "timeout connecting to " + _ServerIp + ":" + _ServerPort);
						throw new TimeoutException("Timeout connecting to " + _ServerIp + ":" + _ServerPort);
					}
					_Client.EndConnect(asyncResult);
					_SourceIp = ((IPEndPoint)_Client.Client.LocalEndPoint).Address.ToString();
					_SourcePort = ((IPEndPoint)_Client.Client.LocalEndPoint).Port;
					_TcpStream = _Client.GetStream();
					_DataStream = _TcpStream;
					_SslStream = null;
					if (_Keepalive.EnableTcpKeepAlives)
					{
						EnableKeepalives();
					}
					Connected = true;
				}
				catch (Exception ex)
				{
					_Settings.Logger?.Invoke(Severity.Error, _Header + "exception encountered: " + Environment.NewLine + SerializationHelper.SerializeJson(ex, pretty: true));
					_Events.HandleExceptionEncountered(this, new ExceptionEventArgs(ex));
					throw;
				}
			}
			else
			{
				if (_Mode != Mode.Ssl)
				{
					throw new ArgumentException("Unknown mode: " + _Mode);
				}
				_Settings.Logger?.Invoke(Severity.Info, _Header + "connecting with SSL to " + _ServerIp + ":" + _ServerPort);
				_Client.LingerState = new LingerOption(enable: true, 0);
				asyncResult = _Client.BeginConnect(_ServerIp, _ServerPort, null, null);
				waitHandle = asyncResult.AsyncWaitHandle;
				try
				{
					if (!waitHandle.WaitOne(TimeSpan.FromSeconds(_Settings.ConnectTimeoutSeconds), exitContext: false))
					{
						_Client.Close();
						_Settings.Logger?.Invoke(Severity.Error, _Header + "timeout connecting to " + _ServerIp + ":" + _ServerPort);
						throw new TimeoutException("Timeout connecting to " + _ServerIp + ":" + _ServerPort);
					}
					_Client.EndConnect(asyncResult);
					_SourceIp = ((IPEndPoint)_Client.Client.LocalEndPoint).Address.ToString();
					_SourcePort = ((IPEndPoint)_Client.Client.LocalEndPoint).Port;
					if (_Settings.AcceptInvalidCertificates)
					{
						_SslStream = new SslStream(_Client.GetStream(), leaveInnerStreamOpen: false, _SslConfiguration.ServerCertificateValidationCallback, _SslConfiguration.ClientCertificateSelectionCallback);
					}
					else
					{
						_SslStream = new SslStream(_Client.GetStream(), leaveInnerStreamOpen: false);
					}
					_SslStream.AuthenticateAsClient(_ServerIp, _SslCertificateCollection, _TlsVersion.ToSslProtocols(), !_Settings.AcceptInvalidCertificates);
					if (!_SslStream.IsEncrypted)
					{
						_Settings.Logger?.Invoke(Severity.Error, _Header + "stream to " + _ServerIp + ":" + _ServerPort + " is not encrypted");
						throw new AuthenticationException("Stream is not encrypted");
					}
					if (!_SslStream.IsAuthenticated)
					{
						_Settings.Logger?.Invoke(Severity.Error, _Header + "stream to " + _ServerIp + ":" + _ServerPort + " is not authenticated");
						throw new AuthenticationException("Stream is not authenticated");
					}
					if (_Settings.MutuallyAuthenticate && !_SslStream.IsMutuallyAuthenticated)
					{
						_Settings.Logger?.Invoke(Severity.Error, _Header + "mutual authentication with " + _ServerIp + ":" + _ServerPort + " failed");
						throw new AuthenticationException("Mutual authentication failed");
					}
					_DataStream = _SslStream;
					Connected = true;
				}
				catch (Exception ex2)
				{
					_Settings.Logger?.Invoke(Severity.Error, _Header + "exception encountered: " + Environment.NewLine + SerializationHelper.SerializeJson(ex2, pretty: true));
					_Events.HandleExceptionEncountered(this, new ExceptionEventArgs(ex2));
					throw;
				}
			}
			_TokenSource = new CancellationTokenSource();
			_Token = _TokenSource.Token;
			_LastActivity = DateTime.Now;
			_IsTimeout = false;
			_DataReceiver = Task.Run(() => DataReceiver(), _Token);
			_IdleServerMonitor = Task.Run(() => IdleServerMonitor(), _Token);
			_Events.HandleServerConnected(this, new ConnectionEventArgs(_ServerIp + ":" + _ServerPort));
			_Settings.Logger?.Invoke(Severity.Info, _Header + "connected to " + _ServerIp + ":" + _ServerPort);
		}

		public void Disconnect(bool sendNotice = true)
		{
			if (!Connected)
			{
				throw new InvalidOperationException("Not connected to the server.");
			}
			_Settings.Logger?.Invoke(Severity.Info, _Header + "disconnecting from " + _ServerIp + ":" + _ServerPort);
			if (Connected && sendNotice)
			{
				WatsonMessage watsonMessage = new WatsonMessage();
				watsonMessage.Status = MessageStatus.Shutdown;
				SendInternal(watsonMessage, 0L, null);
			}
			if (_TokenSource != null && !_TokenSource.IsCancellationRequested)
			{
				_TokenSource.Cancel();
				_TokenSource.Dispose();
			}
			if (_SslStream != null)
			{
				_SslStream.Close();
			}
			if (_TcpStream != null)
			{
				_TcpStream.Close();
			}
			if (_Client != null)
			{
				_Client.Close();
			}
			while (true)
			{
				Task dataReceiver = _DataReceiver;
				if (dataReceiver != null && dataReceiver.Status == TaskStatus.Running)
				{
					Task.Delay(10).Wait();
					continue;
				}
				break;
			}
			while (true)
			{
				Task idleServerMonitor = _IdleServerMonitor;
				if (idleServerMonitor == null || idleServerMonitor.Status != TaskStatus.Running)
				{
					break;
				}
				Task.Delay(10).Wait();
			}
			Connected = false;
			_Settings.Logger?.Invoke(Severity.Info, _Header + "disconnected from " + _ServerIp + ":" + _ServerPort);
		}

		public void Authenticate(string presharedKey)
		{
			if (string.IsNullOrEmpty(presharedKey))
			{
				throw new ArgumentNullException("presharedKey");
			}
			if (presharedKey.Length != 16)
			{
				throw new ArgumentException("Preshared key length must be 16 bytes.");
			}
			WatsonMessage watsonMessage = new WatsonMessage();
			watsonMessage.Status = MessageStatus.AuthRequested;
			watsonMessage.PresharedKey = Encoding.UTF8.GetBytes(presharedKey);
			SendInternal(watsonMessage, 0L, null);
		}

		public bool Send(string data, Dictionary<object, object> metadata = null)
		{
			if (string.IsNullOrEmpty(data))
			{
				return Send(new byte[0], metadata);
			}
			return Send(Encoding.UTF8.GetBytes(data), metadata);
		}

		public bool Send(byte[] data, Dictionary<object, object> metadata = null, int start = 0)
		{
			if (data == null)
			{
				data = new byte[0];
			}
			WatsonCommon.BytesToStream(data, start, out var contentLength, out var stream);
			return Send(contentLength, stream, metadata);
		}

		public bool Send(long contentLength, Stream stream, Dictionary<object, object> metadata = null)
		{
			if (contentLength < 0)
			{
				throw new ArgumentException("Content length must be zero or greater.");
			}
			if (stream == null)
			{
				stream = new MemoryStream(new byte[0]);
			}
			WatsonMessage msg = new WatsonMessage(metadata, contentLength, stream, syncRequest: false, syncResponse: false, null, null, _Settings.DebugMessages ? _Settings.Logger : null);
			return SendInternal(msg, contentLength, stream);
		}

		public async Task<bool> SendAsync(string data, Dictionary<object, object> metadata = null, CancellationToken token = default(CancellationToken))
		{
			if (string.IsNullOrEmpty(data))
			{
				return await SendAsync(new byte[0], metadata);
			}
			if (token == default(CancellationToken))
			{
				token = _Token;
			}
			return await SendAsync(Encoding.UTF8.GetBytes(data), metadata, 0, token).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task<bool> SendAsync(byte[] data, Dictionary<object, object> metadata = null, int start = 0, CancellationToken token = default(CancellationToken))
		{
			if (token == default(CancellationToken))
			{
				token = _Token;
			}
			if (data == null)
			{
				data = new byte[0];
			}
			WatsonCommon.BytesToStream(data, start, out var contentLength, out var stream);
			return await SendAsync(contentLength, stream, metadata, token).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task<bool> SendAsync(long contentLength, Stream stream, Dictionary<object, object> metadata = null, CancellationToken token = default(CancellationToken))
		{
			if (contentLength < 0)
			{
				throw new ArgumentException("Content length must be zero or greater.");
			}
			if (token == default(CancellationToken))
			{
				token = _Token;
			}
			if (stream == null)
			{
				stream = new MemoryStream(new byte[0]);
			}
			WatsonMessage msg = new WatsonMessage(metadata, contentLength, stream, syncRequest: false, syncResponse: false, null, null, _Settings.DebugMessages ? _Settings.Logger : null);
			return await SendInternalAsync(msg, contentLength, stream, token).ConfigureAwait(continueOnCapturedContext: false);
		}

		public SyncResponse SendAndWait(int timeoutMs, string data, Dictionary<object, object> metadata = null)
		{
			if (timeoutMs < 1000)
			{
				throw new ArgumentException("Timeout milliseconds must be 1000 or greater.");
			}
			if (string.IsNullOrEmpty(data))
			{
				return SendAndWait(timeoutMs, new byte[0], metadata);
			}
			return SendAndWait(timeoutMs, Encoding.UTF8.GetBytes(data), metadata);
		}

		public SyncResponse SendAndWait(int timeoutMs, byte[] data, Dictionary<object, object> metadata = null, int start = 0)
		{
			if (timeoutMs < 1000)
			{
				throw new ArgumentException("Timeout milliseconds must be 1000 or greater.");
			}
			if (data == null)
			{
				data = new byte[0];
			}
			DateTime.Now.AddMilliseconds(timeoutMs);
			WatsonCommon.BytesToStream(data, start, out var contentLength, out var stream);
			return SendAndWait(timeoutMs, contentLength, stream, metadata);
		}

		public SyncResponse SendAndWait(int timeoutMs, long contentLength, Stream stream, Dictionary<object, object> metadata = null)
		{
			if (contentLength < 0)
			{
				throw new ArgumentException("Content length must be zero or greater.");
			}
			if (timeoutMs < 1000)
			{
				throw new ArgumentException("Timeout milliseconds must be 1000 or greater.");
			}
			if (stream == null)
			{
				stream = new MemoryStream(new byte[0]);
			}
			DateTime value = DateTime.Now.AddMilliseconds(timeoutMs);
			WatsonMessage msg = new WatsonMessage(metadata, contentLength, stream, syncRequest: true, syncResponse: false, value, Guid.NewGuid().ToString(), _Settings.DebugMessages ? _Settings.Logger : null);
			return SendAndWaitInternal(msg, timeoutMs, contentLength, stream);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_Settings.Logger?.Invoke(Severity.Info, _Header + "disposing");
				if (Connected)
				{
					Disconnect();
				}
				if (_WriteLock != null)
				{
					_WriteLock.Dispose();
				}
				if (_ReadLock != null)
				{
					_ReadLock.Dispose();
				}
				_Settings = null;
				_Events = null;
				_Callbacks = null;
				_Statistics = null;
				_Keepalive = null;
				_SslConfiguration = null;
				_SourceIp = null;
				_ServerIp = null;
				_Client = null;
				_DataStream = null;
				_TcpStream = null;
				_SslStream = null;
				_SslCertificate = null;
				_SslCertificateCollection = null;
				_WriteLock = null;
				_ReadLock = null;
				_DataReceiver = null;
			}
		}

		private void EnableKeepalives()
		{
		}

		private async Task DataReceiver()
		{
			DisconnectReason reason = DisconnectReason.Normal;
			while (true)
			{
				try
				{
					if (_Client == null || !_Client.Connected)
					{
						_Settings?.Logger?.Invoke(Severity.Debug, _Header + "disconnect detected");
						break;
					}
					await _ReadLock.WaitAsync(_Token);
					WatsonMessage msg = new WatsonMessage(_DataStream, _Settings.DebugMessages ? _Settings.Logger : null);
					if (!(await msg.BuildFromStream(_Token).ConfigureAwait(continueOnCapturedContext: false)))
					{
						_Settings?.Logger?.Invoke(Severity.Debug, _Header + "disconnect detected");
						break;
					}
					if (msg == null)
					{
						await Task.Delay(30, _Token).ConfigureAwait(continueOnCapturedContext: false);
						continue;
					}
					_LastActivity = DateTime.Now;
					if (msg.Status == MessageStatus.Removed)
					{
						_Settings?.Logger?.Invoke(Severity.Info, _Header + "disconnect due to server-side removal");
						reason = DisconnectReason.Removed;
						break;
					}
					if (msg.Status == MessageStatus.Shutdown)
					{
						_Settings?.Logger?.Invoke(Severity.Info, _Header + "disconnect due to server shutdown");
						reason = DisconnectReason.Shutdown;
						break;
					}
					if (msg.Status == MessageStatus.Timeout)
					{
						_Settings?.Logger?.Invoke(Severity.Info, _Header + "disconnect due to timeout");
						reason = DisconnectReason.Timeout;
						break;
					}
					if (msg.Status == MessageStatus.AuthSuccess)
					{
						_Settings.Logger?.Invoke(Severity.Debug, _Header + "authentication successful");
						Task.Run(delegate
						{
							_Events.HandleAuthenticationSucceeded(this, EventArgs.Empty);
						}, _Token);
						continue;
					}
					if (msg.Status == MessageStatus.AuthFailure)
					{
						_Settings.Logger?.Invoke(Severity.Error, _Header + "authentication failed");
						reason = DisconnectReason.AuthFailure;
						Task.Run(delegate
						{
							_Events.HandleAuthenticationFailure(this, EventArgs.Empty);
						}, _Token);
						break;
					}
					if (msg.Status == MessageStatus.AuthRequired)
					{
						_Settings.Logger?.Invoke(Severity.Info, _Header + "authentication required by server; please authenticate using pre-shared key");
						string text = _Callbacks.HandleAuthenticationRequested();
						if (!string.IsNullOrEmpty(text))
						{
							Authenticate(text);
						}
						continue;
					}
					if (msg.SyncRequest.HasValue && msg.SyncRequest.Value)
					{
						DateTime expiration = WatsonCommon.GetExpirationTimestamp(msg);
						byte[] data = await WatsonCommon.ReadMessageDataAsync(msg, _Settings.StreamBufferSize).ConfigureAwait(continueOnCapturedContext: false);
						if (DateTime.Now < expiration)
						{
							SyncRequest req = new SyncRequest(_ServerIp + ":" + _ServerPort, msg.ConversationGuid, msg.Expiration.Value, msg.Metadata, data);
							SyncResponse syncResponse = _Callbacks.HandleSyncRequestReceived(req);
							if (syncResponse != null)
							{
								WatsonCommon.BytesToStream(syncResponse.Data, 0, out var contentLength, out var stream);
								WatsonMessage msg2 = new WatsonMessage(syncResponse.Metadata, contentLength, stream, syncRequest: false, syncResponse: true, msg.Expiration.Value, msg.ConversationGuid, _Settings.DebugMessages ? _Settings.Logger : null);
								SendInternal(msg2, contentLength, stream);
							}
						}
						else
						{
							_Settings.Logger?.Invoke(Severity.Debug, _Header + "expired synchronous request received and discarded");
						}
						goto IL_09e9;
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
							_Settings.Logger?.Invoke(Severity.Debug, _Header + "expired synchronous response received and discarded");
						}
						goto IL_09e9;
					}
					if (_Events.IsUsingMessages)
					{
						byte[] data3 = await WatsonCommon.ReadMessageDataAsync(msg, _Settings.StreamBufferSize).ConfigureAwait(continueOnCapturedContext: false);
						MessageReceivedEventArgs args = new MessageReceivedEventArgs(_ServerIp + ":" + _ServerPort, msg.Metadata, data3);
						await Task.Run(delegate
						{
							_Events.HandleMessageReceived(this, args);
						});
						goto IL_09e9;
					}
					if (_Events.IsUsingStreams)
					{
						StreamReceivedEventArgs sr = null;
						if (msg.ContentLength >= _Settings.MaxProxiedStreamSize)
						{
							WatsonStream stream2 = new WatsonStream(msg.ContentLength, msg.DataStream);
							sr = new StreamReceivedEventArgs(_ServerIp + ":" + _ServerPort, msg.Metadata, msg.ContentLength, stream2);
							_Events.HandleStreamReceived(this, sr);
						}
						else
						{
							MemoryStream stream3 = WatsonCommon.DataStreamToMemoryStream(msg.ContentLength, msg.DataStream, _Settings.StreamBufferSize);
							WatsonStream stream2 = new WatsonStream(msg.ContentLength, stream3);
							sr = new StreamReceivedEventArgs(_ServerIp + ":" + _ServerPort, msg.Metadata, msg.ContentLength, stream2);
							Task.Run(delegate
							{
								_Events.HandleStreamReceived(this, sr);
							}, _Token);
						}
						goto IL_09e9;
					}
					_Settings.Logger?.Invoke(Severity.Error, _Header + "event handler not set for either MessageReceived or StreamReceived");
					goto end_IL_003c;
					IL_09e9:
					_Statistics.IncrementReceivedMessages();
					_Statistics.AddReceivedBytes(msg.ContentLength);
					continue;
					end_IL_003c:;
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
					_Settings?.Logger?.Invoke(Severity.Error, _Header + "data receiver exception for " + _ServerIp + ":" + _ServerPort + ":" + Environment.NewLine + SerializationHelper.SerializeJson(ex4, pretty: true) + Environment.NewLine);
					_Events?.HandleExceptionEncountered(this, new ExceptionEventArgs(ex4));
				}
				finally
				{
					if (_ReadLock != null)
					{
						_ReadLock.Release();
					}
				}
				break;
			}
			Connected = false;
			if (_IsTimeout)
			{
				reason = DisconnectReason.Timeout;
			}
			_Settings?.Logger?.Invoke(Severity.Debug, _Header + "data receiver terminated for " + _ServerIp + ":" + _ServerPort);
			_Events?.HandleServerDisconnected(this, new DisconnectionEventArgs(_ServerIp + ":" + _ServerPort, reason));
		}

		private bool SendInternal(WatsonMessage msg, long contentLength, Stream stream)
		{
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			if (!Connected)
			{
				return false;
			}
			if (contentLength > 0 && (stream == null || !stream.CanRead))
			{
				throw new ArgumentException("Cannot read from supplied stream.");
			}
			bool flag = false;
			if (_Client == null || !_Client.Connected)
			{
				flag = true;
				return false;
			}
			_WriteLock.Wait();
			try
			{
				SendHeaders(msg);
				SendDataStream(contentLength, stream);
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
				_Settings.Logger?.Invoke(Severity.Error, _Header + "failed to write message to " + _ServerIp + ":" + _ServerPort + ":" + Environment.NewLine + SerializationHelper.SerializeJson(ex3, pretty: true));
				_Events.HandleExceptionEncountered(this, new ExceptionEventArgs(ex3));
				flag = true;
				return false;
			}
			finally
			{
				_WriteLock.Release();
				if (flag)
				{
					Connected = false;
					Dispose();
				}
			}
		}

		private async Task<bool> SendInternalAsync(WatsonMessage msg, long contentLength, Stream stream, CancellationToken token)
		{
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			if (!Connected)
			{
				return false;
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
			bool disconnectDetected = false;
			if (_Client == null || !_Client.Connected)
			{
				return false;
			}
			await _WriteLock.WaitAsync(token).ConfigureAwait(continueOnCapturedContext: false);
			try
			{
				await SendHeadersAsync(msg, token).ConfigureAwait(continueOnCapturedContext: false);
				await SendDataStreamAsync(contentLength, stream, token).ConfigureAwait(continueOnCapturedContext: false);
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
				_Settings.Logger?.Invoke(Severity.Error, _Header + "failed to write message to " + _ServerIp + ":" + _ServerPort + ":" + Environment.NewLine + ex3.ToString() + Environment.NewLine);
				disconnectDetected = true;
				return false;
			}
			finally
			{
				_WriteLock.Release();
				if (disconnectDetected)
				{
					Connected = false;
					Dispose();
				}
			}
		}

		private SyncResponse SendAndWaitInternal(WatsonMessage msg, int timeoutMs, long contentLength, Stream stream)
		{
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			if (!Connected)
			{
				throw new InvalidOperationException("Client is not connected to the server.");
			}
			if (contentLength > 0 && (stream == null || !stream.CanRead))
			{
				throw new ArgumentException("Cannot read from supplied stream.");
			}
			bool flag = false;
			if (_Client == null || !_Client.Connected)
			{
				flag = true;
				throw new InvalidOperationException("Client is not connected to the server.");
			}
			_WriteLock.Wait();
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
				SendHeaders(msg);
				SendDataStream(contentLength, stream);
				_Statistics.IncrementSentMessages();
				_Statistics.AddSentBytes(contentLength);
			}
			catch (TaskCanceledException)
			{
				return null;
			}
			catch (OperationCanceledException)
			{
				return null;
			}
			catch (Exception ex3)
			{
				_Settings.Logger?.Invoke(Severity.Error, _Header + "failed to write message to " + _ServerIp + ":" + _ServerPort + ":" + Environment.NewLine + ex3.ToString() + Environment.NewLine);
				flag = true;
				throw;
			}
			finally
			{
				_WriteLock.Release();
				if (flag)
				{
					Connected = false;
					Dispose();
				}
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

		private void SendHeaders(WatsonMessage msg)
		{
			byte[] headerBytes = msg.HeaderBytes;
			_DataStream.Write(headerBytes, 0, headerBytes.Length);
			_DataStream.Flush();
		}

		private async Task SendHeadersAsync(WatsonMessage msg, CancellationToken token)
		{
			byte[] headerBytes = msg.HeaderBytes;
			await _DataStream.WriteAsync(headerBytes, 0, headerBytes.Length, token).ConfigureAwait(continueOnCapturedContext: false);
			await _DataStream.FlushAsync(token).ConfigureAwait(continueOnCapturedContext: false);
		}

		private void SendDataStream(long contentLength, Stream stream)
		{
			if (contentLength <= 0)
			{
				return;
			}
			long num = contentLength;
			int num2 = 0;
			if (_Settings.StreamBufferSize != _SendBuffer.Length)
			{
				_SendBuffer = new byte[_Settings.StreamBufferSize];
			}
			while (num > 0)
			{
				num2 = stream.Read(_SendBuffer, 0, _SendBuffer.Length);
				if (num2 > 0)
				{
					_DataStream.Write(_SendBuffer, 0, num2);
					num -= num2;
				}
			}
			_DataStream.Flush();
		}

		private async Task SendDataStreamAsync(long contentLength, Stream stream, CancellationToken token)
		{
			if (contentLength <= 0)
			{
				return;
			}
			long bytesRemaining = contentLength;
			if (_Settings.StreamBufferSize != _SendBuffer.Length)
			{
				_SendBuffer = new byte[_Settings.StreamBufferSize];
			}
			while (bytesRemaining > 0)
			{
				int bytesRead = await stream.ReadAsync(_SendBuffer, 0, _SendBuffer.Length, token).ConfigureAwait(continueOnCapturedContext: false);
				if (bytesRead > 0)
				{
					await _DataStream.WriteAsync(_SendBuffer, 0, bytesRead, token).ConfigureAwait(continueOnCapturedContext: false);
					bytesRemaining -= bytesRead;
				}
			}
			await _DataStream.FlushAsync(token).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async Task IdleServerMonitor()
		{
			while (!_Token.IsCancellationRequested)
			{
				await Task.Delay(_Settings.IdleServerEvaluationIntervalMs, _Token).ConfigureAwait(continueOnCapturedContext: false);
				if (_Settings.IdleServerTimeoutMs != 0)
				{
					DateTime dateTime = _LastActivity.AddMilliseconds(_Settings.IdleServerTimeoutMs);
					if (DateTime.Now > dateTime)
					{
						_Settings.Logger?.Invoke(Severity.Warn, _Header + "disconnecting from " + _ServerIp + ":" + _ServerPort + " due to timeout");
						_IsTimeout = true;
						Disconnect();
					}
				}
			}
		}
	}
}
