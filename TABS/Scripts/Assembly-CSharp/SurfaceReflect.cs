using UnityEngine;
using UnityEngine.Events;

public class SurfaceReflect : ProjectileSurfaceEffect
{
	public ParticleSystem part;

	public UnityEvent events;

	public override bool DoEffect(HitData hit, GameObject projectile)
	{
		if (!base.enabled)
		{
			return false;
		}
		MoveTransform component = projectile.GetComponent<MoveTransform>();
		TeamHolder component2 = projectile.GetComponent<TeamHolder>();
		ProjectileHit component3 = projectile.GetComponent<ProjectileHit>();
		if ((bool)component2)
		{
			component2.SwitchTeam();
		}
		component.velocity *= -1f;
		component3.canHitOrgUnit = true;
		projectile.GetComponent<RaycastTrail>().ignoredFrames = 2;
		part.transform.position = hit.point + base.transform.forward * 0.4f;
		part.Emit(10);
		events.Invoke();
		return true;
	}
}
