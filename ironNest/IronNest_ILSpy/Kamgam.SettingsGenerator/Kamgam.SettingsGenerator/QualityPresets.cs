using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class QualityPresets
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyValuePair<int, QualityPreset>, int> _003C_003E9__6_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal unsafe int _003CGetPresetList_003Eb__6_0(KeyValuePair<int, QualityPreset> kv)
		{
			//IL_0008: Expected O, but got Ref
			//IL_0033: Expected O, but got I
			//IL_007f: Expected O, but got Ref
			object obj2 = default(object);
			object obj = (object)(&obj2);
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v3 (Il2CppClass<System.Int32>)+FC]");
			object obj3 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v3 (Il2CppClass<System.Int32>)+FC]");
			if ((nint)obj3 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			}
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+38]");
			return 0;
		}
	}

	public static Dictionary<int, QualityPreset> Presets;

	public unsafe static void AddAllLevels()
	{
		int qualityLevel = QualitySettings.GetQualityLevel();
		string[] names = QualitySettings.names;
		int num = 0;
		int num2 = 0;
		string[] array = names;
		int num3 = default(int);
		while (num < array.Length)
		{
			QualitySettings.SetQualityLevel(num2, applyExpensiveChanges: false);
			int qualityLevel2 = QualitySettings.GetQualityLevel();
			bool flag = Presets.ContainsKey((int)(&num3));
			num3 = qualityLevel2;
			if (!flag)
			{
				QualityPreset value = QualityPreset.CreateFromCurrentLevel();
				Presets.Add((int)(&num3), value);
				num3 = qualityLevel2;
			}
			num2++;
			array = QualitySettings.names;
			num = num2;
		}
		QualitySettings.SetQualityLevel(qualityLevel);
	}

	public unsafe static void AddCurrentLevel()
	{
		int qualityLevel = QualitySettings.GetQualityLevel();
		int num = default(int);
		if (!Presets.ContainsKey((int)(&num)))
		{
			QualityPreset value = QualityPreset.CreateFromCurrentLevel();
			Presets.Add((int)(&num), value);
		}
	}

	public unsafe static void RestoreCurrentLevel()
	{
		int qualityLevel = QualitySettings.GetQualityLevel();
		int num = default(int);
		if (Presets.ContainsKey((int)(&num)))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
			QualityPreset qualityPreset = default(QualityPreset);
			qualityPreset.ApplyToCurrentLevel();
		}
	}

	public unsafe static void RestoreCurrentFrom(int level)
	{
		int num = default(int);
		if (Presets.ContainsKey((int)(&num)))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
			QualityPreset qualityPreset = default(QualityPreset);
			qualityPreset.ApplyToCurrentLevel();
		}
	}

	public unsafe static QualityPreset GetPreset(int level)
	{
		if (Presets != null)
		{
			int num = default(int);
			if (!Presets.ContainsKey((int)(&num)))
			{
				return null;
			}
			if (Presets != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
				QualityPreset result = default(QualityPreset);
				return result;
			}
		}
		return (QualityPreset)(object)new NullReferenceException();
	}

	public unsafe static List<QualityPreset> GetPresetList()
	{
		List<QualityPreset> list = new List<QualityPreset>();
		Func<KeyValuePair<int, QualityPreset>, int> selector = _003C_003Ec._003C_003E9__6_0;
		if (_003C_003Ec._003C_003E9__6_0 == null)
		{
			selector = (_003C_003Ec._003C_003E9__6_0 = delegate
			{
				//IL_0008: Expected O, but got Ref
				//IL_0033: Expected O, but got I
				//IL_007f: Expected O, but got Ref
				object obj2 = default(object);
				object obj = (object)(&obj2);
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v3 (Il2CppClass<System.Int32>)+FC]");
				object obj3 = (nint)0 + (nint)15;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v3 (Il2CppClass<System.Int32>)+FC]");
				if ((nint)obj3 > 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				}
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+38]");
				return 0;
			});
		}
		int num = Enumerable.Max(Presets, selector);
		bool flag = num <= 0;
		int num2 = 0;
		if (!flag)
		{
			int num3 = default(int);
			while (true)
			{
				if (Presets != null)
				{
					if (Presets.ContainsKey((int)(&num3)))
					{
						QualityPreset preset = GetPreset(num2);
						if (list == null)
						{
							goto IL_00f0;
						}
						list.Add(preset);
					}
					int num4 = num2 + 1;
					bool flag2 = num4 < num;
					num3 = num2;
					num2 = num4;
					if (!flag2)
					{
						break;
					}
					continue;
				}
				goto IL_00f0;
				IL_00f0:
				return (List<QualityPreset>)(object)new NullReferenceException();
			}
		}
		return list;
	}

	static QualityPresets()
	{
		Dictionary<int, QualityPreset> presets = new Dictionary<int, QualityPreset>();
		Presets = presets;
	}
}
