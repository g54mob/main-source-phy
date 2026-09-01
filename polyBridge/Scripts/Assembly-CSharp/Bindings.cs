using System.Collections.Generic;
using UnityEngine;

public class Bindings
{
	public static Dictionary<BindingType, Binding> m_Bindings = new Dictionary<BindingType, Binding>();

	public static void Init()
	{
		AddBinding(BindingType.START_SIM, "BINDING_START_SIM", KeyCode.Space, KeyCode.None, GamepadButtonType.START);
		AddBinding(BindingType.PAUSE_SIM, "BINDING_PAUSE_SIM", KeyCode.P, KeyCode.None, GamepadButtonType.WEST);
		AddBinding(BindingType.SANDBOX_BUILD_SIM_CYCLE, "BINDING_SANDBOX_BUILD_SIM_CYCLE", KeyCode.Tab, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.UNDO, "BINDING_UNDO", KeyCode.Z, KeyCode.None, GamepadButtonType.LEFTSTICK_PRESSED);
		AddBinding(BindingType.REDO, "BINDING_REDO", KeyCode.Y, KeyCode.None, GamepadButtonType.RIGHTSTICK_PRESSED);
		AddBinding(BindingType.STRESS_VIS, "BINDING_STRESS_VIS", KeyCode.T, KeyCode.None, GamepadButtonType.EAST);
		AddBinding(BindingType.GRID, "BINDING_GRID", KeyCode.G, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.LEVEL_INFO, "BINDING_LEVEL_INFO", KeyCode.I, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.SPLIT_JOINT, "BINDING_SPLIT_JOINT", KeyCode.J, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.MOVE, "BINDING_MOVE_JOINT", KeyCode.LeftShift, KeyCode.RightShift, GamepadButtonType.NONE);
		AddBinding(BindingType.INCREASE_SIM_SPEED, "BINDING_INCREASE_SIM_SPEED", KeyCode.Period, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.DECREASE_SIM_SPEED, "BINDING_DECREASE_SIM_SPEED", KeyCode.Comma, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.DELETE_SELECTION, "BINDING_DELETE_SELECTION", KeyCode.D, KeyCode.Delete, GamepadButtonType.NONE);
		AddBinding(BindingType.AUTO_TRIANGULATE, "BINDING_AUTO_TRIANGULATE", KeyCode.R, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.COPY_SELECTION, "BINDING_COPY_SELECTION", KeyCode.C, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.CUT_SELECTION, "BINDING_CUT_SELECTION", KeyCode.X, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.FLIP_HORIZONTAL, "BINDING_FLIP_HORIZONTAL", KeyCode.F, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.FLIP_VERTICAL, "BINDING_FLIP_VERTICAL", KeyCode.V, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.TOGGLE_HUD, "BINDING_TOGGLE_HUD", KeyCode.H, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.MULTI_SELECT, "BINDING_MULTI_SELECT", KeyCode.LeftControl, KeyCode.RightControl, GamepadButtonType.NONE);
		AddBinding(BindingType.DRAW_BUILD, "BINDING_DRAW_BUILD_PAN", KeyCode.Mouse0, KeyCode.None, GamepadButtonType.SOUTH);
		AddBinding(BindingType.SELECT, "BINDING_SELECT_HOLD", KeyCode.None, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.ERASE, "BINDING_ERASE_HOLD", KeyCode.W, KeyCode.None, GamepadButtonType.WEST);
		AddBinding(BindingType.SELECT_INTERRUPT, "BINDING_SELECT_INTERRUPT", KeyCode.Mouse1, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.ROTATE_SIM_CAMERA, "BINDING_ROTATE_SIM_CAMERA", KeyCode.Mouse1, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.PAN_CAMERA_UP, "BINDING_PAN_CAMERA_UP", KeyCode.UpArrow, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.PAN_CAMERA_LEFT, "BINDING_PAN_CAMERA_LEFT", KeyCode.LeftArrow, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.PAN_CAMERA_DOWN, "BINDING_PAN_CAMERA_DOWN", KeyCode.DownArrow, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.PAN_CAMERA_RIGHT, "BINDING_PAN_CAMERA_RIGHT", KeyCode.RightArrow, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.SELECT_ROAD, "BINDING_SELECT_ROAD", KeyCode.Alpha1, KeyCode.Keypad1, GamepadButtonType.NONE);
		AddBinding(BindingType.SELECT_WOOD, "BINDING_SELECT_WOOD", KeyCode.Alpha2, KeyCode.Keypad2, GamepadButtonType.NONE);
		AddBinding(BindingType.SELECT_STEEL, "BINDING_SELECT_STEEL", KeyCode.Alpha3, KeyCode.Keypad3, GamepadButtonType.NONE);
		AddBinding(BindingType.SELECT_HYDRAULICS, "BINDING_SELECT_HYDRAULICS", KeyCode.Alpha4, KeyCode.Keypad4, GamepadButtonType.NONE);
		AddBinding(BindingType.SELECT_ROPE, "BINDING_SELECT_ROPE", KeyCode.Alpha5, KeyCode.Keypad5, GamepadButtonType.NONE);
		AddBinding(BindingType.SELECT_CABLE, "BINDING_SELECT_CABLE", KeyCode.Alpha6, KeyCode.Keypad6, GamepadButtonType.NONE);
		AddBinding(BindingType.SELECT_SPRING, "BINDING_SELECT_SPRING", KeyCode.Alpha7, KeyCode.Keypad7, GamepadButtonType.NONE);
		AddBinding(BindingType.SELECT_PILLAR, "BINDING_SELECT_FOUNDATION", KeyCode.Alpha8, KeyCode.Keypad8, GamepadButtonType.NONE);
		AddBinding(BindingType.MOVE_OFF_GRID, "BINDING_MOVE_OFF_GRID", KeyCode.LeftShift, KeyCode.RightShift, GamepadButtonType.NONE);
		AddBinding(BindingType.HORIZONTAL_CONSTRAINT, "BINDING_HORIZONTAL_CONSTRAINT", KeyCode.X, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.VERTICAL_CONSTRAINT, "BINDING_VERTICAL_CONSTRAINT", KeyCode.C, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.SCREENSHOT, "BINDING_SCREENSHOT", KeyCode.F11, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.TRACE_START, "BINDING_TRACE_START", KeyCode.Alpha9, KeyCode.Keypad9, GamepadButtonType.NONE);
		AddBinding(BindingType.TRACE_FILL, "BINDING_TRACE_FILL", KeyCode.Alpha0, KeyCode.Keypad0, GamepadButtonType.NONE);
		AddBinding(BindingType.ROTATE_CLIPBOARD_LEFT, "BINDING_ROTATE_CLIPBOARD_LEFT", KeyCode.Q, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.ROTATE_CLIPBOARD_RIGHT, "BINDING_ROTATE_CLIPBOARD_RIGHT", KeyCode.E, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.TRACE_LOCK_TANGENTS, "BINDING_TRACE_LOCK_TANGENTS", KeyCode.L, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.TRACE_SHAPE, "BINDING_TRACE_SHAPE", KeyCode.K, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.TRACE_CLEAR, "BINDING_TRACE_CLEAR", KeyCode.O, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.PAN_WITH_MOUSE, "BINDING_PAN_WITH_MOUSE", KeyCode.Mouse2, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.SHOW_ALL_SPLIT_JOINT_NUMBERS, "BINDING_SHOW_ALL_SPLIT_JOINT_NUMBERS", KeyCode.A, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.SAVE, "BINDING_SAVE", KeyCode.F7, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.QUICKSAVE, "BINDING_QUICKSAVE", KeyCode.F6, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.LOAD, "BINDING_LOAD", KeyCode.F8, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.EMULATE_MOUSE1, "BINDING_EMULATE_RIGHT_CLICK", KeyCode.LeftAlt, KeyCode.RightAlt, GamepadButtonType.NONE);
		AddBinding(BindingType.ZOOM_IN, "BINDING_ZOOM_IN", KeyCode.Equals, KeyCode.KeypadPlus, GamepadButtonType.NONE);
		AddBinding(BindingType.ZOOM_OUT, "BINDING_ZOOM_OUT", KeyCode.Minus, KeyCode.KeypadMinus, GamepadButtonType.NONE);
		AddBinding(BindingType.CYCLE_SIM_VIEW, "BINDING_CYCLE_SIM_VIEW", KeyCode.V, KeyCode.None, GamepadButtonType.DPAD_LEFT);
		AddBinding(BindingType.HELP, "BINDING_HELP", KeyCode.F1, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.PAUSE_ON_BREAK, "BINDING_PAUSE_ON_BREAK", KeyCode.F10, KeyCode.Pause, GamepadButtonType.DPAD_DOWN);
		AddBinding(BindingType.EDGE_BISECT, "BINDING_EDGE_BISECT", KeyCode.None, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.NUDGE_HYDRO_UP, "BINDING_NUDGE_HYDRO_UP", KeyCode.RightBracket, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.NUDGE_HYDRO_DOWN, "BINDING_NUDGE_HYDRO_DOWN", KeyCode.LeftBracket, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.CYCLE_FOLLOW_CAR, "BINDING_CYCLE_FOLLOW_CAR", KeyCode.Tab, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.FOLLOW_CAR, "BINDING_FOLLOW_CAR", KeyCode.F, KeyCode.None, GamepadButtonType.DPAD_RIGHT);
		AddBinding(BindingType.LOCK_2D, "UI_SETTINGS_LOCK_BUILD_CAMERA", KeyCode.Slash, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.SHOW_ALL_TOOLTIPS, "BINDING_SHOW_ALL_TOOLTIPS", KeyCode.T, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.TRACE_SNAP_TANGENTS, "TRACE_GRID_BINDING", KeyCode.U, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL, "BINDING_HORIZONTAL_CONSTRAINT_UNIVERSAL", KeyCode.N, KeyCode.None, GamepadButtonType.NONE);
		AddBinding(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL, "BINDING_VERTICAL_CONSTRAINT_UNIVERSAL", KeyCode.M, KeyCode.None, GamepadButtonType.NONE);
	}

	public static void CheckForSpecialDuplicates()
	{
		BindingModeFlags bindingModeFlags = BindingModeFlags.Build | BindingModeFlags.Sandbox | BindingModeFlags.Simulation;
		RemoveBindingsIfDuplicated(BindingType.LOCK_2D, bindingModeFlags);
		RemoveBindingsIfDuplicated(BindingType.SHOW_ALL_TOOLTIPS, BindingModeFlags.Build);
		RemoveBindingsIfDuplicated(BindingType.TRACE_SNAP_TANGENTS, BindingModeFlags.Build);
		RemoveBindingsIfDuplicated(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL, bindingModeFlags);
		RemoveBindingsIfDuplicated(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL, bindingModeFlags);
	}

	public static Binding GetBinding(BindingType type)
	{
		if (!m_Bindings.ContainsKey(type))
		{
			return null;
		}
		return m_Bindings[type];
	}

	public static Binding GetBindingUsingKey(KeyCode keyCode)
	{
		foreach (KeyValuePair<BindingType, Binding> binding in m_Bindings)
		{
			if (binding.Value.m_KeyCode == keyCode)
			{
				return binding.Value;
			}
		}
		return null;
	}

	public static void ResetToDefaults()
	{
		foreach (KeyValuePair<BindingType, Binding> binding in m_Bindings)
		{
			binding.Value.m_KeyCode = binding.Value.m_KeyCodeDefault;
			binding.Value.m_AltKeyCode = binding.Value.m_AltKeyCodeDefault;
		}
	}

	private static void AddBinding(BindingType type, string displayNameLocId, KeyCode keyCode, KeyCode altKeyCode, GamepadButtonType gamepadButtonType)
	{
		Binding value = new Binding(type, displayNameLocId, keyCode, altKeyCode, gamepadButtonType);
		m_Bindings.Add(type, value);
	}

	private static void RemoveBindingsIfDuplicated(BindingType bindingType, BindingModeFlags bindingModeFlags)
	{
		Binding binding = GetBinding(bindingType);
		if (binding == null)
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		foreach (KeyValuePair<BindingType, Binding> binding2 in m_Bindings)
		{
			if (binding2.Key == bindingType)
			{
				continue;
			}
			BindingSlot bindingSlotForBinding = GameUI.m_Instance.m_Settings.m_ControlsPanel.GetBindingSlotForBinding(binding2.Key);
			if (bindingSlotForBinding != null && bindingSlotForBinding.ModeOverlaps(bindingModeFlags))
			{
				if (binding.m_KeyCode != KeyCode.None && (binding.m_KeyCode == binding2.Value.m_KeyCode || binding.m_KeyCode == binding2.Value.m_AltKeyCode))
				{
					flag = true;
				}
				if (binding.m_AltKeyCode != KeyCode.None && (binding.m_AltKeyCode == binding2.Value.m_KeyCode || binding.m_AltKeyCode == binding2.Value.m_AltKeyCode))
				{
					flag2 = true;
				}
			}
		}
		if (flag)
		{
			m_Bindings[bindingType].m_KeyCode = KeyCode.None;
		}
		if (flag2)
		{
			m_Bindings[bindingType].m_AltKeyCode = KeyCode.None;
		}
	}

	private static void RemoveBindingsIfSpecificDuplicated(BindingType bindingType, BindingType otherBindingType)
	{
		Binding binding = GetBinding(bindingType);
		Binding binding2 = GetBinding(otherBindingType);
		if (binding != null && binding2 != null)
		{
			if (binding.m_KeyCode == binding2.m_KeyCode || binding.m_KeyCode == binding2.m_AltKeyCode)
			{
				m_Bindings[bindingType].m_KeyCode = KeyCode.None;
			}
			if (binding.m_AltKeyCode == binding2.m_KeyCode || binding.m_AltKeyCode == binding2.m_AltKeyCode)
			{
				m_Bindings[bindingType].m_AltKeyCode = KeyCode.None;
			}
		}
	}
}
