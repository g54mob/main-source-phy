using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Error
	{
		[DataMember(Name = "message")]
		public string Message { get; set; }
	}
}
