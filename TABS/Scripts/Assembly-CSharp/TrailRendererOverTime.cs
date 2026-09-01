using UnityEngine;

public class TrailRendererOverTime : MonoBehaviour
{
	private TrailRenderer trailRenderer;

	public Gradient gradient;

	public AnimationCurve curve;

	public float time = 2f;

	private float counter;

	private void Awake()
	{
		trailRenderer = GetComponent<TrailRenderer>();
	}

	private void OnEnable()
	{
		counter = 0f;
	}

	private void Update()
	{
		counter += Time.deltaTime / time;
		trailRenderer.widthMultiplier = curve.Evaluate(counter);
		trailRenderer.startColor = gradient.Evaluate(counter);
		trailRenderer.endColor = gradient.Evaluate(counter);
	}
}
