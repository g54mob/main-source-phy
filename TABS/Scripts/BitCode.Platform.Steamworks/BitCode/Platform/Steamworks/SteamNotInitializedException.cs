using System;

namespace BitCode.Platform.Steamworks
{
	public class SteamNotInitializedException : Exception
	{
		public SteamNotInitializedException()
			: this("Steamworks not initialized error")
		{
		}

		public SteamNotInitializedException(string message)
			: base(message)
		{
		}
	}
}
