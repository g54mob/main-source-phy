using UnityEngine;

public class LerpFollowParent : MonoBehaviour
{
	public float targetSpeed = 1f;

	public float timeSpeed = 5f;

	private Vector3 velocity;

	private Transform myTarget;

	public void Go(GameObject target)
	{
		myTarget = target.transform;
		base.transform.SetParent(target.transform.parent.parent);
	}

	private void Update()
	{
		if (!myTarget)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		velocity = Vector3.Lerp(velocity, (myTarget.position - base.transform.position) * targetSpeed, timeSpeed * Time.deltaTime);
		base.transform.position += 10f * Time.deltaTime * velocity;
	}
}
