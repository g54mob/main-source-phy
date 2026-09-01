using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class ModLogoDisplay : MonoBehaviour, IModViewElement
	{
		[Serializable]
		public class TextureChangedEvent : UnityEvent<Texture2D>
		{
		}

		public RawImage image;

		public Texture noImage;

		private Texture current;

		public LogoSize logoSize;

		public TextureChangedEvent onTextureChanged;

		private ModView m_view;

		private int m_modId;

		private LogoImageLocator m_locator;

		public int ModId
		{
			get
			{
				return m_modId;
			}
		}

		public LogoImageLocator Locator
		{
			get
			{
				return m_locator;
			}
		}

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
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

		public virtual void DisplayProfile(ModProfile profile)
		{
			int modId = 0;
			LogoImageLocator locator = null;
			if (profile != null)
			{
				modId = profile.id;
				locator = profile.logoLocator;
			}
			DisplayLogo(modId, locator);
		}

		public virtual void DisplayLogo(int modId, LogoImageLocator locator)
		{
			m_modId = modId;
			if (m_locator == locator)
			{
				return;
			}
			m_locator = locator;
			OnDestroy();
			image.texture = noImage;
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
				ImageRequestManager.instance.RequestModLogo(modId, locator, logoSize, action, action, null);
			}
		}

		protected virtual void ApplyTexture(LogoImageLocator locator, Texture2D texture)
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
				Debug.LogError("[RequestModLogo] Error: Zero length texture");
				if (onTextureChanged != null)
				{
					onTextureChanged.Invoke(texture);
				}
			}
			OnDestroy();
			image.texture = noImage;
			image.enabled = true;
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
