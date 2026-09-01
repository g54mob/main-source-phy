using UnityEngine;

public class Lerp : MonoBehaviour
{
	public Transform target;

	public float speed = 2f;

	public bool ignoreX;

	public bool ignoreY;

	public bool ignoreZ;

	private void LateUpdate()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(ignoreX ? base.transform.position.x : target.position.x, ignoreY ? base.transform.position.y : target.position.y, ignoreZ ? base.transform.position.z : target.position.z), Time.deltaTime * speed);
	}
}
