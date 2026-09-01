using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ColorblindAccessibilityAnalysis
	{
		[DataMember(Name = "distinct_edges")]
		public double DistinctEdges { get; set; }

		[DataMember(Name = "distinct_colors")]
		public double DistinctColors { get; set; }

		[DataMember(Name = "most_indistinct_pair")]
		public string[] MostIndistinctPair { get; set; }
	}
}
