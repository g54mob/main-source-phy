using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsRow : MonoBehaviour
{
	public SettingsRowType m_SettingsRowType;

	public Image m_Background;

	public Image m_Arrow;

	[NonSerialized]
	public Action<GamepadButtonType> m_Action;

	private static float m_TimeLastAudio;

	public void SendInput(GamepadButtonType button)
	{
		m_Action?.Invoke(button);
	}

	public static void ToggleProcessInput(Toggle toggle, GamepadButtonType button)
	{
		toggle.isOn = !toggle.isOn;
		InterfaceAudio.PlayToggleAudio();
	}

	public static void DropdownProcessInput(TMP_Dropdown dropdown, GamepadButtonType button)
	{
		if (dropdown.options == null || dropdown.options.Count == 1)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		switch (button)
		{
		case GamepadButtonType.DPAD_RIGHT:
		{
			int num2 = dropdown.value + 1;
			if (num2 >= dropdown.options.Count)
			{
				num2 = 0;
			}
			dropdown.value = num2;
			break;
		}
		case GamepadButtonType.DPAD_LEFT:
		{
			int num = dropdown.value - 1;
			if (num < 0)
			{
				num = dropdown.options.Count;
			}
			dropdown.value = num;
			break;
		}
		}
		if ((double)(Time.unscaledTime - m_TimeLastAudio) > 0.15)
		{
			InterfaceAudio.Play("ui_menu_hover");
			m_TimeLastAudio = Time.unscaledTime;
		}
	}

	public static void SliderProcessInput(Slider slider, GamepadButtonType button)
	{
		switch (button)
		{
		case GamepadButtonType.DPAD_RIGHT:
			if (slider.value < slider.maxValue)
			{
				slider.value += 1f;
				if ((double)(Time.unscaledTime - m_TimeLastAudio) > 0.15)
				{
					InterfaceAudio.Play("ui_menu_hover");
					m_TimeLastAudio = Time.unscaledTime;
				}
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
			break;
		case GamepadButtonType.DPAD_LEFT:
			if (slider.value > 0f)
			{
				slider.value -= 1f;
				if ((double)(Time.unscaledTime - m_TimeLastAudio) > 0.15)
				{
					InterfaceAudio.Play("ui_menu_hover");
					m_TimeLastAudio = Time.unscaledTime;
				}
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
			break;
		}
	}
}
