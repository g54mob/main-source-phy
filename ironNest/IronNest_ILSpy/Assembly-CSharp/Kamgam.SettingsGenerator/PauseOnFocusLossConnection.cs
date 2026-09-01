using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class PauseOnFocusLossConnection : Connection<bool>
{
	public override bool Get()
	{
		return PauseManager._003CPauseOnFocusLoss_003Ek__BackingField;
	}

	public override void Set(bool value)
	{
		//IL_0018: Expected I, but got O
		//IL_0028: Expected O, but got I
		//IL_0038: Expected O, but got I
		while (true)
		{
			PauseManager._003CPauseOnFocusLoss_003Ek__BackingField = value;
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.PauseOnFocusLossConnection>)+258]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.PauseOnFocusLossConnection>)+260]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v78 @ rax_v6 (should have been resolved before IL gen)");
		}
	}
}
