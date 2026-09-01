using System;
using System.Collections.Generic;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class GetSetConnectionWithOptions<T> : ConnectionWithOptions<T>
{
	private Func<int> m_Getter;

	private Action<int> m_Setter;

	private Func<List<T>> m_OptionLabelGetter;

	private Action<List<T>> m_OptionLabelSetter;

	protected int _selectedIndex;

	protected List<T> _optionLabels;

	public event Func<int> Getter
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 40;
			Delegate obj2 = this.OptionLabelSetter;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 40;
			Delegate obj2 = this.OptionLabelSetter;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<int> Setter
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_007e: Expected O, but got I4
			object obj = this + 48;
			Delegate obj2 = (Delegate)_selectedIndex;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_007e: Expected O, but got I4
			object obj = this + 48;
			Delegate obj2 = (Delegate)_selectedIndex;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Func<List<T>> OptionLabelGetter
	{
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			//IL_0023: Expected O, but got I4
			List<T> list = _optionLabels;
			object obj = this + 56;
			object obj3 = default(object);
			List<T> list2 = default(List<T>);
			while (true)
			{
				Delegate obj2 = Delegate.Combine((Delegate)(object)list, value);
				nint num = 0;
				if ((object)obj2 == null)
				{
					obj3 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if (obj3 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag = list2 != list;
				list = list2;
				if (!flag)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			//IL_0023: Expected O, but got I4
			List<T> list = _optionLabels;
			object obj = this + 56;
			object obj3 = default(object);
			List<T> list2 = default(List<T>);
			while (true)
			{
				Delegate obj2 = Delegate.Remove((Delegate)(object)list, value);
				nint num = 0;
				if ((object)obj2 == null)
				{
					obj3 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if (obj3 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag = list2 != list;
				list = list2;
				if (!flag)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<List<T>> OptionLabelSetter
	{
		add
		{
			//IL_0010: Expected O, but got I
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_0029: Expected O, but got I4
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>)+40]");
			Delegate obj = (Delegate)0;
			object obj2 = this + 64;
			object obj4 = default(object);
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj, value);
				nint num = 0;
				if ((object)obj3 == null)
				{
					obj4 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if (obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag = (object)obj5 != obj;
				obj = obj5;
				if (!flag)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0010: Expected O, but got I
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_0029: Expected O, but got I4
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>)+40]");
			Delegate obj = (Delegate)0;
			object obj2 = this + 64;
			object obj4 = default(object);
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj, value);
				nint num = 0;
				if ((object)obj3 == null)
				{
					obj4 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if (obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag = (object)obj5 != obj;
				obj = obj5;
				if (!flag)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public GetSetConnectionWithOptions(Func<int> getter, Action<int> setter, Func<List<T>> optionLabelGetter = null, Action<List<T>> optionLabelSetter = null)
	{
		//IL_0010: Expected O, but got I
		//IL_0020: Expected O, but got I
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0300: Expected O, but got I4
		//IL_0316: Expected I, but got O
		//IL_036a: Expected O, but got I4
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00b9: Expected O, but got I4
		//IL_00cf: Expected I, but got O
		//IL_0146: Expected O, but got I
		//IL_0150: Expected I, but got O
		//IL_0160: Expected O, but got I
		//IL_0170: Expected O, but got I
		//IL_0234: Expected O, but got I
		//IL_0244: Expected O, but got I
		//IL_0254: Expected O, but got I
		//IL_0264: Expected O, but got I
		//IL_03ed: Expected O, but got I
		//IL_0401: Expected O, but got I
		//IL_0411: Expected O, but got I
		//IL_0421: Expected O, but got I
		//IL_0477: Expected O, but got I
		//IL_0487: Expected O, but got I
		//IL_0497: Expected O, but got I
		//IL_01ad: Expected O, but got I4
		//IL_0299: Expected O, but got I4
		//IL_02a1: Expected I, but got O
		//IL_0186: Expected O, but got I4
		//IL_0272: Expected O, but got I4
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ stack_30+20]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v26 @ rax_v2+C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D3290");
		Delegate obj3 = this.OptionLabelSetter;
		Delegate obj9 = default(Delegate);
		Delegate obj14 = default(Delegate);
		object obj21 = default(object);
		IntPtr intPtr = default(IntPtr);
		Delegate obj23 = default(Delegate);
		object obj30 = default(object);
		Delegate obj32 = default(Delegate);
		while (true)
		{
			Delegate obj4 = Delegate.Combine(obj3, getter);
			bool flag = (object)obj4 == null;
			Delegate obj5 = obj4;
			Delegate obj7;
			nint num;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if ((object)obj5 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					object obj6 = 0;
					obj7 = obj4;
					num = (nint)typeof(Func<int>);
					goto IL_03d0;
				}
			}
			object obj8 = this + 40;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag2 = (object)obj9 != obj3;
			obj3 = obj9;
			if (flag2)
			{
				continue;
			}
			Delegate obj10 = (Delegate)_selectedIndex;
			while (true)
			{
				Delegate obj11 = Delegate.Combine(obj10, setter);
				bool flag3 = (object)obj11 == null;
				Delegate obj12 = obj11;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag4 = (object)obj12 == null;
					object obj6 = 0;
					obj7 = obj11;
					num = (nint)typeof(Action<int>);
					if (flag4)
					{
						break;
					}
				}
				object obj13 = this + 48;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag5 = (object)obj14 != obj10;
				obj10 = obj14;
				if (flag5)
				{
					continue;
				}
				goto IL_0119;
			}
			break;
			IL_0119:
			if (optionLabelGetter == null)
			{
				goto IL_0207;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ stack_30+20]");
			object obj15 = 0;
			num = (nint)_optionLabels;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v353 @ rax_v31+C0]");
			object obj16 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v355 @ rcx_v28+48]");
			object obj17 = 0;
			Delegate b = optionLabelGetter;
			while (true)
			{
				Delegate obj18 = Delegate.Combine((Delegate)num, b);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ r15_v7+20]");
				object obj19 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v492 @ rcx_v31+C0]");
				object obj20 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v493 @ rdx_v27+8]");
				obj3 = (Delegate)0;
				if ((object)obj18 == null)
				{
					obj21 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag6 = obj21 == null;
					object obj6 = 0;
					obj7 = obj18;
					if (flag6)
					{
						break;
					}
				}
				object obj22 = this + 56;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag7 = intPtr != (IntPtr)num;
				num = intPtr;
				b = optionLabelGetter;
				if (flag7)
				{
					continue;
				}
				goto IL_0207;
			}
			goto IL_03d0;
			IL_0207:
			if ((object)obj23 == null)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ stack_30+20]");
			object obj24 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>)+40]");
			obj7 = (Delegate)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v468 @ rax_v22+C0]");
			object obj25 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v470 @ rcx_v19+50]");
			object obj26 = 0;
			while (true)
			{
				Delegate obj27 = Delegate.Combine(obj7, obj23);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ r12_v8+20]");
				object obj28 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ rcx_v22+C0]");
				object obj29 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v541 @ rdx_v22+18]");
				obj3 = (Delegate)0;
				if ((object)obj27 == null)
				{
					obj30 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag8 = obj30 == null;
					object obj6 = 0;
					num = (nint)obj27;
					if (flag8)
					{
						break;
					}
				}
				object obj31 = this + 64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag9 = (object)obj32 != obj7;
				obj7 = obj32;
				if (!flag9)
				{
					return;
				}
			}
			goto IL_0446;
			IL_03d0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			goto IL_0446;
			IL_0446:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			break;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public override int Get()
	{
		//IL_004b: Expected I4, but got O
		Action<List<T>> optionLabelSetter = this.OptionLabelSetter;
		if (this.OptionLabelSetter != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v4 @ rcx_v1 (System.Action`1<System.Collections.Generic.List`1<T>>)+18] (should have been resolved before IL gen)");
			int result = default(int);
			return result;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public override void Set(int selectedIndex)
	{
		int selectedIndex2 = _selectedIndex;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v2 @ rcx_v1 (System.Int32)+18] (should have been resolved before IL gen)");
	}

	public int GetLastKnownValue()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>)+48]");
		return 0;
	}

	public override List<T> GetOptionLabels()
	{
		//IL_0052: Expected O, but got I
		//IL_0045: Expected O, but got I
		if (_optionLabels != null)
		{
			List<T> optionLabels = _optionLabels;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v14._size (System.Int32) (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>)+50]");
			return (List<T>)0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>)+50]");
		return (List<T>)0;
	}

	public override void SetOptionLabels(List<T> optionLabels)
	{
		//IL_003a: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>)+40]");
		if ((nint)0 != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>)+40]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v21 @ rcx_v3+18] (should have been resolved before IL gen)");
		}
	}

	public override void RefreshOptionLabels()
	{
		//IL_000b: Expected I, but got O
		//IL_001b: Expected O, but got I
		//IL_002b: Expected O, but got I
		_ = 0;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public int GetLastSelectedIndex()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.GetSetConnectionWithOptions`1<T>)+48]");
		return 0;
	}

	public void SetLastSelectedIndex(int index)
	{
	}
}
