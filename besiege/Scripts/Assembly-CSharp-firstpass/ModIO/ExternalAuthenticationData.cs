using System;
using System.Collections.Generic;

namespace ModIO
{
	public struct ExternalAuthenticationData
	{
		public static class OculusRiftKeys
		{
			public const string NONCE = "oculusRiftNonce";

			public const string USER_ID = "oculusRiftId";
		}

		public UserPortal portal;

		public string ticket;

		public Dictionary<string, string> additionalData;

		[Obsolete("Use ExternalAuthenticationData.portal instead.")]
		public ExternalAuthenticationProvider provider
		{
			get
			{
				return (ExternalAuthenticationProvider)portal;
			}
			set
			{
				portal = ExternalAuthenticationProviderEnum.ToUserPortalEnum(value);
			}
		}
	}
}
