using Dreamteck.Splines;
using UnityEngine;

[ExecuteInEditMode]
public class ZedAxisSplineUtils : MonoBehaviour
{
	public bool generate;

	public SplineComputer spline;

	public Transform parentObject;

	private void Update()
	{
		if (generate)
		{
			generate = false;
			GenerateObjectListFromSpline(spline, parentObject);
		}
	}

	private void GenerateObjectListFromSpline(SplineComputer spline, Transform parentObject)
	{
		for (int i = 0; i < spline.pointCount - 1; i++)
		{
			GameObject obj = new GameObject();
			obj.transform.parent = parentObject;
			obj.name = i.ToString();
			obj.transform.SetSiblingIndex(i);
			obj.transform.localPosition = spline.GetPointPosition(i, SplineComputer.Space.Local);
		}
	}
}
