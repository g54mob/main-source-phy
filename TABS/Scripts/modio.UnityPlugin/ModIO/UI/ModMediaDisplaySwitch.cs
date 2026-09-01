using UnityEngine;

namespace ModIO.UI
{
	public class ModMediaDisplaySwitch : MonoBehaviour, IModViewElement
	{
		public ModLogoDisplay logo;

		public GalleryImageDisplay galleryImage;

		public YouTubeThumbnailDisplay youTubeThumbnail;

		private ModView m_view;

		private ModProfile m_profile;

		GameObject IModViewElement.gameObject => base.gameObject;

		protected virtual void OnEnable()
		{
			bool flag = false;
			if (logo != null)
			{
				logo.gameObject.SetActive(!flag);
				flag = true;
			}
			if (galleryImage != null)
			{
				galleryImage.gameObject.SetActive(!flag);
				flag = true;
			}
			if (youTubeThumbnail != null)
			{
				youTubeThumbnail.gameObject.SetActive(!flag);
				flag = true;
			}
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayProfile);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayProfile);
					DisplayProfile(m_view.profile);
				}
				else
				{
					DisplayProfile(null);
				}
			}
		}

		public void DisplayProfile(ModProfile profile)
		{
			if (m_profile != profile)
			{
				m_profile = profile;
				int modId = 0;
				LogoImageLocator locator = null;
				if (profile != null)
				{
					modId = profile.id;
					locator = profile.logoLocator;
				}
				DisplayLogo(modId, locator);
			}
		}

		public void DisplayLogo(int modId, LogoImageLocator locator)
		{
			if (galleryImage != null)
			{
				galleryImage.gameObject.SetActive(value: false);
			}
			if (youTubeThumbnail != null)
			{
				youTubeThumbnail.gameObject.SetActive(value: false);
			}
			if (logo != null)
			{
				logo.gameObject.SetActive(locator != null);
				if (locator != null)
				{
					logo.DisplayLogo(modId, locator);
				}
			}
		}

		public void DisplayLogo(ModLogoDisplay display)
		{
			int modId = 0;
			LogoImageLocator locator = null;
			if (display != null)
			{
				modId = display.ModId;
				locator = display.Locator;
			}
			DisplayLogo(modId, locator);
		}

		public void DisplayGalleryImage(int modId, GalleryImageLocator locator)
		{
			if (logo != null)
			{
				logo.gameObject.SetActive(value: false);
			}
			if (youTubeThumbnail != null)
			{
				youTubeThumbnail.gameObject.SetActive(value: false);
			}
			if (galleryImage != null)
			{
				galleryImage.gameObject.SetActive(locator != null);
				if (locator != null)
				{
					galleryImage.DisplayGalleryImage(modId, locator);
				}
			}
		}

		public void DisplayGalleryImage(GalleryImageDisplay display)
		{
			int modId = 0;
			GalleryImageLocator locator = null;
			if (display != null)
			{
				modId = display.ModId;
				locator = display.Locator;
			}
			DisplayGalleryImage(modId, locator);
		}

		public void DisplayYouTubeThumbnail(int modId, string youTubeId)
		{
			if (logo != null)
			{
				logo.gameObject.SetActive(value: false);
			}
			if (galleryImage != null)
			{
				galleryImage.gameObject.SetActive(value: false);
			}
			if (youTubeThumbnail != null)
			{
				bool flag = !string.IsNullOrEmpty(youTubeId);
				youTubeThumbnail.gameObject.SetActive(flag);
				if (flag)
				{
					youTubeThumbnail.DisplayThumbnail(modId, youTubeId);
				}
			}
		}

		public void DisplayYouTubeThumbnail(YouTubeThumbnailDisplay display)
		{
			int modId = 0;
			string youTubeId = null;
			if (display != null)
			{
				modId = display.ModId;
				youTubeId = display.YouTubeId;
			}
			DisplayYouTubeThumbnail(modId, youTubeId);
		}
	}
}
