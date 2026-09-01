using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class CreateSlideshowParams : BaseParams
	{
		public Transformation ManifestTransformation { get; set; }

		public SlideshowManifest ManifestJson { get; set; }

		public string PublicId { get; set; }

		public Transformation Transformation { get; set; }

		public List<string> Tags { get; set; }

		public bool? Overwrite { get; set; }

		public string NotificationUrl { get; set; }

		public string UploadPreset { get; set; }

		public override void Check()
		{
			if (ManifestTransformation == null && ManifestJson == null)
			{
				throw new ArgumentException("Please specify ManifestTransformation or ManifestJson");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "manifest_json", JsonConvert.SerializeObject(ManifestJson, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore
			}));
			BaseParams.AddParam(sortedDictionary, "manifest_transformation", ManifestTransformation?.Generate());
			BaseParams.AddParam(sortedDictionary, "public_id", PublicId);
			BaseParams.AddParam(sortedDictionary, "transformation", Transformation?.Generate());
			BaseParams.AddParam(sortedDictionary, "tags", Tags);
			BaseParams.AddParam(sortedDictionary, "overwrite", Overwrite);
			BaseParams.AddParam(sortedDictionary, "notification_url", NotificationUrl);
			BaseParams.AddParam(sortedDictionary, "upload_preset", UploadPreset);
			return sortedDictionary;
		}
	}
}
