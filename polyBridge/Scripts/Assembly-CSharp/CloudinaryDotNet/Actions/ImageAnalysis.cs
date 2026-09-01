using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ImageAnalysis
	{
		[DataMember(Name = "face_count")]
		public int FaceCount { get; set; }

		[DataMember(Name = "faces")]
		public int[][] Faces { get; set; }

		[DataMember(Name = "grayscale")]
		public bool GrayScale { get; set; }

		[DataMember(Name = "illustration_score")]
		public double IllustrationScore { get; set; }

		[DataMember(Name = "transparent")]
		public bool Transparent { get; set; }

		[DataMember(Name = "etag")]
		public string Etag { get; set; }

		[DataMember(Name = "colors")]
		public Dictionary<string, double> Colors { get; set; }
	}
}
