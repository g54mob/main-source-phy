using System;
using Cpp2ILInjected;
using Localisation;
using TMPro;
using UnityEngine;

public class StaticLocalisedText : MonoBehaviour
{
	public TextIdentifier Key;

	private TMP_Text Text;

	private TMP_FontAsset OriginalFont;

	private float originalFontSize;

	private bool wasAutoEnabledOnStart;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		TMP_Text text = default(TMP_Text);
		Text = text;
		TMP_FontAsset originalFont;
		if (Text != null)
		{
			TMP_Text text2 = Text;
			originalFont = text2.m_fontAsset;
		}
		else
		{
			originalFont = null;
		}
		OriginalFont = originalFont;
		TMP_Text text3 = Text;
		originalFontSize = text3.m_fontSize;
		TMP_Text text4 = Text;
		wasAutoEnabledOnStart = text4.m_enableAutoSizing;
	}

	private void OnEnable()
	{
		Action value = UpdateText;
		LocalisationManager.OnLanguageChanged += value;
		LocalisationManager instance = LocalisationManager.Instance;
		if (instance._003CIsReady_003Ek__BackingField)
		{
			UpdateText();
		}
	}

	private void OnDisable()
	{
		Action value = UpdateText;
		LocalisationManager.OnLanguageChanged -= value;
	}

	public void UpdateText()
	{
		//IL_0120: Expected O, but got I4
		//IL_0246: Expected O, but got I
		//IL_0256: Expected O, but got I
		if (!(Text != null))
		{
			return;
		}
		bool flag = Key.TryGet(out var text);
		if (!wasAutoEnabledOnStart)
		{
			LocalisationManager instance = LocalisationManager.Instance;
			if (instance.CurrentLanguage == "English")
			{
				Text.enableAutoSizing = false;
				Text.fontSize = originalFontSize;
			}
			else
			{
				Text.enableAutoSizing = true;
			}
		}
		Text.text = text;
		string text2 = (string)(object)Key;
		if (Key != null)
		{
			text2 = (string)text2._stringLength;
		}
		if (!string.IsNullOrEmpty(text2))
		{
			string text3 = Text.text;
			if (!string.IsNullOrEmpty(text3))
			{
				goto IL_0276;
			}
		}
		TextIdentifier key = Key;
		Transform transform = base.transform;
		string text5;
		if (transform != null)
		{
			string text4 = transform.name;
			Transform transform2 = transform;
			while (true)
			{
				Transform parent = transform2.parent;
				bool flag2 = parent != null;
				bool flag3 = !flag2;
				text5 = text4;
				if (flag3)
				{
					break;
				}
				Transform parent2 = transform2.parent;
				string text6 = parent2.name;
				text4 = text6 + "/" + text4;
				transform2 = parent2;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v551 @ rax_v31+B8]");
			object obj2 = 0;
			text5 = (string)obj2;
		}
		string message = "String [" + key.Key + "] Is Empty!. On Object: " + text5;
		Debug.LogError(message);
		goto IL_0276;
		IL_0276:
		if (OriginalFont != null)
		{
			TMP_FontAsset font = LocalisationManager.Instance.GetFont(OriginalFont);
			Text.font = font;
		}
	}

	private static string GetHierarchyPath(Transform transform)
	{
		//IL_0105: Expected O, but got I
		//IL_0115: Expected O, but got I
		if (transform != null)
		{
			if ((object)transform != null)
			{
				string text = transform.name;
				Transform transform2 = transform;
				while (true)
				{
					Transform parent = transform2.parent;
					if (parent != null)
					{
						Transform parent2 = transform2.parent;
						if ((object)parent2 == null)
						{
							break;
						}
						string text2 = parent2.name;
						text = text2 + "/" + text;
						transform2 = parent2;
						continue;
					}
					return text;
				}
			}
			return (string)(object)new NullReferenceException();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rax_v4+B8]");
		return (string)0;
	}
}
