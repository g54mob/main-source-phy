using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class PublishResourceResult : BaseResult
	{
		[DataMember(Name = "published")]
		public List<object> Published { get; set; }

		[DataMember(Name = "failed")]
		public List<object> Failed { get; set; }
	}
}
