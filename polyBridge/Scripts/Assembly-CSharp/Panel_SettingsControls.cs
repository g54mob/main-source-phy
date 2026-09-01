using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SettingsControls : MonoBehaviour
{
	public GameObject m_Content;

	public GameObject m_BindingHeaderPrefab;

	public GameObject m_BindingHeaderSlimPrefab;

	public GameObject m_BindingSlotPrefab;

	[Header("Footer")]
	public Button m_ResetToDefaults;

	private List<BindingSlot> m_Slots = new List<BindingSlot>();

	public void Start()
	{
		m_ResetToDefaults.onClick.AddListener(OnResetToDefaults);
	}

	public void Init()
	{
		BindingModeFlags flags = BindingModeFlags.Build | BindingModeFlags.Sandbox | BindingModeFlags.Simulation;
		AddHeader("UI_CONTROLS_CATEGORY_DRAWING");
		AddSlot(BindingType.DRAW_BUILD, BindingModeFlags.Build);
		AddSlot(BindingType.SELECT_INTERRUPT, BindingModeFlags.Build);
		AddSlot(BindingType.MOVE, BindingModeFlags.Build);
		AddSlot(BindingType.ERASE, BindingModeFlags.Build);
		AddHeader("UI_CONTROLS_CATEGORY_CAMERA");
		AddSlot(BindingType.PAN_WITH_MOUSE, flags);
		AddSlot(BindingType.ROTATE_SIM_CAMERA, BindingModeFlags.Simulation);
		AddSlot(BindingType.CYCLE_SIM_VIEW, BindingModeFlags.Simulation);
		AddSlot(BindingType.LOCK_2D, flags);
		AddSlot(BindingType.PAN_CAMERA_UP, flags);
		AddSlot(BindingType.PAN_CAMERA_DOWN, flags);
		AddSlot(BindingType.PAN_CAMERA_LEFT, flags);
		AddSlot(BindingType.PAN_CAMERA_RIGHT, flags);
		AddSlot(BindingType.ZOOM_IN, flags);
		AddSlot(BindingType.ZOOM_OUT, flags);
		AddHeader("UI_CONTROLS_CATEGORY_MATERIALS");
		AddSlot(BindingType.SELECT_ROAD, BindingModeFlags.Build);
		AddSlot(BindingType.SELECT_WOOD, BindingModeFlags.Build);
		AddSlot(BindingType.SELECT_STEEL, BindingModeFlags.Build);
		AddSlot(BindingType.SELECT_HYDRAULICS, BindingModeFlags.Build);
		AddSlot(BindingType.SELECT_ROPE, BindingModeFlags.Build);
		AddSlot(BindingType.SELECT_CABLE, BindingModeFlags.Build);
		AddSlot(BindingType.SELECT_SPRING, BindingModeFlags.Build);
		AddSlot(BindingType.SELECT_PILLAR, BindingModeFlags.Build);
		AddHeader("UI_CONTROLS_CATEGORY_SELECTION");
		AddSlot(BindingType.DELETE_SELECTION, flags);
		AddSlot(BindingType.MOVE_OFF_GRID, BindingModeFlags.Sandbox);
		AddSlot(BindingType.MULTI_SELECT, BindingModeFlags.Build | BindingModeFlags.Sandbox);
		AddSlot(BindingType.EMULATE_MOUSE1, flags);
		AddHeader("UI_CONTROLS_CATEGORY_COPYPASTE");
		AddSlot(BindingType.COPY_SELECTION, BindingModeFlags.Build);
		AddSlot(BindingType.CUT_SELECTION, BindingModeFlags.Build);
		AddSlot(BindingType.ROTATE_CLIPBOARD_LEFT, BindingModeFlags.Build);
		AddSlot(BindingType.ROTATE_CLIPBOARD_RIGHT, BindingModeFlags.Build);
		AddSlot(BindingType.FLIP_HORIZONTAL, BindingModeFlags.Build | BindingModeFlags.Sandbox);
		AddSlot(BindingType.FLIP_VERTICAL, BindingModeFlags.Build | BindingModeFlags.Sandbox);
		AddHeader("UI_CONTROLS_CATEGORY_TRACE");
		AddSlot(BindingType.TRACE_START, BindingModeFlags.Build);
		AddSlot(BindingType.TRACE_CLEAR, BindingModeFlags.Build);
		AddSlot(BindingType.TRACE_FILL, BindingModeFlags.Build);
		AddSlot(BindingType.TRACE_LOCK_TANGENTS, BindingModeFlags.Build);
		AddSlot(BindingType.TRACE_SNAP_TANGENTS, BindingModeFlags.Build);
		AddSlot(BindingType.TRACE_SHAPE, BindingModeFlags.Build);
		AddHeader("UI_CONTROLS_CATEGORY_SIMULATION");
		AddSlot(BindingType.START_SIM, flags);
		AddSlot(BindingType.INCREASE_SIM_SPEED, flags);
		AddSlot(BindingType.DECREASE_SIM_SPEED, flags);
		AddSlot(BindingType.PAUSE_SIM, BindingModeFlags.Simulation);
		AddSlot(BindingType.STRESS_VIS, BindingModeFlags.Simulation);
		AddSlot(BindingType.PAUSE_ON_BREAK, flags);
		AddSlot(BindingType.FOLLOW_CAR, BindingModeFlags.Simulation);
		AddSlot(BindingType.CYCLE_FOLLOW_CAR, BindingModeFlags.Simulation);
		AddHeader("UI_CONTROLS_CATEGORY_TOOLS");
		AddSlot(BindingType.UNDO, BindingModeFlags.Build | BindingModeFlags.Sandbox);
		AddSlot(BindingType.REDO, BindingModeFlags.Build | BindingModeFlags.Sandbox);
		AddSlot(BindingType.AUTO_TRIANGULATE, BindingModeFlags.Build);
		AddSlot(BindingType.GRID, flags);
		AddSlot(BindingType.NUDGE_HYDRO_UP, BindingModeFlags.Build);
		AddSlot(BindingType.NUDGE_HYDRO_DOWN, BindingModeFlags.Build);
		AddSlot(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL, flags);
		AddSlot(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL, flags);
		AddSlot(BindingType.EDGE_BISECT, BindingModeFlags.Build);
		AddHeader("UI_CONTROLS_CATEGORY_SPLITJOINTS");
		AddSlot(BindingType.SPLIT_JOINT, BindingModeFlags.Build);
		AddSlot(BindingType.SHOW_ALL_SPLIT_JOINT_NUMBERS, BindingModeFlags.Build);
		AddHeader("UI_CONTROLS_CATEGORY_SAVELOAD");
		AddSlot(BindingType.SAVE, BindingModeFlags.Build | BindingModeFlags.Sandbox);
		AddSlot(BindingType.LOAD, BindingModeFlags.Build | BindingModeFlags.Sandbox);
		AddSlot(BindingType.QUICKSAVE, BindingModeFlags.Sandbox);
		AddHeader("UI_CONTROLS_CATEGORY_INFO");
		AddSlot(BindingType.LEVEL_INFO, flags);
		AddSlot(BindingType.HELP, BindingModeFlags.Build);
		AddSlot(BindingType.SHOW_ALL_TOOLTIPS, BindingModeFlags.Build);
		AddHeader("UI_CONTROLS_CATEGORY_OTHER");
		AddSlot(BindingType.SANDBOX_BUILD_SIM_CYCLE, BindingModeFlags.Build | BindingModeFlags.Sandbox);
		AddSlot(BindingType.SCREENSHOT, flags);
		AddSlot(BindingType.TOGGLE_HUD, flags);
	}

	public void OnEnableManual()
	{
		InitSlots();
	}

	public void OnDisableManual()
	{
	}

	public void Apply()
	{
		foreach (BindingSlot slot in m_Slots)
		{
			ApplyShadowBinding(slot.m_ShadowBinding);
		}
	}

	public List<BindingSlot> GetConflictingBindingSlots(BindingSlot source, KeyCode keyCode, PopUpBindingType bindingType)
	{
		List<BindingSlot> list = new List<BindingSlot>();
		foreach (BindingSlot slot in m_Slots)
		{
			if (ConflictExistsBetweenSlots(source, slot, bindingType, keyCode))
			{
				list.Add(slot);
			}
		}
		return list;
	}

	public bool ConflictExistsBetweenSlots(BindingSlot source, BindingSlot compare, PopUpBindingType bindingType, KeyCode keyCode)
	{
		if (source != compare)
		{
			if (compare != source && compare.ModeOverlaps(source.m_ModeFlags))
			{
				return compare.m_ShadowBinding.Contains(keyCode);
			}
			return false;
		}
		if (bindingType == PopUpBindingType.PRIMARY && source.m_ShadowBinding.m_AltKeyCode == keyCode)
		{
			return true;
		}
		if (bindingType == PopUpBindingType.ALT && source.m_ShadowBinding.m_KeyCode == keyCode)
		{
			return true;
		}
		return false;
	}

	public void RefreshSlotDisplayNames(Binding binding)
	{
		foreach (BindingSlot slot in m_Slots)
		{
			if (slot.m_ShadowBinding == binding)
			{
				slot.UpdateBindingDisplayNames();
			}
		}
	}

	public void OnResetToDefaults()
	{
		InterfaceAudio.Play("ui_settings_reset");
		PopUpMessage.DisplayConfirmation(Localize.Get("POPUP_RESET_CONTROLS"), useYesNoLabels: true, ConfirmOnDefaults);
	}

	public BindingSlot GetBindingSlotForBinding(BindingType bindingType)
	{
		foreach (BindingSlot slot in m_Slots)
		{
			if (slot.m_ShadowBinding.m_BindingType == bindingType)
			{
				return slot;
			}
		}
		return null;
	}

	private BindingSlot AddSlot(BindingType bindingType, BindingModeFlags flags)
	{
		GameObject gameObject = Object.Instantiate(m_BindingSlotPrefab, m_Content.transform);
		if (!gameObject)
		{
			return null;
		}
		BindingSlot component = gameObject.GetComponent<BindingSlot>();
		if ((bool)component)
		{
			component.SetBinding(bindingType);
			component.m_ModeFlags = flags;
			m_Slots.Add(component);
			component.m_Background.color = ((m_Slots.IndexOf(component) % 2 == 0) ? GameUI.m_Instance.m_Settings.m_RowEvenColor : GameUI.m_Instance.m_Settings.m_RowOddColor);
		}
		return component;
	}

	private void AddSlotReadOnly(BindingType bindingType, BindingModeFlags flags)
	{
		BindingSlot bindingSlot = AddSlot(bindingType, flags);
		if ((bool)bindingSlot)
		{
			Color color = new Color(bindingSlot.m_ActionDisplayName.color.r, bindingSlot.m_ActionDisplayName.color.g, bindingSlot.m_ActionDisplayName.color.b, 26f / 51f);
			bindingSlot.m_ActionDisplayName.color = color;
			bindingSlot.m_ClearKeyButton.gameObject.SetActive(value: false);
			bindingSlot.m_ClearAltKeyButton.gameObject.SetActive(value: false);
			bindingSlot.m_EditKeyButton.GetComponent<Button>().enabled = false;
			bindingSlot.m_EditKeyButton.GetComponentInChildren<Image>().gameObject.SetActive(value: false);
			bindingSlot.m_EditKeyButton.GetComponentInChildren<TextMeshProUGUI>().color = color;
			bindingSlot.m_EditAltKeyButton.GetComponent<Button>().enabled = false;
			bindingSlot.m_EditAltKeyButton.GetComponentInChildren<Image>().gameObject.SetActive(value: false);
			bindingSlot.m_EditAltKeyButton.GetComponentInChildren<TextMeshProUGUI>().color = color;
		}
	}

	private void AddSlimHeader(string locId)
	{
		GameObject gameObject = Object.Instantiate(m_BindingHeaderSlimPrefab, m_Content.transform);
		if ((bool)gameObject)
		{
			BindingHeader component = gameObject.GetComponent<BindingHeader>();
			if ((bool)component)
			{
				component.m_ModeLocId = locId;
			}
		}
	}

	private BindingHeader AddHeader(string locId)
	{
		GameObject gameObject = Object.Instantiate(m_BindingHeaderPrefab, m_Content.transform);
		if (!gameObject)
		{
			return null;
		}
		BindingHeader component = gameObject.GetComponent<BindingHeader>();
		if ((bool)component)
		{
			component.m_ModeLocId = locId;
		}
		return component;
	}

	private void AddHeaderReadOnly(string locId)
	{
		BindingHeader bindingHeader = AddHeader(locId);
		if ((bool)bindingHeader)
		{
			bindingHeader.m_BindingText.gameObject.SetActive(value: false);
			bindingHeader.m_AltBindingText.gameObject.SetActive(value: false);
		}
	}

	private void ConfirmOnDefaults()
	{
		foreach (BindingSlot slot in m_Slots)
		{
			slot.ResetToDefaults();
		}
	}

	private void ApplyShadowBinding(Binding shadowBinding)
	{
		Binding binding = Bindings.GetBinding(shadowBinding.m_BindingType);
		if (binding != null)
		{
			binding.m_KeyCode = shadowBinding.m_KeyCode;
			binding.m_AltKeyCode = shadowBinding.m_AltKeyCode;
		}
	}

	private void InitSlots()
	{
		foreach (BindingSlot slot in m_Slots)
		{
			slot.Init();
		}
	}
}
