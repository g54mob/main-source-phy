using System;
using InControl;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class GlyphService : ServicePrefab
{
	[SerializeField]
	private ControllerIconsScriptableObject m_xboxOneControllerIcons;

	[SerializeField]
	private ControllerIconsScriptableObject m_ps4ControllerIcons;

	[SerializeField]
	private ControllerIconsScriptableObject m_switchControllerIcons;

	[SerializeField]
	private PCIconsScriptableObject m_pcIcons;

	public string GetActionGlyph(PlayerAction action, InputType inputType, InputDeviceStyle deviceStyle = InputDeviceStyle.Unknown, bool forceText = false, GlyphServiceExtraInfo getExtraInfo = null)
	{
		switch (inputType)
		{
		case InputType.Controller:
			if (getExtraInfo != null)
			{
				getExtraInfo.BindingSourceType = BindingSourceType.DeviceBindingSource;
			}
			return GetBindingsGlyph(GetBindingSource(action, BindingSourceType.DeviceBindingSource), inputType, deviceStyle, forceText, getExtraInfo);
		case InputType.Keyboard:
		{
			MouseBindingSource mouseBindingSource = (MouseBindingSource)GetBindingSource(action, BindingSourceType.MouseBindingSource);
			if (mouseBindingSource == null)
			{
				KeyBindingSource keyBindingSource = (KeyBindingSource)GetBindingSource(action, BindingSourceType.KeyBindingSource);
				if (keyBindingSource != null)
				{
					if (getExtraInfo != null)
					{
						getExtraInfo.BindingSourceType = BindingSourceType.KeyBindingSource;
					}
					return GetBindingsGlyph(keyBindingSource, inputType, deviceStyle, forceText, getExtraInfo);
				}
				return string.Empty;
			}
			if (getExtraInfo != null)
			{
				getExtraInfo.BindingSourceType = BindingSourceType.MouseBindingSource;
			}
			return GetBindingsGlyph(mouseBindingSource, inputType, deviceStyle, forceText, getExtraInfo);
		}
		default:
			throw new ArgumentOutOfRangeException("inputType", inputType, null);
		}
	}

	public string GetBindingsGlyph(BindingSource binding, InputType inputType, InputDeviceStyle deviceStyle = InputDeviceStyle.Unknown, bool forceText = false, GlyphServiceExtraInfo getExtraInfo = null)
	{
		if (binding == null)
		{
			return string.Empty;
		}
		switch (inputType)
		{
		case InputType.Keyboard:
			switch (binding.BindingSourceType)
			{
			case BindingSourceType.KeyBindingSource:
			{
				KeyBindingSource keyBindingSource = (KeyBindingSource)binding;
				if (!forceText && m_pcIcons.TryGetValue(keyBindingSource.Control.GetInclude(0), out var index2))
				{
					if (getExtraInfo != null)
					{
						getExtraInfo.IconIndex = index2;
					}
					return $"<sprite={index2}/>";
				}
				return binding.Name;
			}
			case BindingSourceType.MouseBindingSource:
			{
				MouseBindingSource mouseBindingSource = (MouseBindingSource)binding;
				if (!forceText && m_pcIcons.TryGetValue(mouseBindingSource.Control, out var index))
				{
					if (getExtraInfo != null)
					{
						getExtraInfo.IconIndex = index;
					}
					return $"<sprite={index}/>";
				}
				return binding.Name;
			}
			}
			break;
		case InputType.Controller:
		{
			if (binding == null)
			{
				return string.Empty;
			}
			BindingSourceType bindingSourceType = binding.BindingSourceType;
			if (bindingSourceType == BindingSourceType.DeviceBindingSource)
			{
				DeviceBindingSource deviceBindingSource = (DeviceBindingSource)binding;
				int deviceGlyphSpriteIndex = GetDeviceGlyphSpriteIndex(deviceBindingSource.Control, deviceStyle);
				if (getExtraInfo != null)
				{
					getExtraInfo.IconIndex = deviceGlyphSpriteIndex;
				}
				return $"<sprite={deviceGlyphSpriteIndex}/>";
			}
			break;
		}
		}
		return string.Empty;
	}

	private bool IsJoystickDirectional(InputControlType control)
	{
		if (control == InputControlType.LeftStickDown || control == InputControlType.LeftStickLeft || control == InputControlType.LeftStickRight || control == InputControlType.LeftStickUp || control == InputControlType.RightStickDown || control == InputControlType.RightStickLeft || control == InputControlType.RightStickRight || control == InputControlType.RightStickUp)
		{
			return true;
		}
		return false;
	}

	private string GetDeviceDirectionalJoystickSpriteString(InputControlType inputControlType)
	{
		switch (inputControlType)
		{
		case InputControlType.LeftStickUp:
		case InputControlType.RightStickUp:
			return $"<rotate=270><sprite={84}/></rotate>";
		case InputControlType.LeftStickDown:
		case InputControlType.RightStickDown:
			return $"<rotate=90><sprite={84}/></rotate>";
		case InputControlType.LeftStickLeft:
		case InputControlType.RightStickLeft:
			return $"<rotate=0><sprite={84}/></rotate>";
		case InputControlType.LeftStickRight:
		case InputControlType.RightStickRight:
			return $"<rotate=180><sprite={84}/></rotate>";
		default:
			return string.Empty;
		}
	}

	private int GetDeviceGlyphSpriteIndex(InputControlType inputControlType, InputDeviceStyle deviceStyle = InputDeviceStyle.Unknown)
	{
		int value = 0;
		ControllerIconsScriptableObject controllerIcons = GetControllerIcons(deviceStyle);
		if (controllerIcons.m_controllerIcons == null)
		{
			Debug.LogError("Controller icon dictionary is null");
			return value;
		}
		if (controllerIcons.m_controllerIcons.TryGetValue(inputControlType, out value))
		{
			return value;
		}
		Debug.LogError($"Icon not found for: {inputControlType} for Device Style: {deviceStyle} in {controllerIcons.name}");
		return value;
	}

	private ControllerIconsScriptableObject GetControllerIcons(InputDeviceStyle deviceStyle)
	{
		switch (deviceStyle)
		{
		case InputDeviceStyle.PlayStation4:
			return m_ps4ControllerIcons;
		case InputDeviceStyle.NintendoSwitch:
			return m_switchControllerIcons;
		default:
			return m_xboxOneControllerIcons;
		}
	}

	private BindingSource GetBindingSource(PlayerAction action, BindingSourceType sourceType)
	{
		if (action == null)
		{
			return null;
		}
		foreach (BindingSource binding in action.Bindings)
		{
			if (binding.BindingSourceType == sourceType)
			{
				return binding;
			}
		}
		return null;
	}
}
