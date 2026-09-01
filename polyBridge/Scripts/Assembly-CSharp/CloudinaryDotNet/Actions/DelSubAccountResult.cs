using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class DelSubAccountResult : BaseResult
	{
		[DataMember(Name = "message")]
		public string Message { get; set; }
	}
}
