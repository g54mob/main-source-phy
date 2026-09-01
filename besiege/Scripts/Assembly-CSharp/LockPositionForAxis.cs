using UnityEngine;

public class LockPositionForAxis : MonoBehaviour
{
	public bool x;

	public bool y;

	public bool z;

	protected Vector3 start;

	protected Transform t;

	private void Start()
	{
		t = base.transform;
		start = t.position;
	}

	private void LateUpdate()
	{
		t.position = new Vector3((!x) ? t.position.x : start.x, (!y) ? t.position.y : start.y, (!z) ? t.position.z : start.z);
	}
}
