using UnityEngine;

public class ProjectileBounce : ProjectileHitEffect
{
	private MoveTransform move;

	public float minBounce = 0.3f;

	public float maxBounce = 0.5f;

	public float minVel;

	private ProjectileHit projectielHit;

	private void Start()
	{
		move = GetComponent<MoveTransform>();
		projectielHit = GetComponent<ProjectileHit>();
	}

	private void Update()
	{
	}

	public override bool DoEffect(HitData hit)
	{
		move.velocity = Vector3.Reflect(move.velocity, hit.normal);
		float num = Random.Range(minBounce, maxBounce);
		move.velocity *= num;
		if ((bool)projectielHit)
		{
			projectielHit.force *= num;
			projectielHit.damage *= num;
		}
		if (minVel != 0f && move.velocity.magnitude < minVel)
		{
			move.velocity = move.velocity.normalized * minVel;
		}
		if (move.velocity.magnitude < 0.2f)
		{
			base.transform.position = hit.point;
			move.enabled = false;
			return false;
		}
		return true;
	}
}
