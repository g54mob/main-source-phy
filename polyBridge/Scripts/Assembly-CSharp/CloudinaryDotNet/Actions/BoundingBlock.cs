using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class BoundingBlock
	{
		[DataMember(Name = "vertices")]
		public List<Point> Vertices { get; set; }
	}
}
