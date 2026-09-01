using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ModerationResponse
	{
		[DataMember(Name = "moderation_labels")]
		public ModerationLabel[] ModerationLabels;
	}
}
