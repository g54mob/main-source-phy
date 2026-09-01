using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CloudinaryDotNet.Actions
{
	public class AccessControlRule
	{
		[JsonProperty(PropertyName = "access_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public AccessType AccessType { get; set; }

		[JsonProperty(PropertyName = "start", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? Start { get; set; }

		[JsonProperty(PropertyName = "end", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? End { get; set; }
	}
}
