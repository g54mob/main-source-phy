using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandboxStylePicker : MonoBehaviour
{
	public GameObject m_SandboxTogglePrefab;

	public ToggleGroup m_ToggleGroup;

	public SandboxPanelResizer m_SandboxPanelResizer;

	private List<SandboxToggle> m_Toggles = new List<SandboxToggle>();

	public void CreateButtons(int count, Action<int> callback)
	{
		foreach (SandboxToggle toggle in m_Toggles)
		{
			toggle.gameObject.SetActive(value: false);
			UnityEngine.Object.Destroy(toggle.gameObject);
		}
		m_Toggles.Clear();
		for (int i = 0; i < count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(m_SandboxTogglePrefab, base.transform.parent);
			if (gameObject != null)
			{
				SandboxToggle component = gameObject.GetComponent<SandboxToggle>();
				if (component != null)
				{
					component.m_Toggle.group = m_ToggleGroup;
					component.SetCallback(callback, i);
					m_Toggles.Add(component);
				}
			}
		}
		m_SandboxPanelResizer.ForceUpdate();
	}

	public void Select(int index)
	{
		m_Toggles[index].m_Toggle.isOn = true;
	}

	public void SetButtonText(int index, string text)
	{
		m_Toggles[index].SetText(text);
	}
}
