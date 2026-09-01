using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxNudge : MonoBehaviour
{
	[Header("Nudge")]
	public TMP_InputField m_InputFieldNudge;

	public Button m_GamepadInputFieldButton;

	public GameObject m_NudgeZButtons;

	[Header("Buttons")]
	public Button m_NudgeUpButton;

	public Button m_NudgeDownButton;

	public Button m_NudgeLeftButton;

	public Button m_NudgeRightButton;

	public Button m_NudgeForwardButton;

	public Button m_NudgeBackButton;

	public static float MIN_INCREMENT = 0.001f;

	public static float MAX_INCREMENT = 200f;

	private float m_ContinuousHoldTime;

	private float m_NextTickTime;

	private bool m_ContinuousHoldActive;

	private bool m_DoSnapShotWhenContinuousHoldOff;

	private void Start()
	{
		m_NudgeUpButton.onClick.AddListener(OnNudgeUp);
		m_NudgeDownButton.onClick.AddListener(OnNudgeDown);
		m_NudgeLeftButton.onClick.AddListener(OnNudgeLeft);
		m_NudgeRightButton.onClick.AddListener(OnNudgeRight);
		m_NudgeForwardButton.onClick.AddListener(OnNudgeForward);
		m_NudgeBackButton.onClick.AddListener(OnNudgeBack);
		m_GamepadInputFieldButton.onClick.AddListener(OnGamepadInputField);
	}

	private void OnEnable()
	{
		RefreshInputField();
	}

	private void OnDisable()
	{
		m_ContinuousHoldActive = false;
	}

	private void Update()
	{
		UpdateContinuousHold();
		ProcessInput();
	}

	public void EnableNudgeZ(bool enable)
	{
		m_NudgeZButtons.SetActive(enable);
	}

	public bool InputFieldHasFocus()
	{
		if (m_InputFieldNudge.gameObject.activeInHierarchy)
		{
			return m_InputFieldNudge.isFocused;
		}
		return false;
	}

	public void SetNudgeIncrement(float increment)
	{
		SandboxSettings.m_MultiSelectMovementIncrement = increment;
		RefreshInputField();
	}

	public void UpdateForCurrentDevice()
	{
		m_InputFieldNudge.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_GamepadInputFieldButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
		m_ContinuousHoldActive = false;
	}

	private void ProcessInput()
	{
		if (ActivePanels.None())
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_UP))
			{
				SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(NudgeDirection.UP, GameGrid.m_Spacing), GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_UP));
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_DOWN))
			{
				SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(NudgeDirection.DOWN, GameGrid.m_Spacing), GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_DOWN));
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_LEFT) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_LEFT))
			{
				SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(NudgeDirection.LEFT, GameGrid.m_Spacing), GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_LEFT));
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_RIGHT))
			{
				SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(NudgeDirection.RIGHT, GameGrid.m_Spacing), GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_RIGHT));
			}
		}
		if (!SandboxSelectionSet.IsEmpty() && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			MaybeNudgeWithKeyPress(Bindings.GetBinding(BindingType.PAN_CAMERA_UP).m_KeyCode, NudgeDirection.UP);
			MaybeNudgeWithKeyPress(Bindings.GetBinding(BindingType.PAN_CAMERA_DOWN).m_KeyCode, NudgeDirection.DOWN);
			MaybeNudgeWithKeyPress(Bindings.GetBinding(BindingType.PAN_CAMERA_LEFT).m_KeyCode, NudgeDirection.LEFT);
			MaybeNudgeWithKeyPress(Bindings.GetBinding(BindingType.PAN_CAMERA_RIGHT).m_KeyCode, NudgeDirection.RIGHT);
			MaybeNudgeWithKeyPress(Bindings.GetBinding(BindingType.PAN_CAMERA_UP).m_AltKeyCode, NudgeDirection.UP);
			MaybeNudgeWithKeyPress(Bindings.GetBinding(BindingType.PAN_CAMERA_DOWN).m_AltKeyCode, NudgeDirection.DOWN);
			MaybeNudgeWithKeyPress(Bindings.GetBinding(BindingType.PAN_CAMERA_LEFT).m_AltKeyCode, NudgeDirection.LEFT);
			MaybeNudgeWithKeyPress(Bindings.GetBinding(BindingType.PAN_CAMERA_RIGHT).m_AltKeyCode, NudgeDirection.RIGHT);
		}
	}

	private static void MaybeNudgeWithKeyPress(KeyCode keycode, NudgeDirection nudgeDirection)
	{
		if (keycode != KeyCode.None && (Input.GetKeyDown(keycode) || KeyboardRepeater.JustRepeated(keycode)))
		{
			SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(nudgeDirection, GameGrid.m_Spacing), KeyboardRepeater.JustRepeated(keycode));
			InterfaceAudio.Play("ui_menu_hover");
		}
	}

	private void OnNudgeUp()
	{
		if (m_ContinuousHoldActive)
		{
			m_ContinuousHoldActive = false;
			if (m_ContinuousHoldTime > GameUI.KEY_REPEAT_START_DELAY_SECONDS)
			{
				return;
			}
		}
		SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(NudgeDirection.UP, SandboxSettings.m_MultiSelectMovementIncrement), GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_UP));
		InterfaceAudio.Play("ui_menu_hover");
	}

	private void OnNudgeDown()
	{
		if (m_ContinuousHoldActive)
		{
			m_ContinuousHoldActive = false;
			if (m_ContinuousHoldTime > GameUI.KEY_REPEAT_START_DELAY_SECONDS)
			{
				return;
			}
		}
		SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(NudgeDirection.DOWN, SandboxSettings.m_MultiSelectMovementIncrement), GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_DOWN));
		InterfaceAudio.Play("ui_menu_hover");
	}

	private void OnNudgeLeft()
	{
		if (m_ContinuousHoldActive)
		{
			m_ContinuousHoldActive = false;
			if (m_ContinuousHoldTime > GameUI.KEY_REPEAT_START_DELAY_SECONDS)
			{
				return;
			}
		}
		SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(NudgeDirection.LEFT, SandboxSettings.m_MultiSelectMovementIncrement), GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_LEFT));
		InterfaceAudio.Play("ui_menu_hover");
	}

	private void OnNudgeRight()
	{
		if (m_ContinuousHoldActive)
		{
			m_ContinuousHoldActive = false;
			if (m_ContinuousHoldTime > GameUI.KEY_REPEAT_START_DELAY_SECONDS)
			{
				return;
			}
		}
		SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(NudgeDirection.RIGHT, SandboxSettings.m_MultiSelectMovementIncrement), GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_RIGHT));
		InterfaceAudio.Play("ui_menu_hover");
	}

	private void OnNudgeForward()
	{
		if (m_ContinuousHoldActive)
		{
			m_ContinuousHoldActive = false;
			if (m_ContinuousHoldTime > GameUI.KEY_REPEAT_START_DELAY_SECONDS)
			{
				return;
			}
		}
		SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(NudgeDirection.FORWARD, SandboxSettings.m_MultiSelectMovementIncrement), GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_RIGHT));
		InterfaceAudio.Play("ui_menu_hover");
	}

	private void OnNudgeBack()
	{
		if (m_ContinuousHoldActive)
		{
			m_ContinuousHoldActive = false;
			if (m_ContinuousHoldTime > GameUI.KEY_REPEAT_START_DELAY_SECONDS)
			{
				return;
			}
		}
		SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(NudgeDirection.BACK, SandboxSettings.m_MultiSelectMovementIncrement), GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_RIGHT));
		InterfaceAudio.Play("ui_menu_hover");
	}

	private void OnGamepadInputField()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_InputFieldNudge.text, m_InputFieldNudge.characterLimit, Localize.Get("UI_SANDBOX_SET_MOVEMENT_STEP"), multiline: false, OnIncrementEntered);
	}

	private void UpdateContinuousHold()
	{
		if (GameInput.GetMouseButtonJustReleased(0) && m_ContinuousHoldActive)
		{
			m_ContinuousHoldActive = false;
			if (m_DoSnapShotWhenContinuousHoldOff)
			{
				m_DoSnapShotWhenContinuousHoldOff = false;
				SandboxUndo.SnapShot();
			}
		}
		if (GameInput.GetMouseButtonJustPressed(0) && !m_ContinuousHoldActive)
		{
			m_ContinuousHoldActive = true;
			m_ContinuousHoldTime = 0f;
			m_NextTickTime = 0f;
		}
		if (!m_ContinuousHoldActive || SandboxSelectionSet.SelectionFollowsMouse())
		{
			return;
		}
		NudgeDirection nudgeDirection = NudgeDirection.NONE;
		if (m_NudgeUpButton.GetComponent<PointerEvents>().m_IsHovering)
		{
			nudgeDirection = NudgeDirection.UP;
		}
		else if (m_NudgeDownButton.GetComponent<PointerEvents>().m_IsHovering)
		{
			nudgeDirection = NudgeDirection.DOWN;
		}
		else if (m_NudgeLeftButton.GetComponent<PointerEvents>().m_IsHovering)
		{
			nudgeDirection = NudgeDirection.LEFT;
		}
		else if (m_NudgeRightButton.GetComponent<PointerEvents>().m_IsHovering)
		{
			nudgeDirection = NudgeDirection.RIGHT;
		}
		else if (m_NudgeForwardButton.GetComponent<PointerEvents>().m_IsHovering)
		{
			nudgeDirection = NudgeDirection.FORWARD;
		}
		else if (m_NudgeBackButton.GetComponent<PointerEvents>().m_IsHovering)
		{
			nudgeDirection = NudgeDirection.BACK;
		}
		if (nudgeDirection != NudgeDirection.NONE)
		{
			m_ContinuousHoldTime += Time.unscaledDeltaTime;
			if (m_ContinuousHoldTime > GameUI.KEY_REPEAT_START_DELAY_SECONDS - GameUI.KEY_REPEAT_INTERVAL_SECONDS)
			{
				m_NextTickTime += Time.unscaledDeltaTime;
				if (m_NextTickTime > GameUI.KEY_REPEAT_INTERVAL_SECONDS)
				{
					SandboxSelectionSet.DoNudge(SandboxSelectionSet.GetNudgeVector(nudgeDirection, SandboxSettings.m_MultiSelectMovementIncrement), m_ContinuousHoldActive);
					InterfaceAudio.Play("ui_menu_hover");
					m_NextTickTime = 0f;
					m_DoSnapShotWhenContinuousHoldOff = true;
				}
			}
		}
		else
		{
			m_ContinuousHoldTime = 0f;
			m_NextTickTime = 0f;
		}
	}

	private void OnIncrementEntered(string text)
	{
		if (!string.IsNullOrEmpty(text) && float.TryParse(text, out var result))
		{
			SandboxSettings.m_MultiSelectMovementIncrement = result;
			RefreshInputField();
		}
	}

	private void RefreshInputField()
	{
		m_InputFieldNudge.text = Utils.FormatThreeDecimalPlaces(SandboxSettings.m_MultiSelectMovementIncrement);
	}
}
