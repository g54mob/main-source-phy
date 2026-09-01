using System;

namespace ModIO
{
	[Obsolete]
	public static class ExternalAuthenticationProviderEnum
	{
		public static UserPortal ToUserPortalEnum(ExternalAuthenticationProvider provider)
		{
			UserPortal result = UserPortal.None;
			switch (provider)
			{
			case ExternalAuthenticationProvider.Steam:
				result = UserPortal.Steam;
				break;
			case ExternalAuthenticationProvider.GOG:
				result = UserPortal.GOG;
				break;
			case ExternalAuthenticationProvider.ItchIO:
				result = UserPortal.itchio;
				break;
			case ExternalAuthenticationProvider.OculusRift:
				result = UserPortal.Oculus;
				break;
			case ExternalAuthenticationProvider.XboxLive:
				result = UserPortal.XboxLive;
				break;
			case ExternalAuthenticationProvider.Switch:
				result = UserPortal.Nintendo;
				break;
			case ExternalAuthenticationProvider.Discord:
				result = UserPortal.Discord;
				break;
			}
			return result;
		}
	}
}
