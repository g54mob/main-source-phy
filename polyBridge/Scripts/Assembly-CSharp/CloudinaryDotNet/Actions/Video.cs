using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Video
	{
		[DataMember(Name = "pix_format")]
		public string Format { get; set; }

		[DataMember(Name = "codec")]
		public string Codec { get; set; }

		[DataMember(Name = "level")]
		public int? Level { get; set; }

		[DataMember(Name = "bit_rate")]
		public int? BitRate { get; set; }

		[DataMember(Name = "profile")]
		public string Profile { get; set; }
	}
}
