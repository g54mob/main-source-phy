using System;
using Cpp2ILInjected;
using SleepyNodes;

[Serializable]
public class ContextVariableOrInline<T>
{
	public enum SelectionTypes
	{
		Inline,
		Context
	}

	public SelectionTypes SelectionType;

	public T Value;

	public string ContextKey;

	public unsafe T Get(StateNode.NodeExecutionState state)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_002d: Expected O, but got I
		//IL_003d: Expected O, but got I
		//IL_0053: Expected O, but got I
		//IL_023a: Expected O, but got I
		//IL_0242: Expected O, but got Ref
		//IL_007f: Expected O, but got I8
		//IL_0281: Expected O, but got I
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Expected O, but got Unknown
		//IL_030e: Expected O, but got Ref
		//IL_0328: Expected O, but got I
		//IL_0338: Expected O, but got I
		//IL_02ca: Expected O, but got I
		//IL_0094: Expected O, but got I
		//IL_00f6: Expected O, but got I
		//IL_010e: Expected O, but got I
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Expected O, but got Unknown
		//IL_0379: Expected O, but got Ref
		//IL_00c4: Expected O, but got I
		//IL_00dc: Expected O, but got I
		//IL_0144: Expected O, but got I
		//IL_01a1: Expected O, but got I
		//IL_01b1: Expected O, but got I
		//IL_01c1: Expected O, but got I
		//IL_01d7: Expected O, but got I
		//IL_01f1: Expected O, but got Ref
		//IL_0390: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ r9_v1+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rax_v1+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ r10_v1+10]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v2+FC]");
		object obj8 = default(object);
		object obj10;
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v2+FC]");
			object obj7 = (nint)0 + (nint)15;
			obj8 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v2+FC]");
			if ((nint)obj7 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v2+FC]");
			object obj9 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v2+FC]");
			if ((nint)obj9 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v2+FC]");
			obj10 = (nint)0 + (nint)15;
			object obj11 = obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v2+FC]");
			if ((nint)obj11 > 0)
			{
				goto IL_02ee;
			}
		}
		obj10 = 1152921504606846960L;
		goto IL_02ee;
		IL_0395:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
		IL_02ee:
		object obj12 = obj10 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ r9_v1+20]");
		object obj13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rax_v20+C0]");
		object obj14 = 0;
		object obj15 = obj14;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ r9_v1+20]");
		object obj16 = 0;
		object obj17 = default(object);
		object obj20;
		object obj29;
		if (obj17 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rax_v22+C0]");
			object obj18 = 0;
			object obj19 = obj18;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rax_v39+80]");
			obj20 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rax_v22+C0]");
			object obj21 = 0;
			object obj22 = obj21;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rcx_v16+80]");
			obj20 = 0;
			if ((nint)obj17 == 1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rcx_v16+80]");
				object obj23 = (nint)0 + (nint)64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+58]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ r9_v1+20]");
					object obj24 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ rax_v36+C0]");
					object obj25 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v254 @ rcx_v20+10]");
					object obj26 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v255 @ rax_v37+28]");
					object obj27 = (nint)0 >> 31;
					bool flag = obj27 != null;
					object obj28 = (object)(&obj2);
					if (!flag)
					{
						obj28 = obj8;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180755750");
					obj29 = (object)(&obj2);
					goto IL_0395;
				}
				return (T)new NullReferenceException();
			}
		}
		object obj30 = obj20 + 32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		obj29 = (object)(&obj2);
		goto IL_0395;
	}
}
