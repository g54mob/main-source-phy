public class MLimits : MapperType
{
	private float _loadMin;

	private float _loadMax;

	private float _defaultMin;

	private float _defaultMax;

	public FauxTransform iconInfo;

	public float Min { get; set; }

	public float Max { get; set; }

	public float MaxValue { get; set; }

	public MToggle UseLimitsToggle { get; set; }

	public ILimitsDisplay LimitsDisplay { get; private set; }

	public bool IsActive
	{
		get
		{
			return UseLimitsToggle.IsActive;
		}
	}

	public override bool isDefaultValue
	{
		get
		{
			return Min == _defaultMin && Max == _defaultMax;
		}
	}

	public event LimitsChangeHandler LimitsChanged;

	public MLimits(int nameLocalisationId, string key, float defaultMin, float defaultMax, float highestAngle, FauxTransform iconInfo, ILimitsDisplay limitsDisplay)
		: base(nameLocalisationId, key)
	{
		Min = (_loadMin = (_defaultMin = defaultMin));
		Max = (_loadMax = (_defaultMax = defaultMax));
		MaxValue = highestAngle;
		LimitsDisplay = limitsDisplay;
		InvokeLimitsChanged();
		base.defaultData = Serialize();
		this.iconInfo = iconInfo;
	}

	public MLimits(string displayName, string key, float defaultMin, float defaultMax, float highestAngle, FauxTransform iconInfo, ILimitsDisplay limitsDisplay)
		: base(displayName, key)
	{
		Min = (_loadMin = (_defaultMin = defaultMin));
		Max = (_loadMax = (_defaultMax = defaultMax));
		MaxValue = highestAngle;
		LimitsDisplay = limitsDisplay;
		InvokeLimitsChanged();
		base.defaultData = Serialize();
		this.iconInfo = iconInfo;
	}

	public override XData Serialize()
	{
		return new XSingleArray("bmt-" + base.Key, new float[2] { Min, Max });
	}

	public override XData SerializeLoadValue()
	{
		return new XSingleArray("bmt-" + base.Key, new float[2] { _loadMin, _loadMax });
	}

	public override XData SerializeDefault()
	{
		return new XSingleArray("bmt-" + base.Key, new float[2] { _defaultMin, _defaultMax });
	}

	public override void DeSerialize(XData raw)
	{
		float[] array = (float[])(XSingleArray)raw;
		_loadMin = array[0];
		Min = _loadMin;
		_loadMax = array[1];
		Max = _loadMax;
		InvokeLimitsChanged();
	}

	public override void ResetDefaults()
	{
		Min = _defaultMin;
		Max = _defaultMax;
		InvokeLimitsChanged();
	}

	public override void ResetValue()
	{
		Min = _loadMin;
		Max = _loadMax;
		InvokeLimitsChanged();
	}

	public override void ApplyValue()
	{
		_loadMin = Min;
		_loadMax = Max;
		InvokeLimitsChanged();
	}

	public override bool CompareValue(MapperType other)
	{
		MLimits mLimits = other as MLimits;
		return mLimits != null && mLimits.Min == Min && mLimits.Max == Max;
	}

	private void InvokeLimitsChanged()
	{
		LimitsChangeHandler limitsChanged = this.LimitsChanged;
		if (limitsChanged != null)
		{
			limitsChanged();
		}
	}
}
