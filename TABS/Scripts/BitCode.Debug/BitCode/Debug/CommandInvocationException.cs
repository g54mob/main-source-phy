using System;

namespace BitCode.Debug
{
	public class CommandInvocationException : Exception
	{
		public CommandInvocationException()
		{
		}

		public CommandInvocationException(string message)
			: base(message)
		{
		}

		public CommandInvocationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
