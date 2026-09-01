using BitCode.Users;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class ProfileWidget : MonoBehaviour
	{
		public Image Avatar;

		public TMP_Text text;

		public Button actionButton;

		public ProfilesManager Manager;

		private IUserAccount user;

		public IUserAccount User
		{
			get
			{
				return user;
			}
			set
			{
				user = value;
				actionButton.gameObject.SetActive(user is IRemoteAccount);
			}
		}

		public void OnActionClicked()
		{
			Manager?.OnActionClick(this);
		}
	}
}
