using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

[Serializable]
public class SettingColorOption : SettingWithValue<int>, ISettingWithOptions<Color>, ISettingWithConnection<int>, ISettingWithValue<int>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
{
	public const SettingData.DataType DataType = SettingData.DataType.ColorOption;

	private Action<SettingColorOption> m_OnSettingColorOptionChanged;

	private Action<int> m_OnValueChanged;

	protected List<Color> _options;

	protected bool _overrideConnectionLabels;

	[NonSerialized]
	public IConnectionWithOptions<Color> Connection;

	private ColorOptionConnectionSO ConnectionObject;

	[NonSerialized]
	protected int _selectedIndex;

	protected bool _valueInitialized;

	public event Action<SettingColorOption> OnSettingColorOptionChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 112;
			Delegate obj2 = this.m_OnSettingColorOptionChanged;
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
			Delegate obj2 = this.m_OnSettingColorOptionChanged;
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
		nint num = (nint)typeof(ColorOptionConnectionSO);
		nint num2 = (nint)connectionSO;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.ColorOptionConnectionSO>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.ColorOptionConnectionSO>)+130]");
		ColorOptionConnectionSO colorOptionConnectionSO;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r10_v2 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rax_v14+FFFFFFF8+v45 @ rax_v4*8]");
			bool flag = 0 == (nint)typeof(ColorOptionConnectionSO);
			colorOptionConnectionSO = (ColorOptionConnectionSO)1;
			if (flag)
			{
				goto IL_012b;
			}
		}
		colorOptionConnectionSO = null;
		goto IL_012b;
		IL_012b:
		bool flag2 = (object)colorOptionConnectionSO == null;
		ConnectionSO connectionObject = null;
		if (!flag2)
		{
			connectionObject = connectionSO;
		}
		ColorOptionConnectionSO colorOptionConnectionSO2;
		do
		{
			ConnectionObject = (ColorOptionConnectionSO)connectionObject;
			nint num4 = (nint)typeof(ColorOptionConnectionSO);
			nint num5 = (nint)connectionSO;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.ColorOptionConnectionSO>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.ColorOptionConnectionSO>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.ConnectionSO>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ rax_v11+FFFFFFF8+v147 @ rax_v8*8]");
				bool flag3 = 0 == (nint)typeof(ColorOptionConnectionSO);
				colorOptionConnectionSO2 = (ColorOptionConnectionSO)1;
				if (flag3)
				{
					continue;
				}
			}
			colorOptionConnectionSO2 = null;
		}
		while ((object)colorOptionConnectionSO2 != null);
	}

	public override int GetValue()
	{
		return _selectedIndex;
	}

	public unsafe Color GetColorValue()
	{
		//IL_0010: Expected F4, but got I
		//IL_00ef: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206D80]");
		float r = 0f;
		int value = GetValue();
		List<Color> options = default(List<Color>);
		if (!HasConnection())
		{
			options = _options;
		}
		else
		{
			if (Connection == null)
			{
				return (Color)new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		}
		if (value >= 0 && options != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rax_v6 (System.Collections.Generic.List`1<UnityEngine.Color>)+18]");
			if ((nint)value < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				float num = default(float);
				r = num;
			}
		}
		Color color = default(Color);
		((Color*)(nint)color)->r = r;
		return color;
	}

	public unsafe Color GetColorValue(Color defaultColor)
	{
		//IL_00f1: Expected native int or pointer, but got O
		int value = GetValue();
		List<Color> options = default(List<Color>);
		if (!HasConnection())
		{
			options = _options;
		}
		else
		{
			if (Connection == null)
			{
				return (Color)new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		}
		float r;
		if (value >= 0 && options != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rax_v6 (System.Collections.Generic.List`1<UnityEngine.Color>)+18]");
			if ((nint)value < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				float num = default(float);
				r = num;
				goto IL_00e9;
			}
		}
		r = defaultColor.r;
		goto IL_00e9;
		IL_00e9:
		Color color = default(Color);
		((Color*)(nint)color)->r = r;
		return color;
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorOption)+20]");
			if ((nint)0 != 0)
			{
				PushToConnection();
			}
		}
	}

	public SettingColorOption(SettingData data, List<string> groups = null, List<Color> options = null)
		: base(data, groups)
	{
		_options = options;
	}

	public SettingColorOption(string path, int selectedIndex, List<string> groups = null, List<Color> options = null)
		: base(path, groups)
	{
		SetValue(selectedIndex);
		List<Color> options2 = default(List<Color>);
		_options = options2;
	}

	protected override void triggerOnSettingChanged()
	{
		base.triggerOnSettingChanged();
		Action<SettingColorOption> onSettingColorOptionChanged = this.m_OnSettingColorOptionChanged;
		if (this.m_OnSettingColorOptionChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v31 @ rcx_v3 (System.Action`1<Kamgam.SettingsGenerator.SettingColorOption>)+18] (should have been resolved before IL gen)");
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
				List<Color> options = default(List<Color>);
				_options = options;
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ r9_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+4F0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorOption)+30]");
		SetValue(0);
		if (HasConnection())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorOption)+20]");
			if ((nint)0 != 0)
			{
				nint num2 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+5D8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+5E0]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v52 @ rax_v5 (should have been resolved before IL gen)");
			}
		}
	}

	public void SetOptionLabels(List<Color> options)
	{
		ClearOptions();
		if (options == null)
		{
			return;
		}
		AddOptions(options);
		_overrideConnectionLabels = _overrideConnectionLabels;
		if (HasConnection())
		{
			if (!_overrideConnectionLabels)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				List<Color> options2 = default(List<Color>);
				_options = options2;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			}
			invokePulledFromConnectionListeners();
		}
	}

	public bool HasOptions()
	{
		//IL_010c: Expected I4, but got O
		//IL_0053: Expected O, but got I
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		bool flag = HasConnection();
		if (!flag)
		{
			if (_options == null)
			{
				return flag;
			}
			List<Color> options = _options;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v6 (System.Collections.Generic.List`1<UnityEngine.Color>)+18]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v6 (System.Collections.Generic.List`1<UnityEngine.Color>)+18]");
			object obj = num ^ 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v6 (System.Collections.Generic.List`1<UnityEngine.Color>)+18]");
			object obj2 = 0 & obj;
			bool flag2 = (nint)obj2 < 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v6 (System.Collections.Generic.List`1<UnityEngine.Color>)+18]");
			bool flag3 = (nint)0 < (nint)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v6 (System.Collections.Generic.List`1<UnityEngine.Color>)+18]");
			bool flag4 = (nint)0 == 0;
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

	public List<Color> GetOptionLabels()
	{
		if (!HasConnection())
		{
			return _options;
		}
		if (Connection != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			List<Color> result = default(List<Color>);
			return result;
		}
		return (List<Color>)(object)new NullReferenceException();
	}

	public void ClearOptions()
	{
		//IL_00a3: Expected O, but got I
		if (_options == null)
		{
			return;
		}
		List<Color> options = _options;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rbx_v4 (System.Collections.Generic.List`1<UnityEngine.Color>)+1C]");
		_ = (nint)0 + (nint)1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			_ = 0;
			return;
		}
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rbx_v4 (System.Collections.Generic.List`1<UnityEngine.Color>)+18]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rbx_v4 (System.Collections.Generic.List`1<UnityEngine.Color>)+10]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rbx_v4 (System.Collections.Generic.List`1<UnityEngine.Color>)+18]");
			Array.Clear((Array)num, 0, 0);
		}
	}

	public void AddOptions(IEnumerable<Color> optionsToAdd)
	{
		if (_options == null)
		{
			List<Color> options = new List<Color>();
			_options = options;
		}
		_options.AddRange(optionsToAdd);
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+4E8]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r9_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+4F0]");
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
		return SettingData.DataType.ColorOption;
	}

	public override SettingData SerializeValueToData()
	{
		//IL_009a: Expected O, but got I
		SettingData.DataType type = default(SettingData.DataType);
		SettingData settingData = new SettingData(null, type);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorOption)+18]");
		settingData.ID = (string)0;
		settingData.Type = SettingData.DataType.ColorOption;
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
		if (checkDataType(data.Type, SettingData.DataType.ColorOption))
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
			IConnectionWithOptions<Color> connection = default(IConnectionWithOptions<Color>);
			Connection = connection;
			if (Connection != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 100 Invalid \"Jump target not found in method: 0x180A40160\"");
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
				IConnectionWithOptions<Color> connection = default(IConnectionWithOptions<Color>);
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+5B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+5C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void PullFromConnection(bool propagateChange)
	{
		if (HasConnection())
		{
			ClearOptions();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			IEnumerable<Color> enumerable = default(IEnumerable<Color>);
			if (enumerable != null)
			{
				AddOptions(enumerable);
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

	public void SetConnection(IConnectionWithOptions<Color> connection)
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

	public IConnection<int> GetConnection()
	{
		return Connection;
	}

	public override IConnection GetConnectionInterface()
	{
		return Connection;
	}

	public void SetConnection(IConnection<int> connection)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("IConnectionWithOptions<Color> required but IConnection<int> given.");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public override void OnQualityChanged(int qualityLevel)
	{
		if (HasConnection())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
		}
	}
}
