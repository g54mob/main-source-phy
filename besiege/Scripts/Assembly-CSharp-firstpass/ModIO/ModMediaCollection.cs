using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModMediaCollection
	{
		[JsonProperty("youtube")]
		public string[] youTubeURLs;

		[JsonProperty("sketchfab")]
		public string[] sketchfabURLs;

		[JsonProperty("images")]
		public GalleryImageLocator[] galleryImageLocators;

		[JsonProperty("gif_images")]
		public GalleryImageLocator[] galleryGIFLocators;

		public GalleryImageLocator GetGalleryImageWithFileName(string fileName)
		{
			GalleryImageLocator[] array = galleryImageLocators;
			foreach (GalleryImageLocator galleryImageLocator in array)
			{
				if (galleryImageLocator.fileName == fileName)
				{
					return galleryImageLocator;
				}
			}
			return null;
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (galleryGIFLocators != null || galleryImageLocators == null)
			{
				return;
			}
			List<GalleryImageLocator> list = new List<GalleryImageLocator>();
			List<GalleryImageLocator> list2 = new List<GalleryImageLocator>();
			GalleryImageLocator[] array = galleryImageLocators;
			foreach (GalleryImageLocator galleryImageLocator in array)
			{
				string extension = Path.GetExtension(galleryImageLocator.fileName);
				if (extension.ToUpper() == ".GIF")
				{
					list.Add(galleryImageLocator);
				}
				else
				{
					list2.Add(galleryImageLocator);
				}
			}
			galleryImageLocators = list2.ToArray();
			galleryGIFLocators = list.ToArray();
		}
	}
}
