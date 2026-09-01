using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ListUserGroupsResult : BaseResult
	{
		[DataMember(Name = "user_groups")]
		public UserGroupResult[] UserGroups { get; set; }
	}
}
