using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ModIO
{
	[Serializable]
	public class LogoImageLocator : IMultiSizeImageLocator<LogoSize>, IImageLocator
	{
		[JsonProperty("filename")]
		public string fileName;

		[JsonProperty("original")]
		public string original;

		[JsonProperty("thumb_320x180")]
		public string thumbnail_320x180;

		[JsonProperty("thumb_640x360")]
		public string thumbnail_640x360;

		[JsonProperty("thumb_1280x720")]
		public string thumbnail_1280x720;

		public string GetFileName()
		{
			return fileName;
		}

		public string GetURL()
		{
			return original;
		}

		public string GetSizeURL(LogoSize size)
		{
			switch (size)
			{
			case LogoSize.Original:
				return original;
			case LogoSize.Thumbnail_320x180:
				return thumbnail_320x180;
			case LogoSize.Thumbnail_640x360:
				return thumbnail_640x360;
			case LogoSize.Thumbnail_1280x720:
				return thumbnail_1280x720;
			default:
				Debug.LogError("[mod.io] Unrecognized LogoSize");
				return string.Empty;
			}
		}

		public SizeURLPair<LogoSize>[] GetAllURLs()
		{
			return new SizeURLPair<LogoSize>[4]
			{
				new SizeURLPair<LogoSize>
				{
					size = LogoSize.Original,
					url = original
				},
				new SizeURLPair<LogoSize>
				{
					size = LogoSize.Thumbnail_320x180,
					url = thumbnail_320x180
				},
				new SizeURLPair<LogoSize>
				{
					size = LogoSize.Thumbnail_640x360,
					url = thumbnail_640x360
				},
				new SizeURLPair<LogoSize>
				{
					size = LogoSize.Thumbnail_1280x720,
					url = thumbnail_1280x720
				}
			};
		}

		public LogoSize GetOriginalSize()
		{
			return LogoSize.Original;
		}
	}
}
