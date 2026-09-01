using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class MaterialPropertyBlockColorSetter : MonoBehaviour
{
	public Renderer Renderer;

	public int MaterialIndex;

	[NonSerialized]
	protected Dictionary<string, Color> _scheduledColors;

	[NonSerialized]
	protected MaterialPropertyBlock _propertyBlock;

	private readonly string[] _colorPropertyNames = new string[3] { "_BaseColor", "_MainColor", "_Color" };

	public void Init()
	{
		if (Renderer == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
			Renderer renderer = default(Renderer);
			Renderer = renderer;
		}
	}

	public Material GetSharedMaterial()
	{
		//IL_004f: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
		int materialIndex = MaterialIndex;
		int materialIndex2 = MaterialIndex;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rax_v5+18]");
		if ((nint)materialIndex2 < (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rax_v5+20+v49 @ rax_v6 (System.Int32)*8]");
			return (Material)0;
		}
		return (Material)(object)new IndexOutOfRangeException();
	}

	public bool HasScheduledChanges()
	{
		if (_scheduledColors == null)
		{
			return false;
		}
		int count = _scheduledColors.Count;
		int num = count ^ count;
		int num2 = count & num;
		bool flag = num2 < 0;
		bool flag2 = count < 0;
		bool flag3 = count == 0;
		bool flag4 = flag2 == flag;
		bool flag5 = !flag3;
		return flag5 & flag4;
	}

	public bool HasScheduledProperty(string propertyName)
	{
		if (_scheduledColors != null)
		{
			bool flag = _scheduledColors.ContainsKey(propertyName);
			bool flag2 = !flag;
			return !flag2;
		}
		return false;
	}

	protected unsafe void schedule<T>(ref Dictionary<string, T> dict, string propertyName, T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_0064: Expected O, but got I
		//IL_0074: Expected O, but got I
		//IL_008a: Expected O, but got I
		//IL_018e: Expected O, but got I
		//IL_0196: Expected O, but got Ref
		//IL_00b9: Expected O, but got I
		//IL_00d3: Expected O, but got I
		//IL_00ff: Expected O, but got I
		//IL_010d: Expected O, but got Ref
		//IL_011d: Expected O, but got I
		//IL_01fa: Expected O, but got I
		//IL_020a: Expected O, but got I
		//IL_0220: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+60]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1+38]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v2+18]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ r9_v1+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ r9_v1+FC]");
		object obj8 = default(object);
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ r9_v1+FC]");
			object obj7 = (nint)0 + (nint)15;
			obj8 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ r9_v1+FC]");
			if ((nint)obj7 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			if (dict != null)
			{
				goto IL_00ef;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1+38]");
		object obj9 = 0;
		object obj10 = null;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1+38]");
		object obj11 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180831000");
		ref Dictionary<string, T> reference = ref *(Dictionary<string, T>*)obj10;
		goto IL_00ef;
		IL_00ef:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1+38]");
		object obj12 = 0;
		T val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 88));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rax_v12+18]");
		object obj13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rcx_v4+28]");
		if ((nint)0 < (nint)0)
		{
			val = value;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1+38]");
		object obj14 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rax_v14+18]");
		object obj15 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r8_v2+28]");
		object obj16 = (nint)0 >> 31;
		if (obj16 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		}
		else
		{
			object obj17 = obj8;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180722C40");
	}

	protected unsafe void addOrUpdateScheduled<T>(Dictionary<string, T> source, string propertyName, T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_0069: Expected O, but got I
		//IL_0079: Expected O, but got I
		//IL_008f: Expected O, but got I
		//IL_01ad: Expected O, but got Ref
		//IL_00be: Expected O, but got I
		//IL_00d8: Expected O, but got I
		//IL_00e6: Expected O, but got Ref
		//IL_00f6: Expected O, but got I
		//IL_0243: Expected O, but got I
		//IL_0253: Expected O, but got I
		//IL_0269: Expected O, but got I
		//IL_0283: Expected O, but got Ref
		//IL_01cc: Expected O, but got I
		//IL_01dc: Expected O, but got I
		//IL_01f2: Expected O, but got I
		//IL_020c: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+50]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rdi_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rdi_v1+38]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rax_v2+10]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rcx_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rcx_v2+FC]");
		object obj7 = default(object);
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj7 = (object)(&obj2);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rdi_v1+38]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082ABA0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rdi_v1+38]");
		object obj9 = 0;
		T val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ rcx_v4+10]");
		object obj10 = 0;
		object obj11 = default(object);
		if (obj11 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rax_v9+28]");
			if ((nint)0 < (nint)0)
			{
				val = value;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rdi_v1+38]");
			object obj12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v177 @ rax_v17+10]");
			object obj13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ rcx_v9+28]");
			object obj14 = (nint)0 >> 31;
			bool flag = obj14 != null;
			object obj15 = (object)(&obj2);
			if (!flag)
			{
				obj15 = obj7;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082A970");
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rax_v9+28]");
			if ((nint)0 < (nint)0)
			{
				val = value;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rdi_v1+38]");
			object obj16 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ rax_v11+10]");
			object obj17 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rcx_v6+28]");
			object obj18 = (nint)0 >> 31;
			bool flag2 = obj18 != null;
			object obj19 = (object)(&obj2);
			if (!flag2)
			{
				obj19 = obj7;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180831400");
		}
	}

	protected unsafe T getScheduled<T>(Dictionary<string, T> dict, string propertyName, T defaultValue)
	{
		//IL_0008: Expected O, but got Ref
		//IL_001d: Expected O, but got I
		//IL_0069: Expected O, but got I
		//IL_0079: Expected O, but got I
		//IL_008f: Expected O, but got I
		//IL_00be: Expected O, but got I
		//IL_0114: Expected O, but got I
		//IL_0122: Expected O, but got Ref
		//IL_0132: Expected O, but got I
		//IL_00f5: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+58]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbx_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbx_v1+38]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rax_v2+8]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rcx_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rcx_v2+FC]");
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			if (dict == null)
			{
				goto IL_0104;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbx_v1+38]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082ABA0");
		object obj8 = default(object);
		if (obj8 == null)
		{
			goto IL_0104;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbx_v1+38]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
		goto IL_0167;
		IL_0104:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbx_v1+38]");
		object obj10 = 0;
		T val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rax_v10+8]");
		object obj11 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rcx_v6+28]");
		if ((nint)0 < (nint)0)
		{
			T val2 = default(T);
			val = val2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		goto IL_0167;
		IL_0167:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	protected unsafe Color getProperty(string propertyName, Color defaultValue = default(Color))
	{
		//IL_00b7: Expected native int or pointer, but got O
		float r;
		if (_propertyBlock != null && _propertyBlock.HasColor(propertyName))
		{
			if (_propertyBlock == null)
			{
				return (Color)new NullReferenceException();
			}
			r = _propertyBlock.GetColor(propertyName).r;
		}
		else
		{
			r = defaultValue.r;
		}
		Color color = default(Color);
		((Color*)(nint)color)->r = r;
		return color;
	}

	protected bool hasColorProperty(string propertyName)
	{
		//IL_0045: Expected I4, but got O
		if (_propertyBlock != null)
		{
			return _propertyBlock.HasColor(propertyName);
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	protected unsafe T get<T>(Dictionary<string, T> dict, string propertyName, Func<string, T, T> propertyGetter, T defaultValue = default(T))
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_021e: Expected O, but got I
		//IL_022e: Expected O, but got I
		//IL_0244: Expected O, but got I
		//IL_03cb: Expected O, but got I
		//IL_03d3: Expected O, but got Ref
		//IL_0283: Expected O, but got I
		//IL_0067: Expected O, but got I
		//IL_013a: Expected O, but got I
		//IL_0148: Expected O, but got Ref
		//IL_0158: Expected O, but got I
		//IL_007c: Expected O, but got I
		//IL_0190: Expected O, but got I
		//IL_01a5: Expected O, but got I
		//IL_01b5: Expected O, but got I
		//IL_01cb: Expected O, but got I
		//IL_01e5: Expected O, but got Ref
		//IL_00b3: Expected O, but got I
		//IL_00c1: Expected O, but got Ref
		//IL_00d1: Expected O, but got I
		//IL_038e: Expected O, but got Ref
		//IL_039e: Expected O, but got I
		//IL_03a6: Expected O, but got Ref
		//IL_02e3: Expected O, but got I
		//IL_02f3: Expected O, but got I
		//IL_0309: Expected O, but got I
		//IL_0109: Expected O, but got I
		//IL_0338: Expected O, but got Ref
		//IL_0348: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+70]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
		bool flag = (nint)0 != 0;
		string text = propertyName;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+50]");
			text = (string)0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v2+10]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r10_v1+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r10_v1+FC]");
		object obj8 = default(object);
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r10_v1+FC]");
			object obj7 = (nint)0 + (nint)15;
			obj8 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r10_v1+FC]");
			if ((nint)obj7 <= 0)
			{
				goto IL_03f2;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r10_v1+FC]");
		object obj9 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r10_v1+FC]");
		if ((nint)obj9 <= 0)
		{
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		if (dict != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
			object obj10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082ABA0");
			object obj11 = default(object);
			if (obj11 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
				object obj12 = 0;
				object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 96));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v175 @ rax_v27+10]");
				object obj14 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ rcx_v11+28]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+60]");
					obj13 = 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
				object obj15 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ rax_v29+10]");
				object obj16 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rdx_v11+28]");
				object obj17 = (nint)0 >> 31;
				object obj18 = default(object);
				if (obj17 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				}
				else
				{
					obj18 = obj8;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180723040");
				object obj19 = (object)(&obj2);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r10_v1+FC]");
				object obj20 = 0;
				object obj21 = obj18;
				goto IL_03f2;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
		object obj22 = 0;
		object obj23 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 96));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ rax_v18+10]");
		object obj24 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ rcx_v6+28]");
		if ((nint)0 < (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+60]");
			obj23 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		if (propertyGetter != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rdi_v1+38]");
			object obj25 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ rax_v21+10]");
			object obj26 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ rcx_v8+28]");
			object obj27 = (nint)0 >> 31;
			bool flag2 = obj27 != null;
			object obj28 = (object)(&obj2);
			if (!flag2)
			{
				obj28 = obj8;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [propertyGetter @ r9 (System.Func`3<System.String, T, T>)+18] (should have been resolved before IL gen)");
			object obj19 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r10_v1+FC]");
			object obj20 = 0;
			object obj21 = (object)(&obj2);
			goto IL_03f2;
		}
		return (T)new NullReferenceException();
		IL_03f2:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	public void ScheduleColor(string propertyName, Color color)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		object obj = this + 48;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180723300");
	}

	public unsafe Color GetScheduledColor(string propertyName, Color defaultValue = default(Color))
	{
		//IL_0017: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180723040");
		Color color = default(Color);
		float r = default(float);
		((Color*)(nint)color)->r = r;
		return color;
	}

	public unsafe Color GetPropertyColor(string propertyName, Color defaultValue = default(Color))
	{
		//IL_00c9: Expected native int or pointer, but got O
		float r;
		if (_propertyBlock != null)
		{
			if (_propertyBlock.HasColor(propertyName))
			{
				if (_propertyBlock == null)
				{
					return (Color)new NullReferenceException();
				}
				r = _propertyBlock.GetColor(propertyName).r;
			}
			else
			{
				r = defaultValue.r;
			}
		}
		else
		{
			r = defaultValue.r;
		}
		Color color = default(Color);
		((Color*)(nint)color)->r = r;
		return color;
	}

	public unsafe Color GetColor(string propertyName, Color defaultValue = default(Color))
	{
		//IL_000d: Expected native int or pointer, but got O
		Func<string, Color, Color> func = GetPropertyColor;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180723130");
		Color color = default(Color);
		float r = default(float);
		((Color*)(nint)color)->r = r;
		return color;
	}

	protected unsafe void applyList<T>(Dictionary<string, T> dict, Action<string, T> setter)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0403: Expected O, but got I
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Expected O, but got Unknown
		//IL_0211: Expected O, but got I
		//IL_0438: Expected O, but got I
		//IL_0448: Expected O, but got I
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Expected O, but got Unknown
		//IL_0255: Expected O, but got Ref
		//IL_026b: Expected O, but got I
		//IL_01b1: Expected O, but got Ref
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Expected O, but got Unknown
		//IL_02bd: Expected O, but got I
		//IL_001f: Expected O, but got I8
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Expected O, but got Unknown
		//IL_030f: Expected O, but got I
		//IL_0031: Expected O, but got I8
		//IL_00f2: Expected O, but got Ref
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_0353: Expected O, but got Ref
		//IL_0373: Expected O, but got I
		//IL_0043: Expected O, but got I8
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Expected O, but got Unknown
		//IL_0055: Expected O, but got I8
		//IL_0067: Expected O, but got I8
		//IL_0138: Expected O, but got I
		//IL_0148: Expected O, but got I
		//IL_0158: Expected O, but got I
		//IL_016e: Expected O, but got I
		//IL_0188: Expected O, but got Ref
		//IL_0099: Expected O, but got Ref
		//IL_00ac: Expected O, but got Ref
		//IL_00c6: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		nint num2 = 0;
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v4 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, T>+Enumerator<System.String, T>>)+FC]");
		object obj3 = (nint)0 + (nint)16;
		object obj4 = obj3 + 15;
		object obj8 = default(object);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rcx_v6 (Il2CppClass<T>)+FC]");
			object obj5 = (nint)0 + (nint)15;
			object obj6 = obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rcx_v6 (Il2CppClass<T>)+FC]");
			if ((nint)obj6 <= 0)
			{
				obj5 = 1152921504606846960L;
			}
			object obj7 = obj5 & -16;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj8 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v2 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, T>+Enumerator<System.String, T>>)+FC]");
			object obj9 = (nint)0 + (nint)15;
			object obj10 = obj9;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v2 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, T>+Enumerator<System.String, T>>)+FC]");
			if ((nint)obj10 <= 0)
			{
				obj9 = 1152921504606846960L;
			}
			object obj11 = obj9 & -16;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rcx_v3 (Il2CppClass<System.Collections.Generic.KeyValuePair`2<System.String, T>>)+FC]");
			object obj12 = (nint)0 + (nint)15;
			object obj13 = obj12;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rcx_v3 (Il2CppClass<System.Collections.Generic.KeyValuePair`2<System.String, T>>)+FC]");
			if ((nint)obj13 <= 0)
			{
				obj12 = 1152921504606846960L;
			}
			object obj14 = obj12 & -16;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v2 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, T>+Enumerator<System.String, T>>)+FC]");
			object obj15 = (nint)0 + (nint)15;
			object obj16 = obj15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v2 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, T>+Enumerator<System.String, T>>)+FC]");
			if ((nint)obj16 <= 0)
			{
				obj15 = 1152921504606846960L;
			}
			object obj17 = obj15 & -16;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rcx_v3 (Il2CppClass<System.Collections.Generic.KeyValuePair`2<System.String, T>>)+FC]");
			object obj18 = (nint)0 + (nint)15;
			object obj19 = obj18;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rcx_v3 (Il2CppClass<System.Collections.Generic.KeyValuePair`2<System.String, T>>)+FC]");
			if ((nint)obj19 <= 0)
			{
				obj18 = 1152921504606846960L;
			}
			object obj20 = obj18 & -16;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+88]");
			if ((nint)0 == 0)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			_ = 0;
			object obj21 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 152));
			object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			_ = ref obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v2 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, T>+Enumerator<System.String, T>>)+FC]");
			object obj23 = 0;
		}
		object obj26 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+98]");
			object obj24 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v351 @ rax_v34+38]");
			object obj25 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180843BB0");
			if (obj26 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				object obj27 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 136));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
				if (setter == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+98]");
				object obj28 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ rax_v46+38]");
				object obj29 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ rcx_v34+50]");
				object obj30 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v376 @ rax_v47+28]");
				object obj31 = (nint)0 >> 31;
				bool flag = obj31 != null;
				object obj23 = (object)(&obj2);
				if (!flag)
				{
					obj23 = obj8;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [setter @ r8 (System.Action`2<System.String, T>)+18] (should have been resolved before IL gen)");
				continue;
			}
			object obj32 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806F8000");
			return;
		}
		throw new NullReferenceException();
	}

	public void Apply()
	{
		if (_propertyBlock == null)
		{
			MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
			_propertyBlock = propertyBlock;
		}
		if (!(Renderer == null))
		{
			Renderer.Internal_GetPropertyBlockMaterialIndex(_propertyBlock, MaterialIndex);
			Action<string, Color> setter = _propertyBlock.SetColor;
			applyList(_scheduledColors, setter);
			Renderer.Internal_SetPropertyBlockMaterialIndex(_propertyBlock, MaterialIndex);
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Renderer is null.");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public void ClearScheduled()
	{
		if (_scheduledColors != null)
		{
			_scheduledColors.Clear();
		}
	}

	public void ResetProperties()
	{
		if (!(Renderer == null))
		{
			MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
			_propertyBlock = propertyBlock;
			Renderer.Internal_SetPropertyBlockMaterialIndex(_propertyBlock, MaterialIndex);
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Renderer is null.");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public void SetMainColor(Color color)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_005a: Expected O, but got I4
		//IL_0063: Expected O, but got I4
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		string text;
		if (Renderer != null)
		{
			Material sharedMaterial = Renderer.GetSharedMaterial();
			string[] colorPropertyNames = _colorPropertyNames;
			object obj = _colorPropertyNames + 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj3 < colorPropertyNames.Length)
			{
				text = (string)obj;
				if (!sharedMaterial.HasProperty((string)obj))
				{
					obj2++;
					obj += 8;
					obj3 = obj2;
					continue;
				}
				goto IL_01ff;
			}
		}
		text = null;
		goto IL_01ff;
		IL_01ff:
		object obj4 = this + 48;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180723300");
		if (_propertyBlock == null)
		{
			MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
			_propertyBlock = propertyBlock;
		}
		if (!(Renderer == null))
		{
			Renderer.Internal_GetPropertyBlockMaterialIndex(_propertyBlock, MaterialIndex);
			Action<string, Color> setter = _propertyBlock.SetColor;
			applyList(_scheduledColors, setter);
			Renderer.Internal_SetPropertyBlockMaterialIndex(_propertyBlock, MaterialIndex);
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Renderer is null.");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public string GetMainColorPropertyName()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_0093: Expected O, but got I4
		//IL_009c: Expected O, but got I4
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		if (!(Renderer != null))
		{
			goto IL_0113;
		}
		if ((object)Renderer != null)
		{
			Material sharedMaterial = Renderer.GetSharedMaterial();
			string[] colorPropertyNames = _colorPropertyNames;
			if (_colorPropertyNames != null)
			{
				object obj = _colorPropertyNames + 32;
				object obj2 = 0;
				object obj3 = 0;
				while ((nint)obj3 < colorPropertyNames.Length)
				{
					if ((object)sharedMaterial != null)
					{
						if (!sharedMaterial.HasProperty((string)obj))
						{
							obj2++;
							obj += 8;
							obj3 = obj2;
							continue;
						}
						return (string)obj;
					}
					goto IL_0122;
				}
				goto IL_0113;
			}
		}
		goto IL_0122;
		IL_0113:
		return null;
		IL_0122:
		return (string)(object)new NullReferenceException();
	}
}
