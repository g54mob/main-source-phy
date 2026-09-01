using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CloudinaryDotNet.Core;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	public class UploadPresetParams : BaseParams
	{
		public string Name { get; set; }

		public bool Unsigned { get; set; }

		public bool DisallowPublicId { get; set; }

		public bool? Backup { get; set; }

		public string Type { get; set; }

		public string Tags { get; set; }

		public bool Invalidate { get; set; }

		public bool UseFilename { get; set; }

		public bool? UniqueFilename { get; set; }

		public bool DiscardOriginalFilename { get; set; }

		public string NotificationUrl { get; set; }

		public string Proxy { get; set; }

		public string Folder { get; set; }

		public bool? Overwrite { get; set; }

		public string RawConvert { get; set; }

		public StringDictionary Context { get; set; }

		public string[] AllowedFormats { get; set; }

		public string Moderation { get; set; }

		public string Format { get; set; }

		public object Transformation { get; set; }

		public ICollection<object> EagerTransforms { get; set; }

		public bool Exif { get; set; }

		public bool Colors { get; set; }

		public bool Faces { get; set; }

		public bool QualityAnalysis { get; set; }

		public object FaceCoordinates { get; set; }

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

		public bool ImageMetadata { get; set; }

		public bool EagerAsync { get; set; }

		public string EagerNotificationUrl { get; set; }

		public string Categorization { get; set; }

		public float? AutoTagging { get; set; }

		public string Detection { get; set; }

		public string SimilaritySearch { get; set; }

		public string Ocr { get; set; }

		public bool Live { get; set; }

		public string Eval { get; set; }

		public bool? AccessibilityAnalysis { get; set; }

		public UploadPresetParams()
		{
		}

		public UploadPresetParams(GetUploadPresetResult preset)
		{
			Name = preset.Name;
			Unsigned = preset.Unsigned;
			if (preset.Settings == null)
			{
				return;
			}
			DisallowPublicId = preset.Settings.DisallowPublicId;
			Backup = preset.Settings.Backup;
			Type = preset.Settings.Type;
			if (preset.Settings.Tags != null)
			{
				if (preset.Settings.Tags.Type == JTokenType.String)
				{
					Tags = preset.Settings.Tags.ToString();
				}
				else if (preset.Settings.Tags.Type == JTokenType.Array)
				{
					Tags = string.Join(",", preset.Settings.Tags.Values<string>().ToArray());
				}
			}
			Invalidate = preset.Settings.Invalidate;
			UseFilename = preset.Settings.UseFilename;
			UniqueFilename = preset.Settings.UniqueFilename;
			DiscardOriginalFilename = preset.Settings.DiscardOriginalFilename;
			NotificationUrl = preset.Settings.NotificationUrl;
			Proxy = preset.Settings.Proxy;
			Folder = preset.Settings.Folder;
			Overwrite = preset.Settings.Overwrite;
			RawConvert = preset.Settings.RawConvert;
			if (preset.Settings.Context != null)
			{
				Context = new StringDictionary();
				foreach (JProperty item in (IEnumerable<JToken>)preset.Settings.Context)
				{
					Context.Add(item.Name, item.Value.ToString());
				}
			}
			if (preset.Settings.AllowedFormats != null)
			{
				if (preset.Settings.AllowedFormats.Type == JTokenType.String)
				{
					AllowedFormats = preset.Settings.AllowedFormats.ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				}
				else if (preset.Settings.AllowedFormats.Type == JTokenType.Array)
				{
					AllowedFormats = preset.Settings.AllowedFormats.Select((JToken t) => t.ToString()).ToArray();
				}
			}
			Moderation = preset.Settings.Moderation;
			Format = preset.Settings.Format;
			if (preset.Settings.Transformation != null)
			{
				if (preset.Settings.Transformation.Type == JTokenType.String)
				{
					Transformation = preset.Settings.Transformation.ToString();
				}
				else if (preset.Settings.Transformation.Type == JTokenType.Array)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					foreach (JObject item2 in (IEnumerable<JToken>)preset.Settings.Transformation)
					{
						foreach (KeyValuePair<string, JToken> item3 in item2)
						{
							dictionary.Add(item3.Key, item3.Value.ToString());
						}
					}
					Transformation = new Transformation(dictionary);
				}
			}
			if (preset.Settings.EagerTransforms != null)
			{
				EagerTransforms = new List<object>();
				foreach (JToken item4 in (IEnumerable<JToken>)preset.Settings.EagerTransforms)
				{
					if (item4.Type == JTokenType.String)
					{
						EagerTransforms.Add(item4.ToString());
					}
					else
					{
						if (item4.Type != JTokenType.Array)
						{
							continue;
						}
						Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
						foreach (JObject item5 in (IEnumerable<JToken>)item4)
						{
							foreach (KeyValuePair<string, JToken> item6 in item5)
							{
								dictionary2.Add(item6.Key, item6.Value.ToString());
							}
						}
						EagerTransforms.Add(new Transformation(dictionary2));
					}
				}
			}
			Exif = preset.Settings.Exif;
			Colors = preset.Settings.Colors;
			Faces = preset.Settings.Faces;
			QualityAnalysis = preset.Settings.QualityAnalysis;
			if (preset.Settings.FaceCoordinates != null)
			{
				if (preset.Settings.FaceCoordinates.Type == JTokenType.String)
				{
					FaceCoordinates = preset.Settings.FaceCoordinates.ToString();
				}
				else if (preset.Settings.FaceCoordinates.Type == JTokenType.Array)
				{
					List<Rectangle> list = new List<Rectangle>();
					foreach (JToken item7 in (IEnumerable<JToken>)preset.Settings.FaceCoordinates)
					{
						list.Add(new Rectangle(item7[0].Value<int>(), item7[1].Value<int>(), item7[2].Value<int>(), item7[3].Value<int>()));
					}
				}
			}
			ImageMetadata = preset.Settings.ImageMetadata;
			EagerAsync = preset.Settings.EagerAsync;
			EagerNotificationUrl = preset.Settings.EagerNotificationUrl;
			Categorization = preset.Settings.Categorization;
			AutoTagging = preset.Settings.AutoTagging;
			Detection = preset.Settings.Detection;
			SimilaritySearch = preset.Settings.SimilaritySearch;
			Ocr = preset.Settings.Ocr;
			Live = preset.Settings.Live;
		}

		public override void Check()
		{
			if (Overwrite.HasValue && Overwrite.Value && Unsigned)
			{
				throw new ArgumentException("Don't set both Overwrite and Unsigned to true!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "name", Name);
			BaseParams.AddParam(sortedDictionary, "unsigned", Unsigned);
			BaseParams.AddParam(sortedDictionary, "disallow_public_id", DisallowPublicId);
			BaseParams.AddParam(sortedDictionary, "type", Type);
			BaseParams.AddParam(sortedDictionary, "tags", Tags);
			BaseParams.AddParam(sortedDictionary, "use_filename", UseFilename);
			BaseParams.AddParam(sortedDictionary, "moderation", Moderation);
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
			BaseParams.AddParam(sortedDictionary, "invalidate", Invalidate);
			BaseParams.AddParam(sortedDictionary, "discard_original_filename", DiscardOriginalFilename);
			BaseParams.AddParam(sortedDictionary, "notification_url", NotificationUrl);
			BaseParams.AddParam(sortedDictionary, "proxy", Proxy);
			BaseParams.AddParam(sortedDictionary, "folder", Folder);
			BaseParams.AddParam(sortedDictionary, "raw_convert", RawConvert);
			BaseParams.AddParam(sortedDictionary, "backup", Backup);
			BaseParams.AddParam(sortedDictionary, "overwrite", Overwrite);
			BaseParams.AddParam(sortedDictionary, "unique_filename", UniqueFilename);
			BaseParams.AddParam(sortedDictionary, "live", Live);
			BaseParams.AddParam(sortedDictionary, "eval", Eval);
			BaseParams.AddParam(sortedDictionary, "accessibility_analysis", AccessibilityAnalysis);
			BaseParams.AddParam(sortedDictionary, "transformation", GetTransformation(Transformation));
			if (AutoTagging.HasValue)
			{
				BaseParams.AddParam(sortedDictionary, "auto_tagging", AutoTagging.Value);
			}
			if (FaceCoordinates != null)
			{
				BaseParams.AddParam(sortedDictionary, "face_coordinates", FaceCoordinates.ToString());
			}
			if (EagerTransforms != null && EagerTransforms.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "eager", string.Join("|", EagerTransforms.Select(GetTransformation).ToArray()));
			}
			if (AllowedFormats != null)
			{
				BaseParams.AddParam(sortedDictionary, "allowed_formats", string.Join(",", AllowedFormats));
			}
			if (Context != null && Context.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "context", Utils.SafeJoin("|", Context.SafePairs));
			}
			return sortedDictionary;
		}

		private string GetTransformation(object o)
		{
			if (o == null)
			{
				return null;
			}
			if (o is string)
			{
				return (string)o;
			}
			if (o is Transformation)
			{
				return ((Transformation)o).Generate();
			}
			throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Instance of type {0} is not supported as Transformation!", o.GetType()));
		}
	}
}
