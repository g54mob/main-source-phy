using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class StreamingProfileBaseData
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "display_name")]
		public string DisplayName { get; set; }

		[DataMember(Name = "predefined")]
		public bool Predefined { get; set; }
	}
}
