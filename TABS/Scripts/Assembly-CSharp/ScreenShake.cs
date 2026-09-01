using UnityEngine;

public class ScreenShake : MonoBehaviour
{
	public float multiplier = 1f;

	private Vector3 velocity;

	private Vector3 upVelocity;

	private float drag = 1f;

	public float spring = 1f;

	public static ScreenShake Instance;

	public AnimationCurve dropOffCurve;

	private float shakeMultiplier = 1f;

	private float m_rotationMultiplier = 1f;

	public float RotationMultiplier
	{
		get
		{
			return m_rotationMultiplier;
		}
		set
		{
			m_rotationMultiplier = value;
		}
	}

	private void Start()
	{
		Instance = this;
	}

	private void FixedUpdate()
	{
		float fixedDeltaTime = Time.fixedDeltaTime;
		shakeMultiplier = Mathf.Lerp(shakeMultiplier, 0.5f, fixedDeltaTime * 5f);
		velocity += Vector3.Angle(base.transform.forward, base.transform.parent.forward) * fixedDeltaTime * 50f * Vector3.Cross(base.transform.forward, base.transform.parent.forward).normalized * spring;
		velocity -= drag * velocity * 20f * fixedDeltaTime;
		upVelocity += Vector3.Angle(base.transform.up, base.transform.parent.up) * fixedDeltaTime * 50f * Vector3.Cross(base.transform.up, base.transform.parent.up).normalized * spring;
		upVelocity -= drag * upVelocity * 20f * fixedDeltaTime;
	}

	private void Update()
	{
		float num = Mathf.Clamp(Time.deltaTime, 0f, 0.03f);
		base.transform.Rotate(velocity * m_rotationMultiplier * 10f * num, Space.World);
		base.transform.Rotate(upVelocity * m_rotationMultiplier * 10f * num, Space.World);
	}

	public void AddForce(Vector3 force, Vector3 position)
	{
		if (!(this == null) && !(base.gameObject == null) && !(Instance == null))
		{
			force *= multiplier;
			force = (force.normalized + Random.insideUnitSphere * 0.3f).normalized * force.magnitude;
			force = Vector3.Cross(force, base.transform.position - position).normalized * force.magnitude;
			Vector3 vector = force * 10f * shakeMultiplier * dropOffCurve.Evaluate(Vector3.Distance(base.transform.position, position));
			shakeMultiplier -= vector.magnitude * 0.05f;
			shakeMultiplier = Mathf.Clamp(shakeMultiplier, 0.01f, 1f);
			velocity += vector;
		}
	}
}
