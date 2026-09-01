using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudinaryDotNet.Actions
{
	public class MultiAssetsParams : BaseParams
	{
		public string Tag { get; set; }

		public List<string> Urls { get; set; } = new List<string>();

		public Transformation Transformation { get; set; }

		public string NotificationUrl { get; set; }

		public bool Async { get; set; }

		public string Format { get; set; }

		public ArchiveCallMode? Mode { get; set; }

		public MultiAssetsParams(string tag)
		{
			Tag = tag;
		}

		public MultiAssetsParams(List<string> urls)
		{
			Urls = urls;
		}

		public override void Check()
		{
			bool flag = Urls == null || !Urls.Any();
			if (string.IsNullOrEmpty(Tag) && flag)
			{
				throw new ArgumentException("Either Tag or Urls must be specified");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "tag", Tag);
			BaseParams.AddParam(sortedDictionary, "notification_url", NotificationUrl);
			BaseParams.AddParam(sortedDictionary, "format", Format);
			BaseParams.AddParam(sortedDictionary, "async", Async);
			if (Urls != null && Urls.Any())
			{
				BaseParams.AddParam(sortedDictionary, "urls", Urls);
			}
			if (Transformation != null)
			{
				BaseParams.AddParam(sortedDictionary, "transformation", Transformation.Generate());
			}
			if (Mode.HasValue)
			{
				BaseParams.AddParam(sortedDictionary, "mode", ApiShared.GetCloudinaryParam(Mode.Value));
			}
			return sortedDictionary;
		}
	}
}
