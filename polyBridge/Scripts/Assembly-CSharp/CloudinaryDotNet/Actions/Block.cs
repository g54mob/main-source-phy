using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public abstract class Block
	{
		[DataMember(Name = "property")]
		public PageProperty Property { get; set; }

		[DataMember(Name = "boundingBox")]
		public BoundingBlock BoundingBox { get; set; }
	}
}
