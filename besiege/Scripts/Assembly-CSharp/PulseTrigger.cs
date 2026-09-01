using UnityEngine;

public class PulseTrigger : MonoBehaviour
{
	public float PulseTimer = 2f;

	private bool CanShoot = true;

	public bool randomStartTime;

	private void Start()
	{
		if (randomStartTime)
		{
			InvokeRepeating("Pulse", Random.Range(0f, PulseTimer), PulseTimer);
		}
		else
		{
			InvokeRepeating("Pulse", 1f, PulseTimer);
		}
	}

	public void Pulse()
	{
		GetComponent<Rigidbody>().WakeUp();
		if (CanShoot)
		{
			GetComponent<Collider>().enabled = false;
			CanShoot = false;
		}
		else
		{
			GetComponent<Collider>().enabled = true;
			CanShoot = true;
		}
		GetComponent<Rigidbody>().WakeUp();
	}
}
