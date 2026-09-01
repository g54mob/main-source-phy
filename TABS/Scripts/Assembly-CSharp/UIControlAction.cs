using InControl;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIControlAction : MonoBehaviour
{
	[SerializeField]
	private LocalizeText m_settingName;

	[SerializeField]
	private UIControlBinding m_binding;

	[SerializeField]
	private Button m_button;

	private PlayerAction m_playerAction;

	private bool m_isOn;

	private bool m_reselectOnBind;

	private void Awake()
	{
		m_button.onClick.AddListener(OnToggleValueChanged);
	}

	public void SetText(string name)
	{
		m_settingName.Localized = false;
		m_settingName.Text.text = name;
	}

	public void SetLocalizedText(string nameKey)
	{
		m_settingName.Localized = true;
		m_settingName.LocaleID = nameKey;
	}

	public void SetPlayerAction(PlayerAction action)
	{
		m_playerAction = action;
		BindingSource bindingSource = null;
		BindingSource bindingSource2 = null;
		for (int i = 0; i < action.Bindings.Count; i++)
		{
			BindingSource bindingSource3 = action.Bindings[i];
			bool flag = false;
			switch (PlayerActions.Instance.InputType)
			{
			case InputType.Controller:
				if (bindingSource3.BindingSourceType == BindingSourceType.DeviceBindingSource)
				{
					flag = true;
				}
				break;
			case InputType.Keyboard:
				if (bindingSource3.BindingSourceType == BindingSourceType.MouseBindingSource || bindingSource3.BindingSourceType == BindingSourceType.KeyBindingSource)
				{
					flag = true;
				}
				break;
			}
			if (!flag)
			{
				continue;
			}
			if (bindingSource == null)
			{
				bindingSource = bindingSource3;
				continue;
			}
			if (!(bindingSource2 == null))
			{
				break;
			}
			bindingSource2 = bindingSource3;
		}
		m_binding.Initialize(action, bindingSource, bindingSource2);
	}

	public void ResetPlayerAction()
	{
		SetPlayerAction(m_playerAction);
	}

	public void ResetToggles()
	{
		m_binding.UpdateBindingText();
		if (m_reselectOnBind)
		{
			EventSystem.current.SetSelectedGameObject(m_button.gameObject);
		}
	}

	private void OnToggleValueChanged()
	{
		m_reselectOnBind = !Input.GetMouseButtonUp(0);
		m_binding.StartListening();
	}

	private void RefreshBinding()
	{
		SetPlayerAction(m_playerAction);
	}

	private void OnEnable()
	{
		PlayerActions.Instance.OnLastInputTypeChanged += OnInputTypeChanged;
	}

	private void OnDisable()
	{
		PlayerActions.Instance.OnLastInputTypeChanged -= OnInputTypeChanged;
	}

	private void OnInputTypeChanged(BindingSourceType obj)
	{
		RefreshBinding();
	}
}
