using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class GetFoldersResult : BaseResult
	{
		[DataMember(Name = "folders")]
		public List<Folder> Folders { get; set; }

		[DataMember(Name = "next_cursor")]
		public string NextCursor { get; set; }

		[DataMember(Name = "total_count")]
		public int TotalCount { get; set; }
	}
}
