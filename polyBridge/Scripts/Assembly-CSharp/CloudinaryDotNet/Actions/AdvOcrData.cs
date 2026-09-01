using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class AdvOcrData
	{
		[DataMember(Name = "textAnnotations")]
		public List<TextAnnotation> TextAnnotations { get; set; }

		[DataMember(Name = "fullTextAnnotation")]
		public FullTextAnnotation FullTextAnnotation { get; set; }
	}
}
