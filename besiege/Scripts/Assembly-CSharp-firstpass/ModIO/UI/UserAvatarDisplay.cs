using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class UserAvatarDisplay : MonoBehaviour, IUserViewElement
	{
		[Serializable]
		public class TextureChangedEvent : UnityEvent<Texture2D>
		{
		}

		public Image image;

		private Texture current;

		public UserAvatarSize avatarSize;

		public TextureChangedEvent onTextureChanged;

		private UserView m_view;

		private int m_userId = -1;

		private AvatarImageLocator m_locator;

		public int UserId
		{
			get
			{
				return m_userId;
			}
		}

		public AvatarImageLocator Locator
		{
			get
			{
				return m_locator;
			}
		}

		public void SetUserView(UserView view)
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

		public virtual void DisplayProfile(UserProfile profile)
		{
			int userId = -1;
			AvatarImageLocator locator = null;
			if (profile != null)
			{
				userId = profile.id;
				locator = profile.avatarLocator;
			}
			DisplayAvatar(userId, locator);
		}

		public virtual void DisplayAvatar(int userId, AvatarImageLocator locator)
		{
			m_userId = userId;
			if (m_locator == locator)
			{
				return;
			}
			m_locator = locator;
			OnDestroy();
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
				ImageRequestManager.instance.RequestUserAvatar(userId, locator, avatarSize, action, action, null);
			}
		}

		protected virtual void ApplyTexture(AvatarImageLocator locator, Texture2D texture)
		{
			if (this != null && m_locator == locator && texture != null)
			{
				OnDestroy();
				current = texture;
				image.sprite = UIUtilities.CreateSpriteFromTexture(texture);
				image.enabled = true;
				if (onTextureChanged != null)
				{
					onTextureChanged.Invoke(texture);
				}
			}
			else
			{
				OnDestroy();
			}
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
