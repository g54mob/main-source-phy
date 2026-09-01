using System.Collections;
using UnityEngine;

public class Throwing : MonoBehaviour
{
	public AnimationCurve throwCurve;

	public float curveForce = 100f;

	public float throwForce = 100f;

	private HoldingHandler holding;

	private bool isThrowing;

	private void Start()
	{
		holding = GetComponent<HoldingHandler>();
	}

	public void Throw(Vector3 target)
	{
		if ((bool)holding.rightObject && !(holding.rightObject.rig.mass > 500f) && !isThrowing)
		{
			StartCoroutine(DoThrow(target));
		}
	}

	private IEnumerator DoThrow(Vector3 target)
	{
		isThrowing = true;
		Rigidbody rig = holding.rightObject.rig;
		rig.GetComponentInChildren<Collider>().enabled = false;
		float t = throwCurve.keys[throwCurve.keys.Length - 1].time;
		float counter = 0f;
		while (counter < t)
		{
			rig.AddForce(curveForce * throwCurve.Evaluate(counter) * Time.deltaTime * (target - rig.position).normalized, ForceMode.Acceleration);
			counter += Time.deltaTime;
			yield return null;
		}
		holding.LetGoOfAll();
		rig.velocity = Vector3.zero;
		yield return null;
		rig.velocity = ((target - rig.position).normalized + 0.024f * Vector3.Distance(target, rig.position) * Vector3.up) * throwForce;
		isThrowing = false;
		yield return new WaitForSeconds(0.15f);
		rig.GetComponentInChildren<Collider>().enabled = true;
	}
}
