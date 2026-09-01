using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class DelMetadataFieldResult : BaseResult
	{
		[DataMember(Name = "message")]
		public string Message { get; set; }
	}
}
