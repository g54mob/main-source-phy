using UnityEngine;

public class Bezier : MonoBehaviour
{
	public float sections = 10f;

	private LineRenderer lineRenderer;

	public Vector3 GetQuadraticCoordinates(float t, Vector3 p0, Vector3 c0, Vector3 p1)
	{
		return Mathf.Pow(1f - t, 2f) * p0 + 2f * t * (1f - t) * c0 + Mathf.Pow(t, 2f) * p1;
	}

	public void Plot(Vector3 p0, Vector3 c0, Vector3 p1)
	{
	}
}
