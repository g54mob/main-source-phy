using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ImageInfo
	{
		[DataMember(Name = "width")]
		public int Width { get; set; }

		[DataMember(Name = "height")]
		public int Height { get; set; }

		[DataMember(Name = "x")]
		public int X { get; set; }

		[DataMember(Name = "y")]
		public int Y { get; set; }
	}
}
