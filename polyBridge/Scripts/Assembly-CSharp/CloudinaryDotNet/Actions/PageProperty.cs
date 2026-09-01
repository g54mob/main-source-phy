using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class PageProperty
	{
		[DataMember(Name = "detectedLanguages")]
		public List<DetectedLanguage> DetectedLanguages { get; set; }
	}
}
