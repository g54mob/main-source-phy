using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Audio
	{
		[DataMember(Name = "codec")]
		public string Codec { get; set; }

		[DataMember(Name = "bit_rate")]
		public int? BitRate { get; set; }

		[DataMember(Name = "frequency")]
		public int? Frequency { get; set; }

		[DataMember(Name = "channels")]
		public int? Channels { get; set; }

		[DataMember(Name = "channel_layout")]
		public string ChannelLayout { get; set; }
	}
}
