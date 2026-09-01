using UnityEngine;

public class DarkPHandsBlock : ProjectileSurfaceEffect
{
	public GameObject blockHand;

	public override bool DoEffect(HitData hit, GameObject projectile)
	{
		GameObject gameObject = Object.Instantiate(blockHand, hit.point, Quaternion.LookRotation(hit.normal));
		gameObject.GetComponent<RemoveAfterSeconds>().seconds = 0.5f;
		gameObject.transform.position += gameObject.transform.up * -0.2f;
		projectile.GetComponent<MoveTransform>().velocity *= -1f;
		projectile.GetComponent<ProjectileHit>().SwitchTarget(gameObject);
		projectile.GetComponent<ProjectileHit>().ResetTeam();
		return false;
	}
}
