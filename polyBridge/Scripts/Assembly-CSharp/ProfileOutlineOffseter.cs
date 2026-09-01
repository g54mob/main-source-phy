using Dreamteck.Splines;
using UnityEngine;

public class ProfileOutlineOffseter : MonoBehaviour
{
	public SplineComputer m_SourceSplineComputer;

	public Vector3 m_Offset;

	public void OffsetOutline()
	{
		if (m_SourceSplineComputer == null)
		{
			Debug.Log("No Source Spline Computer Specified...");
			return;
		}
		SplinePoint[] points = m_SourceSplineComputer.GetPoints(SplineComputer.Space.Local);
		for (int i = 0; i < points.Length; i++)
		{
			m_SourceSplineComputer.SetPointPosition(i, points[i].position + m_Offset, SplineComputer.Space.Local);
		}
	}
}
