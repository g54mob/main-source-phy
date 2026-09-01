using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class BoundingBox
	{
		[DataMember(Name = "tl")]
		public Point TopLeft { get; set; }

		[DataMember(Name = "size")]
		public Size Size { get; set; }
	}
}
