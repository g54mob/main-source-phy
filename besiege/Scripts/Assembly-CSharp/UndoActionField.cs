using System;
using System.Collections.Generic;
using Mono.CSharp;

public class UndoActionField : UndoAction
{
	private readonly Dictionary<Guid, Tuple<XData, XData>> guidData;

	public UndoActionField(Machine m, Dictionary<BlockBehaviour, Tuple<XData, XData>> blockData)
	{
		changesParameters = true;
		guidData = new Dictionary<Guid, Tuple<XData, XData>>();
		foreach (KeyValuePair<BlockBehaviour, Tuple<XData, XData>> blockDatum in blockData)
		{
			guidData.Add(blockDatum.Key.Guid, blockDatum.Value);
		}
		machine = m;
	}

	public override bool Undo()
	{
		List<Tuple<BlockBehaviour, XData>> list = new List<Tuple<BlockBehaviour, XData>>();
		foreach (KeyValuePair<Guid, Tuple<XData, XData>> guidDatum in guidData)
		{
			BlockBehaviour block;
			if (machine.GetBlock(guidDatum.Key, out block))
			{
				list.Add(new Tuple<BlockBehaviour, XData>(block, guidDatum.Value.Item1));
			}
		}
		NetworkEditFieldHandler networkEditFieldHandler = EditFieldHandler.Instance as NetworkEditFieldHandler;
		if ((bool)networkEditFieldHandler)
		{
			networkEditFieldHandler.OnEditBlockField(list, false, true);
		}
		else
		{
			for (int i = 0; i < list.Count; i++)
			{
				BlockBehaviour block = list[i].Item1;
				XData item = list[i].Item2;
				if (item != null)
				{
					block.Load(item);
					block.OnSave(new XDataHolder());
				}
			}
			if (!isMultiAction)
			{
				BlockBehaviour block = list[list.Count - 1].Item1;
				OpenBlockMapper(block);
			}
		}
		return true;
	}

	public override bool Redo()
	{
		List<Tuple<BlockBehaviour, XData>> list = new List<Tuple<BlockBehaviour, XData>>();
		foreach (KeyValuePair<Guid, Tuple<XData, XData>> guidDatum in guidData)
		{
			BlockBehaviour block;
			if (machine.GetBlock(guidDatum.Key, out block))
			{
				list.Add(new Tuple<BlockBehaviour, XData>(block, guidDatum.Value.Item2));
			}
		}
		NetworkEditFieldHandler networkEditFieldHandler = EditFieldHandler.Instance as NetworkEditFieldHandler;
		if ((bool)networkEditFieldHandler)
		{
			networkEditFieldHandler.OnEditBlockField(list, false, true);
		}
		else
		{
			for (int i = 0; i < list.Count; i++)
			{
				BlockBehaviour block = list[i].Item1;
				XData item = list[i].Item2;
				if (item != null)
				{
					block.Load(item);
					block.OnSave(new XDataHolder());
				}
			}
			if (!isMultiAction)
			{
				BlockBehaviour block = list[list.Count - 1].Item1;
				OpenBlockMapper(block);
			}
		}
		return true;
	}
}
