using UnityEngine;

public class FollowLocalY : MonoBehaviour
{
	public Transform target;

	private void LateUpdate()
	{
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, target.localEulerAngles.y, base.transform.localEulerAngles.z);
	}
}
