using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class RawUploadParams : BasicRawUploadParams
	{
		public string Tags { get; set; }

		public bool? Invalidate { get; set; }

		public Dictionary<string, string> Headers { get; set; }

		public bool? UseFilename { get; set; }

		public bool? UniqueFilename { get; set; }

		public string DisplayName { get; set; }

		public bool? UseFilenameAsDisplayName { get; set; }

		public bool? DiscardOriginalFilename { get; set; }

		public string NotificationUrl { get; set; }

		public string AccessMode { get; set; }

		public string Proxy { get; set; }

		public string Folder { get; set; }

		public string AssetFolder { get; set; }

		public bool? Overwrite { get; set; }

		public string RawConvert { get; set; }

		public StringDictionary Context { get; set; }

		public StringDictionary MetadataFields { get; set; }

		public string[] AllowedFormats { get; set; }

		public string Moderation { get; set; }

		public string Async { get; set; }

		public List<AccessControlRule> AccessControl { get; set; }

		public string Eval { get; set; }

		public RawUploadParams()
		{
			Overwrite = true;
			UniqueFilename = true;
			Context = new StringDictionary();
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "tags", Tags);
			BaseParams.AddParam(sortedDictionary, "use_filename", UseFilename);
			BaseParams.AddParam(sortedDictionary, "moderation", Moderation);
			if (UseFilename.HasValue && UseFilename.Value)
			{
				BaseParams.AddParam(sortedDictionary, "unique_filename", UniqueFilename);
			}
			BaseParams.AddParam(sortedDictionary, "display_name", DisplayName);
			BaseParams.AddParam(sortedDictionary, "use_filename_as_display_name", UseFilenameAsDisplayName);
			if (AllowedFormats != null)
			{
				BaseParams.AddParam(sortedDictionary, "allowed_formats", string.Join(",", AllowedFormats));
			}
			BaseParams.AddParam(sortedDictionary, "invalidate", Invalidate);
			BaseParams.AddParam(sortedDictionary, "discard_original_filename", DiscardOriginalFilename);
			BaseParams.AddParam(sortedDictionary, "notification_url", NotificationUrl);
			BaseParams.AddParam(sortedDictionary, "access_mode", AccessMode);
			BaseParams.AddParam(sortedDictionary, "proxy", Proxy);
			BaseParams.AddParam(sortedDictionary, "folder", Folder);
			BaseParams.AddParam(sortedDictionary, "asset_folder", AssetFolder);
			BaseParams.AddParam(sortedDictionary, "raw_convert", RawConvert);
			BaseParams.AddParam(sortedDictionary, "overwrite", Overwrite);
			BaseParams.AddParam(sortedDictionary, "async", Async);
			BaseParams.AddParam(sortedDictionary, "eval", Eval);
			if (Context != null && Context.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "context", Utils.SafeJoin("|", Context.SafePairs));
			}
			if (MetadataFields != null && MetadataFields.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "metadata", Utils.SafeJoin("|", MetadataFields.SafePairs));
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
