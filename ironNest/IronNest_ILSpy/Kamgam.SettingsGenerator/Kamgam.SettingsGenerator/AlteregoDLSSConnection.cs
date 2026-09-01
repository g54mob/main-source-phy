using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class AlteregoDLSSConnection : ConnectionWithOptions<string>
{
	protected List<string> _labels;

	public bool CheckForCameraMarker = true;

	protected List<int> _enumOptionsAsIntegers;

	public AlteregoDLSSConnection()
	{
		List<int> enumOptionsAsIntegers = new List<int>(6);
		_enumOptionsAsIntegers = enumOptionsAsIntegers;
		base._002Ector();
		Logger.LogWarning("AlteregoDLSSConnection: Alterego DLSS is not yet set up. Please add DLSS as a post processing effect and install the NVIDIA package. Please contact Alterego Games for more info and support.");
	}

	public bool IsSupported()
	{
		return false;
	}

	public override List<string> GetOptionLabels()
	{
		Logger.LogWarning("AlteregoDLSSConnection: Alterego DLSS is not yet set up. Please add DLSS as a post processing effect and install the NVIDIA package. Please contact Alterego Games for more info and support.");
		return _labels;
	}

	protected List<int> getOptionsEnumList()
	{
		Logger.LogWarning("AlteregoDLSSConnection: Alterego DLSS is not yet set up. Please add DLSS as a post processing effect and install the NVIDIA package. Please contact Alterego Games for more info and support.");
		return _enumOptionsAsIntegers;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		if (optionLabels != null && optionLabels._size == 4)
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.AlteregoDLSSConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.AlteregoDLSSConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override int Get()
	{
		Logger.LogWarning("AlteregoDLSSConnection: Alterego DLSS is not yet set up. Please add DLSS as a post processing effect and install the NVIDIA package. Please contact Alterego Games for more info and support.");
		return 0;
	}

	public override void Set(int index)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.AlteregoDLSSConnection>)+258]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.AlteregoDLSSConnection>)+260]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
