using UnityEngine;

public class mySmoothFollow1 : MonoBehaviour
{
	public Transform target1;

	public Transform target2;

	public float smoothAmount;

	private void LateUpdate()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, (target1.position + target2.position) / 2f, Time.deltaTime * smoothAmount);
	}
}
