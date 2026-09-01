using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(Button))]
	public class AccountReactiveButton : MonoBehaviour
	{
		public Button.ButtonClickedEvent onLoggedOutClick;

		public Button.ButtonClickedEvent onModioAccountClick;

		public Button.ButtonClickedEvent onExternalAccountClick;

		private Button m_button;

		private void Start()
		{
			m_button = base.gameObject.GetComponent<Button>();
			m_button.onClick.AddListener(OnButtonClick);
		}

		private void OnButtonClick()
		{
			if (LocalUser.AuthenticationState != AuthenticationState.ValidToken)
			{
				if (onLoggedOutClick != null)
				{
					onLoggedOutClick.Invoke();
				}
			}
			else if (LocalUser.ExternalAuthentication.portal == UserPortal.None)
			{
				if (onModioAccountClick != null)
				{
					onModioAccountClick.Invoke();
				}
			}
			else if (onExternalAccountClick != null)
			{
				onExternalAccountClick.Invoke();
			}
		}
	}
}
