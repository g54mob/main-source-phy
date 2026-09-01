using UnityEngine;

public class CurveScaler : MonoBehaviour
{
	public AnimationCurve scaleCurve = AnimationCurve.Constant(0f, 0f, 0f);

	public float AmplitudeFactor = 1f;

	public float timeFactor = 1f;

	private float timer;

	private int scaleCurveIndex;

	private void Update()
	{
		timer += Time.unscaledDeltaTime * timeFactor;
		scaleCurveIndex = scaleCurve.length - 1;
		if (scaleCurveIndex >= 0 && timer >= scaleCurve[scaleCurveIndex].time)
		{
			timer = 0f;
		}
		base.transform.localScale = Vector3.one * (1f + scaleCurve.Evaluate(timer) * AmplitudeFactor);
	}
}
