using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

[Serializable]
public class SettingKeyCombination : SettingWithValue<KeyCombination>, ISettingWithConnection<KeyCombination>, ISettingWithValue<KeyCombination>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
{
	public const SettingData.DataType DataType = SettingData.DataType.KeyCombination;

	[NonSerialized]
	public IConnection<KeyCombination> Connection;

	private KeyCombinationConnectionSO ConnectionObject;

	[NonSerialized]
	protected KeyCombination _keyCombination;

	protected bool _valueInitialized;

	private Action<SettingKeyCombination> m_OnSettingKeyCombinationChanged;

	private Action<KeyCombination> m_OnValueChanged;

	public event Action<SettingKeyCombination> OnSettingKeyCombinationChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 152;
			Delegate obj2 = this.m_OnSettingKeyCombinationChanged;
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
			object obj = this + 152;
			Delegate obj2 = this.m_OnSettingKeyCombinationChanged;
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

	public event Action<KeyCombination> OnValueChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 160;
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
			object obj = this + 160;
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
		nint num = (nint)typeof(KeyCombinationConnectionSO);
		nint num2 = (nint)connectionSO;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.KeyCombinationConnectionSO>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.KeyCombinationConnectionSO>)+130]");
		KeyCombinationConnectionSO keyCombinationConnectionSO;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rax_v14+FFFFFFF8+v45 @ rax_v4*8]");
			bool flag = 0 == (nint)typeof(KeyCombinationConnectionSO);
			keyCombinationConnectionSO = (KeyCombinationConnectionSO)1;
			if (flag)
			{
				goto IL_012b;
			}
		}
		keyCombinationConnectionSO = null;
		goto IL_012b;
		IL_012b:
		bool flag2 = (object)keyCombinationConnectionSO == null;
		ConnectionSO connectionObject = null;
		if (!flag2)
		{
			connectionObject = connectionSO;
		}
		KeyCombinationConnectionSO keyCombinationConnectionSO2;
		do
		{
			ConnectionObject = (KeyCombinationConnectionSO)connectionObject;
			nint num4 = (nint)typeof(KeyCombinationConnectionSO);
			nint num5 = (nint)connectionSO;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.KeyCombinationConnectionSO>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.KeyCombinationConnectionSO>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ rax_v11+FFFFFFF8+v147 @ rax_v8*8]");
				bool flag3 = 0 == (nint)typeof(KeyCombinationConnectionSO);
				keyCombinationConnectionSO2 = (KeyCombinationConnectionSO)1;
				if (flag3)
				{
					continue;
				}
			}
			keyCombinationConnectionSO2 = null;
		}
		while ((object)keyCombinationConnectionSO2 != null);
	}

	public override KeyCombination GetValue()
	{
		return _keyCombination;
	}

	public override void SetValue(KeyCombination keyCombination, bool propagateChange = true)
	{
		if ((object)_keyCombination == (object)keyCombination)
		{
			object obj = (object)keyCombination >> 32;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombination)+8C]");
			if (0 == (nint)obj && _valueInitialized)
			{
				return;
			}
		}
		_valueInitialized = true;
		_keyCombination = keyCombination;
		if (propagateChange)
		{
			OnChanged();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombination)+20]");
			if ((nint)0 != 0)
			{
				PushToConnection();
			}
		}
	}

	public SettingKeyCombination(SettingData data, List<string> groups = null)
		: base(data, groups)
	{
	}

	public SettingKeyCombination(string path, KeyCombination keyCombination, List<string> groups = null)
	{
		//IL_0022: Expected I, but got O
		//IL_0032: Expected O, but got I
		//IL_0042: Expected O, but got I
		base._002Ector(path, groups);
		base.SetDefault(keyCombination);
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+4E8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+4F0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v50 @ rax_v5 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected override void triggerOnSettingChanged()
	{
		base.triggerOnSettingChanged();
		Action<SettingKeyCombination> onSettingKeyCombinationChanged = this.m_OnSettingKeyCombinationChanged;
		if (this.m_OnSettingKeyCombinationChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v31 @ rcx_v3 (System.Action`1<Kamgam.SettingsGenerator.SettingKeyCombination>)+18] (should have been resolved before IL gen)");
		}
		Action<KeyCombination> onValueChanged = this.m_OnValueChanged;
		if (this.m_OnValueChanged != null)
		{
			KeyCombination value = GetValue();
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v52 @ rdi_v1 (System.Action`1<Kamgam.SettingsGenerator.KeyCombination>)+18] (should have been resolved before IL gen)");
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ r9_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+4F0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombination)+30]");
		SetValue((KeyCombination)0);
		if (HasConnection())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombination)+20]");
			if ((nint)0 != 0)
			{
				nint num2 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+5D8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+5E0]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v52 @ rax_v5 (should have been resolved before IL gen)");
			}
		}
	}

	public override object GetValueAsObject()
	{
		KeyCombination value = GetValue();
		object obj = default(object);
		return (KeyCombination)obj;
	}

	public override void SetValueFromObject(object value, bool propagateChange = true)
	{
		//IL_008b: Expected I, but got O
		//IL_000d: Expected I, but got O
		//IL_004b: Expected I, but got O
		//IL_0063: Expected O, but got I
		//IL_0073: Expected O, but got I
		object obj2 = default(object);
		while (true)
		{
			nint num = (nint)typeof(KeyCombination);
			nint num2 = (nint)value;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rdx_v3 (Il2CppClass<System.Object>)+40]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ r9_v1 (Il2CppClass<Kamgam.SettingsGenerator.KeyCombination>)+40]");
			if (num3 != 0)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
			nint num4 = (nint)this;
			object obj = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+4E8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+4F0]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v80 @ r10_v1 (should have been resolved before IL gen)");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.KeyCombination;
	}

	public override SettingData SerializeValueToData()
	{
		//IL_00cf: Expected O, but got I
		//IL_0065: Expected I4, but got O
		//IL_0099: Expected I4, but got O
		SettingData.DataType type = default(SettingData.DataType);
		SettingData settingData = new SettingData(null, type);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombination)+18]");
		settingData.ID = (string)0;
		settingData.Type = SettingData.DataType.KeyCombination;
		KeyCombination value = GetValue();
		int[] array = new int[2];
		if (array.Length > 0)
		{
			array[0] = (int)value;
			if (array.Length > 1)
			{
				object obj = default(object);
				array[1] = (int)obj;
				settingData.IntValues = array;
				return settingData;
			}
		}
		return (SettingData)(object)new IndexOutOfRangeException();
	}

	public override void DeserializeValueFromData(SettingData data)
	{
		//IL_0061: Expected O, but got I4
		if (checkDataType(data.Type, SettingData.DataType.KeyCombination))
		{
			int[] intValues = data.IntValues;
			SetValue((KeyCombination)intValues[0], propagateChange: false);
		}
	}

	public override bool HasConnection()
	{
		bool flag = (nint)Connection < 0;
		bool flag2 = Connection == null;
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+5B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+5C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void PullFromConnection(bool propagateChange)
	{
		if (HasConnection())
		{
			KeyCombination keyCombination = Connection.Get();
			SetValue(keyCombination, propagateChange);
			invokePulledFromConnectionListeners();
		}
	}

	public override void PushToConnection()
	{
		base.PushToConnection();
		if (HasConnection())
		{
			KeyCombination value = GetValue();
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

	public void SetConnection(IConnection<KeyCombination> connection)
	{
		Connection = connection;
		if (connection != null)
		{
			base.InitializeConnection();
		}
	}

	public IConnection<KeyCombination> GetConnection()
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
