using UnityEngine;

public abstract class ProjectileHitEffect : MonoBehaviour
{
	public abstract bool DoEffect(HitData hit);
}
