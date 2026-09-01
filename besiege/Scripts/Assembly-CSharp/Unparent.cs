using UnityEngine;

public class Unparent : MonoBehaviour
{
	public Transform target;

	private void Start()
	{
		if (target == null)
		{
			target = base.transform.parent.parent;
		}
		base.transform.parent = target;
	}
}
