using UnityEngine;

namespace Battlehub
{
	public static class ParticleSystemExt
	{
		public static int GetColliderCount(this ParticleSystem.TriggerModule o)
		{
			return o.colliderCount;
		}

		public static int GetPlaneCount(this ParticleSystem.CollisionModule o)
		{
			return o.planeCount;
		}
	}
}
