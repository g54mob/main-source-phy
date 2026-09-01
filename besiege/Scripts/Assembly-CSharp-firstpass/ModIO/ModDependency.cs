using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModDependency
	{
		[JsonProperty("mod_id")]
		public int modId;

		[JsonProperty("date_added")]
		public int dateAdded;
	}
}
