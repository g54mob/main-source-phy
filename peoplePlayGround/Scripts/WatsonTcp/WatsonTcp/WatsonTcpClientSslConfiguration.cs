using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WatsonTcp
{
	public class WatsonTcpClientSslConfiguration
	{
		private LocalCertificateSelectionCallback _ClientCertSelectionCallback;

		private RemoteCertificateValidationCallback _ServerCertValidationCallback;

		public LocalCertificateSelectionCallback ClientCertificateSelectionCallback
		{
			get
			{
				if (_ClientCertSelectionCallback == null)
				{
					_ClientCertSelectionCallback = DefaultSelectClientCertificate;
				}
				return _ClientCertSelectionCallback;
			}
			set
			{
				_ClientCertSelectionCallback = value;
			}
		}

		public RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (_ServerCertValidationCallback == null)
				{
					_ServerCertValidationCallback = DefaultValidateServerCertificate;
				}
				return _ServerCertValidationCallback;
			}
			set
			{
				_ServerCertValidationCallback = value;
			}
		}

		public WatsonTcpClientSslConfiguration()
		{
		}

		public WatsonTcpClientSslConfiguration(WatsonTcpClientSslConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("Can not copy from null client SSL configuration");
			}
			_ClientCertSelectionCallback = configuration._ClientCertSelectionCallback;
			_ServerCertValidationCallback = configuration._ServerCertValidationCallback;
		}

		private static X509Certificate DefaultSelectClientCertificate(object sender, string targetHost, X509CertificateCollection clientCertificates, X509Certificate serverCertificate, string[] acceptableIssuers)
		{
			if (clientCertificates == null || clientCertificates.Count == 0)
			{
				return null;
			}
			return clientCertificates[0];
		}

		private static bool DefaultValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
	}
}
