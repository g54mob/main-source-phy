using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class MultiConnection<T> : IConnection<T>, IConnection, IQualityChangeReceiver
{
	public IConnection<T> DefaultConnection;

	protected List<IConnection<T>> _connections;

	protected List<Action<T>> _changeListeners;

	public unsafe void AddConnection(IConnection<T> connection)
	{
		//IL_002b: Expected O, but got I
		//IL_003d: Expected O, but got Ref
		//IL_0211: Expected O, but got I
		//IL_0221: Expected O, but got I
		//IL_015e: Expected O, but got I
		//IL_016e: Expected O, but got I
		//IL_0079: Expected O, but got I
		//IL_0089: Expected O, but got I
		//IL_01a0: Expected I, but got O
		//IL_0258: Expected O, but got I
		//IL_009e: Expected O, but got I
		//IL_00a7: Expected O, but got I4
		//IL_0118: Expected O, but got I
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ r8_v4 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+28]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		object obj6 = default(object);
		object obj21 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ stack_18_v3+20]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ rcx_v5+C0]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			object obj17;
			if (obj6 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (connection == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ stack_18_v3+20]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rcx_v10+C0]");
				object obj8 = 0;
				nint num3 = (nint)connection;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.IConnection`1<T>>)+12E]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.IConnection`1<T>>)+B0]");
					object obj9 = 0;
					object obj10 = 0;
					while (true)
					{
						object obj11 = obj10 + obj10;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ r9_v3+v280 @ rcx_v18*8]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ rax_v18+10]");
						if (num4 == 0)
						{
							break;
						}
						obj10++;
						object obj12 = obj10;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.IConnection`1<T>>)+12E]");
						if ((nint)obj12 < 0)
						{
							continue;
						}
						goto IL_00de;
					}
					object obj13 = obj10 + obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ r9_v3+8+v313 @ rcx_v20*8]");
					object obj14 = (nint)0 + (nint)3;
					object obj15 = obj14 << 4;
					object obj16 = obj15 + 312;
					obj17 = obj16 + num3;
					goto IL_0248;
				}
				goto IL_00de;
			}
			object obj18 = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rax_v11+20]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rdx_v7+C0]");
			object obj20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			return;
			IL_00de:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj17 = obj21;
			goto IL_0248;
			IL_0248:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ r8_v10+8]");
			obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v320 @ r8_v10] (should have been resolved before IL gen)");
		}
		throw new NullReferenceException();
	}

	public unsafe void RemoveConnection(IConnection<T> connection)
	{
		//IL_002b: Expected O, but got I
		//IL_003d: Expected O, but got Ref
		//IL_0211: Expected O, but got I
		//IL_0221: Expected O, but got I
		//IL_015e: Expected O, but got I
		//IL_016e: Expected O, but got I
		//IL_0079: Expected O, but got I
		//IL_0089: Expected O, but got I
		//IL_01a0: Expected I, but got O
		//IL_0258: Expected O, but got I
		//IL_009e: Expected O, but got I
		//IL_00a7: Expected O, but got I4
		//IL_0118: Expected O, but got I
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A5880");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ r8_v4 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+28]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		object obj6 = default(object);
		object obj21 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ stack_18_v3+20]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ rcx_v5+C0]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			object obj17;
			if (obj6 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (connection == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ stack_18_v3+20]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rcx_v10+C0]");
				object obj8 = 0;
				nint num3 = (nint)connection;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.IConnection`1<T>>)+12E]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.IConnection`1<T>>)+B0]");
					object obj9 = 0;
					object obj10 = 0;
					while (true)
					{
						object obj11 = obj10 + obj10;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ r9_v3+v280 @ rcx_v18*8]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ rax_v18+10]");
						if (num4 == 0)
						{
							break;
						}
						obj10++;
						object obj12 = obj10;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.IConnection`1<T>>)+12E]");
						if ((nint)obj12 < 0)
						{
							continue;
						}
						goto IL_00de;
					}
					object obj13 = obj10 + obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ r9_v3+8+v313 @ rcx_v20*8]");
					object obj14 = (nint)0 + (nint)4;
					object obj15 = obj14 << 4;
					object obj16 = obj15 + 312;
					obj17 = obj16 + num3;
					goto IL_0248;
				}
				goto IL_00de;
			}
			object obj18 = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rax_v11+20]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rdx_v7+C0]");
			object obj20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			return;
			IL_00de:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj17 = obj21;
			goto IL_0248;
			IL_0248:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ r8_v10+8]");
			obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v320 @ r8_v10] (should have been resolved before IL gen)");
		}
		throw new NullReferenceException();
	}

	public unsafe void ClearConnections()
	{
		//IL_0033: Expected O, but got I4
		//IL_013c: Expected O, but got I
		//IL_016c: Expected O, but got I
		//IL_017c: Expected O, but got I
		//IL_0057: Expected O, but got I
		//IL_0087: Expected O, but got Ref
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_0109: Expected O, but got I4
		//IL_00c1: Expected O, but got I
		//IL_00e1: Expected O, but got I
		List<IConnection<T>> connections = _connections;
		bool flag = (nint)_connections < 0;
		object obj = connections._size - 1;
		if (!flag)
		{
			object obj3 = default(object);
			object obj5;
			do
			{
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ r9_v3 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+80]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool flag2 = (nint)obj3 < 0;
				bool flag3 = obj3 == null;
				object obj4 = (object)(&obj3);
				if (!flag3)
				{
					flag2 = (nint)_connections < 0;
					nint num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v243 @ r9_v6 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+80]");
					obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					nint num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ r8_v6 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+88]");
					obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BD370");
				}
				obj--;
				obj5 = !flag2;
			}
			while (obj5 != null);
		}
		List<IConnection<T>> connections2 = _connections;
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rcx_v8 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+90]");
		object obj6 = 0;
		int version = connections2._version + 1;
		connections2._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rdx_v8+20]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rax_v13+C0]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj9 = default(object);
		if (obj9 == null)
		{
			connections2._size = 0;
			return;
		}
		connections2._size = 0;
		if (connections2._size > 0)
		{
			Array.Clear(connections2._items, 0, connections2._size);
		}
	}

	public unsafe IConnection<T> GetDefaultConnection()
	{
		//IL_0057: Expected O, but got I
		//IL_0069: Expected O, but got Ref
		//IL_015e: Expected O, but got I
		//IL_016e: Expected O, but got I
		//IL_007e: Expected O, but got I
		//IL_008e: Expected O, but got I
		//IL_009e: Expected O, but got I
		//IL_00dd: Expected O, but got I
		//IL_00ed: Expected O, but got I
		if (DefaultConnection == null)
		{
			if (_connections != null)
			{
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+98]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				object obj3 = default(object);
				object obj2 = (object)(&obj3);
				object obj6 = default(object);
				IConnection<T> connection = default(IConnection<T>);
				while (true)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ stack_10_v3+20]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ rcx_v4+C0]");
					object obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
					if (obj6 == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ stack_10_v3+20]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ rcx_v12+C0]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v189 @ r8_v6+A8]");
					obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (connection != null)
					{
						object obj9 = obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ rax_v18+20]");
						object obj10 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ rdx_v9+C0]");
						object obj11 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
						return connection;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180060BE0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				Exception ex = new Exception("Multi Connection has no connections. Can not get default connection.");
				throw ex;
			}
			return (IConnection<T>)new NullReferenceException();
		}
		return DefaultConnection;
	}

	public unsafe T Get()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_0037: Expected O, but got I
		//IL_0047: Expected O, but got I
		//IL_005d: Expected O, but got I
		//IL_00e7: Expected O, but got I
		//IL_00f7: Expected O, but got I
		//IL_008c: Expected O, but got I
		//IL_009c: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rax_v1+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ r9_v1+D8]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rax_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rax_v2+FC]");
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+20]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rax_v7+C0]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BCCA0");
			object obj9 = default(object);
			if (obj9 == null)
			{
				return (T)new NullReferenceException();
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+20]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rcx_v1+C0]");
		object obj11 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180038F60");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	public unsafe T GetDefault()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_0037: Expected O, but got I
		//IL_0047: Expected O, but got I
		//IL_005d: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ r8+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v1+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ r9_v1+D8]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rax_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rax_v2+FC]");
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BCF40");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		}
		T result = default(T);
		return result;
	}

	public unsafe void Set(T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0023: Expected O, but got I
		//IL_0039: Expected O, but got I
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Expected O, but got Unknown
		//IL_0282: Expected O, but got Ref
		//IL_0085: Expected O, but got I
		//IL_0093: Expected O, but got Ref
		//IL_00cb: Expected O, but got Ref
		//IL_00de: Expected O, but got Ref
		//IL_006a: Expected O, but got I8
		//IL_02a3: Expected O, but got I
		//IL_02b3: Expected O, but got I
		//IL_02c3: Expected O, but got I
		//IL_02d1: Expected O, but got Ref
		//IL_0242: Expected O, but got I
		//IL_0252: Expected O, but got I
		//IL_00f8: Expected O, but got I
		//IL_0108: Expected O, but got I
		//IL_0118: Expected O, but got I
		//IL_0128: Expected O, but got I
		//IL_0136: Expected O, but got Ref
		//IL_0144: Expected O, but got Ref
		//IL_0183: Expected O, but got I
		//IL_0193: Expected O, but got I
		//IL_01a3: Expected O, but got I
		//IL_01b3: Expected O, but got I
		//IL_01c1: Expected O, but got Ref
		//IL_0312: Expected O, but got I
		//IL_0322: Expected O, but got I
		//IL_0332: Expected O, but got I
		//IL_0342: Expected O, but got I
		//IL_0358: Expected O, but got I
		//IL_0372: Expected O, but got Ref
		//IL_0390: Expected O, but got I
		//IL_03a0: Expected O, but got I
		//IL_021d: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v23 @ r9_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+D8]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		object obj5 = obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2+FC]");
		if ((nint)obj5 <= 0)
		{
			obj4 = 1152921504606846960L;
		}
		object obj6 = obj4 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		object obj7 = (object)(&obj2);
		_ = 0;
		_ = 0;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ r8_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+98]");
		object obj8 = 0;
		object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+30]");
		_ = 0;
		_ = 0;
		object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
		object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 144));
		object obj16 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
			object obj12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rax_v10+20]");
			object obj13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rcx_v3+C0]");
			object obj14 = 0;
			object obj15 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj16 == null)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
			object obj17 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ rax_v14+20]");
			object obj18 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rcx_v6+C0]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r8_v4+A8]");
			obj8 = 0;
			object obj20 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
			object obj21 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+80]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
				object obj22 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rax_v16+20]");
				object obj23 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rcx_v8+C0]");
				object obj24 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ rax_v17+D8]");
				object obj25 = 0;
				T val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 136));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rcx_v9+28]");
				if ((nint)0 < (nint)0)
				{
					val = value;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+90]");
				object obj26 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rdx_v11+20]");
				object obj27 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ rax_v19+C0]");
				object obj28 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ rcx_v11+D8]");
				object obj29 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ rax_v20+28]");
				object obj30 = (nint)0 >> 31;
				bool flag = obj30 != null;
				object obj31 = (object)(&obj2);
				if (!flag)
				{
					obj31 = obj7;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rdx_v11+20]");
				object obj32 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ rax_v21+C0]");
				object obj33 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180038F60");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+80]");
				obj8 = 0;
				obj6 = obj31;
			}
		}
		object obj34 = obj11;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ rax_v12+20]");
		object obj35 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rdx_v5+C0]");
		object obj36 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public unsafe void AddChangeListener(Action<T> listener)
	{
		//IL_002b: Expected O, but got I
		//IL_003d: Expected O, but got Ref
		//IL_011d: Expected O, but got I
		//IL_012d: Expected O, but got I
		//IL_00ed: Expected O, but got I
		//IL_00fd: Expected O, but got I
		//IL_0052: Expected O, but got I
		//IL_0062: Expected O, but got I
		//IL_0072: Expected O, but got I
		//IL_00a9: Expected O, but got I
		//IL_00b9: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180690A10");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ r8_v3 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+98]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		object obj6 = default(object);
		object obj9 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ stack_18_v2+20]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rcx_v4+C0]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj6 == null)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ stack_18_v2+20]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rcx_v7+C0]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ r8_v6+A8]");
			obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (obj9 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ stack_18_v2+20]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ rcx_v9+C0]");
				object obj11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
				obj = obj9;
			}
		}
		object obj12 = obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v10+20]");
		object obj13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rdx_v6+C0]");
		object obj14 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public unsafe void RemoveChangeListener(Action<T> listener)
	{
		//IL_002b: Expected O, but got I
		//IL_003d: Expected O, but got Ref
		//IL_011d: Expected O, but got I
		//IL_012d: Expected O, but got I
		//IL_00ed: Expected O, but got I
		//IL_00fd: Expected O, but got I
		//IL_0052: Expected O, but got I
		//IL_0062: Expected O, but got I
		//IL_0072: Expected O, but got I
		//IL_00a9: Expected O, but got I
		//IL_00b9: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A5880");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ r8_v4 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+98]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		object obj6 = default(object);
		object obj9 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ stack_18_v2+20]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ rcx_v5+C0]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj6 == null)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ stack_18_v2+20]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ rcx_v8+C0]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v189 @ r8_v7+A8]");
			obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (obj9 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ stack_18_v2+20]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rcx_v10+C0]");
				object obj11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
				obj = obj9;
			}
		}
		object obj12 = obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ rax_v11+20]");
		object obj13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v191 @ rdx_v6+C0]");
		object obj14 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public unsafe void OnQualityChanged(int qualityLevel)
	{
		//IL_001b: Expected O, but got I
		//IL_002d: Expected O, but got Ref
		//IL_00c0: Expected O, but got I
		//IL_00d0: Expected O, but got I
		//IL_0090: Expected O, but got I
		//IL_00a0: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r8_v2 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+98]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		object obj6 = default(object);
		object obj7 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ stack_18_v2+20]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v108 @ rcx_v4+C0]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj6 == null)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			bool flag = obj7 == null;
			obj = obj7;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
				obj = obj7;
			}
		}
		object obj8 = obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ rax_v9+20]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rdx_v5+C0]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public int GetOrder()
	{
		//IL_0022: Expected I4, but got O
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BCCA0");
		object obj = default(object);
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			int result = default(int);
			return result;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public unsafe void SetOrder(int order)
	{
		//IL_002a: Expected O, but got I
		//IL_003c: Expected O, but got Ref
		//IL_00ee: Expected O, but got I
		//IL_00fe: Expected O, but got I
		//IL_009f: Expected O, but got I
		//IL_00af: Expected O, but got I
		if (DefaultConnection != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
		}
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ r8_v2 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+98]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		object obj6 = default(object);
		object obj7 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ stack_18_v2+20]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v5+C0]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj6 == null)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			bool flag = obj7 == null;
			obj = obj7;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
				obj = obj7;
			}
		}
		object obj8 = obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ rax_v10+20]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rdx_v7+C0]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public unsafe void Destroy()
	{
		//IL_0020: Expected O, but got I
		//IL_0049: Expected O, but got I4
		//IL_01a0: Expected O, but got I
		//IL_01b0: Expected O, but got I
		//IL_01c0: Expected O, but got I
		//IL_01f0: Expected O, but got I
		//IL_0200: Expected O, but got I
		//IL_006d: Expected O, but got I
		//IL_007d: Expected O, but got I
		//IL_008d: Expected O, but got I
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_0173: Expected O, but got I4
		//IL_02d4: Expected O, but got I
		//IL_0304: Expected O, but got I
		//IL_0314: Expected O, but got I
		//IL_02aa: Expected O, but got I4
		//IL_00f1: Expected O, but got I
		//IL_0101: Expected O, but got I
		//IL_0111: Expected O, but got I
		//IL_012b: Expected O, but got I
		//IL_013b: Expected O, but got I
		List<IConnection<T>> connections = _connections;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ r8_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+108]");
		object obj = 0;
		bool flag = (nint)_connections < 0;
		object obj2 = connections._size - 1;
		nint num2 = 0;
		if (!flag)
		{
			object obj6 = default(object);
			object obj11;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rsi_v1+20]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ rax_v5+C0]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v177 @ r9_v3+80]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool flag2 = (nint)obj6 < 0;
				bool flag3 = obj6 == null;
				num2 = (nint)(&obj6);
				if (!flag3)
				{
					flag2 = (nint)_connections < 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rsi_v1+20]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v294 @ rax_v8+C0]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ r9_v6+80]");
					obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rsi_v1+20]");
					object obj9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ rax_v10+C0]");
					object obj10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ r8_v7+88]");
					num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BD370");
				}
				obj2--;
				obj11 = !flag2;
			}
			while (obj11 != null);
		}
		List<IConnection<T>> connections2 = _connections;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rsi_v1+20]");
		object obj12 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rax_v13+C0]");
		object obj13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ rcx_v8+90]");
		object obj14 = 0;
		int version = connections2._version + 1;
		connections2._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ rdx_v8+20]");
		object obj15 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ rax_v14+C0]");
		object obj16 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj17 = default(object);
		if (obj17 == null)
		{
			connections2._size = 0;
			int num3 = (int)num2;
		}
		else
		{
			int num3 = connections2._size;
			connections2._size = 0;
			if (connections2._size > 0)
			{
				Array.Clear(connections2._items, 0, connections2._size);
				object obj5 = 0;
			}
		}
		List<Action<T>> changeListeners = _changeListeners;
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v300 @ rcx_v12 (Il2CppRgctx<Kamgam.SettingsGenerator.MultiConnection`1>)+110]");
		object obj18 = 0;
		int version2 = changeListeners._version + 1;
		changeListeners._version = version2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ rdx_v10+20]");
		object obj19 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v301 @ rax_v18+C0]");
		object obj20 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj21 = default(object);
		if (obj21 == null)
		{
			changeListeners._size = 0;
			return;
		}
		changeListeners._size = 0;
		if (changeListeners._size > 0)
		{
			Array.Clear(changeListeners._items, 0, changeListeners._size);
		}
	}

	public MultiConnection()
	{
		nint num = 0;
		List<IConnection<T>> connections = null;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6A40");
		_connections = connections;
		nint num3 = 0;
		List<Action<T>> changeListeners = null;
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6A40");
		_changeListeners = changeListeners;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
