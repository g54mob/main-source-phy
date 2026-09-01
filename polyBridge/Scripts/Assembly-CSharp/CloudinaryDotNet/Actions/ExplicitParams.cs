using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class ExplicitParams : BaseParams
	{
		public List<Transformation> EagerTransforms { get; set; }

		public bool? EagerAsync { get; set; }

		public string EagerNotificationUrl { get; set; }

		public string Type { get; set; }

		public string Ocr { get; set; }

		public ResourceType ResourceType { get; set; }

		public string PublicId { get; set; }

		public Dictionary<string, string> Headers { get; set; }

		public string Tags { get; set; }

		public object FaceCoordinates { get; set; }

		public object CustomCoordinates { get; set; }

		public StringDictionary Context { get; set; }

		public StringDictionary Metadata { get; set; }

		public List<ResponsiveBreakpoint> ResponsiveBreakpoints { get; set; }

		public List<AccessControlRule> AccessControl { get; set; }

		public bool Invalidate { get; set; }

		public bool? Async { get; set; }

		public bool QualityAnalysis { get; set; }

		public bool? Overwrite { get; set; }

		public bool? CinemagraphAnalysis { get; set; }

		public bool? ImageMetadata { get; set; }

		public string NotificationUrl { get; set; }

		public bool? Colors { get; set; }

		public bool? Phash { get; set; }

		public bool? Faces { get; set; }

		public string QualityOverride { get; set; }

		public string Moderation { get; set; }

		public bool? AccessibilityAnalysis { get; set; }

		public ExplicitParams(string publicId)
		{
			PublicId = publicId;
			ResourceType = ResourceType.Image;
			Type = string.Empty;
			Tags = string.Empty;
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
			BaseParams.AddParam(sortedDictionary, "ocr", Ocr);
			BaseParams.AddParam(sortedDictionary, "eager_async", EagerAsync);
			BaseParams.AddParam(sortedDictionary, "eager_notification_url", EagerNotificationUrl);
			BaseParams.AddParam(sortedDictionary, "invalidate", Invalidate);
			BaseParams.AddParam(sortedDictionary, "async", Async);
			BaseParams.AddParam(sortedDictionary, "quality_analysis", QualityAnalysis);
			BaseParams.AddParam(sortedDictionary, "cinemagraph_analysis", CinemagraphAnalysis);
			BaseParams.AddParam(sortedDictionary, "overwrite", Overwrite);
			BaseParams.AddParam(sortedDictionary, "image_metadata", ImageMetadata);
			BaseParams.AddParam(sortedDictionary, "notification_url", NotificationUrl);
			BaseParams.AddParam(sortedDictionary, "quality_override", QualityOverride);
			BaseParams.AddParam(sortedDictionary, "moderation", Moderation);
			BaseParams.AddParam(sortedDictionary, "accessibility_analysis", AccessibilityAnalysis);
			if (ResourceType == ResourceType.Image)
			{
				BaseParams.AddParam(sortedDictionary, "colors", Colors);
				BaseParams.AddParam(sortedDictionary, "phash", Phash);
				BaseParams.AddParam(sortedDictionary, "faces", Faces);
			}
			BaseParams.AddCoordinates(sortedDictionary, "face_coordinates", FaceCoordinates);
			BaseParams.AddCoordinates(sortedDictionary, "custom_coordinates", CustomCoordinates);
			if (EagerTransforms != null)
			{
				BaseParams.AddParam(sortedDictionary, "eager", string.Join("|", EagerTransforms.Select((Transformation t) => t.Generate()).ToArray()));
			}
			if (Context != null && Context.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "context", Utils.SafeJoin("|", Context.SafePairs));
			}
			if (Metadata != null && Metadata.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "metadata", Utils.SafeJoin("|", Metadata.SafePairs));
			}
			if (ResponsiveBreakpoints != null && ResponsiveBreakpoints.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "responsive_breakpoints", JsonConvert.SerializeObject(ResponsiveBreakpoints));
			}
			if (AccessControl != null && AccessControl.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "access_control", JsonConvert.SerializeObject(AccessControl));
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
			return sortedDictionary;
		}
	}
}
