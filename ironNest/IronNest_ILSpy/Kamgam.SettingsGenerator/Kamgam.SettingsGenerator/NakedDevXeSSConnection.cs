using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class NakedDevXeSSConnection : ConnectionWithOptions<string>
{
	protected List<string> _labels;

	public NakedDevXeSSConnection()
	{
		Logger.LogWarning("NakedDevXeSSConnection: XeSS is not yet set up. Please consult The Naked Dev Games Manual for more info and support. https://docs.google.com/document/d/1nb1cdNNc9zzmvbDbwPERKm21g_Cp8o9V2sjVV1JsQNM");
	}

	public bool IsSupported()
	{
		return false;
	}

	public override List<string> GetOptionLabels()
	{
		Logger.LogWarning("NakedDevXeSSConnection: The Naked Dev XeSS is not yet set up. Please consult the Naked Dev Manual for more info and support. https://docs.google.com/document/d/1nb1cdNNc9zzmvbDbwPERKm21g_Cp8o9V2sjVV1JsQNM");
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.NakedDevXeSSConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.NakedDevXeSSConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override int Get()
	{
		Logger.LogWarning("NakedDevXeSSConnection: The Naked Dev XeSS is not yet set up. Please consult the The Naked Dev Manual for more info and support. https://docs.google.com/document/d/1nb1cdNNc9zzmvbDbwPERKm21g_Cp8o9V2sjVV1JsQNM");
		return 0;
	}

	public override void Set(int index)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.NakedDevXeSSConnection>)+258]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.NakedDevXeSSConnection>)+260]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
