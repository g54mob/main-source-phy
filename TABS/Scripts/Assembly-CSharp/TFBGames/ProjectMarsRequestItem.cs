using System;
using BitCode.Users;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class ProjectMarsRequestItem : PlayerProfileUI, ISelectHandler, IEventSystemHandler, IDeselectHandler
	{
		[SerializeField]
		private Button inviteButton;

		[SerializeField]
		private Button profileButton;

		[SerializeField]
		private GameObject glyphs;

		[SerializeField]
		private GameObject profileGlyph;

		[SerializeField]
		private LocalizeText onlineStatusLabel;

		private bool UsingController => PlayerActions.Instance.InputType == InputType.Controller;

		private bool Selected => glyphs.activeSelf;

		public event Action<PlayerProfile> InviteClicked;

		public event Action<PlayerProfile> ProfileClicked;

		private void Awake()
		{
			if (nameLabel != null)
			{
				nameLabel.Localized = false;
			}
			inviteButton.onClick.AddListener(OnInviteClick);
			profileButton.onClick.AddListener(OnProfileClicked);
		}

		private void OnInviteClick()
		{
			this.InviteClicked?.Invoke(base.Profile);
		}

		private void OnProfileClicked()
		{
			this.ProfileClicked?.Invoke(base.Profile);
		}

		public void OnSelect(BaseEventData eventData)
		{
			if (UsingController)
			{
				glyphs.SetActive(value: true);
			}
		}

		public void OnDeselect(BaseEventData eventData)
		{
			if (UsingController)
			{
				glyphs.SetActive(value: false);
			}
		}

		public void ShowProfileButton()
		{
			if (UsingController)
			{
				profileGlyph.SetActive(value: true);
			}
			else
			{
				profileButton.gameObject.SetActive(value: true);
			}
		}

		private void Update()
		{
			if (UsingController && Selected)
			{
				if (PlayerActions.Instance.m_sendInvite.WasPressed)
				{
					OnInviteClick();
					glyphs.SetActive(value: false);
				}
				else if (PlayerActions.Instance.m_showProfile.WasPressed)
				{
					OnProfileClicked();
				}
			}
		}

		protected override void OnPlayerOnlineStatusChanged(UserAccountOnlineStatus playerOnlineStatus)
		{
			base.OnPlayerOnlineStatusChanged(playerOnlineStatus);
			if (!(onlineStatusLabel == null))
			{
				switch (playerOnlineStatus)
				{
				case UserAccountOnlineStatus.Online:
					onlineStatusLabel.LocaleID = "Online";
					inviteButton.interactable = true;
					break;
				case UserAccountOnlineStatus.Busy:
					onlineStatusLabel.LocaleID = "Busy";
					break;
				case UserAccountOnlineStatus.Away:
					onlineStatusLabel.LocaleID = "Away";
					break;
				case UserAccountOnlineStatus.Invisible:
				case UserAccountOnlineStatus.Offline:
					onlineStatusLabel.LocaleID = "Offline";
					inviteButton.interactable = false;
					break;
				default:
					onlineStatusLabel.LocaleID = string.Empty;
					throw new ArgumentOutOfRangeException("playerOnlineStatus", playerOnlineStatus, null);
				}
			}
		}
	}
}
