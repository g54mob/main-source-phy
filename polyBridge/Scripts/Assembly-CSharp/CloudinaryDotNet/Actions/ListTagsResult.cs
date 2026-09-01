using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ListTagsResult : BaseResult
	{
		[DataMember(Name = "tags")]
		public string[] Tags { get; set; }

		[DataMember(Name = "next_cursor")]
		public string NextCursor { get; set; }
	}
}
