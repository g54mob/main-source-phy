using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class IdentityInfo
	{
		[DataMember(Name = "access_key")]
		public string AccessKey { get; set; }
	}
}
