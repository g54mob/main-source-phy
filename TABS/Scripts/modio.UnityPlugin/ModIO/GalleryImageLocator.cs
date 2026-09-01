using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ModIO
{
	[Serializable]
	public class GalleryImageLocator : IMultiSizeImageLocator<ModGalleryImageSize>, IImageLocator
	{
		[JsonProperty("filename")]
		public string fileName;

		[JsonProperty("original")]
		public string original;

		[JsonProperty("thumb_320x180")]
		public string thumbnail_320x180;

		public string GetFileName()
		{
			return fileName;
		}

		public string GetURL()
		{
			return original;
		}

		public string GetSizeURL(ModGalleryImageSize size)
		{
			switch (size)
			{
			case ModGalleryImageSize.Original:
				return original;
			case ModGalleryImageSize.Thumbnail_320x180:
				return thumbnail_320x180;
			default:
				Debug.LogError("[mod.io] Unrecognized ModGalleryImageSize");
				return string.Empty;
			}
		}

		public SizeURLPair<ModGalleryImageSize>[] GetAllURLs()
		{
			return new SizeURLPair<ModGalleryImageSize>[2]
			{
				new SizeURLPair<ModGalleryImageSize>
				{
					size = ModGalleryImageSize.Original,
					url = original
				},
				new SizeURLPair<ModGalleryImageSize>
				{
					size = ModGalleryImageSize.Thumbnail_320x180,
					url = thumbnail_320x180
				}
			};
		}

		public ModGalleryImageSize GetOriginalSize()
		{
			return ModGalleryImageSize.Original;
		}
	}
}
