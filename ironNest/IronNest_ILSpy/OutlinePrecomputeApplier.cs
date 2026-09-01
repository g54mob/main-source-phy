using System;
using System.Reflection;
using Cpp2ILInjected;
using UnityEngine;

public class OutlinePrecomputeApplier : MonoBehaviour
{
	private unsafe void Awake()
	{
		//IL_0069: Expected I, but got O
		//IL_0079: Expected O, but got I
		//IL_00ce: Expected I, but got O
		//IL_00d7: Expected O, but got I4
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_00fe: Expected O, but got I4
		//IL_0103: Expected I, but got O
		//IL_010c: Expected O, but got I4
		//IL_02ef: Expected I4, but got O
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Expected O, but got Unknown
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Expected O, but got Unknown
		//IL_0255: Expected O, but got I4
		//IL_025e: Expected O, but got I4
		//IL_0266: Expected I, but got O
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		Outline[] array = UnityEngine.Object.FindObjectsByType<Outline>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Outline));
		bool flag = (object)typeFromHandle == null;
		nint num = 0;
		RuntimeTypeHandle typeFromHandle2 = (RuntimeTypeHandle)typeof(Outline);
		if (!flag)
		{
			nint num2 = (nint)typeFromHandle;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ r9_v4 (Il2CppClass<System.Type>)+6C0]");
			object obj = 0;
			FieldInfo field = typeFromHandle.GetField("precomputeOutline", (BindingFlags)36);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
			object obj2 = default(object);
			if (obj2 != null)
			{
				Debug.LogError("[OutlinePrecomputeApplier] Could not find 'precomputeOutline' field on Outline. Check the field name in Outline.cs.");
				return;
			}
			bool flag2 = array == null;
			num = unchecked((nint)null);
			typeFromHandle2 = (RuntimeTypeHandle)0;
			if (!flag2)
			{
				object obj3 = array + 32;
				RuntimeTypeHandle runtimeTypeHandle = (RuntimeTypeHandle)0;
				num = unchecked((nint)null);
				typeFromHandle2 = (RuntimeTypeHandle)0;
				RuntimeTypeHandle runtimeTypeHandle2 = default(RuntimeTypeHandle);
				object obj4 = default(object);
				object obj5 = default(object);
				object arg = default(object);
				object arg2 = default(object);
				while (true)
				{
					if ((nint)typeFromHandle2 < array.Length)
					{
						if ((object)field == null)
						{
							break;
						}
						IntPtr value = ((RuntimeTypeHandle*)(&field))->value;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v419 @ r8_v10 (System.IntPtr)+2C8] (should have been resolved before IL gen)");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
						num = 0;
						bool flag3 = (object)runtimeTypeHandle2 == null;
						typeFromHandle2 = (RuntimeTypeHandle)field;
						if (flag3)
						{
							break;
						}
						IntPtr value2 = runtimeTypeHandle2.value;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ rdx_v17 (System.IntPtr)+40]");
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r8_v2 (Il2CppMethodInfo)+40]");
						bool flag4 = num3 != 0;
						typeFromHandle2 = runtimeTypeHandle2;
						if (!flag4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
							if (obj4 != null)
							{
								runtimeTypeHandle = (RuntimeTypeHandle)(runtimeTypeHandle + 1);
								obj3 += 8;
								typeFromHandle2 = runtimeTypeHandle;
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							field.SetValue(obj3, obj5);
							runtimeTypeHandle = (RuntimeTypeHandle)(runtimeTypeHandle + 1);
							obj3 += 8;
							object obj6 = 1;
							obj = 0;
							num = (nint)obj5;
							typeFromHandle2 = runtimeTypeHandle;
							continue;
						}
						Outline[] array2 = UnityEngine.Object.FindObjectsByType<Outline>((FindObjectsInactive)typeFromHandle2, (FindObjectsSortMode)num);
						throw new IndexOutOfRangeException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string message = $"[OutlinePrecomputeApplier] Done. Fixed: {arg}, Already OK: {arg2}. Save your scene now, then delete this component.";
					Debug.Log(message);
					return;
				}
			}
		}
		throw new NullReferenceException();
	}
}
