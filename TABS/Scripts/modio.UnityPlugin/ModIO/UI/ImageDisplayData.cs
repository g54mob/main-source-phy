using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ModIO.UI
{
	[Serializable]
	[Obsolete("No longer supported.")]
	public struct ImageDisplayData
	{
		[Obsolete("Use ModIO.UI.ImageDescriptor instead.")]
		public enum MediaType
		{
			None = 0,
			ModLogo = 1,
			ModGalleryImage = 2,
			YouTubeThumbnail = 3,
			UserAvatar = 4
		}

		public static UserAvatarSize avatarThumbnailSize = UserAvatarSize.Thumbnail_50x50;

		public static LogoSize logoThumbnailSize = LogoSize.Thumbnail_320x180;

		public static ModGalleryImageSize galleryThumbnailSize = ModGalleryImageSize.Thumbnail_320x180;

		public int ownerId;

		[FormerlySerializedAs("mediaType")]
		public ImageDescriptor descriptor;

		public string imageId;

		public string originalURL;

		public string thumbnailURL;

		[Obsolete("Images are now to be fetched via ImageRequestManager")]
		public Texture2D originalTexture;

		[Obsolete("Images are now to be fetched via ImageRequestManager")]
		public Texture2D thumbnailTexture;

		public int modId
		{
			get
			{
				return ownerId;
			}
			set
			{
				ownerId = value;
			}
		}

		public int userId
		{
			get
			{
				return ownerId;
			}
			set
			{
				ownerId = value;
			}
		}

		public string fileName
		{
			get
			{
				return imageId;
			}
			set
			{
				imageId = value;
			}
		}

		public string youTubeId
		{
			get
			{
				return imageId;
			}
			set
			{
				imageId = value;
			}
		}

		[Obsolete("Use ImageDisplayData.descriptor instead.")]
		public MediaType mediaType
		{
			get
			{
				return (MediaType)descriptor;
			}
			set
			{
				descriptor = (ImageDescriptor)value;
			}
		}

		public string GetImageURL(bool original)
		{
			if (original)
			{
				return originalURL;
			}
			return thumbnailURL;
		}

		public static ImageDisplayData CreateForModLogo(int modId, LogoImageLocator locator)
		{
			return new ImageDisplayData
			{
				ownerId = modId,
				descriptor = ImageDescriptor.ModLogo,
				imageId = locator.GetFileName(),
				originalURL = locator.GetSizeURL(LogoSize.Original),
				thumbnailURL = locator.GetSizeURL(logoThumbnailSize)
			};
		}

		public static ImageDisplayData CreateForModGalleryImage(int modId, GalleryImageLocator locator)
		{
			return new ImageDisplayData
			{
				ownerId = modId,
				descriptor = ImageDescriptor.ModGalleryImage,
				imageId = locator.GetFileName(),
				originalURL = locator.GetSizeURL(ModGalleryImageSize.Original),
				thumbnailURL = locator.GetSizeURL(galleryThumbnailSize)
			};
		}

		public static ImageDisplayData CreateForYouTubeThumbnail(int modId, string youTubeId)
		{
			string text = Utility.GenerateYouTubeThumbnailURL(youTubeId);
			return new ImageDisplayData
			{
				ownerId = modId,
				descriptor = ImageDescriptor.YouTubeThumbnail,
				imageId = youTubeId,
				originalURL = text,
				thumbnailURL = text
			};
		}

		public static ImageDisplayData CreateForUserAvatar(int userId, AvatarImageLocator locator)
		{
			return new ImageDisplayData
			{
				ownerId = userId,
				descriptor = ImageDescriptor.UserAvatar,
				imageId = locator.GetFileName(),
				originalURL = locator.GetSizeURL(UserAvatarSize.Original),
				thumbnailURL = locator.GetSizeURL(avatarThumbnailSize)
			};
		}

		[Obsolete("Images are now to be fetched via ImageRequestManager")]
		public Texture2D GetImageTexture(bool original)
		{
			return null;
		}

		[Obsolete("Images are now to be fetched via ImageRequestManager")]
		public void SetImageTexture(bool original, Texture2D value)
		{
		}
	}
}
