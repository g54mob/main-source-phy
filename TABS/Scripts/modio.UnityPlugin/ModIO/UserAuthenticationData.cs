using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	[Obsolete("Replaced by LocalUser.")]
	public struct UserAuthenticationData
	{
		public static readonly UserAuthenticationData NONE = new UserAuthenticationData
		{
			userId = -1,
			token = null,
			wasTokenRejected = false,
			steamTicket = null,
			gogTicket = null
		};

		public static readonly string FILE_LOCATION = IOUtilities.CombinePath(DataStorage.CACHE_DIRECTORY, "user.data");

		public int userId;

		public string token;

		public bool wasTokenRejected;

		public string steamTicket;

		public string gogTicket;

		[JsonIgnore]
		public bool IsTokenValid
		{
			get
			{
				if (!wasTokenRejected)
				{
					return !string.IsNullOrEmpty(token);
				}
				return false;
			}
		}

		public static UserAuthenticationData instance
		{
			get
			{
				LocalUser localUser = LocalUser.instance;
				UserProfile profile = localUser.profile;
				string text = null;
				string text2 = null;
				switch (LocalUser.ExternalAuthentication.portal)
				{
				case UserPortal.Steam:
					text = LocalUser.ExternalAuthentication.ticket;
					break;
				case UserPortal.GOG:
					text2 = LocalUser.ExternalAuthentication.ticket;
					break;
				}
				return new UserAuthenticationData
				{
					userId = (profile?.id ?? (-1)),
					token = localUser.oAuthToken,
					wasTokenRejected = localUser.wasTokenRejected,
					steamTicket = text,
					gogTicket = text2
				};
			}
			set
			{
				LocalUser userData = LocalUser.instance;
				if (userData.profile == null || userData.profile.id != value.userId)
				{
					if (value.userId == -1)
					{
						userData.profile = null;
					}
					else
					{
						userData.profile = new UserProfile
						{
							id = value.userId
						};
					}
				}
				userData.oAuthToken = value.token;
				userData.wasTokenRejected = value.wasTokenRejected;
				ExternalAuthenticationData externalAuthentication = new ExternalAuthenticationData
				{
					ticket = null,
					portal = UserPortal.None
				};
				if (!string.IsNullOrEmpty(value.steamTicket))
				{
					externalAuthentication.ticket = value.steamTicket;
					externalAuthentication.portal = UserPortal.Steam;
				}
				else if (!string.IsNullOrEmpty(value.gogTicket))
				{
					externalAuthentication.ticket = value.gogTicket;
					externalAuthentication.portal = UserPortal.GOG;
				}
				LocalUser.AssertListsNotNull(ref userData);
				LocalUser.instance = userData;
				LocalUser.isLoaded = true;
				LocalUser.Save();
				LocalUser.ExternalAuthentication = externalAuthentication;
			}
		}

		public static void Clear()
		{
			instance = NONE;
		}
	}
}
