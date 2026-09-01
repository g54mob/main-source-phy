using InControl;
using Landfall.TABS;
using Landfall.TABS_Input;

namespace TFBGames
{
	public class ShowUiModePopup : ServicePrefab
	{
		private enum State
		{
			Initializing = 0,
			Idle = 1,
			ShowPopupInNextUpdate = 2,
			UnregisterInNextUpdate = 3,
			Unregistered = 4
		}

		private const string DidShowUiModePopupKey = "DidShowUiModePopup";

		private const int DidShowUiModePopupValue = 1;

		private const string PopupMessage = "POPUP_GAMEPAD_MESSAGE";

		private const int MaxShowPopupDelay = 2;

		private State m_state;

		private IPlayerPrefsPlatform m_playerPrefs;

		private InputService m_inputService;

		private ModalPanel m_modalPanel;

		private int m_ShowPopupDelay;

		private void Update()
		{
			switch (m_state)
			{
			case State.ShowPopupInNextUpdate:
				if (m_ShowPopupDelay > 0 && m_modalPanel != null && !m_modalPanel.IsPopupOpen)
				{
					m_ShowPopupDelay--;
					if (m_ShowPopupDelay <= 0)
					{
						ShowPopup();
					}
				}
				break;
			case State.UnregisterInNextUpdate:
				ServiceLocator.UnRegisterSerice<ShowUiModePopup>();
				break;
			}
		}

		public override void OnStart()
		{
			base.OnStart();
			m_playerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			m_modalPanel = ServiceLocator.GetService<ModalPanel>();
			m_inputService = ServiceLocator.GetService<InputService>();
			ServiceLocator.GetService<WaitForStorage>().FireWhenReady(OnStorageReady);
		}

		public override void UnRegister()
		{
			base.UnRegister();
			base.enabled = false;
			if (m_inputService != null)
			{
				m_inputService.InputDeviceStyleChanged -= OnInputDeviceStyleChanged;
			}
			m_playerPrefs = null;
			m_inputService = null;
			m_modalPanel = null;
			SetState(State.Unregistered);
		}

		private void OnStorageReady()
		{
			if (m_playerPrefs == null || m_playerPrefs.GetInt("DidShowUiModePopup") == 1)
			{
				SetState(State.UnregisterInNextUpdate);
				return;
			}
			SetState(State.Idle);
			if (m_inputService != null)
			{
				m_inputService.InputDeviceStyleChanged += OnInputDeviceStyleChanged;
			}
			CheckIfShouldShowPopup();
		}

		private void OnInputDeviceStyleChanged(InputDeviceStyle deviceStyle)
		{
			CheckIfShouldShowPopup();
		}

		private void SetState(State newState)
		{
			m_state = newState;
			if (newState == State.ShowPopupInNextUpdate)
			{
				m_ShowPopupDelay = 2;
			}
		}

		private void CheckIfShouldShowPopup()
		{
			if (m_state == State.Idle && !(m_inputService == null) && m_playerPrefs != null && !(m_modalPanel == null) && PlayerActions.Instance.InputType == InputType.Controller)
			{
				SetState(State.ShowPopupInNextUpdate);
			}
		}

		private void ShowPopup()
		{
			m_ShowPopupDelay = 0;
			m_modalPanel.PopUp("POPUP_GAMEPAD_MESSAGE");
			m_playerPrefs.SetInt("DidShowUiModePopup", 1);
			m_playerPrefs.Save();
			SetState(State.UnregisterInNextUpdate);
		}
	}
}
