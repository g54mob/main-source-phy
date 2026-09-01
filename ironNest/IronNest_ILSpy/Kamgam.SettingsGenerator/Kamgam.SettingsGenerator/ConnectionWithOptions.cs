using System.Collections.Generic;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public abstract class ConnectionWithOptions<TOption> : Connection<int>, IConnectionWithOptions<TOption>, IConnection<int>, IConnection, IQualityChangeReceiver
{
	public bool HasOptions()
	{
		//IL_0005: Expected I, but got O
		//IL_003f: Expected O, but got I
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionWithOptions`1<TOption>>)+2C8] (should have been resolved before IL gen)");
		bool flag = default(bool);
		if (!flag)
		{
			return flag;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal1 @ rax_v2 (System.Boolean)+18]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal1 @ rax_v2 (System.Boolean)+18]");
		object obj = num2 ^ 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal1 @ rax_v2 (System.Boolean)+18]");
		object obj2 = 0 & obj;
		bool flag2 = (nint)obj2 < 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal1 @ rax_v2 (System.Boolean)+18]");
		bool flag3 = (nint)0 < (nint)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal1 @ rax_v2 (System.Boolean)+18]");
		bool flag4 = (nint)0 == 0;
		bool flag5 = flag3 == flag2;
		bool flag6 = !flag4;
		return flag6 & flag5;
	}

	public abstract List<TOption> GetOptionLabels();

	public abstract void SetOptionLabels(List<TOption> optionLabels);

	public abstract void RefreshOptionLabels();

	protected ConnectionWithOptions()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
