using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
[AddComponentMenu("Character/Platform Input Controller")]
public class PlatformInputController : MonoBehaviour
{
	public bool autoRotate = true;

	private float maxRotationSpeed = 360f;

	private CharacterMotor motor;

	private void Awake()
	{
		motor = GetComponent<CharacterMotor>();
	}

	private void Update()
	{
		Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
		if (vector != Vector3.zero)
		{
			float magnitude = vector.magnitude;
			vector /= magnitude;
			magnitude = Mathf.Min(1f, magnitude);
			magnitude *= magnitude;
			vector *= magnitude;
		}
		vector = Camera.main.transform.rotation * vector;
		Quaternion quaternion = Quaternion.FromToRotation(-Camera.main.transform.forward, base.transform.up);
		vector = quaternion * vector;
		motor.inputMoveDirection = vector;
		motor.inputJump = Input.GetButton("Jump");
		if (autoRotate && vector.sqrMagnitude > 0.01f)
		{
			Vector3 v = ConstantSlerp(base.transform.forward, vector, maxRotationSpeed * Time.deltaTime);
			v = ProjectOntoPlane(v, base.transform.up);
			base.transform.rotation = Quaternion.LookRotation(v, base.transform.up);
		}
	}

	private Vector3 ProjectOntoPlane(Vector3 v, Vector3 normal)
	{
		return v - Vector3.Project(v, normal);
	}

	private Vector3 ConstantSlerp(Vector3 from, Vector3 to, float angle)
	{
		float t = Mathf.Min(1f, angle / Vector3.Angle(from, to));
		return Vector3.Slerp(from, to, t);
	}
}
