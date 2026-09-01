using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class AlteregoFSR2Connection : ConnectionWithOptions<string>
{
	protected List<string> _labels;

	public bool IsSupported()
	{
		return true;
	}

	public override List<string> GetOptionLabels()
	{
		Logger.LogWarning("AlteregoFSR2Connection: Alterego FSR is not yet set up. Please consult the Alterego Games Manual for more info and support.");
		return _labels;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		if (optionLabels != null)
		{
			_labels = optionLabels;
		}
		else
		{
			Debug.LogError("Invalid new labels. Need to be four.");
		}
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.AlteregoFSR2Connection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.AlteregoFSR2Connection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override int Get()
	{
		Logger.LogWarning("AlteregoFSR2Connection: Alterego FSR2 is not yet set up. Please consult the Alterego Games Manual for more info and support.");
		return 0;
	}

	public unsafe static T GetFieldValue<T>(object obj, string fieldName)
	{
		//IL_0008: Expected O, but got Ref
		//IL_007c: Expected O, but got I
		//IL_009a: Expected O, but got I
		//IL_026f: Expected O, but got I
		//IL_00c6: Expected O, but got I8
		//IL_02b3: Expected O, but got I
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Expected O, but got Unknown
		//IL_0301: Expected O, but got I
		//IL_022c: Expected O, but got Ref
		//IL_023c: Expected O, but got I
		//IL_016d: Expected O, but got I
		//IL_01a9: Expected O, but got I
		//IL_01e1: Expected O, but got I
		//IL_01f1: Expected O, but got I
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
		object obj4 = 0;
		object obj5 = obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
		object obj9;
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			object obj7 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			if ((nint)obj7 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			object obj8 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			if ((nint)obj8 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			obj9 = (nint)0 + (nint)15;
			object obj10 = obj9;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			if ((nint)obj10 > 0)
			{
				goto IL_0325;
			}
		}
		obj9 = 1152921504606846960L;
		goto IL_0325;
		IL_0241:
		return (T)new NullReferenceException();
		IL_0364:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
		IL_0325:
		object obj11 = obj9 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			object obj12 = default(object);
			if (obj12 != null)
			{
				object obj13 = obj12;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v230 @ r9_v2+6B8] (should have been resolved before IL gen)");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
				object obj14 = default(object);
				object obj20;
				object obj21;
				if (obj14 == null)
				{
					object obj15 = default(object);
					if (obj15 == null)
					{
						goto IL_0241;
					}
					object obj16 = obj15;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v307 @ r8_v12+2C8] (should have been resolved before IL gen)");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
					object obj17 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj18 = default(object);
					if (obj18 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
						object obj19 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A67B0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+58]");
						obj20 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
						obj21 = 0;
						goto IL_0364;
					}
				}
				else
				{
					Logger.LogError("TheNakedDev UpscalerController.qualityMode was not found. Maybe the internal API changed. Please contact TheNakedDev for support.");
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				obj20 = (object)(&obj3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
				obj21 = 0;
				goto IL_0364;
			}
		}
		goto IL_0241;
	}

	public override void Set(int index)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.AlteregoFSR2Connection>)+258]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.AlteregoFSR2Connection>)+260]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
