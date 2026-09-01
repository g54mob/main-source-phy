using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PopUpTwoChoices : MonoBehaviour
{
	public delegate void OnChoiceDelegate();

	public OnChoiceDelegate m_ChoiceCallbackA;

	public OnChoiceDelegate m_ChoiceCallbackB;

	public TextMeshProUGUI m_Message;

	public TextMeshProUGUI m_ChoiceAText;

	public TextMeshProUGUI m_ChoiceBText;

	public Button m_ChoiceA;

	public Button m_ChoiceB;

	public Button m_Cancel;

	public Toggle m_NeverShowAgainToggle;

	[NonSerialized]
	public BindingType m_ChoiceAShortcut;

	[NonSerialized]
	public BindingType m_ChoiceBShortcut;

	[NonSerialized]
	public PopUpWarningCategory m_Category;

	private void OnEnable()
	{
		m_Cancel.onClick.AddListener(OnCancel);
		m_ChoiceA.onClick.AddListener(OnChoiceA);
		m_ChoiceB.onClick.AddListener(OnChoiceB);
		m_ChoiceAShortcut = BindingType.NONE;
		m_ChoiceBShortcut = BindingType.NONE;
		BridgeJointPlacement.CancelSelection();
		InterfaceAudio.Play("ui_menubar_gen_on");
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		m_ChoiceA.onClick.RemoveAllListeners();
		m_ChoiceB.onClick.RemoveAllListeners();
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
	}

	public void Close()
	{
		if (m_NeverShowAgainToggle.isOn)
		{
			if (!Profiles.m_ActiveProfile.m_NeverShowAgain.Contains(m_Category))
			{
				Profiles.m_ActiveProfile.m_NeverShowAgain.Add(m_Category);
				Profiles.SaveActiveProfile();
			}
		}
		else if (Profiles.m_ActiveProfile.m_NeverShowAgain.Contains(m_Category))
		{
			Profiles.m_ActiveProfile.m_NeverShowAgain.Remove(m_Category);
			Profiles.SaveActiveProfile();
		}
		GameUI.m_Instance.m_PopUpTwoChoices.gameObject.SetActive(value: false);
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_menubar_gen_off");
		Close();
	}

	private void OnChoiceA()
	{
		if (m_ChoiceCallbackA != null)
		{
			m_ChoiceCallbackA();
		}
		Close();
		InterfaceAudio.Play("ui_menu_select");
	}

	private void OnChoiceB()
	{
		if (m_ChoiceCallbackB != null)
		{
			m_ChoiceCallbackB();
		}
		Close();
		InterfaceAudio.Play("ui_menu_select");
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			OnCancel();
			return;
		}
		if (m_ChoiceAShortcut != BindingType.NONE && GameInput.JustPressed(m_ChoiceAShortcut))
		{
			OnChoiceA();
		}
		if (m_ChoiceBShortcut != BindingType.NONE && GameInput.JustPressed(m_ChoiceBShortcut))
		{
			OnChoiceB();
		}
	}
}
