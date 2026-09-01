using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModTag
	{
		[JsonProperty("name")]
		public string name;

		[JsonProperty("date_added")]
		public int dateAdded;
	}
}
