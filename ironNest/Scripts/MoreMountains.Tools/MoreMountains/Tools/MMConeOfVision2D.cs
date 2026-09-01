using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	[AddComponentMenu("More Mountains/Tools/Vision/MMConeOfVision2D")]
	public class MMConeOfVision2D : MonoBehaviour
	{
		public struct RaycastData
		{
			public bool Hit;

			public Vector3 Point;

			public float Distance;

			public float Angle;

			public RaycastData(bool hit, Vector3 point, float distance, float angle)
			{
				Hit = false;
				Point = default(Vector3);
				Distance = 0f;
				Angle = 0f;
			}
		}

		public struct MeshEdgePosition
		{
			public Vector3 PointA;

			public Vector3 PointB;

			public MeshEdgePosition(Vector3 pointA, Vector3 pointB)
			{
				PointA = default(Vector3);
				PointB = default(Vector3);
			}
		}

		[Header("Vision")]
		public LayerMask ObstacleMask;

		public float VisionRadius;

		[Range(0f, 360f)]
		public float VisionAngle;

		[Range(0f, 360f)]
		public float AngleOffset;

		[MMReadOnly]
		public Vector3 Direction;

		[MMReadOnly]
		public Vector3 EulerAngles;

		[Header("Target scanning")]
		public bool ShouldScanForTargets;

		public LayerMask TargetMask;

		public float ScanFrequencyInSeconds;

		[MMReadOnly]
		public List<Transform> VisibleTargets;

		[Header("Mesh")]
		public bool ShouldDrawMesh;

		public float MeshDensity;

		public int EdgePrecision;

		public float EdgeThreshold;

		public MeshFilter VisionMeshFilter;

		protected Mesh _visionMesh;

		protected Collider2D[] _targetsWithinDistance;

		protected Transform _target;

		protected Vector3 _directionToTarget;

		protected float _distanceToTarget;

		protected float _lastScanTimestamp;

		protected RaycastHit2D _scanForTargetsHit2D;

		protected List<Vector3> _viewPoints;

		protected RaycastData _oldViewCast;

		protected RaycastData _viewCast;

		protected Vector3[] _vertices;

		protected int[] _triangles;

		protected Vector3 _minPoint;

		protected Vector3 _maxPoint;

		protected Vector3 _direction;

		protected RaycastData _returnRaycastData;

		protected RaycastHit2D _raycastAtAngleHit2D;

		protected int _numberOfVerticesLastTime;

		protected virtual void Awake()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		public virtual void SetDirectionAndAngles(Vector3 direction, Vector3 eulerAngles)
		{
		}

		protected virtual void ScanForTargets()
		{
		}

		protected virtual void DrawMesh()
		{
		}

		private MeshEdgePosition FindMeshEdgePosition(RaycastData minimumViewCast, RaycastData maximumViewCast)
		{
			return default(MeshEdgePosition);
		}

		private RaycastData RaycastAtAngle(float angle)
		{
			return default(RaycastData);
		}
	}
}
