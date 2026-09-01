using System;
using System.Collections.Generic;
using System.Text;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

namespace Localisation;

public class LocalisationManager : MonoBehaviour
{
	public static LocalisationManager Instance;

	private static Action m_OnLanguageChanged;

	private bool _003CIsReady_003Ek__BackingField;

	public const string PlayerPrefs_Language = "CurrentLanguage";

	public const string DefaultLanguage = "English";

	public LocalisationLangData LangData;

	public LocalisationFontData FontData;

	public string CurrentLanguage = "English";

	public Dictionary<string, TextEntry> CurrentLoadedStrings;

	private bool _popupShown;

	private bool _cutscenePopupShown;

	public bool IsReady
	{
		get
		{
			return _003CIsReady_003Ek__BackingField;
		}
		private set
		{
			_003CIsReady_003Ek__BackingField = value;
		}
	}

	public static event Action OnLanguageChanged
	{
		add
		{
			//IL_004f: Expected I, but got O
			//IL_0065: Expected O, but got I
			Delegate obj = LocalisationManager.m_OnLanguageChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Combine(obj, value);
				bool flag = (object)obj2 == null;
				Delegate obj3 = null;
				if (!flag)
				{
					bool flag2 = (object)obj2.GetType() != typeof(Action);
					obj3 = null;
					if (!flag2)
					{
						obj3 = obj2;
					}
					if ((object)obj3 == null)
					{
						break;
					}
				}
				nint num = (nint)typeof(LocalisationManager);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rax_v5 (Il2CppClass<Localisation.LocalisationManager>)+B8]");
				object obj4 = (nint)0 + (nint)8;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj;
				obj = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_004f: Expected I, but got O
			//IL_0065: Expected O, but got I
			Delegate obj = LocalisationManager.m_OnLanguageChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Remove(obj, value);
				bool flag = (object)obj2 == null;
				Delegate obj3 = null;
				if (!flag)
				{
					bool flag2 = (object)obj2.GetType() != typeof(Action);
					obj3 = null;
					if (!flag2)
					{
						obj3 = obj2;
					}
					if ((object)obj3 == null)
					{
						break;
					}
				}
				nint num = (nint)typeof(LocalisationManager);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rax_v5 (Il2CppClass<Localisation.LocalisationManager>)+B8]");
				object obj4 = (nint)0 + (nint)8;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj;
				obj = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Awake()
	{
		if (!(Instance != null))
		{
			Instance = this;
			return;
		}
		GameObject obj = base.gameObject;
		UnityEngine.Object.Destroy(obj);
	}

	private void Start()
	{
		GameObject target = base.gameObject;
		UnityEngine.Object.DontDestroyOnLoad(target);
		FontData.Init();
		LangData.Init();
		SystemLanguage systemLanguage = Application.systemLanguage;
		LocalisationLangData.LangData languageData = LangData.GetLanguageData(systemLanguage);
		string language = PlayerPrefs.GetString("CurrentLanguage", languageData.LangCode);
		LocalisationLangData.LangData languageData2 = LangData.GetLanguageData(language);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A63B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		LocalisationLangData.LangData languageData3 = LangData.GetLanguageData(languageData2.Lang);
		CurrentLanguage = languageData2.Lang;
		Load();
		_003CIsReady_003Ek__BackingField = true;
	}

	public void ShowLanguagePopupIfNeeded()
	{
		if (!_cutscenePopupShown)
		{
			_cutscenePopupShown = true;
			SystemLanguage systemLanguage = Application.systemLanguage;
			LocalisationLangData.LangData languageData = LangData.GetLanguageData(systemLanguage);
			string language = PlayerPrefs.GetString("CurrentLanguage", languageData.LangCode);
			LocalisationLangData.LangData languageData2 = LangData.GetLanguageData(language);
			if (languageData2.Lang != "English")
			{
				UILocalisationPopup uILocalisationPopup = UnityEngine.Object.FindFirstObjectByType<UILocalisationPopup>(FindObjectsInactive.Include);
				GameObject gameObject = uILocalisationPopup.gameObject;
				gameObject.SetActive(value: true);
			}
		}
	}

	private void Load()
	{
		//IL_00bb: Expected I, but got O
		//IL_00f1: Expected O, but got I
		//IL_00fa: Expected O, but got I4
		//IL_0103: Expected O, but got I4
		//IL_01b0: Expected O, but got I
		//IL_0168: Expected O, but got I
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		CurrentLoadedStrings.Clear();
		string path = CurrentLanguage.ToString();
		TextAsset textAsset = Resources.Load<TextAsset>(path);
		if (!(textAsset != null))
		{
			Debug.Log("Failed to load DialogItems");
			TextEntry textEntry = null;
		}
		else
		{
			byte[] bytes = textAsset.bytes;
			Encoding uTF = Encoding.UTF8;
			nint num = (nint)uTF;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v366 @ r8_v3 (Il2CppClass<System.Text.Encoding>)+358]");
			nint num2 = 0;
			string text = uTF.GetString(bytes);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B3E0");
			TextEntry textEntry = (TextEntry)0;
			object obj = 0;
			object obj2 = 0;
			TextEntry textEntry2 = default(TextEntry);
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_8_v2+10]");
				object obj3 = 0;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rax_v17+18]");
				if ((nint)obj4 >= 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Dictionary<string, TextEntry> currentLoadedStrings = CurrentLoadedStrings;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ stack_18_v3+10]");
				currentLoadedStrings.set_Item((string)0, textEntry2);
				obj++;
				num2 = 0;
				textEntry = textEntry2;
				obj2 = obj;
			}
		}
		Action onLanguageChanged = LocalisationManager.m_OnLanguageChanged;
		if (LocalisationManager.m_OnLanguageChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v345.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public string Get(string Key)
	{
		string text;
		if (CurrentLoadedStrings != null)
		{
			if (!CurrentLoadedStrings.TryGetValue(Key, out var value))
			{
				text = "' - Item not in dictionary for language ";
				goto IL_010c;
			}
			if (value != null)
			{
				if (string.IsNullOrEmpty(value.Text))
				{
					text = "' - Item is empty for language ";
					goto IL_010c;
				}
				if (value != null)
				{
					return value.Text;
				}
			}
		}
		return (string)(object)new NullReferenceException();
		IL_010c:
		string message = "Error Getting DialogItemKey Key '" + Key + text + CurrentLanguage;
		Debug.Log(message);
		return "[" + Key + "]";
	}

	public unsafe bool TryGet(string Key, out string text)
	{
		//IL_00ff: Expected I4, but got O
		string text2;
		ref string reference;
		if (CurrentLoadedStrings != null)
		{
			if (!CurrentLoadedStrings.TryGetValue(Key, out var value))
			{
				text2 = "' - Item not in dictionary for language ";
				goto IL_011e;
			}
			if (value != null)
			{
				if (string.IsNullOrEmpty(value.Text))
				{
					text2 = "' - Item is empty for language ";
					goto IL_011e;
				}
				if (value != null)
				{
					reference = ref *(string*)value.Text;
					return true;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_011e:
		string message = "Error Getting DialogItemKey Key '" + Key + text2 + CurrentLanguage;
		Debug.Log(message);
		string text3 = "[" + Key + "]";
		reference = ref *(string*)text3;
		return false;
	}

	public TMP_FontAsset GetFont(TMP_FontAsset original)
	{
		if (!(original != null) || !(FontData != null) || !(CurrentLanguage != "English"))
		{
			goto IL_0160;
		}
		LocalisationFontData fontData = FontData;
		if ((object)FontData != null && (object)original != null)
		{
			string key = original.name;
			if (fontData.Runtime != null)
			{
				if (fontData.Runtime.TryGetValue(key, out var value))
				{
					if (value == null)
					{
						goto IL_0165;
					}
					if (value.TryGetValue(CurrentLanguage, out var value2))
					{
						return value2;
					}
				}
				goto IL_0160;
			}
		}
		goto IL_0165;
		IL_0160:
		return original;
		IL_0165:
		return (TMP_FontAsset)(object)new NullReferenceException();
	}

	public void DetectLanguage()
	{
		SystemLanguage systemLanguage = Application.systemLanguage;
		LocalisationLangData.LangData languageData = LangData.GetLanguageData(systemLanguage);
		string language = PlayerPrefs.GetString("CurrentLanguage", languageData.LangCode);
		LocalisationLangData.LangData languageData2 = LangData.GetLanguageData(language);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A63B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		LocalisationLangData.LangData languageData3 = LangData.GetLanguageData(languageData2.Lang);
		CurrentLanguage = languageData2.Lang;
		Load();
	}

	public void SwitchLanguage(string language, bool save = false)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A63B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		LocalisationLangData.LangData languageData = LangData.GetLanguageData(language);
		if (save)
		{
			PlayerPrefs.SetString("CurrentLanguage", languageData.Lang);
		}
		CurrentLanguage = language;
		Load();
	}

	public LocalisationManager()
	{
		Dictionary<string, TextEntry> currentLoadedStrings = new Dictionary<string, TextEntry>();
		CurrentLoadedStrings = currentLoadedStrings;
		base._002Ector();
	}
}
