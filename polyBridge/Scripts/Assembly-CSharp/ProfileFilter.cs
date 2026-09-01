using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileFilter : MonoBehaviour
{
	public TMP_Dropdown m_Dropdown;

	private Action<string> m_ChangedCallback;

	private void Awake()
	{
		m_Dropdown.onValueChanged.AddListener(delegate
		{
			OnProfileChanged();
		});
		m_Dropdown.alphaFadeSpeed = 0f;
	}

	public void Init(Action<string> callback)
	{
		m_ChangedCallback = callback;
		PopulateDropdown();
		DropdownUtils.SelectItem(m_Dropdown, Profiles.GetActiveProfileName());
	}

	public string GetSelectedProfileName()
	{
		return m_Dropdown.captionText.text;
	}

	private void PopulateDropdown()
	{
		m_Dropdown.ClearOptions();
		List<string> profileNames = Profiles.GetProfileNames();
		if (profileNames != null && profileNames.Count > 0)
		{
			m_Dropdown.AddOptions(profileNames);
		}
	}

	private void OnProfileChanged()
	{
		m_ChangedCallback?.Invoke(m_Dropdown.captionText.text);
	}
}
