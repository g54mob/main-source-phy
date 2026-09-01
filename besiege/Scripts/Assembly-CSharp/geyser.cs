using UnityEngine;

public class geyser : MonoBehaviour
{
	public float timeLow;

	public float timeHigh;

	public ParticleSystem particleEffect;

	public float timer;

	public float geyserPower;

	private void Start()
	{
		timer = Random.Range(timeLow, timeHigh);
	}

	private void Update()
	{
		if (StatMaster.levelSimulating && !particleEffect.isPlaying)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				Geyser();
			}
		}
	}

	private void Geyser()
	{
		timer = Random.Range(timeLow, timeHigh);
		particleEffect.Play();
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Something entered the trigger");
		if (particleEffect.isPlaying)
		{
			other.attachedRigidbody.AddExplosionForce(geyserPower, base.transform.position, 200f, geyserPower, ForceMode.Force);
		}
	}
}
