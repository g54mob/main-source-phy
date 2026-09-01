using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ProfilingData
	{
		[DataMember(Name = "cpu")]
		public long Cpu { get; set; }

		[DataMember(Name = "real")]
		public long Real { get; set; }

		[DataMember(Name = "action")]
		public ProfilingDataAction Action { get; set; }
	}
}
