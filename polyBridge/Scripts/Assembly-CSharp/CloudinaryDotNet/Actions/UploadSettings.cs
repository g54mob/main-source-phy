using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class UploadSettings
	{
		[DataMember(Name = "disallow_public_id")]
		public bool DisallowPublicId { get; set; }

		[DataMember(Name = "backup")]
		public bool? Backup { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "tags")]
		public JToken Tags { get; set; }

		[DataMember(Name = "invalidate")]
		public bool Invalidate { get; set; }

		[DataMember(Name = "use_filename")]
		public bool UseFilename { get; set; }

		[DataMember(Name = "unique_filename")]
		public bool? UniqueFilename { get; set; }

		[DataMember(Name = "discard_original_filename")]
		public bool DiscardOriginalFilename { get; set; }

		[DataMember(Name = "notification_url")]
		public string NotificationUrl { get; set; }

		[DataMember(Name = "proxy")]
		public string Proxy { get; set; }

		[DataMember(Name = "folder")]
		public string Folder { get; set; }

		[DataMember(Name = "overwrite")]
		public bool? Overwrite { get; set; }

		[DataMember(Name = "raw_convert")]
		public string RawConvert { get; set; }

		[DataMember(Name = "context")]
		public JToken Context { get; set; }

		[DataMember(Name = "allowed_formats")]
		public JToken AllowedFormats { get; set; }

		[DataMember(Name = "moderation")]
		public string Moderation { get; set; }

		[DataMember(Name = "format")]
		public string Format { get; set; }

		[DataMember(Name = "transformation")]
		public JToken Transformation { get; set; }

		[DataMember(Name = "eager")]
		public JToken EagerTransforms { get; set; }

		[DataMember(Name = "exif")]
		public bool Exif { get; set; }

		[DataMember(Name = "colors")]
		public bool Colors { get; set; }

		[DataMember(Name = "faces")]
		public bool Faces { get; set; }

		[DataMember(Name = "quality_analysis")]
		public bool QualityAnalysis { get; set; }

		[DataMember(Name = "face_coordinates")]
		public JToken FaceCoordinates { get; set; }

		[Obsolete("Property Metadata is deprecated, please use ImageMetadata instead")]
		public bool Metadata
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
		public bool ImageMetadata { get; set; }

		[DataMember(Name = "eager_async")]
		public bool EagerAsync { get; set; }

		[DataMember(Name = "eager_notification_url")]
		public string EagerNotificationUrl { get; set; }

		[DataMember(Name = "categorization")]
		public string Categorization { get; set; }

		[DataMember(Name = "auto_tagging")]
		public float? AutoTagging { get; set; }

		[DataMember(Name = "detection")]
		public string Detection { get; set; }

		[DataMember(Name = "similarity_search")]
		public string SimilaritySearch { get; set; }

		[DataMember(Name = "ocr")]
		public string Ocr { get; set; }

		[JsonConverter(typeof(SafeBooleanConverter))]
		[DataMember(Name = "live")]
		public bool Live { get; set; }
	}
}
