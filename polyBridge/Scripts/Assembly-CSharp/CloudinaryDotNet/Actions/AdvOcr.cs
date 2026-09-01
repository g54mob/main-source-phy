using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class AdvOcr
	{
		[DataMember(Name = "status")]
		public string Status { get; set; }

		[DataMember(Name = "data")]
		public List<AdvOcrData> Data { get; set; }
	}
}
