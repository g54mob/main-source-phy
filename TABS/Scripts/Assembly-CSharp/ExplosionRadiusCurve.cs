using UnityEngine;

public class ExplosionRadiusCurve : MonoBehaviour
{
	private Explosion explosion;

	public AnimationCurve radiusCurve;

	private float counter;

	private float newRadius = 1f;

	private float originalRadius = 1f;

	private bool counting;

	private void Start()
	{
		explosion = GetComponent<Explosion>();
		originalRadius = explosion.radius;
	}

	private void Update()
	{
		if (counting)
		{
			counter += Time.deltaTime;
		}
	}

	public void UpdateRadius()
	{
		newRadius = radiusCurve.Evaluate(counter) + originalRadius;
		explosion.radius = newRadius;
	}

	public void StartCounting()
	{
		counting = true;
	}

	public void StopCounting()
	{
		counting = false;
	}

	public void ResetCounter()
	{
		counter = 0f;
	}
}
