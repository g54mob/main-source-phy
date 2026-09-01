using UnityEngine;

public class PositionSpring : MonoBehaviour
{
	public Transform target;

	public float spring;

	public float damper;

	private Vector3 velocity;

	private Vector3 pos;

	private float startDistance;

	private Vector3 targetPos;

	private float currentDistance;

	private float cappedTime;

	private void Start()
	{
		base.transform.LookAt(target.position);
		pos = target.InverseTransformPoint(base.transform.position);
		startDistance = Vector3.Distance(base.transform.position, target.position);
	}

	private void Update()
	{
		cappedTime = Mathf.Clamp(Time.deltaTime, 0f, 0.02f);
		targetPos = target.TransformPoint(pos);
		currentDistance = Vector3.Distance(base.transform.position, targetPos);
		if (currentDistance > startDistance * 1.5f)
		{
			base.transform.position = targetPos + (base.transform.position - targetPos).normalized * startDistance * 1.5f;
		}
		velocity += (targetPos - base.transform.position) * cappedTime * spring;
		velocity += -velocity * cappedTime * damper;
		base.transform.position += velocity * cappedTime;
		base.transform.rotation = Quaternion.LookRotation((target.position - base.transform.position).normalized + Vector3.up);
	}
}
