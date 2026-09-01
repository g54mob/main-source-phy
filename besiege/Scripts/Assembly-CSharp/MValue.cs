using UnityEngine;

public sealed class MValue : MapperType
{
	private float _value;

	private float _loadValue;

	private float _defaultValue;

	[HideInInspector]
	public bool clampValue;

	public float Value
	{
		get
		{
			return _value;
		}
		set
		{
			if (_value != value)
			{
				_value = value;
				InvokeValueChanged(value);
			}
		}
	}

	public float Min { get; private set; }

	public float Max { get; private set; }

	public override bool isDefaultValue
	{
		get
		{
			return _value == _defaultValue;
		}
	}

	public event ValueChangeHandler ValueChanged;

	public MValue(int nameLocalisationId, string key, float value)
		: base(nameLocalisationId, key)
	{
		_value = (_loadValue = (_defaultValue = value));
		InvokeValueChanged(value);
		base.defaultData = Serialize();
	}

	public MValue(string displayName, string key, float value)
		: base(displayName, key)
	{
		_value = (_loadValue = (_defaultValue = value));
		InvokeValueChanged(value);
		base.defaultData = Serialize();
	}

	public MValue(int nameLocalisationId, string key, float value, float min, float max)
		: base(nameLocalisationId, key)
	{
		if (min > max)
		{
			min = max - float.Epsilon;
			Debug.LogWarning("Min is greater or equal to Max.");
		}
		Min = min;
		Max = max;
		clampValue = true;
		_value = (_loadValue = (_defaultValue = value));
		InvokeValueChanged(value);
		base.defaultData = Serialize();
	}

	public MValue(string displayName, string key, float value, float min, float max)
		: base(displayName, key)
	{
		if (min > max)
		{
			min = max - float.Epsilon;
			Debug.LogWarning("Min is greater or equal to Max.");
		}
		Min = min;
		Max = max;
		clampValue = true;
		_value = (_loadValue = (_defaultValue = value));
		InvokeValueChanged(value);
		base.defaultData = Serialize();
	}

	public override void ResetDefaults()
	{
		Value = _defaultValue;
	}

	public override void ApplyValue()
	{
		_loadValue = _value;
		InvokeValueChanged(_value);
	}

	public void SetValue(float newValue)
	{
		_value = newValue;
	}

	public override void ResetValue()
	{
		Value = _loadValue;
	}

	public override XData Serialize()
	{
		return new XSingle("bmt-" + base.Key, Value);
	}

	public override XData SerializeLoadValue()
	{
		return new XSingle("bmt-" + base.Key, _loadValue);
	}

	public override XData SerializeDefault()
	{
		return new XSingle("bmt-" + base.Key, _defaultValue);
	}

	public override bool CompareValue(MapperType other)
	{
		MValue mValue = other as MValue;
		return mValue != null && mValue.Value == Value;
	}

	public override void DeSerialize(XData raw)
	{
		_value = (_loadValue = (float)(XSingle)raw);
		InvokeValueChanged(_value);
	}

	private void InvokeValueChanged(float value)
	{
		ValueChangeHandler valueChanged = this.ValueChanged;
		if (valueChanged != null)
		{
			valueChanged(value);
		}
	}
}
