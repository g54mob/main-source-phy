using Cpp2ILInjected;

namespace SleepyNodes;

public class State_ClearSignalAlarm : StateNode
{
	public enum Printers
	{
		None,
		Primary,
		Secondary
	}

	public StateNode To;

	private static readonly (Printers, Teleprinter.Teleprinters)[] Map;

	public Printers Printer;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		//IL_001d: Expected O, but got I4
		//IL_0027: Expected O, but got I4
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_0068: Expected I4, but got O
		NodeExecutionState state2 = default(NodeExecutionState);
		base.OnEnter(state2);
		(Printers, Teleprinter.Teleprinters)[] map = Map;
		object obj = Map + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < map.Length)
		{
			object obj4 = Printer & obj;
			if (obj4 == obj)
			{
				Teleprinter.Teleprinters type = (Teleprinter.Teleprinters)(obj >> 32);
				Teleprinter teleprinter = Teleprinter.GetTeleprinter(type);
				bool flag = teleprinter != null;
				state2 = null;
				if (flag)
				{
					teleprinter.ClearAlarm();
					state2 = null;
				}
			}
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
	}

	public override void OnExecute(NodeExecutionState state)
	{
		//IL_0038: Expected I, but got O
		//IL_0048: Expected O, but got I
		//IL_0058: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7A7]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ r9_v1 (Il2CppClass<SleepyNodes.State_ClearSignalAlarm>)+218]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ r9_v1 (Il2CppClass<SleepyNodes.State_ClearSignalAlarm>)+220]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v34 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	unsafe static State_ClearSignalAlarm()
	{
		(Printers, Teleprinter.Teleprinters)[] map = new(Printers, Teleprinter.Teleprinters)[2];
		object obj = default(object);
		object obj2 = default(object);
		(Printers, Teleprinter.Teleprinters) tuple = ((Printers)(int)(&obj), (Teleprinter.Teleprinters)(int)(&obj2));
		(Printers, Teleprinter.Teleprinters) tuple2 = ((Printers)(int)(&obj), (Teleprinter.Teleprinters)(int)(&obj2));
		_ = 0;
		Map = map;
	}
}
