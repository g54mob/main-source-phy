using System;
using Cpp2ILInjected;

[Serializable]
public class MedalConditionSet
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

		public MedalCondition Condition;
	}

	public ConditionPair[] Conditions;

	public bool Resolve(MedalTrackedValues values)
	{
		//IL_002c: Expected O, but got I4
		//IL_0047: Expected O, but got I8
		//IL_0058: Expected O, but got I4
		//IL_0061: Expected O, but got I4
		//IL_0073: Expected O, but got I4
		//IL_043d: Expected I4, but got O
		//IL_00d2: Expected O, but got I
		//IL_0107: Expected O, but got I
		//IL_0166: Expected O, but got I
		//IL_0192: Expected O, but got I4
		//IL_01b5: Expected O, but got I
		//IL_01c9: Expected O, but got I
		//IL_0466: Expected O, but got I4
		//IL_0208: Expected O, but got I8
		//IL_0243: Expected O, but got I
		//IL_0268: Expected O, but got I4
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Expected O, but got Unknown
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Expected O, but got Unknown
		//IL_02b1: Expected O, but got I
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Expected O, but got Unknown
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Expected O, but got Unknown
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Expected O, but got Unknown
		//IL_02fc: Expected O, but got I4
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Expected O, but got Unknown
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Expected O, but got Unknown
		//IL_0343: Expected O, but got I4
		ConditionPair[] conditions = Conditions;
		bool flag = Conditions == null;
		MedalTrackedValues medalTrackedValues = values;
		object obj = 0;
		if (!flag)
		{
			object obj2 = 6442450944L;
			medalTrackedValues = values;
			object obj3 = 32;
			obj = 0;
			bool flag2 = true;
			object obj4 = 0;
			ConditionPair[] conditions3 = default(ConditionPair[]);
			while (true)
			{
				if ((nint)obj < conditions.Length)
				{
					ConditionPair[] conditions2 = Conditions;
					if (Conditions == null)
					{
						break;
					}
					if ((nint)obj4 < conditions2.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rdi_v5+v156 @ rax_v15 (ConditionPair[])]");
						object obj5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rdi_v5+v156 @ rax_v15 (ConditionPair[])]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rbp_v7+18]");
						object obj6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rbp_v7+18]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbp_v8+10]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbp_v8+10]");
						float num = ((MedalNumberExpression)0).Resolve(values);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbp_v8+20]");
						bool flag3 = (nint)0 == 0;
						medalTrackedValues = values;
						obj = 0;
						if (flag3)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbp_v8+20]");
						num = ((MedalNumberExpression)0).Resolve(values);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbp_v8+18]");
						object obj7 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbp_v8+18]");
						if ((nint)0 <= (nint)5)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ r12_v4+422830+v157 @ rax_v16*4]");
							object obj8 = 0 + 6442450944L;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v351 @ rcx_v18 (should have been resolved before IL gen)");
						}
						else
						{
							conditions3 = Conditions;
							bool flag4 = Conditions == null;
							medalTrackedValues = null;
							obj = 0;
							if (flag4)
							{
								break;
							}
						}
						if ((nint)obj4 < conditions3.Length)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rdi_v5+v53 @ rcx_v13 (ConditionPair[])]");
							object obj9 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rdi_v5+v53 @ rcx_v13 (ConditionPair[])]");
							bool flag5 = (nint)0 == 0;
							medalTrackedValues = null;
							obj = 0;
							if (flag5)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rcx_v14+10]");
							bool flag6 = (nint)0 == 0;
							if (!flag6)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rcx_v14+10]");
								object obj10 = -1;
								if (!flag6)
								{
									object obj11 = obj10 - 1;
									bool flag7 = obj11 == null;
									bool flag8 = (nint)obj10 != 1;
									medalTrackedValues = null;
									obj = 0;
									if (flag8)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
										NotImplementedException ex = new NotImplementedException();
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
										throw ex;
									}
									conditions = Conditions;
									bool flag9 = !flag7;
									obj4++;
									obj3 += 8;
									medalTrackedValues = (MedalTrackedValues)flag2;
									obj = obj4;
									flag2 = flag9;
								}
								else
								{
									if (!flag2)
									{
										return false;
									}
									conditions = Conditions;
									obj4++;
									obj3 += 8;
									medalTrackedValues = null;
									obj = obj4;
									flag2 = false;
								}
							}
							else
							{
								conditions = Conditions;
								obj4++;
								obj3 += 8;
								medalTrackedValues = null;
								obj = obj4;
								flag2 = false;
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

	public MedalConditionSet()
	{
		ConditionPair[] conditions = new ConditionPair[0];
		Conditions = conditions;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
