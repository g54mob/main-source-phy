using UnityEngine;

public sealed class MColourSlider : MapperType
{
	public Held HeldDown;

	public Released ReleasedButton;

	private Color _value;

	public bool snapColors;

	public bool useHue;

	private Color _loadValue;

	private Color _defaultValue;

	public Color Value
	{
		get
		{
			return _value;
		}
		set
		{
			if (!(_value == value))
			{
				_value = value;
				InvokeValueChanged(value);
			}
		}
	}

	public override bool isDefaultValue
	{
		get
		{
			return _defaultValue == Value;
		}
	}

	public event ColourChangeHandler ValueChanged;

	public MColourSlider(int nameLocalisationId, string key, Color value, bool snapToClosestColor, bool useHue)
		: base(nameLocalisationId, key)
	{
		_value = (_loadValue = (_defaultValue = value));
		snapColors = snapToClosestColor;
		this.useHue = useHue;
		InvokeValueChanged(value);
		base.defaultData = SerializeLoadValue();
	}

	public MColourSlider(string displayName, string key, Color value, bool snapToClosestColor, bool useHue)
		: base(displayName, key)
	{
		_value = (_loadValue = (_defaultValue = value));
		snapColors = snapToClosestColor;
		this.useHue = useHue;
		InvokeValueChanged(value);
		base.defaultData = SerializeLoadValue();
	}

	public override void ResetDefaults()
	{
		Value = _defaultValue;
	}

	public override void ResetValue()
	{
		Value = _loadValue;
	}

	public override void ApplyValue()
	{
		_loadValue = _value;
	}

	public override XData Serialize()
	{
		return new XColor("bmt-" + base.Key, Value);
	}

	public override XData SerializeLoadValue()
	{
		return new XColor("bmt-" + base.Key, _loadValue);
	}

	public override XData SerializeDefault()
	{
		return new XColor("bmt-" + base.Key, _defaultValue);
	}

	public override void DeSerialize(XData raw)
	{
		_value = (_loadValue = (Color)(XColor)raw);
		InvokeValueChanged(_value);
	}

	public override bool CompareValue(MapperType other)
	{
		MColourSlider mColourSlider = other as MColourSlider;
		return mColourSlider != null && mColourSlider.Value == Value;
	}

	private void InvokeValueChanged(Color value)
	{
		ColourChangeHandler valueChanged = this.ValueChanged;
		if (valueChanged != null)
		{
			valueChanged(value);
		}
	}
}
