using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ListResourcesResult : BaseResult
	{
		[DataMember(Name = "resources")]
		public Resource[] Resources { get; set; }

		[DataMember(Name = "next_cursor")]
		public string NextCursor { get; set; }
	}
}
