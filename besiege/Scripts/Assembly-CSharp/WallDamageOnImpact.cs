using UnityEngine;

public class WallDamageOnImpact : MonoBehaviour
{
	public float myHealth = 5f;

	public float damageScaler = 0.001f;

	public CastleWallBreak breakCode;

	public Material myMaterial;

	public float impactThreshold = 10f;

	public float breakExplodePower = 2000f;

	public float breakExplodeRadius = 2f;

	public float breakExplodeUpForce = 1f;

	private void OnCollisionEnter(Collision other)
	{
		float f = other.relativeVelocity.sqrMagnitude * other.collider.attachedRigidbody.mass * damageScaler;
		f = Mathf.Round(f);
		if (f > impactThreshold)
		{
			DestroyMe(other);
		}
	}

	private void DamageMe(float amount, Collision collisionInfo)
	{
		if (myHealth <= 0f)
		{
			DestroyMe(collisionInfo);
		}
	}

	private void DestroyMe(Collision collisionInfo)
	{
		breakCode.BreakExplosion(4000f, collisionInfo.contacts[0].point, 3f, breakExplodeUpForce);
	}
}
