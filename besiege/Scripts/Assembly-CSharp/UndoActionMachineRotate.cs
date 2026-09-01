using System.Collections.Generic;
using UnityEngine;

public class UndoActionMachineRotate : UndoAction
{
	private readonly Quaternion lastRot;

	private readonly Quaternion rot;

	public UndoActionMachineRotate(Machine m, Quaternion prevRot, Quaternion newRot)
	{
		if (StatMaster.isMP)
		{
			PlayerBuildZone buildZone = PlayerData.localPlayer.buildZone;
			Vector3 newPos = Vector3.zero;
			buildZone.UndoTransform(newPos, newRot, out newPos, out rot);
			buildZone.UndoTransform(newPos, prevRot, out newPos, out lastRot);
		}
		else
		{
			lastRot = prevRot;
			rot = newRot;
		}
		changesTransform = true;
		machine = m;
	}

	public override bool Redo()
	{
		Quaternion newRot = rot;
		if (StatMaster.isMP)
		{
			Vector3 newPos = Vector3.zero;
			PlayerData.localPlayer.buildZone.ApplyTransform(newPos, newRot, out newPos, out newRot);
		}
		machine.SetRotation(newRot);
		return true;
	}

	public override bool Undo()
	{
		Quaternion newRot = lastRot;
		if (StatMaster.isMP)
		{
			Vector3 newPos = Vector3.zero;
			PlayerData.localPlayer.buildZone.ApplyTransform(newPos, newRot, out newPos, out newRot);
		}
		machine.SetRotation(newRot);
		return true;
	}

	public override List<BlockBehaviour> GetBlocks()
	{
		return new List<BlockBehaviour>();
	}
}
