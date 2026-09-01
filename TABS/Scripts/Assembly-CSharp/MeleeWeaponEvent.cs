using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeaponEvent : CollisionWeaponEffect
{
	public float delay;

	public UnityEvent collisionEvent;

	public bool triggerOnlyOnce = true;

	public float cd;

	private float counter = float.PositiveInfinity;

	private bool done;

	private void Start()
	{
	}

	private void Update()
	{
		counter += Time.deltaTime;
	}

	public override void DoEffect(Transform hitTransform, Collision collision)
	{
		if (!(counter < cd) && !done)
		{
			StartCoroutine(DoEvent(hitTransform, collision));
		}
	}

	private IEnumerator DoEvent(Transform hitTransform, Collision collision)
	{
		yield return new WaitForSeconds(delay);
		collisionEvent.Invoke();
		if (triggerOnlyOnce)
		{
			base.enabled = false;
			done = true;
		}
		counter = 0f;
	}
}
