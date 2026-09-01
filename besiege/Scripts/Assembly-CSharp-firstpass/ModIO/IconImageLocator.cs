using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ModIO
{
	[Serializable]
	public class IconImageLocator : IMultiSizeImageLocator<IconSize>, IImageLocator
	{
		[JsonProperty("filename")]
		public string fileName;

		[JsonProperty("original")]
		public string original;

		[JsonProperty("thumb_64x64")]
		public string thumbnail_64x64;

		[JsonProperty("thumb_128x128")]
		public string thumbnail_128x128;

		[JsonProperty("thumb_256x256")]
		public string thumbnail_256x256;

		public string GetFileName()
		{
			return fileName;
		}

		public string GetURL()
		{
			return original;
		}

		public string GetSizeURL(IconSize size)
		{
			switch (size)
			{
			case IconSize.Original:
				return original;
			case IconSize.Thumbnail_64x64:
				return thumbnail_64x64;
			case IconSize.Thumbnail_128x128:
				return thumbnail_128x128;
			case IconSize.Thumbnail_256x256:
				return thumbnail_256x256;
			default:
				Debug.LogError("[mod.io] Unrecognized IconSize");
				return string.Empty;
			}
		}

		public SizeURLPair<IconSize>[] GetAllURLs()
		{
			return new SizeURLPair<IconSize>[4]
			{
				new SizeURLPair<IconSize>
				{
					size = IconSize.Original,
					url = original
				},
				new SizeURLPair<IconSize>
				{
					size = IconSize.Thumbnail_64x64,
					url = thumbnail_64x64
				},
				new SizeURLPair<IconSize>
				{
					size = IconSize.Thumbnail_128x128,
					url = thumbnail_128x128
				},
				new SizeURLPair<IconSize>
				{
					size = IconSize.Thumbnail_256x256,
					url = thumbnail_256x256
				}
			};
		}

		public IconSize GetOriginalSize()
		{
			return IconSize.Original;
		}
	}
}
