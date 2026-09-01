using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

[Serializable]
public class SettingOption : SettingWithValue<int>, ISettingWithOptions<string>, ISettingWithConnection<int>, ISettingWithValue<int>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
{
	public const SettingData.DataType DataType = SettingData.DataType.Option;

	private Action<SettingOption> m_OnSettingOptionChanged;

	private Action<int> m_OnValueChanged;

	protected List<string> _optionLabels;

	protected bool _overrideConnectionLabels;

	[NonSerialized]
	public IConnectionWithOptions<string> Connection;

	private OptionConnectionSO ConnectionObject;

	[NonSerialized]
	protected int _selectedIndex;

	protected bool _valueInitialized;

	public event Action<SettingOption> OnSettingOptionChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 112;
			Delegate obj2 = this.m_OnSettingOptionChanged;
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
			Delegate obj2 = this.m_OnSettingOptionChanged;
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

	public event Action<int> OnValueChanged
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
		nint num = (nint)typeof(OptionConnectionSO);
		nint num2 = (nint)connectionSO;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.OptionConnectionSO>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.OptionConnectionSO>)+130]");
		OptionConnectionSO optionConnectionSO;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rax_v14+FFFFFFF8+v45 @ rax_v4*8]");
			bool flag = 0 == (nint)typeof(OptionConnectionSO);
			optionConnectionSO = (OptionConnectionSO)1;
			if (flag)
			{
				goto IL_012b;
			}
		}
		optionConnectionSO = null;
		goto IL_012b;
		IL_012b:
		bool flag2 = (object)optionConnectionSO == null;
		ConnectionSO connectionObject = null;
		if (!flag2)
		{
			connectionObject = connectionSO;
		}
		OptionConnectionSO optionConnectionSO2;
		do
		{
			ConnectionObject = (OptionConnectionSO)connectionObject;
			nint num4 = (nint)typeof(OptionConnectionSO);
			nint num5 = (nint)connectionSO;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.OptionConnectionSO>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.OptionConnectionSO>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ rax_v11+FFFFFFF8+v147 @ rax_v8*8]");
				bool flag3 = 0 == (nint)typeof(OptionConnectionSO);
				optionConnectionSO2 = (OptionConnectionSO)1;
				if (flag3)
				{
					continue;
				}
			}
			optionConnectionSO2 = null;
		}
		while ((object)optionConnectionSO2 != null);
	}

	public override int GetValue()
	{
		return _selectedIndex;
	}

	public override void SetValue(int selectedIndex, bool propagateChange = true)
	{
		if (_selectedIndex == selectedIndex && _valueInitialized)
		{
			return;
		}
		_valueInitialized = true;
		_selectedIndex = selectedIndex;
		if (propagateChange)
		{
			OnChanged();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingOption)+20]");
			if ((nint)0 != 0)
			{
				PushToConnection();
			}
		}
	}

	public SettingOption(SettingData data, List<string> groups = null, List<string> optionNames = null)
		: base(data, groups)
	{
		_optionLabels = optionNames;
	}

	public SettingOption(string path, int selectedIndex, List<string> groups = null, List<string> optionNames = null)
		: base(path, groups)
	{
		SetValue(selectedIndex);
		List<string> optionLabels = default(List<string>);
		_optionLabels = optionLabels;
	}

	protected override void triggerOnSettingChanged()
	{
		base.triggerOnSettingChanged();
		Action<SettingOption> onSettingOptionChanged = this.m_OnSettingOptionChanged;
		if (this.m_OnSettingOptionChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v31 @ rcx_v3 (System.Action`1<Kamgam.SettingsGenerator.SettingOption>)+18] (should have been resolved before IL gen)");
		}
		Action<int> onValueChanged = this.m_OnValueChanged;
		if (this.m_OnValueChanged != null)
		{
			int value = GetValue();
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v52 @ rdi_v1 (System.Action`1<System.Int32>)+18] (should have been resolved before IL gen)");
		}
	}

	public override void OnAfterDeserialize()
	{
		base.OnAfterDeserialize();
	}

	public void SetOverrideConnectionLabels(bool overrideLabels)
	{
		_overrideConnectionLabels = overrideLabels;
		if (HasConnection())
		{
			if (!_overrideConnectionLabels)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				List<string> optionLabels = default(List<string>);
				_optionLabels = optionLabels;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			}
			invokePulledFromConnectionListeners();
		}
	}

	protected void initLabelOverridesAfterNewConnection()
	{
		if (Connection != null && _overrideConnectionLabels)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
	}

	public bool GetOverrideConnectionLabels()
	{
		return _overrideConnectionLabels;
	}

	public override void ResetToDefault()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0079: Expected I, but got O
		//IL_0089: Expected O, but got I
		//IL_0099: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ r9_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+4F0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingOption)+30]");
		SetValue(0);
		if (HasConnection())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingOption)+20]");
			if ((nint)0 != 0)
			{
				nint num2 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+5D8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+5E0]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v52 @ rax_v5 (should have been resolved before IL gen)");
			}
		}
	}

	public bool HasOptions()
	{
		//IL_00fd: Expected I4, but got O
		bool flag = HasConnection();
		if (!flag)
		{
			if (_optionLabels == null)
			{
				return flag;
			}
			List<string> optionLabels = _optionLabels;
			int num = optionLabels._size ^ optionLabels._size;
			int num2 = optionLabels._size & num;
			bool flag2 = num2 < 0;
			bool flag3 = optionLabels._size < 0;
			bool flag4 = optionLabels._size == 0;
			bool flag5 = flag3 == flag2;
			bool flag6 = !flag4;
			return flag6 & flag5;
		}
		if (Connection != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool result = default(bool);
			return result;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public List<string> GetOptionLabels()
	{
		if (!HasConnection())
		{
			return _optionLabels;
		}
		if (Connection != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<string> result = default(List<string>);
			return result;
		}
		return (List<string>)(object)new NullReferenceException();
	}

	public void SetOptionLabels(List<string> options)
	{
		if (options != _optionLabels)
		{
			ClearOptionLabels();
		}
		if (options == null)
		{
			return;
		}
		AddOptionLabels(options);
		_overrideConnectionLabels = _overrideConnectionLabels;
		if (HasConnection())
		{
			if (!_overrideConnectionLabels)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				List<string> optionLabels = default(List<string>);
				_optionLabels = optionLabels;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			}
			invokePulledFromConnectionListeners();
		}
	}

	public void ClearOptionLabels()
	{
		if (_optionLabels == null)
		{
			return;
		}
		List<string> optionLabels = _optionLabels;
		int version = optionLabels._version + 1;
		optionLabels._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			optionLabels._size = 0;
			return;
		}
		optionLabels._size = 0;
		if (optionLabels._size > 0)
		{
			Array.Clear(optionLabels._items, 0, optionLabels._size);
		}
	}

	public void AddOptionLabels(IEnumerable<string> optionsToAdd)
	{
		if (_optionLabels == null)
		{
			List<string> optionLabels = new List<string>();
			_optionLabels = optionLabels;
		}
		_optionLabels.AddRange(optionsToAdd);
	}

	public override void SetValueFromObject(object value, bool propagateChange = true)
	{
		//IL_0010: Expected O, but got I
		//IL_001d: Expected I, but got O
		//IL_005b: Expected I, but got O
		//IL_0073: Expected O, but got I
		//IL_0083: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+4E8]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+4F0]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v62 @ r10_v2 (should have been resolved before IL gen)");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public override object GetValueAsObject()
	{
		int value = GetValue();
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object result = default(object);
		return result;
	}

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.Option;
	}

	public override SettingData SerializeValueToData()
	{
		//IL_009a: Expected O, but got I
		SettingData.DataType type = default(SettingData.DataType);
		SettingData settingData = new SettingData(null, type);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingOption)+18]");
		settingData.ID = (string)0;
		settingData.Type = SettingData.DataType.Option;
		int[] array = new int[1];
		int value = GetValue();
		if (array.Length > 0)
		{
			array[0] = value;
			settingData.IntValues = array;
			return settingData;
		}
		return (SettingData)(object)new IndexOutOfRangeException();
	}

	public override void DeserializeValueFromData(SettingData data)
	{
		if (checkDataType(data.Type, SettingData.DataType.Option))
		{
			int[] intValues = data.IntValues;
			SetValue(intValues[0], propagateChange: false);
		}
	}

	protected void extractConnectionFromObject()
	{
		if (Connection == null && ConnectionObject != null && (object)ConnectionObject != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			IConnectionWithOptions<string> connection = default(IConnectionWithOptions<string>);
			Connection = connection;
			if (Connection != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 100 Invalid \"Jump target not found in method: 0x180A45010\"");
			}
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
				IConnectionWithOptions<string> connection = default(IConnectionWithOptions<string>);
				Connection = connection;
				flag = (nint)Connection < 0;
				flag2 = Connection == null;
				if (flag2)
				{
					goto IL_0111;
				}
				initLabelOverridesAfterNewConnection();
			}
			flag = (nint)Connection < 0;
			flag2 = Connection == null;
		}
		goto IL_0111;
		IL_0111:
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+5B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+5C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void PullFromConnection(bool propagateChange)
	{
		if (HasConnection())
		{
			ClearOptionLabels();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			IEnumerable<string> enumerable = default(IEnumerable<string>);
			if (enumerable != null)
			{
				AddOptionLabels(enumerable);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			int selectedIndex = default(int);
			SetValue(selectedIndex, propagateChange);
			invokePulledFromConnectionListeners();
		}
	}

	public override void PushToConnection()
	{
		base.PushToConnection();
		if (HasConnection())
		{
			int value = GetValue();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
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

	public void SetConnection(IConnectionWithOptions<string> connection)
	{
		Connection = connection;
		if (connection != null)
		{
			base.InitializeConnection();
			if (Connection != null && _overrideConnectionLabels)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			}
		}
	}

	public void SetConnection(IConnection<int> connection)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("IConnectionWithOptions<string> required but IConnection<int> given.");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public IConnection<int> GetConnection()
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
