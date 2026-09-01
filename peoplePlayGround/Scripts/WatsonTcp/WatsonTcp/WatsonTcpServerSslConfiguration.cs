using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WatsonTcp
{
	public class WatsonTcpServerSslConfiguration
	{
		private bool _ClientCertRequired = true;

		private RemoteCertificateValidationCallback _ClientCertValidationCallback;

		public bool ClientCertificateRequired
		{
			get
			{
				return _ClientCertRequired;
			}
			set
			{
				_ClientCertRequired = value;
			}
		}

		public RemoteCertificateValidationCallback ClientCertificateValidationCallback
		{
			get
			{
				if (_ClientCertValidationCallback == null)
				{
					_ClientCertValidationCallback = DefaultValidateClientCertificate;
				}
				return _ClientCertValidationCallback;
			}
			set
			{
				_ClientCertValidationCallback = value;
			}
		}

		public WatsonTcpServerSslConfiguration()
		{
		}

		public WatsonTcpServerSslConfiguration(WatsonTcpServerSslConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("Can not copy from null server SSL configuration");
			}
			_ClientCertRequired = configuration._ClientCertRequired;
			_ClientCertValidationCallback = configuration._ClientCertValidationCallback;
		}

		private static bool DefaultValidateClientCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
	}
}
