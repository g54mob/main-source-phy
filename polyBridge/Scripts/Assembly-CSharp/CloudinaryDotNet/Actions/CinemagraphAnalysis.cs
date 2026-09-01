using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class CinemagraphAnalysis
	{
		[DataMember(Name = "cinemagraph_score")]
		public double CinemagraphScore { get; set; }
	}
}
