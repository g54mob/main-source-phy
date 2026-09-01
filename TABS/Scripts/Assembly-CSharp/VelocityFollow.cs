using UnityEngine;

public class VelocityFollow : MonoBehaviour
{
	public Transform target;

	public float spring = 1f;

	public float drag = 1f;

	public float movementHelp;

	private Vector3 velocity;

	private Vector3 deltaPos;

	private Vector3 lastPos;

	private void Start()
	{
		base.transform.position = target.position;
		lastPos = target.position;
	}

	private void Update()
	{
		deltaPos = target.position - lastPos;
		lastPos = target.position;
		base.transform.position += deltaPos * movementHelp;
		float num = Mathf.Clamp(Time.deltaTime, 0f, 0.05f / drag);
		velocity += 500f * num * spring * (target.position - base.transform.position);
		velocity -= 20f * num * velocity;
		base.transform.position += Time.deltaTime * velocity;
	}
}
