public sealed class MToggle : MapperType
{
	private bool _isActive;

	private bool _loadActive;

	private bool _defaultActive;

	public bool IsActive
	{
		get
		{
			return _isActive;
		}
		set
		{
			if (_isActive != value)
			{
				_isActive = value;
				InvokeToggled(value);
			}
		}
	}

	public override bool isDefaultValue
	{
		get
		{
			return _isActive == _defaultActive;
		}
	}

	public event ToggleHandler Toggled;

	public MToggle(int nameLocalisationId, string key, bool defaultValue)
		: base(nameLocalisationId, key)
	{
		_isActive = (_loadActive = (_defaultActive = defaultValue));
		InvokeToggled(_isActive);
		base.defaultData = Serialize();
	}

	public MToggle(string displayName, string key, bool defaultValue)
		: base(displayName, key)
	{
		_isActive = (_loadActive = (_defaultActive = defaultValue));
		InvokeToggled(_isActive);
		base.defaultData = Serialize();
	}

	public override void ResetDefaults()
	{
		IsActive = _defaultActive;
	}

	public override void ResetValue()
	{
		IsActive = _loadActive;
	}

	public void SetValue(bool toggle)
	{
		_isActive = toggle;
	}

	public override void ApplyValue()
	{
		_loadActive = _isActive;
		InvokeToggled(_isActive);
	}

	public override XData Serialize()
	{
		return new XBoolean("bmt-" + base.Key, IsActive);
	}

	public override XData SerializeLoadValue()
	{
		return new XBoolean("bmt-" + base.Key, _loadActive);
	}

	public override XData SerializeDefault()
	{
		return new XBoolean("bmt-" + base.Key, _defaultActive);
	}

	public override void DeSerialize(XData raw)
	{
		_isActive = (_loadActive = (bool)(XBoolean)raw);
		InvokeToggled(_isActive);
	}

	public override bool CompareValue(MapperType other)
	{
		MToggle mToggle = other as MToggle;
		return mToggle != null && mToggle.IsActive == _isActive;
	}

	private void InvokeToggled(bool isactive)
	{
		ToggleHandler toggled = this.Toggled;
		if (toggled != null)
		{
			toggled(isactive);
		}
	}
}
