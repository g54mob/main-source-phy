using System.Collections;
using UnityEngine;

public class AddScreensShake : MonoBehaviour
{
	public enum ShakeDirection
	{
		forward = 0,
		up = 1
	}

	public float amount = 1f;

	public float randomFactor = 0.3f;

	public AnimationCurve shakeOverTimeCurve;

	public ShakeDirection shakeDirection;

	public bool playOnAwake = true;

	private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

	private void Start()
	{
		if (playOnAwake)
		{
			DoShake();
		}
	}

	public void DoShake()
	{
		if (shakeOverTimeCurve.length > 1)
		{
			StartCoroutine(ShakeOverTime());
		}
		else
		{
			Shake();
		}
	}

	private IEnumerator ShakeOverTime()
	{
		float t = 0f;
		float shakeTime = shakeOverTimeCurve[shakeOverTimeCurve.length - 1].time;
		while (t < shakeTime)
		{
			t += Time.fixedDeltaTime;
			Shake(shakeOverTimeCurve.Evaluate(t) * Time.fixedDeltaTime * 60f);
			yield return waitForFixedUpdate;
		}
	}

	private void Shake(float multiplier = 1f)
	{
		Vector3 a = base.transform.forward;
		if (shakeDirection == ShakeDirection.up)
		{
			a = Vector3.up;
		}
		a = Vector3.Lerp(a, Random.onUnitSphere, randomFactor);
		if ((bool)ScreenShake.Instance)
		{
			ScreenShake.Instance.AddForce(amount * multiplier * a, base.transform.position);
		}
	}
}
