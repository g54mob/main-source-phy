using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Word : Block
	{
		[DataMember(Name = "symbols")]
		public List<Symbol> Symbols { get; set; }
	}
}
