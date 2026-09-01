using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SandboxToggle : MonoBehaviour
{
	public Toggle m_Toggle;

	public Image m_MixedImage;

	public TextMeshProUGUI m_Label;

	private Action<int> m_Callback;

	private int m_Index;

	private PointerEvents m_PointerEvents;

	private void Awake()
	{
		m_PointerEvents = m_Toggle.GetComponent<PointerEvents>();
		m_PointerEvents.RegisterOnClickedDelegate(OnToggle);
	}

	public void SetText(string text)
	{
		m_Label.text = text;
	}

	public void SetCallback(Action<int> callback, int index)
	{
		m_Callback = callback;
		m_Index = index;
	}

	public void EnableMixedImage(bool on)
	{
		if (m_MixedImage != null)
		{
			m_MixedImage.gameObject.SetActive(on);
		}
	}

	private void OnToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		if (m_Toggle.isOn)
		{
			m_Callback?.Invoke(m_Index);
		}
	}
}
