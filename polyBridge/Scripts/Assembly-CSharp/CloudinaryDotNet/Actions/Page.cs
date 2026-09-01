using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Page
	{
		[DataMember(Name = "property")]
		public PageProperty Property { get; set; }

		[DataMember(Name = "width")]
		public int? Width { get; set; }

		[DataMember(Name = "height")]
		public int? Height { get; set; }

		[DataMember(Name = "blocks")]
		public List<TextBlock> Blocks { get; set; }
	}
}
