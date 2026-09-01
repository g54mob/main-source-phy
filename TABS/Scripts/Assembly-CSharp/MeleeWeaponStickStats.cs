using UnityEngine;
using UnityEngine.Events;

public class MeleeWeaponStickStats : MonoBehaviour
{
	public float fixPositionAmount;

	public float breakForce = 20000f;

	public bool onlyOtherTeam;

	public bool walkBackwardsWhenStuck;

	public float downwardsForceOnStuckRig;

	public float time = 3f;

	public UnityEvent stickEvent;

	public bool lockRotation;

	public float radius = 0.3f;

	private void Start()
	{
	}
}
