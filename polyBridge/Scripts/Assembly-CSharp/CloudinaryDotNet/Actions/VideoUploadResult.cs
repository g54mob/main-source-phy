using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class VideoUploadResult : RawUploadResult
	{
		[DataMember(Name = "width")]
		public int Width { get; set; }

		[DataMember(Name = "height")]
		public int Height { get; set; }

		[DataMember(Name = "video")]
		public Video Video { get; set; }

		[DataMember(Name = "audio")]
		public Audio Audio { get; set; }

		[DataMember(Name = "frame_rate")]
		public double FrameRate { get; set; }

		[DataMember(Name = "bit_rate")]
		public int BitRate { get; set; }

		[DataMember(Name = "duration")]
		public double Duration { get; set; }

		[DataMember(Name = "pages")]
		public int Pages { get; set; }

		[DataMember(Name = "cinemagraph_analysis")]
		public CinemagraphAnalysis CinemagraphAnalysis { get; set; }

		[DataMember(Name = "context")]
		public JToken Context { get; set; }

		[DataMember(Name = "is_audio")]
		public bool IsAudio { get; set; }

		[DataMember(Name = "rotation")]
		public int Rotation { get; set; }

		[DataMember(Name = "nb_frames")]
		public int NbFrames { get; set; }
	}
}
