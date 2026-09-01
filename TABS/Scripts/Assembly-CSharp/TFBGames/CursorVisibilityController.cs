using InControl;
using Landfall.TABS_Input;
using LevelCreator;
using UnityEngine;

namespace TFBGames
{
	public class CursorVisibilityController : ServicePrefab
	{
		private const CursorLockMode DefaultCursorLockMode = CursorLockMode.None;

		private const bool DefaultCursorVisibility = true;

		private const CursorLockMode LockModeWhenNotSupportedOrUsingGamepad = CursorLockMode.Locked;

		private CursorLockMode m_lockState;

		private bool m_visible = true;

		private CursorLockMode? m_restoreGamepadLockState;

		private bool? m_restoreGamepadVisible;

		private PlayerActions m_playerActions;

		private bool m_didSetLockState;

		private bool m_didSetVisibility;

		private InputType m_inputType;

		private void OnApplicationFocus(bool hasFocus)
		{
			if (hasFocus)
			{
				OnGotFocusOrUnPaused();
			}
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (!pauseStatus)
			{
				OnGotFocusOrUnPaused();
			}
		}

		public override void OnStart()
		{
			base.OnStart();
			m_playerActions = PlayerActions.Instance;
			m_playerActions.OnLastInputTypeChanged += OnLastInputTypeChanged;
			if (!m_didSetLockState)
			{
				SetLockState(CursorLockMode.None);
			}
			if (!m_didSetVisibility)
			{
				SetVisibility(visible: true);
			}
		}

		public override void UnRegister()
		{
			base.UnRegister();
			if (m_playerActions != null)
			{
				m_playerActions.OnLastInputTypeChanged -= OnLastInputTypeChanged;
			}
		}

		public void SetLockState(CursorLockMode lockState)
		{
			if (!DMEditor.Instance)
			{
				m_didSetLockState = true;
				if (!DoesPlatformSupportCursor())
				{
					lockState = CursorLockMode.Locked;
				}
				if (IsUsingGamepad())
				{
					m_restoreGamepadLockState = lockState;
					lockState = CursorLockMode.Locked;
				}
				m_lockState = lockState;
				Cursor.lockState = m_lockState;
			}
		}

		public void SetVisibility(bool visible)
		{
			if (!DMEditor.Instance)
			{
				m_didSetVisibility = true;
				if (!DoesPlatformSupportCursor())
				{
					visible = false;
				}
				if (IsUsingGamepad())
				{
					m_restoreGamepadVisible = visible;
					visible = false;
				}
				m_visible = visible;
				Cursor.visible = m_visible;
			}
		}

		public void SetLockStateAndVisibility(CursorLockMode lockState, bool visible)
		{
			SetLockState(lockState);
			SetVisibility(visible);
		}

		private bool DoesPlatformSupportCursor()
		{
			return true;
		}

		private bool IsUsingGamepad()
		{
			if (m_playerActions != null)
			{
				return m_playerActions.InputType == InputType.Controller;
			}
			return false;
		}

		private void OnGotFocusOrUnPaused()
		{
			CheckInputAndUpdateCursorStates();
			SetLockStateAndVisibility(m_lockState, m_visible);
		}

		private void CheckInputAndUpdateCursorStates()
		{
			if (m_playerActions == null || m_inputType == m_playerActions.InputType)
			{
				return;
			}
			m_inputType = m_playerActions.InputType;
			switch (m_inputType)
			{
			case InputType.Controller:
			{
				CursorLockMode lockState = m_lockState;
				bool visible = m_visible;
				SetLockStateAndVisibility(CursorLockMode.Locked, visible: false);
				m_restoreGamepadLockState = lockState;
				m_restoreGamepadVisible = visible;
				break;
			}
			case InputType.Keyboard:
			case InputType.Any:
				if (m_restoreGamepadLockState.HasValue)
				{
					SetLockState(m_restoreGamepadLockState.Value);
					m_restoreGamepadLockState = null;
				}
				if (m_restoreGamepadVisible.HasValue)
				{
					SetVisibility(m_restoreGamepadVisible.Value);
					m_restoreGamepadVisible = null;
				}
				break;
			}
		}

		public void OnLastInputTypeChanged(BindingSourceType bindingSourceType)
		{
			CheckInputAndUpdateCursorStates();
		}
	}
}
