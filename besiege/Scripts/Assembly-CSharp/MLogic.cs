using UnityEngine;

public sealed class MLogic : MapperType
{
	private EntityLogic _value;

	public EntityLogic Value
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
				_value.ClearHandler();
				_value.LogicChanged += OnLogicChange;
			}
		}
	}

	public override bool isDefaultValue
	{
		get
		{
			return false;
		}
	}

	public event LogicChangeHandler LogicChanged;

	public MLogic(string displayName, string key, EntityLogic value)
		: base(displayName, key)
	{
		Value = value;
	}

	public void OnLogicChange()
	{
		InvokeLogicChanged();
	}

	public override XData Serialize()
	{
		return null;
	}

	public override XData SerializeLoadValue()
	{
		return null;
	}

	public override XData SerializeDefault()
	{
		return null;
	}

	public override void DeSerialize(XData raw)
	{
	}

	public override bool CompareValue(MapperType other)
	{
		Debug.LogError("CompareValue not implemented for MLogic");
		return false;
	}

	private void InvokeLogicChanged()
	{
		LogicChangeHandler logicChanged = this.LogicChanged;
		if (logicChanged != null)
		{
			logicChanged();
		}
	}
}
