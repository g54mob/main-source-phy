using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class SearchResult : BaseResult
	{
		[DataMember(Name = "total_count")]
		public int TotalCount { get; set; }

		[DataMember(Name = "time")]
		public long Time { get; set; }

		[DataMember(Name = "resources")]
		public List<SearchResource> Resources { get; set; }

		[DataMember(Name = "next_cursor")]
		public string NextCursor { get; set; }

		[DataMember(Name = "aggregations")]
		public Dictionary<string, Dictionary<string, int>> Aggregations { get; set; }
	}
}
