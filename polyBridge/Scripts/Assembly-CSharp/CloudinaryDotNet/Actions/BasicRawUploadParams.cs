using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class BasicRawUploadParams : BaseParams
	{
		public FileDescription File { get; set; }

		public string PublicId { get; set; }

		public string PublicIdPrefix { get; set; }

		public bool? Backup { get; set; }

		public string Type { get; set; }

		public virtual ResourceType ResourceType => ResourceType.Raw;

		public string FilenameOverride { get; set; }

		public override void Check()
		{
			if (File == null)
			{
				throw new ArgumentException("File must be specified in UploadParams!");
			}
			if (!File.IsRemote && File.Stream == null && string.IsNullOrEmpty(File.FilePath))
			{
				throw new ArgumentException("File is not ready!");
			}
			if (string.IsNullOrEmpty(File.FileName))
			{
				throw new ArgumentException("File name must be specified in UploadParams!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "public_id", PublicId);
			BaseParams.AddParam(sortedDictionary, "public_id_prefix", PublicIdPrefix);
			BaseParams.AddParam(sortedDictionary, "type", Type);
			BaseParams.AddParam(sortedDictionary, "filename_override", FilenameOverride);
			if (Backup.HasValue)
			{
				BaseParams.AddParam(sortedDictionary, "backup", Backup.Value);
			}
			return sortedDictionary;
		}
	}
}
