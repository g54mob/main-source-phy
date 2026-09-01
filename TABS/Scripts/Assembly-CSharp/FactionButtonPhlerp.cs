using UnityEngine;

public class FactionButtonPhlerp : MonoBehaviour
{
	public float Spring = 1f;

	public float Drag = 1f;

	private Vector3 velocity;

	private Transform target;

	private void Update()
	{
		float num = Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.02f);
		if ((bool)target)
		{
			Vector3 b = (target.position - base.transform.position) * Spring;
			velocity = Vector3.Lerp(velocity, b, Drag * num);
			base.transform.position += velocity * num;
		}
	}

	public void AddForce(Vector3 force)
	{
		velocity += force;
	}

	public void SetTarget(Transform target)
	{
		this.target = target;
	}
}
