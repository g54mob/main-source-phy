using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

[Serializable]
public class SettingColor : SettingWithValue<Color>, ISettingWithConnection<Color>, ISettingWithValue<Color>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
{
	public const SettingData.DataType DataType = SettingData.DataType.Color;

	private Action<SettingColor> m_OnSettingColorChanged;

	private Action<Color> m_OnValueChanged;

	[NonSerialized]
	public IConnection<Color> Connection;

	private ColorConnectionSO ConnectionObject;

	[NonSerialized]
	protected Color _color;

	protected bool _valueInitialized;

	public event Action<SettingColor> OnSettingColorChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 128;
			Delegate obj2 = this.m_OnSettingColorChanged;
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
			object obj = this + 128;
			Delegate obj2 = this.m_OnSettingColorChanged;
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

	public event Action<Color> OnValueChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 136;
			Delegate obj2 = this.m_OnValueChanged;
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
			object obj = this + 136;
			Delegate obj2 = this.m_OnValueChanged;
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

	public override ConnectionSO GetConnectionSO()
	{
		return ConnectionObject;
	}

	public override void SetConnectionSO(ConnectionSO connectionSO)
	{
		//IL_001f: Expected I, but got O
		//IL_0027: Expected I, but got O
		//IL_0037: Expected O, but got I
		//IL_0073: Expected O, but got I
		//IL_0098: Expected O, but got I4
		//IL_017d: Expected I, but got O
		//IL_0185: Expected I, but got O
		//IL_0195: Expected O, but got I
		//IL_00cd: Expected O, but got I
		//IL_00f2: Expected O, but got I4
		if ((object)connectionSO == null)
		{
			ConnectionObject = null;
			return;
		}
		nint num = (nint)typeof(ColorConnectionSO);
		nint num2 = (nint)connectionSO;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.ColorConnectionSO>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.ColorConnectionSO>)+130]");
		ColorConnectionSO colorConnectionSO;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rax_v14+FFFFFFF8+v45 @ rax_v4*8]");
			bool flag = 0 == (nint)typeof(ColorConnectionSO);
			colorConnectionSO = (ColorConnectionSO)1;
			if (flag)
			{
				goto IL_012b;
			}
		}
		colorConnectionSO = null;
		goto IL_012b;
		IL_012b:
		bool flag2 = (object)colorConnectionSO == null;
		ConnectionSO connectionObject = null;
		if (!flag2)
		{
			connectionObject = connectionSO;
		}
		ColorConnectionSO colorConnectionSO2;
		do
		{
			ConnectionObject = (ColorConnectionSO)connectionObject;
			nint num4 = (nint)typeof(ColorConnectionSO);
			nint num5 = (nint)connectionSO;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.ColorConnectionSO>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.ColorConnectionSO>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ rax_v11+FFFFFFF8+v147 @ rax_v8*8]");
				bool flag3 = 0 == (nint)typeof(ColorConnectionSO);
				colorConnectionSO2 = (ColorConnectionSO)1;
				if (flag3)
				{
					continue;
				}
			}
			colorConnectionSO2 = null;
		}
		while ((object)colorConnectionSO2 != null);
	}

	public unsafe override Color GetValue()
	{
		//IL_000f: Expected F4, but got O
		//IL_000a: Expected native int or pointer, but got O
		Color color = default(Color);
		((Color*)(nint)color)->r = (float)_color;
		return color;
	}

	public override void SetValue(Color color, bool propagateChange = true)
	{
		//IL_0041: Expected O, but got F4
		object obj2 = default(object);
		object obj = obj2 - obj2;
		float num = (float)_color - color.r;
		object obj3 = obj2 - obj2;
		object obj4 = obj2 - obj2;
		object obj5 = obj * obj;
		float num2 = num * num;
		object obj6 = obj3 * obj3;
		float num3 = (float)obj5 + num2;
		object obj7 = obj4 * obj4;
		float num4 = num3 + (float)obj6;
		float num5 = num4 + (float)obj7;
		if (9.9999994E-11f > num5 && _valueInitialized)
		{
			return;
		}
		_valueInitialized = true;
		_color = (Color)color.r;
		if (propagateChange)
		{
			OnChanged();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColor)+20]");
			if ((nint)0 != 0)
			{
				PushToConnection();
			}
		}
	}

	public SettingColor(SettingData data, List<string> groups = null)
		: base(data, groups)
	{
	}

	public unsafe SettingColor(string path, Color value, List<string> groups = null)
	{
		//IL_0022: Expected O, but got Ref
		base._002Ector(path, groups);
		object obj = default(object);
		SetValue((Color)(&obj));
	}

	protected override void triggerOnSettingChanged()
	{
		base.triggerOnSettingChanged();
		Action<SettingColor> onSettingColorChanged = this.m_OnSettingColorChanged;
		if (this.m_OnSettingColorChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v31 @ rcx_v3 (System.Action`1<Kamgam.SettingsGenerator.SettingColor>)+18] (should have been resolved before IL gen)");
		}
		Action<Color> onValueChanged = this.m_OnValueChanged;
		if (this.m_OnValueChanged != null)
		{
			Color value = GetValue();
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v52 @ rdi_v1 (System.Action`1<UnityEngine.Color>)+18] (should have been resolved before IL gen)");
		}
	}

	public unsafe override void ResetToDefault()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		//IL_0034: Expected O, but got Ref
		//IL_0081: Expected I, but got O
		//IL_0091: Expected O, but got I
		//IL_00a1: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColor)+30]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ r9_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingColor>)+4F0]");
		object obj2 = 0;
		object obj3 = default(object);
		SetValue((Color)(&obj3));
		if (HasConnection())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColor)+20]");
			if ((nint)0 != 0)
			{
				nint num2 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingColor>)+5D8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingColor>)+5E0]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v54 @ rax_v5 (should have been resolved before IL gen)");
			}
		}
	}

	public override object GetValueAsObject()
	{
		Color value = GetValue();
		object obj = default(object);
		return (Color)obj;
	}

	public unsafe override void SetValueFromObject(object value, bool propagateChange = true)
	{
		//IL_0063: Expected I, but got O
		//IL_000d: Expected I, but got O
		//IL_0054: Expected O, but got Ref
		nint num = (nint)typeof(Color);
		nint num2 = (nint)value;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rdx_v3 (Il2CppClass<System.Object>)+40]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ r9_v1 (Il2CppClass<UnityEngine.Color>)+40]");
		if (num3 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
			object obj = default(object);
			SetValue((Color)(&obj), propagateChange);
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.Color;
	}

	public override SettingData SerializeValueToData()
	{
		//IL_013c: Expected O, but got I
		//IL_009e: Expected F4, but got O
		//IL_00d2: Expected F4, but got O
		//IL_0106: Expected F4, but got O
		SettingData.DataType type = default(SettingData.DataType);
		SettingData settingData = new SettingData(null, type);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColor)+18]");
		settingData.ID = (string)0;
		settingData.Type = SettingData.DataType.Color;
		Color value = GetValue();
		float[] array = new float[4];
		if (array.Length > 0)
		{
			array[0] = value.r;
			if (array.Length > 1)
			{
				object obj = default(object);
				array[1] = (float)obj;
				if (array.Length > 2)
				{
					array[2] = (float)obj;
					if (array.Length > 3)
					{
						array[3] = (float)obj;
						settingData.FloatValues = array;
						return settingData;
					}
				}
			}
		}
		return (SettingData)(object)new IndexOutOfRangeException();
	}

	public unsafe override void DeserializeValueFromData(SettingData data)
	{
		//IL_0045: Expected O, but got Ref
		if (checkDataType(data.Type, SettingData.DataType.Color))
		{
			object obj = default(object);
			SetValue((Color)(&obj), propagateChange: false);
		}
	}

	protected void extractConnectionFromObject()
	{
		if (Connection == null && ConnectionObject != null && (object)ConnectionObject != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			IConnection<Color> connection = default(IConnection<Color>);
			Connection = connection;
		}
	}

	public override bool HasConnection()
	{
		bool flag = (nint)Connection < 0;
		bool flag2 = Connection == null;
		if (Connection == null)
		{
			if (ConnectionObject != null && (object)ConnectionObject != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				IConnection<Color> connection = default(IConnection<Color>);
				Connection = connection;
			}
			flag = (nint)Connection < 0;
			flag2 = Connection == null;
		}
		bool flag3 = !flag;
		bool flag4 = !flag2;
		return flag4 & flag3;
	}

	public override bool HasConnectionObject()
	{
		return ConnectionObject != null;
	}

	public override void PullFromConnection()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingColor>)+5B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingColor>)+5C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public unsafe override void PullFromConnection(bool propagateChange)
	{
		//IL_0028: Expected O, but got Ref
		if (HasConnection())
		{
			Color color = Connection.Get();
			object obj = default(object);
			SetValue((Color)(&obj), propagateChange: false);
			invokePulledFromConnectionListeners();
		}
	}

	public unsafe override void PushToConnection()
	{
		//IL_0028: Expected O, but got Ref
		base.PushToConnection();
		if (HasConnection())
		{
			Color value = GetValue();
			object obj = default(object);
			Connection.Set((Color)(&obj));
		}
	}

	public override int GetConnectionOrder()
	{
		//IL_0047: Expected I4, but got O
		if (!HasConnection())
		{
			return 0;
		}
		if (Connection != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			int result = default(int);
			return result;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public void SetConnection(IConnection<Color> connection)
	{
		Connection = connection;
		if (connection != null)
		{
			base.InitializeConnection();
		}
	}

	public IConnection<Color> GetConnection()
	{
		return Connection;
	}

	public override IConnection GetConnectionInterface()
	{
		return Connection;
	}

	public override void OnQualityChanged(int qualityLevel)
	{
		if (HasConnection())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
		}
	}
}
