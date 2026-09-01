public class ProjectileCopy : ProjectileHitEffect
{
	private ProjectileHit projectileHit;

	public MeleeWeaponCopySelf selfCopy;

	private void Start()
	{
		projectileHit = base.transform.GetComponent<ProjectileHit>();
	}

	public override bool DoEffect(HitData hit)
	{
		selfCopy.RangedDoEffect(hit.transform);
		return false;
	}
}
