using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class UILocalisationDropdown : MonoBehaviour
{
	public TMP_Dropdown Dropdown_CurrentLangauge;

	private readonly List<string> _languages;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void Populate()
	{
	}

	private void OnChanged(int index)
	{
	}
}
