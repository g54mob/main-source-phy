using BitCode.Users;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TFBGames
{
	public class PlayerProfileUI : MonoBehaviour
	{
		[SerializeField]
		protected LocalizeText nameLabel;

		[FormerlySerializedAs("presenceLabel")]
		[SerializeField]
		private TMP_Text statusLabel;

		[SerializeField]
		private Image iconImage;

		private PlayerProfile playerProfile;

		protected PlayerProfile Profile => playerProfile;

		public object CustomData => playerProfile?.CustomData;

		public void SetPlayerStatus(string statusString)
		{
			if (playerProfile != null)
			{
				playerProfile.SetStatus(statusString);
			}
			if (statusLabel != null)
			{
				statusLabel.text = statusString;
			}
		}

		public void SetPlayerName(string playerName)
		{
			if (nameLabel != null)
			{
				bool localized = playerName == "MP_LABEL_PLAYER_ONE" || playerName == "MP_LABEL_PLAYER_TWO";
				nameLabel.Localized = localized;
				nameLabel.LocaleID = playerName;
			}
		}

		public void SetPlayerSprite(Sprite sprite)
		{
			if (iconImage != null && sprite != null)
			{
				iconImage.sprite = sprite;
			}
		}

		public virtual void SetPlayerProfile(PlayerProfile profile)
		{
			ClearProfile();
			if (profile == null)
			{
				Debug.LogError("Could not set player profile. Passed in PlayerProfile was null.");
				return;
			}
			playerProfile = profile;
			SetPlayerName(profile.PlayerName);
			SetPlayerSprite(profile.PlayerIcon);
			if (playerProfile != null)
			{
				playerProfile.PlayerNameUpdated += OnPlayerNameUpdated;
				playerProfile.PlayerSpriteUpdated += OnPlayerSpriteUpdated;
				playerProfile.PlayerPresenceUpdated += OnPlayerPresenceUpdated;
				playerProfile.PlayerOnlineStatusChanged += OnPlayerOnlineStatusChanged;
			}
		}

		protected virtual void ClearProfile()
		{
			if (playerProfile != null)
			{
				playerProfile.PlayerNameUpdated -= OnPlayerNameUpdated;
				playerProfile.PlayerSpriteUpdated -= OnPlayerSpriteUpdated;
				playerProfile.PlayerPresenceUpdated -= OnPlayerPresenceUpdated;
				playerProfile.PlayerOnlineStatusChanged -= OnPlayerOnlineStatusChanged;
			}
			playerProfile = null;
			if (nameLabel != null)
			{
				nameLabel.LocaleID = string.Empty;
			}
			if (statusLabel != null)
			{
				statusLabel.text = string.Empty;
			}
		}

		protected virtual void OnPlayerNameUpdated(string playerName)
		{
			SetPlayerName(playerName);
		}

		protected virtual void OnPlayerSpriteUpdated(Sprite playerSprite)
		{
			SetPlayerSprite(playerSprite);
		}

		protected virtual void OnPlayerPresenceUpdated(string presenceString)
		{
		}

		protected virtual void OnPlayerOnlineStatusChanged(UserAccountOnlineStatus playerOnlineStatus)
		{
		}

		protected void UpdateStatusLabel(string text)
		{
			if (statusLabel != null)
			{
				statusLabel.text = text;
			}
		}

		private void OnDestroy()
		{
			ClearProfile();
		}
	}
}
