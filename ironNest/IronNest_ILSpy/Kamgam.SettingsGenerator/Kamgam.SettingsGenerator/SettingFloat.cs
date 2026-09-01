using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

[Serializable]
public class SettingFloat : SettingWithValue<float>, ISettingWithConnection<float>, ISettingWithValue<float>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
{
	public const SettingData.DataType DataType = SettingData.DataType.Float;

	private Action<SettingFloat> m_OnSettingFloatChanged;

	private Action<float> m_OnValueChanged;

	[NonSerialized]
	public IConnection<float> Connection;

	private FloatConnectionSO ConnectionObject;

	[NonSerialized]
	protected float _value;

	protected bool _valueInitialized;

	public event Action<SettingFloat> OnSettingFloatChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 112;
			Delegate obj2 = this.m_OnSettingFloatChanged;
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
			object obj = this + 112;
			Delegate obj2 = this.m_OnSettingFloatChanged;
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

	public event Action<float> OnValueChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 120;
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
			object obj = this + 120;
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
		nint num = (nint)typeof(FloatConnectionSO);
		nint num2 = (nint)connectionSO;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.FloatConnectionSO>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.FloatConnectionSO>)+130]");
		FloatConnectionSO floatConnectionSO;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rax_v14+FFFFFFF8+v45 @ rax_v4*8]");
			bool flag = 0 == (nint)typeof(FloatConnectionSO);
			floatConnectionSO = (FloatConnectionSO)1;
			if (flag)
			{
				goto IL_012b;
			}
		}
		floatConnectionSO = null;
		goto IL_012b;
		IL_012b:
		bool flag2 = (object)floatConnectionSO == null;
		ConnectionSO connectionObject = null;
		if (!flag2)
		{
			connectionObject = connectionSO;
		}
		FloatConnectionSO floatConnectionSO2;
		do
		{
			ConnectionObject = (FloatConnectionSO)connectionObject;
			nint num4 = (nint)typeof(FloatConnectionSO);
			nint num5 = (nint)connectionSO;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.FloatConnectionSO>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.FloatConnectionSO>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ rax_v11+FFFFFFF8+v147 @ rax_v8*8]");
				bool flag3 = 0 == (nint)typeof(FloatConnectionSO);
				floatConnectionSO2 = (FloatConnectionSO)1;
				if (flag3)
				{
					continue;
				}
			}
			floatConnectionSO2 = null;
		}
		while ((object)floatConnectionSO2 != null);
	}

	public override float GetValue()
	{
		return _value;
	}

	public override void SetValue(float value, bool propagateChange = true)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A4229Dh\"");
		if (_value == value && _valueInitialized)
		{
			return;
		}
		_value = value;
		_valueInitialized = true;
		if (propagateChange)
		{
			OnChanged();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloat)+20]");
			if ((nint)0 != 0)
			{
				PushToConnection();
			}
		}
	}

	public SettingFloat(SettingData data, List<string> groups = null)
		: base(data, groups)
	{
	}

	public SettingFloat(string path, float value, List<string> groups = null)
	{
		//IL_0018: Expected I, but got O
		//IL_0028: Expected O, but got I
		//IL_0038: Expected O, but got I
		base._002Ector(path, groups);
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+4E8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+4F0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v44 @ rax_v3 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected override void triggerOnSettingChanged()
	{
		base.triggerOnSettingChanged();
		Action<SettingFloat> onSettingFloatChanged = this.m_OnSettingFloatChanged;
		if (this.m_OnSettingFloatChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v31 @ rcx_v3 (System.Action`1<Kamgam.SettingsGenerator.SettingFloat>)+18] (should have been resolved before IL gen)");
		}
		Action<float> onValueChanged = this.m_OnValueChanged;
		if (this.m_OnValueChanged != null)
		{
			float value = GetValue();
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v52 @ rdi_v1 (System.Action`1<System.Single>)+18] (should have been resolved before IL gen)");
		}
	}

	public override void ResetToDefault()
	{
		//IL_0005: Expected I, but got O
		//IL_001c: Expected F4, but got I
		//IL_0069: Expected I, but got O
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloat)+30]");
		SetValue(0f);
		if (HasConnection())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloat)+20]");
			if ((nint)0 != 0)
			{
				nint num2 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v50 @ rdx_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+5D8] (should have been resolved before IL gen)");
			}
		}
	}

	public override object GetValueAsObject()
	{
		float value = GetValue();
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object result = default(object);
		return result;
	}

	public override void SetValueFromObject(object value, bool propagateChange = true)
	{
		//IL_0010: Expected O, but got I
		//IL_001d: Expected I, but got O
		//IL_005b: Expected I, but got O
		//IL_0073: Expected O, but got I
		//IL_0083: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
		object obj = 0;
		nint num = (nint)value;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ r9_v2 (Il2CppClass<System.Object>)+40]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v1+40]");
		if (num2 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
			nint num3 = (nint)this;
			object obj3 = default(object);
			object obj2 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+4E8]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+4F0]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v62 @ rdx_v2 (should have been resolved before IL gen)");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.Float;
	}

	public override SettingData SerializeValueToData()
	{
		//IL_009a: Expected O, but got I
		//IL_0064: Expected F4, but got O
		SettingData.DataType type = default(SettingData.DataType);
		SettingData settingData = new SettingData(null, type);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloat)+18]");
		settingData.ID = (string)0;
		settingData.Type = SettingData.DataType.Float;
		float[] array = new float[1];
		float value = GetValue();
		if (array.Length > 0)
		{
			object obj = default(object);
			array[0] = (float)obj;
			settingData.FloatValues = array;
			return settingData;
		}
		return (SettingData)(object)new IndexOutOfRangeException();
	}

	public override void DeserializeValueFromData(SettingData data)
	{
		if (checkDataType(data.Type, SettingData.DataType.Float))
		{
			float[] floatValues = data.FloatValues;
			SetValue(floatValues[0], propagateChange: false);
		}
	}

	protected void extractConnectionFromObject()
	{
		if (Connection == null && ConnectionObject != null && (object)ConnectionObject != null)
		{
			IConnection<float> connection = ConnectionObject.GetConnection();
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
				IConnection<float> connection = ConnectionObject.GetConnection();
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+5B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+5C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void PullFromConnection(bool propagateChange)
	{
		//IL_001c: Expected I, but got O
		//IL_0054: Expected O, but got I
		//IL_005d: Expected O, but got I4
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		if (!HasConnection())
		{
			return;
		}
		IConnection<float> connection = Connection;
		nint num = (nint)connection;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.IConnection`1<System.Single>>)+12E]");
		if ((nint)0 >= (nint)0)
		{
			goto IL_0094;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.IConnection`1<System.Single>>)+B0]");
		object obj = 0;
		object obj2 = 0;
		while (true)
		{
			object obj3 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r8_v7+v143 @ rax_v15*8]");
			if (0 != (nint)typeof(IConnection<float>))
			{
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.IConnection`1<System.Single>>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_0094;
			}
			break;
		}
		goto IL_00a3;
		IL_0094:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_00a3;
		IL_00a3:
		float num2 = connection.Get();
		float value = default(float);
		SetValue(value, propagateChange);
		invokePulledFromConnectionListeners();
	}

	public override void PushToConnection()
	{
		base.PushToConnection();
		if (HasConnection())
		{
			float value = GetValue();
			float value2 = default(float);
			Connection.Set(value2);
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

	public void SetConnection(IConnection<float> connection)
	{
		Connection = connection;
		if (connection != null)
		{
			base.InitializeConnection();
		}
	}

	public IConnection<float> GetConnection()
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
