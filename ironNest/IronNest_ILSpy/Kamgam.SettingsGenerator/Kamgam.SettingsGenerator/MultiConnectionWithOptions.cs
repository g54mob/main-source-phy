using System;
using System.Collections.Generic;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class MultiConnectionWithOptions<TOption> : MultiConnection<int>, IConnectionWithOptions<TOption>, IConnection<int>, IConnection, IQualityChangeReceiver
{
	public bool HasOptions()
	{
		//IL_0232: Expected I4, but got O
		//IL_002d: Expected O, but got I
		//IL_003d: Expected O, but got I
		//IL_0079: Expected O, but got I
		//IL_0089: Expected O, but got I
		//IL_00a8: Expected O, but got I
		//IL_00b8: Expected O, but got I
		//IL_00cd: Expected O, but got I
		//IL_00d6: Expected O, but got I4
		//IL_01a7: Expected O, but got I
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_015c: Expected O, but got I
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		nint num = 0;
		IntPtr intPtr = num;
		if (this != null)
		{
			IConnection<int> defaultConnection = GetDefaultConnection();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rdi_v1 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnectionWithOptions`1>>)+20]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v4+C0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj3 = default(object);
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rdi_v1 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnectionWithOptions`1>>)+20]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rax_v9+C0]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rdi_v1 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnectionWithOptions`1>>)+20]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ rcx_v10+C0]");
				object obj7 = 0;
				object obj9 = default(object);
				object obj8 = obj9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r10_v1+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_010d;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r10_v1+B0]");
				object obj10 = 0;
				object obj11 = 0;
				while (true)
				{
					object obj12 = obj11 + obj11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v96 @ r9_v1+v201 @ rax_v22*8]");
					nint num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rdx_v7+18]");
					if (num2 == 0)
					{
						break;
					}
					obj11++;
					object obj13 = obj11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r10_v1+12E]");
					if ((nint)obj13 < 0)
					{
						continue;
					}
					goto IL_010d;
				}
				object obj14 = obj11 + obj11;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v96 @ r9_v1+8+v251 @ rcx_v19*8]");
				object obj15 = (nint)0 + (nint)1;
				object obj16 = obj15 << 4;
				object obj17 = obj16 + 312;
				object obj18 = obj17 + obj8;
				goto IL_011c;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_010d:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_011c;
		IL_011c:
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v253 @ rax_v14] (should have been resolved before IL gen)");
		bool flag = default(bool);
		if (!flag)
		{
			return flag;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal2 @ rax_v15 (System.Boolean)+18]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal2 @ rax_v15 (System.Boolean)+18]");
		object obj19 = num3 ^ 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal2 @ rax_v15 (System.Boolean)+18]");
		object obj20 = 0 & obj19;
		bool flag2 = (nint)obj20 < 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal2 @ rax_v15 (System.Boolean)+18]");
		bool flag3 = (nint)0 < (nint)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal2 @ rax_v15 (System.Boolean)+18]");
		bool flag4 = (nint)0 == 0;
		bool flag5 = flag3 == flag2;
		bool flag6 = !flag4;
		return flag6 & flag5;
	}

	public List<TOption> GetOptionLabels()
	{
		//IL_00d8: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_007a: Expected O, but got I4
		//IL_0105: Expected O, but got I
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		object obj2;
		object obj5 = default(object);
		if (this != null)
		{
			IConnection<int> defaultConnection = GetDefaultConnection();
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj = default(object);
			if (obj != null)
			{
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				nint num3 = 0;
				object obj3 = default(object);
				obj2 = obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ r10_v1+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00b1;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ r10_v1+B0]");
				object obj4 = 0;
				obj5 = 0;
				while (true)
				{
					object obj6 = obj5 + obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ r9_v1+v182 @ rax_v18*8]");
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rdx_v6 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnectionWithOptions`1>)+18]");
					if (num4 == 0)
					{
						break;
					}
					obj5++;
					object obj7 = obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ r10_v1+12E]");
					if ((nint)obj7 < 0)
					{
						continue;
					}
					goto IL_00b1;
				}
				goto IL_00e2;
			}
		}
		return (List<TOption>)(object)new NullReferenceException();
		IL_00c0:
		object obj9 = default(object);
		object obj8 = obj9;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rax_v13+8]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v93 @ r8_v3 (should have been resolved before IL gen)");
		goto IL_00e2;
		IL_00b1:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_00c0;
		IL_00e2:
		object obj11 = obj5 + obj5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ r9_v1+8+v237 @ rcx_v19*8]");
		object obj12 = (nint)0 + (nint)1;
		object obj13 = obj12 << 4;
		object obj14 = obj13 + 312;
		obj9 = obj14 + obj2;
		goto IL_00c0;
	}

	public void SetOptionLabels(List<TOption> optionLabels)
	{
		//IL_00c0: Expected O, but got I
		//IL_0059: Expected O, but got I
		//IL_0062: Expected O, but got I4
		//IL_00ed: Expected O, but got I
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		IConnection<int> defaultConnection = GetDefaultConnection();
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		nint num3 = 0;
		object obj2 = default(object);
		object obj = obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v93 @ r10_v1+12E]");
		if ((nint)0 >= (nint)0)
		{
			goto IL_0099;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v93 @ r10_v1+B0]");
		object obj3 = 0;
		object obj4 = 0;
		while (true)
		{
			object obj5 = obj4 + obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ r9_v3+v187 @ rax_v18*8]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ rdx_v6 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnectionWithOptions`1>)+18]");
			if (num4 == 0)
			{
				break;
			}
			obj4++;
			object obj6 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v93 @ r10_v1+12E]");
			if ((nint)obj6 < 0)
			{
				continue;
			}
			goto IL_0099;
		}
		goto IL_00ca;
		IL_0099:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_00a8;
		IL_00a8:
		object obj8 = default(object);
		object obj7 = obj8;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rax_v13+8]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v98 @ r9_v2 (should have been resolved before IL gen)");
		goto IL_00ca;
		IL_00ca:
		object obj10 = obj4 + obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ r9_v3+8+v243 @ rcx_v19*8]");
		object obj11 = (nint)0 + (nint)2;
		object obj12 = obj11 << 4;
		object obj13 = obj12 + 312;
		obj8 = obj13 + obj;
		goto IL_00a8;
	}

	public void RefreshOptionLabels()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<IConnection<int>>.Enumerator enumerator = default(List<IConnection<int>>.Enumerator);
		object obj = default(object);
		IntPtr intPtr = default(IntPtr);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (obj != null)
			{
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (intPtr != (IntPtr)0)
				{
					nint num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
			}
		}
		enumerator.Dispose();
	}

	public MultiConnectionWithOptions()
	{
		List<IConnection<int>> list = new List<IConnection<int>>();
		List<Action<int>> list2 = new List<Action<int>>();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
