using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ApiAccessKey
	{
		[DataMember(Name = "key")]
		public string Key { get; set; }

		[DataMember(Name = "secret")]
		public string Secret { get; set; }

		[DataMember(Name = "enabled")]
		public bool Enabled { get; set; }
	}
}
