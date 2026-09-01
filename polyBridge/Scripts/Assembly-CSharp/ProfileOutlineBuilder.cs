using Dreamteck.Splines;
using UnityEngine;

public class ProfileOutlineBuilder : MonoBehaviour
{
	public SplineComputer m_SourceSplineComputer;

	public GameObject m_ProfilePointsParent;

	public void BuildProfileOutline()
	{
		if (m_SourceSplineComputer == null)
		{
			Debug.Log("No Source Spline Computer Specified...");
			return;
		}
		if (m_ProfilePointsParent == null)
		{
			Debug.Log("No Profile Points Parent specified...");
			return;
		}
		SplinePoint[] points = m_SourceSplineComputer.GetPoints(SplineComputer.Space.Local);
		_ = new Vector2[points.Length - 1];
		for (int i = 0; i < points.Length - 1; i++)
		{
			GameObject obj = new GameObject();
			obj.transform.parent = m_ProfilePointsParent.transform;
			obj.transform.position = new Vector3(points[i].position.x, points[i].position.y, 0f);
			obj.name = $"Vert_{i}";
		}
	}
}
