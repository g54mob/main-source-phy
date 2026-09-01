using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MKey : MapperType
{
	private const string ignoredPrefix = "Ignored=";

	private const string useMessagePrefix = "Use=";

	private const string messagePrefix = "Message=";

	public Action KeyCountChanged;

	private List<KeyCode> _loadKeyCodes = new List<KeyCode>();

	private List<KeyCode> _keyCodes = new List<KeyCode>();

	private List<KeyCode> _defaultKeyCodes = new List<KeyCode>();

	private KeyInputController inputController;

	private bool isMouseKey;

	public bool isEmulator;

	public bool ignored;

	private bool _loadIgnored;

	private bool _defaultIgnored;

	public string[] message = new string[1] { string.Empty };

	private string[] _loadMessage = new string[1] { string.Empty };

	private string[] _defaultMessage = new string[1] { string.Empty };

	public bool useMessage;

	private bool _loadUseMessage;

	private bool _defaultUseMessage;

	private int emulators;

	private bool wasEmulating;

	private bool temp;

	private float fixedTime;

	private float? _variableMapperHeight;

	private float? _variableSelectorHeight;

	public int KeysCount
	{
		get
		{
			return _keyCodes.Count;
		}
	}

	public int LoadKeysCount
	{
		get
		{
			return _loadKeyCodes.Count;
		}
	}

	public bool Ignored
	{
		get
		{
			return ignored;
		}
		set
		{
			ignored = value;
			InvokeKeysChanged();
		}
	}

	public bool Emulating
	{
		get
		{
			return emulators > 0;
		}
	}

	public float? VariableMapperHeight
	{
		get
		{
			return _variableMapperHeight;
		}
		set
		{
			_variableMapperHeight = value;
		}
	}

	public float? VariableSelectorHeight
	{
		get
		{
			return _variableSelectorHeight;
		}
		set
		{
			_variableSelectorHeight = value;
		}
	}

	public float Value
	{
		get
		{
			float result = 0f;
			if (!ignored && !useMessage && !MouseKeyBlocked())
			{
				for (int i = 0; i < _keyCodes.Count; i++)
				{
					if (inputController.IsHeld(_keyCodes[i]))
					{
						result = 1f;
						break;
					}
				}
			}
			return result;
		}
	}

	public bool IsDown
	{
		get
		{
			Debug.LogWarning("IsDown is deprecated, please use IsHeld");
			return IsHeld;
		}
	}

	public bool IsHeld
	{
		get
		{
			bool result = false;
			if (!ignored && !useMessage && !MouseKeyBlocked())
			{
				for (int i = 0; i < _keyCodes.Count; i++)
				{
					if (inputController.IsHeld(_keyCodes[i]))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}
	}

	public bool IsPressed
	{
		get
		{
			bool result = false;
			if (!ignored && !useMessage && !MouseKeyBlocked())
			{
				for (int i = 0; i < _keyCodes.Count; i++)
				{
					if (inputController.IsPressed(_keyCodes[i]))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}
	}

	public bool IsReleased
	{
		get
		{
			bool result = false;
			if (!ignored && Value <= 0f && !MouseKeyBlocked())
			{
				for (int i = 0; i < _keyCodes.Count; i++)
				{
					if (inputController.IsReleased(_keyCodes[i]))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}
	}

	public override bool isDefaultValue
	{
		get
		{
			if (_defaultKeyCodes.Count != _keyCodes.Count)
			{
				return false;
			}
			for (int i = 0; i < _defaultKeyCodes.Count; i++)
			{
				if (_defaultKeyCodes[i] != _keyCodes[i])
				{
					return false;
				}
			}
			return true;
		}
	}

	public event KeysChangeHandler KeysChanged;

	public MKey(int nameLocalisationId, string key, KeyCode defaultKey, bool isEmulator = false)
		: base(nameLocalisationId, key)
	{
		AddKey(defaultKey);
		_defaultKeyCodes.AddRange(_keyCodes);
		_loadKeyCodes.AddRange(_keyCodes);
		_loadIgnored = (_defaultIgnored = false);
		_loadUseMessage = (_defaultUseMessage = false);
		_loadMessage = new string[1] { string.Empty };
		InvokeKeysChanged();
		base.defaultData = Serialize();
		this.isEmulator = isEmulator;
	}

	public MKey(string displayName, string key, KeyCode defaultKey, bool isEmulator = false)
		: base(displayName, key)
	{
		AddKey(defaultKey);
		_defaultKeyCodes.AddRange(_keyCodes);
		_loadKeyCodes.AddRange(_keyCodes);
		_loadIgnored = (_defaultIgnored = false);
		_loadUseMessage = (_defaultUseMessage = false);
		_loadMessage = new string[1] { string.Empty };
		InvokeKeysChanged();
		base.defaultData = Serialize();
		this.isEmulator = isEmulator;
	}

	public static string[] SplitVariable(string s)
	{
		return s.Split(';');
	}

	public static string CombineVariables(string[] a)
	{
		string text = string.Empty;
		for (int i = 0; i < a.Length; i++)
		{
			if (i > 0)
			{
				text += ";";
			}
			text += a[i];
		}
		return text;
	}

	public void UpdateEmulation(bool emulated)
	{
		emulators += (emulated ? 1 : (-1));
	}

	public float EmulationValue()
	{
		return (!Emulating) ? 0f : 1f;
	}

	public bool EmulationPressed()
	{
		return CheckEmulation(true, false);
	}

	public bool EmulationHeld(bool includePressed = false)
	{
		return (!includePressed) ? CheckEmulation(true, true) : Emulating;
	}

	public bool EmulationReleased()
	{
		return CheckEmulation(false, true);
	}

	protected bool CheckEmulation(bool condition, bool lastCondition)
	{
		if (fixedTime != Time.fixedTime)
		{
			wasEmulating = temp;
			fixedTime = Time.fixedTime;
		}
		temp = Emulating;
		if (Emulating == condition && wasEmulating == lastCondition)
		{
			return true;
		}
		return false;
	}

	public bool MouseKeyBlocked()
	{
		return isMouseKey && StatMaster.hudOccluding && (!StatMaster.isMP || StatMaster.activePlayerCount == 1);
	}

	public bool IsChanged()
	{
		if (_keyCodes.Count != _loadKeyCodes.Count || ignored != _loadIgnored)
		{
			return true;
		}
		if (useMessage != _loadUseMessage || message != _loadMessage)
		{
			return true;
		}
		for (int i = 0; i < _keyCodes.Count; i++)
		{
			if (_keyCodes[i] != _loadKeyCodes[i])
			{
				return true;
			}
		}
		return false;
	}

	public bool IsEmpty()
	{
		return _keyCodes.Count == 0 || (_keyCodes.Count == 1 && _keyCodes[0] == KeyCode.None);
	}

	public override void ResetValue()
	{
		_keyCodes.Clear();
		for (int i = 0; i < _loadKeyCodes.Count; i++)
		{
			_keyCodes.Add(_loadKeyCodes[i]);
		}
		ignored = _loadIgnored;
		useMessage = _loadUseMessage;
		message = _loadMessage;
		InvokeKeysChanged();
	}

	public override void ResetDefaults()
	{
		_keyCodes.Clear();
		for (int i = 0; i < _defaultKeyCodes.Count; i++)
		{
			_keyCodes.Add(_defaultKeyCodes[i]);
		}
		ignored = _defaultIgnored;
		useMessage = _defaultUseMessage;
		message = _defaultMessage;
		InvokeKeysChanged();
	}

	public void ResetUseMessage()
	{
		useMessage = _loadUseMessage;
	}

	public bool Compare(MKey other)
	{
		if (other.KeysCount != _keyCodes.Count || other.Ignored != ignored)
		{
			return false;
		}
		if (other.useMessage == useMessage)
		{
			if (message.Length != other.message.Length)
			{
				return false;
			}
			for (int i = 0; i < message.Length; i++)
			{
				if (i < other.message.Length && message[i] != other.message[i])
				{
					return false;
				}
			}
			for (int j = 0; j < _keyCodes.Count; j++)
			{
				if (_keyCodes[j] != other.GetKey(j))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	public bool Contains(MKey other)
	{
		if (other.Ignored != ignored)
		{
			return false;
		}
		if (other.useMessage == useMessage)
		{
			for (int i = 0; i < other.message.Length; i++)
			{
				if (message.Contains(other.message[i]))
				{
					return false;
				}
			}
			for (int j = 0; j < other._keyCodes.Count; j++)
			{
				if (!_keyCodes.Contains(other._keyCodes[j]))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	public bool CompareLoad(MKey other)
	{
		if (other.KeysCount != _loadKeyCodes.Count || other.Ignored != _loadIgnored)
		{
			return false;
		}
		if (other.useMessage == _loadUseMessage)
		{
			if (_loadMessage.Length != other.message.Length)
			{
				return false;
			}
			for (int i = 0; i < _loadMessage.Length; i++)
			{
				if (i < other.message.Length && _loadMessage[i] != other.message[i])
				{
					return false;
				}
			}
			for (int j = 0; j < _loadKeyCodes.Count; j++)
			{
				if (_loadKeyCodes[j] != other.GetKey(j))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	public bool ContainsLoad(MKey other)
	{
		if (other.Ignored != _loadIgnored)
		{
			return false;
		}
		if (other.useMessage != _loadUseMessage || other.message != _loadMessage)
		{
			return false;
		}
		for (int i = 0; i < other._loadKeyCodes.Count; i++)
		{
			if (!_loadKeyCodes.Contains(other._loadKeyCodes[i]))
			{
				return false;
			}
		}
		return true;
	}

	public override void ApplyValue()
	{
		int count = _loadKeyCodes.Count;
		bool flag = _loadUseMessage != useMessage;
		_loadKeyCodes.Clear();
		_loadKeyCodes.AddRange(_keyCodes);
		_loadIgnored = ignored;
		_loadUseMessage = useMessage;
		_loadMessage = message;
		InvokeKeysChanged();
		if ((count != _keyCodes.Count || flag) && KeyCountChanged != null)
		{
			KeyCountChanged();
		}
	}

	public void SetInputController(KeyInputController controller)
	{
		inputController = controller;
	}

	public int AddKey(KeyCode key)
	{
		_keyCodes.Add(key);
		isMouseKey = key == KeyCode.Mouse0 || key == KeyCode.Mouse1;
		return _keyCodes.Count - 1;
	}

	public bool HasKey(KeyCode key)
	{
		return _keyCodes.Contains(key);
	}

	public bool HasLoadKey(KeyCode key)
	{
		return _loadKeyCodes.Contains(key);
	}

	public KeyCode GetKey(int index)
	{
		return (index >= 0 && index < _keyCodes.Count) ? _keyCodes[index] : KeyCode.None;
	}

	public KeyCode GetLoadKey(int index)
	{
		return (index >= 0 && index < _loadKeyCodes.Count) ? _loadKeyCodes[index] : KeyCode.None;
	}

	public int IndexOf(KeyCode key)
	{
		return _keyCodes.IndexOf(key);
	}

	public int LoadIndexOf(KeyCode key)
	{
		return _loadKeyCodes.IndexOf(key);
	}

	public int AddOrReplaceKey(int index, KeyCode newKey)
	{
		int num = -1;
		if (_keyCodes.Contains(newKey))
		{
			num = _keyCodes.IndexOf(newKey);
			if (num == index)
			{
				num = -1;
			}
		}
		int num2 = 0;
		if (index >= _keyCodes.Count)
		{
			num2 = _keyCodes.Count;
			_keyCodes.Add(newKey);
		}
		else
		{
			num2 = index;
			_keyCodes[index] = newKey;
		}
		if (num != -1)
		{
			_keyCodes.RemoveAt(num);
		}
		isMouseKey = newKey == KeyCode.Mouse0 || newKey == KeyCode.Mouse1;
		return num2;
	}

	public void MatchKeys(MKey key)
	{
		_keyCodes.Clear();
		for (int i = 0; i < key.KeysCount; i++)
		{
			KeyCode key2 = key.GetKey(i);
			_keyCodes.Add(key2);
		}
		ignored = key.Ignored;
		useMessage = key.useMessage;
		message = new string[key.message.Length];
		for (int j = 0; j < key.message.Length; j++)
		{
			message[j] = key.message[j];
		}
	}

	public void SetIgnored(bool newIgnored)
	{
		ignored = newIgnored;
	}

	public void RemoveKey(int index)
	{
		if (index < _keyCodes.Count)
		{
			_keyCodes.RemoveAt(index);
		}
	}

	public void RemoveRedundant()
	{
		while (_keyCodes.Contains(KeyCode.None) && _keyCodes.Count > 1)
		{
			_keyCodes.Remove(KeyCode.None);
		}
	}

	public override XData Serialize()
	{
		return Serialize("bmt-" + base.Key);
	}

	public XStringArray Serialize(string keyName)
	{
		int count = _keyCodes.Count;
		string text = CombineVariables(message);
		bool flag = !string.IsNullOrEmpty(text);
		string[] array = new string[count + (ignored ? 1 : 0) + (flag ? 1 : 0) + (useMessage ? 1 : 0)];
		int i;
		for (i = 0; i < count; i++)
		{
			array[i] = KeyCodeConverter.GetKey(_keyCodes[i]);
		}
		if (ignored)
		{
			array[i] = "Ignored=" + ignored;
			i++;
		}
		if (flag)
		{
			array[i] = "Message=" + text;
			i++;
		}
		if (useMessage)
		{
			array[i] = "Use=" + useMessage;
		}
		return new XStringArray(keyName, array);
	}

	public override bool CompareValue(MapperType other)
	{
		MKey mKey = other as MKey;
		return mKey != null && mKey.useMessage == useMessage && ((!useMessage && mKey.ignored == ignored && mKey._keyCodes.SequenceEqual(_keyCodes)) || (useMessage && mKey.message.SequenceEqual(message)));
	}

	public override XData SerializeLoadValue()
	{
		return SerializeLoadValue("bmt-" + base.Key);
	}

	public XStringArray SerializeLoadValue(string keyName)
	{
		int count = _loadKeyCodes.Count;
		string text = CombineVariables(_loadMessage);
		bool flag = !string.IsNullOrEmpty(text);
		string[] array = new string[count + (_loadIgnored ? 1 : 0) + (flag ? 1 : 0) + (_loadUseMessage ? 1 : 0)];
		int i;
		for (i = 0; i < count; i++)
		{
			array[i] = KeyCodeConverter.GetKey(_loadKeyCodes[i]);
		}
		if (_loadIgnored)
		{
			array[i] = "Ignored=" + _loadIgnored;
			i++;
		}
		if (flag)
		{
			array[i] = "Message=" + text;
			i++;
		}
		if (_loadUseMessage)
		{
			array[i] = "Use=" + _loadUseMessage;
		}
		return new XStringArray(keyName, array);
	}

	public override XData SerializeDefault()
	{
		int count = _defaultKeyCodes.Count;
		string text = CombineVariables(_defaultMessage);
		bool flag = !string.IsNullOrEmpty(text);
		string[] array = new string[count + (_defaultIgnored ? 1 : 0) + (flag ? 1 : 0) + (_defaultUseMessage ? 1 : 0)];
		int i;
		for (i = 0; i < count; i++)
		{
			array[i] = KeyCodeConverter.GetKey(_defaultKeyCodes[i]);
		}
		if (_defaultIgnored)
		{
			array[i] = "Ignored=" + _defaultIgnored;
			i++;
		}
		if (flag)
		{
			array[i] = "Message=" + text;
			i++;
		}
		if (_defaultUseMessage)
		{
			array[i] = "Use=" + _defaultUseMessage;
		}
		return new XStringArray("bmt-" + base.Key, array);
	}

	public override string ToString()
	{
		return ToString(false);
	}

	public string ToString(bool load)
	{
		List<KeyCode> list = ((!load) ? _keyCodes : _loadKeyCodes);
		string text = "{";
		for (int i = 0; i < list.Count; i++)
		{
			string text2 = text;
			text = string.Concat(text2, "key", i, "=", list[i], ",");
		}
		text = text + "Ignored=" + ((!load) ? ignored : _loadIgnored);
		text = text + "Message=" + ((!load) ? message : _loadMessage);
		text = text + "Use=" + ((!load) ? useMessage : _loadUseMessage);
		return text + "}";
	}

	public override void DeSerialize(XData raw)
	{
		string[] array = (string[])(XStringArray)raw;
		int count = _loadKeyCodes.Count;
		_loadKeyCodes.Clear();
		_keyCodes.Clear();
		ignored = false;
		message = new string[1] { string.Empty };
		useMessage = false;
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			if (text == null)
			{
				Debug.LogError("null key on deserialize");
				continue;
			}
			KeyCode keyCode;
			if (KeyCodeConverter.GetKey(text, out keyCode))
			{
				_loadKeyCodes.Add(keyCode);
				AddOrReplaceKey(i, keyCode);
				continue;
			}
			int num = text.IndexOf("Ignored=");
			if (num != -1)
			{
				string text2 = text.Substring(num + "Ignored=".Length);
				if (!bool.TryParse(text2, out ignored))
				{
					Debug.LogWarning("Couldn't parse '" + text2 + "' into a bool!");
				}
				continue;
			}
			int num2 = text.IndexOf("Message=");
			if (num2 != -1)
			{
				message = SplitVariable(text.Substring(num2 + "Message=".Length));
				continue;
			}
			int num3 = text.IndexOf("Use=");
			if (num3 != -1)
			{
				string text3 = text.Substring(num3 + "Use=".Length);
				if (!bool.TryParse(text3, out useMessage))
				{
					Debug.LogWarning("Couldn't parse '" + text3 + "' into a bool!");
				}
			}
			else
			{
				Debug.LogWarning("Couldn't parse key '" + text + "'!");
			}
		}
		bool flag = _loadUseMessage != useMessage;
		_loadIgnored = ignored;
		_loadMessage = message;
		_loadUseMessage = useMessage;
		InvokeKeysChanged();
		if (flag)
		{
			if (KeyCountChanged != null)
			{
				KeyCountChanged();
			}
		}
		else if (_keyCodes.Count != count && (count <= 1 || _keyCodes.Count <= 1) && KeyCountChanged != null)
		{
			KeyCountChanged();
		}
	}

	public void InvokeKeysChanged()
	{
		if ((bool)BlockMapper.CurrentInstance)
		{
			BlockMapper.CurrentInstance.FindAllVariables();
		}
		KeysChangeHandler keysChanged = this.KeysChanged;
		if (keysChanged != null)
		{
			keysChanged();
		}
	}
}
