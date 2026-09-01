using UnityEngine;

public sealed class MSlider : MapperType
{
	public Held HeldDown;

	public Released ReleasedButton;

	private float _value;

	private float _loadValue;

	private float _defaultValue;

	public bool maxInfinity;

	public bool logScaling;

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
				InvokeValueChanged(_value);
			}
		}
	}

	public bool IsWithinRange
	{
		get
		{
			if (float.IsNaN(_value))
			{
				return false;
			}
			if (Unclamped)
			{
				return !UnsignedOnly || _value >= 0f;
			}
			return _value > Min - 0.0001f && _value < Max + 0.0001f;
		}
	}

	public string Suffix { get; private set; }

	public string Prefix { get; private set; }

	public float Min { get; private set; }

	public float Max { get; private set; }

	public bool Unclamped { get; private set; }

	public bool Looped { get; private set; }

	public bool UnsignedOnly { get; private set; }

	public override bool isDefaultValue
	{
		get
		{
			return _value == _defaultValue;
		}
	}

	public event ValueChangeHandler ValueChanged;

	public MSlider(int nameLocalisationId, string key, float value, float min, float max, string prefix, string suffix, bool unclamped, bool looped, bool unsignedOnly = false)
		: base(nameLocalisationId, key)
	{
		if (min > max)
		{
			min = max - float.Epsilon;
			Debug.LogWarning("Min is greater or equal to Max.");
		}
		Min = min;
		Max = max;
		Prefix = prefix;
		Suffix = suffix;
		Unclamped = unclamped;
		Looped = looped;
		UnsignedOnly = unsignedOnly;
		_value = (_loadValue = (_defaultValue = value));
		InvokeValueChanged(value);
		base.defaultData = Serialize();
	}

	public MSlider(string displayName, string key, float value, float min, float max, string prefix, string suffix, bool unclamped, bool looped, bool unsignedOnly = false)
		: base(displayName, key)
	{
		if (min > max)
		{
			min = max - float.Epsilon;
			Debug.LogWarning("Min is greater or equal to Max.");
		}
		Min = min;
		Max = max;
		Prefix = prefix;
		Suffix = suffix;
		Unclamped = unclamped;
		Looped = looped;
		UnsignedOnly = unsignedOnly;
		_value = (_loadValue = (_defaultValue = value));
		InvokeValueChanged(value);
		base.defaultData = Serialize();
	}

	public void SetRange(float min, float max)
	{
		Min = min;
		Max = max;
	}

	public override void ResetDefaults()
	{
		_value = _defaultValue;
		ApplyValue();
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

	public override void DeSerialize(XData raw)
	{
		_value = (_loadValue = (float)(XSingle)raw);
		if (XData.Clamp && !Unclamped && !StatMaster.KeyMapper.disableSliderLimits)
		{
			_value = (_loadValue = Mathf.Clamp(_value, Min, Max));
		}
		InvokeValueChanged(_value);
	}

	public override bool CompareValue(MapperType other)
	{
		MSlider mSlider = other as MSlider;
		return mSlider != null && mSlider.Value == Value;
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
