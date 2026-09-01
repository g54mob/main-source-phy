using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModfileLocator
	{
		[JsonProperty("binary_url")]
		public string binaryURL;

		[JsonProperty("date_expires")]
		public int dateExpires;
	}
}
