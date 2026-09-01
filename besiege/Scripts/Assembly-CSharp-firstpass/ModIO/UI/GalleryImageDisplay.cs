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

		public RawImage image;

		private Texture current;

		public ModGalleryImageSize imageSize;

		public TextureChangedEvent onTextureChanged;

		private int m_modId;

		private GalleryImageLocator m_locator;

		public int ModId
		{
			get
			{
				return m_modId;
			}
		}

		public GalleryImageLocator Locator
		{
			get
			{
				return m_locator;
			}
		}

		public virtual void DisplayGalleryImage(int modId, GalleryImageLocator locator)
		{
			m_modId = modId;
			if (m_locator == locator)
			{
				return;
			}
			m_locator = locator;
			image.texture = null;
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
			if (this != null && m_locator == locator && texture != null)
			{
				if (texture.GetRawTextureData().Length > 0)
				{
					OnDestroy();
					current = texture;
					image.texture = texture;
					image.enabled = true;
					if (onTextureChanged != null)
					{
						onTextureChanged.Invoke(texture);
					}
					return;
				}
				Debug.LogError("[GalleryImageDisplay] Error: Zero length texture");
				if (onTextureChanged != null)
				{
					onTextureChanged.Invoke(texture);
				}
			}
			image.enabled = false;
			OnDestroy();
		}

		private void OnDestroy()
		{
			if ((bool)current)
			{
				UnityEngine.Object.DestroyImmediate(current);
			}
			current = null;
		}
	}
}
