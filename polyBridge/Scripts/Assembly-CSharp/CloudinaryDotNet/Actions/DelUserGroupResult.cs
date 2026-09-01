using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class DelUserGroupResult : BaseResult
	{
		[DataMember(Name = "ok")]
		public bool Ok { get; set; }
	}
}
