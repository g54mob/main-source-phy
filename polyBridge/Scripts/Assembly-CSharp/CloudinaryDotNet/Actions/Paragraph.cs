using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Paragraph : Block
	{
		[DataMember(Name = "words")]
		public List<Word> Words { get; set; }

		[DataMember(Name = "text")]
		public string Text { get; set; }
	}
}
