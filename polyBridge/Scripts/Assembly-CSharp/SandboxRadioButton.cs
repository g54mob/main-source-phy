using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SandboxRadioButton : MonoBehaviour
{
	public Button m_Button;

	public TextMeshProUGUI m_Text;

	public Image m_Background;

	public Image m_Foreground;

	private Action<int> m_Callback;

	private int m_Index;

	private List<SandboxRadioButton> m_LinkedButtons = new List<SandboxRadioButton>();

	private void Awake()
	{
		m_Button.onClick.AddListener(OnButton);
	}

	public void SetText(string text)
	{
		m_Text.text = text;
	}

	public void SetCallback(Action<int> callback, int index)
	{
		m_Callback = callback;
		m_Index = index;
	}

	public void LinkButton(SandboxRadioButton button)
	{
		m_LinkedButtons.Add(button);
	}

	public bool IsOn()
	{
		return m_Foreground.gameObject.activeInHierarchy;
	}

	public void TurnOn()
	{
		m_Foreground.gameObject.SetActive(value: true);
	}

	public void TurnOff()
	{
		m_Foreground.gameObject.SetActive(value: false);
	}

	private void OnButton()
	{
		foreach (SandboxRadioButton linkedButton in m_LinkedButtons)
		{
			linkedButton.TurnOff();
		}
		TurnOn();
		m_Callback?.Invoke(m_Index);
	}
}
