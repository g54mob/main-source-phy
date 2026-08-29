using UnityEngine;

public class VRJoystickTest : MonoBehaviour
{
	public Transform vrHand;

	public Vector3 rotationAxis;

	public bool is2D;

	public bool invertDirection1D;

	public float angleLimit = 45f;

	private Vector3 interactionStartVector;

	private void Update()
	{
		base.transform.localRotation = Quaternion.identity;
		Vector3 vector = base.transform.TransformVector(rotationAxis);
		Vector3 position = base.transform.position;
		Vector3 position2 = vrHand.position;
		if (is2D)
		{
			Vector3 vector2 = UnityUtils.limitVectorToCone(position2 - position, vector, angleLimit);
			base.transform.localRotation = Quaternion.FromToRotation(rotationAxis, base.transform.InverseTransformVector(vector2));
			Debug.DrawLine(position, vector2 - position, Color.blue);
			Debug.DrawLine(position, position + vector, Color.cyan);
			return;
		}
		Vector3 vector3 = new Plane(vector, position).ClosestPointOnPlane(position2);
		Vector3 to = vector3 - position;
		UnityUtils.drawSphereAt(vector3);
		if (Input.GetKeyDown(KeyCode.I))
		{
			interactionStartVector = to;
		}
		Debug.DrawLine(position, position + interactionStartVector, Color.green);
		Debug.DrawLine(position, vector3, Color.blue);
		float value = Vector3.SignedAngle(interactionStartVector, to, rotationAxis);
		value = Mathf.Clamp(value, 0f - angleLimit, angleLimit);
		base.transform.localRotation = Quaternion.AngleAxis(value, invertDirection1D ? (-rotationAxis) : rotationAxis);
	}
}
