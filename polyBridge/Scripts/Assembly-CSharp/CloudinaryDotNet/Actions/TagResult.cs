using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class TagResult : BaseResult
	{
		[DataMember(Name = "public_ids")]
		public string[] PublicIds { get; set; }
	}
}
