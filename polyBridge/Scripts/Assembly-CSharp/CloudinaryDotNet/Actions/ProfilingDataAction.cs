using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ProfilingDataAction
	{
		[DataMember(Name = "action")]
		public string Action { get; set; }

		[DataMember(Name = "parameter")]
		public string Parameter { get; set; }

		[DataMember(Name = "presize")]
		public long[] Presize { get; set; }

		[DataMember(Name = "postsize")]
		public long[] Postsize { get; set; }
	}
}
