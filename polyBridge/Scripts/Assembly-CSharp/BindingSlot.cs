using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BindingSlot : MonoBehaviour
{
	public TextMeshProUGUI m_ActionDisplayName;

	public TextMeshProUGUI m_KeyPressDisplayName;

	public TextMeshProUGUI m_AltKeyPressDisplayName;

	public Image m_Background;

	[Header("Buttons")]
	public Button m_EditKeyButton;

	public Button m_ClearKeyButton;

	public Button m_EditAltKeyButton;

	public Button m_ClearAltKeyButton;

	[NonSerialized]
	public BindingModeFlags m_ModeFlags;

	[NonSerialized]
	public Binding m_ShadowBinding;

	private void Start()
	{
		m_EditKeyButton.onClick.AddListener(OnEditKeyPress);
		m_ClearKeyButton.onClick.AddListener(OnClearKeyPress);
		m_EditAltKeyButton.onClick.AddListener(OnEditAltKeyPress);
		m_ClearAltKeyButton.onClick.AddListener(OnClearAltKeyPress);
	}

	public void Init()
	{
		if (m_ShadowBinding == null)
		{
			Debug.LogWarningFormat("null m_ShadowBinging in BindingSlot.Init()");
		}
		Binding binding = Bindings.GetBinding(m_ShadowBinding.m_BindingType);
		if (binding == null)
		{
			Debug.LogWarningFormat("Failed to find Binding {0} int BindingSlot.Init()", m_ShadowBinding.m_BindingType.ToString());
		}
		else
		{
			m_ShadowBinding.m_KeyCode = binding.m_KeyCode;
			m_ShadowBinding.m_AltKeyCode = binding.m_AltKeyCode;
			UpdateBindingDisplayNames();
		}
	}

	public void SetBinding(BindingType type)
	{
		Binding binding = Bindings.GetBinding(type);
		if (binding == null)
		{
			Debug.LogWarningFormat("No binding found for type {0}", type.ToString());
			return;
		}
		m_ShadowBinding = new Binding(binding.m_BindingType, binding.m_DisplayNameLocId, binding.m_KeyCode, binding.m_AltKeyCode, binding.m_GamepadButtonType);
		if (m_ShadowBinding != null)
		{
			m_ActionDisplayName.text = Localize.Get(m_ShadowBinding.m_DisplayNameLocId);
			UpdateBindingDisplayNames();
		}
	}

	public void ResetToDefaults()
	{
		Binding binding = Bindings.GetBinding(m_ShadowBinding.m_BindingType);
		if (binding != null)
		{
			m_ShadowBinding.m_KeyCode = binding.m_KeyCodeDefault;
			m_ShadowBinding.m_AltKeyCode = binding.m_AltKeyCodeDefault;
			UpdateBindingDisplayNames();
		}
	}

	public void UpdateBindingDisplayNames()
	{
		m_KeyPressDisplayName.text = m_ShadowBinding.GetKeyBindingString();
		m_AltKeyPressDisplayName.text = m_ShadowBinding.GetAltKeyBindingString();
	}

	public bool ModeOverlaps(BindingModeFlags compareFlags)
	{
		return (m_ModeFlags & compareFlags) != 0;
	}

	public bool BindingsOverlap(BindingSlot compareSlot)
	{
		if (m_ShadowBinding.m_KeyCode != compareSlot.m_ShadowBinding.m_KeyCode)
		{
			return m_ShadowBinding.m_AltKeyCode == compareSlot.m_ShadowBinding.m_AltKeyCode;
		}
		return true;
	}

	private void OnEnable()
	{
		Binding binding = Bindings.GetBinding(m_ShadowBinding.m_BindingType);
		if (binding != null)
		{
			m_ActionDisplayName.text = Localize.Get(binding.m_DisplayNameLocId);
			UpdateBindingDisplayNames();
		}
	}

	private void OnDisable()
	{
	}

	private void OnEditKeyPress()
	{
		DisplayBindingPopup(PopUpBindingType.PRIMARY);
	}

	private void OnClearKeyPress()
	{
		if (m_ShadowBinding.m_KeyCode == KeyCode.None)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_KEY_ALREADY_UNBOUND", GameUI.MarkupForGold(m_ActionDisplayName.text)));
		}
		else
		{
			PopUpMessage.DisplayWarning(Localize.Get("POPUP_KEY_DELETE_BINDING", GameUI.MarkupForGold(m_ActionDisplayName.text), GameUI.MarkupForGold(m_ShadowBinding.GetKeyBindingString())), useYesNoLables: false, ConfirmClearKeyPress);
		}
	}

	private void OnEditAltKeyPress()
	{
		DisplayBindingPopup(PopUpBindingType.ALT);
	}

	private void OnClearAltKeyPress()
	{
		if (m_ShadowBinding.m_AltKeyCode == KeyCode.None)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_KEY_ALREADY_UNBOUND", GameUI.MarkupForGold(m_ActionDisplayName.text)));
		}
		else
		{
			PopUpMessage.DisplayWarning(Localize.Get("POPUP_KEY_DELETE_BINDING", GameUI.MarkupForGold(m_ActionDisplayName.text), GameUI.MarkupForGold(m_ShadowBinding.GetAltKeyBindingString())), useYesNoLables: false, ConfirmClearAltKeyPress);
		}
	}

	private string GetClearMessage()
	{
		return string.Format(Localize.Get("UI_BINDING_DELETE"), GameUI.MarkupForGold(m_ActionDisplayName.text), m_ShadowBinding.GetTooltipBindingString());
	}

	private void ConfirmClearKeyPress()
	{
		m_ShadowBinding.ClearKeyBinding();
		UpdateBindingDisplayNames();
	}

	private void ConfirmClearAltKeyPress()
	{
		m_ShadowBinding.ClearAltKeyBinding();
		UpdateBindingDisplayNames();
	}

	private void DisplayBindingPopup(PopUpBindingType popupBindingType)
	{
		if (GameUI.m_Instance.m_PopUpMessage.gameObject.activeInHierarchy)
		{
			Debug.LogWarningFormat("Tried to display popup message when popup is currently active");
			return;
		}
		GameUI.m_Instance.m_PopUpBinding.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_PopUpBinding.m_PopupBindingType = popupBindingType;
		GameUI.m_Instance.m_PopUpBinding.m_PromptBindingText.text = GetPromptBindingText();
		if (m_ShadowBinding != null)
		{
			GameUI.m_Instance.m_PopUpBinding.m_BindingSlot = this;
			GameUI.m_Instance.m_PopUpBinding.m_CurrentBindingText.text = GetCurrentBindingText();
		}
	}

	private string GetPromptBindingText()
	{
		return string.Format(Localize.Get("UI_BINDING_PRESS_KEY"), GameUI.MarkupForGold(m_ActionDisplayName.text));
	}

	private string GetCurrentBindingText()
	{
		if (m_ShadowBinding == null)
		{
			return string.Empty;
		}
		if (m_ShadowBinding.m_KeyCode != KeyCode.None && m_ShadowBinding.m_AltKeyCode != KeyCode.None)
		{
			return string.Format(Localize.Get("UI_BINDING_ALREADY_BOUND_TWO"), GameUI.MarkupForGold(m_ShadowBinding.GetKeyBindingString()), GameUI.MarkupForGold(m_ShadowBinding.GetAltKeyBindingString()));
		}
		if (m_ShadowBinding.m_KeyCode != KeyCode.None)
		{
			return string.Format(Localize.Get("UI_BINDING_ALREADY_BOUND_ONE"), GameUI.MarkupForGold(m_ShadowBinding.GetKeyBindingString()));
		}
		if (m_ShadowBinding.m_AltKeyCode != KeyCode.None)
		{
			return string.Format(Localize.Get("UI_BINDING_ALREADY_BOUND_ONE"), GameUI.MarkupForGold(m_ShadowBinding.GetAltKeyBindingString()));
		}
		return string.Empty;
	}
}
