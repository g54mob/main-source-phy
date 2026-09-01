using System;
using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(UserView))]
	public class AuthenticatedUserViewController : MonoBehaviour, IAuthenticatedUserUpdateReceiver
	{
		[Serializable]
		private struct UserProfileData
		{
			public UserProfile profile;

			public Texture2D avatar;
		}

		[SerializeField]
		private UserProfileData m_unauthenticatedUser = new UserProfileData
		{
			profile = new UserProfile(),
			avatar = null
		};

		[Obsolete]
		[SerializeField]
		[HideInInspector]
		private UserDisplayData m_guestData;

		public UserView view => base.gameObject.GetComponent<UserView>();

		protected virtual void Start()
		{
			m_unauthenticatedUser.profile.avatarLocator = new AvatarImageLocator
			{
				fileName = "_AVATAR_",
				original = ":GUEST_AVATAR:",
				thumbnail_50x50 = ":GUEST_AVATAR:",
				thumbnail_100x100 = ":GUEST_AVATAR:"
			};
			ImageRequestManager.instance.guestAvatar = m_unauthenticatedUser.avatar;
			view.profile = m_unauthenticatedUser.profile;
			ModManager.GetAuthenticatedUserProfile(delegate(UserProfile p)
			{
				if (this != null)
				{
					view.profile = p;
				}
			}, delegate(WebRequestError e)
			{
				MessageSystem.QueueMessage(MessageDisplayData.Type.Error, "Unable to fetch your profile from the mod.io servers.\n" + e.displayMessage);
			});
		}

		public void OnUserLoggedIn(UserProfile profile)
		{
			view.profile = profile;
		}

		public void OnUserLoggedOut()
		{
			view.profile = m_unauthenticatedUser.profile;
		}

		public void OnUserProfileUpdated(UserProfile profile)
		{
			view.profile = profile;
		}
	}
}
