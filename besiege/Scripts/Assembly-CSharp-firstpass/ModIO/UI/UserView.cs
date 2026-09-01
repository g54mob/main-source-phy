using System;
using UnityEngine;
using UnityEngine.Events;

namespace ModIO.UI
{
	[DisallowMultipleComponent]
	public class UserView : MonoBehaviour
	{
		[Serializable]
		public class ProfileChangedEvent : UnityEvent<UserProfile>
		{
		}

		[SerializeField]
		private UserProfile m_profile;

		public ProfileChangedEvent onProfileChanged;

		[HideInInspector]
		[Obsolete("Use UserAvatarDisplay component instead.")]
		public ImageDisplay avatarDisplay;

		[Obsolete("Use UserProfileFieldDisplay components instead.")]
		[HideInInspector]
		public UserProfileDisplayComponent profileDisplay;

		public UserProfile profile
		{
			get
			{
				return m_profile;
			}
			set
			{
				if (m_profile != value)
				{
					m_profile = value;
					if (onProfileChanged != null)
					{
						onProfileChanged.Invoke(m_profile);
					}
				}
			}
		}

		[Obsolete]
		public UserDisplayData data
		{
			get
			{
				if (m_profile == null)
				{
					return default(UserDisplayData);
				}
				return new UserDisplayData
				{
					profile = UserProfileDisplayData.CreateFromProfile(profile),
					avatar = ImageDisplayData.CreateForUserAvatar(profile.id, profile.avatarLocator)
				};
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public event Action<UserView> onClick;

		protected virtual void Awake()
		{
			IUserViewElement[] componentsInChildren = base.gameObject.GetComponentsInChildren<IUserViewElement>(true);
			IUserViewElement[] array = componentsInChildren;
			foreach (IUserViewElement userViewElement in array)
			{
				userViewElement.SetUserView(this);
			}
		}

		public void NotifyClicked()
		{
			if (this.onClick != null)
			{
				this.onClick(this);
			}
		}

		[Obsolete("No longer necessary.")]
		public void Initialize()
		{
		}

		[Obsolete("Use UserView.profile instead.")]
		public void DisplayUser(UserProfile userProfile)
		{
			profile = userProfile;
		}
	}
}
