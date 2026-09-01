using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ListTransformsResult : BaseResult
	{
		[DataMember(Name = "transformations")]
		public TransformDesc[] Transformations { get; set; }

		[DataMember(Name = "next_cursor")]
		public string NextCursor { get; set; }
	}
}
