using System;
using UnityEngine;

public class UndoActionRotate : UndoAction
{
	private readonly Quaternion lastRot;

	private readonly Quaternion rot;

	private readonly Vector3 lastPos;

	private readonly Vector3 pos;

	public UndoActionRotate(Machine m, Guid blockGuid, Vector3 p, Vector3 lPos, Quaternion r, Quaternion lRot)
	{
		rot = r;
		lastRot = lRot;
		pos = p;
		lastPos = lPos;
		guid = blockGuid;
		changesTransform = true;
		machine = m;
		machine.RotateBlock(guid, machine.BuildingMachine.rotation * r);
		machine.MoveBlock(guid, machine.BuildingMachine.TransformPoint(p));
	}

	public override bool Redo()
	{
		Transform buildingMachine = machine.BuildingMachine;
		machine.RotateBlock(guid, buildingMachine.rotation * rot);
		machine.MoveBlock(guid, buildingMachine.TransformPoint(pos));
		return true;
	}

	public override bool Undo()
	{
		Transform buildingMachine = machine.BuildingMachine;
		machine.RotateBlock(guid, buildingMachine.rotation * lastRot);
		machine.MoveBlock(guid, buildingMachine.TransformPoint(lastPos));
		return true;
	}
}
