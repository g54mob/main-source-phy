using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class DelResResult : BaseResult
	{
		[DataMember(Name = "deleted")]
		public Dictionary<string, string> Deleted { get; set; }

		[DataMember(Name = "next_cursor")]
		public string NextCursor { get; set; }

		[DataMember(Name = "partial")]
		public bool Partial { get; set; }

		[DataMember(Name = "deleted_counts")]
		public Dictionary<string, DeletedDataStatistics> DeletedCounts { get; set; }
	}
}
