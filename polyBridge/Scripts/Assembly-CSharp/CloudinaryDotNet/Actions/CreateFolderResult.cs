using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class CreateFolderResult : BaseResult
	{
		[DataMember(Name = "success")]
		public bool Success { get; set; }

		[DataMember(Name = "path")]
		public string Path { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}
