using RootMotion.FinalIK;
using UnityEngine;

public class PlayerIKHandler : MonoBehaviour
{
	public LimbIK left;

	public LimbIK right;

	public void StartHolding(FPSWeapon weapon)
	{
		right.solver.target = weapon.GetComponentInChildren<HandRight>(includeInactive: true).transform;
		left.solver.target = weapon.GetComponentInChildren<HandLeft>(includeInactive: true).transform;
		FPSLeftHand componentInChildren = weapon.GetComponentInChildren<FPSLeftHand>();
		FPSRightHand componentInChildren2 = weapon.GetComponentInChildren<FPSRightHand>();
		if ((bool)componentInChildren)
		{
			left.solver.target = componentInChildren.transform;
		}
		if ((bool)componentInChildren2)
		{
			right.solver.target = componentInChildren2.transform;
		}
	}

	public void Drop(FPSWeapon weapon)
	{
	}
}
