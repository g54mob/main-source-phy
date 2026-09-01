using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

[Serializable]
public abstract class SettingWithValue<TValue> : ISettingWithValue<TValue>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
{
	[NonSerialized]
	protected bool _hasUserData;

	protected bool _isActive;

	public string ID;

	public const string _IdFieldName = "ID";

	public bool ApplyImmediately;

	protected List<string> _groups;

	protected TValue _defaultValue;

	[NonSerialized]
	public bool HasDefaultValue;

	public bool IgnoreConnectionDefaults;

	protected bool _hasChanged;

	protected Func<string, string> _translateFunc;

	protected List<Action<TValue>> _applyListeners;

	protected List<Action<TValue>> _changeListeners;

	protected List<Action<TValue>> _pulledFromConnectionListeners;

	protected List<Action> _genericPulledFromConnectionListeners;

	private Action<ISetting> m_OnSettingChanged;

	private Action<ISetting> m_OnSettingApplied;

	public bool IsActive
	{
		get
		{
			//IL_0024: Expected O, but got I
			//IL_0033: Expected I4, but got O
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = default(object);
			return (byte)(int)obj2 != 0;
		}
		set
		{
			//IL_0034: Expected O, but got I
			//IL_004a: Expected O, but got I
			//IL_006a: Expected O, but got I
			//IL_007c: Expected O, but got I4
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj2 = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj3 = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj4 = value;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			Logger.LogWarning("Changing the IsActive state of settings at runtime is not recommended.");
		}
	}

	public event Action<ISetting> OnSettingChanged
	{
		add
		{
			//IL_00d6: Expected O, but got I
			//IL_000e: Expected O, but got I4
			//IL_0066: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj = (nint)0 + (nint)480;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj3 = default(object);
			Delegate obj2 = (Delegate)obj3;
			object obj6 = default(object);
			Delegate obj8 = default(Delegate);
			while (true)
			{
				Delegate obj4 = Delegate.Combine(obj2, value);
				if ((object)obj4 == null)
				{
					object obj5 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj6 == null;
					object obj5 = obj6;
					if (flag)
					{
						break;
					}
				}
				nint num2 = 0;
				IntPtr intPtr2 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rax_v9 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
				object obj7 = (nint)0 + (nint)480;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj8 != obj2;
				obj2 = obj8;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_00d6: Expected O, but got I
			//IL_000e: Expected O, but got I4
			//IL_0066: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj = (nint)0 + (nint)480;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj3 = default(object);
			Delegate obj2 = (Delegate)obj3;
			object obj6 = default(object);
			Delegate obj8 = default(Delegate);
			while (true)
			{
				Delegate obj4 = Delegate.Remove(obj2, value);
				if ((object)obj4 == null)
				{
					object obj5 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj6 == null;
					object obj5 = obj6;
					if (flag)
					{
						break;
					}
				}
				nint num2 = 0;
				IntPtr intPtr2 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rax_v9 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
				object obj7 = (nint)0 + (nint)480;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj8 != obj2;
				obj2 = obj8;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<ISetting> OnSettingApplied
	{
		add
		{
			//IL_00d6: Expected O, but got I
			//IL_000e: Expected O, but got I4
			//IL_0066: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj = (nint)0 + (nint)512;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj3 = default(object);
			Delegate obj2 = (Delegate)obj3;
			object obj6 = default(object);
			Delegate obj8 = default(Delegate);
			while (true)
			{
				Delegate obj4 = Delegate.Combine(obj2, value);
				if ((object)obj4 == null)
				{
					object obj5 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj6 == null;
					object obj5 = obj6;
					if (flag)
					{
						break;
					}
				}
				nint num2 = 0;
				IntPtr intPtr2 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rax_v9 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
				object obj7 = (nint)0 + (nint)512;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj8 != obj2;
				obj2 = obj8;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_00d6: Expected O, but got I
			//IL_000e: Expected O, but got I4
			//IL_0066: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj = (nint)0 + (nint)512;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj3 = default(object);
			Delegate obj2 = (Delegate)obj3;
			object obj6 = default(object);
			Delegate obj8 = default(Delegate);
			while (true)
			{
				Delegate obj4 = Delegate.Remove(obj2, value);
				if ((object)obj4 == null)
				{
					object obj5 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj6 == null;
					object obj5 = obj6;
					if (flag)
					{
						break;
					}
				}
				nint num2 = 0;
				IntPtr intPtr2 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rax_v9 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
				object obj7 = (nint)0 + (nint)512;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj8 != obj2;
				obj2 = obj8;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public virtual ConnectionSO GetConnectionSO()
	{
		return null;
	}

	public virtual void SetConnectionSO(ConnectionSO connectionSO)
	{
	}

	public virtual SettingData.DataType GetConnectionSettingDataType()
	{
		return SettingData.DataType.Unknown;
	}

	public SettingWithValue(SettingData data, List<string> groups)
	{
		//IL_001e: Expected O, but got I
		//IL_0034: Expected O, but got I
		//IL_0054: Expected O, but got I
		//IL_006c: Expected O, but got I4
		//IL_0094: Expected O, but got I
		//IL_00aa: Expected O, but got I
		//IL_00cf: Expected O, but got I
		//IL_00e2: Expected O, but got I4
		//IL_011e: Expected O, but got I
		//IL_0134: Expected O, but got I
		//IL_0154: Expected O, but got I
		//IL_017f: Expected I, but got O
		//IL_01a7: Expected O, but got I
		//IL_01bd: Expected O, but got I
		//IL_01e2: Expected O, but got I
		//IL_0221: Expected O, but got I
		//IL_0237: Expected O, but got I
		//IL_0257: Expected O, but got I
		//IL_026f: Expected O, but got I4
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj2 = (nint)0 + (nint)32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj3 = (nint)0 + (nint)32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj4 = 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		nint num2 = 0;
		IntPtr intPtr2 = num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v7 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v7 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj6 = (nint)0 + (nint)128;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v7 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj7 = (nint)0 + (nint)128;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj8 = 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		nint num3 = 0;
		IntPtr intPtr3 = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj10 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj11 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object iD = data.ID;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		nint num4 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v104 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+548] (should have been resolved before IL gen)");
		nint num5 = 0;
		IntPtr intPtr4 = num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rax_v21 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj12 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rax_v21 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj13 = (nint)0 + (nint)160;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rax_v21 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj14 = (nint)0 + (nint)160;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		nint num6 = 0;
		IntPtr intPtr5 = num6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rax_v26 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj15 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rax_v26 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj16 = (nint)0 + (nint)128;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rax_v26 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj17 = (nint)0 + (nint)128;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj18 = 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public SettingWithValue(string id, List<string> groups)
	{
		//IL_001e: Expected O, but got I
		//IL_0034: Expected O, but got I
		//IL_0054: Expected O, but got I
		//IL_006c: Expected O, but got I4
		//IL_0094: Expected O, but got I
		//IL_00aa: Expected O, but got I
		//IL_00cf: Expected O, but got I
		//IL_00e2: Expected O, but got I4
		//IL_0119: Expected O, but got I
		//IL_012f: Expected O, but got I
		//IL_0154: Expected O, but got I
		//IL_0193: Expected O, but got I
		//IL_01a9: Expected O, but got I
		//IL_01c9: Expected O, but got I
		//IL_0208: Expected O, but got I
		//IL_021e: Expected O, but got I
		//IL_0243: Expected O, but got I
		//IL_0256: Expected O, but got I4
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj2 = (nint)0 + (nint)32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj3 = (nint)0 + (nint)32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj4 = 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		nint num2 = 0;
		IntPtr intPtr2 = num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v7 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v7 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj6 = (nint)0 + (nint)128;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v7 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj7 = (nint)0 + (nint)128;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj8 = 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		nint num3 = 0;
		IntPtr intPtr3 = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v13 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v13 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj10 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v13 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj11 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		nint num4 = 0;
		IntPtr intPtr4 = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rax_v18 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj12 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rax_v18 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj13 = (nint)0 + (nint)160;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rax_v18 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj14 = (nint)0 + (nint)160;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		nint num5 = 0;
		IntPtr intPtr5 = num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v23 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj15 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v23 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj16 = (nint)0 + (nint)128;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v23 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj17 = (nint)0 + (nint)128;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj18 = 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public virtual void OnBeforeSerialize()
	{
	}

	public virtual void OnAfterDeserialize()
	{
		//IL_0024: Expected O, but got I
		//IL_005e: Expected O, but got I
		//IL_0074: Expected O, but got I
		//IL_0099: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		string text = ((string)obj2).Trim();
		nint num2 = 0;
		IntPtr intPtr2 = num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rcx_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rcx_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj4 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rcx_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj5 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj6 = text;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public unsafe virtual void InitializeConnection()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0028: Expected O, but got I
		//IL_003e: Expected O, but got I
		//IL_01af: Expected I, but got O
		//IL_01b7: Expected O, but got Ref
		//IL_0062: Expected I, but got O
		//IL_00b5: Expected I, but got O
		//IL_011b: Expected O, but got I
		//IL_014a: Expected O, but got I
		//IL_0160: Expected O, but got I
		//IL_017a: Expected O, but got Ref
		//IL_0204: Expected I, but got O
		//IL_0212: Expected O, but got Ref
		//IL_0235: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r8_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+30]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rax_v2+FC]");
		object obj5 = default(object);
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			nint num2 = (nint)this;
			obj5 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v45 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+588] (should have been resolved before IL gen)");
			object obj6 = default(object);
			if (obj6 == null)
			{
				return;
			}
		}
		nint num3 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v62 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+5C8] (should have been resolved before IL gen)");
		nint num4 = 0;
		object obj7 = default(object);
		IConnection<TValue> defaultFromConnection;
		if (obj7 == null)
		{
			defaultFromConnection = null;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			IConnection<TValue> connection = default(IConnection<TValue>);
			bool flag = connection == null;
			defaultFromConnection = connection;
			if (flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				return;
			}
		}
		nint num5 = (nint)this;
		((SettingWithValue<>)(object)this).SetDefaultFromConnection(defaultFromConnection);
		nint num6 = 0;
		IntPtr intPtr = num6;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj8 = default(object);
		if (obj8 == null)
		{
			nint num7 = 0;
			IntPtr intPtr2 = num7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ rax_v19 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj9 = (nint)0 + (nint)192;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			nint num8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rcx_v12 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+30]");
			object obj10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v243 @ rax_v23+28]");
			object obj11 = (nint)0 >> 31;
			bool flag2 = obj11 != null;
			object obj12 = (object)(&obj2);
			if (!flag2)
			{
				obj12 = obj5;
			}
			nint num9 = (nint)this;
			object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64));
			_ = 1;
			obj = obj12;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v247 @ rcx_v15 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+4F0]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v249 @ rax_v25+10] (should have been resolved before IL gen)");
		}
	}

	public string GetID()
	{
		//IL_0024: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object result = default(object);
		return (string)result;
	}

	public void SetHasUserData(bool loaded)
	{
		//IL_001e: Expected O, but got I
		//IL_003f: Expected O, but got I4
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = loaded;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public bool HasUserData()
	{
		//IL_001d: Expected I4, but got O
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj = default(object);
		return (byte)(int)obj != 0;
	}

	public abstract SettingData.DataType GetDataType();

	public abstract TValue GetValue();

	public abstract void SetValue(TValue value, bool propagateChange = true);

	public abstract void SetValueFromObject(object value, bool propagateChange = true);

	public bool MatchesID(string id)
	{
		//IL_0024: Expected O, but got I
		//IL_008d: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object value = default(object);
		if (!string.IsNullOrEmpty((string)value) && !string.IsNullOrEmpty(id))
		{
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v9 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj2 = (nint)0 + (nint)64;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj3 = default(object);
			return (string)obj3 == id;
		}
		return false;
	}

	public unsafe virtual void SetDefault(TValue defaultValue)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0028: Expected O, but got I
		//IL_003e: Expected O, but got I
		//IL_00bb: Expected O, but got Ref
		//IL_00d1: Expected O, but got I
		//IL_0127: Expected O, but got I
		//IL_014f: Expected O, but got I
		//IL_0165: Expected O, but got I
		//IL_0080: Expected O, but got I
		//IL_0093: Expected O, but got I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+30]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rax_v2+FC]");
		TValue val;
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			val = (TValue)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 40));
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+30]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v8+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_00f9;
			}
		}
		val = defaultValue;
		goto IL_00f9;
		IL_00f9:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num3 = 0;
		IntPtr intPtr = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v11 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj6 = (nint)0 + (nint)192;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7800");
		nint num4 = 0;
		IntPtr intPtr2 = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj8 = (nint)0 + (nint)224;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj9 = (nint)0 + (nint)224;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj10 = 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public unsafe virtual void SetDefaultFromConnection(IConnection<TValue> connection)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0028: Expected O, but got I
		//IL_003e: Expected O, but got I
		//IL_0130: Expected I, but got O
		//IL_0138: Expected O, but got Ref
		//IL_0081: Expected O, but got I
		//IL_00d6: Expected O, but got I
		//IL_00ec: Expected O, but got I
		//IL_0106: Expected O, but got Ref
		//IL_0165: Expected I, but got O
		//IL_0173: Expected O, but got Ref
		//IL_0188: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ r9_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+30]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rax_v2+FC]");
		object obj5 = default(object);
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			nint num2 = (nint)this;
			obj5 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v41 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+588] (should have been resolved before IL gen)");
			object obj6 = default(object);
			if (obj6 == null)
			{
				return;
			}
		}
		nint num3 = 0;
		IntPtr intPtr = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v11 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj7 = (nint)0 + (nint)256;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj8 = default(object);
		if (obj8 == null)
		{
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180038F60");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ rcx_v7 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+30]");
			object obj9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rax_v19+28]");
			object obj10 = (nint)0 >> 31;
			bool flag = obj10 != null;
			object obj11 = (object)(&obj2);
			if (!flag)
			{
				obj11 = obj5;
			}
			nint num6 = (nint)this;
			object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v187 @ rcx_v10 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+510]");
			object obj13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v188 @ rax_v20+10] (should have been resolved before IL gen)");
		}
	}

	public abstract void ResetToDefault();

	public void ResetToUnappliedValue(bool propagateChange = true)
	{
		//IL_011c: Expected O, but got I
		//IL_000a: Expected I, but got O
		//IL_0036: Expected I, but got O
		//IL_0056: Expected O, but got I
		//IL_0066: Expected O, but got I
		//IL_0076: Expected O, but got I
		//IL_008e: Expected O, but got I
		//IL_00a4: Expected O, but got I
		//IL_00c4: Expected O, but got I
		//IL_00dc: Expected O, but got I4
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = --128;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 == null)
		{
			nint num2 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v53 @ rdx_v5 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+588] (should have been resolved before IL gen)");
			object obj3 = default(object);
			if (obj3 != null)
			{
				nint num3 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v93 @ r8_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+5B8] (should have been resolved before IL gen)");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v101 @ rcx_v8 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+58]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rax_v14+20]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rcx_v9+C0]");
				object obj6 = 0;
				object obj7 = obj6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rcx_v10+80]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rcx_v10+80]");
				object obj9 = (nint)0 + (nint)288;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rcx_v10+80]");
				object obj10 = (nint)0 + (nint)288;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				return;
			}
		}
		Logger.LogWarning("Can not reset to unapplied value if ApplyImmediate is FALSE or if the settings has no connection.");
	}

	public bool MatchesAnyGroup(string[] groups)
	{
		//IL_0048: Expected O, but got I
		//IL_0093: Expected O, but got I
		//IL_01f3: Expected I4, but got O
		//IL_00f0: Expected O, but got I4
		//IL_00f9: Expected O, but got I4
		//IL_0122: Expected O, but got I
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_0197: Expected I, but got O
		if (groups != null && groups.Length != 0)
		{
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ rax_v6 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj = (nint)0 + (nint)160;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = default(object);
			if (obj2 != null)
			{
				nint num2 = 0;
				IntPtr intPtr2 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v262 @ rax_v9 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
				object obj3 = (nint)0 + (nint)160;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj5 = default(object);
				object obj4 = obj5;
				if (obj5 == null)
				{
					goto IL_01e5;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rcx_v7+18]");
				if ((nint)0 != 0)
				{
					object obj6 = 0;
					object obj7 = 0;
					object obj9 = default(object);
					List<string>.Enumerator enumerator = default(List<string>.Enumerator);
					string text = default(string);
					object obj10 = default(object);
					while ((nint)obj7 < groups.Length)
					{
						nint num3 = 0;
						IntPtr intPtr3 = num3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v325 @ rax_v18 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
						object obj8 = (nint)0 + (nint)160;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
						if (obj9 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
							nint num4 = 0;
							while (enumerator.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								bool flag = text == groups[obj7];
								bool flag2 = !flag;
								num4 = unchecked((nint)null);
								if (!flag2)
								{
									enumerator.Dispose();
									return true;
								}
							}
							enumerator.Dispose();
							obj7++;
							obj6 = obj10;
							continue;
						}
						goto IL_01e5;
					}
				}
			}
		}
		return false;
		IL_01e5:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public List<string> GetGroups()
	{
		//IL_0024: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)160;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object result = default(object);
		return (List<string>)result;
	}

	public void SetGroups(List<string> groups)
	{
		//IL_001e: Expected O, but got I
		//IL_0034: Expected O, but got I
		//IL_0054: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj2 = (nint)0 + (nint)160;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj3 = (nint)0 + (nint)160;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	protected unsafe bool checkDataType(SettingData.DataType serializedDataType, SettingData.DataType dataType)
	{
		//IL_024d: Expected I4, but got O
		//IL_0064: Expected O, but got Ref
		//IL_00df: Expected O, but got Ref
		//IL_0175: Expected O, but got I
		if (serializedDataType == dataType)
		{
			return true;
		}
		string[] array = new string[7];
		if (array.Length > 0)
		{
			array[0] = "SGSettings: The serialized data type is '";
			IntPtr intPtr = default(IntPtr);
			string text = ((Enum)(&intPtr)).ToString();
			if (array.Length > 1)
			{
				array[1] = text;
				if (array.Length > 2)
				{
					array[2] = "' instead of the expected '";
					object obj = default(object);
					string text2 = ((Enum)(&obj)).ToString();
					if (array.Length > 3)
					{
						array[3] = text2;
						if (array.Length > 4)
						{
							array[4] = "' for settings path '";
							nint num = 0;
							IntPtr intPtr2 = num;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v293 @ rax_v16 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
							object obj2 = (nint)0 + (nint)64;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
							if (array.Length > 5)
							{
								object obj3 = default(object);
								array[5] = (string)obj3;
								if (array.Length > 6)
								{
									array[6] = "'. Please delete any saved settings data and then try again.";
									string message = string.Concat(array);
									Debug.LogError(message);
									return false;
								}
							}
						}
					}
				}
			}
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
	}

	public bool MatchesAnyDataType(IList<SettingData.DataType> dataTypes)
	{
		//IL_0040: Expected I, but got O
		//IL_00dd: Expected I, but got O
		//IL_0078: Expected O, but got I
		//IL_01b6: Expected O, but got I4
		//IL_0148: Expected O, but got I4
		//IL_015e: Expected O, but got I
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		if (dataTypes != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			bool flag = (nint)obj <= 0;
			int num = 0;
			if (!flag)
			{
				object obj8 = default(object);
				do
				{
					nint num2 = (nint)dataTypes;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ r10_v4 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.SettingData+DataType>>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_00b8;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ r10_v4 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.SettingData+DataType>>)+B0]");
					object obj2 = 0;
					int num3 = 0;
					while (true)
					{
						object obj3 = num3 + num3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r8_v10+v187 @ rax_v15*8]");
						if (0 == (nint)typeof(IList<SettingData.DataType>))
						{
							break;
						}
						num3++;
						int num4 = num3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ r10_v4 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.SettingData+DataType>>)+12E]");
						if ((nint)num4 < (nint)0)
						{
							continue;
						}
						goto IL_00b8;
					}
					object obj4 = num3 + num3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r8_v10+8+v242 @ rdx_v12*8]");
					object obj5 = (nint)0 << 4;
					object obj6 = obj5 + 312;
					object obj7 = obj6 + num2;
					goto IL_00c7;
					IL_00c7:
					SettingData.DataType dataType = dataTypes.get_Item(num);
					nint num5 = (nint)this;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v260 @ rdx_v8 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+4C8] (should have been resolved before IL gen)");
					if ((nint)dataType != (nint)obj8)
					{
						num++;
						continue;
					}
					return true;
					IL_00b8:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					goto IL_00c7;
				}
				while (num < (nint)obj);
			}
		}
		return false;
	}

	public abstract SettingData SerializeValueToData();

	public abstract void DeserializeValueFromData(SettingData data);

	public void AddChangeListener(Action<TValue> onChanged)
	{
		//IL_0024: Expected O, but got I
		//IL_018a: Expected O, but got I
		//IL_0141: Expected O, but got I
		//IL_0091: Expected O, but got I
		//IL_00a7: Expected O, but got I
		//IL_00c7: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)384;
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj5 = (nint)0 + (nint)384;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj6 = (nint)0 + (nint)384;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj7 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		}
		nint num5 = 0;
		IntPtr intPtr3 = num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rax_v6 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj8 = (nint)0 + (nint)384;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A3EA0");
		object obj9 = default(object);
		if (obj9 == null)
		{
			nint num7 = 0;
			IntPtr intPtr4 = num7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj10 = (nint)0 + (nint)384;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			nint num8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
		}
	}

	public void RemoveChangeListener(Action<TValue> onChanged)
	{
		//IL_0024: Expected O, but got I
		//IL_006f: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)384;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 != null)
		{
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rax_v6 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj3 = (nint)0 + (nint)384;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A5880");
		}
	}

	public void AddApplyListener(Action<TValue> onApplied)
	{
		//IL_0024: Expected O, but got I
		//IL_018a: Expected O, but got I
		//IL_0141: Expected O, but got I
		//IL_0091: Expected O, but got I
		//IL_00a7: Expected O, but got I
		//IL_00c7: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)352;
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj5 = (nint)0 + (nint)352;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj6 = (nint)0 + (nint)352;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj7 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		}
		nint num5 = 0;
		IntPtr intPtr3 = num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rax_v6 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj8 = (nint)0 + (nint)352;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A3EA0");
		object obj9 = default(object);
		if (obj9 == null)
		{
			nint num7 = 0;
			IntPtr intPtr4 = num7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj10 = (nint)0 + (nint)352;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			nint num8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
		}
	}

	public void RemoveApplyListener(Action<TValue> onApplied)
	{
		//IL_0024: Expected O, but got I
		//IL_006f: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)352;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 != null)
		{
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rax_v6 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj3 = (nint)0 + (nint)352;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A5880");
		}
	}

	protected unsafe void invokeApplyListeners()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0023: Expected O, but got I
		//IL_0039: Expected O, but got I
		//IL_02d2: Expected O, but got Ref
		//IL_0302: Expected O, but got I
		//IL_0068: Expected O, but got I
		//IL_0078: Expected O, but got I
		//IL_0088: Expected O, but got I
		//IL_00a6: Expected O, but got I
		//IL_00c5: Expected O, but got I
		//IL_00d5: Expected O, but got I
		//IL_00e5: Expected O, but got I
		//IL_00f5: Expected O, but got I
		//IL_0103: Expected O, but got Ref
		//IL_013b: Expected O, but got Ref
		//IL_014e: Expected O, but got Ref
		//IL_0339: Expected O, but got I
		//IL_0349: Expected O, but got I
		//IL_0359: Expected O, but got I
		//IL_0367: Expected O, but got Ref
		//IL_02a0: Expected O, but got I
		//IL_02b0: Expected O, but got I
		//IL_0168: Expected O, but got I
		//IL_0178: Expected O, but got I
		//IL_0188: Expected O, but got I
		//IL_0198: Expected O, but got I
		//IL_01a6: Expected O, but got Ref
		//IL_01b4: Expected O, but got Ref
		//IL_01ce: Expected O, but got I
		//IL_020d: Expected O, but got I
		//IL_021d: Expected O, but got I
		//IL_022d: Expected O, but got I
		//IL_023d: Expected O, but got I
		//IL_0253: Expected O, but got I
		//IL_026d: Expected O, but got Ref
		//IL_039e: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ r8_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+30]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v2+FC]");
		object obj5 = default(object);
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj5 = (object)(&obj2);
			_ = 0;
			_ = 0;
			nint num2 = 0;
			IntPtr intPtr = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v9 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj6 = (nint)0 + (nint)352;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj7 = default(object);
			if (obj7 == null)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rax_v12+20]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rcx_v4+C0]");
		object obj10 = 0;
		object obj11 = obj10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v5+80]");
		object obj12 = (nint)0 + (nint)352;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
		object obj13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ rax_v16+20]");
		object obj14 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ rdx_v6+C0]");
		object obj15 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ r8_v4+98]");
		object obj16 = 0;
		object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+30]");
		_ = 0;
		_ = 0;
		object obj18 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
		object obj19 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 120));
		object obj24 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
			object obj20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rax_v19+20]");
			object obj21 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rcx_v9+C0]");
			object obj22 = 0;
			object obj23 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj24 == null)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
			object obj25 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rax_v23+20]");
			object obj26 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ rcx_v12+C0]");
			object obj27 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r8_v7+A8]");
			obj16 = 0;
			object obj28 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
			object obj29 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+80]");
			object obj30 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+80]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18003F110");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
				object obj31 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ rax_v26+20]");
				object obj32 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ rcx_v15+C0]");
				object obj33 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ rax_v27+30]");
				object obj34 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ rcx_v16+28]");
				object obj35 = (nint)0 >> 31;
				bool flag = obj35 != null;
				object obj36 = (object)(&obj2);
				if (!flag)
				{
					obj36 = obj5;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ rbx_v4+28]");
				obj16 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v168 @ rbx_v4+18] (should have been resolved before IL gen)");
			}
		}
		object obj37 = obj19;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ rax_v21+20]");
		object obj38 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rdx_v11+C0]");
		object obj39 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public void Apply()
	{
		//IL_001e: Expected O, but got I
		//IL_0034: Expected O, but got I
		//IL_0054: Expected O, but got I
		//IL_006c: Expected O, but got I4
		//IL_007b: Expected I, but got O
		//IL_00ad: Expected I, but got O
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj2 = (nint)0 + (nint)288;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj3 = (nint)0 + (nint)288;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		nint num2 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v28 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+588] (should have been resolved before IL gen)");
		object obj5 = default(object);
		if (obj5 != null)
		{
			((SettingWithValue<>)(object)this).PushToConnection();
			nint num3 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v48 @ rdx_v13 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+5A8] (should have been resolved before IL gen)");
		}
		triggerOnSettingApplied();
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180902100");
	}

	public unsafe void OnChanged()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0023: Expected O, but got I
		//IL_0039: Expected O, but got I
		//IL_0370: Expected O, but got Ref
		//IL_0392: Expected O, but got I
		//IL_03a2: Expected O, but got I
		//IL_03b2: Expected O, but got I
		//IL_03ca: Expected O, but got I
		//IL_03e0: Expected O, but got I
		//IL_0400: Expected O, but got I
		//IL_0061: Expected O, but got I4
		//IL_0081: Expected O, but got I
		//IL_0091: Expected O, but got I
		//IL_00a1: Expected O, but got I
		//IL_00bf: Expected O, but got I
		//IL_041f: Expected O, but got I
		//IL_042f: Expected O, but got I
		//IL_043f: Expected O, but got I
		//IL_045d: Expected O, but got I
		//IL_0106: Expected O, but got I
		//IL_0116: Expected O, but got I
		//IL_0126: Expected O, but got I
		//IL_0144: Expected O, but got I
		//IL_0163: Expected O, but got I
		//IL_0173: Expected O, but got I
		//IL_0183: Expected O, but got I
		//IL_0193: Expected O, but got I
		//IL_01a1: Expected O, but got Ref
		//IL_01d9: Expected O, but got Ref
		//IL_01ec: Expected O, but got Ref
		//IL_0494: Expected O, but got I
		//IL_04a4: Expected O, but got I
		//IL_04b4: Expected O, but got I
		//IL_04c2: Expected O, but got Ref
		//IL_033e: Expected O, but got I
		//IL_034e: Expected O, but got I
		//IL_0206: Expected O, but got I
		//IL_0216: Expected O, but got I
		//IL_0226: Expected O, but got I
		//IL_0236: Expected O, but got I
		//IL_0244: Expected O, but got Ref
		//IL_0252: Expected O, but got Ref
		//IL_026c: Expected O, but got I
		//IL_02ab: Expected O, but got I
		//IL_02bb: Expected O, but got I
		//IL_02cb: Expected O, but got I
		//IL_02db: Expected O, but got I
		//IL_02f1: Expected O, but got I
		//IL_030b: Expected O, but got Ref
		//IL_04f9: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ r8_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+30]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v2+FC]");
		object obj5 = default(object);
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj5 = (object)(&obj2);
			_ = 0;
			_ = 0;
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rcx_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+F8]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v9+20]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v2+C0]");
			object obj8 = 0;
			object obj9 = obj8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rcx_v3+80]");
			object obj10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rcx_v3+80]");
			object obj11 = (nint)0 + (nint)288;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rcx_v3+80]");
			object obj12 = (nint)0 + (nint)288;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		}
		object obj13 = 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		triggerOnSettingChanged();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
		object obj14 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rax_v16+20]");
		object obj15 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v8+C0]");
		object obj16 = 0;
		object obj17 = obj16;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ rcx_v9+80]");
		object obj18 = --128;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj19 = default(object);
		if (obj19 != null)
		{
			triggerOnSettingApplied();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
		object obj20 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rax_v20+20]");
		object obj21 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rcx_v12+C0]");
		object obj22 = 0;
		object obj23 = obj22;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rcx_v13+80]");
		object obj24 = (nint)0 + (nint)384;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj25 = default(object);
		if (obj25 == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
		object obj26 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v24+20]");
		object obj27 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rcx_v16+C0]");
		object obj28 = 0;
		object obj29 = obj28;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ rcx_v17+80]");
		object obj30 = (nint)0 + (nint)384;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
		object obj31 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ rax_v28+20]");
		object obj32 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rdx_v14+C0]");
		object obj33 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ r8_v5+98]");
		object obj34 = 0;
		object obj35 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+30]");
		_ = 0;
		_ = 0;
		object obj36 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
		object obj37 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 120));
		object obj42 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
			object obj38 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ rax_v31+20]");
			object obj39 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v260 @ rcx_v21+C0]");
			object obj40 = 0;
			object obj41 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj42 == null)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
			object obj43 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v35+20]");
			object obj44 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v266 @ rcx_v24+C0]");
			object obj45 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v267 @ r8_v8+A8]");
			obj34 = 0;
			object obj46 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 112));
			object obj47 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+70]");
			object obj48 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+70]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18003F110");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+78]");
				object obj49 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rax_v38+20]");
				object obj50 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v280 @ rcx_v27+C0]");
				object obj51 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v281 @ rax_v39+30]");
				object obj52 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v282 @ rcx_v28+28]");
				object obj53 = (nint)0 >> 31;
				bool flag = obj53 != null;
				object obj54 = (object)(&obj2);
				if (!flag)
				{
					obj54 = obj5;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ rbx_v5+28]");
				obj34 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v219 @ rbx_v5+18] (should have been resolved before IL gen)");
			}
		}
		object obj55 = obj37;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rax_v33+20]");
		object obj56 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v269 @ rdx_v19+C0]");
		object obj57 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	protected virtual void triggerOnSettingChanged()
	{
		//IL_0024: Expected O, but got I
		//IL_0063: Expected O, but got I
		//IL_0073: Expected O, but got I
		//IL_0083: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)480;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj3 = default(object);
		object obj2 = obj3;
		if (obj3 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rcx_v1+18]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rcx_v1+28]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rcx_v1+40]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v28 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	protected virtual void triggerOnSettingApplied()
	{
		//IL_0024: Expected O, but got I
		//IL_0063: Expected O, but got I
		//IL_0073: Expected O, but got I
		//IL_0083: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)512;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj3 = default(object);
		object obj2 = obj3;
		if (obj3 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rcx_v1+18]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rcx_v1+28]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rcx_v1+40]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v28 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	public void MarkAsChanged()
	{
		//IL_001e: Expected O, but got I
		//IL_0034: Expected O, but got I
		//IL_0054: Expected O, but got I
		//IL_006c: Expected O, but got I4
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj2 = (nint)0 + (nint)288;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj3 = (nint)0 + (nint)288;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj4 = 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public void MarkAsUnchanged()
	{
		//IL_001e: Expected O, but got I
		//IL_0034: Expected O, but got I
		//IL_0054: Expected O, but got I
		//IL_006c: Expected O, but got I4
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj2 = (nint)0 + (nint)288;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj3 = (nint)0 + (nint)288;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public bool HasUnappliedChanges()
	{
		//IL_0024: Expected O, but got I
		//IL_0033: Expected I4, but got O
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)288;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		return (byte)(int)obj2 != 0;
	}

	public void AddPulledFromConnectionListener(Action<TValue> onApply)
	{
		//IL_0024: Expected O, but got I
		//IL_018a: Expected O, but got I
		//IL_0141: Expected O, but got I
		//IL_0091: Expected O, but got I
		//IL_00a7: Expected O, but got I
		//IL_00c7: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)416;
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj5 = (nint)0 + (nint)416;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj6 = (nint)0 + (nint)416;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj7 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		}
		nint num5 = 0;
		IntPtr intPtr3 = num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rax_v6 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj8 = (nint)0 + (nint)416;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A3EA0");
		object obj9 = default(object);
		if (obj9 == null)
		{
			nint num7 = 0;
			IntPtr intPtr4 = num7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj10 = (nint)0 + (nint)416;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			nint num8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
		}
	}

	public void RemovePulledFromConnectionListener(Action<TValue> onApply)
	{
		//IL_0024: Expected O, but got I
		//IL_006f: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)416;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 != null)
		{
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rax_v6 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj3 = (nint)0 + (nint)416;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A5880");
		}
	}

	protected unsafe void invokePulledFromConnectionListeners()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0023: Expected O, but got I
		//IL_0039: Expected O, but got I
		//IL_0346: Expected O, but got Ref
		//IL_0357: Expected I, but got O
		//IL_0068: Expected O, but got I
		//IL_0078: Expected O, but got I
		//IL_0088: Expected O, but got I
		//IL_00a6: Expected O, but got I
		//IL_038e: Expected O, but got I
		//IL_039e: Expected O, but got I
		//IL_03ae: Expected O, but got I
		//IL_00dd: Expected O, but got I
		//IL_00ed: Expected O, but got I
		//IL_00fd: Expected O, but got I
		//IL_011b: Expected O, but got I
		//IL_013a: Expected O, but got I
		//IL_014a: Expected O, but got I
		//IL_015a: Expected O, but got I
		//IL_016a: Expected O, but got I
		//IL_0178: Expected O, but got Ref
		//IL_01b0: Expected O, but got Ref
		//IL_01c3: Expected O, but got Ref
		//IL_03c9: Expected O, but got I
		//IL_03d9: Expected O, but got I
		//IL_03e9: Expected O, but got I
		//IL_03f7: Expected O, but got Ref
		//IL_0315: Expected O, but got I
		//IL_0325: Expected O, but got I
		//IL_01dd: Expected O, but got I
		//IL_01ed: Expected O, but got I
		//IL_01fd: Expected O, but got I
		//IL_020d: Expected O, but got I
		//IL_021b: Expected O, but got Ref
		//IL_0229: Expected O, but got Ref
		//IL_0243: Expected O, but got I
		//IL_0282: Expected O, but got I
		//IL_0292: Expected O, but got I
		//IL_02a2: Expected O, but got I
		//IL_02b2: Expected O, but got I
		//IL_02c8: Expected O, but got I
		//IL_02e2: Expected O, but got Ref
		//IL_042e: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rdx_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+30]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rax_v2+FC]");
		object obj5 = default(object);
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj5 = (object)(&obj2);
			_ = 0;
			_ = 0;
			nint num2 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v52 @ rdx_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+588] (should have been resolved before IL gen)");
			object obj6 = default(object);
			if (obj6 == null)
			{
				goto IL_037e;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+78]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v13+20]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rcx_v4+C0]");
		object obj9 = 0;
		object obj10 = obj9;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v5+80]");
		object obj11 = (nint)0 + (nint)416;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj12 = default(object);
		if (obj12 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+78]");
			object obj13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ rax_v16+20]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v141 @ rcx_v7+C0]");
			object obj15 = 0;
			object obj16 = obj15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rcx_v8+80]");
			object obj17 = (nint)0 + (nint)416;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+78]");
			object obj18 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ rax_v21+20]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rdx_v12+C0]");
			object obj20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v187 @ r8_v2+98]");
			object obj21 = 0;
			object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+30]");
			_ = 0;
			_ = 0;
			object obj23 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			object obj24 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 120));
			object obj29 = default(object);
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+78]");
				object obj25 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ rax_v24+20]");
				object obj26 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v236 @ rcx_v12+C0]");
				object obj27 = 0;
				object obj28 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
				if (obj29 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+78]");
				object obj30 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v241 @ rax_v28+20]");
				object obj31 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rcx_v15+C0]");
				object obj32 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v243 @ r8_v5+A8]");
				obj21 = 0;
				object obj33 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
				object obj34 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+80]");
				object obj35 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+80]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18003F110");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+78]");
					object obj36 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v255 @ rax_v31+20]");
					object obj37 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ rcx_v18+C0]");
					object obj38 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v257 @ rax_v32+30]");
					object obj39 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ rcx_v19+28]");
					object obj40 = (nint)0 >> 31;
					bool flag = obj40 != null;
					object obj41 = (object)(&obj2);
					if (!flag)
					{
						obj41 = obj5;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ rbx_v4+28]");
					obj21 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v192 @ rbx_v4+18] (should have been resolved before IL gen)");
				}
			}
			object obj42 = obj24;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rax_v26+20]");
			object obj43 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ rdx_v17+C0]");
			object obj44 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
		goto IL_037e;
		IL_037e:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+78]");
		object obj45 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rax_v11+20]");
		object obj46 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rcx_v2+C0]");
		object obj47 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180902320");
	}

	public unsafe void AddPulledFromConnectionListener(Action onApply)
	{
		//IL_0120: Expected O, but got I
		//IL_016e: Expected O, but got I
		//IL_0032: Expected O, but got I
		//IL_0048: Expected O, but got I
		//IL_0068: Expected O, but got I
		//IL_00d1: Expected O, but got I
		//IL_00f6: Expected O, but got I4
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)448;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 == null)
		{
			List<Action> list = new List<Action>();
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj4 = (nint)0 + (nint)448;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rcx_v17 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj5 = (nint)0 + (nint)448;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj6 = list;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		}
		nint num3 = 0;
		IntPtr intPtr3 = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ rax_v7 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj7 = (nint)0 + (nint)448;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj8 = default(object);
		if (!((List<Action>)obj8).Contains(onApply))
		{
			nint num4 = 0;
			IntPtr intPtr4 = num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			Action item = (Action)((nint)0 + (nint)448);
			bool flag = ((List<Action>)this).Contains(item);
			((List<Action>)((bool*)(flag ? 1 : 0))->m_value).Add(onApply);
		}
	}

	public void RemovePulledFromConnectionListener(Action onApply)
	{
		//IL_0073: Expected O, but got I
		//IL_0029: Expected O, but got I
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = (nint)0 + (nint)448;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 != null)
		{
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v7 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
			object obj3 = (nint)0 + (nint)448;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj4 = default(object);
			bool flag = ((List<Action>)obj4).Remove(onApply);
		}
	}

	protected void invokeGenericPulledFromConnectionListeners()
	{
		//IL_00df: Expected I, but got O
		//IL_0029: Expected O, but got I
		//IL_0074: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v36 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<TValue>>)+588] (should have been resolved before IL gen)");
		object obj = default(object);
		if (obj == null)
		{
			return;
		}
		nint num2 = 0;
		IntPtr intPtr = num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v7 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj2 = (nint)0 + (nint)448;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj3 = default(object);
		if (obj3 == null)
		{
			return;
		}
		nint num3 = 0;
		IntPtr intPtr2 = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ rax_v10 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj4 = (nint)0 + (nint)448;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<Action>.Enumerator enumerator = default(List<Action>.Enumerator);
		object obj5 = default(object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (obj5 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v170 @ stack_8_v3+18] (should have been resolved before IL gen)");
			}
		}
		enumerator.Dispose();
	}

	public unsafe void RemoveAllListeners()
	{
		//IL_047c: Expected O, but got I
		//IL_011b: Expected O, but got I
		//IL_025a: Expected O, but got I
		//IL_001b: Expected O, but got I
		//IL_003e: Expected O, but got I
		//IL_004e: Expected O, but got I
		//IL_0350: Expected O, but got I
		//IL_0160: Expected O, but got I
		//IL_0183: Expected O, but got I
		//IL_0193: Expected O, but got I
		//IL_0395: Expected O, but got I
		//IL_03b8: Expected O, but got I
		//IL_03c8: Expected O, but got I
		//IL_00e9: Expected O, but got I
		//IL_00f2: Expected O, but got I4
		//IL_0228: Expected O, but got I
		//IL_0231: Expected O, but got I4
		//IL_031e: Expected O, but got I
		//IL_0327: Expected O, but got I4
		//IL_044d: Expected O, but got I
		nint num = 0;
		int value = ((int*)num)->m_value;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v3 (System.Int32)+80]");
		object obj = (nint)0 + (nint)384;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj3 = default(object);
		object obj2 = obj3;
		bool flag = obj3 == null;
		int num2 = 0;
		if (!flag)
		{
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rcx_v25 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+110]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rbx_v1+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rdx_v18+20]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rax_v30+C0]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj7 = default(object);
			if (obj7 == null)
			{
				_ = 0;
				num2 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rbx_v1+18]");
				num2 = 0;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rbx_v1+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rbx_v1+10]");
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rbx_v1+18]");
					Array.Clear((Array)num4, 0, 0);
					object obj8 = 0;
				}
			}
		}
		nint num5 = 0;
		IntPtr intPtr = num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rax_v7 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj9 = (nint)0 + (nint)416;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj11 = default(object);
		object obj10 = obj11;
		if (obj11 != null)
		{
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rcx_v21 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+110]");
			object obj12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rbx_v2+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rdx_v16+20]");
			object obj13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rax_v26+C0]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj15 = default(object);
			if (obj15 == null)
			{
				_ = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rbx_v2+18]");
				num2 = 0;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rbx_v2+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rbx_v2+10]");
					nint num7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rbx_v2+18]");
					Array.Clear((Array)num7, 0, 0);
					object obj8 = 0;
				}
			}
		}
		nint num8 = 0;
		IntPtr intPtr2 = num8;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v175 @ rax_v11 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj16 = (nint)0 + (nint)448;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj18 = default(object);
		object obj17 = obj18;
		if (obj18 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rbx_v3+1C]");
			_ = (nint)0 + (nint)1;
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<Action>())
			{
				_ = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rbx_v3+18]");
				num2 = 0;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rbx_v3+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rbx_v3+10]");
					nint num9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rbx_v3+18]");
					Array.Clear((Array)num9, 0, 0);
					object obj8 = 0;
				}
			}
		}
		nint num10 = 0;
		IntPtr intPtr3 = num10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v243 @ rax_v15 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj19 = (nint)0 + (nint)352;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj21 = default(object);
		object obj20 = obj21;
		if (obj21 == null)
		{
			return;
		}
		nint num11 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v262 @ rcx_v13 (Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>)+110]");
		object obj22 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rbx_v4+1C]");
		_ = (nint)0 + (nint)1;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ rdx_v13+20]");
		object obj23 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v264 @ rax_v19+C0]");
		object obj24 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj25 = default(object);
		if (obj25 == null)
		{
			_ = 0;
			return;
		}
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rbx_v4+18]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rbx_v4+10]");
			nint num12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rbx_v4+18]");
			Array.Clear((Array)num12, 0, 0);
		}
	}

	public abstract object GetValueAsObject();

	public abstract bool HasConnection();

	public abstract bool HasConnectionObject();

	public abstract void PullFromConnection();

	public abstract void PullFromConnection(bool propagateChange);

	public abstract IConnection GetConnectionInterface();

	public virtual void PushToConnection()
	{
		//IL_001e: Expected O, but got I
		//IL_0034: Expected O, but got I
		//IL_0054: Expected O, but got I
		//IL_006c: Expected O, but got I4
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj2 = (nint)0 + (nint)288;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingWithValue`1>>)+80]");
		object obj3 = (nint)0 + (nint)288;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public abstract int GetConnectionOrder();

	public abstract void OnQualityChanged(int qualityLevel);
}
