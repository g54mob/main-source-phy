using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace WatsonTcp
{
	internal class ClientMetadata : IDisposable
	{
		internal SemaphoreSlim WriteLock = new SemaphoreSlim(1, 1);

		internal SemaphoreSlim ReadLock = new SemaphoreSlim(1, 1);

		internal CancellationTokenSource TokenSource = new CancellationTokenSource();

		internal CancellationToken Token;

		private TcpClient _TcpClient;

		private NetworkStream _NetworkStream;

		private SslStream _SslStream;

		private Stream _DataStream;

		private string _IpPort;

		internal TcpClient TcpClient => _TcpClient;

		internal NetworkStream NetworkStream
		{
			get
			{
				return _NetworkStream;
			}
			set
			{
				_NetworkStream = value;
				if (_NetworkStream != null)
				{
					_DataStream = _NetworkStream;
				}
			}
		}

		internal SslStream SslStream
		{
			get
			{
				return _SslStream;
			}
			set
			{
				_SslStream = value;
				if (_SslStream != null)
				{
					_DataStream = _SslStream;
				}
			}
		}

		internal Stream DataStream => _DataStream;

		internal string IpPort => _IpPort;

		internal byte[] SendBuffer { get; set; } = new byte[65536];

		internal Task DataReceiver { get; set; }

		internal ClientMetadata(TcpClient tcp)
		{
			if (tcp == null)
			{
				throw new ArgumentNullException("tcp");
			}
			_TcpClient = tcp;
			_IpPort = tcp.Client.RemoteEndPoint.ToString();
			NetworkStream = tcp.GetStream();
			Token = TokenSource.Token;
		}

		public void Dispose()
		{
			if (TokenSource != null && !TokenSource.IsCancellationRequested)
			{
				TokenSource.Cancel();
				TokenSource.Dispose();
			}
			if (_SslStream != null)
			{
				_SslStream.Close();
			}
			if (_NetworkStream != null)
			{
				_NetworkStream.Close();
			}
			if (_TcpClient != null)
			{
				_TcpClient.Close();
				_TcpClient.Dispose();
			}
			while (true)
			{
				Task dataReceiver = DataReceiver;
				if (dataReceiver != null && dataReceiver.Status == TaskStatus.Running)
				{
					Task.Delay(30).Wait();
					continue;
				}
				break;
			}
		}
	}
}
