using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Symbol : Block
	{
		[DataMember(Name = "text")]
		public string Text { get; set; }
	}
}
