using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Detection
	{
		[DataMember(Name = "rekognition_face")]
		public RekognitionFace RekognitionFace { get; set; }
	}
}
