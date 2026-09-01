using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Predominant
	{
		[DataMember(Name = "google")]
		public object[][] Google { get; set; }

		[DataMember(Name = "cloudinary")]
		public object[][] Cloudinary { get; set; }
	}
}
