using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class ImageUploadParams : RawUploadParams
	{
		public string Format { get; set; }

		public Transformation Transformation { get; set; }

		public List<Transformation> EagerTransforms { get; set; }

		public new string Type
		{
			get
			{
				return base.Type;
			}
			set
			{
				base.Type = value;
			}
		}

		public override ResourceType ResourceType => ResourceType.Image;

		public bool? Exif { get; set; }

		public bool? Colors { get; set; }

		public bool? Faces { get; set; }

		public bool? QualityAnalysis { get; set; }

		public object FaceCoordinates { get; set; }

		public object CustomCoordinates { get; set; }

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

		public bool? EagerAsync { get; set; }

		public string EagerNotificationUrl { get; set; }

		public string Categorization { get; set; }

		public string BackgroundRemoval { get; set; }

		public float? AutoTagging { get; set; }

		public string Detection { get; set; }

		public string SimilaritySearch { get; set; }

		public string Ocr { get; set; }

		public bool? ReturnDeleteToken { get; set; }

		public string UploadPreset { get; set; }

		public bool? Unsigned { get; set; }

		public bool? Phash { get; set; }

		public List<ResponsiveBreakpoint> ResponsiveBreakpoints { get; set; }

		public bool? CinemagraphAnalysis { get; set; }

		public bool? AccessibilityAnalysis { get; set; }

		public ImageUploadParams()
		{
			base.Overwrite = null;
			base.UniqueFilename = null;
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "format", Format);
			BaseParams.AddParam(sortedDictionary, "exif", Exif);
			BaseParams.AddParam(sortedDictionary, "faces", Faces);
			BaseParams.AddParam(sortedDictionary, "quality_analysis", QualityAnalysis);
			BaseParams.AddParam(sortedDictionary, "colors", Colors);
			BaseParams.AddParam(sortedDictionary, "image_metadata", ImageMetadata);
			BaseParams.AddParam(sortedDictionary, "eager_async", EagerAsync);
			BaseParams.AddParam(sortedDictionary, "eager_notification_url", EagerNotificationUrl);
			BaseParams.AddParam(sortedDictionary, "categorization", Categorization);
			BaseParams.AddParam(sortedDictionary, "detection", Detection);
			BaseParams.AddParam(sortedDictionary, "ocr", Ocr);
			BaseParams.AddParam(sortedDictionary, "similarity_search", SimilaritySearch);
			BaseParams.AddParam(sortedDictionary, "upload_preset", UploadPreset);
			BaseParams.AddParam(sortedDictionary, "unsigned", Unsigned);
			BaseParams.AddParam(sortedDictionary, "phash", Phash);
			BaseParams.AddParam(sortedDictionary, "background_removal", BackgroundRemoval);
			BaseParams.AddParam(sortedDictionary, "return_delete_token", ReturnDeleteToken);
			BaseParams.AddParam(sortedDictionary, "cinemagraph_analysis", CinemagraphAnalysis);
			BaseParams.AddParam(sortedDictionary, "accessibility_analysis", AccessibilityAnalysis);
			if (AutoTagging.HasValue)
			{
				BaseParams.AddParam(sortedDictionary, "auto_tagging", AutoTagging.Value);
			}
			BaseParams.AddCoordinates(sortedDictionary, "face_coordinates", FaceCoordinates);
			BaseParams.AddCoordinates(sortedDictionary, "custom_coordinates", CustomCoordinates);
			if (Transformation != null)
			{
				BaseParams.AddParam(sortedDictionary, "transformation", Transformation.Generate());
			}
			if (EagerTransforms != null && EagerTransforms.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "eager", string.Join("|", EagerTransforms.Select((Transformation t) => t.Generate()).ToArray()));
			}
			if (ResponsiveBreakpoints != null && ResponsiveBreakpoints.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "responsive_breakpoints", JsonConvert.SerializeObject(ResponsiveBreakpoints));
			}
			return sortedDictionary;
		}
	}
}
