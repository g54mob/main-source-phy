using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitCode.Networking;
using BitCode.Users;
using DM;
using GamepadUI.StateManager.Core;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS_Input;
using Photon.Bolt;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class ProjectMarsHandler : UIComponentMainMenu
	{
		private const string LocalizedErrorHeading = "POPUP_ERROR";

		private const string NoInternetError = "NETWORK_ERROR_UNKNOWN";

		[SerializeField]
		private LocalizeText headingText;

		[Header("Sub menus")]
		[SerializeField]
		private UISubMenu localMultiplayerUIHandler;

		[SerializeField]
		private UISubMenu multiplayerSettingsMenu;

		[Header("Project Mars Screens")]
		[SerializeField]
		private ProjectMarsModeSelectScreen modeSelectScreen;

		[SerializeField]
		private UISubMenu friendsScreen;

		[SerializeField]
		private ProjectMarsIncomingRequestsScreen invitesScreen;

		[SerializeField]
		private ProjectMarsWaitingScreen projectMarsWaitingScreen;

		[SerializeField]
		private ProjectMarsErrorScreen errorScreen;

		[Header("Buttons")]
		[SerializeField]
		private LevelSelection levelSelection;

		[Header("UIStateManager")]
		[SerializeField]
		protected UIComponentMainMenu m_mainButtons;

		private PlayerActions playerActions;

		private IAccountPermissions accountPermissions;

		private GlobalSettingsHandler settingsHandler;

		private ModalPanel modalPanel;

		private SettingsProfileManager settingsProfileManager;

		private bool didGoBackWhileWaitingForLevelSelect;

		private bool didGoBackWhileWaitingForMultiplayerCheck;

		private bool didGoBackWhileGettingSessions;

		private bool didGoBackWhileJoiningInviteGame;

		private bool canPlayCrossNetworkSession;

		private ProjectMarsDelayAuthentication delayAuthentication;

		private int authenticationCount;

		private IGameInvitation inviteBeingProcessed;

		private PlatformSyncedNetworkService networkService;

		private INetworkUserAuthenticator userAuthenticator;

		private bool didDoUserAuthentication;

		private List<NetworkSession> allSessions = new List<NetworkSession>();

		private MainMenuJoinInviteController joinInviteController;

		public IUserAccount CurrentlyInvitedUser { get; set; }

		public bool CheckPermissionsOnOpen { get; set; } = true;

		public bool IsPublicSession { get; private set; } = true;

		public static ProjectMarsHandler Instance { get; private set; }

		public UISubMenu LocalMultiplayerUIHandler => localMultiplayerUIHandler;

		public UISubMenu MultiplayerSettingsMenu => multiplayerSettingsMenu;

		public event ProjectMarsHandlerDestroyedEventHandler Destroyed;

		public event ProjectMarsHandlerDoingUserAuthEventHandler DoingUserAuth;

		protected override void Awake()
		{
			base.Awake();
			Instance = this;
			playerActions = PlayerActions.Instance;
			accountPermissions = ServiceLocator.GetService<IAccountPermissions>();
			settingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
			modalPanel = ServiceLocator.GetService<ModalPanel>();
			settingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
			networkService = ServiceLocator.GetService<INetworkService>() as PlatformSyncedNetworkService;
			if (networkService == null)
			{
				throw new Exception("Network service isn't of type PlatformSyncedNetworkService");
			}
			userAuthenticator = ServiceLocator.GetService<INetworkUserAuthenticator>();
			joinInviteController = base.gameObject.AddComponent<MainMenuJoinInviteController>();
			GameObject gameObject = new GameObject("ProjectMarsDelayAuthentication");
			delayAuthentication = gameObject.AddComponent<ProjectMarsDelayAuthentication>();
			delayAuthentication.Initialize(DoUserAuthentication);
			RegisterProjectMarsServices();
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			didDoUserAuthentication = false;
			SubscribeToModalPanelCloseEvent(subscribe: false);
			OnOpenMultiplayer();
		}

		protected override void OnClose()
		{
			base.OnClose();
			CheckPermissionsOnOpen = true;
			delayAuthentication.Clear();
			SubscribeToModalPanelCloseEvent(subscribe: false);
		}

		protected void OnOpenMultiplayer()
		{
			if (!CheckPermissionsOnOpen)
			{
				OpenFirstSubMenu();
				return;
			}
			canPlayCrossNetworkSession = true;
			CloseSubMenuAndClearStack();
			if (!accountPermissions.IsSignedIn)
			{
				modalPanel.PopUp("POPUP_NOT_SIGNED_IN_MULTIPLAYER");
				stateManager.OpenUIComponent(m_mainButtons);
				base.Close();
				return;
			}
			if (inviteBeingProcessed == null)
			{
				OpenFirstSubMenu();
				return;
			}
			SetHeading("MP_LABEL_LOADING");
			delayAuthentication.ScheduleUserAuthentication(null, inviteBeingProcessed, delegate(string data, Exception exception, int lastAuthenticationCount)
			{
				if (ProcessUserAuthenticationData(data, exception, closeIfException: true, lastAuthenticationCount, closeProjectMarsWaiting: false))
				{
					OpenFirstSubMenu();
				}
			});
		}

		private bool ProcessUserAuthenticationData(string data, Exception exception, bool closeIfException, int lastAuthenticationCount, bool closeProjectMarsWaiting)
		{
			if (authenticationCount != lastAuthenticationCount)
			{
				return false;
			}
			if (exception is CancelUserAuthenticationException)
			{
				if (closeIfException)
				{
					GoBack();
					base.Close();
				}
				CloseProjectMarsWaiting(closeProjectMarsWaiting);
				return false;
			}
			if (exception != null)
			{
				Debug.LogError($"User authentication failed:\n{exception}");
				modalPanel.PopUp("MP_POPUP_USER_AUTHENTICATION_FAILED");
				if (closeIfException)
				{
					GoBack();
					base.Close();
				}
				CloseProjectMarsWaiting(closeProjectMarsWaiting);
				return false;
			}
			if (networkService != null)
			{
				networkService.SetUserAuthenticationData(data);
			}
			Debug.Log("TOKEN: " + data);
			didDoUserAuthentication = true;
			CloseProjectMarsWaiting(closeProjectMarsWaiting);
			return true;
		}

		private void CloseProjectMarsWaiting(bool closeProjectMarsWaiting)
		{
			if (closeProjectMarsWaiting)
			{
				CloseSubMenu(projectMarsWaitingScreen, removeFromStack: true);
				if (modalPanel.IsPopupOpen)
				{
					SubscribeToModalPanelCloseEvent(subscribe: true);
				}
				else
				{
					HandleShowModeSelectScreen();
				}
			}
		}

		private void DoUserAuthentication(string regionCode, DelayUserAuthenticationCallback callback)
		{
			this.DoingUserAuth?.Invoke();
			int lastAuthenticationCount = ++authenticationCount;
			userAuthenticator.AuthenticateUserAsync(regionCode, delegate(string data, Exception exception)
			{
				callback?.Invoke(data, exception, lastAuthenticationCount);
			});
		}

		private void SubscribeToModalPanelCloseEvent(bool subscribe)
		{
			if (!(modalPanel == null))
			{
				modalPanel.OnPopUpClose -= OnPopUpCloseOpenModeSelectScreen;
				if (subscribe)
				{
					modalPanel.OnPopUpClose += OnPopUpCloseOpenModeSelectScreen;
				}
			}
		}

		private void OnCanPlayInAMultiplayerSession(bool permitted, PlayerProfile playerToInvite, bool isJoiningSessionFromInvite)
		{
			if (!permitted)
			{
				inviteBeingProcessed = null;
				base.Close();
				stateManager.OpenUIComponent(m_mainButtons);
				return;
			}
			SettingsInstance settingsInstance = settingsHandler.GetSettingsInstance("ALLOW_CROSS_NETWORK");
			if (settingsInstance != null && settingsInstance.currentValue == 0)
			{
				canPlayCrossNetworkSession = false;
				if (isJoiningSessionFromInvite)
				{
					JoinSessionFromInviteAfterCheckingPermissions();
				}
				else
				{
					StartGame(playerToInvite);
				}
				return;
			}
			accountPermissions.CanPlayCrossNetworkSessionAsync(delegate(bool permittedCrossPlay)
			{
				canPlayCrossNetworkSession = permittedCrossPlay;
				if (isJoiningSessionFromInvite)
				{
					JoinSessionFromInviteAfterCheckingPermissions();
				}
				else
				{
					StartGame(playerToInvite);
				}
			});
		}

		private void StartGame(PlayerProfile playerToInvite)
		{
			if (playerToInvite == null)
			{
				StartQuickGame();
			}
			else
			{
				StartInviteGame(playerToInvite);
			}
		}

		protected override void Update()
		{
			base.Update();
			if (base.IsActive)
			{
				UpdateGamepads();
			}
		}

		public void GoBack()
		{
			didGoBackWhileWaitingForLevelSelect = true;
			authenticationCount++;
			didGoBackWhileWaitingForMultiplayerCheck = true;
			didGoBackWhileGettingSessions = true;
			didGoBackWhileJoiningInviteGame = true;
			inviteBeingProcessed = null;
			networkService.ShutdownAsync(null);
			if (OnBackPressed())
			{
				base.Close();
				stateManager.OpenUIComponent(m_mainButtons);
			}
		}

		private void OpenFirstSubMenu()
		{
			if (inviteBeingProcessed != null)
			{
				StartJoiningSessionFromInvite();
				return;
			}
			OpenSubMenu(modeSelectScreen);
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				modeSelectScreen.GetComponentInChildren<Selectable>().Select();
			}
		}

		private void UpdateGamepads()
		{
			if (playerActions.m_back.WasPressed)
			{
				GoBack();
			}
		}

		protected override void OnSubMenuPressedBackButton(UISubMenu menu)
		{
			base.OnSubMenuPressedBackButton(menu);
			GoBack();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.Destroyed?.Invoke();
			Instance = null;
			if (delayAuthentication != null)
			{
				delayAuthentication.Clear();
			}
			SubscribeToModalPanelCloseEvent(subscribe: false);
			DeregisterProjectMarsServices();
		}

		private void RegisterProjectMarsServices()
		{
			if (joinInviteController != null)
			{
				joinInviteController.ReceivedInvitation += OnReceivedMainMenuInvitation;
			}
			if (modeSelectScreen != null)
			{
				modeSelectScreen.SetQuickMarsButtonEvent(OnQuickMarsButtonClicked);
			}
			if (invitesScreen != null)
			{
				invitesScreen.InvitePlayerClicked += OnPlayerInviteClicked;
			}
		}

		private void DeregisterProjectMarsServices()
		{
			if (joinInviteController != null)
			{
				joinInviteController.ReceivedInvitation -= OnReceivedMainMenuInvitation;
			}
			if (invitesScreen != null)
			{
				invitesScreen.InvitePlayerClicked -= OnPlayerInviteClicked;
			}
		}

		private async void OnPlayerInviteClicked(PlayerProfile playerToInvite)
		{
			didGoBackWhileWaitingForMultiplayerCheck = false;
			if (!(await IsConnectedToInternet()))
			{
				ShowPopUpThatReturnsToPreviousPage();
				return;
			}
			accountPermissions.CanPlayInAMultiplayerSessionAsync(showPopup: true, "MP_POPUP_NOT_ALLOWED_TO_PLAY_IN_A_MULTIPLAYER_SESSION", delegate(bool permitted)
			{
				OnCanPlayInAMultiplayerSession(permitted, playerToInvite, isJoiningSessionFromInvite: false);
			});
		}

		private void ShowPopUpThatReturnsToPreviousPage()
		{
			ServiceLocator.GetService<ModalPanel>().PopUp("NETWORK_ERROR_UNKNOWN", delegate
			{
				GoBack();
			});
		}

		private void StartInviteGame(PlayerProfile playerToInvite)
		{
			SetPlayerToInvite(playerToInvite.UserAccount);
			SetSessionAccessibility(isPublicSession: false);
			ShowLevelSelect();
		}

		private void SetPlayerToInvite(IUserAccount user)
		{
			CurrentlyInvitedUser = user;
		}

		private void ShowError(string errorMessage, bool closeSubMenu)
		{
			if (closeSubMenu && currentSubMenu != null)
			{
				OnBackPressed();
			}
			OpenSubMenu(errorScreen);
			errorScreen.DisplayMessage(Localizer.GetSinglePhrase(errorMessage));
			SetHeading("POPUP_ERROR");
		}

		private void LoadMap(NetworkSession session)
		{
			ServiceLocator.GetService<GameModeService>().SetGameMode<OnlineMultiplayerGameMode>();
			MapAsset mapAssetByTypeAndMapIndex = ContentDatabase.Instance().GetMapAssetByTypeAndMapIndex(session.Metadata.RoomMapType, session.Metadata.RoomMapIndex);
			SyncClientScene();
			TABSSceneManager.LoadMap(mapAssetByTypeAndMapIndex);
		}

		private void SyncClientScene()
		{
			if (!BoltNetwork.IsServer)
			{
				BoltNetwork.LoadSceneSync();
			}
		}

		private NetworkSessionFilter CreateSessionFilter()
		{
			return new NetworkSessionFilter(NetworkSessionHelper.GetGameVersion(), canPlayCrossNetworkSession, settingsProfileManager.CurrentSettingsProfile.AllowedMultiplayerPlatforms);
		}

		private void OnQuickMarsButtonClicked()
		{
			if (!didDoUserAuthentication)
			{
				projectMarsWaitingScreen.SetMode(LandingScreenMode.AuthenticatingUser);
				OpenSubMenu(projectMarsWaitingScreen);
				delayAuthentication.ScheduleUserAuthentication(projectMarsWaitingScreen, null, delegate(string data, Exception exception, int lastAuthenticationCount)
				{
					if (ProcessUserAuthenticationData(data, exception, closeIfException: false, lastAuthenticationCount, closeProjectMarsWaiting: true))
					{
						HandleOpenQuickMars();
					}
				});
			}
			else
			{
				HandleOpenQuickMars();
			}
		}

		private async Task<bool> IsConnectedToInternet()
		{
			if (!(await ServiceLocator.GetService<PermissionsHelper>().IsOnline(showPopup: false)))
			{
				ServiceLocator.GetService<INetworkService>().ShutdownAsync(null);
				return false;
			}
			return true;
		}

		private void HandleOpenQuickMars()
		{
			projectMarsWaitingScreen.SetMode(LandingScreenMode.WaitingForFinding);
			OpenSubMenu(projectMarsWaitingScreen);
			didGoBackWhileWaitingForMultiplayerCheck = false;
			accountPermissions.CanPlayInAMultiplayerSessionAsync(showPopup: false, string.Empty, delegate(bool permitted)
			{
				CheckIfConnectedToInternetAfterPermission(permitted, null, isJoiningSessionFromInvite: false);
			});
		}

		private async void CheckIfConnectedToInternetAfterPermission(bool permitted, PlayerProfile playerToInvite, bool isJoiningSessionFromInvite)
		{
			if (!(await IsConnectedToInternet()))
			{
				ShowPopUpThatReturnsToPreviousPage();
			}
			else if (!permitted)
			{
				modalPanel.PopUp("MP_POPUP_NOT_ALLOWED_TO_PLAY_IN_A_MULTIPLAYER_SESSION", delegate
				{
				});
				GoBack();
			}
			else
			{
				OnCanPlayInAMultiplayerSession(permitted, playerToInvite, isJoiningSessionFromInvite);
			}
		}

		private void StartQuickGame()
		{
			if (!didGoBackWhileWaitingForMultiplayerCheck)
			{
				CampaignPlayerDataHolder.StartedPlayingOnlineMultiplayer();
				SetSessionAccessibility(isPublicSession: true);
				projectMarsWaitingScreen.SetMode(LandingScreenMode.Finding);
				OpenSubMenu(projectMarsWaitingScreen);
				didGoBackWhileGettingSessions = false;
				if (networkService.IsRunning)
				{
					networkService.ShutdownAsync(OnShutDownForQuickGameToGetSessions);
				}
				else
				{
					networkService.GetSessionsAsync(OnGotSessionsForQuickGame);
				}
			}
		}

		private void SetSessionAccessibility(bool isPublicSession)
		{
			IsPublicSession = isPublicSession;
		}

		private void OnShutDownForQuickGameToGetSessions(NetworkException exception)
		{
			if (!didGoBackWhileGettingSessions)
			{
				if (exception != null)
				{
					OnHandleQuickGameError(exception);
				}
				else
				{
					networkService.GetSessionsAsync(OnGotSessionsForQuickGame);
				}
			}
		}

		private void OnGotSessionsForQuickGame(NetworkSession[] sessions, NetworkException exception)
		{
			if (didGoBackWhileGettingSessions)
			{
				return;
			}
			if (exception != null || sessions == null || sessions.Length == 0)
			{
				OnHandleQuickGameError(exception);
				return;
			}
			allSessions.Clear();
			allSessions.AddRange(sessions);
			allSessions.Shuffle();
			NetworkSessionFilter filter = CreateSessionFilter();
			NetworkSession networkSession = null;
			int i = 0;
			for (int count = allSessions.Count; i < count; i++)
			{
				NetworkSession networkSession2 = allSessions[i];
				if (NetworkSessionHelper.CanJoinSession(networkSession2, filter, canJoinIfHidden: false))
				{
					networkSession = networkSession2;
					break;
				}
			}
			if (networkSession == null)
			{
				OnHandleQuickGameError(null);
			}
			else
			{
				networkService.JoinSessionAsync(isQuickGame: true, new JoinSessionProperties(networkSession.Id, null), OnJoinedSessionForQuickMatch);
			}
		}

		private static string SessionToString(NetworkSession session)
		{
			if (session == null)
			{
				return string.Empty;
			}
			return $"isOpen: {session.IsOpen}     isVisible: {session.IsVisible}     " + $"mapType: {session.Metadata.RoomMapType}     mapIndex: {session.Metadata.RoomMapIndex}     " + $"platform: {session.Metadata.HostPlatform}     version: {session.Metadata.GameVersionNumber}     " + "playerName: " + session.Metadata.HostPlayerDisplayName + "     sessionId: " + session.Id;
		}

		private void OnJoinedSessionForQuickMatch(NetworkSession session, NetworkException exception)
		{
			if (!didGoBackWhileGettingSessions)
			{
				if (exception != null)
				{
					OnHandleQuickGameError(exception);
					return;
				}
				projectMarsWaitingScreen.SetMode(LandingScreenMode.Joining);
				OpenSubMenu(projectMarsWaitingScreen);
				projectMarsWaitingScreen.ShowSessionInfo(session, LoadMap, OnHandleQuickGameError);
			}
		}

		private void OnHandleQuickGameError(NetworkException exception)
		{
			if (exception != null)
			{
				switch (exception.ErrorCode)
				{
				case NetworkErrorCode.UserCancelled:
					return;
				case NetworkErrorCode.ServiceIsBusyWithAsync:
					Debug.LogError(exception.Message);
					ShowError(GetErrorMessage(exception.ErrorCode), closeSubMenu: true);
					return;
				}
			}
			didGoBackWhileWaitingForLevelSelect = false;
			networkService.ShutdownAsync(OnShutDownForQuickGameAndShowLevelSelect);
		}

		private void OnShutDownForQuickGameAndShowLevelSelect(NetworkException exception)
		{
			if (didGoBackWhileWaitingForLevelSelect)
			{
				return;
			}
			if (exception != null)
			{
				if (exception.ErrorCode != NetworkErrorCode.UserCancelled)
				{
					ShowError(GetErrorMessage(exception.ErrorCode), closeSubMenu: true);
					Debug.LogError(exception.Message);
				}
			}
			else
			{
				ShowLevelSelect();
			}
		}

		protected override void OnReceivedInvitation(IGameInvitation invite)
		{
		}

		private void OnReceivedMainMenuInvitation(MainMenuJoinInviteController controller, IGameInvitation invite)
		{
			if (invite == null)
			{
				return;
			}
			inviteBeingProcessed = invite;
			if (!base.IsActive)
			{
				MainMenuButtons.Instance.OpenMultiplayerMenu();
			}
			else if (!didDoUserAuthentication)
			{
				projectMarsWaitingScreen.SetMode(LandingScreenMode.AuthenticatingUser);
				OpenSubMenu(projectMarsWaitingScreen);
				delayAuthentication.ScheduleUserAuthentication(projectMarsWaitingScreen, inviteBeingProcessed, delegate(string data, Exception exception, int lastAuthenticationCount)
				{
					if (ProcessUserAuthenticationData(data, exception, closeIfException: true, lastAuthenticationCount, closeProjectMarsWaiting: true))
					{
						StartJoiningSessionFromInvite();
					}
				});
			}
			else
			{
				StartJoiningSessionFromInvite();
			}
		}

		private async void StartJoiningSessionFromInvite()
		{
			if (!(await IsConnectedToInternet()))
			{
				ShowPopUpThatReturnsToPreviousPage();
				return;
			}
			didGoBackWhileWaitingForMultiplayerCheck = false;
			accountPermissions.CanPlayInAMultiplayerSessionAsync(showPopup: true, "MP_POPUP_NOT_ALLOWED_TO_PLAY_IN_A_MULTIPLAYER_SESSION", delegate(bool permitted)
			{
				OnCanPlayInAMultiplayerSession(permitted, null, isJoiningSessionFromInvite: true);
			});
		}

		private void JoinSessionFromInviteAfterCheckingPermissions()
		{
			CampaignPlayerDataHolder.StartedPlayingOnlineMultiplayer();
			CloseSubMenuAndClearStack();
			projectMarsWaitingScreen.SetMode(LandingScreenMode.JoiningFromInvite);
			OpenSubMenu(projectMarsWaitingScreen);
			didGoBackWhileJoiningInviteGame = false;
			if (networkService.IsRunning && !networkService.IsClient)
			{
				networkService.ShutdownAsync(OnShutDownForInviteGame);
			}
			else
			{
				JoinSessionFromInvite();
			}
		}

		private void OnShutDownForInviteGame(NetworkException exception)
		{
			if (!didGoBackWhileJoiningInviteGame)
			{
				if (exception != null)
				{
					OnHandleInviteGameError(exception);
				}
				else
				{
					JoinSessionFromInvite();
				}
			}
		}

		private void JoinSessionFromInvite()
		{
			networkService.JoinSessionFromInviteAsync(inviteBeingProcessed, OnJoinedSessionForInviteGame);
		}

		private void OnJoinedSessionForInviteGame(NetworkSession session, NetworkException exception)
		{
			if (didGoBackWhileJoiningInviteGame)
			{
				networkService.ShutdownAsync(null);
				return;
			}
			if (ExceptionIsAUserCancelledException(exception))
			{
				OnHandleInviteGameError(exception);
				return;
			}
			if (session == null || session.Metadata == null)
			{
				exception = new NetworkException(NetworkErrorCode.FailedToConnectToSession);
				OnHandleInviteGameError(exception);
				return;
			}
			if (!ContentDatabase.Instance().GetVersion().Equals(session.Metadata.GameVersionNumber, StringComparison.InvariantCulture))
			{
				exception = new NetworkException(NetworkErrorCode.VersionMismatch);
				OnHandleInviteGameError(exception);
				return;
			}
			if (exception != null)
			{
				OnHandleInviteGameError(exception);
				return;
			}
			inviteBeingProcessed = null;
			projectMarsWaitingScreen.SetMode(LandingScreenMode.Joining);
			OpenSubMenu(projectMarsWaitingScreen);
			projectMarsWaitingScreen.ShowSessionInfo(session, LoadMap, OnHandleInviteGameError);
		}

		private bool ExceptionIsAUserCancelledException(NetworkException exception)
		{
			if (exception == null)
			{
				return false;
			}
			if (exception != null && exception.ErrorCode == NetworkErrorCode.UserCancelled)
			{
				return true;
			}
			if (exception.InnerException != null && exception.InnerException is NetworkException ex && ex.ErrorCode == NetworkErrorCode.UserCancelled)
			{
				return true;
			}
			return false;
		}

		private void OnHandleInviteGameError(NetworkException exception)
		{
			inviteBeingProcessed = null;
			CloseSubMenuAndClearStack();
			networkService.ShutdownAsync(null);
			if (exception != null && !ExceptionIsAUserCancelledException(exception))
			{
				ShowError(GetErrorMessage(exception.ErrorCode), closeSubMenu: true);
			}
		}

		public void CreateMars(MapAsset.MapType mapType, int mapIndex, bool isPublicSession)
		{
			projectMarsWaitingScreen.SetMode(LandingScreenMode.Hosting);
			OpenSubMenu(projectMarsWaitingScreen);
			networkService.CreateSessionAsync(new CreateSessionProperties(mapType, mapIndex, canPlayCrossNetworkSession, isPublicSession), OnCreateSession);
		}

		private void OnCreateSession(NetworkSession session, NetworkException exception)
		{
			if (exception != null)
			{
				if (exception.ErrorCode != NetworkErrorCode.UserCancelled)
				{
					ShowError(GetErrorMessage(exception.ErrorCode), closeSubMenu: true);
				}
				return;
			}
			if (CurrentlyInvitedUser != null)
			{
				socialService.InvitePlayer(CurrentlyInvitedUser);
			}
			LoadMap(session);
		}

		public override void OpenSubMenu(UISubMenu menu)
		{
			SetHeading(menu.Title);
			base.OpenSubMenu(menu);
		}

		public async void OpenSendInviteScreen()
		{
			if (invitesScreen == null)
			{
				return;
			}
			if (!(await IsConnectedToInternet()))
			{
				ShowPopUpThatReturnsToPreviousPage();
			}
			else if (!didDoUserAuthentication)
			{
				projectMarsWaitingScreen.SetMode(LandingScreenMode.AuthenticatingUser);
				OpenSubMenu(projectMarsWaitingScreen);
				delayAuthentication.ScheduleUserAuthentication(projectMarsWaitingScreen, null, delegate(string data, Exception exception, int lastAuthenticationCount)
				{
					if (ProcessUserAuthenticationData(data, exception, closeIfException: false, lastAuthenticationCount, closeProjectMarsWaiting: true))
					{
						HandleOpenSendInviteScreen();
					}
				});
			}
			else
			{
				HandleOpenSendInviteScreen();
			}
		}

		public void OpenLobbyScreen()
		{
			if (invitesScreen != null)
			{
				OpenSubMenu(invitesScreen);
				invitesScreen.SetMode(RequestScreenMode.PublicLobby, SetHeading, ShowError, OnJoinedSession, OnHost, CreateSessionFilter());
			}
		}

		private void HandleOpenSendInviteScreen()
		{
			OpenSubMenu(invitesScreen);
			invitesScreen.SetMode(RequestScreenMode.SendInvitation, SetHeading);
		}

		private void OnPopUpCloseOpenModeSelectScreen()
		{
			SubscribeToModalPanelCloseEvent(subscribe: false);
			HandleShowModeSelectScreen();
		}

		private void HandleShowModeSelectScreen()
		{
			if (base.StackCount <= 1)
			{
				OpenSubMenu(modeSelectScreen);
			}
		}

		private void SetHeading(string title)
		{
			if (headingText != null)
			{
				headingText.LocaleID = title;
			}
		}

		private void ShowLevelSelect()
		{
			if (levelSelection == null)
			{
				Debug.LogError("levelSelection has not been set in ProjectMarsHandler.");
				return;
			}
			levelSelection.SetForProjectMars();
			OpenSubMenu(levelSelection.GetComponent<UISubMenu>());
			StartCoroutine(Delay());
			IEnumerator Delay()
			{
				yield return null;
				levelSelection.EnableBackButtonContainer(enable: false);
			}
		}

		private void OnJoinedSession(NetworkSession session)
		{
			CampaignPlayerDataHolder.StartedPlayingOnlineMultiplayer();
			LoadMap(session);
		}

		private void OnHost()
		{
			ShowLevelSelect();
		}

		private string GetErrorMessage(NetworkErrorCode errorCode)
		{
			switch (errorCode)
			{
			case NetworkErrorCode.Disconnected:
				return "NETWORK_ERROR_DISCONNECTED";
			case NetworkErrorCode.Shutdown:
				return "NETWORK_ERROR_SHUTDOWN";
			case NetworkErrorCode.FailedToStart:
				return "NETWORK_ERROR_FAILED_TO_START";
			case NetworkErrorCode.FailedToConnectToServer:
				return "NETWORK_ERROR_FAILED_SERVER";
			case NetworkErrorCode.ConnectionRefused:
				return "NETWORK_ERROR_CONNECTION_REFUSED";
			case NetworkErrorCode.FailedToConnectToSession:
				return "NETWORK_ERROR_FAILED_CONNECTION";
			case NetworkErrorCode.FailedToCreateSession:
				return "NETWORK_ERROR_FAILED_SESSION";
			case NetworkErrorCode.Timeout:
				return "NETWORK_ERROR_TIME_OUT";
			case NetworkErrorCode.VersionMismatch:
				return "NETWORK_ERROR_VERSION_MISMATCH";
			case NetworkErrorCode.NoInternetConnection:
				return "NETWORK_ERROR_NO_INTERNET_CONNECTION";
			default:
				return "NETWORK_ERROR_UNKNOWN";
			}
		}
	}
}
