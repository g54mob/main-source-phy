using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class UserGroupResult : BaseResult
	{
		[DataMember(Name = "id")]
		public string GroupId { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "users")]
		public string[] Users { get; set; }
	}
}
