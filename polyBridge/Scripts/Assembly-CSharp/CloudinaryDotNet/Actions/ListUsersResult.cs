using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ListUsersResult : BaseResult
	{
		[DataMember(Name = "users")]
		public UserResult[] Users { get; set; }
	}
}
