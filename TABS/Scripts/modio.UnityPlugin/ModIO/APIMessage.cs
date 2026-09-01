using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class APIMessage
	{
		[JsonProperty("code")]
		public int code;

		[JsonProperty("message")]
		public string message;
	}
}
