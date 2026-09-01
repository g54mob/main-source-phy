using InControl;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIControlBinding : MonoBehaviour
{
	private PlayerAction m_action;

	private BindingSource m_binding1;

	private BindingSource m_binding2;

	private TextMeshProUGUI m_text;

	private GlyphService m_glyphService;

	private void Awake()
	{
		m_text = GetComponentInChildren<TextMeshProUGUI>(includeInactive: true);
	}

	public void Initialize(PlayerAction action, BindingSource binding1, BindingSource binding2)
	{
		m_action = action;
		m_binding1 = binding1;
		m_binding2 = binding2;
		UpdateBindingText();
	}

	public void UpdateBindingText()
	{
		InputType inputType = PlayerActions.Instance.InputType;
		m_text.fontSize = ((inputType == InputType.Controller) ? 32 : 16);
		string text = string.Empty;
		if (m_binding1 == null && m_binding2 == null)
		{
			text = Localizer.GetSinglePhrase("SETTINGS_UNBOUND");
		}
		else
		{
			if (m_binding1 != null)
			{
				text = GetBindingText(m_binding1);
			}
			if (m_binding2 != null)
			{
				if (m_binding1 != null)
				{
					string text2 = " " + Localizer.GetSinglePhrase("LABEL_OR") + " ";
					text2 = ((inputType == InputType.Controller) ? ("<size=50%><voffset=6>" + text2 + "</voffset></size>") : text2);
					text += text2;
				}
				text += GetBindingText(m_binding2);
			}
		}
		m_text.text = text;
	}

	private string GetBindingText(BindingSource bindingSource)
	{
		if (m_glyphService == null)
		{
			m_glyphService = ServiceLocator.GetService<GlyphService>();
		}
		if (PlayerActions.Instance.InputType == InputType.Controller)
		{
			return m_glyphService.GetBindingsGlyph(bindingSource, PlayerActions.Instance.InputType);
		}
		return Localizer.GetSinglePhrase(PlayerActions.ControlToKey(bindingSource.Name, bindingSource.BindingSourceType));
	}

	public void RemoveBindings()
	{
		m_action.RemoveBinding(m_binding1);
		m_action.RemoveBinding(m_binding2);
	}

	public void StartListening()
	{
		if (!m_action.IsListeningForBinding)
		{
			Debug.Log("Start listening to binding for " + m_action.Name);
			EventSystem.current.SetSelectedGameObject(null);
			if (m_binding1 == null)
			{
				m_action.ListenForBindingReplacing(m_binding1);
			}
			else if (m_binding2 == null)
			{
				m_action.ListenForBindingReplacing(m_binding2);
			}
			else
			{
				RemoveBindings();
				m_action.ListenForBinding();
			}
			m_text.text = "???";
		}
	}
}
