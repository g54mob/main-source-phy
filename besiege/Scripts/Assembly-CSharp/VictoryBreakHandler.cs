using System;
using UnityEngine;

public class VictoryBreakHandler : MonoBehaviour
{
	public bool stopAfterFirst = true;

	public BreakOnForce[] breakOnForces;

	public int victoryPoints;

	private void Start()
	{
		for (int i = 0; i < breakOnForces.Length; i++)
		{
			if (breakOnForces[i] != null)
			{
				BreakOnForce obj = breakOnForces[i];
				obj.victoryTriggered = (Action)Delegate.Combine(obj.victoryTriggered, new Action(OnVictory));
			}
		}
	}

	private void OnVictory()
	{
		WinCondition.currentObjsCompleted += victoryPoints;
		if (!stopAfterFirst)
		{
			return;
		}
		for (int i = 0; i < breakOnForces.Length; i++)
		{
			if (breakOnForces[i] != null)
			{
				BreakOnForce obj = breakOnForces[i];
				obj.victoryTriggered = (Action)Delegate.Remove(obj.victoryTriggered, new Action(OnVictory));
			}
		}
		base.enabled = false;
	}

	private void OnDestroy()
	{
		for (int i = 0; i < breakOnForces.Length; i++)
		{
			if (breakOnForces[i] != null)
			{
				BreakOnForce obj = breakOnForces[i];
				obj.victoryTriggered = (Action)Delegate.Remove(obj.victoryTriggered, new Action(OnVictory));
			}
		}
	}
}
