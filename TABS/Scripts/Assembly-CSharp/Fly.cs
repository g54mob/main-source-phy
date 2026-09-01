using UnityEngine;

public class Fly : MonoBehaviour
{
	private DataHandler data;

	private void Start()
	{
		data = GetComponentInParent<DataHandler>();
	}

	private void Update()
	{
		Ray ray = new Ray(data.mainRig.position, Vector3.down);
		RaycastHit hitInfo = default(RaycastHit);
		Physics.Raycast(ray, out hitInfo, 3f);
		if ((bool)hitInfo.transform)
		{
			data.TouchGround(hitInfo.point, hitInfo.normal);
		}
	}
}
