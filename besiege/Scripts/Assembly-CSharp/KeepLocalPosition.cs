using UnityEngine;

public class KeepLocalPosition : MonoBehaviour
{
	protected Vector3 localPos;

	private void Awake()
	{
		localPos = base.transform.localPosition;
	}

	private void LateUpdate()
	{
		base.transform.localPosition = localPos;
	}
}
