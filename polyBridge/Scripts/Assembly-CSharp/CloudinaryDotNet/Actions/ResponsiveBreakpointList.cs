using System.Collections.Generic;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class ResponsiveBreakpointList
	{
		[JsonProperty("breakpoints")]
		public List<Breakpoint> Breakpoints { get; set; }

		[JsonProperty("transformation")]
		public string Transformation { get; set; }
	}
}
