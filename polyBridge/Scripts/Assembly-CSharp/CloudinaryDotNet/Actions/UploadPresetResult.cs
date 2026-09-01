using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class UploadPresetResult : BaseResult
	{
		[DataMember(Name = "message")]
		public string Message { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}
