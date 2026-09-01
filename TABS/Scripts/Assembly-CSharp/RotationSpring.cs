using UnityEngine;

public class RotationSpring : MonoBehaviour
{
	public float drag = 0.8f;

	public float spring = 1f;

	public float inheritedMovement = 1f;

	public float gravity = 1f;

	[Space(25f)]
	public Transform connectedTo;

	public Transform centerOfMass;

	private Vector3 rot = new Vector3(0f, 0f, 1f);

	private Vector3 pos;

	private Vector3 upRot = new Vector3(0f, 1f, 0f);

	[Space(25f)]
	private Vector3 velocity;

	private Vector3 lastPosition;

	[HideInInspector]
	public bool stuck;

	private Vector3 deltaPos;

	private void Start()
	{
		upRot = connectedTo.InverseTransformDirection(base.transform.up);
		rot = connectedTo.InverseTransformDirection(base.transform.forward);
		pos = connectedTo.InverseTransformPoint(base.transform.position);
		lastPosition = connectedTo.TransformPoint(pos);
	}

	private void Update()
	{
		base.transform.position = connectedTo.TransformPoint(pos);
		velocity += Vector3.Angle(connectedTo.TransformDirection(rot), base.transform.forward) * -Vector3.Cross(connectedTo.TransformDirection(rot), base.transform.forward).normalized * spring * 0.5f * Time.deltaTime * 60f;
		velocity += Vector3.Angle(Vector3.down, base.transform.forward) * -Vector3.Cross(Vector3.down, base.transform.forward).normalized * gravity * Time.deltaTime * 60f;
		velocity += Vector3.Angle(connectedTo.TransformDirection(upRot), base.transform.up) * -Vector3.Cross(connectedTo.TransformDirection(upRot), base.transform.up).normalized * spring * 0.5f * Time.deltaTime * 60f;
		velocity *= drag;
		base.transform.Rotate(velocity * 10f * Time.deltaTime, Space.World);
		Vector3 vector = connectedTo.TransformPoint(pos);
		deltaPos = vector - lastPosition;
		if (deltaPos != Vector3.zero)
		{
			Vector3 vector2 = Vector3.Cross(deltaPos, centerOfMass.position - base.transform.position).normalized * deltaPos.magnitude;
			velocity += vector2 * Time.deltaTime * 10000f * inheritedMovement * 5f;
		}
		lastPosition = vector;
	}
}
