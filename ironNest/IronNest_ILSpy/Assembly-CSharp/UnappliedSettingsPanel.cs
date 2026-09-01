using System;
using System.Collections.Generic;
using System.Text;
using Cpp2ILInjected;
using Kamgam.SettingsGenerator;
using Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UnappliedSettingsPanel : MonoBehaviour
{
	private TMP_Text _unappliedSettingsText;

	private SettingsLocalizationKeys _settingsLocalizationKeys;

	private UnityEvent _onHide;

	private UnityEvent _onShow;

	public void Show(List<ISetting> unappliedSettings)
	{
		SetUnappliedSettings(unappliedSettings);
		GameObject gameObject = base.gameObject;
		gameObject.SetActive(value: true);
		if (_onShow != null)
		{
			_onShow.Invoke();
		}
	}

	public void Hide()
	{
		GameObject gameObject = base.gameObject;
		gameObject.SetActive(value: false);
		if (_onHide != null)
		{
			_onHide.Invoke();
		}
	}

	private unsafe void SetUnappliedSettings(List<ISetting> unappliedSettings)
	{
		//IL_0399: Expected O, but got I
		//IL_03a9: Expected O, but got I
		//IL_0049: Expected O, but got I4
		//IL_051e: Expected O, but got Ref
		//IL_007a: Expected O, but got Ref
		//IL_0090: Expected I, but got O
		//IL_0119: Expected O, but got I4
		//IL_00c7: Expected O, but got I
		//IL_01f0: Expected O, but got I
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_0227: Expected O, but got I4
		//IL_01c8: Expected O, but got I4
		if (unappliedSettings != null && unappliedSettings._size != 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			object obj = 0;
			List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
			string text = default(string);
			object obj12 = default(object);
			string settingId = default(string);
			object obj13 = default(object);
			string text3 = default(string);
			string key = default(string);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				SettingsLocalizationKeys settingsLocalizationKeys = _settingsLocalizationKeys;
				bool flag = text == null;
				string text2 = (string)(&enumerator);
				object obj3;
				object obj11;
				if (!flag)
				{
					nint num = (nint)text;
					object obj2 = obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ r10_v11 (Il2CppClass<System.String>)+12E]");
					if ((nint)obj2 >= 0)
					{
						goto IL_0106;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ r10_v11 (Il2CppClass<System.String>)+B0]");
					obj3 = 0;
					object obj4 = obj;
					while (true)
					{
						object obj5 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v619 @ r8_v17+v730 @ rax_v61*8]");
						if (0 == (nint)typeof(ISetting))
						{
							break;
						}
						obj4++;
						object obj6 = obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ r10_v11 (Il2CppClass<System.String>)+12E]");
						if ((nint)obj6 < 0)
						{
							continue;
						}
						goto IL_0106;
					}
					object obj7 = obj4 + obj4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v619 @ r8_v17+8+v790 @ rcx_v47*8]");
					object obj8 = (nint)0 + (nint)4;
					object obj9 = obj8 << 4;
					object obj10 = obj9 + 312;
					obj11 = obj10 + num;
					goto IL_04ad;
				}
				throw new NullReferenceException();
				IL_0106:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj3 = 4;
				obj11 = obj12;
				goto IL_04ad;
				IL_04ad:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v797 @ rdx_v23] (should have been resolved before IL gen)");
				if ((object)_settingsLocalizationKeys == null)
				{
					throw new NullReferenceException();
				}
				SettingsLocalizationKeys._003C_003Ec__DisplayClass1_0 CS_0024_003C_003E8__locals3 = new SettingsLocalizationKeys._003C_003Ec__DisplayClass1_0();
				if (CS_0024_003C_003E8__locals3 != null)
				{
					CS_0024_003C_003E8__locals3.settingId = settingId;
					Predicate<SettingLocalization> predicate = delegate(SettingLocalization x)
					{
						//IL_0048: Expected I4, but got O
						if (x == null)
						{
							NullReferenceException ex = new NullReferenceException();
							return (byte)(int)ex != 0;
						}
						return x._id == CS_0024_003C_003E8__locals3.settingId;
					};
					if (settingsLocalizationKeys._settingsLocalizationKeys != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A4610");
						if (obj13 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ stack_-88+18]");
							if ((nint)0 == 0)
							{
								throw new NullReferenceException();
							}
							obj = 0;
						}
						else
						{
							obj = 0;
						}
						if (obj13 == null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							string value = "- " + text3;
							bool flag2 = stringBuilder == null;
							text2 = "- ";
							if (!flag2)
							{
								StringBuilder stringBuilder2 = stringBuilder.AppendLine(value);
								continue;
							}
							throw new NullReferenceException();
						}
						if ((object)LocalisationManager.Instance != null)
						{
							string text4 = LocalisationManager.Instance.Get(key);
							string value2 = "- " + text4;
							bool flag3 = stringBuilder == null;
							text2 = "- ";
							if (!flag3)
							{
								StringBuilder stringBuilder3 = stringBuilder.AppendLine(value2);
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			bool flag4 = stringBuilder == null;
			List<ISetting>.Enumerator enumerator2 = (List<ISetting>.Enumerator)(&enumerator);
			if (!flag4)
			{
				string text5 = stringBuilder.ToString();
				if ((object)_unappliedSettingsText != null)
				{
					_unappliedSettingsText.text = text5;
					return;
				}
			}
		}
		else
		{
			List<ISetting>.Enumerator enumerator2 = (List<ISetting>.Enumerator)_unappliedSettingsText;
			if ((object)_unappliedSettingsText != null)
			{
				object obj14 = enumerator2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj15 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rax_v22+B8]");
				object obj16 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v96 @ r8_v10+558] (should have been resolved before IL gen)");
				return;
			}
		}
		throw new NullReferenceException();
	}
}
