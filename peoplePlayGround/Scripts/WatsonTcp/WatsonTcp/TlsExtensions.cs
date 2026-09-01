using System;
using System.Security.Authentication;

namespace WatsonTcp
{
	public static class TlsExtensions
	{
		public static SslProtocols ToSslProtocols(this TlsVersion tlsVersion)
		{
			if (tlsVersion == TlsVersion.Tls12)
			{
				return SslProtocols.Tls12;
			}
			throw new ArgumentOutOfRangeException($"Unsupported TLS version {tlsVersion}.");
		}
	}
}
