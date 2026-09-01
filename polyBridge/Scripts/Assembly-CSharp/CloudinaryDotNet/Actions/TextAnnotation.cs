using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class TextAnnotation
	{
		[DataMember(Name = "locale")]
		public string Locale { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "boundingPoly")]
		public BoundingBlock BoundingPoly { get; set; }
	}
}
