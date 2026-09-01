using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class Slide
	{
		[JsonProperty(PropertyName = "media")]
		public string Media { get; set; }

		[JsonProperty(PropertyName = "type")]
		public int Type { get; set; }

		[JsonProperty(PropertyName = "transition_s")]
		public string Transition { get; set; }

		[JsonProperty(PropertyName = "sdur")]
		public int SlideDuration { get; set; }

		[JsonProperty(PropertyName = "tdur")]
		public int TransitionDuration { get; set; }

		public Slide(string media)
		{
			Media = media;
		}
	}
}
