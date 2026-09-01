using UnityEngine;

namespace CodeAnimo
{
	public class MouseHitFinder : MonoBehaviour
	{
		public Camera UserCamera;

		public RaycastHit targetData;

		[Range(0f, 10000f)]
		public float range = 10000f;

		public LayerMask activeLayers = -1;

		private Ray lastHitRay;

		private bool m_didLastCallHit;

		public bool MouseHitSomething()
		{
			if (UserCamera != null)
			{
				Ray ray = UserCamera.ScreenPointToRay(Input.mousePosition);
				m_didLastCallHit = Physics.Raycast(ray, out targetData, range, activeLayers.value);
				if (m_didLastCallHit)
				{
					lastHitRay = ray;
				}
				return m_didLastCallHit;
			}
			Debug.LogException(new MissingReferenceException("No Camera Selected by MouseHitFinder."), this);
			return false;
		}

		public ScreenRayCastData CastScreenRay(Vector3 screenPoint, Camera viewport)
		{
			ScreenRayCastData screenRayCastData = new ScreenRayCastData();
			screenRayCastData.usedRay = viewport.ScreenPointToRay(screenPoint);
			screenRayCastData.activeLayers = activeLayers;
			screenRayCastData.range = range;
			screenRayCastData.hit = Physics.Raycast(screenRayCastData.usedRay, out screenRayCastData.hitData, range, activeLayers.value);
			return screenRayCastData;
		}

		public void OnDrawGizmosSelected()
		{
			if (m_didLastCallHit)
			{
				Gizmos.DrawLine(lastHitRay.origin, targetData.point);
			}
		}
	}
}
