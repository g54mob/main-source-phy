using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class DeletedDataStatistics
	{
		[DataMember(Name = "original")]
		public int Original { get; set; }

		[DataMember(Name = "derived")]
		public int Derived { get; set; }
	}
}
