using UnityEngine;

[AddComponentMenu("Physics/CollisionHook")]
public class CollisionHook : MonoBehaviour, IExplosionEffect
{
	public event CollisionHappend CollisionHappend;

	public event ExplosionHappend ExplosionHappend;

	public virtual void OnCollisionEnter(Collision other)
	{
		if (this.CollisionHappend != null)
		{
			this.CollisionHappend(other);
		}
	}

	public virtual bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (this.ExplosionHappend != null)
		{
			return this.ExplosionHappend(base.gameObject, power, upPower, torquePower, explosionPos, radius, mask, inWater);
		}
		return false;
	}
}
