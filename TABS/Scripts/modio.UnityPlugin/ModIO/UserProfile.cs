using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class UserProfile
	{
		public const int NULL_ID = -1;

		public const int USERNAME_MAXLENGTH = 20;

		[JsonProperty("id")]
		public int id;

		[JsonProperty("name_id")]
		public string nameId;

		[JsonProperty("username")]
		public string username;

		[JsonProperty("avatar")]
		public AvatarImageLocator avatarLocator;

		[JsonProperty("date_online")]
		public int lastOnline;

		[JsonProperty("timezone")]
		public string timezone;

		[JsonProperty("language")]
		public string language;

		[JsonProperty("profile_url")]
		public string profileURL;

		[JsonProperty("username_platform")]
		public string usernamePlatform;
	}
}
