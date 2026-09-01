using System;
using System.Collections.Generic;
using BitCode.Platform.Steamworks;
using BitCode.Platform.Steamworks.Networking;
using BitCode.Users;
using UnityEngine;

namespace TFBGames
{
	public class ProfilesManager : MonoBehaviour
	{
		public GameObject ProfilePrefab;

		public int ProfileOffset = 200;

		public bool OnlyOnlineFriends = true;

		private List<IUserAccount> displayedUsers = new List<IUserAccount>();

		private List<ProfileWidget> profileWidgets = new List<ProfileWidget>();

		private IPlatformNetworkManagerService platformNetworkManagerService;

		private IGameInvitationService gameInvitationService;

		private AccountManager accountManager;

		private INetworkService networkService;

		private ILocalAccount localAccount;

		private void Start()
		{
			platformNetworkManagerService = ServiceLocator.GetService<IPlatformNetworkManagerService>();
			gameInvitationService = ServiceLocator.GetService<IGameInvitationService>();
			accountManager = ServiceLocator.GetService<AccountManager>();
			networkService = ServiceLocator.GetService<INetworkService>();
			localAccount = ServiceLocator.GetService<AccountManager>().LocalAccountManager.GetPrimaryAccount();
			if (accountManager.LocalAccountManager != null)
			{
				ILocalAccount primaryAccount = accountManager.LocalAccountManager.GetPrimaryAccount();
				ServiceLocator.GetService<IFriendService>().GetFriendListAsync(primaryAccount, GetFriendsCallback);
			}
		}

		private void OnDestroy()
		{
			for (int i = 0; i < displayedUsers.Count; i++)
			{
				if (!(displayedUsers[i] is ILocalAccount))
				{
					if (displayedUsers[i] is IDisposable disposable)
					{
						disposable.Dispose();
					}
					displayedUsers.Clear();
				}
			}
		}

		public async void OnActionClick(ProfileWidget profile)
		{
			Debug.Log("Invite Clicked for - " + profile.User.Name.Value);
			if (!string.IsNullOrEmpty(networkService.CurrentSessionId))
			{
				ILocalAccount primaryAccount = accountManager.LocalAccountManager.GetPrimaryAccount();
				IRemoteAccount[] invitees = new IRemoteAccount[1] { profile.User as IRemoteAccount };
				SteamGameInvitation invitation = gameInvitationService.CreateInviteToMultiplayerSession(platformNetworkManagerService.ActiveSession) as SteamGameInvitation;
				await gameInvitationService.SendGameInviteAsync(primaryAccount, invitees, invitation);
			}
		}

		protected void GetFriendsCallback(IRemoteAccount[] friends, Exception e)
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}
			List<IUserAccount> list = new List<IUserAccount>();
			list.Add(localAccount);
			list.AddRange(friends);
			SetTrackedDetails(list);
			displayedUsers.Clear();
			foreach (IUserAccount item in list)
			{
				if (item is SteamLocalAccount)
				{
					displayedUsers.Add(item);
				}
				else if ((!OnlyOnlineFriends || (item.OnlineStatus.Status == UserAccountPropertyStatus.Loaded && item.OnlineStatus.Value == UserAccountOnlineStatus.Online)) && item is SteamRemoteAccount)
				{
					displayedUsers.Add(item);
				}
			}
			CreateProfileWidgets(displayedUsers.Count);
			for (int i = 0; i < displayedUsers.Count; i++)
			{
				IUserAccount userAccount = displayedUsers[i];
				ProfileWidget profileWidget = profileWidgets[i];
				if (userAccount.Name.Status == UserAccountPropertyStatus.Loaded)
				{
					_ = userAccount.Name.Value;
				}
				string text = ((userAccount.OnlineStatus.Status == UserAccountPropertyStatus.Loaded) ? userAccount.OnlineStatus.Value.ToString() : "(???)");
				profileWidget.User = userAccount;
				profileWidget.text.text = userAccount.Name.Value + " (" + text + ")";
				if (userAccount.AvatarImage.Status == UserAccountPropertyStatus.Loaded)
				{
					SteamPlatformUtils steamPlatformUtils = ServiceLocator.GetService<IPlatformUtils>() as SteamPlatformUtils;
					profileWidget.Avatar.sprite = steamPlatformUtils.CreateSpriteFromImageData(userAccount.AvatarImage.Value);
				}
			}
		}

		private void CreateProfileWidgets(int count)
		{
			Debug.Log("Count = " + count);
			base.gameObject.transform.DestroyAllChildren();
			for (int i = 0; i < count; i++)
			{
				ProfileWidget component = UnityEngine.Object.Instantiate(ProfilePrefab).GetComponent<ProfileWidget>();
				component.Manager = this;
				component.transform.SetParent(base.transform);
				component.transform.localPosition = new Vector3(i * ProfileOffset, 0f, 0f);
				profileWidgets.Add(component);
			}
		}

		private void SetTrackedDetails(List<IUserAccount> users)
		{
			foreach (IUserAccount user in users)
			{
				SetTrackedUserDetails(user);
			}
		}

		private void SetTrackedUserDetails(IUserAccount userAcc)
		{
			userAcc.Name.SetTracked(track: true);
			userAcc.OnlineStatus.SetTracked(track: true);
			userAcc.AvatarImage.SetTracked(track: true);
			userAcc.AvatarImage.ValueChanged += OnUserAvatarImageUpdated;
		}

		private void OnUserAvatarImageUpdated(IUserAccount source)
		{
			if (source.Name.Status == UserAccountPropertyStatus.Loaded)
			{
				_ = source.Name.Value;
			}
			_ = source.AvatarImage.Value;
			int num = displayedUsers.IndexOf(source);
			if (num != -1 && source.AvatarImage.Status == UserAccountPropertyStatus.Loaded && ServiceLocator.GetService<IPlatformUtils>() is SteamPlatformUtils steamPlatformUtils)
			{
				profileWidgets[num].Avatar.sprite = steamPlatformUtils.CreateSpriteFromImageData(source.AvatarImage.Value);
			}
		}
	}
}
