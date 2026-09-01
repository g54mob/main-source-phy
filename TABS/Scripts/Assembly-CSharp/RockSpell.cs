using System.Collections;
using UnityEngine;

public class RockSpell : TargetableEffect
{
	public AnimationCurve upCurve;

	public AnimationCurve forceCurve;

	public float force;

	private float rockStartY;

	private Rigidbody rig;

	private Vector3 direction;

	public float prediction = 1f;

	private Rigidbody target;

	private IEnumerator Go()
	{
		float t = upCurve.keys[upCurve.keys.Length - 1].time;
		float c = 0f;
		while (c < t)
		{
			c += Time.deltaTime;
			rig.transform.localPosition = new Vector3(0f, upCurve.Evaluate(c) + rockStartY, 0f);
			yield return null;
		}
		rig.isKinematic = false;
		t = forceCurve.keys[forceCurve.keys.Length - 1].time;
		c = 0f;
		GetDirection(rig.position, target.position, target);
		rig.useGravity = false;
		while (c < t)
		{
			c += Time.deltaTime;
			rig.velocity = direction * forceCurve.Evaluate(c) * force;
			yield return null;
		}
		rig.useGravity = true;
	}

	public override void DoEffect(Transform startPoint, Transform endPoint)
	{
	}

	public override void DoEffect(Vector3 startPoint, Vector3 endPoint, Rigidbody targetRig = null)
	{
		target = targetRig;
		rig = GetComponentInChildren<Rigidbody>();
		rockStartY = rig.transform.localPosition.y;
		StartCoroutine(Go());
	}

	private void GetDirection(Vector3 startPoint, Vector3 endPoint, Rigidbody targetRig)
	{
		Vector3 vector = endPoint;
		vector += targetRig.velocity * prediction * 0.1f * Vector3.Distance(startPoint, vector);
		direction = (vector - startPoint).normalized;
	}
}
