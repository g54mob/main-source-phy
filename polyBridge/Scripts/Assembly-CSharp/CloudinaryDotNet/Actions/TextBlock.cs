using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class TextBlock : Block
	{
		[DataMember(Name = "paragraphs")]
		public List<Paragraph> Paragraphs { get; set; }

		[DataMember(Name = "blockType")]
		public string BlockType { get; set; }
	}
}
