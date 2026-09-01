using System;
using BitCode.Users;
using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	[Serializable]
	public class PlayerProfile
	{
		[SerializeField]
		private string playerName;

		[SerializeField]
		private Sprite playerIcon;

		private string presenceString;

		private UserAccountOnlineStatus onlineStatus;

		private Team playerTeam;

		private string statusString;

		public string PlayerName => playerName;

		public Sprite PlayerIcon => playerIcon;

		public string PresenceString => presenceString;

		public UserAccountOnlineStatus AccountOnlineStatus => onlineStatus;

		public Team PlayerTeam => playerTeam;

		public string StatusString => statusString;

		public object CustomData { get; }

		public IUserAccount UserAccount { get; set; }

		public event Action<string> StatusUpdated;

		public event Action NameUpdated;

		public event Action AvatarImageUpdated;

		public event Action<Sprite> PlayerSpriteUpdated;

		public event Action<string> PlayerNameUpdated;

		public event Action<string> PlayerPresenceUpdated;

		public event Action<UserAccountOnlineStatus> PlayerOnlineStatusChanged;

		public PlayerProfile(string playerName, Sprite playerIcon, string statusString, Team playerTeam, object customData)
		{
			this.playerName = playerName;
			this.playerIcon = playerIcon;
			this.statusString = statusString;
			this.playerTeam = playerTeam;
			CustomData = customData;
		}

		public PlayerProfile(IUserAccount userAccount, object customData)
		{
			UserAccount = userAccount;
			if (UserAccount != null)
			{
				UserAccount.AvatarImage.ValueChanged += OnAvatarImageChanged;
				UserAccount.Presence.ValueChanged += OnPlayerPresenceChanged;
				UserAccount.OnlineStatus.ValueChanged += OnPlayerOnlineStatusChanged;
				UserAccount.Name.ValueChanged += OnPlayerNameChanged;
				SetInformationFromUserAccount(UserAccount);
			}
			CustomData = customData;
		}

		private void SetInformationFromUserAccount(IUserAccount userAccount)
		{
			if (userAccount.Name.Status == UserAccountPropertyStatus.Loaded)
			{
				playerName = userAccount.Name.Value;
			}
			if (userAccount.AvatarImage.Status == UserAccountPropertyStatus.Loaded)
			{
				playerIcon = ProjectMarsHelpers.GetSpriteFromUser(userAccount);
			}
			if (userAccount.Presence.Status == UserAccountPropertyStatus.Loaded)
			{
				presenceString = userAccount.Presence.Value;
			}
			if (userAccount.OnlineStatus.Status == UserAccountPropertyStatus.Loaded)
			{
				onlineStatus = userAccount.OnlineStatus.Value;
			}
		}

		private void OnPlayerNameChanged(IUserAccount user)
		{
			if (user == null || user.Name.Status != UserAccountPropertyStatus.Loaded)
			{
				Debug.Log("<color=blue>Name not loaded yet.</color>");
				return;
			}
			playerName = user.Name.Value;
			SetPlayerName(playerName);
		}

		private void OnAvatarImageChanged(IUserAccount user)
		{
			if (user != null && user.AvatarImage.Status == UserAccountPropertyStatus.Loaded)
			{
				Sprite spriteFromUser = ProjectMarsHelpers.GetSpriteFromUser(user);
				if (spriteFromUser != null)
				{
					playerIcon = spriteFromUser;
					this.PlayerSpriteUpdated?.Invoke(playerIcon);
				}
			}
		}

		private void OnPlayerPresenceChanged(IUserAccount user)
		{
			if (user != null && user.Presence.Status == UserAccountPropertyStatus.Loaded)
			{
				presenceString = user.Presence.Value;
				this.PlayerPresenceUpdated?.Invoke(presenceString);
			}
		}

		private void OnPlayerOnlineStatusChanged(IUserAccount user)
		{
			if (user != null && user.OnlineStatus.Status == UserAccountPropertyStatus.Loaded)
			{
				onlineStatus = user.OnlineStatus.Value;
				this.PlayerOnlineStatusChanged?.Invoke(onlineStatus);
			}
		}

		public void SetStatus(string status)
		{
			statusString = status;
			this.StatusUpdated?.Invoke(statusString);
		}

		public void SetPlayerName(string newName)
		{
			playerName = newName;
			this.PlayerNameUpdated?.Invoke(playerName);
		}

		~PlayerProfile()
		{
			if (UserAccount != null)
			{
				UserAccount.AvatarImage.ValueChanged -= OnAvatarImageChanged;
				UserAccount.Name.ValueChanged -= OnPlayerNameChanged;
				UserAccount.Presence.ValueChanged -= OnPlayerPresenceChanged;
				UserAccount.OnlineStatus.ValueChanged += OnPlayerOnlineStatusChanged;
			}
		}

		private void UpdateTracking()
		{
			UserAccount.Name.StartTracking(OnNameChanged);
			UserAccount.AvatarImage.StartTracking(OnAvatarChanged);
		}

		private void OnNameChanged(IUserAccount account)
		{
			playerName = account.Name.Value;
			this.NameUpdated?.Invoke();
		}

		private void OnAvatarChanged(IUserAccount account)
		{
			playerIcon = ProjectMarsHelpers.GetSpriteFromUser(account);
			this.AvatarImageUpdated?.Invoke();
		}
	}
}
