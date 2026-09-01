using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

[Serializable]
public class SettingString : SettingWithValue<string>, ISettingWithConnection<string>, ISettingWithValue<string>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
{
	public const SettingData.DataType DataType = SettingData.DataType.String;

	private Action<SettingString> m_OnSettingStringChanged;

	private Action<string> m_OnValueChanged;

	[NonSerialized]
	public IConnection<string> Connection;

	private StringConnectionSO ConnectionObject;

	[NonSerialized]
	protected string _value;

	protected bool _valueInitialized;

	public event Action<SettingString> OnSettingStringChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 120;
			Delegate obj2 = this.m_OnSettingStringChanged;
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
			Delegate obj2 = this.m_OnSettingStringChanged;
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

	public event Action<string> OnValueChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 128;
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
			object obj = this + 128;
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
		nint num = (nint)typeof(StringConnectionSO);
		nint num2 = (nint)connectionSO;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.StringConnectionSO>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.StringConnectionSO>)+130]");
		StringConnectionSO stringConnectionSO;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rax_v14+FFFFFFF8+v45 @ rax_v4*8]");
			bool flag = 0 == (nint)typeof(StringConnectionSO);
			stringConnectionSO = (StringConnectionSO)1;
			if (flag)
			{
				goto IL_012b;
			}
		}
		stringConnectionSO = null;
		goto IL_012b;
		IL_012b:
		bool flag2 = (object)stringConnectionSO == null;
		ConnectionSO connectionObject = null;
		if (!flag2)
		{
			connectionObject = connectionSO;
		}
		StringConnectionSO stringConnectionSO2;
		do
		{
			ConnectionObject = (StringConnectionSO)connectionObject;
			nint num4 = (nint)typeof(StringConnectionSO);
			nint num5 = (nint)connectionSO;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.StringConnectionSO>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.StringConnectionSO>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ rax_v11+FFFFFFF8+v147 @ rax_v8*8]");
				bool flag3 = 0 == (nint)typeof(StringConnectionSO);
				stringConnectionSO2 = (StringConnectionSO)1;
				if (flag3)
				{
					continue;
				}
			}
			stringConnectionSO2 = null;
		}
		while ((object)stringConnectionSO2 != null);
	}

	public override string GetValue()
	{
		return _value;
	}

	public override void SetValue(string value, bool propagateChange = true)
	{
		if (_value == value && _valueInitialized)
		{
			return;
		}
		_valueInitialized = true;
		_value = value;
		if (propagateChange)
		{
			OnChanged();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingString)+20]");
			if ((nint)0 != 0)
			{
				PushToConnection();
			}
		}
	}

	public SettingString(SettingData data, List<string> groups = null)
		: base(data, groups)
	{
	}

	public SettingString(string path, string value, List<string> groups = null)
	{
		//IL_0018: Expected I, but got O
		//IL_0028: Expected O, but got I
		//IL_0038: Expected O, but got I
		base._002Ector(path, groups);
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+4E8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+4F0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v44 @ rax_v3 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected override void triggerOnSettingChanged()
	{
		base.triggerOnSettingChanged();
		Action<SettingString> onSettingStringChanged = this.m_OnSettingStringChanged;
		if (this.m_OnSettingStringChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v31 @ rcx_v3 (System.Action`1<Kamgam.SettingsGenerator.SettingString>)+18] (should have been resolved before IL gen)");
		}
		Action<string> onValueChanged = this.m_OnValueChanged;
		if (this.m_OnValueChanged != null)
		{
			string value = GetValue();
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v52 @ rdi_v1 (System.Action`1<System.String>)+18] (should have been resolved before IL gen)");
		}
	}

	public override void ResetToDefault()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_002c: Expected O, but got I
		//IL_0079: Expected I, but got O
		//IL_0089: Expected O, but got I
		//IL_0099: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ r9_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+4F0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingString)+30]");
		SetValue((string)0);
		if (HasConnection())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingString)+20]");
			if ((nint)0 != 0)
			{
				nint num2 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+5D8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+5E0]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v52 @ rax_v5 (should have been resolved before IL gen)");
			}
		}
	}

	public override object GetValueAsObject()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+4D8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+4E0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void SetValueFromObject(object value, bool propagateChange = true)
	{
		//IL_0063: Expected I, but got O
		//IL_0073: Expected O, but got I
		//IL_0083: Expected O, but got I
		bool flag = value == null;
		object obj = value;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			bool flag2 = value != null;
			obj = null;
			if (!flag2)
			{
				obj = value;
			}
			if (obj == null)
			{
				goto IL_008d;
			}
		}
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+4E8]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+4F0]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v51 @ rax_v2 (should have been resolved before IL gen)");
		goto IL_008d;
		IL_008d:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.String;
	}

	public override SettingData SerializeValueToData()
	{
		//IL_009f: Expected O, but got I
		SettingData.DataType type = default(SettingData.DataType);
		SettingData settingData = new SettingData(null, type);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingString)+18]");
		settingData.ID = (string)0;
		settingData.Type = SettingData.DataType.String;
		string[] array = new string[1];
		string value = GetValue();
		if (array.Length > 0)
		{
			array[0] = value;
			settingData.StringValues = array;
			return settingData;
		}
		return (SettingData)(object)new IndexOutOfRangeException();
	}

	public override void DeserializeValueFromData(SettingData data)
	{
		if (checkDataType(data.Type, SettingData.DataType.String))
		{
			string[] stringValues = data.StringValues;
			string text = stringValues[0];
			if (text._stringLength > 65000)
			{
				Debug.LogError("SG SettingString: String is too long and will be truncated.");
				string[] stringValues2 = data.StringValues;
				string text2 = stringValues2[0].Substring(0, 65000);
				stringValues2[0] = text2;
			}
			string[] stringValues3 = data.StringValues;
			SetValue(stringValues3[0], propagateChange: false);
		}
	}

	protected void extractConnectionFromObject()
	{
		if (Connection == null && ConnectionObject != null && (object)ConnectionObject != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			IConnection<string> connection = default(IConnection<string>);
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
				IConnection<string> connection = default(IConnection<string>);
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+5B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+5C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void PullFromConnection(bool propagateChange)
	{
		if (HasConnection())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			string value = default(string);
			SetValue(value, propagateChange);
			invokePulledFromConnectionListeners();
		}
	}

	public override void PushToConnection()
	{
		base.PushToConnection();
		if (HasConnection())
		{
			string value = GetValue();
			Connection.Set(value);
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

	public void SetConnection(IConnection<string> connection)
	{
		Connection = connection;
		if (connection != null)
		{
			base.InitializeConnection();
		}
	}

	public IConnection<string> GetConnection()
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
