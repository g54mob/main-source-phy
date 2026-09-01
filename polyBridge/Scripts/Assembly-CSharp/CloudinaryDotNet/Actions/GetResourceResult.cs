using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class GetResourceResult : BaseResult
	{
		[DataMember(Name = "resource_type")]
		protected string m_resourceType;

		[DataMember(Name = "public_id")]
		public string PublicId { get; set; }

		[DataMember(Name = "format")]
		public string Format { get; set; }

		[DataMember(Name = "version")]
		public string Version { get; set; }

		public ResourceType ResourceType => ApiShared.ParseCloudinaryParam<ResourceType>(m_resourceType);

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[Obsolete("Property Created is deprecated, please use CreatedAt instead")]
		public string Created
		{
			get
			{
				return CreatedAt;
			}
			set
			{
				CreatedAt = value;
			}
		}

		[DataMember(Name = "created_at")]
		public string CreatedAt { get; set; }

		[Obsolete("Property Length is deprecated, please use Bytes instead")]
		public long Length
		{
			get
			{
				return Bytes;
			}
			set
			{
				Bytes = value;
			}
		}

		[DataMember(Name = "bytes")]
		public long Bytes { get; set; }

		[DataMember(Name = "width")]
		public int Width { get; set; }

		[DataMember(Name = "height")]
		public int Height { get; set; }

		[DataMember(Name = "url")]
		public string Url { get; set; }

		[DataMember(Name = "secure_url")]
		public string SecureUrl { get; set; }

		[DataMember(Name = "next_cursor")]
		public string NextCursor { get; set; }

		[DataMember(Name = "derived_next_cursor")]
		public string DerivedNextCursor { get; set; }

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

		[DataMember(Name = "derived")]
		public Derived[] Derived { get; set; }

		[DataMember(Name = "tags")]
		public string[] Tags { get; set; }

		[DataMember(Name = "moderation")]
		public List<Moderation> Moderation { get; set; }

		[DataMember(Name = "context")]
		public JToken Context { get; set; }

		[DataMember(Name = "metadata")]
		public JToken MetadataFields { get; set; }

		[DataMember(Name = "phash")]
		public string Phash { get; set; }

		[DataMember(Name = "predominant")]
		public Predominant Predominant { get; set; }

		[DataMember(Name = "coordinates")]
		public Coordinates Coordinates { get; set; }

		[DataMember(Name = "info")]
		public Info Info { get; set; }

		[DataMember(Name = "access_control")]
		public List<AccessControlRule> AccessControl { get; set; }

		[DataMember(Name = "pages")]
		public int Pages { get; set; }

		[DataMember(Name = "access_mode")]
		public string AccessMode { get; set; }

		[DataMember(Name = "cinemagraph_analysis")]
		public CinemagraphAnalysis CinemagraphAnalysis { get; set; }

		[DataMember(Name = "accessibility_analysis")]
		public AccessibilityAnalysis AccessibilityAnalysis { get; set; }

		[DataMember(Name = "asset_id")]
		public string AssetId { get; set; }

		[DataMember(Name = "versions")]
		public List<AssetVersion> Versions { get; set; }
	}
}
