using System;

namespace BitCode.Platform.Steamworks
{
	public class SteamApiException : Exception
	{
		public SteamApiException()
			: this("Steamworks Api error")
		{
		}

		public SteamApiException(string message)
			: base(message)
		{
		}
	}
}
