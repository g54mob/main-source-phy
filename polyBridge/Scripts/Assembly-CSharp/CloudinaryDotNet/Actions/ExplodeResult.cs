using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ExplodeResult : BaseResult
	{
		[DataMember(Name = "status")]
		public string Status { get; set; }

		[DataMember(Name = "batch_id")]
		public string BatchId { get; set; }
	}
}
