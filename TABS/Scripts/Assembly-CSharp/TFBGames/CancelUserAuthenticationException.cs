using System;

namespace TFBGames
{
	public class CancelUserAuthenticationException : Exception
	{
		public CancelUserAuthenticationException()
		{
		}

		public CancelUserAuthenticationException(string message)
			: base(message)
		{
		}
	}
}
