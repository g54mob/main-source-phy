using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class RekognitionFace
	{
		[DataMember(Name = "status")]
		public string Status { get; set; }

		[DataMember(Name = "data")]
		public List<Face> Faces { get; set; }
	}
}
