using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Size
	{
		[DataMember(Name = "width")]
		public double Width { get; set; }

		[DataMember(Name = "height")]
		public double Height { get; set; }
	}
}
