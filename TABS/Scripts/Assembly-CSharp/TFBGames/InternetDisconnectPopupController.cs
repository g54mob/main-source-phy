using System;
using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	public class InternetDisconnectPopupController : MonoBehaviour
	{
		private enum State
		{
			Disabled = 0,
			Idle = 1,
			DelayBeforeShowingDialog = 2,
			WaitingToShowDialog = 3,
			ShowingDialog = 4
		}

		private const string DisconnectedMessageKey = "NETWORK_ERROR_DISCONNECTED";

		private const float ShowMessageDelay = 2f;

		private State m_State;

		private IInternetStatusService m_InternetStatus;

		private ModalPanel m_ModalPanel;

		private float m_ShowMessageTime;

		private bool m_HasPendingDisable;

		private bool m_WasConnected;

		public event Action InternetDisconnectedPopupClosed;

		private void Awake()
		{
			m_InternetStatus = ServiceLocator.GetService<IInternetStatusService>();
			m_ModalPanel = ServiceLocator.GetService<ModalPanel>();
		}

		private void Update()
		{
			UpdateState();
		}

		public void Enable()
		{
			if (m_State == State.Disabled)
			{
				m_WasConnected = true;
				m_HasPendingDisable = false;
				SetState(State.Idle);
			}
		}

		public void Disable()
		{
			if (m_State != State.ShowingDialog)
			{
				SetState(State.Disabled);
			}
			else
			{
				m_HasPendingDisable = true;
			}
		}

		private void SubscribeToEvent(bool subscribe)
		{
			if (m_InternetStatus != null)
			{
				m_InternetStatus.InternetDisconnected -= OnInternetDisconnected;
				if (subscribe)
				{
					m_InternetStatus.InternetDisconnected += OnInternetDisconnected;
				}
			}
		}

		private void SetState(State newState)
		{
			if (m_InternetStatus != null)
			{
				m_State = newState;
				switch (m_State)
				{
				case State.Disabled:
					m_HasPendingDisable = false;
					SubscribeToEvent(subscribe: false);
					break;
				case State.Idle:
					SubscribeToEvent(subscribe: true);
					break;
				case State.DelayBeforeShowingDialog:
					m_ShowMessageTime = Time.realtimeSinceStartup + 2f;
					break;
				}
			}
		}

		private void UpdateState()
		{
			switch (m_State)
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

		private void OnInternetDisconnected()
		{
			if (m_State == State.Idle)
			{
				HandleDisconnect();
			}
		}

		private void HandleDisconnect()
		{
			SetState(State.DelayBeforeShowingDialog);
		}

		private void UpdateIdle()
		{
			if (m_InternetStatus != null)
			{
				bool flag = m_InternetStatus.IsConnectedWithCache(connectIfNotConnected: false);
				bool wasConnected = m_WasConnected;
				m_WasConnected = flag;
				if (!flag && wasConnected)
				{
					HandleDisconnect();
				}
			}
		}

		private void UpdateDelayBeforeShowingDialog()
		{
			bool flag = m_InternetStatus.IsConnectedWithCache(connectIfNotConnected: false);
			if (m_InternetStatus != null && flag)
			{
				SetState(State.Idle);
			}
			else if (!(m_ShowMessageTime >= Time.realtimeSinceStartup))
			{
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
			if (m_ModalPanel.IsPopupOpen)
			{
				return false;
			}
			m_ModalPanel.PopUp("NETWORK_ERROR_DISCONNECTED", OnClosedDialog);
			return true;
		}

		private void OnClosedDialog()
		{
			if (m_HasPendingDisable)
			{
				SetState(State.Disabled);
			}
			else
			{
				SetState(State.Idle);
			}
			this.InternetDisconnectedPopupClosed?.Invoke();
		}
	}
}
