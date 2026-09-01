using System.Collections.Generic;
using UnityEngine;

public sealed class MVisual : MapperType
{
	private List<BlockSkinLoader.SkinPack.Skin> _items = new List<BlockSkinLoader.SkinPack.Skin>();

	private int _value;

	private BlockVisualController _controller;

	public List<BlockSkinLoader.SkinPack.Skin> Items
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
			return _value;
		}
		set
		{
			_value = value;
			InvokeChange(value);
		}
	}

	public BlockVisualController Controller
	{
		get
		{
			return _controller;
		}
		set
		{
			_controller = value;
		}
	}

	public override bool isDefaultValue
	{
		get
		{
			return false;
		}
	}

	public BlockSkinLoader.SkinPack.Skin Selection
	{
		get
		{
			return Items[Value];
		}
	}

	public event VisChangeHandler ValueChanged;

	public MVisual(BlockVisualController selector, int defaultIndex, List<BlockSkinLoader.SkinPack.Skin> items, string key = null, string displayName = null)
		: base(displayName, key)
	{
		_items = items;
		_value = ((defaultIndex != -1) ? defaultIndex : 0);
		_controller = selector;
		base.DisplayName = Selection.pack.name.ToUpper();
		InvokeChange(_value);
	}

	public void SetSkin(BlockSkinLoader.SkinPack.Skin skin)
	{
		int num = _items.IndexOf(skin);
		Value = ((num != -1) ? num : 0);
	}

	public void SetValue(int val)
	{
		_value = val;
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

	public override void ResetValue()
	{
	}

	public override void DeSerialize(XData raw)
	{
	}

	public override bool CompareValue(MapperType other)
	{
		Debug.LogError("CompareValue not implemented for MVisual");
		return false;
	}

	private void InvokeChange(int value)
	{
		VisChangeHandler valueChanged = this.ValueChanged;
		if (valueChanged != null)
		{
			valueChanged(value);
		}
	}
}
