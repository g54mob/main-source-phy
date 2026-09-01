using System.Collections.Generic;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class Slideshow
	{
		[JsonProperty(PropertyName = "transition_s")]
		public string Transition { get; set; }

		[JsonProperty(PropertyName = "transformation_s")]
		public string Transformation { get; set; }

		[JsonProperty(PropertyName = "sdur")]
		public int SlideDuration { get; set; }

		[JsonProperty(PropertyName = "tdur")]
		public int TransitionDuration { get; set; }

		[JsonProperty(PropertyName = "slides")]
		public List<Slide> Slides { get; set; }
	}
}
