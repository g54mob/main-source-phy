using System;
using System.Collections.Generic;
using System.Globalization;

namespace CloudinaryDotNet.Actions
{
	public class GetResourceParams : BaseParams
	{
		public string PublicId { get; set; }

		public ResourceType ResourceType { get; set; }

		public string Type { get; set; }

		public bool Exif { get; set; }

		public bool Colors { get; set; }

		public bool Faces { get; set; }

		public bool QualityAnalysis { get; set; }

		[Obsolete("Property Metadata is deprecated, please use ImageMetadata instead")]
		public bool? Metadata
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

		public bool? ImageMetadata { get; set; }

		public bool Coordinates { get; set; }

		public int MaxResults { get; set; }

		public bool Phash { get; set; }

		public bool Pages { get; set; }

		public string DerivedNextCursor { get; set; }

		public string Prefix { get; set; }

		public string NextCursor { get; set; }

		public bool? CinemagraphAnalysis { get; set; }

		public string StartAt { get; set; }

		public string Direction { get; set; }

		public bool? Tags { get; set; }

		public bool? Context { get; set; }

		public bool? Moderation { get; set; }

		public bool? AccessibilityAnalysis { get; set; }

		public bool? Versions { get; set; }

		public GetResourceParams(string publicId)
		{
			PublicId = publicId;
			Type = "upload";
			Exif = false;
			Colors = false;
			Faces = false;
			Pages = false;
		}

		public override void Check()
		{
			if (string.IsNullOrEmpty(PublicId))
			{
				throw new ArgumentException("PublicId must be set!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			if (MaxResults > 0)
			{
				BaseParams.AddParam(sortedDictionary, "max_results", MaxResults.ToString(CultureInfo.InvariantCulture));
			}
			BaseParams.AddParam(sortedDictionary, "exif", Exif);
			BaseParams.AddParam(sortedDictionary, "colors", Colors);
			BaseParams.AddParam(sortedDictionary, "faces", Faces);
			BaseParams.AddParam(sortedDictionary, "quality_analysis", QualityAnalysis);
			BaseParams.AddParam(sortedDictionary, "image_metadata", ImageMetadata);
			BaseParams.AddParam(sortedDictionary, "phash", Phash);
			BaseParams.AddParam(sortedDictionary, "coordinates", Coordinates);
			BaseParams.AddParam(sortedDictionary, "pages", Pages);
			BaseParams.AddParam(sortedDictionary, "derived_next_cursor", DerivedNextCursor);
			BaseParams.AddParam(sortedDictionary, "cinemagraph_analysis", CinemagraphAnalysis);
			BaseParams.AddParam(sortedDictionary, "tags", Tags);
			BaseParams.AddParam(sortedDictionary, "context", Context);
			BaseParams.AddParam(sortedDictionary, "moderation", Moderation);
			BaseParams.AddParam(sortedDictionary, "prefix", Prefix);
			BaseParams.AddParam(sortedDictionary, "next_cursor", NextCursor);
			BaseParams.AddParam(sortedDictionary, "start_at", StartAt);
			BaseParams.AddParam(sortedDictionary, "direction", Direction);
			BaseParams.AddParam(sortedDictionary, "accessibility_analysis", AccessibilityAnalysis);
			BaseParams.AddParam(sortedDictionary, "versions", Versions);
			return sortedDictionary;
		}
	}
}
