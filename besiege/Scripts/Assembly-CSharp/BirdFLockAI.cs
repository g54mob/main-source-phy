using System.Collections;
using UnityEngine;

public class BirdFLockAI : MonoBehaviour
{
	public float velocityCutOff;

	public Rigidbody myRigidbody;

	public bool flyingAway;

	public float flyingSpeed;

	public float upSpeed = 10f;

	public float damping = 0.8f;

	public float flyingDuration = 0.8f;

	public Transform targetObj;

	private Vector3 directionToObj;

	private void Start()
	{
		myRigidbody.Sleep();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!flyingAway && (bool)other.attachedRigidbody && other.attachedRigidbody.velocity.sqrMagnitude > velocityCutOff)
		{
			StartCoroutine(FlyAway());
		}
	}

	private void Update()
	{
		directionToObj = myRigidbody.position - targetObj.position;
	}

	private void FixedUpdate()
	{
		if (flyingAway)
		{
			myRigidbody.AddForce(flyingSpeed * directionToObj);
		}
	}

	private IEnumerator FlyAway()
	{
		flyingAway = true;
		yield return new WaitForSeconds(flyingDuration);
		flyingAway = false;
	}
}
