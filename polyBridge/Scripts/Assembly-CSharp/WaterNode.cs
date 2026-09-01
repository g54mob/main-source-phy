using UnityEngine;

public class WaterNode
{
	public Vector2 origin = Vector2.zero;

	public bool sleeping = true;

	private float force;

	private float accumulator;

	private readonly float rippleSpeed = 9.5f;

	private readonly float rippleDamping = 0.25f;

	public void ApplyForce(float amount = 1f, bool firstTime = false)
	{
		if (Mathf.Approximately(force, 0f))
		{
			accumulator = 0f;
		}
		force += amount;
		force = Mathf.Clamp(force, 0f, 0.85f);
		sleeping = false;
	}

	public float GetPointHeight()
	{
		return Mathf.Sin(accumulator) * force;
	}

	public void UpdateManual(float dt)
	{
		accumulator += dt * rippleSpeed;
		force -= dt * rippleDamping;
		if (force <= 0f)
		{
			force = 0f;
			sleeping = true;
		}
	}

	public void ClearForce()
	{
		force = 0f;
		sleeping = true;
	}
}
