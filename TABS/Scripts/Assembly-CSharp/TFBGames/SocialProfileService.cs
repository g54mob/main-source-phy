using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitCode.Networking;
using BitCode.Users;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TFBGames
{
	public class SocialProfileService : IService
	{
		private enum State
		{
			Idle = 0,
			WaitingToShowConfirmationDialog = 1,
			ShowingConfirmationDialog = 2,
			GoingToMainMenu = 3,
			EnteredMainMenuDelay = 4,
			WaitingForShutdown = 5,
			ReadyToJoinSession = 6,
			InviteAcceptedFromShell = 7
		}

		private const int MaxEnteredMainMenuDelay = 3;

		private const string LocalizedInvitedToJoinMessage = "MESSAGE_INVITED_TO_JOIN";

		private const string LocalizedNetworkErrorMessage = "NETWORK_ERROR_FAILED_CONNECTION";

		private const string LocalizedProgressLossWarningMessage = "MESSAGE_INVITED_TO_JOIN_WITH_WARNING";

		private const string LocalizedSessionFull = "MULTIPLAYER_SESSION_FULL";

		private const int PropertyExceptionsCapacity = 10;

		private State state;

		private List<IUserAccount> cachedUserAccounts = new List<IUserAccount>();

		private INetworkService networkService;

		private IPlatformNetworkManagerService platformNetworkManagerService;

		private IGameInvitationService invitationService;

		private AccountManager accountManager;

		private IFriendService friendService;

		private ModalPanel modalPanel;

		private ILocalAccount localAccount;

		private IGameInvitation gameInvitation;

		private int enteredMainMenuDelay;

		private int modalPanelOpenId;

		private GameModeService gameModeService;

		private readonly Dictionary<string, Exception> propertyExceptions = new Dictionary<string, Exception>(10);

		public event Action<IGameInvitation> ReceivedInvitation;

		public event Action<List<PlayerProfile>> ProfilesRetrieved;

		public virtual void OnStart()
		{
			gameModeService = ServiceLocator.GetService<GameModeService>();
		}

		public void GetFriends()
		{
			localAccount = accountManager.ActiveAccount;
			if (accountManager.LocalAccountManager == null)
			{
				string message = "accountManager.LocalAccountManager is null";
				GetFriendsCallback(null, new InvalidOperationException(message));
			}
			else if (localAccount == null)
			{
				string message2 = "localAccount is null";
				GetFriendsCallback(null, new InvalidOperationException(message2));
			}
			else if (friendService == null)
			{
				string message3 = "friendService is null";
				GetFriendsCallback(null, new InvalidOperationException(message3));
			}
			else
			{
				friendService.GetFriendListAsync(localAccount, GetFriendsCallback);
			}
		}

		public void InvitePlayer(IUserAccount user)
		{
			InviteUserToSession(user);
		}

		public PlayerProfile GetPlayerProfileFromID(object id)
		{
			if (!(id is ulong num))
			{
				return null;
			}
			foreach (IUserAccount cachedUserAccount in cachedUserAccounts)
			{
				if (cachedUserAccount.OnlineAccountId == num)
				{
					return ProjectMarsHelpers.ProfileFromUserAccount(cachedUserAccount);
				}
			}
			return null;
		}

		private void SetState(State newState)
		{
			state = newState;
			switch (state)
			{
			case State.Idle:
				ClearInvitation();
				break;
			case State.ShowingConfirmationDialog:
				HandleInviteBasedOnGameMode(withPopUp: true);
				break;
			case State.GoingToMainMenu:
				networkService.ShutdownAsync(null);
				TABSSceneManager.LoadMainMenu();
				break;
			case State.WaitingForShutdown:
				networkService.ShutdownAsync(null);
				break;
			case State.EnteredMainMenuDelay:
				enteredMainMenuDelay = 3;
				break;
			case State.ReadyToJoinSession:
				JoinSessionFromInvite();
				break;
			case State.InviteAcceptedFromShell:
				CloseModalPanelPopUp();
				HandleInviteBasedOnGameMode(withPopUp: false);
				break;
			case State.WaitingToShowConfirmationDialog:
				break;
			}
		}

		private void ClearInvitation()
		{
			gameInvitation = null;
		}

		private void CloseModalPanelPopUp()
		{
			if (modalPanel == null)
			{
				Debug.LogError("modalPanel is null");
			}
			else if (modalPanel.IsPopupOpen)
			{
				modalPanel.ForcePopUpClose();
			}
		}

		private void HandleInviteBasedOnGameMode(bool withPopUp)
		{
			if (gameModeService == null)
			{
				Debug.LogError("gameModeService is null");
				SetState(State.Idle);
				return;
			}
			BaseGameMode currentGameMode = gameModeService.CurrentGameMode;
			if (currentGameMode != null)
			{
				if (currentGameMode is UnitCreatorGameMode)
				{
					modalPanelOpenId = modalPanel.Choice(string.Empty, "MESSAGE_INVITED_TO_JOIN_WITH_WARNING", OnConfirmationDialogYes, OnConfirmationDialogNo);
					return;
				}
				if (currentGameMode is MapCreatorGameMode)
				{
					modalPanelOpenId = modalPanel.Choice(string.Empty, "MESSAGE_INVITED_TO_JOIN_WITH_WARNING", OnConfirmationDialogYes, OnConfirmationDialogNo);
					return;
				}
			}
			if (withPopUp)
			{
				modalPanelOpenId = modalPanel.Choice(string.Empty, "MESSAGE_INVITED_TO_JOIN", OnConfirmationDialogYes, OnConfirmationDialogNo);
			}
			else
			{
				OnConfirmationDialogYes();
			}
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (state == State.GoingToMainMenu)
			{
				SetState(State.EnteredMainMenuDelay);
			}
		}

		private async void InviteUserToSession(IUserAccount user)
		{
			if (networkService.IsRunning && platformNetworkManagerService.CanSendInvites)
			{
				IMultiplayerSession activeSession = platformNetworkManagerService.ActiveSession;
				ILocalAccount activeAccount = accountManager.ActiveAccount;
				IRemoteAccount[] invitees = new IRemoteAccount[1] { user as IRemoteAccount };
				byte[] applicationData = networkService.CreateJoinSessionPropertiesAsDataBuffer();
				IGameInvitation gameInvitation = invitationService.CreateInviteToMultiplayerSession(activeSession, applicationData);
				if (activeAccount != null && invitationService != null && gameInvitation != null)
				{
					await invitationService.SendGameInviteAsync(activeAccount, invitees, gameInvitation);
				}
			}
		}

		private void OnInvitationReceived(IGameInvitation invitation, ILocalAccount recipient)
		{
			if (state == State.Idle)
			{
				gameInvitation = invitation;
				if (modalPanel.IsPopupOpen)
				{
					SetState(State.WaitingToShowConfirmationDialog);
				}
				else
				{
					SetState(State.ShowingConfirmationDialog);
				}
			}
		}

		private void OnConfirmationDialogNo()
		{
			if (!CheckIfAnotherClassOpenedTheDialog())
			{
				SetState(State.Idle);
			}
		}

		private void OnConfirmationDialogYes()
		{
			if (!TABSSceneManager.IsInMainMenuScene())
			{
				SetState(State.GoingToMainMenu);
			}
			else if (networkService.IsRunning)
			{
				SetState(State.WaitingForShutdown);
			}
			else
			{
				SetState(State.ReadyToJoinSession);
			}
		}

		private async void JoinSessionFromInvite()
		{
			IGameInvitation gameInvitation = this.gameInvitation;
			SetState(State.Idle);
			this.gameInvitation = gameInvitation;
			if (this.gameInvitation == null)
			{
				return;
			}
			try
			{
				this.ReceivedInvitation?.Invoke(this.gameInvitation);
				ClearInvitation();
			}
			catch (Exception)
			{
				ClearInvitation();
				modalPanel.PopUp("NETWORK_ERROR_FAILED_CONNECTION");
			}
		}

		private void GetFriendsCallback(IRemoteAccount[] friends, Exception e)
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
			}
			List<PlayerProfile> list = new List<PlayerProfile>();
			if (friends != null)
			{
				cachedUserAccounts = GetAllUserAccounts(friends);
				SetTrackedDetails(cachedUserAccounts);
				foreach (IUserAccount cachedUserAccount in cachedUserAccounts)
				{
					try
					{
						if (cachedUserAccount.OnlineStatus.Status == UserAccountPropertyStatus.Loaded && !(cachedUserAccount is ILocalAccount) && cachedUserAccount.OnlineStatus.Value != UserAccountOnlineStatus.Offline && cachedUserAccount.OnlineStatus.Value != UserAccountOnlineStatus.Invisible && cachedUserAccount.OnlineStatus.Value != UserAccountOnlineStatus.Unknown && cachedUserAccount.OnlineStatus.Status == UserAccountPropertyStatus.Loaded && cachedUserAccount is IRemoteAccount)
						{
							PlayerProfile item = new PlayerProfile(cachedUserAccount, null);
							list.Add(item);
						}
					}
					catch (Exception ex)
					{
						Debug.LogError(ex.Message);
					}
				}
			}
			this.ProfilesRetrieved?.Invoke(list);
		}

		private List<IUserAccount> GetAllUserAccounts(IRemoteAccount[] friends)
		{
			List<IUserAccount> list = new List<IUserAccount>();
			if (localAccount != null)
			{
				list.Add(localAccount);
			}
			list.AddRange(friends);
			return list;
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
			propertyExceptions.Clear();
			string key = string.Empty;
			try
			{
				key = "Name";
				userAcc.Name.SetTracked(track: true);
			}
			catch (Exception value)
			{
				propertyExceptions.Add(key, value);
			}
			try
			{
				key = "AvatarImage";
				userAcc.AvatarImage.SetTracked(track: true);
			}
			catch (Exception value2)
			{
				propertyExceptions.Add(key, value2);
			}
			try
			{
				key = "OnlineStatus";
				userAcc.OnlineStatus.SetTracked(track: true);
			}
			catch (Exception value3)
			{
				propertyExceptions.Add(key, value3);
			}
			if (propertyExceptions.Count <= 0)
			{
				return;
			}
			foreach (KeyValuePair<string, Exception> propertyException in propertyExceptions)
			{
				key = propertyException.Key;
				Exception value4 = propertyException.Value;
				if (value4 == null)
				{
					continue;
				}
				if (!(value4 is NotSupportedException ex))
				{
					if (!(value4 is NotImplementedException ex2))
					{
						value4 = value4;
						Exception ex3 = value4;
						Debug.LogErrorFormat("Got an exception while trying to use a property ({0}).\n{1}", key, ex3);
					}
					else
					{
						NotImplementedException ex4 = ex2;
						Debug.LogWarningFormat("Got an exception while trying to use a property ({0}).\n{1}", key, ex4);
					}
				}
				else
				{
					NotSupportedException ex5 = ex;
					Debug.LogWarningFormat("Got an exception while trying to use a property ({0}).\n{1}", key, ex5);
				}
			}
		}

		public virtual void OnAwake()
		{
			invitationService = ServiceLocator.GetService<IGameInvitationService>();
			platformNetworkManagerService = ServiceLocator.GetService<IPlatformNetworkManagerService>();
			accountManager = ServiceLocator.GetService<AccountManager>();
			networkService = ServiceLocator.GetService<INetworkService>();
			friendService = ServiceLocator.GetService<IFriendService>();
			modalPanel = ServiceLocator.GetService<ModalPanel>();
			if (invitationService != null)
			{
				invitationService.InvitationReceived += OnInvitationReceived;
			}
			SceneManager.sceneLoaded += OnSceneLoaded;
			accountManager.ActiveAccountChanged += OnActiveAccountChanged;
			accountManager.FireWhenAccountIsSelected(OnAccountIsSelected);
		}

		public virtual void UnRegister()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			for (int i = 0; i < cachedUserAccounts.Count; i++)
			{
				if (!(cachedUserAccounts[i] is ILocalAccount))
				{
					if (cachedUserAccounts[i] is IDisposable disposable)
					{
						disposable.Dispose();
					}
					cachedUserAccounts.Clear();
				}
			}
			if (invitationService != null)
			{
				invitationService.InvitationReceived -= OnInvitationReceived;
			}
			if (accountManager != null)
			{
				accountManager.ActiveAccountChanged -= OnActiveAccountChanged;
			}
		}

		public virtual void OnUpdate()
		{
			switch (state)
			{
			case State.WaitingToShowConfirmationDialog:
				if (!modalPanel.IsPopupOpen)
				{
					SetState(State.ShowingConfirmationDialog);
				}
				break;
			case State.ShowingConfirmationDialog:
				CheckIfAnotherClassOpenedTheDialog();
				break;
			case State.WaitingForShutdown:
			{
				PlatformSyncedNetworkService platformSyncedNetworkService = (PlatformSyncedNetworkService)networkService;
				if (!networkService.IsRunning && !platformSyncedNetworkService.IsSessionActive)
				{
					SetState(State.ReadyToJoinSession);
				}
				break;
			}
			case State.EnteredMainMenuDelay:
				enteredMainMenuDelay--;
				if (enteredMainMenuDelay <= 0)
				{
					if (networkService.IsRunning)
					{
						SetState(State.WaitingForShutdown);
					}
					else
					{
						SetState(State.ReadyToJoinSession);
					}
				}
				break;
			case State.GoingToMainMenu:
				break;
			}
		}

		public virtual void OnRegister()
		{
		}

		public virtual void OnFixedUpdate()
		{
		}

		public virtual void OnLateUpdate()
		{
		}

		private void OnAccountIsSelected(ILocalAccount initialAccount)
		{
			localAccount = initialAccount;
		}

		private void OnActiveAccountChanged(ILocalAccount newAccount)
		{
			localAccount = newAccount;
		}

		private bool CheckIfAnotherClassOpenedTheDialog()
		{
			if (modalPanel.OpenId == modalPanelOpenId)
			{
				return false;
			}
			if (modalPanel.IsPopupOpen)
			{
				SetState(State.WaitingToShowConfirmationDialog);
			}
			else
			{
				SetState(State.ShowingConfirmationDialog);
			}
			return true;
		}

		public bool CanShowFriedProfile()
		{
			return false;
		}

		public async Task ShowFriendProfile(IUserAccount itemUserAccount)
		{
			Debug.LogError("Profile UI Not implemented for this platform");
		}
	}
}
