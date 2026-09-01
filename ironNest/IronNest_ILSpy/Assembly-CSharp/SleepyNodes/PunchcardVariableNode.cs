using System;
using Cpp2ILInjected;

namespace SleepyNodes;

public class PunchcardVariableNode : Node
{
	public string ID;

	public PunchcardVariable.VariableTypes VariableType;

	public int VariableInt;

	public float VariableFloat;

	public string VariableText;

	public bool VariableBool;

	public GridReference VariableCoordinate;

	public ShellSlotPool.ShellSlotSides VariableShellSlot;

	public unsafe override object GetValue(NodePort port)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0067: Expected O, but got I
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected Ref, but got Unknown
		//IL_011c: Expected O, but got I4
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected Ref, but got Unknown
		//IL_01aa: Expected O, but got I4
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected Ref, but got Unknown
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Expected O, but got Unknown
		//IL_013a: Expected O, but got I
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Expected Ref, but got Unknown
		//IL_02bd: Expected O, but got I4
		//IL_01c8: Expected O, but got I
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected Ref, but got Unknown
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Expected O, but got Unknown
		//IL_024d: Expected O, but got I
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Expected Ref, but got Unknown
		//IL_03d0: Expected O, but got I4
		//IL_02db: Expected O, but got I
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Expected O, but got Unknown
		//IL_04f1: Expected I4, but got O
		//IL_0360: Expected O, but got I
		//IL_03ee: Expected O, but got I
		StateGraph stateGraph = (StateGraph)graph;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if ((object)graph != null)
		{
			nint num = (nint)typeof(StateGraph);
			nint num2 = (nint)stateGraph;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rdx_v3 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r8_v3 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rdx_v3 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r8_v3 (Il2CppClass<SleepyNodes.StateGraph>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v6+FFFFFFF8+v59 @ rax_v5*8]");
				if (0 == (nint)typeof(StateGraph))
				{
					if (port != null)
					{
						object obj3 = default(object);
						if (port._fieldName == "VariableInt")
						{
							bool flag = ((StateGraph)graph).TryGetVariable(ID, out *(int*)(obj3 - 48));
							bool flag2 = !flag;
							object obj4 = 0;
							if (!flag2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-30]");
								obj4 = 0;
							}
							object obj5 = obj3 + 56;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						}
						else
						{
							if (!(port._fieldName == "VariableFloat"))
							{
								if (port._fieldName == "VariableText")
								{
									if (((StateGraph)graph).TryGetVariable(ID, out *(string*)(obj3 - 32)))
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-20]");
										return 0;
									}
								}
								else
								{
									if (port._fieldName == "VariableBool")
									{
										bool flag3 = ((StateGraph)graph).TryGetVariable(ID, out *(bool*)(obj3 + 32));
										bool flag4 = !flag3;
										object obj6 = 0;
										if (!flag4)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+20]");
											obj6 = 0;
										}
										object obj7 = obj3 + 56;
										bool flag5 = obj6 == null;
										_ = !flag5;
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										goto IL_047a;
									}
									if (port._fieldName == "VariableCoordinate")
									{
										if (((StateGraph)graph).TryGetVariable(ID, out *(GridReference*)(obj3 - 24)))
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-18]");
											return 0;
										}
									}
									else if (port._fieldName == "VariableShellSlot")
									{
										bool flag6 = ((StateGraph)graph).TryGetVariable(ID, out *(ShellSlotPool.ShellSlotSides*)(obj3 - 40));
										bool flag7 = !flag6;
										object obj8 = 0;
										if (!flag7)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-28]");
											obj8 = 0;
										}
										object obj9 = obj3 + 56;
										return (ShellSlotPool.ShellSlotSides)obj9;
									}
								}
								goto IL_03f3;
							}
							bool flag8 = ((StateGraph)graph).TryGetVariable(ID, out *(float*)(obj3 - 44));
							bool flag9 = !flag8;
							object obj10 = 0;
							if (!flag9)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-2C]");
								obj10 = 0;
							}
							object obj11 = obj3 + 56;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						}
						goto IL_047a;
					}
					return new NullReferenceException();
				}
			}
		}
		goto IL_03f3;
		IL_03f3:
		return null;
		IL_047a:
		object result = default(object);
		return result;
	}
}
