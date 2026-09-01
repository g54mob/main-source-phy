using UnityEngine;

public class CurveRotator : MonoBehaviour
{
	public AnimationCurve rotationCurve = AnimationCurve.Constant(0f, 0f, 0f);

	public float AmplitudeFactor = 1f;

	public float timeFactor = 1f;

	private float timer;

	private void Update()
	{
		timer += Time.unscaledDeltaTime * timeFactor;
		if (timer >= rotationCurve[rotationCurve.length - 1].time)
		{
			timer = 0f;
		}
		base.transform.localRotation = Quaternion.Euler(0f, 0f, rotationCurve.Evaluate(timer) * AmplitudeFactor);
	}
}
