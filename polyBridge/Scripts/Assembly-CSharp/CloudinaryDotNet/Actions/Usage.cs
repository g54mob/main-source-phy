using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Usage
	{
		[DataMember(Name = "usage")]
		public long Used { get; set; }

		[DataMember(Name = "limit")]
		public long Limit { get; set; }

		[DataMember(Name = "used_percent")]
		public float UsedPercent { get; set; }

		[DataMember(Name = "credits_usage")]
		public float CreditsUsage { get; set; }
	}
}
