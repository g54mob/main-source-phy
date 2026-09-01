using UnityEngine;
using UnityEngine.Events;

public class MountPos : MonoBehaviour
{
	public enum MountType
	{
		Sit = 0,
		Hold = 1,
		SitAndHold = 2
	}

	public StaticAnimation mountAnimation;

	public MountType mountType;

	public bool setGrounded = true;

	public bool neverLetGo;

	public float letGoAngle = 80f;

	public bool giveForwardObjectToRider;

	public bool removeArmColliders;

	public bool EnableCollision = true;

	public bool LockedLegs = true;

	public bool inheritVelocity;

	public float jointStrength = 1f;

	public float angles = 25f;

	public float angularJointStrength = 1f;

	public float breakForce;

	public bool killVehicleIfRiderDies;

	public bool killRiderIfVehicleDies;

	public float dropMountWithinRange;

	public float dropDelay;

	public float upwardsDropForce;

	public float forwardDropForce;

	public float setMassOnMountOnDrop;

	public float forwardDismountForce;

	public float upwardDismountForce;

	public bool sendGroundedToMount;

	public bool overrideInputFull;

	public bool overrideInputDirection;

	public UnityEvent riderDieEvent;

	public float dieDelay;

	public Explosion[] explosionsToProtectAgainst;

	public bool disablePossession;
}
