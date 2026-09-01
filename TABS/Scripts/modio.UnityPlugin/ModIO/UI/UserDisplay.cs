using UnityEngine;

namespace ModIO.UI
{
	public class UserDisplay : MonoBehaviour
	{
		public TestMenu menuComponent;

		[Header("xbox")]
		public GenericTextComponent xblUserToken;

		public GenericTextComponent xblUserId;

		public GenericTextComponent xblUserOnlineId;

		[Header("mod.io")]
		public GenericTextComponent modioUserToken;

		public GenericTextComponent modioUserId;

		public GenericTextComponent modioUserName;

		public GenericTextComponent modioPlatformUser;

		public GenericTextComponent modioSubCount;

		private void OnGUI()
		{
			string text = LocalUser.OAuthToken;
			UserProfile userProfile = LocalUser.Profile;
			string colorName;
			if (string.IsNullOrEmpty(text))
			{
				colorName = "red";
				text = ((text != null) ? "EMPTY" : "NULL");
			}
			else
			{
				colorName = "cyan";
			}
			if (modioUserToken.displayComponent != null)
			{
				modioUserToken.text = BuildText(colorName, text);
			}
			if (userProfile == null)
			{
				colorName = "red";
				userProfile = new UserProfile
				{
					id = -1,
					username = "NULL",
					usernamePlatform = "NULL"
				};
			}
			else
			{
				colorName = "cyan";
			}
			if (modioUserId.displayComponent != null)
			{
				modioUserId.text = BuildText(colorName, userProfile.id.ToString());
			}
			if (modioUserName.displayComponent != null)
			{
				modioUserName.text = BuildText(colorName, userProfile.username);
			}
			if (modioPlatformUser.displayComponent != null)
			{
				if (string.IsNullOrEmpty(userProfile.usernamePlatform))
				{
					userProfile.usernamePlatform = "N/A";
				}
				modioPlatformUser.text = BuildText(colorName, userProfile.usernamePlatform);
			}
			if (modioSubCount.displayComponent != null)
			{
				string display = "NULL";
				if (LocalUser.SubscribedModIds != null)
				{
					display = LocalUser.SubscribedModIds.Count.ToString();
				}
				modioSubCount.text = BuildText(colorName, display);
			}
			if (xblUserId.displayComponent != null)
			{
				xblUserId.text = menuComponent.XBLId.ToString();
			}
			if (xblUserOnlineId.displayComponent != null)
			{
				xblUserOnlineId.text = menuComponent.XBLOnlineId.ToString();
			}
			if (xblUserToken.displayComponent != null)
			{
				string text2 = menuComponent.XBLToken;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = "NULL";
				}
				xblUserToken.text = text2;
			}
		}

		private static string BuildText(string colorName, string display)
		{
			return "<color=" + colorName + ">" + display + "</color>";
		}
	}
}
