using System;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class SubAccountResult : BaseResult
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "cloud_name")]
		public string CloudName { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "enabled")]
		public bool Enabled { get; set; }

		[DataMember(Name = "api_access_keys")]
		public ApiAccessKey[] ApiAccessKeys { get; set; }

		[DataMember(Name = "created_at")]
		public DateTime CreatedAt { get; set; }

		[DataMember(Name = "custom_attributes")]
		public StringDictionary CustomAttributes { get; set; }
	}
}
