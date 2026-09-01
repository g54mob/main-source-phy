using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Info
	{
		[DataMember(Name = "detection")]
		public Detection Detection { get; set; }

		[DataMember(Name = "ocr")]
		public Ocr Ocr { get; set; }
	}
}
