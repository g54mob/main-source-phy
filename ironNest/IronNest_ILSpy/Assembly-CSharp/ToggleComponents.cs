using Cpp2ILInjected;
using UnityEngine;

public class ToggleComponents : MonoBehaviour
{
	private bool toggleOnAwake;

	private bool toggleOnAwakeValue;

	private Component[] components;

	private void Awake()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_003d: Expected O, but got I4
		//IL_0046: Expected O, but got I4
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Expected O, but got Unknown
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Expected O, but got Unknown
		//IL_00a9: Expected I, but got O
		//IL_00b1: Expected I, but got O
		//IL_00c1: Expected O, but got I
		//IL_0175: Expected I, but got O
		//IL_0185: Expected O, but got I
		//IL_0239: Expected I, but got O
		//IL_0249: Expected O, but got I
		//IL_00fd: Expected O, but got I
		//IL_01c1: Expected O, but got I
		//IL_0285: Expected O, but got I
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_015a: Expected I, but got O
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected O, but got Unknown
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Expected O, but got Unknown
		//IL_021e: Expected I, but got O
		//IL_02c6: Expected I, but got O
		if (!toggleOnAwake)
		{
			return;
		}
		Component[] array = components;
		object obj = components + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < array.Length)
		{
			Object obj4 = (Object)obj;
			if ((bool)(Object)obj && obj != null)
			{
				nint num = (nint)typeof(Behaviour);
				nint num2 = (nint)obj4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ rcx_v8 (Il2CppClass<UnityEngine.Behaviour>)+130]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r8_v4 (Il2CppClass<UnityEngine.Object>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ rcx_v8 (Il2CppClass<UnityEngine.Behaviour>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r8_v4 (Il2CppClass<UnityEngine.Object>)+C8]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v322 @ rax_v22+FFFFFFF8+v308 @ rax_v12*8]");
					if (0 == (nint)typeof(Behaviour))
					{
						((Behaviour)obj).enabled = toggleOnAwakeValue;
						obj2++;
						obj += 8;
						num2 = unchecked((nint)null);
						obj3 = obj2;
						continue;
					}
				}
				nint num4 = (nint)typeof(Collider);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ rdx_v7 (Il2CppClass<UnityEngine.Collider>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r8_v4 (Il2CppClass<UnityEngine.Object>)+130]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ rdx_v7 (Il2CppClass<UnityEngine.Collider>)+130]");
				if (num5 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r8_v4 (Il2CppClass<UnityEngine.Object>)+C8]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v351 @ rax_v19+FFFFFFF8+v337 @ rax_v14*8]");
					if (0 == (nint)typeof(Collider))
					{
						((Collider)obj).enabled = toggleOnAwakeValue;
						obj2++;
						obj += 8;
						num2 = unchecked((nint)null);
						obj3 = obj2;
						continue;
					}
				}
				nint num6 = (nint)typeof(Renderer);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rdx_v8 (Il2CppClass<UnityEngine.Renderer>)+130]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r8_v4 (Il2CppClass<UnityEngine.Object>)+130]");
				nint num7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rdx_v8 (Il2CppClass<UnityEngine.Renderer>)+130]");
				if (num7 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r8_v4 (Il2CppClass<UnityEngine.Object>)+C8]");
					object obj10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v281 @ rax_v17+FFFFFFF8+v280 @ rax_v16*8]");
					if (0 == (nint)typeof(Renderer))
					{
						((Renderer)obj).enabled = toggleOnAwakeValue;
						num2 = unchecked((nint)null);
					}
				}
			}
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
	}

	public void Toggle(bool isEnabled)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_001e: Expected O, but got I4
		//IL_0027: Expected O, but got I4
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Expected O, but got Unknown
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Expected O, but got Unknown
		//IL_008a: Expected I, but got O
		//IL_0092: Expected I, but got O
		//IL_00a2: Expected O, but got I
		//IL_0154: Expected I, but got O
		//IL_0164: Expected O, but got I
		//IL_0216: Expected I, but got O
		//IL_0226: Expected O, but got I
		//IL_00de: Expected O, but got I
		//IL_01a0: Expected O, but got I
		//IL_0262: Expected O, but got I
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0139: Expected I, but got O
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Expected O, but got Unknown
		//IL_01fb: Expected I, but got O
		//IL_02a1: Expected I, but got O
		Component[] array = components;
		object obj = components + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < array.Length)
		{
			Object obj4 = (Object)obj;
			if ((bool)(Object)obj && obj != null)
			{
				nint num = (nint)typeof(Behaviour);
				nint num2 = (nint)obj4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v269 @ rcx_v7 (Il2CppClass<UnityEngine.Behaviour>)+130]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ r8_v3 (Il2CppMethodInfo)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v269 @ rcx_v7 (Il2CppClass<UnityEngine.Behaviour>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ r8_v3 (Il2CppMethodInfo)+C8]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v284 @ rax_v21+FFFFFFF8+v270 @ rax_v11*8]");
					if (0 == (nint)typeof(Behaviour))
					{
						((Behaviour)obj).enabled = isEnabled;
						obj2++;
						obj += 8;
						num2 = unchecked((nint)null);
						obj3 = obj2;
						continue;
					}
				}
				nint num4 = (nint)typeof(Collider);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v298 @ rdx_v6 (Il2CppClass<UnityEngine.Collider>)+130]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ r8_v3 (Il2CppMethodInfo)+130]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v298 @ rdx_v6 (Il2CppClass<UnityEngine.Collider>)+130]");
				if (num5 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ r8_v3 (Il2CppMethodInfo)+C8]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ rax_v18+FFFFFFF8+v299 @ rax_v13*8]");
					if (0 == (nint)typeof(Collider))
					{
						((Collider)obj).enabled = isEnabled;
						obj2++;
						obj += 8;
						num2 = unchecked((nint)null);
						obj3 = obj2;
						continue;
					}
				}
				nint num6 = (nint)typeof(Renderer);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v239 @ rdx_v7 (Il2CppClass<UnityEngine.Renderer>)+130]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ r8_v3 (Il2CppMethodInfo)+130]");
				nint num7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v239 @ rdx_v7 (Il2CppClass<UnityEngine.Renderer>)+130]");
				if (num7 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ r8_v3 (Il2CppMethodInfo)+C8]");
					object obj10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v243 @ rax_v16+FFFFFFF8+v242 @ rax_v15*8]");
					if (0 == (nint)typeof(Renderer))
					{
						((Renderer)obj).enabled = isEnabled;
						num2 = unchecked((nint)null);
					}
				}
			}
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
	}
}
