using UnityEngine;

public class PhysicsFaker : MonoBehaviour
{
	private Vector3 m_startPos;

	private Vector3 m_velocity;

	[Header("--Force--")]
	public float m_spring = 1f;

	public float m_drag = 0.8f;

	[Header("--Movement Cap--")]
	public Vector3 max;

	public Vector3 min;

	private Vector3 m_startRotForward;

	private Vector3 m_startRotUp;

	private Quaternion m_startRot;

	private Vector3 m_torqueVel;

	[Header("--Torque--")]
	public float m_torqueSpring = 2f;

	public float m_torqueDrag = 0.7f;

	[Header("--Rotation Cap--")]
	public Vector3 maxRot;

	public Vector3 minRot;

	private void Awake()
	{
		m_startPos = base.transform.localPosition;
		Vector3 direction = base.transform.TransformDirection(Vector3.forward);
		m_startRotForward = base.transform.parent.InverseTransformDirection(direction);
		Vector3 direction2 = base.transform.TransformDirection(Vector3.up);
		m_startRotUp = base.transform.parent.InverseTransformDirection(direction2);
	}

	public void AddForceLocal(Vector3 force)
	{
		m_velocity += force;
	}

	public void AddTorqueLocal(Vector3 torque)
	{
		m_torqueVel += torque;
	}

	private void VelocityUpdate()
	{
		m_velocity += (m_startPos - base.transform.localPosition) * Time.deltaTime * 50f * m_spring;
		base.transform.localPosition += m_velocity * Time.deltaTime;
	}

	private void TorqueUpdate()
	{
		Vector3 direction = base.transform.parent.TransformDirection(m_startRotForward);
		direction = base.transform.InverseTransformDirection(direction);
		Vector3 vector = Vector3.Cross(Vector3.forward, direction).normalized * Vector3.Angle(Vector3.forward, direction);
		Vector3 direction2 = base.transform.parent.TransformDirection(m_startRotUp);
		direction2 = base.transform.InverseTransformDirection(direction2);
		Vector3 vector2 = Vector3.Cross(Vector3.up, direction2).normalized * Vector3.Angle(Vector3.up, direction2);
		m_torqueVel += (vector + vector2) * m_torqueSpring * Time.deltaTime;
	}

	private void Update()
	{
		VelocityUpdate();
		TorqueUpdate();
	}

	private void FixedUpdate()
	{
		m_velocity *= m_drag;
		m_torqueVel *= m_torqueDrag;
	}

	private void LateUpdate()
	{
		base.transform.Rotate(m_torqueVel * Time.deltaTime * 100f, Space.Self);
		base.transform.localPosition = new Vector3((min.x == 0f && max.x == 0f) ? base.transform.localPosition.x : Mathf.Clamp(base.transform.localPosition.x, min.x, max.x), (min.y == 0f && max.y == 0f) ? base.transform.localPosition.y : Mathf.Clamp(base.transform.localPosition.y, min.y, max.y), (min.z == 0f && max.z == 0f) ? base.transform.localPosition.z : Mathf.Clamp(base.transform.localPosition.z, min.z, max.z));
		base.transform.localEulerAngles = new Vector3((minRot.x == 0f && maxRot.x == 0f) ? base.transform.localEulerAngles.x : Mathf.Clamp(base.transform.localEulerAngles.x, minRot.x, maxRot.x), (minRot.y == 0f && maxRot.y == 0f) ? base.transform.localEulerAngles.y : Mathf.Clamp(base.transform.localEulerAngles.y, minRot.y, maxRot.y), (minRot.z == 0f && maxRot.z == 0f) ? base.transform.localEulerAngles.z : Mathf.Clamp(base.transform.localEulerAngles.z, minRot.z, maxRot.z));
	}
}
