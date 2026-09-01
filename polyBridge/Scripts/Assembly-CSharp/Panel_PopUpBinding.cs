using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PopUpBinding : MonoBehaviour
{
	public TextMeshProUGUI m_PromptBindingText;

	public TextMeshProUGUI m_CurrentBindingText;

	public Button m_Cancel;

	[NonSerialized]
	public BindingSlot m_BindingSlot;

	[NonSerialized]
	public PopUpBindingType m_PopupBindingType;

	private int[] m_AllKeyCodes;

	private KeyCode m_ConflictKeyCode;

	private void Awake()
	{
		m_AllKeyCodes = (int[])Enum.GetValues(typeof(KeyCode));
		m_Cancel.onClick.AddListener(OnCancel);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_menu_cancel");
		Close();
	}

	private void Close()
	{
		GameUI.m_Instance.m_PopUpBinding.gameObject.SetActive(value: false);
	}

	private void CheckForInput()
	{
		for (int i = 0; i < m_AllKeyCodes.Length; i++)
		{
			KeyCode keyCode = (KeyCode)m_AllKeyCodes[i];
			if (Input.GetKeyDown(keyCode) && IsBindableKeyCode(keyCode) && (!IsMousePrimaryClick(keyCode) || !GameUI.PointerOver(typeof(Button))))
			{
				List<BindingSlot> conflictingBindingSlots = GameUI.m_Instance.m_Settings.m_ControlsPanel.GetConflictingBindingSlots(m_BindingSlot, keyCode, m_PopupBindingType);
				if (conflictingBindingSlots.Count > 0)
				{
					m_ConflictKeyCode = keyCode;
					DisplayBindingConflictPopup(conflictingBindingSlots, keyCode, OnKeyConflictPopupOK);
				}
				else
				{
					ApplyKeyBindingChange(m_BindingSlot, keyCode);
					InterfaceAudio.Play("ui_menu_accept");
					Close();
				}
				break;
			}
		}
	}

	private bool ModesOverlap(BindingSlot A, BindingSlot B)
	{
		return (A.m_ModeFlags & B.m_ModeFlags) != 0;
	}

	private void OnKeyConflictPopupOK()
	{
		foreach (BindingSlot conflictingBindingSlot in GameUI.m_Instance.m_Settings.m_ControlsPanel.GetConflictingBindingSlots(m_BindingSlot, m_ConflictKeyCode, m_PopupBindingType))
		{
			if (conflictingBindingSlot.m_ShadowBinding.m_KeyCode == m_ConflictKeyCode)
			{
				conflictingBindingSlot.m_ShadowBinding.m_KeyCode = KeyCode.None;
			}
			if (conflictingBindingSlot.m_ShadowBinding.m_AltKeyCode == m_ConflictKeyCode)
			{
				conflictingBindingSlot.m_ShadowBinding.m_AltKeyCode = KeyCode.None;
			}
			conflictingBindingSlot.UpdateBindingDisplayNames();
		}
		ApplyKeyBindingChange(m_BindingSlot, m_ConflictKeyCode);
		Close();
	}

	private void ApplyKeyBindingChange(BindingSlot slot, KeyCode keyCode)
	{
		if (m_PopupBindingType == PopUpBindingType.PRIMARY)
		{
			m_BindingSlot.m_ShadowBinding.m_KeyCode = keyCode;
		}
		else
		{
			m_BindingSlot.m_ShadowBinding.m_AltKeyCode = keyCode;
		}
		GameUI.m_Instance.m_Settings.m_ControlsPanel.RefreshSlotDisplayNames(m_BindingSlot.m_ShadowBinding);
	}

	private bool IsBindableKeyCode(KeyCode keyCode)
	{
		if (keyCode == KeyCode.Escape || keyCode == KeyCode.BackQuote)
		{
			return false;
		}
		return keyCode <= KeyCode.Mouse6;
	}

	private bool IsMousePrimaryClick(KeyCode keyCode)
	{
		if (keyCode == KeyCode.Mouse0)
		{
			return true;
		}
		return false;
	}

	private void DisplayBindingConflictPopup(List<BindingSlot> conflictingSlots, KeyCode conflictKeyCode, Panel_PopUpBindingConflict.OnOkDelegate okDelegate)
	{
		GameUI.m_Instance.m_PopUpBindingConflict.gameObject.SetActive(value: true);
		string text = ((conflictingSlots[0].m_ShadowBinding.m_KeyCode == conflictKeyCode) ? conflictingSlots[0].m_ShadowBinding.GetKeyBindingString() : conflictingSlots[0].m_ShadowBinding.GetAltKeyBindingString());
		switch (conflictingSlots.Count)
		{
		case 1:
			if (conflictingSlots[0] == m_BindingSlot)
			{
				bool flag = conflictingSlots[0].m_ShadowBinding.m_KeyCode == conflictKeyCode;
				GameUI.m_Instance.m_PopUpBindingConflict.m_Line1.text = string.Format(Localize.Get("UI_BINDING_ALREADY_BOUND"), GameUI.MarkupForGold(text), flag ? Localize.Get("UI_BINDING_PRIMARY") : Localize.Get("UI_BINDING_ALT"), GameUI.MarkupForGold(conflictingSlots[0].m_ActionDisplayName.text));
			}
			else
			{
				GameUI.m_Instance.m_PopUpBindingConflict.m_Line1.text = string.Format(Localize.Get("UI_BINDING_USED_BY"), GameUI.MarkupForGold(text), GameUI.MarkupForGold(conflictingSlots[0].m_ActionDisplayName.text));
			}
			break;
		case 2:
			GameUI.m_Instance.m_PopUpBindingConflict.m_Line1.text = string.Format(Localize.Get("UI_BINDING_USED_BY_TWO"), GameUI.MarkupForGold(text), GameUI.MarkupForGold(conflictingSlots[0].m_ActionDisplayName.text), GameUI.MarkupForGold(conflictingSlots[1].m_ActionDisplayName.text));
			break;
		case 3:
			GameUI.m_Instance.m_PopUpBindingConflict.m_Line1.text = string.Format(Localize.Get("UI_BINDING_USED_BY_THREE"), GameUI.MarkupForGold(text), GameUI.MarkupForGold(conflictingSlots[0].m_ActionDisplayName.text), GameUI.MarkupForGold(conflictingSlots[1].m_ActionDisplayName.text), GameUI.MarkupForGold(conflictingSlots[2].m_ActionDisplayName.text));
			break;
		default:
			GameUI.m_Instance.m_PopUpBindingConflict.m_Line1.text = string.Format(Localize.Get("UI_BINDING_USED_BY_MULTIPLE"), GameUI.MarkupForGold(text));
			break;
		}
		if (conflictingSlots.Count == 1)
		{
			if (conflictingSlots[0] == m_BindingSlot)
			{
				bool flag2 = conflictingSlots[0].m_ShadowBinding.m_KeyCode == conflictKeyCode;
				GameUI.m_Instance.m_PopUpBindingConflict.m_Line2.text = string.Format(Localize.Get("UI_BINDING_MOVE"), GameUI.MarkupForGold(text), flag2 ? Localize.Get("UI_BINDING_PRIMARY") : Localize.Get("UI_BINDING_ALT"), flag2 ? Localize.Get("UI_BINDING_ALT") : Localize.Get("UI_BINDING_PRIMARY"));
			}
			else
			{
				GameUI.m_Instance.m_PopUpBindingConflict.m_Line2.text = string.Format(Localize.Get("UI_BINDING_MOVE_TWO"), GameUI.MarkupForGold(text), GameUI.MarkupForGold(m_BindingSlot.m_ActionDisplayName.text), GameUI.MarkupForGold(conflictingSlots[0].m_ActionDisplayName.text));
			}
		}
		else
		{
			GameUI.m_Instance.m_PopUpBindingConflict.m_Line2.text = string.Format(Localize.Get("UI_BINDING_MOVE_MULTIPLE"), GameUI.MarkupForGold(text), GameUI.MarkupForGold(m_BindingSlot.m_ActionDisplayName.text));
		}
		GameUI.m_Instance.m_PopUpBindingConflict.m_OnOkDelegate = okDelegate;
	}

	private void ProcessInput()
	{
		if (!uConsole.IsOn() && ActivePanels.IsTopPanel(base.gameObject))
		{
			if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				OnCancel();
			}
			CheckForInput();
		}
	}
}
