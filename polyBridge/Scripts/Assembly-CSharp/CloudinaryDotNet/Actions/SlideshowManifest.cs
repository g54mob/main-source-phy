using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class SlideshowManifest
	{
		[JsonProperty(PropertyName = "w")]
		public int Width { get; set; }

		[JsonProperty(PropertyName = "h")]
		public int Height { get; set; }

		[JsonProperty(PropertyName = "du", NullValueHandling = NullValueHandling.Ignore)]
		public int Duration { get; set; }

		[JsonProperty(PropertyName = "fps", NullValueHandling = NullValueHandling.Ignore)]
		public int Fps { get; set; }

		[JsonProperty(PropertyName = "vars")]
		public Slideshow Variables { get; set; }
	}
}
