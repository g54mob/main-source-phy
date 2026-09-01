using System;
using UnityEngine;

public class UndoActionMove : UndoAction
{
	private readonly Vector3 lastPos;

	private readonly Vector3 pos;

	public UndoActionMove(Machine m, Guid blockGuid, Vector3 p, Vector3 lPos)
	{
		pos = p;
		lastPos = lPos;
		guid = blockGuid;
		changesTransform = true;
		machine = m;
		machine.MoveBlock(guid, machine.BuildingMachine.TransformPoint(p));
	}

	public override bool Redo()
	{
		machine.MoveBlock(guid, machine.BuildingMachine.TransformPoint(pos));
		return true;
	}

	public override bool Undo()
	{
		machine.MoveBlock(guid, machine.BuildingMachine.TransformPoint(lastPos));
		return true;
	}
}
