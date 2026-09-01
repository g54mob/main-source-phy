using UnityEngine;

[AddComponentMenu("Destruction/Break On Force (Ship)")]
public class BreakOnForceShip : BreakOnForceNoScaling
{
	public Rigidbody orb;

	public MonoBehaviour orbFollow;

	public SmoothLookAtMachine lookAt;

	public override Transform BreakObj()
	{
		orb.useGravity = true;
		lookAt.source = lookAt.transform;
		Object.Destroy(orbFollow);
		return base.BreakObj();
	}
}
