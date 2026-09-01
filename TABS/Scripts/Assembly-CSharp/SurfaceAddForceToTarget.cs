using UnityEngine;

public class SurfaceAddForceToTarget : ProjectileSurfaceEffect
{
	public FindDataHandler dataFinder;

	public float forceMultiplier = 1f;

	public override bool DoEffect(HitData hit, GameObject projectile)
	{
		if (!dataFinder)
		{
			dataFinder = GetComponentInParent<FindDataHandler>();
		}
		if ((bool)dataFinder)
		{
			ProjectileHit component = projectile.GetComponent<ProjectileHit>();
			if ((bool)component)
			{
				component.AddForceToTarget(dataFinder.data.mainRig.position, dataFinder.data.mainRig, forceMultiplier);
			}
		}
		return false;
	}
}
