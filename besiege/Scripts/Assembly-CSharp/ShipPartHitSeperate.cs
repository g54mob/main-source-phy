using UnityEngine;

public class ShipPartHitSeperate : MonoBehaviour
{
	public ShipMultibodyAI controller;

	public float speedInfluence = 0.25f;

	public void OnJointBreak(float breakForce)
	{
		controller.globalSpeed -= controller.orgGlobalSpeed * speedInfluence;
	}
}
