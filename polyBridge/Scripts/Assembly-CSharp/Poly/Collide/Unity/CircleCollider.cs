using Poly.Base;
using Poly.Extension;
using Poly.Physics;
using UnityEngine;

namespace Poly.Collide.Unity
{
	public class CircleCollider : MonoBehaviour
	{
		public float radius;

		public PhysicsMaterial2D physicsMaterial;

		public int collisionGroup;

		public int collisionLayer;

		private void OnDrawGizmos()
		{
			if (!Application.isPlaying || !SingletonBehaviour<World>.instance)
			{
				Gizmos.color = Color.white;
				for (int i = 0; i < base.transform.childCount; i++)
				{
					Gizmos.DrawLine(base.transform.GetChild(i).position, base.transform.GetChild((i + 1) % base.transform.childCount).position);
					float num = base.transform.lossyScale.MaxCoordValue() * radius;
					Gizmos.DrawWireSphere(base.transform.GetChild(i).position, num);
				}
			}
		}
	}
}
