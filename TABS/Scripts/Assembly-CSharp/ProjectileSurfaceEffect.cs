using UnityEngine;

public abstract class ProjectileSurfaceEffect : MonoBehaviour
{
	public abstract bool DoEffect(HitData hit, GameObject projectile);
}
