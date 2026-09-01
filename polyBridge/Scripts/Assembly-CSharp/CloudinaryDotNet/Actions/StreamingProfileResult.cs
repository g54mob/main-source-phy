using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class StreamingProfileResult : BaseResult
	{
		[DataMember(Name = "message")]
		public string Message { get; set; }

		[DataMember(Name = "data")]
		public StreamingProfileData Data { get; set; }
	}
}
