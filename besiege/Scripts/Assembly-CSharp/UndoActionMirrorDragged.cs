using System;
using UnityEngine;

public class UndoActionMirrorDragged : UndoAction
{
	private readonly Quaternion rot;

	private readonly Quaternion lastRot;

	private readonly Vector3 pos;

	private readonly Vector3 lastPos;

	private readonly Vector3 posA;

	private readonly Vector3 lastPosA;

	private readonly Vector3 eulerA;

	private readonly Vector3 lastEulerA;

	private readonly Vector3 posB;

	private readonly Vector3 lastPosB;

	private readonly Vector3 eulerB;

	private readonly Vector3 lastEulerB;

	public UndoActionMirrorDragged(Machine m, Guid blockGuid, Vector3 p, Vector3 lPos, Quaternion r, Quaternion lRot, Vector3 pA, Vector3 lpA, Vector3 pB, Vector3 lpB, Vector3 eA, Vector3 leA, Vector3 eB, Vector3 leB)
	{
		rot = r;
		lastRot = lRot;
		pos = p;
		lastPos = lPos;
		posA = pA;
		lastPosA = lpA;
		posB = pB;
		lastPosB = lpB;
		eulerA = eA;
		lastEulerA = leA;
		eulerB = eB;
		lastEulerB = leB;
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
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			GenericDraggedBlock genericDraggedBlock = block as GenericDraggedBlock;
			genericDraggedBlock.SetPositionsGlobal(posA, eulerA, posB, eulerB, true);
		}
		return true;
	}

	public override bool Undo()
	{
		Transform buildingMachine = machine.BuildingMachine;
		machine.RotateBlock(guid, buildingMachine.rotation * lastRot);
		machine.MoveBlock(guid, buildingMachine.TransformPoint(lastPos));
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			GenericDraggedBlock genericDraggedBlock = block as GenericDraggedBlock;
			genericDraggedBlock.SetPositionsGlobal(lastPosA, lastEulerA, lastPosB, lastEulerB, true);
		}
		return true;
	}
}
