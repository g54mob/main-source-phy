using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using SleepyNodes;

[Serializable]
public class ConditionSet
{
	[Serializable]
	public class ConditionPair
	{
		public enum Operation
		{
			Base,
			And,
			Or
		}

		public Operation operation;

		public Condition Condition;
	}

	public ConditionPair[] Conditions;

	public bool Resolve(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		//IL_0042: Expected O, but got I4
		//IL_004b: Expected O, but got I4
		//IL_005d: Expected O, but got I4
		//IL_0382: Expected I4, but got O
		//IL_0093: Expected O, but got I
		//IL_00c2: Expected I, but got O
		//IL_00ff: Expected I, but got O
		//IL_0126: Expected O, but got I
		//IL_015a: Expected I, but got O
		//IL_0199: Expected O, but got I
		//IL_01c5: Expected I, but got O
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Expected O, but got Unknown
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected O, but got Unknown
		//IL_020e: Expected O, but got I
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Expected O, but got Unknown
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Expected O, but got Unknown
		//IL_0243: Expected I, but got O
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Expected O, but got Unknown
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		ConditionPair[] conditions = Conditions;
		bool flag = Conditions == null;
		StateNode.NodeExecutionState nodeExecutionState2 = default(StateNode.NodeExecutionState);
		StateNode.NodeExecutionState nodeExecutionState = nodeExecutionState2;
		List<MapEntity> list2 = default(List<MapEntity>);
		List<MapEntity> list = list2;
		if (!flag)
		{
			object obj = 32;
			object obj2 = 0;
			bool flag2 = true;
			object obj3 = 0;
			while (true)
			{
				if ((nint)obj2 < conditions.Length)
				{
					if ((nint)obj3 < conditions.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rdi_v5+v176 @ rax_v12 (ConditionPair[])]");
						object obj4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rdi_v5+v176 @ rax_v12 (ConditionPair[])]");
						bool flag3 = (nint)0 == 0;
						nodeExecutionState = nodeExecutionState2;
						list = list2;
						nint num = (nint)conditions;
						if (flag3)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rax_v15+18]");
						bool flag4 = (nint)0 == 0;
						nodeExecutionState = nodeExecutionState2;
						list = list2;
						num = (nint)conditions;
						if (flag4)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rax_v15+18]");
						bool flag5 = ((Condition)0).Execute(nodeExecutionState2, list2);
						ConditionPair[] conditions2 = Conditions;
						bool flag6 = Conditions == null;
						nodeExecutionState = nodeExecutionState2;
						list = list2;
						num = unchecked((nint)null);
						if (flag6)
						{
							break;
						}
						if ((nint)obj3 < conditions2.Length)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rdi_v5+v46 @ rcx_v12 (ConditionPair[])]");
							object obj5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rdi_v5+v46 @ rcx_v12 (ConditionPair[])]");
							bool flag7 = (nint)0 == 0;
							nodeExecutionState = nodeExecutionState2;
							list = list2;
							num = unchecked((nint)null);
							if (flag7)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v13+10]");
							bool flag8 = (nint)0 == 0;
							if (!flag8)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v13+10]");
								object obj6 = -1;
								if (!flag8)
								{
									bool flag9 = (nint)obj6 != 1;
									nodeExecutionState = nodeExecutionState2;
									list = list2;
									num = unchecked((nint)null);
									if (flag9)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
										NotImplementedException ex = new NotImplementedException();
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
										throw ex;
									}
									flag2 |= flag5;
									conditions = Conditions;
									obj3++;
									obj += 8;
									obj2 = obj3;
								}
								else
								{
									if (!flag2 && !flag5)
									{
										return false;
									}
									flag2 &= flag5;
									conditions = Conditions;
									obj3++;
									obj += 8;
									obj2 = obj3;
								}
							}
							else
							{
								obj3++;
								conditions = Conditions;
								obj += 8;
								obj2 = obj3;
								flag2 = flag5;
							}
							continue;
						}
					}
					IndexOutOfRangeException ex2 = new IndexOutOfRangeException();
					return (byte)(int)ex2 != 0;
				}
				return flag2;
			}
		}
		throw new NullReferenceException();
	}

	public ConditionSet()
	{
		ConditionPair[] conditions = new ConditionPair[0];
		Conditions = conditions;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
