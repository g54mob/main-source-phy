using System;
using Cpp2ILInjected;
using Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPunchcard : MonoBehaviour
{
	public TMP_Text nameText;

	public TMP_Text costText;

	public TMP_Text descriptionText;

	public TMP_Text UsesText;

	public Image iconImage;

	public string costFormat = "Cost: {0}";

	public TextIdentifier costFormatLoc;

	public string usesFormat;

	public TextIdentifier usesFormatLoc;

	public PunchcardDefinitionV2 CurrentDefinition;

	private void OnEnable()
	{
		Action value = UpdateVisuals;
		LocalisationManager.OnLanguageChanged += value;
	}

	private void OnDisable()
	{
		Action value = UpdateVisuals;
		LocalisationManager.OnLanguageChanged -= value;
	}

	public void Initialize(PunchcardDefinitionV2 def)
	{
		CurrentDefinition = def;
		UpdateVisuals();
	}

	public unsafe void UpdateVisuals()
	{
		//IL_00ac: Expected I, but got O
		//IL_012b: Expected I, but got O
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected I4, but got Unknown
		//IL_0285: Expected I, but got O
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected I4, but got Unknown
		//IL_036c: Expected I, but got O
		if (!(CurrentDefinition != null))
		{
			return;
		}
		PunchcardDefinitionV2 currentDefinition = CurrentDefinition;
		UnityEngine.Object obj = nameText;
		string text = currentDefinition.Title.Get();
		if (nameText != null)
		{
			bool flag = text == null;
			string text2 = "";
			if (!flag)
			{
				text2 = text;
			}
			nint num = (nint)obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v511 @ r8_v31 (Il2CppClass<UnityEngine.Object>)+558] (should have been resolved before IL gen)");
		}
		PunchcardDefinitionV2 currentDefinition2 = CurrentDefinition;
		UnityEngine.Object obj2 = descriptionText;
		string text3 = currentDefinition2.Description.Get();
		if (descriptionText != null)
		{
			bool flag2 = text3 == null;
			string text4 = "";
			if (!flag2)
			{
				text4 = text3;
			}
			nint num2 = (nint)obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v594 @ r8_v29 (Il2CppClass<UnityEngine.Object>)+558] (should have been resolved before IL gen)");
		}
		PunchcardDefinitionV2 currentDefinition3 = CurrentDefinition;
		if (iconImage != null)
		{
			iconImage.sprite = currentDefinition3.Icon;
			bool flag3 = currentDefinition3.Icon != null;
			iconImage.enabled = flag3;
		}
		bool flag4 = costFormatLoc.TryGet(out var text5);
		string text6 = text5;
		if (!flag4)
		{
			text6 = costFormat;
		}
		UnityEngine.Object obj3 = costText;
		bool flag5 = string.IsNullOrEmpty(text6);
		PunchcardDefinitionV2 currentDefinition4 = CurrentDefinition;
		string text7;
		if (flag5)
		{
			int num3 = currentDefinition4 + 32;
			text7 = ((int*)num3)->ToString();
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			text7 = string.Format(text6, arg);
			int cost = currentDefinition4.Cost;
		}
		if (costText != null)
		{
			bool flag6 = text7 == null;
			string text8 = "";
			if (!flag6)
			{
				text8 = text7;
			}
			nint num4 = (nint)obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v777 @ r8_v23 (Il2CppClass<UnityEngine.Object>)+558] (should have been resolved before IL gen)");
		}
		bool flag7 = usesFormatLoc.TryGet(out var text9);
		string text10 = text9;
		if (!flag7)
		{
			text10 = usesFormat;
		}
		UnityEngine.Object usesText = UsesText;
		string text11;
		if (string.IsNullOrEmpty(text10))
		{
			int num5 = CurrentDefinition + 100;
			text11 = ((int*)num5)->ToString();
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = default(object);
			text11 = string.Format(text10, arg2);
		}
		if (UsesText != null)
		{
			bool flag8 = text11 == null;
			string text12 = "";
			if (!flag8)
			{
				text12 = text11;
			}
			nint num6 = (nint)usesText;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v885 @ r8_v20 (Il2CppClass<UnityEngine.Object>)+558] (should have been resolved before IL gen)");
		}
	}

	private void SetSafe(TMP_Text txt, string value)
	{
		if (txt != null)
		{
			bool flag = value == null;
			string text = "";
			if (!flag)
			{
				text = value;
			}
			txt.text = text;
		}
	}

	private void SetIcon(Sprite sprite)
	{
		if (iconImage != null)
		{
			iconImage.sprite = sprite;
			bool flag = sprite != null;
			iconImage.enabled = flag;
		}
	}

	public UIPunchcard()
	{
		TextIdentifier textIdentifier = new TextIdentifier();
		costFormatLoc = textIdentifier;
		usesFormat = "Uses: {0}";
		usesFormatLoc = new TextIdentifier();
		base._002Ector();
	}
}
