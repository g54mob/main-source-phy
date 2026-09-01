using System;

namespace BitCode.Debug
{
	public class TokenResolutionException : CommandInvocationException
	{
		public readonly string Token;

		public readonly Type Type;

		public TokenResolutionException(string token, Type type)
		{
			Token = token;
			Type = type;
		}

		public TokenResolutionException(string token, Type type, string message)
			: base(message)
		{
			while (true)
			{
				int num = 92131100;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x7C7FF96D)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						Token = token;
						num = (int)(num2 * 11656351) ^ -1093107856;
						continue;
					case 2u:
						Type = type;
						num = (int)((num2 * 1595312555) ^ 0x5F39CC2C);
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		public TokenResolutionException(string token, Type type, string message, Exception innerException)
			: base(message, innerException)
		{
			Token = token;
			Type = type;
		}
	}
}
