using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class FileHash
	{
		[JsonProperty("md5")]
		public string md5;
	}
}
