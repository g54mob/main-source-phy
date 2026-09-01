using System.Collections;
using UnityEngine;

public class PhysicsFollow : MonoBehaviour
{
	public Transform target;

	public float followPower = 100f;

	public bool normalise;

	private float actualFollowPower = 100f;

	private Transform myTransform;

	private Rigidbody myRigidbody;

	private void Start()
	{
		myTransform = base.transform;
		myRigidbody = GetComponent<Rigidbody>();
		actualFollowPower = followPower;
	}

	private void FixedUpdate()
	{
		if (!normalise)
		{
			myRigidbody.AddForce((target.position - myTransform.position) * actualFollowPower);
		}
		else
		{
			myRigidbody.AddForce((target.position - myTransform.position).normalized * actualFollowPower);
		}
	}

	private IEnumerator LerpPowerIn(float speedy)
	{
		float cTime = 0f;
		float rate = 1f / speedy;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			actualFollowPower = Mathf.Lerp(0f, followPower, cTime);
			yield return null;
		}
	}
}
