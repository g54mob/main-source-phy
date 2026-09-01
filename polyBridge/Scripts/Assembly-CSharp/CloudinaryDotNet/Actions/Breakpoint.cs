using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	[JsonObject]
	public class Breakpoint
	{
		[JsonProperty("width")]
		public int Width { get; set; }

		[JsonProperty("height")]
		public int Height { get; set; }

		[JsonProperty("bytes")]
		public long Bytes { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("secure_url")]
		public string SecureUrl { get; set; }
	}
}
