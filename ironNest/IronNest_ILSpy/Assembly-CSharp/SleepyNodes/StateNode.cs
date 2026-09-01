using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

[Serializable]
public abstract class StateNode : Node
{
	public class NodeExecutionState
	{
		public string ID;

		public StateNode Node;

		public Dictionary<string, object> State;

		public string lastFieldPort;

		public bool ListeningToEvents;

		public static NodeExecutionState NewState
		{
			get
			{
				NodeExecutionState nodeExecutionState = new NodeExecutionState();
				Dictionary<string, object> state = new Dictionary<string, object>();
				nodeExecutionState.State = state;
				nodeExecutionState._002Ector();
				Guid guid = Guid.NewGuid();
				Guid guid2 = default(Guid);
				string iD = guid2.ToString();
				nodeExecutionState.ID = iD;
				return nodeExecutionState;
			}
		}

		public unsafe void Set<T>(string key, T value)
		{
			//IL_0008: Expected O, but got Ref
			//IL_0060: Expected O, but got I
			//IL_00b8: Expected O, but got Ref
			//IL_00f9: Expected I, but got O
			object obj2 = default(object);
			object obj = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
			if ((nint)0 == 0)
			{
			}
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rcx_v2 (Il2CppClass<T>)+FC]");
			object obj3 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rcx_v2 (Il2CppClass<T>)+FC]");
			T val;
			if ((nint)obj3 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rcx_v3 (Il2CppClass<T>)+28]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00e6;
				}
			}
			val = value;
			goto IL_00e6;
			IL_00e6:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			object value2 = (IntPtr)obj2;
			State.set_Item(key, value2);
		}

		public unsafe T Get<T>(string key, T defaultValue)
		{
			//IL_0008: Expected O, but got Ref
			//IL_0018: Expected O, but got I
			//IL_008c: Expected O, but got I
			//IL_00aa: Expected O, but got I
			//IL_0254: Expected O, but got I
			//IL_00d6: Expected O, but got I8
			//IL_0281: Unknown result type (might be due to invalid IL or missing references)
			//IL_0286: Expected O, but got Unknown
			//IL_01d5: Expected O, but got I
			//IL_01e3: Expected O, but got Ref
			//IL_02d2: Expected O, but got Ref
			//IL_02f7: Expected O, but got Ref
			//IL_02f7: Expected O, but got I
			//IL_02fb: Expected O, but got I4
			//IL_0129: Expected O, but got I
			//IL_0147: Expected O, but got I
			//IL_0174: Expected O, but got I
			//IL_0192: Expected O, but got I
			//IL_01af: Expected O, but got I4
			//IL_01af: Expected O, but got Ref
			//IL_01c0: Expected O, but got Ref
			object value = default(object);
			object obj = (object)(&value);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+60]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
				if ((nint)0 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
			object obj3 = 0;
			object obj4 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			object obj5 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			object obj6;
			if ((nint)obj5 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
				obj6 = (nint)0 + (nint)15;
				object obj7 = obj6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
				if ((nint)obj7 > 0)
				{
					goto IL_0278;
				}
			}
			obj6 = 1152921504606846960L;
			goto IL_0278;
			IL_02b9:
			string text;
			string key2 = text;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			bool flag = ((Dictionary<string, object>)(&value)).TryGetValue(key2, out *(object*)null);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+58]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			return (T)((Dictionary<string, object>)num).TryGetValue((string)(&value), out *(object*)null);
			IL_0278:
			object obj8 = obj6 & -16;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			if (State != null)
			{
				ref object value2 = ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 96);
				if (State.TryGetValue(key, out value2))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
					object key3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+60]");
					if (((Dictionary<string, object>)0).TryGetValue((string)key3, out value2))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
						object key4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+60]");
						bool flag2 = ((Dictionary<string, object>)0).TryGetValue((string)key4, out value);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
						bool flag3 = ((Dictionary<string, object>)(&value)).TryGetValue((string)flag2, out *(object*)null);
						text = (string)(&value);
						goto IL_02b9;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
				object obj9 = 0;
				text = (string)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 80));
				object obj10 = obj9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v191 @ rcx_v12+28]");
				if ((nint)0 < (nint)0)
				{
					text = (string)defaultValue;
				}
				goto IL_02b9;
			}
			return (T)new NullReferenceException();
		}

		public unsafe void Set<T>(EntityContextKeys key, T value)
		{
			//IL_0008: Expected O, but got Ref
			//IL_005b: Expected O, but got I
			//IL_00c7: Expected O, but got Ref
			//IL_00d9: Expected O, but got Ref
			//IL_011a: Expected I, but got O
			object obj2 = default(object);
			Enum obj = (Enum)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
			if ((nint)0 == 0)
			{
			}
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2 (Il2CppClass<T>)+FC]");
			object obj3 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2 (Il2CppClass<T>)+FC]");
			string key2 = default(string);
			T val;
			if ((nint)obj3 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				_ = -1;
				obj = (Enum)(object)typeof(EntityContextKeys);
				key2 = ((Enum)(&obj2)).ToString();
				val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 96));
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ r9_v1 (Il2CppClass<T>)+28]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_0107;
				}
			}
			val = value;
			goto IL_0107;
			IL_0107:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			object value2 = (IntPtr)obj2;
			State.set_Item(key2, value2);
		}

		public unsafe T Get<T>(EntityContextKeys key, T defaultValue)
		{
			//IL_0008: Expected O, but got Ref
			//IL_0018: Expected O, but got I
			//IL_008c: Expected O, but got I
			//IL_00aa: Expected O, but got I
			//IL_0283: Expected O, but got I
			//IL_00d6: Expected O, but got I8
			//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b5: Expected O, but got Unknown
			//IL_02f0: Expected O, but got Ref
			//IL_0204: Expected O, but got I
			//IL_0212: Expected O, but got Ref
			//IL_0312: Expected O, but got Ref
			//IL_032f: Expected O, but got Ref
			//IL_032f: Expected O, but got I
			//IL_0333: Expected O, but got I4
			//IL_0148: Expected O, but got I
			//IL_0166: Expected O, but got I
			//IL_0193: Expected O, but got I
			//IL_01b1: Expected O, but got I
			//IL_01d6: Expected O, but got I4
			//IL_01d6: Expected O, but got I
			//IL_01ef: Expected O, but got I
			object value = default(object);
			Enum obj = (Enum)(&value);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Enum)+80]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
				if ((nint)0 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
			object obj3 = 0;
			object obj4 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
			object obj5 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
			object obj6;
			if ((nint)obj5 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
				obj6 = (nint)0 + (nint)15;
				object obj7 = obj6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
				if ((nint)obj7 > 0)
				{
					goto IL_02a7;
				}
			}
			obj6 = 1152921504606846960L;
			goto IL_02a7;
			IL_02f9:
			string text;
			string key2 = text;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
			bool flag = ((Dictionary<string, object>)(&value)).TryGetValue(key2, out *(object*)null);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
			IntPtr intPtr = default(IntPtr);
			return (T)((Dictionary<string, object>)(nint)intPtr).TryGetValue((string)(&value), out *(object*)null);
			IL_02a7:
			object obj8 = obj6 & -16;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref value;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			obj = (Enum)(object)typeof(EntityContextKeys);
			_ = -1;
			string key3 = ((Enum)(&value)).ToString();
			if (State != null)
			{
				ref object value2 = ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 128);
				if (State.TryGetValue(key3, out value2))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
					object key4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Enum)+80]");
					if (((Dictionary<string, object>)0).TryGetValue((string)key4, out value2))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
						object key5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Enum)+80]");
						bool flag2 = ((Dictionary<string, object>)0).TryGetValue((string)key5, out value);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Enum)+60]");
						nint num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
						bool flag3 = ((Dictionary<string, object>)num).TryGetValue((string)flag2, out *(object*)null);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Enum)+60]");
						text = (string)0;
						goto IL_02f9;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
				object obj9 = 0;
				text = (string)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 112));
				object obj10 = obj9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rcx_v13+28]");
				if ((nint)0 < (nint)0)
				{
					text = (string)defaultValue;
				}
				goto IL_02f9;
			}
			return (T)new NullReferenceException();
		}

		public unsafe void Set<T>(LocationContextKeys key, T value)
		{
			//IL_0008: Expected O, but got Ref
			//IL_005b: Expected O, but got I
			//IL_00c7: Expected O, but got Ref
			//IL_00d9: Expected O, but got Ref
			//IL_011a: Expected I, but got O
			object obj2 = default(object);
			Enum obj = (Enum)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
			if ((nint)0 == 0)
			{
			}
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2 (Il2CppClass<T>)+FC]");
			object obj3 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2 (Il2CppClass<T>)+FC]");
			string key2 = default(string);
			T val;
			if ((nint)obj3 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				_ = -1;
				obj = (Enum)(object)typeof(LocationContextKeys);
				key2 = ((Enum)(&obj2)).ToString();
				val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 96));
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ r9_v1 (Il2CppClass<T>)+28]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_0107;
				}
			}
			val = value;
			goto IL_0107;
			IL_0107:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			object value2 = (IntPtr)obj2;
			State.set_Item(key2, value2);
		}

		public unsafe T Get<T>(LocationContextKeys key, T defaultValue)
		{
			//IL_0008: Expected O, but got Ref
			//IL_0018: Expected O, but got I
			//IL_008c: Expected O, but got I
			//IL_00aa: Expected O, but got I
			//IL_0283: Expected O, but got I
			//IL_00d6: Expected O, but got I8
			//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b5: Expected O, but got Unknown
			//IL_02f0: Expected O, but got Ref
			//IL_0204: Expected O, but got I
			//IL_0212: Expected O, but got Ref
			//IL_0312: Expected O, but got Ref
			//IL_032f: Expected O, but got Ref
			//IL_032f: Expected O, but got I
			//IL_0333: Expected O, but got I4
			//IL_0148: Expected O, but got I
			//IL_0166: Expected O, but got I
			//IL_0193: Expected O, but got I
			//IL_01b1: Expected O, but got I
			//IL_01d6: Expected O, but got I4
			//IL_01d6: Expected O, but got I
			//IL_01ef: Expected O, but got I
			object value = default(object);
			Enum obj = (Enum)(&value);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Enum)+80]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
				if ((nint)0 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
			object obj3 = 0;
			object obj4 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
			object obj5 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
			object obj6;
			if ((nint)obj5 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
				obj6 = (nint)0 + (nint)15;
				object obj7 = obj6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
				if ((nint)obj7 > 0)
				{
					goto IL_02a7;
				}
			}
			obj6 = 1152921504606846960L;
			goto IL_02a7;
			IL_02f9:
			string text;
			string key2 = text;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
			bool flag = ((Dictionary<string, object>)(&value)).TryGetValue(key2, out *(object*)null);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
			IntPtr intPtr = default(IntPtr);
			return (T)((Dictionary<string, object>)(nint)intPtr).TryGetValue((string)(&value), out *(object*)null);
			IL_02a7:
			object obj8 = obj6 & -16;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref value;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			obj = (Enum)(object)typeof(LocationContextKeys);
			_ = -1;
			string key3 = ((Enum)(&value)).ToString();
			if (State != null)
			{
				ref object value2 = ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 128);
				if (State.TryGetValue(key3, out value2))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
					object key4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Enum)+80]");
					if (((Dictionary<string, object>)0).TryGetValue((string)key4, out value2))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
						object key5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Enum)+80]");
						bool flag2 = ((Dictionary<string, object>)0).TryGetValue((string)key5, out value);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Enum)+60]");
						nint num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
						bool flag3 = ((Dictionary<string, object>)num).TryGetValue((string)flag2, out *(object*)null);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Enum)+60]");
						text = (string)0;
						goto IL_02f9;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rdi_v1+38]");
				object obj9 = 0;
				text = (string)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 112));
				object obj10 = obj9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rcx_v13+28]");
				if ((nint)0 < (nint)0)
				{
					text = (string)defaultValue;
				}
				goto IL_02f9;
			}
			return (T)new NullReferenceException();
		}

		public unsafe bool TryGet<T>(EntityContextKeys key, out T value)
		{
			//IL_0036: Expected O, but got Ref
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
			if ((nint)0 == 0)
			{
			}
			object obj = default(object);
			string key2 = ((Enum)(&obj)).ToString();
			return TryGet<T>(key2, out value);
		}

		public unsafe bool TryGet<T>(LocationContextKeys key, out T value)
		{
			//IL_0036: Expected O, but got Ref
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
			if ((nint)0 == 0)
			{
			}
			object obj = default(object);
			string key2 = ((Enum)(&obj)).ToString();
			return TryGet<T>(key2, out value);
		}

		public unsafe bool TryGet<T>(string key, out T value)
		{
			//IL_0008: Expected O, but got Ref
			//IL_0060: Expected O, but got I
			//IL_0210: Expected O, but got I
			//IL_008c: Expected O, but got I8
			//IL_023d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0242: Expected O, but got Unknown
			//IL_01ea: Expected I4, but got O
			//IL_010a: Expected O, but got I
			//IL_010a: Expected O, but got I
			//IL_0143: Expected O, but got I
			//IL_0143: Expected O, but got I
			//IL_0160: Expected O, but got I4
			//IL_0160: Expected O, but got Ref
			//IL_0182: Expected O, but got Ref
			//IL_0182: Expected O, but got Ref
			//IL_019f: Expected O, but got Ref
			//IL_019f: Expected O, but got Ref
			//IL_01bc: Expected O, but got Ref
			//IL_01bc: Expected O, but got I
			object value2 = default(object);
			object obj = (object)(&value2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
			if ((nint)0 == 0)
			{
			}
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2 (Il2CppClass<T>)+FC]");
			object obj2 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2 (Il2CppClass<T>)+FC]");
			object obj3;
			if ((nint)obj2 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2 (Il2CppClass<T>)+FC]");
				obj3 = (nint)0 + (nint)15;
				object obj4 = obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2 (Il2CppClass<T>)+FC]");
				if ((nint)obj4 > 0)
				{
					goto IL_0234;
				}
			}
			obj3 = 1152921504606846960L;
			goto IL_0234;
			IL_0234:
			object obj5 = obj3 & -16;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			if (State != null)
			{
				ref object value3 = ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value2, 48);
				if (State.TryGetValue(key, out value3))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+30]");
					if (((Dictionary<string, object>)0).TryGetValue((string)0, out value3))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+30]");
						bool flag = ((Dictionary<string, object>)0).TryGetValue((string)0, out value2);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2 (Il2CppClass<T>)+FC]");
						bool flag2 = ((Dictionary<string, object>)(&value2)).TryGetValue((string)flag, out *(object*)null);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2 (Il2CppClass<T>)+FC]");
						bool flag3 = ((Dictionary<string, object>)(&value2)).TryGetValue((string)(&value2), out *(object*)null);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2 (Il2CppClass<T>)+FC]");
						bool flag4 = ((Dictionary<string, object>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref value)).TryGetValue((string)(&value2), out *(object*)null);
						bool flag5 = ((Dictionary<string, object>)0).TryGetValue((string)System.Runtime.CompilerServices.Unsafe.AsPointer(ref value), out value2);
						return true;
					}
				}
				return false;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		public NodeExecutionState()
		{
			Dictionary<string, object> state = new Dictionary<string, object>();
			State = state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public string NodeID;

	public StateNode From;

	public unsafe void SetState<T>(NodeExecutionState state, string key, T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_001d: Expected O, but got I
		//IL_0096: Expected O, but got I
		//IL_00b4: Expected O, but got I
		//IL_0155: Expected O, but got Ref
		//IL_017d: Expected O, but got I
		//IL_018b: Expected O, but got Ref
		//IL_00f0: Expected O, but got I
		//IL_010e: Expected O, but got I
		//IL_0128: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+50]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbx_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbx_v1+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbx_v1+38]");
		object obj4 = 0;
		object obj5 = obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
		object obj7 = default(object);
		T val;
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj7 = (object)(&obj2);
			string text = NodeID + "." + key;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbx_v1+38]");
			object obj8 = 0;
			val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72));
			object obj9 = obj8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ r9_v2+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_01bb;
			}
		}
		val = value;
		goto IL_01bb;
		IL_01bb:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbx_v1+38]");
		object obj10 = 0;
		object obj11 = obj10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rcx_v7+28]");
		object obj12 = (nint)0 >> 31;
		bool flag = obj12 != null;
		object obj13 = (object)(&obj2);
		if (!flag)
		{
			obj13 = obj7;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18077A580");
	}

	public unsafe T GetState<T>(NodeExecutionState state, string key, T defaultValue)
	{
		//IL_0008: Expected O, but got Ref
		//IL_001d: Expected O, but got I
		//IL_008c: Expected O, but got I
		//IL_00aa: Expected O, but got I
		//IL_0167: Expected O, but got I
		//IL_016f: Expected O, but got Ref
		//IL_01a8: Expected O, but got I
		//IL_01c2: Expected O, but got I
		//IL_01d6: Expected O, but got I
		//IL_01e4: Expected O, but got Ref
		//IL_00e6: Expected O, but got I
		//IL_0104: Expected O, but got I
		//IL_011e: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+68]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rbx_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rbx_v1+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rbx_v1+38]");
		object obj4 = 0;
		object obj5 = obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2+FC]");
		object obj8 = default(object);
		T val;
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2+FC]");
			object obj7 = (nint)0 + (nint)15;
			obj8 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2+FC]");
			if ((nint)obj7 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+40]");
			object obj9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rcx_v4+30]");
			string text = (string)0 + "." + key;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rbx_v1+38]");
			object obj10 = 0;
			val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 88));
			object obj11 = obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ r8_v2+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0214;
			}
		}
		val = defaultValue;
		goto IL_0214;
		IL_0214:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		if (state != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rbx_v1+38]");
			object obj12 = 0;
			object obj13 = obj12;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ rcx_v9+28]");
			object obj14 = (nint)0 >> 31;
			bool flag = obj14 != null;
			object obj15 = (object)(&obj2);
			if (!flag)
			{
				obj15 = obj8;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180755750");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			T result = default(T);
			return result;
		}
		return (T)new NullReferenceException();
	}

	public bool TryGetState<T>(NodeExecutionState state, string key, out T value)
	{
		//IL_00c1: Expected I4, but got O
		//IL_00a4: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ stack_28+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ stack_28+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		string text = NodeID + "." + key;
		if (state != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ stack_28+38]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18077A670");
			bool result = default(bool);
			return result;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void OnValidate()
	{
		//IL_0261: Expected O, but got I4
		//IL_00c5: Expected I, but got O
		//IL_00eb: Expected I, but got O
		//IL_010d: Expected I, but got O
		//IL_0123: Expected I, but got O
		//IL_0131: Expected I, but got O
		//IL_0141: Expected O, but got I
		//IL_017d: Expected O, but got I
		//IL_01de: Expected O, but got I
		//IL_01f2: Expected I, but got O
		//IL_022a: Expected I, but got O
		bool flag = string.IsNullOrEmpty(NodeID);
		bool flag2 = !flag;
		Guid guid = (Guid)0;
		if (!flag2)
		{
			Guid guid2 = Guid.NewGuid();
			string nodeID = guid.ToString();
			NodeID = nodeID;
		}
		if (!(graph != null))
		{
			return;
		}
		NodeGraph nodeGraph = graph;
		if (nodeGraph.nodes == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<Node>.Enumerator enumerator = default(List<Node>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			bool flag3 = obj != null;
			num = unchecked((nint)null);
			if (!flag3)
			{
				continue;
			}
			bool flag4 = obj != this;
			num = unchecked((nint)null);
			if (!flag4)
			{
				continue;
			}
			bool flag5 = (object)obj == null;
			num = unchecked((nint)null);
			if (flag5)
			{
				continue;
			}
			num = (nint)obj;
			nint num2 = (nint)typeof(StateNode);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rdx_v12 (Il2CppClass<SleepyNodes.StateNode>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v142 @ r8_v4 (Il2CppMethodInfo)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rdx_v12 (Il2CppClass<SleepyNodes.StateNode>)+130]");
			if (num3 < 0)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v142 @ r8_v4 (Il2CppMethodInfo)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rax_v20+FFFFFFF8+v250 @ rax_v19*8]");
			if (0 == (nint)typeof(StateNode) && (object)obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ stack_8_v3 (UnityEngine.Object)+30]");
				bool flag6 = (string)0 == NodeID;
				bool flag7 = !flag6;
				num = unchecked((nint)null);
				if (!flag7)
				{
					Guid guid3 = Guid.NewGuid();
					string nodeID2 = guid.ToString();
					NodeID = nodeID2;
					num = unchecked((nint)null);
				}
			}
		}
		enumerator.Dispose();
	}

	public virtual void ResetNode()
	{
	}

	public virtual void OnNotification(NodeExecutionState state, string notif)
	{
	}

	public virtual void OnEnter(NodeExecutionState state)
	{
		//IL_0046: Expected I, but got O
		//IL_004e: Expected I, but got O
		//IL_005e: Expected O, but got I
		//IL_009a: Expected O, but got I
		//IL_0105: Expected O, but got I
		state.Node = this;
		NodeGraph nodeGraph = graph;
		if ((object)graph == null)
		{
			return;
		}
		nint num = (nint)typeof(StateGraph);
		nint num2 = (nint)nodeGraph;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rdx_v4 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r8_v3 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rdx_v4 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r8_v3 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rax_v7+FFFFFFF8+v88 @ rax_v6*8]");
			if (0 == (nint)typeof(StateGraph) && !string.IsNullOrWhiteSpace(state.ID))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rdi_v3 (SleepyNodes.NodeGraph)+48]");
				((Dictionary<string, NodeExecutionState>)0).set_Item(state.ID, state);
			}
		}
	}

	public virtual void OnExecute(NodeExecutionState state)
	{
	}

	public virtual void OnExit(NodeExecutionState state, StateNode To, string connectedFieldName)
	{
		//IL_006a: Expected I, but got O
		//IL_0072: Expected I, but got O
		//IL_0082: Expected O, but got I
		//IL_00be: Expected O, but got I
		//IL_019c: Expected O, but got I
		//IL_0140: Expected O, but got I
		//IL_015a: Expected O, but got I
		state.lastFieldPort = connectedFieldName;
		if (To == null)
		{
			NodeGraph nodeGraph = graph;
			if ((object)graph == null)
			{
				return;
			}
			nint num = (nint)typeof(StateGraph);
			nint num2 = (nint)nodeGraph;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ rdx_v6 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r8_v6 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ rdx_v6 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
			if (num3 < 0)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r8_v6 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v11+FFFFFFF8+v175 @ rax_v10*8]");
			if (0 != (nint)typeof(StateGraph))
			{
				return;
			}
			if (!string.IsNullOrWhiteSpace(state.ID))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rbx_v5 (SleepyNodes.NodeGraph)+40]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rbx_v5 (SleepyNodes.NodeGraph)+40]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ rcx_v13+10]");
					if ((string)0 == state.ID)
					{
						_ = 0;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rbx_v5 (SleepyNodes.NodeGraph)+48]");
				bool flag = ((Dictionary<string, NodeExecutionState>)0).Remove(state.ID);
			}
			else
			{
				_ = 0;
			}
		}
		else
		{
			To.OnEnter(state);
		}
	}

	public virtual void OnExit(NodeExecutionState state, string outFieldName)
	{
		//IL_0040: Expected I, but got O
		//IL_0050: Expected O, but got I
		//IL_000a: Expected I, but got O
		//IL_001a: Expected O, but got I
		if (string.IsNullOrEmpty(outFieldName))
		{
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v6 (Il2CppClass<SleepyNodes.StateNode>)+208]");
			object obj = 0;
			StateNode stateNode = null;
			string text = null;
		}
		else
		{
			StateNode connectedNode = GetConnectedNode<StateNode>(outFieldName, out var connectedField);
			nint num2 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rcx_v5 (Il2CppClass<SleepyNodes.StateNode>)+208]");
			object obj = 0;
			StateNode stateNode = connectedNode;
			string text = connectedField;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v63 @ r10_v1 (should have been resolved before IL gen)");
	}

	public override object GetValue(NodePort port)
	{
		return this;
	}

	public virtual void OnEvent(EventNode.EventData data, NodeExecutionState state)
	{
	}

	protected StateNode()
	{
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		base._002Ector();
	}
}
