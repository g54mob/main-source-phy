using UnityEngine;

public class UndoActionTransform : UndoAction
{
	private readonly Vector3 lastPos;

	private readonly Quaternion lastRot;

	private readonly Vector3 pos;

	private readonly Quaternion rot;

	public UndoActionTransform(Machine m, Vector3 prevPos, Vector3 newPos, Quaternion prevRot, Quaternion newRot)
	{
		if (StatMaster.isMP)
		{
			PlayerData.localPlayer.buildZone.UndoTransform(newPos, newRot, out pos, out rot);
			PlayerData.localPlayer.buildZone.UndoTransform(prevPos, prevRot, out lastPos, out lastRot);
		}
		else
		{
			lastPos = prevPos;
			pos = newPos;
			lastRot = prevRot;
			rot = newRot;
		}
		changesTransform = true;
		machine = m;
	}

	public override bool Redo()
	{
		Vector3 newPos = pos;
		Quaternion newRot = rot;
		if (StatMaster.isMP)
		{
			PlayerData.localPlayer.buildZone.ApplyTransform(newPos, newRot, out newPos, out newRot);
		}
		machine.SetTransform(newPos, newRot);
		return true;
	}

	public override bool Undo()
	{
		Vector3 newPos = lastPos;
		Quaternion newRot = lastRot;
		if (StatMaster.isMP)
		{
			PlayerData.localPlayer.buildZone.ApplyTransform(newPos, newRot, out newPos, out newRot);
		}
		machine.SetTransform(newPos, newRot);
		return true;
	}
}
