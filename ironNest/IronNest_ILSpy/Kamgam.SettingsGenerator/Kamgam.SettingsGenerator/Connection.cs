using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public abstract class Connection<TValue> : IConnection<TValue>, IConnection, IQualityChangeReceiver
{
	private Action<int> m_QualityChanged;

	protected List<Action<TValue>> _onChangedListeners;

	protected TValue lastKnownValue;

	public int Order;

	public event Action<int> QualityChanged
	{
		add
		{
			//IL_000e: Expected O, but got I4
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = default(object);
			Delegate obj = (Delegate)obj2;
			object obj5 = default(object);
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj, value);
				if ((object)obj3 == null)
				{
					object obj4 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj5 == null;
					object obj4 = obj5;
					if (flag)
					{
						break;
					}
				}
				nint num2 = 0;
				IntPtr intPtr2 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj6 != obj;
				obj = obj6;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_000e: Expected O, but got I4
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = default(object);
			Delegate obj = (Delegate)obj2;
			object obj5 = default(object);
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj, value);
				if ((object)obj3 == null)
				{
					object obj4 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj5 == null;
					object obj4 = obj5;
					if (flag)
					{
						break;
					}
				}
				nint num2 = 0;
				IntPtr intPtr2 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj6 != obj;
				obj = obj6;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public abstract TValue Get();

	public unsafe virtual TValue GetDefault()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_0032: Expected O, but got I
		//IL_0042: Expected O, but got I
		//IL_0058: Expected O, but got I
		//IL_0086: Expected I, but got O
		//IL_0094: Expected O, but got Ref
		//IL_00a9: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rax_v1+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ r8_v1+10]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rax_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rax_v2+FC]");
		if ((nint)obj6 <= 0)
		{
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		nint num = (nint)this;
		object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
		_ = ref obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rcx_v1 (Il2CppClass<Kamgam.SettingsGenerator.Connection`1<TValue>>)+210]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v47 @ rax_v7+10] (should have been resolved before IL gen)");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		TValue result = default(TValue);
		return result;
	}

	public virtual int GetOrder()
	{
		//IL_0024: Expected O, but got I
		//IL_0033: Expected I4, but got O
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
		object obj = (nint)0 + (nint)96;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		return (int)obj2;
	}

	public virtual void SetOrder(int order)
	{
		//IL_001e: Expected O, but got I
		//IL_0034: Expected O, but got I
		//IL_0054: Expected O, but got I
		//IL_006b: Expected O, but got I4
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
		object obj2 = (nint)0 + (nint)96;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
		object obj3 = (nint)0 + (nint)96;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj4 = order;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public abstract void Set(TValue value);

	public unsafe virtual void NotifyListenersIfChanged(TValue value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0023: Expected O, but got I
		//IL_0039: Expected O, but got I
		//IL_0474: Expected O, but got I
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Expected O, but got Unknown
		//IL_0704: Expected O, but got I
		//IL_04ab: Expected O, but got Ref
		//IL_04db: Expected O, but got I
		//IL_004e: Expected O, but got I
		//IL_005e: Expected O, but got I
		//IL_006e: Expected O, but got I
		//IL_0088: Expected O, but got I
		//IL_0098: Expected O, but got I
		//IL_00a8: Expected O, but got I
		//IL_00b8: Expected O, but got I
		//IL_00c6: Expected O, but got Ref
		//IL_0504: Expected O, but got I
		//IL_0514: Expected O, but got I
		//IL_0119: Expected O, but got Ref
		//IL_00fe: Expected O, but got I
		//IL_015b: Expected O, but got I
		//IL_016b: Expected O, but got I
		//IL_017b: Expected O, but got I
		//IL_018b: Expected O, but got I
		//IL_0199: Expected O, but got Ref
		//IL_0533: Expected O, but got I
		//IL_0543: Expected O, but got I
		//IL_0553: Expected O, but got I
		//IL_0571: Expected O, but got I
		//IL_058b: Expected O, but got I
		//IL_059b: Expected O, but got I
		//IL_05ab: Expected O, but got I
		//IL_05c9: Expected O, but got I
		//IL_01d1: Expected O, but got I
		//IL_0203: Expected O, but got I
		//IL_0213: Expected O, but got I
		//IL_0223: Expected O, but got I
		//IL_0241: Expected O, but got I
		//IL_0260: Expected O, but got I
		//IL_0270: Expected O, but got I
		//IL_0280: Expected O, but got I
		//IL_0290: Expected O, but got I
		//IL_029e: Expected O, but got Ref
		//IL_02d6: Expected O, but got Ref
		//IL_02e9: Expected O, but got Ref
		//IL_05e8: Expected O, but got I
		//IL_05f8: Expected O, but got I
		//IL_0608: Expected O, but got I
		//IL_0616: Expected O, but got Ref
		//IL_043e: Expected O, but got I
		//IL_044e: Expected O, but got I
		//IL_0303: Expected O, but got I
		//IL_0313: Expected O, but got I
		//IL_0323: Expected O, but got I
		//IL_0333: Expected O, but got I
		//IL_0341: Expected O, but got Ref
		//IL_034f: Expected O, but got Ref
		//IL_0369: Expected O, but got I
		//IL_039e: Expected O, but got I
		//IL_03ae: Expected O, but got I
		//IL_03be: Expected O, but got I
		//IL_03ce: Expected O, but got I
		//IL_03dc: Expected O, but got Ref
		//IL_0657: Expected O, but got I
		//IL_0667: Expected O, but got I
		//IL_0677: Expected O, but got I
		//IL_0687: Expected O, but got I
		//IL_069d: Expected O, but got I
		//IL_06b7: Expected O, but got Ref
		//IL_06d5: Expected O, but got I
		//IL_0414: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rdx_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>)+10]");
		object obj3 = 0;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rdx_v2 (Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>)+10]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v5+FC]");
		object obj5 = (nint)0 + (nint)16;
		object obj6 = obj5 + 15;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v25 @ rax_v2+FC]");
			object obj7 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v25 @ rax_v2+FC]");
			if ((nint)obj7 <= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Warning: Method ends with non empty stack (-A8), the output could be wrong!");
				/*Error: End of method reached without returning.*/;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		object obj8 = (object)(&obj2);
		_ = 0;
		_ = 0;
		nint num3 = 0;
		IntPtr intPtr = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v101 @ rax_v18 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
		object obj9 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rax_v21+20]");
		object obj11 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rcx_v7+C0]");
		object obj12 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
		object obj13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r8_v3+20]");
		object obj14 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ rcx_v10+C0]");
		object obj15 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rdx_v7+10]");
		object obj16 = 0;
		object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 136));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rcx_v11+28]");
		if ((nint)0 < (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+88]");
			obj17 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r8_v3+20]");
		object obj18 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rcx_v12+C0]");
		object obj19 = 0;
		object obj20 = default(object);
		obj = obj20;
		object obj21 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 152));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A76C0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+98]");
		if ((nint)0 != 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
		object obj22 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ rax_v27+20]");
		object obj23 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ rcx_v18+C0]");
		object obj24 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v175 @ rax_v28+10]");
		object obj25 = 0;
		object obj26 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 136));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ rcx_v19+28]");
		if ((nint)0 < (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+88]");
			obj26 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
		object obj27 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v302 @ rax_v30+20]");
		object obj28 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ rcx_v21+C0]");
		object obj29 = 0;
		object obj30 = obj29;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v305 @ rcx_v22+80]");
		object obj31 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7800");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
		object obj32 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ rax_v33+20]");
		object obj33 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v311 @ rcx_v24+C0]");
		object obj34 = 0;
		object obj35 = obj34;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ rcx_v25+80]");
		object obj36 = (nint)0 + (nint)32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj37 = default(object);
		if (obj37 == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
		object obj38 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v315 @ rax_v36+20]");
		object obj39 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rcx_v27+C0]");
		object obj40 = 0;
		object obj41 = obj40;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ rcx_v28+80]");
		object obj42 = (nint)0 + (nint)32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
		object obj43 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v322 @ rax_v40+20]");
		object obj44 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ rdx_v19+C0]");
		object obj45 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ r8_v8+28]");
		object obj46 = 0;
		object obj47 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 40));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+28]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+38]");
		_ = 0;
		_ = 0;
		object obj48 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 16));
		object obj49 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 144));
		object obj54 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
			object obj50 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rax_v43+20]");
			object obj51 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ rcx_v32+C0]");
			object obj52 = 0;
			object obj53 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 16));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj54 == null)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
			object obj55 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ rax_v47+20]");
			object obj56 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v378 @ rcx_v35+C0]");
			object obj57 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ r8_v11+38]");
			obj46 = 0;
			object obj58 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 152));
			object obj59 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 16));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+98]");
			object obj60 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+98]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
				object obj61 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ rax_v49+20]");
				object obj62 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ rcx_v37+C0]");
				object obj63 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v388 @ rax_v50+10]");
				object obj64 = 0;
				object obj65 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 136));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rcx_v38+28]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+88]");
					obj65 = 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
				object obj66 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v406 @ rax_v52+20]");
				object obj67 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v407 @ rcx_v40+C0]");
				object obj68 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ rax_v53+10]");
				object obj69 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v409 @ rcx_v41+28]");
				object obj70 = (nint)0 >> 31;
				bool flag = obj70 != null;
				object obj71 = (object)(&obj2);
				if (!flag)
				{
					obj71 = obj8;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v328 @ rbx_v6+28]");
				obj46 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v328 @ rbx_v6+18] (should have been resolved before IL gen)");
			}
		}
		object obj72 = obj49;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v380 @ rax_v45+20]");
		object obj73 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rdx_v24+C0]");
		object obj74 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public void AddChangeListener(Action<TValue> listener)
	{
		//IL_0024: Expected O, but got I
		//IL_018a: Expected O, but got I
		//IL_0141: Expected O, but got I
		//IL_0091: Expected O, but got I
		//IL_00a7: Expected O, but got I
		//IL_00c7: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
		object obj = (nint)0 + (nint)32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 == null)
		{
			nint num2 = 0;
			object obj3 = null;
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6A40");
			nint num4 = 0;
			IntPtr intPtr2 = num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
			object obj5 = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
			object obj6 = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj7 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		}
		nint num5 = 0;
		IntPtr intPtr3 = num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rax_v6 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
		object obj8 = (nint)0 + (nint)32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A3EA0");
		object obj9 = default(object);
		if (obj9 == null)
		{
			nint num7 = 0;
			IntPtr intPtr4 = num7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
			object obj10 = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			nint num8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
		}
	}

	public void RemoveChangeListener(Action<TValue> listener)
	{
		//IL_0024: Expected O, but got I
		//IL_006f: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
		object obj = (nint)0 + (nint)32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 != null)
		{
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rax_v6 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.Connection`1>>)+80]");
			object obj3 = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A5880");
		}
	}

	public virtual void OnQualityChanged(int qualityLevel)
	{
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		object obj = obj2;
		if (obj2 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v16 @ rcx_v1+18] (should have been resolved before IL gen)");
		}
	}

	public virtual void Destroy()
	{
	}
}
