using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Ocr
	{
		[DataMember(Name = "adv_ocr")]
		public AdvOcr AdvOcr { get; set; }
	}
}
