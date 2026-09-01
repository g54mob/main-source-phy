using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class RawPartUploadResult : RawUploadResult
	{
		[DataMember(Name = "upload_id")]
		public string UploadId { get; set; }
	}
}
