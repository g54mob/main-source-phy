using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ListSubAccountsResult : BaseResult
	{
		[DataMember(Name = "sub_accounts")]
		public SubAccountResult[] SubAccounts { get; set; }
	}
}
