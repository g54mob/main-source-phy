using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class QualityAnalysis
	{
		[DataMember(Name = "jpeg_quality")]
		public double JpegQuality { get; set; }

		[DataMember(Name = "jpeg_chroma")]
		public double JpegChroma { get; set; }

		[DataMember(Name = "focus")]
		public double Focus { get; set; }

		[DataMember(Name = "noise")]
		public double Noise { get; set; }

		[DataMember(Name = "contrast")]
		public double Contrast { get; set; }

		[DataMember(Name = "exposure")]
		public double Exposure { get; set; }

		[DataMember(Name = "saturation")]
		public double Saturation { get; set; }

		[DataMember(Name = "lighting")]
		public double Lighting { get; set; }

		[DataMember(Name = "pixel_score")]
		public double PixelScore { get; set; }

		[DataMember(Name = "color_score")]
		public double ColorScore { get; set; }

		[DataMember(Name = "dct")]
		public double Dct { get; set; }

		[DataMember(Name = "blockiness")]
		public double Blockiness { get; set; }

		[DataMember(Name = "chroma_subsampling")]
		public double ChromaSubsampling { get; set; }

		[DataMember(Name = "resolution")]
		public double Resolution { get; set; }
	}
}
