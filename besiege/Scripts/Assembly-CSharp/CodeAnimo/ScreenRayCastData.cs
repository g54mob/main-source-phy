using UnityEngine;

namespace CodeAnimo
{
	public class ScreenRayCastData
	{
		public RaycastHit hitData;

		public Ray usedRay;

		public bool hit;

		public float range;

		public LayerMask activeLayers;
	}
}
