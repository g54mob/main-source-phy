using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ModIO
{
	[Serializable]
	public class AvatarImageLocator : IMultiSizeImageLocator<UserAvatarSize>, IImageLocator
	{
		[JsonProperty("filename")]
		public string fileName;

		[JsonProperty("original")]
		public string original;

		[JsonProperty("thumb_50x50")]
		public string thumbnail_50x50;

		[JsonProperty("thumb_100x100")]
		public string thumbnail_100x100;

		public string GetFileName()
		{
			return fileName;
		}

		public string GetURL()
		{
			return original;
		}

		public string GetSizeURL(UserAvatarSize size)
		{
			switch (size)
			{
			case UserAvatarSize.Original:
				return original;
			case UserAvatarSize.Thumbnail_50x50:
				return thumbnail_50x50;
			case UserAvatarSize.Thumbnail_100x100:
				return thumbnail_100x100;
			default:
				Debug.LogError("[mod.io] Unrecognized UserAvatarSize");
				return string.Empty;
			}
		}

		public SizeURLPair<UserAvatarSize>[] GetAllURLs()
		{
			return new SizeURLPair<UserAvatarSize>[3]
			{
				new SizeURLPair<UserAvatarSize>
				{
					size = UserAvatarSize.Original,
					url = original
				},
				new SizeURLPair<UserAvatarSize>
				{
					size = UserAvatarSize.Thumbnail_50x50,
					url = thumbnail_50x50
				},
				new SizeURLPair<UserAvatarSize>
				{
					size = UserAvatarSize.Thumbnail_100x100,
					url = thumbnail_100x100
				}
			};
		}

		public UserAvatarSize GetOriginalSize()
		{
			return UserAvatarSize.Original;
		}
	}
}
