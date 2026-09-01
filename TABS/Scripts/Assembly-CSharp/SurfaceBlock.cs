using System;
using UnityEngine;
using UnityEngine.Events;

public class SurfaceBlock : ProjectileSurfaceEffect
{
	[Serializable]
	public class OnHitEvent : UnityEvent<Vector3>
	{
	}

	public BlockMove blockMove;

	public OnHitEvent onBlock;

	public override bool DoEffect(HitData hit, GameObject projectile)
	{
		if (blockMove.ProjectileBlock(projectile, hit))
		{
			onBlock?.Invoke(hit.point);
		}
		return true;
	}
}
