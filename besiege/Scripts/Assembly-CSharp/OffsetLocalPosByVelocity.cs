using UnityEngine;

[AddComponentMenu("Water/Objects/OffsetLocalPosByVelocity")]
public class OffsetLocalPosByVelocity : MonoBehaviour
{
	public Rigidbody body;

	public Vector3 axis = Vector3.forward;

	private Vector3 startPos;

	private void Awake()
	{
		startPos = base.transform.localPosition;
	}

	private void Update()
	{
		Vector3 vector = Vector3.Scale(base.transform.parent.InverseTransformDirection((!body) ? Vector3.zero : body.velocity), axis);
		base.transform.localPosition = startPos + vector;
	}
}
