using System;
using System.Collections.Generic;
using DM;
using GamepadUI.StateManager.Core;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TFBGames
{
	public class ProjectMarsIncomingRequestsScreen : UISubMenu
	{
		[SerializeField]
		private PlayerProfileConfiguration testProfiles;

		[SerializeField]
		private GameObject pleaseWaitObject;

		[SerializeField]
		private ProjectMarsRequestItem itemPrefab;

		[SerializeField]
		private Transform itemParent;

		[SerializeField]
		private bool onlyOnlineFriends;

		private List<ProjectMarsRequestItem> currentDisplayItems = new List<ProjectMarsRequestItem>();

		private List<PlayerProfile> profiles = new List<PlayerProfile>();

		private RequestScreenMode currentScreenMode;

		private INetworkService networkService;

		private SocialProfileService socialProfileService;

		private ProjectMarsHandlerJoinedSessionCallback lobbyJoinedSessionCallback;

		private ProjectMarsHandlerHostCallback lobbyHostCallback;

		private ProjectMarsHandlerShowErrorCallback showErrorCallback;

		private ProjectMarsHandlerSetHeadingCallback setHeadingCallback;

		private NetworkSessionFilter sessionFilter;

		public event Action<PlayerProfile> InvitePlayerClicked;

		protected override void Awake()
		{
			base.Awake();
			networkService = ServiceLocator.GetService<INetworkService>();
			socialProfileService = ServiceLocator.GetService<SocialProfileService>();
		}

		public override void Open()
		{
			base.Open();
			SetHeadingText(currentScreenMode, setHeadingCallback);
			if (socialProfileService != null)
			{
				socialProfileService.ProfilesRetrieved += DisplayUsers;
			}
		}

		public override void Close()
		{
			base.Close();
			if (socialProfileService != null)
			{
				socialProfileService.ProfilesRetrieved -= DisplayUsers;
			}
			ShowPleaseWait(visible: false);
			ShowItems(visible: true);
		}

		public void DisplayUsers(List<PlayerProfile> items)
		{
			ShowPleaseWait(visible: false);
			ShowItems(visible: true);
			PopulateList(items);
		}

		public void SetMode(RequestScreenMode screenMode, ProjectMarsHandlerSetHeadingCallback setHeadingCallback, ProjectMarsHandlerShowErrorCallback showErrorCallback = null, ProjectMarsHandlerJoinedSessionCallback joinedSessionCallback = null, ProjectMarsHandlerHostCallback hostCallback = null, NetworkSessionFilter sessionFilter = null)
		{
			ShowPleaseWait(visible: true);
			ShowItems(visible: false);
			SetEnableWithMenusParameter((int)screenMode);
			lobbyJoinedSessionCallback = joinedSessionCallback;
			lobbyHostCallback = hostCallback;
			this.showErrorCallback = showErrorCallback;
			this.setHeadingCallback = setHeadingCallback;
			this.sessionFilter = sessionFilter;
			currentScreenMode = screenMode;
			switch (currentScreenMode)
			{
			case RequestScreenMode.SendInvitation:
				if (socialProfileService != null)
				{
					ShowPleaseWait(visible: true);
					socialProfileService.GetFriends();
				}
				break;
			case RequestScreenMode.PublicLobby:
				if (networkService.IsRunning && !networkService.IsClient)
				{
					networkService.ShutdownAsync(OnShutDownToGetSessions);
				}
				else
				{
					networkService.GetSessionsAsync(OnGetSessions);
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("screenMode", screenMode, null);
			}
			SetHeadingText(currentScreenMode, setHeadingCallback);
		}

		private void PopulateList(IEnumerable<PlayerProfile> items)
		{
			try
			{
				ClearUserList();
				bool flag = ServiceLocator.GetService<SocialProfileService>().CanShowFriedProfile();
				foreach (PlayerProfile item in items)
				{
					ProjectMarsRequestItem projectMarsRequestItem = UnityEngine.Object.Instantiate(itemPrefab, itemParent);
					projectMarsRequestItem.SetPlayerProfile(item);
					projectMarsRequestItem.InviteClicked += OnClickedItem;
					if (flag)
					{
						projectMarsRequestItem.ShowProfileButton();
						projectMarsRequestItem.ProfileClicked += OnProfileClicked;
					}
					currentDisplayItems.Add(projectMarsRequestItem);
				}
				UIHelpers.CreateExplicitLinearNavigation(itemParent.GetSelectableChildren(), horizontal: false);
				if (currentDisplayItems != null && currentDisplayItems.Count > 0)
				{
					EventSystem.current.SetSelectedGameObject(currentDisplayItems[0].gameObject);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
			}
		}

		public void OnHost()
		{
			if (base.IsOpen)
			{
				ShowPleaseWait(visible: false);
				networkService.ShutdownAsync(null);
				lobbyHostCallback?.Invoke();
			}
		}

		protected override void UpdateGamepads()
		{
			base.UpdateGamepads();
			if (playerActions.m_applySettings.WasPressed)
			{
				OnHost();
			}
		}

		private void SetHeadingText(RequestScreenMode mode, ProjectMarsHandlerSetHeadingCallback setHeadingCallback)
		{
			if (mode == RequestScreenMode.PublicLobby)
			{
				setHeadingCallback?.Invoke("LOBBY");
			}
		}

		private void ClearUserList()
		{
			foreach (ProjectMarsRequestItem currentDisplayItem in currentDisplayItems)
			{
				currentDisplayItem.InviteClicked -= OnClickedItem;
				currentDisplayItem.ProfileClicked -= OnProfileClicked;
				UnityEngine.Object.Destroy(currentDisplayItem.gameObject);
			}
			currentDisplayItems.Clear();
		}

		private void ShowPleaseWait(bool visible)
		{
			if (pleaseWaitObject != null)
			{
				pleaseWaitObject.SetActive(visible);
			}
		}

		private void ShowItems(bool visible)
		{
			if (itemParent != null)
			{
				itemParent.gameObject.SetActive(visible);
			}
		}

		private void OnClickedItem(PlayerProfile item)
		{
			ShowPleaseWait(visible: true);
			ShowItems(visible: false);
			switch (currentScreenMode)
			{
			case RequestScreenMode.SendInvitation:
				this.InvitePlayerClicked?.Invoke(item);
				break;
			case RequestScreenMode.PublicLobby:
				if (item != null && item.CustomData != null)
				{
					NetworkSession networkSession = (NetworkSession)item.CustomData;
					networkService.JoinSessionAsync(isQuickGame: true, new JoinSessionProperties(networkSession.Id, null), OnJoinSession);
				}
				else
				{
					OnJoinOrGetSessionsError("MP_POPUP_ERROR_TRY_LATER");
				}
				break;
			default:
				Debug.LogError($"Unsupported mode: {currentScreenMode}");
				break;
			}
		}

		private void OnProfileClicked(PlayerProfile item)
		{
			socialProfileService.ShowFriendProfile(item.UserAccount);
		}

		private PlayerProfile SessionToProfile(NetworkSession session)
		{
			Sprite playerIcon = null;
			MapAsset mapAssetByTypeAndMapIndex = ContentDatabase.Instance().GetMapAssetByTypeAndMapIndex(session.Metadata.RoomMapType, session.Metadata.RoomMapIndex);
			string statusString = ((mapAssetByTypeAndMapIndex != null && mapAssetByTypeAndMapIndex.Entity != null) ? (mapAssetByTypeAndMapIndex.Entity.Name ?? "") : string.Empty) ?? "";
			return new PlayerProfile(session.Metadata.HostPlayerDisplayName, playerIcon, statusString, networkService.RemotePlayerTeam, session);
		}

		private void OnShutDownToGetSessions(NetworkException exception)
		{
			if (exception != null)
			{
				if (exception.ErrorCode == NetworkErrorCode.UserCancelled)
				{
					ShowPleaseWait(visible: false);
				}
				else
				{
					OnJoinOrGetSessionsError("MP_POPUP_ERROR_TRY_LATER");
				}
			}
			else
			{
				networkService.GetSessionsAsync(OnGetSessions);
			}
		}

		private void OnJoinSession(NetworkSession session, NetworkException exception)
		{
			if (exception != null)
			{
				if (exception.ErrorCode == NetworkErrorCode.UserCancelled)
				{
					ShowPleaseWait(visible: false);
				}
				else
				{
					OnJoinOrGetSessionsError("MP_POPUP_ERROR_TRY_LATER");
				}
			}
			else
			{
				lobbyJoinedSessionCallback?.Invoke(session);
			}
		}

		private void OnGetSessions(NetworkSession[] sessions, NetworkException exception)
		{
			if (exception != null)
			{
				switch (exception.ErrorCode)
				{
				case NetworkErrorCode.UserCancelled:
					ShowPleaseWait(visible: false);
					break;
				case NetworkErrorCode.Timeout:
					OnJoinOrGetSessionsError("MP_POPUP_ERROR_SEARCH_TIMEOUT");
					break;
				default:
					OnJoinOrGetSessionsError("MP_POPUP_ERROR_TRY_LATER");
					break;
				}
				return;
			}
			if (sessions == null || sessions.Length == 0)
			{
				OnJoinOrGetSessionsError("MP_POPUP_ERROR_NO_SESSIONS");
				return;
			}
			int num = sessions.Length;
			profiles.Clear();
			for (int i = 0; i < num; i++)
			{
				NetworkSession session = sessions[i];
				if (NetworkSessionHelper.CanJoinSession(session, sessionFilter, canJoinIfHidden: false))
				{
					profiles.Add(SessionToProfile(session));
				}
			}
			if (profiles.Count <= 0)
			{
				OnJoinOrGetSessionsError("MP_POPUP_ERROR_NO_SESSIONS");
			}
			else
			{
				DisplayUsers(profiles);
			}
		}

		private void OnJoinOrGetSessionsError(string errorMessage)
		{
			networkService.ShutdownAsync(null);
			ShowPleaseWait(visible: false);
			showErrorCallback?.Invoke(errorMessage, closeSubMenu: false);
		}
	}
}
