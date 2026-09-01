using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UILocalisationDropdown : MonoBehaviour
{
	public TMP_Dropdown Dropdown_CurrentLangauge;

	private readonly List<string> _languages;

	private void Awake()
	{
		if (Dropdown_CurrentLangauge == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		}
	}

	private void OnEnable()
	{
		Populate();
		TMP_Dropdown dropdown_CurrentLangauge = Dropdown_CurrentLangauge;
		UnityAction<int> call = OnChanged;
		dropdown_CurrentLangauge.m_OnValueChanged.AddListener(call);
	}

	private void OnDisable()
	{
		if (Dropdown_CurrentLangauge != null)
		{
			TMP_Dropdown dropdown_CurrentLangauge = Dropdown_CurrentLangauge;
			UnityAction<int> call = OnChanged;
			dropdown_CurrentLangauge.m_OnValueChanged.RemoveListener(call);
		}
	}

	public void Populate()
	{
		//IL_0155: Expected O, but got I
		//IL_0166: Expected O, but got I
		//IL_019c: Expected O, but got I
		List<string> languages = _languages;
		int version = languages._version + 1;
		languages._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			languages._size = 0;
		}
		else
		{
			languages._size = 0;
			if (languages._size > 0)
			{
				Array.Clear(languages._items, 0, languages._size);
			}
		}
		Dropdown_CurrentLangauge.ClearOptions();
		LocalisationManager instance = LocalisationManager.Instance;
		LocalisationLangData langData = instance.LangData;
		List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		while (true)
		{
			List<LocalisationLangData.LangData> supportedLanguages = langData.SupportedLanguages;
			if (num3 >= supportedLanguages._size)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			List<string> languages2 = _languages;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ stack_8_v3+10]");
			languages2.Add((string)0);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ stack_8_v3+20]");
			TMP_Dropdown.OptionData item = new TMP_Dropdown.OptionData((string)0);
			list.Add(item);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ stack_8_v3+10]");
			bool flag = (string)0 != instance.CurrentLanguage;
			int num4 = num;
			if (flag)
			{
				num4 = num2;
			}
			num++;
			num2 = num4;
			num3 = num;
		}
		Dropdown_CurrentLangauge.AddOptions(list);
		Dropdown_CurrentLangauge.SetValueWithoutNotify(num2);
		Dropdown_CurrentLangauge.RefreshShownValue();
	}

	private void OnChanged(int index)
	{
		if (index >= 0)
		{
			List<string> languages = _languages;
			if (index < languages._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				string language = default(string);
				LocalisationManager.Instance.SwitchLanguage(language, save: true);
			}
		}
	}

	public UILocalisationDropdown()
	{
		List<string> languages = new List<string>();
		_languages = languages;
		base._002Ector();
	}
}
