using Unity.AI.Navigation;
using UnityEngine;

[ExecuteInEditMode]
public class NavMeshLinkSpline : MonoBehaviour
{
	[SerializeField]
	private Spline _splineVisualization;

	[SerializeField]
	private NavMeshLink _navMeshLinkData;

	[SerializeField]
	[Min(0.01f)]
	private float _heightOffset;

	[SerializeField]
	[Range(0.25f, 0.75f)]
	private float _placementOffset;
}
