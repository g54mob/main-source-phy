using UnityEngine;

public class PlaysoundOnCollision : BreakBase
{
	public RandomSoundController rsc;

	public float minForce = 1000f;

	protected override void Start()
	{
		if (rsc == null)
		{
			rsc = GetComponent<RandomSoundController>();
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.relativeVelocity.sqrMagnitude > minForce)
		{
			rsc.Play();
		}
	}
}
