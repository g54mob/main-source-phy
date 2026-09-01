using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Folder
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "path")]
		public string Path { get; set; }
	}
}
