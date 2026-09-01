using UnityEngine;

public class Binding
{
	public BindingType m_BindingType;

	public string m_DisplayNameLocId;

	public KeyCode m_KeyCode;

	public KeyCode m_KeyCodeDefault;

	public KeyCode m_AltKeyCode;

	public KeyCode m_AltKeyCodeDefault;

	public GamepadButtonType m_GamepadButtonType;

	public Binding(BindingType type, string displayNameLocId, KeyCode keyCodeDefault, KeyCode altKeyCodeDefault, GamepadButtonType gamepadButtonTypeDefault)
	{
		m_BindingType = type;
		m_DisplayNameLocId = displayNameLocId;
		m_KeyCode = keyCodeDefault;
		m_KeyCodeDefault = keyCodeDefault;
		m_AltKeyCode = altKeyCodeDefault;
		m_AltKeyCodeDefault = altKeyCodeDefault;
		m_GamepadButtonType = gamepadButtonTypeDefault;
	}

	public string GetKeyBindingString()
	{
		return FormatKeyCodeForDisplay(m_KeyCode);
	}

	public string GetAltKeyBindingString()
	{
		return FormatKeyCodeForDisplay(m_AltKeyCode);
	}

	public bool Contains(KeyCode keycode)
	{
		if (m_KeyCode != keycode)
		{
			return m_AltKeyCode == keycode;
		}
		return true;
	}

	public bool IsUnBound()
	{
		if (m_KeyCode == KeyCode.None)
		{
			return m_AltKeyCode == KeyCode.None;
		}
		return false;
	}

	public string GetTooltipBindingString()
	{
		string result = string.Empty;
		if (m_KeyCode != KeyCode.None && m_AltKeyCode != KeyCode.None && !OnlyShowPrimaryBinding())
		{
			result = string.Format(Localize.Get("UI_TWO_BINDINGS"), GetKeyBindingString(), GetAltKeyBindingString());
		}
		else if (m_KeyCode != KeyCode.None)
		{
			result = ((!OnlyShowPrimaryBinding()) ? GetKeyBindingString() : ((m_KeyCode != KeyCode.LeftShift && m_AltKeyCode != KeyCode.LeftShift) ? ((m_KeyCode != KeyCode.LeftControl && m_AltKeyCode != KeyCode.LeftControl) ? ((m_KeyCode != KeyCode.LeftAlt && m_AltKeyCode != KeyCode.LeftAlt) ? GetKeyBindingString() : "Alt") : "Ctrl") : "Shift"));
		}
		else if (m_AltKeyCode != KeyCode.None)
		{
			result = GetAltKeyBindingString();
		}
		return result;
	}

	public string GetTipsBindingString()
	{
		string text = $"'{GetKeyBindingString()}'";
		string text2 = $"'{GetAltKeyBindingString()}'";
		if (m_KeyCode != KeyCode.None && m_AltKeyCode != KeyCode.None)
		{
			return string.Format(Localize.Get("UI_TWO_BINDINGS"), text, text2);
		}
		if (m_KeyCode != KeyCode.None)
		{
			return text;
		}
		if (m_AltKeyCode != KeyCode.None)
		{
			return text2;
		}
		return Localize.Get("UI_UNBOUND");
	}

	public void SetBindingKeyCode(KeyCode keycode)
	{
		m_KeyCode = keycode;
	}

	public void SetBindingAltKeyCode(KeyCode keyCode)
	{
		m_AltKeyCode = keyCode;
	}

	public void SetDefaultBindings()
	{
		m_KeyCode = m_KeyCodeDefault;
		m_AltKeyCode = m_AltKeyCodeDefault;
	}

	public void ClearKeyBinding()
	{
		m_KeyCode = KeyCode.None;
	}

	public void ClearAltKeyBinding()
	{
		m_AltKeyCode = KeyCode.None;
	}

	public void ClearBindings()
	{
		ClearKeyBinding();
		ClearAltKeyBinding();
	}

	public bool JustPressed()
	{
		KeyCode modifiedKeyCode = GetModifiedKeyCode();
		KeyCode modifiedAltKeyCode = GetModifiedAltKeyCode();
		if (modifiedKeyCode != KeyCode.None && Input.GetKeyDown(modifiedKeyCode))
		{
			if (!GameInput.IsMouseButton(modifiedKeyCode) || !GameUI.IsPointerOverGameObject())
			{
				return true;
			}
			return false;
		}
		if (modifiedAltKeyCode != KeyCode.None && Input.GetKeyDown(modifiedAltKeyCode))
		{
			if (!GameInput.IsMouseButton(modifiedAltKeyCode) || !GameUI.IsPointerOverGameObject())
			{
				return true;
			}
			return false;
		}
		if (m_GamepadButtonType != GamepadButtonType.NONE && GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return GamepadManager.ButtonJustPressed(m_GamepadButtonType);
		}
		return false;
	}

	public bool JustPressedRaw()
	{
		KeyCode modifiedKeyCode = GetModifiedKeyCode();
		KeyCode modifiedAltKeyCode = GetModifiedAltKeyCode();
		if (modifiedKeyCode != KeyCode.None && Input.GetKeyDown(modifiedKeyCode))
		{
			return true;
		}
		if (modifiedAltKeyCode != KeyCode.None && Input.GetKeyDown(modifiedAltKeyCode))
		{
			return true;
		}
		if (m_GamepadButtonType != GamepadButtonType.NONE && GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return GamepadManager.ButtonJustPressed(m_GamepadButtonType);
		}
		return false;
	}

	public bool JustReleased()
	{
		KeyCode modifiedKeyCode = GetModifiedKeyCode();
		KeyCode modifiedAltKeyCode = GetModifiedAltKeyCode();
		if (modifiedKeyCode != KeyCode.None && Input.GetKeyUp(modifiedKeyCode))
		{
			return true;
		}
		if (modifiedAltKeyCode != KeyCode.None && Input.GetKeyUp(modifiedAltKeyCode))
		{
			return true;
		}
		if (m_GamepadButtonType != GamepadButtonType.NONE && GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return GamepadManager.ButtonJustReleased(m_GamepadButtonType);
		}
		return false;
	}

	public bool IsDown()
	{
		KeyCode modifiedKeyCode = GetModifiedKeyCode();
		KeyCode modifiedAltKeyCode = GetModifiedAltKeyCode();
		if (modifiedKeyCode != KeyCode.None && Input.GetKey(modifiedKeyCode))
		{
			return true;
		}
		if (modifiedAltKeyCode != KeyCode.None && Input.GetKey(modifiedAltKeyCode))
		{
			return true;
		}
		if (m_GamepadButtonType != GamepadButtonType.NONE && GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return GamepadManager.ButtonIsDown(m_GamepadButtonType);
		}
		return false;
	}

	private KeyCode GetModifiedKeyCode()
	{
		bool flag = GameInput.EmulateRightClickIsDown();
		if (!flag)
		{
			return m_KeyCode;
		}
		if (flag && m_KeyCode == KeyCode.Mouse1)
		{
			return KeyCode.Mouse0;
		}
		if (flag && m_KeyCode == KeyCode.Mouse0)
		{
			return KeyCode.None;
		}
		return m_KeyCode;
	}

	private KeyCode GetModifiedAltKeyCode()
	{
		bool flag = GameInput.EmulateRightClickIsDown();
		if (!flag)
		{
			return m_AltKeyCode;
		}
		if (flag && m_AltKeyCode == KeyCode.Mouse1)
		{
			return KeyCode.Mouse0;
		}
		if (flag && m_AltKeyCode == KeyCode.Mouse0)
		{
			return KeyCode.None;
		}
		return m_AltKeyCode;
	}

	private string FormatKeyCodeForDisplay(KeyCode keyCode)
	{
		string text = keyCode.ToString();
		if (text.Contains("Alpha"))
		{
			return keyCode.ToString().Replace("Alpha", string.Empty);
		}
		switch (keyCode)
		{
		case KeyCode.None:
			return Localize.Get("KEY_NONE");
		case KeyCode.Backspace:
			return Localize.Get("KEY_BACKSPACE");
		case KeyCode.Tab:
			return Localize.Get("KEY_TAB");
		case KeyCode.Clear:
			return Localize.Get("KEY_CLEAR");
		case KeyCode.Return:
			return Localize.Get("KEY_RETURN");
		case KeyCode.Pause:
			return Localize.Get("KEY_PAUSE");
		case KeyCode.Escape:
			return Localize.Get("KEY_ESCAPE");
		case KeyCode.Space:
			return Localize.Get("KEY_SPACE");
		case KeyCode.Quote:
			return Localize.Get("KEY_QUOTE");
		case KeyCode.Comma:
			return Localize.Get("KEY_COMMA");
		case KeyCode.Period:
			return Localize.Get("KEY_PERIOD");
		case KeyCode.Slash:
			return Localize.Get("KEY_SLASH");
		case KeyCode.Semicolon:
			return Localize.Get("KEY_SEMICOLON");
		case KeyCode.Backslash:
			return Localize.Get("KEY_BACKSLASH");
		case KeyCode.Minus:
			return Localize.Get("KEY_MINUS");
		case KeyCode.Equals:
			return Localize.Get("KEY_EQUALS");
		case KeyCode.BackQuote:
			return Localize.Get("KEY_BACKQUOTE");
		case KeyCode.Delete:
			return Localize.Get("KEY_DELETE");
		case KeyCode.KeypadPeriod:
			return Localize.Get("KEY_KEYPAD_PERIOD");
		case KeyCode.KeypadDivide:
			return Localize.Get("KEY_KEYPAD_DIVIDE");
		case KeyCode.KeypadMultiply:
			return Localize.Get("KEY_KEYPAD_MULTIPLY");
		case KeyCode.KeypadMinus:
			return Localize.Get("KEY_KEYPAD_MINUS");
		case KeyCode.KeypadPlus:
			return Localize.Get("KEY_KEYPAD_PLUS");
		case KeyCode.KeypadEnter:
			return Localize.Get("KEY_KEYPAD_ENTER");
		case KeyCode.KeypadEquals:
			return Localize.Get("KEY_KEYPAD_EQUALS");
		case KeyCode.Insert:
			return Localize.Get("KEY_INSERT");
		case KeyCode.Home:
			return Localize.Get("KEY_HOME");
		case KeyCode.End:
			return Localize.Get("KEY_END");
		case KeyCode.Numlock:
			return Localize.Get("KEY_NUMLOCK");
		case KeyCode.CapsLock:
			return Localize.Get("KEY_CAPSLOCK");
		case KeyCode.ScrollLock:
			return Localize.Get("KEY_SCROLL_LOCK");
		case KeyCode.Print:
			return Localize.Get("KEY_PRINT");
		case KeyCode.SysReq:
			return Localize.Get("KEY_SYSREQ");
		case KeyCode.Break:
			return Localize.Get("KEY_BREAK");
		case KeyCode.Menu:
			return Localize.Get("KEY_MENU");
		case KeyCode.UpArrow:
			return Localize.Get("KEY_UP_ARROW");
		case KeyCode.DownArrow:
			return Localize.Get("KEY_DOWN_ARROW");
		case KeyCode.LeftArrow:
			return Localize.Get("KEY_LEFT_ARROW");
		case KeyCode.RightArrow:
			return Localize.Get("KEY_RIGHT_ARROW");
		case KeyCode.LeftShift:
			return Localize.Get("KEY_LEFT_SHIFT");
		case KeyCode.RightShift:
			return Localize.Get("KEY_RIGHT_SHIFT");
		case KeyCode.LeftControl:
			return Localize.Get("KEY_LEFT_CONTROL");
		case KeyCode.RightControl:
			return Localize.Get("KEY_RIGHT_CONTROL");
		case KeyCode.LeftAlt:
			return Localize.Get("KEY_LEFT_ALT");
		case KeyCode.RightAlt:
			return Localize.Get("KEY_RIGHT_ALT");
		case KeyCode.LeftBracket:
			return Localize.Get("KEY_LEFT_BRACKET");
		case KeyCode.RightBracket:
			return Localize.Get("KEY_RIGHT_BRACKET");
		case KeyCode.LeftMeta:
			return Localize.Get("KEY_LEFT_COMMAND");
		case KeyCode.RightMeta:
			return Localize.Get("KEY_RIGHT_COMMAND");
		case KeyCode.LeftWindows:
			return Localize.Get("KEY_LEFT_WINDOWS");
		case KeyCode.RightWindows:
			return Localize.Get("KEY_RIGHT_WINDOWS");
		case KeyCode.PageDown:
			return Localize.Get("KEY_PAGE_DOWN");
		case KeyCode.PageUp:
			return Localize.Get("KEY_PAGE_UP");
		case KeyCode.Keypad0:
			return string.Format(Localize.Get("KEY_KEYPAD_NUMBER"), (int)(keyCode - 256));
		case KeyCode.Mouse0:
			return "<sprite name=Tooltip_MouseLeft>";
		case KeyCode.Mouse1:
			return "<sprite name=Tooltip_MouseRight>";
		case KeyCode.Mouse2:
			return "<sprite name=Tooltip_MouseMiddle>";
		case KeyCode.Mouse3:
		case KeyCode.Mouse4:
		case KeyCode.Mouse5:
		case KeyCode.Mouse6:
			return string.Format(Localize.Get("KEY_MOUSE_NUMBER"), (int)(keyCode - 323 + 1));
		default:
			return text;
		}
	}

	private bool OnlyShowPrimaryBinding()
	{
		if (m_KeyCode == KeyCode.LeftShift && m_AltKeyCode == KeyCode.RightShift)
		{
			return true;
		}
		if (m_KeyCode == KeyCode.RightShift && m_AltKeyCode == KeyCode.LeftShift)
		{
			return true;
		}
		if (m_KeyCode == KeyCode.LeftControl && m_AltKeyCode == KeyCode.RightControl)
		{
			return true;
		}
		if (m_KeyCode == KeyCode.RightControl && m_AltKeyCode == KeyCode.LeftControl)
		{
			return true;
		}
		if (m_KeyCode == KeyCode.LeftAlt && m_AltKeyCode == KeyCode.RightAlt)
		{
			return true;
		}
		if (m_KeyCode == KeyCode.RightAlt && m_AltKeyCode == KeyCode.LeftAlt)
		{
			return true;
		}
		return false;
	}
}
