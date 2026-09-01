public sealed class MHealthType : MapperType
{
	private HealthRange _range;

	private HealthRange _loadRange;

	private HealthRange _defaultRange;

	public HealthRange HealthRange
	{
		get
		{
			return _range;
		}
		set
		{
			if (_range != value)
			{
				_range = value;
				InvokeRangeChanged(value);
			}
		}
	}

	public override bool isDefaultValue
	{
		get
		{
			return _range == _defaultRange;
		}
	}

	public event HealthHandler HealthRangeChanged;

	public MHealthType(string displayName, string key, HealthRange defaultValue)
		: base(displayName, key)
	{
		_range = (_loadRange = (_defaultRange = defaultValue));
		InvokeRangeChanged(_range);
		base.defaultData = Serialize();
	}

	public override XData Serialize()
	{
		return new XInteger("bmt-" + base.Key, (int)HealthRange);
	}

	public override XData SerializeLoadValue()
	{
		return new XInteger("bmt-" + base.Key, (int)_loadRange);
	}

	public override XData SerializeDefault()
	{
		return new XInteger("bmt-" + base.Key, (int)_defaultRange);
	}

	public override void ApplyValue()
	{
		_loadRange = _range;
		InvokeRangeChanged(_range);
	}

	public void SetValue(HealthRange range)
	{
		_range = range;
	}

	public override void ResetValue()
	{
		HealthRange = _loadRange;
	}

	public override void ResetDefaults()
	{
		HealthRange = _defaultRange;
	}

	public override void DeSerialize(XData raw)
	{
		int loadRange = (int)(XInteger)raw;
		_range = (_loadRange = (HealthRange)loadRange);
		InvokeRangeChanged(_range);
	}

	public override bool CompareValue(MapperType other)
	{
		MHealthType mHealthType = other as MHealthType;
		return mHealthType != null && mHealthType.HealthRange == HealthRange;
	}

	private void InvokeRangeChanged(HealthRange range)
	{
		HealthHandler healthRangeChanged = this.HealthRangeChanged;
		if (healthRangeChanged != null)
		{
			healthRangeChanged(range);
		}
	}
}
