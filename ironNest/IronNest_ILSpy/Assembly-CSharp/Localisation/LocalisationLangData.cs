using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

namespace Localisation;

public class LocalisationLangData : ScriptableObject
{
	[Serializable]
	public class LangData
	{
		public string Lang;

		public SystemLanguage UnityLang;

		public string DisplayName;

		public string LangCode;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<LangData, string> _003C_003E9__10_0;

		public static Func<IGrouping<string, LangData>, string> _003C_003E9__10_1;

		public static Func<IGrouping<string, LangData>, LangData> _003C_003E9__10_2;

		public static Func<LangData, SystemLanguage> _003C_003E9__10_3;

		public static Func<IGrouping<SystemLanguage, LangData>, SystemLanguage> _003C_003E9__10_4;

		public static Func<IGrouping<SystemLanguage, LangData>, LangData> _003C_003E9__10_5;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003CInit_003Eb__10_0(LangData x)
		{
			if (x != null)
			{
				return x.Lang;
			}
			return (string)(object)new NullReferenceException();
		}

		internal string _003CInit_003Eb__10_1(IGrouping<string, LangData> x)
		{
			//IL_000d: Expected I, but got O
			//IL_00a7: Expected O, but got I
			//IL_00d4: Expected O, but got I
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Expected O, but got Unknown
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Expected O, but got Unknown
			//IL_0107: Expected O, but got I
			//IL_0045: Expected O, but got I
			//IL_004e: Expected O, but got I4
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Expected O, but got Unknown
			nint num;
			object obj2 = default(object);
			if (x != null)
			{
				num = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ r10_v1 (Il2CppClass<System.Linq.IGrouping`2<System.String, Localisation.LocalisationLangData+LangData>>)+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_0085;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ r10_v1 (Il2CppClass<System.Linq.IGrouping`2<System.String, Localisation.LocalisationLangData+LangData>>)+B0]");
				object obj = 0;
				obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ r8_v4+v56 @ rax_v7*8]");
					if (0 == (nint)typeof(IGrouping<string, LangData>))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ r10_v1 (Il2CppClass<System.Linq.IGrouping`2<System.String, Localisation.LocalisationLangData+LangData>>)+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_0085;
				}
				goto IL_00b1;
			}
			goto IL_0111;
			IL_0111:
			return (string)(object)new NullReferenceException();
			IL_0085:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			object obj6 = default(object);
			object obj5 = obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v4+8]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v110 @ r8_v3 (should have been resolved before IL gen)");
			goto IL_00b1;
			IL_00b1:
			object obj8 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ r8_v4+8+v139 @ rcx_v8*8]");
			object obj9 = (nint)0 << 4;
			object obj10 = obj9 + 312;
			object obj11 = obj10 + num;
			object obj12 = obj11;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v11+8]");
			object obj13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v114 @ r8_v5 (should have been resolved before IL gen)");
			goto IL_0111;
		}

		internal LangData _003CInit_003Eb__10_2(IGrouping<string, LangData> x)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AFA60");
			LangData result = default(LangData);
			return result;
		}

		internal SystemLanguage _003CInit_003Eb__10_3(LangData x)
		{
			//IL_0035: Expected I4, but got O
			if (x != null)
			{
				return x.UnityLang;
			}
			NullReferenceException ex = new NullReferenceException();
			return (SystemLanguage)ex;
		}

		internal SystemLanguage _003CInit_003Eb__10_4(IGrouping<SystemLanguage, LangData> x)
		{
			//IL_0022: Expected I4, but got O
			if (x != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				SystemLanguage result = default(SystemLanguage);
				return result;
			}
			NullReferenceException ex = new NullReferenceException();
			return (SystemLanguage)ex;
		}

		internal LangData _003CInit_003Eb__10_5(IGrouping<SystemLanguage, LangData> x)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AFA60");
			LangData result = default(LangData);
			return result;
		}
	}

	private static LocalisationLangData _instance;

	private const string DefaultLanguage = "English";

	public List<LangData> SupportedLanguages;

	private Dictionary<string, LangData> LookupByLang;

	private Dictionary<SystemLanguage, LangData> LookupByUnityLang;

	private Dictionary<string, LangData> LookupByDisplayName;

	private Dictionary<string, LangData> LookupByLangCode;

	public static LocalisationLangData Instance
	{
		get
		{
			if (_instance == null)
			{
				LocalisationLangData[] array = Resources.LoadAll<LocalisationLangData>("");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
				LocalisationLangData instance = default(LocalisationLangData);
				_instance = instance;
			}
			return _instance;
		}
	}

	public unsafe void Init()
	{
		//IL_0138: Expected O, but got Ref
		Func<LangData, string> keySelector = _003C_003Ec._003C_003E9__10_0;
		if (_003C_003Ec._003C_003E9__10_0 == null)
		{
			keySelector = (_003C_003Ec._003C_003E9__10_0 = (LangData x) => (string)((x != null) ? ((object)x.Lang) : ((object)new NullReferenceException())));
		}
		IEnumerable<IGrouping<string, LangData>> source = Enumerable.GroupBy(SupportedLanguages, keySelector);
		Func<IGrouping<string, LangData>, string> keySelector2 = _003C_003Ec._003C_003E9__10_1;
		if (_003C_003Ec._003C_003E9__10_1 == null)
		{
			keySelector2 = (_003C_003Ec._003C_003E9__10_1 = delegate(IGrouping<string, LangData> x)
			{
				//IL_000d: Expected I, but got O
				//IL_00a7: Expected O, but got I
				//IL_00d4: Expected O, but got I
				//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
				//IL_00e2: Expected O, but got Unknown
				//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ef: Expected O, but got Unknown
				//IL_0107: Expected O, but got I
				//IL_0045: Expected O, but got I
				//IL_004e: Expected O, but got I4
				//IL_005c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0061: Expected O, but got Unknown
				nint num;
				object obj2 = default(object);
				if (x != null)
				{
					num = (nint)x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ r10_v1 (Il2CppClass<System.Linq.IGrouping`2<System.String, Localisation.LocalisationLangData+LangData>>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_0085;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ r10_v1 (Il2CppClass<System.Linq.IGrouping`2<System.String, Localisation.LocalisationLangData+LangData>>)+B0]");
					object obj = 0;
					obj2 = 0;
					while (true)
					{
						object obj3 = obj2 + obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ r8_v4+v56 @ rax_v7*8]");
						if (0 == (nint)typeof(IGrouping<string, LangData>))
						{
							break;
						}
						obj2++;
						object obj4 = obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ r10_v1 (Il2CppClass<System.Linq.IGrouping`2<System.String, Localisation.LocalisationLangData+LangData>>)+12E]");
						if ((nint)obj4 < 0)
						{
							continue;
						}
						goto IL_0085;
					}
					goto IL_00b1;
				}
				goto IL_0111;
				IL_0111:
				return (string)(object)new NullReferenceException();
				IL_0085:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				object obj6 = default(object);
				object obj5 = obj6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v4+8]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v110 @ r8_v3 (should have been resolved before IL gen)");
				goto IL_00b1;
				IL_00b1:
				object obj8 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ r8_v4+8+v139 @ rcx_v8*8]");
				object obj9 = (nint)0 << 4;
				object obj10 = obj9 + 312;
				object obj11 = obj10 + num;
				object obj12 = obj11;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v11+8]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v114 @ r8_v5 (should have been resolved before IL gen)");
				goto IL_0111;
			});
		}
		Func<IGrouping<string, LangData>, LangData> elementSelector = _003C_003Ec._003C_003E9__10_2;
		if (_003C_003Ec._003C_003E9__10_2 == null)
		{
			elementSelector = (_003C_003Ec._003C_003E9__10_2 = delegate
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AFA60");
				LangData result = default(LangData);
				return result;
			});
		}
		Dictionary<string, LangData> lookupByLang = Enumerable.ToDictionary(source, keySelector2, elementSelector);
		LookupByLang = lookupByLang;
		Func<LangData, SystemLanguage> keySelector3 = _003C_003Ec._003C_003E9__10_3;
		if (_003C_003Ec._003C_003E9__10_3 == null)
		{
			keySelector3 = (_003C_003Ec._003C_003E9__10_3 = delegate(LangData x)
			{
				//IL_0035: Expected I4, but got O
				if (x == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (SystemLanguage)ex;
				}
				return x.UnityLang;
			});
		}
		IEnumerable<IGrouping<SystemLanguage, LangData>> source2 = Enumerable.GroupBy(SupportedLanguages, keySelector3);
		Func<IGrouping<SystemLanguage, LangData>, SystemLanguage> keySelector4 = _003C_003Ec._003C_003E9__10_4;
		if (_003C_003Ec._003C_003E9__10_4 == null)
		{
			keySelector4 = (_003C_003Ec._003C_003E9__10_4 = delegate(IGrouping<SystemLanguage, LangData> x)
			{
				//IL_0022: Expected I4, but got O
				if (x == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (SystemLanguage)ex;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				SystemLanguage result = default(SystemLanguage);
				return result;
			});
		}
		Func<IGrouping<SystemLanguage, LangData>, LangData> elementSelector2 = _003C_003Ec._003C_003E9__10_5;
		if (_003C_003Ec._003C_003E9__10_5 == null)
		{
			elementSelector2 = (_003C_003Ec._003C_003E9__10_5 = delegate
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AFA60");
				LangData result = default(LangData);
				return result;
			});
		}
		Dictionary<SystemLanguage, LangData> lookupByUnityLang = Enumerable.ToDictionary(source2, keySelector4, elementSelector2);
		LookupByUnityLang = lookupByUnityLang;
		Dictionary<string, LangData> lookupByDisplayName = new Dictionary<string, LangData>(StringComparer.s_ordinalIgnoreCase);
		LookupByDisplayName = lookupByDisplayName;
		Dictionary<string, LangData> lookupByLangCode = new Dictionary<string, LangData>(StringComparer.s_ordinalIgnoreCase);
		LookupByLangCode = lookupByLangCode;
		if (SupportedLanguages != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<LangData>.Enumerator enumerator = default(List<LangData>.Enumerator);
			LangData langData = default(LangData);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = langData == null;
				string text = (string)(&enumerator);
				if (!flag)
				{
					if (!string.IsNullOrWhiteSpace(langData.DisplayName))
					{
						keySelector4 = (Func<IGrouping<SystemLanguage, LangData>, SystemLanguage>)(object)LookupByDisplayName;
						text = langData.DisplayName;
						string key = Clean(langData.DisplayName);
						if (LookupByDisplayName == null)
						{
							throw new NullReferenceException();
						}
						LookupByDisplayName.set_Item(key, langData);
					}
					if (!string.IsNullOrWhiteSpace(langData.LangCode))
					{
						string key2 = Clean(langData.LangCode);
						if (LookupByLangCode == null)
						{
							throw new NullReferenceException();
						}
						LookupByLangCode.set_Item(key2, langData);
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			if (LookupByLang != null)
			{
				if (LookupByLang.ContainsKey("English"))
				{
					return;
				}
				LangData langData2 = new LangData();
				if (langData2 != null)
				{
					langData2.Lang = "English";
					langData2.DisplayName = "English";
					langData2.LangCode = "en";
					langData2.UnityLang = SystemLanguage.English;
					if (LookupByLang != null)
					{
						LookupByLang.set_Item("English", langData2);
						return;
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	public LangData GetLanguageData(string language)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A62A]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		LangData value;
		if (language != null)
		{
			string text = language.Trim();
			if (text != null)
			{
				string key = text.Replace("-", "_");
				if (LookupByLang != null)
				{
					if (LookupByLang.TryGetValue(key, out value))
					{
						goto IL_014b;
					}
					if (LookupByDisplayName != null)
					{
						if (LookupByDisplayName.TryGetValue(key, out value))
						{
							goto IL_014b;
						}
						if (LookupByLangCode != null)
						{
							if (LookupByLangCode.TryGetValue(key, out value))
							{
								goto IL_014b;
							}
							if (LookupByLang != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
								LangData result = default(LangData);
								return result;
							}
						}
					}
				}
			}
		}
		return (LangData)(object)new NullReferenceException();
		IL_014b:
		return value;
	}

	public unsafe LangData GetLanguageData(SystemLanguage language)
	{
		if (LookupByUnityLang != null)
		{
			object obj = default(object);
			if (LookupByUnityLang.TryGetValue((SystemLanguage)(int)(&obj), out var value))
			{
				return value;
			}
			if (LookupByLang != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
				LangData result = default(LangData);
				return result;
			}
		}
		return (LangData)(object)new NullReferenceException();
	}

	private static string Clean(string value)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A62A]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (value != null)
		{
			string text = value.Trim();
			if (text != null)
			{
				return text.Replace("-", "_");
			}
		}
		return (string)(object)new NullReferenceException();
	}

	public LocalisationLangData()
	{
		List<LangData> supportedLanguages = new List<LangData>();
		SupportedLanguages = supportedLanguages;
		base._002Ector();
	}
}
