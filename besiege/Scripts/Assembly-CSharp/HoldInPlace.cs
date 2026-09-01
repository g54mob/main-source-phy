using UnityEngine;

public class HoldInPlace : MonoBehaviour
{
	[HideInInspector]
	public bool OnFloor;

	[HideInInspector]
	public Vector3 lockedPos;

	private Machine machine;

	private void Start()
	{
		machine = GetComponentInParent<Machine>();
	}

	private void Update()
	{
		if (OnFloor && !machine.isSimulating)
		{
			Object.Destroy(base.transform.parent.GetComponent<MyBounds>());
			base.transform.parent.position = new Vector3(base.transform.parent.position.x, lockedPos.y, base.transform.parent.position.z);
		}
		if (machine.isSimulating)
		{
			Object.Destroy(this);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 29)
		{
			OnFloor = true;
			lockedPos = base.transform.parent.position;
		}
	}
}
