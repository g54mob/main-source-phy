using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModTeamMember
	{
		[JsonProperty("id")]
		public int id;

		[JsonProperty("user")]
		public UserProfile user;

		[JsonProperty("level")]
		public ModTeamMemberAccessLevel accessLevel;

		[JsonProperty("date_added")]
		public int dateAdded;

		[JsonProperty("position")]
		public string title;
	}
}
