using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class DisableIfAttribute : PropertyAttribute
{
	public enum BehaviourType
	{
		Disable,
		Hide
	}

	private string _003CPropertyName_003Ek__BackingField;

	private object _003CCompareValue_003Ek__BackingField;

	private string _003CPropertyName2_003Ek__BackingField;

	private object _003CCompareValue2_003Ek__BackingField;

	private BehaviourType _003CBehaviour_003Ek__BackingField;

	private bool _003CInvertBehaviour_003Ek__BackingField;

	public string PropertyName
	{
		get
		{
			return _003CPropertyName_003Ek__BackingField;
		}
		private set
		{
			_003CPropertyName_003Ek__BackingField = value;
		}
	}

	public object CompareValue
	{
		get
		{
			return _003CCompareValue_003Ek__BackingField;
		}
		private set
		{
			_003CCompareValue_003Ek__BackingField = value;
		}
	}

	public string PropertyName2
	{
		get
		{
			return _003CPropertyName2_003Ek__BackingField;
		}
		private set
		{
			_003CPropertyName2_003Ek__BackingField = value;
		}
	}

	public object CompareValue2
	{
		get
		{
			return _003CCompareValue2_003Ek__BackingField;
		}
		private set
		{
			_003CCompareValue2_003Ek__BackingField = value;
		}
	}

	public BehaviourType Behaviour
	{
		get
		{
			return _003CBehaviour_003Ek__BackingField;
		}
		private set
		{
			_003CBehaviour_003Ek__BackingField = value;
		}
	}

	public bool InvertBehaviour
	{
		get
		{
			return _003CInvertBehaviour_003Ek__BackingField;
		}
		private set
		{
			_003CInvertBehaviour_003Ek__BackingField = value;
		}
	}

	public DisableIfAttribute(string propertyName, object comparedValue = null, BehaviourType behaviour = BehaviourType.Disable, bool invertBehaviour = false, string propertyName2 = null, object comparedValue2 = null)
	{
	}
}
