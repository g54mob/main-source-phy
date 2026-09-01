using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionWave : SimBehaviour
{
	public bool push = true;

	public bool corrupt = true;

	public float pushForce = 10000f;

	public float upwardsForce = 1f;

	public float endScale = 150f;

	public float duration = 2f;

	public bool damageBlocks;

	public AudioSource explosionAudio;

	public SphereCollider col;

	private HashSet<Rigidbody> pushedBodies = new HashSet<Rigidbody>();

	public void Animate()
	{
		if (base.isSimulating)
		{
			base.gameObject.SetActive(true);
			StartCoroutine(Animation());
		}
	}

	protected IEnumerator Animation()
	{
		Vector3 start = base.transform.localScale;
		Vector3 end = start * endScale;
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float pct = t / duration;
			base.transform.localScale = Vector3.Lerp(start, end, pct);
			yield return null;
		}
		base.transform.localScale = start;
	}

	protected void OnTriggerEnter(Collider other)
	{
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if (!attachedRigidbody || pushedBodies.Contains(attachedRigidbody))
		{
			return;
		}
		pushedBodies.Add(attachedRigidbody);
		if (push)
		{
			SetDynamicOnImpact componentInParent = attachedRigidbody.GetComponentInParent<SetDynamicOnImpact>();
			if ((bool)componentInParent)
			{
				componentInParent.Release();
			}
			attachedRigidbody.AddExplosionForce(pushForce, base.transform.position, base.transform.lossyScale.x * col.radius, upwardsForce, ForceMode.Impulse);
		}
		if (!corrupt)
		{
			return;
		}
		BlockHealthBar component = attachedRigidbody.GetComponent<BlockHealthBar>();
		if (!component)
		{
			return;
		}
		if (damageBlocks)
		{
			component.DamageBlock(4f);
			return;
		}
		BlockVisualController component2 = attachedRigidbody.GetComponent<BlockVisualController>();
		if ((bool)component2)
		{
			component2.SetDamageLevel(1f);
		}
	}
}
