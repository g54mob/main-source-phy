using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class UserResult : BaseResult
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "email")]
		public string Email { get; set; }

		[DataMember(Name = "role")]
		public string Role { get; set; }

		[DataMember(Name = "pending")]
		public bool Pending { get; set; }

		[DataMember(Name = "enabled")]
		public bool Enabled { get; set; }

		[JsonConverter(typeof(SafeArrayConverter))]
		[DataMember(Name = "sub_account_ids")]
		public string[] SubAccountIds { get; set; }

		[DataMember(Name = " all_sub_accounts")]
		public bool AllSubAccounts { get; set; }

		[DataMember(Name = "created_at")]
		public DateTime? CreatedAt { get; set; }
	}
}
