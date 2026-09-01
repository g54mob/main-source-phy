using System.Collections.Generic;

public sealed class MMenu : MapperType
{
	private List<string> _items = new List<string>();

	public bool isFooterMenu;

	private int _loadValue;

	private int _defaultValue;

	private int _value;

	public List<string> Items
	{
		get
		{
			return _items;
		}
		set
		{
			_items = value;
		}
	}

	public int Value
	{
		get
		{
			return (_value < _items.Count) ? _value : 0;
		}
		set
		{
			if (_value != value)
			{
				_value = value;
				InvokeChange(value);
			}
		}
	}

	public string Selection
	{
		get
		{
			return _items[Value];
		}
	}

	public override bool isDefaultValue
	{
		get
		{
			return _value == _defaultValue;
		}
	}

	public event ValueHandler ValueChanged;

	public MMenu(string key, int defaultIndex, List<string> items, bool footerMenu)
		: base(null, key)
	{
		_items = items;
		isFooterMenu = footerMenu;
		_value = (_loadValue = (_defaultValue = defaultIndex));
		InvokeChange(_value);
		base.defaultData = Serialize();
	}

	public void SetValue(int val)
	{
		_value = val;
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
		InvokeChange(_value);
	}

	public override XData Serialize()
	{
		return new XInteger("bmt-" + base.Key, Value);
	}

	public override XData SerializeLoadValue()
	{
		return new XInteger("bmt-" + base.Key, _loadValue);
	}

	public override XData SerializeDefault()
	{
		return new XInteger("bmt-" + base.Key, _defaultValue);
	}

	public override void DeSerialize(XData raw)
	{
		_value = (_loadValue = (int)(XInteger)raw);
		InvokeChange(_value);
	}

	public override bool CompareValue(MapperType other)
	{
		MMenu mMenu = other as MMenu;
		return mMenu != null && mMenu.Value == Value;
	}

	private void InvokeChange(int value)
	{
		ValueHandler valueChanged = this.ValueChanged;
		if (valueChanged != null)
		{
			valueChanged(value);
		}
	}
}
