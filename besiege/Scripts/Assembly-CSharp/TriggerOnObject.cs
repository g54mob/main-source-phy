using System.Collections;
using UnityEngine;

public class TriggerOnObject : MonoBehaviour
{
	public Rigidbody target;

	public GameObject[] score = new GameObject[0];

	private int index = -1;

	private Vector3 startPos = Vector3.zero;

	private Vector3 startScale = Vector3.one;

	private Quaternion startRot = Quaternion.identity;

	private bool animating;

	private void Start()
	{
		startPos = target.transform.position;
		startRot = target.transform.rotation;
		startScale = target.transform.localScale;
	}

	public void OnTriggerEnter(Collider coll)
	{
		if ((bool)coll.attachedRigidbody && !animating && coll.attachedRigidbody == target)
		{
			index++;
			if (index < score.Length)
			{
				score[index].SetActive(true);
			}
			StartCoroutine(AnimateReset(1f));
		}
	}

	private IEnumerator AnimateReset(float duration)
	{
		animating = true;
		Vector3 s = target.transform.localScale;
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float pct = t / duration;
			target.transform.localScale = Vector3.Lerp(s, Vector3.zero, pct);
			yield return null;
		}
		target.isKinematic = true;
		target.transform.localScale = Vector3.zero;
		GlobalParticles.EmitParticleBursts(12, target.transform.position);
		yield return null;
		target.transform.position = startPos;
		target.transform.rotation = startRot;
		for (float t2 = 0f; t2 < duration; t2 += Time.deltaTime)
		{
			float pct2 = t2 / duration;
			target.transform.localScale = Vector3.Lerp(Vector3.zero, startScale, pct2);
			yield return null;
		}
		target.transform.localScale = startScale;
		target.isKinematic = false;
		Rigidbody rigidbody = target;
		Vector3 zero = Vector3.zero;
		target.angularVelocity = zero;
		rigidbody.velocity = zero;
		animating = false;
	}

	private void OnDisable()
	{
		animating = false;
	}
}
