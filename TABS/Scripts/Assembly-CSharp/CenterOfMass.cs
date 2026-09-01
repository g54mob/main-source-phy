using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
	private void Start()
	{
		GetComponentInParent<Rigidbody>().centerOfMass = base.transform.localPosition;
	}
}
