using Landfall.TABS;
using Landfall.TABS.GameMode;
using UnityEngine;

namespace TFBGames
{
	public class NetworkDisconnectMonitor : MonoBehaviour
	{
		private enum State
		{
			Idle = 0,
			DelayBeforeShowingDialog = 1,
			WaitingToShowDialog = 2,
			ShowingDialog = 3
		}

		private const float ShowMessageDelay = 1f;

		private INetworkService m_networkService;

		private INetworkQuitController m_networkQuit;

		private ModalPanel m_modalPanel;

		private float? m_showMessageTime;

		private State m_state;

		private void Awake()
		{
			if (ServiceLocator.GetService<GameModeService>().CurrentGameMode.GetType() != typeof(OnlineMultiplayerGameMode))
			{
				base.enabled = false;
			}
			m_networkService = ServiceLocator.GetService<INetworkService>();
			m_networkQuit = ServiceLocator.GetService<INetworkQuitController>();
			m_modalPanel = ServiceLocator.GetService<ModalPanel>();
		}

		private void Update()
		{
			switch (m_state)
			{
			case State.Idle:
				UpdateIdle();
				break;
			case State.DelayBeforeShowingDialog:
				UpdateDelayBeforeShowingDialog();
				break;
			case State.WaitingToShowDialog:
				UpdateWaitingToShowDialog();
				break;
			}
		}

		private void SetState(State state)
		{
			m_state = state;
		}

		private void UpdateIdle()
		{
			bool flag = m_networkService.IsServer || m_networkService.IsClient;
			if (!(m_networkService.IsRunning && flag) && ((!m_networkQuit.DidQuit && !m_networkQuit.DidOpponentQuit) || m_networkQuit.DialogState == NetworkQuitControllerDialogState.ClosedOutsideBattleScene))
			{
				m_modalPanel.CloseRulePopUps();
				m_showMessageTime = Time.realtimeSinceStartup + 1f;
				SetState(State.DelayBeforeShowingDialog);
			}
		}

		private void UpdateDelayBeforeShowingDialog()
		{
			if (m_showMessageTime.HasValue && !(m_showMessageTime.Value >= Time.realtimeSinceStartup))
			{
				m_showMessageTime = null;
				if (ShowDialog())
				{
					SetState(State.ShowingDialog);
				}
				else
				{
					SetState(State.WaitingToShowDialog);
				}
			}
		}

		private void UpdateWaitingToShowDialog()
		{
			if (ShowDialog())
			{
				SetState(State.ShowingDialog);
			}
		}

		private bool ShowDialog()
		{
			if (m_modalPanel.IsPopupOpen)
			{
				return false;
			}
			m_modalPanel.SetForDisconnectPopUp(showForDisconnect: true);
			m_modalPanel.PopUp("MP_POPUP_CONNCETION_LOST", OnClosedDialog);
			return true;
		}

		private void OnClosedDialog()
		{
			m_modalPanel.SetForDisconnectPopUp(showForDisconnect: false);
			if (!TABSSceneManager.IsInMainMenuScene())
			{
				TABSSceneManager.LoadMainMenu();
			}
		}
	}
}
