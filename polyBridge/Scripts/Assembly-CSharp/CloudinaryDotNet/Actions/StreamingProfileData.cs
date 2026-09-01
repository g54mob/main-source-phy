using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class StreamingProfileData : StreamingProfileBaseData
	{
		[DataMember(Name = "representations")]
		public List<Representation> Representations { get; set; }
	}
}
