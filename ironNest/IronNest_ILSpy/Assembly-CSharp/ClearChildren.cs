using System;
using System.Collections;
using Cpp2ILInjected;
using UnityEngine;

public class ClearChildren : MonoBehaviour
{
	public void DeleteAllChildren()
	{
		//IL_001f: Expected I, but got O
		//IL_00aa: Expected O, but got I4
		//IL_0057: Expected O, but got I
		//IL_0060: Expected O, but got I4
		//IL_019c: Expected O, but got I
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_00dc: Expected I, but got O
		//IL_0167: Expected O, but got I4
		//IL_03a2: Expected I, but got O
		//IL_0114: Expected O, but got I
		//IL_011d: Expected O, but got I4
		//IL_0215: Expected I, but got O
		//IL_0225: Expected O, but got I
		//IL_01df: Expected O, but got I
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0261: Expected O, but got I
		Transform transform = base.transform;
		IEnumerator enumerator = transform.GetEnumerator();
		Transform transform2 = default(Transform);
		object obj9 = default(object);
		object obj19 = default(object);
		Transform transform3 = default(Transform);
		object obj23 = default(object);
		while ((object)transform2 != null)
		{
			nint num = (nint)transform2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r10_v4 (Il2CppClass<UnityEngine.Transform>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0097;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r10_v4 (Il2CppClass<UnityEngine.Transform>)+B0]");
			object obj = 0;
			object obj2 = 0;
			while (true)
			{
				object obj3 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ r8_v4+v231 @ rax_v39*8]");
				if (0 == (nint)typeof(IEnumerator))
				{
					break;
				}
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r10_v4 (Il2CppClass<UnityEngine.Transform>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_0097;
			}
			object obj5 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ r8_v4+8+v287 @ rcx_v32*8]");
			object obj6 = (nint)0 << 4;
			object obj7 = obj6 + 312;
			object obj8 = obj7 + num;
			goto IL_0363;
			IL_0363:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v292 @ rdx_v7] (should have been resolved before IL gen)");
			object obj10;
			object obj18;
			if (obj9 != null)
			{
				if ((object)transform2 == null)
				{
					goto IL_02be;
				}
				nint num2 = (nint)transform2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r10_v6 (Il2CppClass<UnityEngine.Transform>)+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_0154;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r10_v6 (Il2CppClass<UnityEngine.Transform>)+B0]");
				obj10 = 0;
				object obj11 = 0;
				while (true)
				{
					object obj12 = obj11 + obj11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r8_v6+v351 @ rax_v34*8]");
					if (0 == (nint)typeof(IEnumerator))
					{
						break;
					}
					obj11++;
					object obj13 = obj11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r10_v6 (Il2CppClass<UnityEngine.Transform>)+12E]");
					if ((nint)obj13 < 0)
					{
						continue;
					}
					goto IL_0154;
				}
				object obj14 = obj11 + obj11;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r8_v6+8+v407 @ rcx_v24*8]");
				object obj15 = (nint)0 + (nint)1;
				object obj16 = obj15 << 4;
				object obj17 = obj16 + 312;
				obj18 = obj17 + num2;
				goto IL_038a;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180467A90");
			return;
			IL_02be:
			throw new NullReferenceException();
			IL_0154:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj10 = 1;
			obj18 = obj19;
			goto IL_038a;
			IL_038a:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v415 @ rdx_v11] (should have been resolved before IL gen)");
			nint num3 = (nint)typeof(Transform);
			nint num4 = (nint)transform3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v433 @ rcx_v15 (Il2CppClass<UnityEngine.Transform>)+130]");
			object obj20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ r9_v7 (Il2CppClass<System.Collections.IEnumerator>)+130]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v433 @ rcx_v15 (Il2CppClass<UnityEngine.Transform>)+130]");
			if (num5 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ r9_v7 (Il2CppClass<System.Collections.IEnumerator>)+C8]");
				object obj21 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v461 @ rax_v25+FFFFFFF8+v445 @ rax_v24*8]");
				if (0 == (nint)typeof(Transform))
				{
					GameObject obj22 = transform3.gameObject;
					UnityEngine.Object.Destroy(obj22);
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			goto IL_02be;
			IL_0097:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj = 0;
			obj8 = obj23;
			goto IL_0363;
		}
		throw new NullReferenceException();
	}
}
