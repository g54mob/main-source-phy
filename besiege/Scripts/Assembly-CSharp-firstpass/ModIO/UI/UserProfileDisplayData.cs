using System;

namespace ModIO.UI
{
	[Serializable]
	[Obsolete("No longer supported.")]
	public struct UserProfileDisplayData
	{
		public int userId;

		public string nameId;

		public string username;

		public int lastOnline;

		public string timezone;

		public string language;

		public string profileURL;

		public static UserProfileDisplayData CreateFromProfile(UserProfile profile)
		{
			return new UserProfileDisplayData
			{
				userId = profile.id,
				nameId = profile.nameId,
				username = profile.username,
				lastOnline = profile.lastOnline,
				timezone = profile.timezone,
				language = profile.language,
				profileURL = profile.profileURL
			};
		}
	}
}
