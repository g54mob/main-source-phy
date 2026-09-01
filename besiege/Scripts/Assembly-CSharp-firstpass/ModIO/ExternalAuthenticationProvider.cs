using System;

namespace ModIO
{
	[Obsolete("Use UserPortal instead.")]
	public enum ExternalAuthenticationProvider
	{
		None = 0,
		Steam = 1,
		GOG = 2,
		ItchIO = 3,
		OculusRift = 4,
		XboxLive = 5,
		Switch = 6,
		Discord = 7,
		UNDEFINED = 8
	}
}
