public sealed class MText : MapperType
{
	private string _value;

	private string _loadValue;

	private string _defaultValue;

	public string Value
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
				InvokeTextChanged(value);
			}
		}
	}

	public override bool isDefaultValue
	{
		get
		{
			return _value.Equals(_defaultValue);
		}
	}

	public event TextChangeHandler TextChanged;

	public MText(int nameLocalisationId, string key, string value)
		: base(nameLocalisationId, key)
	{
		_value = (_loadValue = (_defaultValue = value));
		InvokeTextChanged(value);
		base.defaultData = Serialize();
	}

	public MText(string displayName, string key, string value)
		: base(displayName, key)
	{
		_value = (_loadValue = (_defaultValue = value));
		InvokeTextChanged(value);
		base.defaultData = Serialize();
	}

	public void SetDefaultText(string newDefault)
	{
		_defaultValue = newDefault;
	}

	public override void ResetValue()
	{
		Value = _loadValue;
	}

	public override void ResetDefaults()
	{
		Value = _defaultValue;
	}

	public override void ApplyValue()
	{
		_loadValue = _value;
		InvokeTextChanged(_value);
	}

	public void SetValue(string newValue)
	{
		_value = newValue;
	}

	public override XData Serialize()
	{
		return new XString("bmt-" + base.Key, Value);
	}

	public override XData SerializeLoadValue()
	{
		return new XString("bmt-" + base.Key, _loadValue);
	}

	public override XData SerializeDefault()
	{
		return new XString("bmt-" + base.Key, _defaultValue);
	}

	public override void DeSerialize(XData raw)
	{
		_value = (_loadValue = (string)(XString)raw);
		InvokeTextChanged(_value);
	}

	public override bool CompareValue(MapperType other)
	{
		MText mText = other as MText;
		return mText != null && mText.Value == Value;
	}

	private void InvokeTextChanged(string value)
	{
		TextChangeHandler textChanged = this.TextChanged;
		if (textChanged != null)
		{
			textChanged(value);
		}
	}
}
