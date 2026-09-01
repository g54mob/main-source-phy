using System.Collections.Generic;
using UnityEngine;

public class UndoActionsMachineMove : UndoAction
{
	private readonly Vector3 lastPos;

	private readonly Vector3 pos;

	public UndoActionsMachineMove(Machine m, Vector3 prevPos, Vector3 newPos)
	{
		if (StatMaster.isMP)
		{
			Quaternion newRot = Quaternion.identity;
			PlayerBuildZone buildZone = PlayerData.localPlayer.buildZone;
			buildZone.UndoTransform(newPos, newRot, out pos, out newRot);
			buildZone.UndoTransform(prevPos, newRot, out lastPos, out newRot);
		}
		else
		{
			lastPos = prevPos;
			pos = newPos;
		}
		changesTransform = true;
		machine = m;
	}

	public override bool Redo()
	{
		Vector3 newPos = pos;
		if (StatMaster.isMP)
		{
			Quaternion newRot = Quaternion.identity;
			PlayerData.localPlayer.buildZone.ApplyTransform(newPos, newRot, out newPos, out newRot);
		}
		machine.SetPosition(newPos);
		return true;
	}

	public override bool Undo()
	{
		Vector3 newPos = lastPos;
		if (StatMaster.isMP)
		{
			Quaternion newRot = Quaternion.identity;
			PlayerData.localPlayer.buildZone.ApplyTransform(newPos, newRot, out newPos, out newRot);
		}
		machine.SetPosition(newPos);
		return true;
	}

	public override List<BlockBehaviour> GetBlocks()
	{
		return new List<BlockBehaviour>();
	}
}
