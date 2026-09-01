using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class UpdateParams : BaseParams
	{
		public string PublicId { get; set; }

		public ResourceType ResourceType { get; set; }

		public string Type { get; set; }

		public Dictionary<string, string> Headers { get; set; }

		public string Tags { get; set; }

		public StringDictionary Context { get; set; }

		public StringDictionary Metadata { get; set; }

		public string RawConvert { get; set; }

		public object FaceCoordinates { get; set; }

		public object CustomCoordinates { get; set; }

		public string Categorization { get; set; }

		public string BackgroundRemoval { get; set; }

		public float? AutoTagging { get; set; }

		public string Detection { get; set; }

		public string SimilaritySearch { get; set; }

		public string Ocr { get; set; }

		public string NotificationUrl { get; set; }

		[Obsolete("Property QualityOveride is deprecated, please use QualityOverride instead")]
		public string QualityOveride
		{
			get
			{
				return QualityOverride;
			}
			set
			{
				QualityOverride = value;
			}
		}

		public string QualityOverride { get; set; }

		public ModerationStatus ModerationStatus { get; set; }

		public List<AccessControlRule> AccessControl { get; set; }

		public UpdateParams(string publicId)
		{
			PublicId = publicId;
			Type = "upload";
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
			BaseParams.AddParam(sortedDictionary, "public_id", PublicId);
			BaseParams.AddParam(sortedDictionary, "tags", Tags);
			BaseParams.AddParam(sortedDictionary, "type", Type);
			BaseParams.AddParam(sortedDictionary, "categorization", Categorization);
			BaseParams.AddParam(sortedDictionary, "detection", Detection);
			BaseParams.AddParam(sortedDictionary, "ocr", Ocr);
			BaseParams.AddParam(sortedDictionary, "similarity_search", SimilaritySearch);
			BaseParams.AddParam(sortedDictionary, "background_removal", BackgroundRemoval);
			if (!string.IsNullOrWhiteSpace(NotificationUrl))
			{
				BaseParams.AddParam(sortedDictionary, "notification_url", NotificationUrl);
			}
			if (ModerationStatus != ModerationStatus.Pending)
			{
				BaseParams.AddParam(sortedDictionary, "moderation_status", ApiShared.GetCloudinaryParam(ModerationStatus));
			}
			if (AutoTagging.HasValue)
			{
				BaseParams.AddParam(sortedDictionary, "auto_tagging", AutoTagging.Value);
			}
			BaseParams.AddParam(sortedDictionary, "raw_convert", RawConvert);
			if (Context != null && Context.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "context", Utils.SafeJoin("|", Context.SafePairs));
			}
			if (Metadata != null && Metadata.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "metadata", Utils.SafeJoin("|", Metadata.SafePairs));
			}
			BaseParams.AddCoordinates(sortedDictionary, "face_coordinates", FaceCoordinates);
			BaseParams.AddCoordinates(sortedDictionary, "custom_coordinates", CustomCoordinates);
			if (!string.IsNullOrWhiteSpace(QualityOverride))
			{
				BaseParams.AddParam(sortedDictionary, "quality_override", QualityOverride);
			}
			if (Headers != null && Headers.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, string> header in Headers)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}: {1}\n", header.Key, header.Value);
				}
				sortedDictionary.Add("headers", stringBuilder.ToString());
			}
			if (AccessControl != null && AccessControl.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "access_control", JsonConvert.SerializeObject(AccessControl));
			}
			return sortedDictionary;
		}
	}
}
