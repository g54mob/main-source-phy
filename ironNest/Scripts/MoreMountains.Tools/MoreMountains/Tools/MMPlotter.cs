using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPlotter : MonoBehaviour
	{
		public MethodInfo TweenMethod;

		public int TweenMethodIndex;

		[Header("Graph")]
		public float GraphSize;

		[Range(0f, 1000f)]
		public int Resolution;

		[Header("Points")]
		public Transform PlotPointPrefab;

		public float PointScaleFactor;

		public Material PlotPointMaterial;

		[MMReadOnly]
		public float DistanceBetweenPoints;

		[Header("Axis")]
		public MMPlotterAxis Axis;

		protected Transform[] _points;

		protected float _pointScale;

		protected Vector3 _scale;

		protected Vector3 _position;

		protected Transform _point;

		protected Vector3 _horizontalAxisStart;

		protected Vector3 _horizontalAxisEnd;

		protected Vector3 _verticalAxisStart;

		protected Vector3 _verticalAxisEnd;

		protected float _axisWidth;

		protected List<MethodInfo> _methodList;

		protected Vector2 _pointValues;

		protected object[] _parameter;

		protected MMPlotterAxis _axis;

		protected Vector3 _positionPointInitialPosition;

		protected Vector3 _positionPointVerticalInitialPosition;

		protected Vector3 _rotationPointInitialRotation;

		protected Vector3 _scalePointInitialScale;

		[Header("Movement")]
		public float MovementPauseDuration;

		protected float _currentMovement;

		protected float _lastMovementEndedAt;

		protected Vector3 _curvePointNewMovement;

		protected string _timeString;

		protected const float _plotterCurvePointScale = 0.1f;

		protected Vector3 _newScale;

		protected float _newValue;

		protected float _newScaleUnit;

		protected Vector3 Vector3Zero;

		public virtual string[] GetMethodsList()
		{
			return null;
		}

		public virtual float InvokeTween(int index, object[] parameters)
		{
			return 0f;
		}

		public virtual string TweenName(int index)
		{
			return null;
		}

		protected virtual void FillMethodList()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void DrawGraph()
		{
		}

		protected virtual void DrawAxis()
		{
		}

		protected virtual void DrawPoints()
		{
		}

		public virtual void SetMaterial(Material newMaterial)
		{
		}

		protected virtual void Cleanup()
		{
		}

		protected virtual void Update()
		{
		}
	}
}
