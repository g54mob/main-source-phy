using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("Use ModLogoDisplay, GalleryImageDisplay, YouTubeThumbnailDisplay, and UserAvatarDisplay instead.")]
	public class ImageDisplay : MonoBehaviour
	{
		[FormerlySerializedAs("m_useOriginal")]
		[Tooltip("Display the image at its original resolution rather than using the thumbnail")]
		[Header("Settings")]
		public bool useOriginal;

		[Tooltip("If the desired version is not yet cached, show an alternate cached version instead of the loading display")]
		public bool enableFallback;

		[Header("UI Components")]
		public Image image;

		public AspectRatioFitter fitter;

		public GameObject loadingOverlay;

		public GameObject avatarOverlay;

		public GameObject logoOverlay;

		public GameObject galleryImageOverlay;

		public GameObject youTubeOverlay;

		[Header("Display Data")]
		[SerializeField]
		private ImageDisplayData m_data = default(ImageDisplayData);

		public ImageDisplayData data
		{
			get
			{
				return m_data;
			}
			set
			{
				m_data = value;
				PresentData();
			}
		}

		public event Action<ImageDisplay> onClick;

		private void PresentData()
		{
			string imageURL = m_data.GetImageURL(useOriginal);
			DisplayLoading();
			if (string.IsNullOrEmpty(imageURL))
			{
				return;
			}
			ImageDisplayData iData = m_data;
			ImageRequestManager.instance.RequestImageForData(m_data, useOriginal, delegate(Texture2D t)
			{
				if (this != null && iData.Equals(m_data))
				{
					if (loadingOverlay != null)
					{
						loadingOverlay.SetActive(false);
					}
					DisplayTexture(t);
					SetOverlayVisibility(true);
				}
			}, null);
		}

		private void DisplayTexture(Texture2D texture)
		{
			if (image != null)
			{
				image.sprite = UIUtilities.CreateSpriteFromTexture(texture);
				if (fitter != null)
				{
					fitter.aspectRatio = (float)texture.width / (float)texture.height;
				}
				image.enabled = true;
			}
		}

		private void SetOverlayVisibility(bool isVisible)
		{
			if (avatarOverlay != null)
			{
				avatarOverlay.SetActive(isVisible && m_data.descriptor == ImageDescriptor.UserAvatar);
			}
			if (logoOverlay != null)
			{
				logoOverlay.SetActive(isVisible && m_data.descriptor == ImageDescriptor.ModLogo);
			}
			if (galleryImageOverlay != null)
			{
				galleryImageOverlay.SetActive(isVisible && m_data.descriptor == ImageDescriptor.ModGalleryImage);
			}
			if (youTubeOverlay != null)
			{
				youTubeOverlay.SetActive(isVisible && m_data.descriptor == ImageDescriptor.YouTubeThumbnail);
			}
		}

		public void Initialize()
		{
			if (Application.isPlaying)
			{
			}
			if (loadingOverlay != null)
			{
				loadingOverlay.SetActive(false);
			}
			if (avatarOverlay != null)
			{
				avatarOverlay.SetActive(false);
			}
			if (logoOverlay != null)
			{
				logoOverlay.SetActive(false);
			}
			if (youTubeOverlay != null)
			{
				youTubeOverlay.SetActive(false);
			}
			if (galleryImageOverlay != null)
			{
				galleryImageOverlay.SetActive(false);
			}
		}

		public void DisplayAvatar(int userId, AvatarImageLocator locator)
		{
			ImageDisplayData imageDisplayData = ImageDisplayData.CreateForUserAvatar(userId, locator);
			m_data = imageDisplayData;
			PresentData();
		}

		public void DisplayLogo(int modId, LogoImageLocator locator)
		{
			ImageDisplayData imageDisplayData = ImageDisplayData.CreateForModLogo(modId, locator);
			m_data = imageDisplayData;
			PresentData();
		}

		public void DisplayGalleryImage(int modId, GalleryImageLocator locator)
		{
			ImageDisplayData imageDisplayData = ImageDisplayData.CreateForModGalleryImage(modId, locator);
			m_data = imageDisplayData;
			PresentData();
		}

		public void DisplayYouTubeThumbnail(int modId, string youTubeVideoId)
		{
			ImageDisplayData imageDisplayData = ImageDisplayData.CreateForYouTubeThumbnail(modId, youTubeVideoId);
			m_data = imageDisplayData;
			PresentData();
		}

		public void DisplayLoading()
		{
			if (image != null)
			{
				image.enabled = false;
			}
			if (loadingOverlay != null)
			{
				loadingOverlay.SetActive(true);
			}
			SetOverlayVisibility(false);
		}

		public void NotifyClicked()
		{
			if (this.onClick != null)
			{
				this.onClick(this);
			}
		}
	}
}
