using System.Collections;
using UnityEngine;

public class BirdSimpleAI : MonoBehaviour
{
	public float velocityCutOff;

	public Rigidbody myRigidbody;

	public bool flyingAway;

	public float flyingSpeed;

	public float upSpeed = 10f;

	public float damping = 0.8f;

	public float flyingDuration = 0.8f;

	private Vector3 directionToObj;

	private void Start()
	{
		myRigidbody.Sleep();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!flyingAway && (bool)other.attachedRigidbody && other.attachedRigidbody.velocity.sqrMagnitude > velocityCutOff)
		{
			directionToObj = (other.transform.position - base.transform.position).normalized * -1f;
			StartCoroutine(FlyAway());
		}
	}

	private void FixedUpdate()
	{
		if (flyingAway)
		{
			myRigidbody.AddForce(flyingSpeed * directionToObj + Vector3.up * upSpeed);
		}
	}

	private IEnumerator FlyAway()
	{
		flyingAway = true;
		yield return new WaitForSeconds(flyingDuration);
		flyingAway = false;
	}
}
