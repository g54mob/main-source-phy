using System.Collections;
using UnityEngine;

public class RotationShake : MonoBehaviour
{
	public float multiplier = 1f;

	private Vector3 velocity;

	private Vector3 upVelocity;

	public float drag = 1f;

	public float spring = 1f;

	private float m_rotationMultiplier = 1f;

	public bool useTimeScale = true;

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

	private void Update()
	{
		float num = Mathf.Clamp(Time.deltaTime, 0f, 0.02f);
		if (!useTimeScale)
		{
			num = Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.02f);
		}
		velocity += 50f * num * spring * Vector3.Angle(base.transform.forward, base.transform.parent.forward) * Vector3.Cross(base.transform.forward, base.transform.parent.forward).normalized;
		velocity -= 20f * num * drag * velocity;
		upVelocity += 50f * num * spring * Vector3.Angle(base.transform.up, base.transform.parent.up) * Vector3.Cross(base.transform.up, base.transform.parent.up).normalized;
		upVelocity -= 20f * num * drag * upVelocity;
		base.transform.Rotate(num * m_rotationMultiplier * 10f * velocity, Space.World);
		base.transform.Rotate(num * 10f * m_rotationMultiplier * upVelocity, Space.World);
	}

	public void AddForce(Vector3 force, Vector3 position, float range = float.PositiveInfinity)
	{
		float num = 1f;
		if (range != 0f && range != float.PositiveInfinity)
		{
			num = CalculateRangeMultiplier(position, range);
		}
		force *= multiplier * num * 10f;
		force = Vector3.Cross(force, base.transform.position - position).normalized * force.magnitude;
		velocity += force;
	}

	public void AddForce(Vector3 force)
	{
		force *= multiplier;
		velocity += force * 10f;
	}

	public void ShakeOverTime(Vector3 force, Vector3 position, float time, float range = float.MaxValue)
	{
		StartCoroutine(DoShakeOverTime(force, position, time, range));
	}

	private IEnumerator DoShakeOverTime(Vector3 force, Vector3 position, float time, float range = float.MaxValue)
	{
		float startTime = time;
		float rangeM = CalculateRangeMultiplier(position, range);
		while (time > 0f)
		{
			AddForce(force.magnitude * 0.3f * (time / startTime) * rangeM * (force.normalized + Random.onUnitSphere).normalized, position);
			time = (useTimeScale ? (time - Time.unscaledDeltaTime) : (time - Time.deltaTime));
			yield return null;
		}
	}

	private float CalculateRangeMultiplier(Vector3 position, float range)
	{
		return Mathf.Clamp((range - Vector3.Distance(base.transform.position, position)) / range, 0f, 1f);
	}
}
