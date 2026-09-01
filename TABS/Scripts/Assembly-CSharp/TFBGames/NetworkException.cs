using System;

namespace TFBGames
{
	public class NetworkException : Exception
	{
		public NetworkErrorCode ErrorCode { get; }

		public NetworkException(NetworkErrorCode errorCode)
		{
			ErrorCode = errorCode;
		}

		public NetworkException(NetworkErrorCode errorCode, string message)
			: base(message)
		{
			ErrorCode = errorCode;
		}

		public NetworkException(NetworkErrorCode errorCode, string message, Exception innerException)
			: base(message, innerException)
		{
			ErrorCode = errorCode;
		}
	}
}
