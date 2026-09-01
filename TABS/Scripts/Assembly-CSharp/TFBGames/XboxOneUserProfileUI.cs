using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	public class XboxOneUserProfileUI : UserProfileUI
	{
		[SerializeField]
		[Tooltip("Picture size to load for the user.")]
		protected XboxOneUserPictureSize pictureSize = XboxOneUserPictureSize.Medium;

		private ModalPanel modalPanel;

		public override void SetCanChangeProfile(bool canChangeProfile)
		{
			base.SetCanChangeProfile(canChangeProfile);
		}
	}
}
