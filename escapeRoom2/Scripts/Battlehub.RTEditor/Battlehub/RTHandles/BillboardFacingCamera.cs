using UnityEngine;

namespace Battlehub.RTHandles
{
	public class BillboardFacingCamera : MonoBehaviour
	{
		private void OnWillRenderObject()
		{
			base.transform.LookAt(base.transform.position - (Camera.current.transform.position - base.transform.position), Vector3.up);
		}
	}
}
