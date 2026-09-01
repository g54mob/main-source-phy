using System.Collections;
using UnityEngine;

public class ForceLerp : MonoBehaviour
{
	public Vector3 targetPosition = Vector3.one;

	private Vector3 velocity;

	public float spring = 1.5f;

	public float drag = 0.75f;

	public float delayWobbleBy;

	private void Update()
	{
		float num = Mathf.Clamp(Time.deltaTime, 0f, 0.05f);
		velocity += 20f * num * spring * (targetPosition - base.transform.position);
		base.transform.position += 2f * num * velocity;
	}

	private void FixedUpdate()
	{
		velocity *= drag;
	}

	public void AddForce(float force)
	{
		if (delayWobbleBy == 0f)
		{
			velocity += 2f * force * Vector3.one;
		}
		else
		{
			StartCoroutine(DelayWobble(force));
		}
	}

	private IEnumerator DelayWobble(float force)
	{
		yield return new WaitForSeconds(delayWobbleBy);
		velocity += 2f * force * Vector3.one;
	}
}
