using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class CreateSlideshowResult : BaseResult
	{
		[DataMember(Name = "status")]
		public string Status { get; set; }

		[DataMember(Name = "public_id")]
		public string PublicId { get; set; }

		[DataMember(Name = "batch_id")]
		public string BatchId { get; set; }
	}
}
