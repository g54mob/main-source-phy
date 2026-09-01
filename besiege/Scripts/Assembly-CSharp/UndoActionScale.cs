using System;
using UnityEngine;

public class UndoActionScale : UndoAction
{
	private readonly Vector3 lastScale;

	private readonly Vector3 scale;

	private readonly Vector3 lastPos;

	private readonly Vector3 pos;

	public UndoActionScale(Machine m, Guid blockGuid, Vector3 lPos, Vector3 p, Vector3 lScale, Vector3 s)
	{
		pos = p;
		lastPos = lPos;
		scale = s;
		lastScale = lScale;
		guid = blockGuid;
		changesTransform = true;
		machine = m;
	}

	public override bool Redo()
	{
		Transform buildingMachine = machine.BuildingMachine;
		machine.MoveBlock(guid, buildingMachine.TransformPoint(pos));
		machine.ScaleBlock(guid, scale);
		return true;
	}

	public override bool Undo()
	{
		Transform buildingMachine = machine.BuildingMachine;
		machine.MoveBlock(guid, buildingMachine.TransformPoint(lastPos));
		machine.ScaleBlock(guid, lastScale);
		return true;
	}
}
