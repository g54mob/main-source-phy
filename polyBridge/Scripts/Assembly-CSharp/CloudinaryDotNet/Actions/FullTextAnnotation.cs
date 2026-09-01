using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class FullTextAnnotation
	{
		[DataMember(Name = "pages")]
		public List<Page> Pages { get; set; }

		[DataMember(Name = "text")]
		public string Text { get; set; }
	}
}
