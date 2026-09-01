using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class GalleryImageDisplay : MonoBehaviour
	{
		[Serializable]
		public class TextureChangedEvent : UnityEvent<Texture2D>
		{
		}

		public Image image;

		public ModGalleryImageSize imageSize;

		public TextureChangedEvent onTextureChanged;

		private int m_modId;

		private GalleryImageLocator m_locator;

		public int ModId => m_modId;

		public GalleryImageLocator Locator => m_locator;

		public virtual void DisplayGalleryImage(int modId, GalleryImageLocator locator)
		{
			m_modId = modId;
			if (m_locator == locator)
			{
				return;
			}
			m_locator = locator;
			image.sprite = null;
			image.enabled = false;
			if (onTextureChanged != null)
			{
				onTextureChanged.Invoke(null);
			}
			if (locator != null)
			{
				Action<Texture2D> action = delegate(Texture2D t)
				{
					ApplyTexture(locator, t);
				};
				Action<Texture2D> onFallbackFound = null;
				if (imageSize == ModGalleryImageSize.Original)
				{
					onFallbackFound = action;
				}
				ImageRequestManager.instance.RequestModGalleryImage(modId, locator, imageSize, action, onFallbackFound, null);
			}
		}

		protected virtual void ApplyTexture(GalleryImageLocator locator, Texture2D texture)
		{
			if (this != null && texture != null && m_locator == locator)
			{
				image.sprite = UIUtilities.CreateSpriteFromTexture(texture);
				image.enabled = true;
				if (onTextureChanged != null)
				{
					onTextureChanged.Invoke(texture);
				}
			}
		}
	}
}
