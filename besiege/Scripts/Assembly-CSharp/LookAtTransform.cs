using UnityEngine;

public class LookAtTransform : MonoBehaviour
{
	public Transform target;

	public Transform upTransform;

	private void LateUpdate()
	{
		if (!(target == null))
		{
			base.transform.LookAt(target, upTransform.up);
		}
	}
}
