using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ImageUploadResult : RawUploadResult
	{
		[DataMember(Name = "width")]
		public int Width { get; set; }

		[DataMember(Name = "height")]
		public int Height { get; set; }

		[DataMember(Name = "exif")]
		public Dictionary<string, string> Exif { get; set; }

		[Obsolete("Property Metadata is deprecated, please use ImageMetadata instead")]
		public Dictionary<string, string> Metadata
		{
			get
			{
				return ImageMetadata;
			}
			set
			{
				ImageMetadata = value;
			}
		}

		[DataMember(Name = "image_metadata")]
		public Dictionary<string, string> ImageMetadata { get; set; }

		[DataMember(Name = "faces")]
		public int[][] Faces { get; set; }

		[DataMember(Name = "quality_analysis")]
		public QualityAnalysis QualityAnalysis { get; set; }

		[DataMember(Name = "quality_score")]
		public double QualityScore { get; set; }

		[DataMember(Name = "colors")]
		public string[][] Colors { get; set; }

		[DataMember(Name = "phash")]
		public string Phash { get; set; }

		[DataMember(Name = "delete_token")]
		public string DeleteToken { get; set; }

		[DataMember(Name = "info")]
		public Info Info { get; set; }

		[DataMember(Name = "pages")]
		public int Pages { get; set; }

		public List<ResponsiveBreakpointList> ResponsiveBreakpoints { get; set; }

		[DataMember(Name = "context")]
		public JToken Context { get; set; }

		[DataMember(Name = "illustration_score")]
		public float IllustrationScore { get; set; }

		[DataMember(Name = "semi_transparent")]
		public bool SemiTransparent { get; set; }

		[DataMember(Name = "grayscale")]
		public bool Grayscale { get; set; }

		[DataMember(Name = "eager")]
		public Eager[] Eager { get; set; }

		[DataMember(Name = "predominant")]
		public Predominant Predominant { get; set; }

		[DataMember(Name = "cinemagraph_analysis")]
		public CinemagraphAnalysis CinemagraphAnalysis { get; set; }

		[DataMember(Name = "accessibility_analysis")]
		public AccessibilityAnalysis AccessibilityAnalysis { get; set; }

		internal override void SetValues(JToken source)
		{
			base.SetValues(source);
			JToken jToken = source["responsive_breakpoints"];
			if (jToken != null)
			{
				ResponsiveBreakpoints = jToken.ToObject<List<ResponsiveBreakpointList>>();
			}
		}
	}
}
