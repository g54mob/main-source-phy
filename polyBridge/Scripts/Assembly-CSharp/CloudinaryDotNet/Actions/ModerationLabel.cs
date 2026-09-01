using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ModerationLabel
	{
		[DataMember(Name = "confidence")]
		public float Confidence;

		[DataMember(Name = "name")]
		public string Name;

		[DataMember(Name = "parent_name")]
		public string ParentName;
	}
}
