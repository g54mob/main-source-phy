using System.Threading.Tasks;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit;
using UdpKit.Platform.Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TFBGames
{
	public class NetworkQuitController : GlobalEventListener, INetworkQuitController, IService
	{
		private const string OpponentLeftGameLocalizationKey = "MP_OPPONENT_LEFT_GAME";

		private INetworkService m_networkService;

		private ModalPanel m_modalPanel;

		private IPlatformUtils m_platformUtils;

		private bool m_didShutdownBolt;

		private bool m_mustWaitForShutdownBeforeLoadingMainMenu;

		private bool m_playerQuitEventLoadMainMenu;

		private bool m_didQuitInResultsScreen;

		private bool m_didOpponentQuitInResultsScreen;

		public bool DidQuit { get; private set; }

		public bool DidOpponentQuit { get; private set; }

		public NetworkQuitControllerDialogState DialogState { get; private set; }

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void OnStart()
		{
			m_networkService = ServiceLocator.GetService<INetworkService>();
			m_modalPanel = ServiceLocator.GetService<ModalPanel>();
			m_platformUtils = ServiceLocator.GetService<IPlatformUtils>();
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		public void UnRegister()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			SetProjectMarsHandlerEventSubscriptions(subscribe: false);
		}

		public void OnUpdate()
		{
			if (DialogState == NetworkQuitControllerDialogState.WaitingToShow)
			{
				ShowOpponentQuitDialog();
			}
		}

		public void QuitMultiplayerGame(bool loadMainMenu = true)
		{
			if (!DidQuit && !DidOpponentQuit)
			{
				DidQuit = true;
				m_didQuitInResultsScreen = IsResultsScreenOpenOrWasJustClosed(checkIfJustClosed: false);
				if (m_networkService.IsConnected)
				{
					m_playerQuitEventLoadMainMenu = loadMainMenu;
					PlayerQuitEvent playerQuitEvent = PlayerQuitEvent.Create(GlobalTargets.Everyone);
					playerQuitEvent.Team = (int)m_networkService.PlayerTeam;
					playerQuitEvent.Send();
				}
				else
				{
					HandlePlayerQuit(loadMainMenu);
				}
			}
		}

		public override bool PersistBetweenStartupAndShutdown()
		{
			return true;
		}

		public override void Disconnected(BoltConnection connection)
		{
			if (connection.DisconnectReason == UdpConnectionDisconnectReason.Timeout && connection.ConnectionType == UdpConnectionType.Unknown)
			{
				Debug.Log("Ignoring unknown timeout disconnection message.");
				return;
			}
			base.Disconnected(connection);
			if (BoltMatchmaking.CurrentSession is PhotonSession photonSession && !photonSession.IsOpen)
			{
				OpponentHasQuit();
			}
		}

		public override void OnEvent(PlayerQuitEvent playerQuitEvent)
		{
			base.OnEvent(playerQuitEvent);
			if (playerQuitEvent.Team == (int)m_networkService.PlayerTeam)
			{
				HandlePlayerQuit(m_playerQuitEventLoadMainMenu);
			}
			else
			{
				OpponentHasQuit();
			}
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (TABSSceneManager.IsInMainMenuScene())
			{
				SetProjectMarsHandlerEventSubscriptions(subscribe: true);
				ResetProperties();
			}
		}

		private void ResetProperties()
		{
			DidQuit = false;
			DidOpponentQuit = false;
			m_didQuitInResultsScreen = false;
			m_didOpponentQuitInResultsScreen = false;
			m_didShutdownBolt = false;
			m_mustWaitForShutdownBeforeLoadingMainMenu = false;
			SetDialogState(NetworkQuitControllerDialogState.Invalid);
		}

		private void SetDialogState(NetworkQuitControllerDialogState newState)
		{
			DialogState = newState;
		}

		private void SetProjectMarsHandlerEventSubscriptions(bool subscribe)
		{
			if (!(ProjectMarsHandler.Instance == null))
			{
				ProjectMarsHandler.Instance.Destroyed -= OnProjectMarsHandlerDestroyed;
				ProjectMarsHandler.Instance.DoingUserAuth -= OnProjectMarsDoingUserAuth;
				if (subscribe)
				{
					ProjectMarsHandler.Instance.Destroyed += OnProjectMarsHandlerDestroyed;
					ProjectMarsHandler.Instance.DoingUserAuth += OnProjectMarsDoingUserAuth;
				}
			}
		}

		private void OnProjectMarsHandlerDestroyed()
		{
			SetProjectMarsHandlerEventSubscriptions(subscribe: false);
		}

		private void OnProjectMarsDoingUserAuth()
		{
			ResetProperties();
		}

		private async void OpponentHasQuit()
		{
			if (!DidOpponentQuit && !DidQuit)
			{
				if (m_networkService.IsServer)
				{
					DestroyAllUnits();
				}
				else if (m_networkService.IsClient)
				{
					m_mustWaitForShutdownBeforeLoadingMainMenu = true;
					await WaitForUnitsToBeDestroyed();
					ShutdownBolt();
				}
				DidOpponentQuit = true;
				m_didOpponentQuitInResultsScreen = IsResultsScreenOpenOrWasJustClosed(checkIfJustClosed: true);
				ShowOpponentQuitDialog();
			}
		}

		private void ShowOpponentQuitDialog()
		{
			m_modalPanel.CloseRulePopUps();
			if (m_modalPanel.IsPopupOpen)
			{
				SetDialogState(NetworkQuitControllerDialogState.WaitingToShow);
				return;
			}
			SetDialogState(NetworkQuitControllerDialogState.IsShowing);
			m_modalPanel.SetForDisconnectPopUp(showForDisconnect: true);
			m_modalPanel.PopUp("MP_OPPONENT_LEFT_GAME", delegate
			{
				SetDialogState(TABSSceneManager.IsInGameScene() ? NetworkQuitControllerDialogState.ClosedInBattleScene : NetworkQuitControllerDialogState.ClosedOutsideBattleScene);
				HandlePlayerQuit();
			});
		}

		private async void HandlePlayerQuit(bool loadMainMenu = true)
		{
			bool didClientQuitDuringResultsScreen = m_networkService.IsServer && DidOpponentQuit && m_didOpponentQuitInResultsScreen;
			bool flag = m_networkService.IsServer && m_networkService.GetConnectionsCount() > 0;
			bool shouldServerWaitForClientToShutDown = false;
			bool showLoadingPopup = false;
			int? loadingPopupOpenId = null;
			if (m_networkService.IsServer)
			{
				DestroyAllUnits();
			}
			if (DidQuit && m_networkService.IsClient)
			{
				m_modalPanel.CloseRulePopUps();
				await WaitForUnitsToBeDestroyed();
				if (m_didQuitInResultsScreen)
				{
					await Task.Delay(4000);
				}
			}
			else if (DidQuit && flag && !loadMainMenu)
			{
				shouldServerWaitForClientToShutDown = true;
			}
			else if (didClientQuitDuringResultsScreen && flag)
			{
				shouldServerWaitForClientToShutDown = true;
				showLoadingPopup = true;
			}
			else if (DidQuit && flag && m_platformUtils != null && m_platformUtils.IsRunningInBackground)
			{
				shouldServerWaitForClientToShutDown = true;
				showLoadingPopup = true;
			}
			if (showLoadingPopup)
			{
				loadingPopupOpenId = m_modalPanel.WaitPopUpWithFocus("MP_LABEL_LOADING", false, -1f, null, null, true);
				await Task.Delay(500);
			}
			if (shouldServerWaitForClientToShutDown)
			{
				NetworkBattleController service = ServiceLocator.GetService<NetworkBattleController>();
				float timeOutSeconds = ((!(service != null) || !service.AreBothPlayersInBattleScene) ? 10f : (didClientQuitDuringResultsScreen ? 7f : 3f));
				float startTime = Time.realtimeSinceStartup;
				while (m_networkService.GetConnectionsCount() > 0)
				{
					await Task.Delay(200);
					if (Time.realtimeSinceStartup - startTime > timeOutSeconds)
					{
						break;
					}
				}
				await Task.Delay(3500);
			}
			ShutdownBolt();
			if (loadingPopupOpenId.HasValue && loadingPopupOpenId.Value == m_modalPanel.OpenId)
			{
				m_modalPanel.CloseWaitPopup();
			}
			m_modalPanel.SetForDisconnectPopUp(showForDisconnect: false);
			if (!TABSSceneManager.IsInMainMenuScene() && loadMainMenu)
			{
				LoadMainMenu();
			}
		}

		private async void LoadMainMenu()
		{
			if (m_mustWaitForShutdownBeforeLoadingMainMenu && m_networkService.IsRunning)
			{
				int openId = m_modalPanel.WaitPopUpWithFocus("MP_LABEL_LOADING", false, -1f, null, null, true);
				await Task.Delay(500);
				while (m_networkService.IsRunning)
				{
					await Task.Delay(200);
				}
				if (openId == m_modalPanel.OpenId)
				{
					m_modalPanel.CloseWaitPopup();
				}
			}
			TABSSceneManager.LoadMainMenu();
		}

		private void ShutdownBolt()
		{
			if (!m_didShutdownBolt && m_networkService.IsRunning)
			{
				m_didShutdownBolt = true;
				m_networkService.ShutdownAsync(null);
			}
		}

		private void DestroyAllUnits()
		{
			ServiceLocator.GetService<INetworkUnitsManager>()?.DestroyAllUnits();
		}

		private async Task WaitForUnitsToBeDestroyed()
		{
			INetworkUnitsManager networkUnits = ServiceLocator.GetService<INetworkUnitsManager>();
			if (networkUnits != null)
			{
				while (m_networkService.IsConnected && networkUnits != null && networkUnits.GetNetworkUnitsCount() > 0)
				{
					await Task.Delay(200);
				}
			}
		}

		private bool IsResultsScreenOpenOrWasJustClosed(bool checkIfJustClosed)
		{
			NetworkBattleController service = ServiceLocator.GetService<NetworkBattleController>();
			if (service != null && service.Phase == NetworkGamePhase.BattleEnded)
			{
				return true;
			}
			if (!checkIfJustClosed)
			{
				return false;
			}
			if (!(ServiceLocator.GetService<GameModeService>().CurrentGameMode is OnlineMultiplayerGameMode onlineMultiplayerGameMode) || !onlineMultiplayerGameMode.TimerEndedTime.HasValue)
			{
				return false;
			}
			return Time.realtimeSinceStartup - onlineMultiplayerGameMode.TimerEndedTime.Value < 1f;
		}
	}
}
