using UnityEngine;

public class KeepOffsetFromTarget : MonoBehaviour
{
	public Transform target;

	public bool autoInitializeOffset;

	public Vector3 offsetFromTarget;

	private void Start()
	{
		if ((bool)target && autoInitializeOffset)
		{
			offsetFromTarget = base.transform.position - target.position;
		}
	}

	private void LateUpdate()
	{
		if ((bool)target)
		{
			base.transform.position = target.position + offsetFromTarget;
		}
	}
}
