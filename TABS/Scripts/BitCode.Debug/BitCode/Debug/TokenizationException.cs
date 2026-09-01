using System;

namespace BitCode.Debug
{
	public class TokenizationException : CommandInvocationException
	{
		public TokenizationException()
		{
		}

		public TokenizationException(string message)
			: base(message)
		{
		}

		public TokenizationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
