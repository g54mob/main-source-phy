using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class StreamingProfileListResult : BaseResult
	{
		[DataMember(Name = "data")]
		public IEnumerable<StreamingProfileBaseData> Data { get; set; }
	}
}
