using UnityEngine;

public class LookAt : MonoBehaviour
{
	public Transform target;

	private void Update()
	{
		if ((bool)target)
		{
			base.transform.LookAt(target);
		}
		base.transform.rotation = Quaternion.LookRotation(new Vector3(base.transform.forward.x, 0f, base.transform.forward.z));
	}
}
