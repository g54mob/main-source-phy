using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	public Camera cam;

	public bool lookAtPlane;

	private Transform camTransform;

	private void Start()
	{
		if (cam == null)
		{
			if ((bool)SingleInstanceFindOnly<AddPiece>.Instance)
			{
				cam = SingleInstanceFindOnly<AddPiece>.Instance.mainCam;
			}
			else
			{
				cam = Camera.main;
			}
		}
		camTransform = cam.transform;
	}

	private void Update()
	{
		if (lookAtPlane)
		{
			Vector3 worldPosition = ProjectPointOnPlane(camTransform.forward, camTransform.position, base.transform.position);
			base.transform.LookAt(worldPosition, camTransform.up);
		}
		else
		{
			base.transform.LookAt(camTransform);
		}
	}

	public static Vector3 ProjectPointOnPlane(Vector3 planeNormal, Vector3 planePoint, Vector3 point)
	{
		float num = 0f - Vector3.Dot(planeNormal, point - planePoint);
		return point + planeNormal * num;
	}
}
