using System;

namespace WatsonTcp
{
	public class WatsonTcpClientSettings
	{
		public bool DebugMessages;

		public Action<Severity, string> Logger;

		public bool AcceptInvalidCertificates = true;

		public bool MutuallyAuthenticate;

		public string PresharedKey;

		private int _StreamBufferSize = 65536;

		private int _MaxProxiedStreamSize = 67108864;

		private int _ConnectTimeoutSeconds = 5;

		private int _IdleServerTimeoutMs;

		private int _IdleServerEvaluationIntervalMs = 1000;

		private int _LocalPort;

		public int StreamBufferSize
		{
			get
			{
				return _StreamBufferSize;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("Stream buffer size must be greater than zero.");
				}
				_StreamBufferSize = value;
			}
		}

		public int MaxProxiedStreamSize
		{
			get
			{
				return _MaxProxiedStreamSize;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("MaxProxiedStreamSize must be greater than zero.");
				}
				_MaxProxiedStreamSize = value;
			}
		}

		public int ConnectTimeoutSeconds
		{
			get
			{
				return _ConnectTimeoutSeconds;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("ConnectTimeoutSeconds must be greater than zero.");
				}
				_ConnectTimeoutSeconds = value;
			}
		}

		public int IdleServerTimeoutMs
		{
			get
			{
				return _IdleServerTimeoutMs;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("IdleClientTimeoutMs must be zero or greater.");
				}
				_IdleServerTimeoutMs = value;
			}
		}

		public int IdleServerEvaluationIntervalMs
		{
			get
			{
				return _IdleServerEvaluationIntervalMs;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("IdleServerEvaluationIntervalMs must be one or greater.");
				}
				_IdleServerEvaluationIntervalMs = value;
			}
		}

		public int LocalPort
		{
			get
			{
				return _LocalPort;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Valid values for LocalPort are 0, 1024-65535.");
				}
				if (value > 0 && value < 1024)
				{
					throw new ArgumentException("Valid values for LocalPort are 0, 1024-65535.");
				}
				if (value > 65535)
				{
					throw new ArgumentException("Valid values for LocalPort are 0, 1024-65535.");
				}
				_LocalPort = value;
			}
		}

		public bool NoDelay { get; set; }
	}
}
