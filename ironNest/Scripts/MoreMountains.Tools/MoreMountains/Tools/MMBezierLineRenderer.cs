using UnityEngine;

namespace MoreMountains.Tools
{
	[ExecuteAlways]
	[RequireComponent(typeof(LineRenderer))]
	[AddComponentMenu("More Mountains/Tools/Sprites/MMBezierLineRenderer")]
	public class MMBezierLineRenderer : MonoBehaviour
	{
		public Transform[] AdjustmentHandles;

		public int NumberOfSegments;

		public string SortingLayerName;

		[MMReadOnly]
		public int NumberOfCurves;

		protected int _sortingLayerID;

		protected LineRenderer _lineRenderer;

		protected Vector3 _point;

		protected Vector3 _p;

		protected bool _initialized;

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void DrawCurve()
		{
		}

		protected virtual Vector3 BezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			return default(Vector3);
		}
	}
}
