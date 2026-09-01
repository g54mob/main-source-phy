using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class AccountOptionsMenu : MonoBehaviour
	{
		[Header("UI Elements")]
		public RectTransform dropdown;

		public UserView loggedUser;

		public Button viewProfileButton;

		public Button logoutButton;

		public Button loginButton;

		private void Start()
		{
			logoutButton.onClick.AddListener(HideMenu);
			loginButton.onClick.AddListener(HideMenu);
		}

		private void OnEnable()
		{
			dropdown.gameObject.SetActive(value: false);
		}

		public void ShowMenu()
		{
			bool flag = LocalUser.AuthenticationState == AuthenticationState.ValidToken;
			loggedUser.gameObject.SetActive(flag);
			logoutButton.gameObject.SetActive(flag);
			loginButton.gameObject.SetActive(!flag);
			viewProfileButton.gameObject.SetActive(flag);
			dropdown.gameObject.SetActive(value: true);
		}

		public void HideMenu()
		{
			dropdown.gameObject.SetActive(value: false);
		}

		public void ToggleMenu()
		{
			if (!dropdown.gameObject.activeSelf)
			{
				ShowMenu();
			}
			else
			{
				HideMenu();
			}
		}

		public void OpenProfileInBrowser()
		{
			UserProfile profile = LocalUser.Profile;
			if (profile != null)
			{
				viewProfileButton.interactable = false;
				string text = string.Empty;
				switch (LocalUser.ExternalAuthentication.portal)
				{
				case UserPortal.Steam:
					text = "?ref=steam";
					break;
				case UserPortal.GOG:
					text = "?ref=gog";
					break;
				}
				Application.OpenURL(profile.profileURL + "/edit" + text);
				viewProfileButton.interactable = true;
			}
		}
	}
}
