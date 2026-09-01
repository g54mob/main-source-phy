using System;

namespace VLB;

public class MinMaxRangeAttribute : Attribute
{
	private float _003CminValue_003Ek__BackingField;

	private float _003CmaxValue_003Ek__BackingField;

	public float minValue
	{
		get
		{
			return _003CminValue_003Ek__BackingField;
		}
		private set
		{
			_003CminValue_003Ek__BackingField = value;
		}
	}

	public float maxValue
	{
		get
		{
			return _003CmaxValue_003Ek__BackingField;
		}
		private set
		{
			_003CmaxValue_003Ek__BackingField = value;
		}
	}

	public MinMaxRangeAttribute(float min, float max)
	{
		_003CminValue_003Ek__BackingField = min;
		_003CmaxValue_003Ek__BackingField = max;
	}
}
