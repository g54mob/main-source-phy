using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using FluffyUnderware.Curvy.Utils;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace FluffyUnderware.Curvy
{
	[HelpURL("https://curvyeditor.com/doclink/curvyspline")]
	[AddComponentMenu("Curvy/Curvy Spline", 1)]
	[ExecuteInEditMode]
	public class CurvySpline : DTVersionedMonoBehaviour
	{
		public const string VERSION = "6.0.0";

		public const string APIVERSION = "600";

		public const string WEBROOT = "https://curvyeditor.com/";

		public const string DOCLINK = "https://curvyeditor.com/doclink/";

		[SerializeField]
		[HideInInspector]
		private List<CurvySplineSegment> ControlPoints = new List<CurvySplineSegment>();

		[HideInInspector]
		public bool ShowGizmos = true;

		[Section("General", true, false, 100, HelpURL = "https://curvyeditor.com/doclink/curvyspline_general")]
		[Tooltip("Interpolation Method")]
		[SerializeField]
		[FormerlySerializedAs("Interpolation")]
		private CurvyInterpolation m_Interpolation = CurvyGlobalManager.DefaultInterpolation;

		[Tooltip("Restrict Control Points to local X/Y axis")]
		[FieldAction("CBCheck2DPlanar", ActionAttribute.ActionEnum.Callback)]
		[SerializeField]
		private bool m_RestrictTo2D;

		[SerializeField]
		[FormerlySerializedAs("Closed")]
		private bool m_Closed;

		[FieldCondition("canHaveManualEndCP", Action = ActionAttribute.ActionEnum.Enable)]
		[Tooltip("Handle End Control Points automatically?")]
		[SerializeField]
		[FormerlySerializedAs("AutoEndTangents")]
		private bool m_AutoEndTangents = true;

		[Tooltip("Orientation Flow")]
		[SerializeField]
		[FormerlySerializedAs("Orientation")]
		private CurvyOrientation m_Orientation = CurvyOrientation.Dynamic;

		[Section("Global Bezier Options", true, false, 100, HelpURL = "https://curvyeditor.com/doclink/curvyspline_bezier")]
		[GroupCondition("m_Interpolation", CurvyInterpolation.Bezier, false)]
		[RangeEx(0f, 1f, "Default Distance %", "Handle length by distance to neighbours")]
		[SerializeField]
		private float m_AutoHandleDistance = 0.39f;

		[Section("Global TCB Options", true, false, 100, HelpURL = "https://curvyeditor.com/doclink/curvyspline_tcb")]
		[GroupCondition("m_Interpolation", CurvyInterpolation.TCB, false)]
		[GroupAction("TCBOptionsGUI", ActionAttribute.ActionEnum.Callback, Position = ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[FormerlySerializedAs("Tension")]
		private float m_Tension;

		[SerializeField]
		[FormerlySerializedAs("Continuity")]
		private float m_Continuity;

		[SerializeField]
		[FormerlySerializedAs("Bias")]
		private float m_Bias;

		[Section("Advanced Settings", true, false, 100, HelpURL = "https://curvyeditor.com/doclink/curvyspline_advanced")]
		[FieldAction("ShowGizmoGUI", ActionAttribute.ActionEnum.Callback, Position = ActionAttribute.ActionPositionEnum.Above)]
		[Label("Color", "Gizmo color")]
		[SerializeField]
		private Color m_GizmoColor = CurvyGlobalManager.DefaultGizmoColor;

		[Label("Active Color", "Selected Gizmo color")]
		[SerializeField]
		private Color m_GizmoSelectionColor = CurvyGlobalManager.DefaultGizmoSelectionColor;

		[RangeEx(1f, 100f, "", "")]
		[SerializeField]
		[FormerlySerializedAs("Granularity")]
		[Tooltip("Defines how densely the cached points are. When the value is 100, the number of cached points per world distance unit is equal to the spline's MaxPointsPerUnit")]
		private int m_CacheDensity = 50;

		[SerializeField]
		[Tooltip("The maximum number of sampling points per world distance unit. Sampling is used in caching or shape extrusion for example")]
		private float m_MaxPointsPerUnit = 8f;

		[SerializeField]
		[Tooltip("Use a GameObject pool at runtime")]
		private bool m_UsePooling = true;

		[SerializeField]
		[Tooltip("Use threading where applicable. Threading is is currently not supported when targetting WebGL and Universal Windows Platform")]
		private bool m_UseThreading;

		[Tooltip("Refresh when Control Point position change?")]
		[SerializeField]
		[FormerlySerializedAs("AutoRefresh")]
		private bool m_CheckTransform = true;

		[SerializeField]
		private CurvyUpdateMethod m_UpdateIn;

		[Group("Events", Expanded = false, Sort = 1000, HelpURL = "https://curvyeditor.com/doclink/curvyspline_events")]
		[SortOrder(0)]
		[SerializeField]
		private CurvySplineEvent m_OnRefresh = new CurvySplineEvent();

		[Group("Events", Sort = 1000)]
		[SortOrder(1)]
		[SerializeField]
		private CurvySplineEvent m_OnAfterControlPointChanges = new CurvySplineEvent();

		[Group("Events", Sort = 1000)]
		[SortOrder(2)]
		[SerializeField]
		private CurvyControlPointEvent m_OnBeforeControlPointAdd = new CurvyControlPointEvent();

		[Group("Events", Sort = 1000)]
		[SortOrder(3)]
		[SerializeField]
		private CurvyControlPointEvent m_OnAfterControlPointAdd = new CurvyControlPointEvent();

		[Group("Events", Sort = 1000)]
		[SortOrder(4)]
		[SerializeField]
		private CurvyControlPointEvent m_OnBeforeControlPointDelete = new CurvyControlPointEvent();

		private bool mIsInitialized;

		private bool isStarted;

		private bool sendOnRefreshEventNextUpdate;

		private readonly object controlPointsRelationshipCacheLock = new object();

		private List<CurvySplineSegment> mSegments = new List<CurvySplineSegment>();

		private ReadOnlyCollection<CurvySplineSegment> readOnlyControlPoints;

		private float length = -1f;

		private int mCacheSize = -1;

		private Bounds? mBounds;

		private bool mDirtyCurve;

		private bool mDirtyOrientation;

		private HashSet<CurvySplineSegment> dirtyControlPointsMinimalSet = new HashSet<CurvySplineSegment>();

		private List<CurvySplineSegment> dirtyCpsExtendedList = new List<CurvySplineSegment>();

		private bool allControlPointsAreDirty;

		private ThreadPoolWorker<CurvySplineSegment> mThreadWorker = new ThreadPoolWorker<CurvySplineSegment>();

		private readonly CurvySplineEventArgs defaultSplineEventArgs;

		private readonly CurvyControlPointEventArgs defaultAddAfterEventArgs;

		private readonly CurvyControlPointEventArgs defaultDeleteEventArgs;

		private CurvySplineSegment _lastDistToSeg;

		private readonly Action<CurvySplineSegment> refreshCurveAction;

		private Vector3 lastProcessedPosition;

		private Quaternion lastProcessedRotation;

		private Vector3 lastProcessedScale;

		private bool globalCoordinatesChangedThisFrame;

		private bool isCpsRelationshipCacheValid;

		private CurvySplineSegment firstSegment;

		private CurvySplineSegment lastSegment;

		private CurvySplineSegment firstVisibleControlPoint;

		private CurvySplineSegment lastVisibleControlPoint;

		public CurvyInterpolation Interpolation
		{
			get
			{
				return m_Interpolation;
			}
			set
			{
				if (m_Interpolation != value)
				{
					m_Interpolation = value;
					InvalidateControlPointsRelationshipCacheINTERNAL();
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
				}
				AutoEndTangents = m_AutoEndTangents;
			}
		}

		public bool RestrictTo2D
		{
			get
			{
				return m_RestrictTo2D;
			}
			set
			{
				if (m_RestrictTo2D != value)
				{
					m_RestrictTo2D = value;
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
				}
			}
		}

		public float AutoHandleDistance
		{
			get
			{
				return m_AutoHandleDistance;
			}
			set
			{
				float num = Mathf.Clamp01(value);
				if (m_AutoHandleDistance != num)
				{
					m_AutoHandleDistance = num;
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
				}
			}
		}

		public bool Closed
		{
			get
			{
				return m_Closed;
			}
			set
			{
				if (m_Closed != value)
				{
					m_Closed = value;
					InvalidateControlPointsRelationshipCacheINTERNAL();
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: true);
				}
				AutoEndTangents = m_AutoEndTangents;
			}
		}

		public bool AutoEndTangents
		{
			get
			{
				return m_AutoEndTangents;
			}
			set
			{
				bool flag = !canHaveManualEndCP() || value;
				if (m_AutoEndTangents != flag)
				{
					m_AutoEndTangents = flag;
					InvalidateControlPointsRelationshipCacheINTERNAL();
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: true);
				}
			}
		}

		public CurvyOrientation Orientation
		{
			get
			{
				return m_Orientation;
			}
			set
			{
				if (m_Orientation != value)
				{
					m_Orientation = value;
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
				}
			}
		}

		public CurvyUpdateMethod UpdateIn
		{
			get
			{
				return m_UpdateIn;
			}
			set
			{
				if (m_UpdateIn != value)
				{
					m_UpdateIn = value;
				}
			}
		}

		public Color GizmoColor
		{
			get
			{
				return m_GizmoColor;
			}
			set
			{
				if (m_GizmoColor != value)
				{
					m_GizmoColor = value;
				}
			}
		}

		public Color GizmoSelectionColor
		{
			get
			{
				return m_GizmoSelectionColor;
			}
			set
			{
				if (m_GizmoSelectionColor != value)
				{
					m_GizmoSelectionColor = value;
				}
			}
		}

		public int CacheDensity
		{
			get
			{
				return m_CacheDensity;
			}
			set
			{
				int num = Mathf.Clamp(value, 1, 100);
				if (m_CacheDensity != num)
				{
					m_CacheDensity = num;
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
				}
			}
		}

		public float MaxPointsPerUnit
		{
			get
			{
				return m_MaxPointsPerUnit;
			}
			set
			{
				float num = Mathf.Clamp(value, 0.0001f, 1000f);
				if (m_MaxPointsPerUnit != num)
				{
					m_MaxPointsPerUnit = num;
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
				}
			}
		}

		public bool UsePooling
		{
			get
			{
				return m_UsePooling;
			}
			set
			{
				if (m_UsePooling != value)
				{
					m_UsePooling = value;
				}
			}
		}

		public bool UseThreading
		{
			get
			{
				return m_UseThreading;
			}
			set
			{
				if (m_UseThreading != value)
				{
					m_UseThreading = value;
				}
			}
		}

		public bool CheckTransform
		{
			get
			{
				return m_CheckTransform;
			}
			set
			{
				if (m_CheckTransform != value)
				{
					m_CheckTransform = value;
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
				}
			}
		}

		public float Tension
		{
			get
			{
				return m_Tension;
			}
			set
			{
				if (m_Tension != value)
				{
					m_Tension = value;
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
				}
			}
		}

		public float Continuity
		{
			get
			{
				return m_Continuity;
			}
			set
			{
				if (m_Continuity != value)
				{
					m_Continuity = value;
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
				}
			}
		}

		public float Bias
		{
			get
			{
				return m_Bias;
			}
			set
			{
				if (m_Bias != value)
				{
					m_Bias = value;
					SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
				}
			}
		}

		public bool IsInitialized => mIsInitialized;

		public Bounds Bounds
		{
			get
			{
				if (!mBounds.HasValue)
				{
					Bounds bounds2;
					if (Count > 0)
					{
						Bounds bounds = this[0].Bounds;
						for (int i = 1; i < Count; i++)
						{
							bounds.Encapsulate(this[i].Bounds);
						}
						bounds2 = bounds;
					}
					else
					{
						bounds2 = new Bounds(base.transform.position, Vector3.zero);
					}
					if (!Dirty)
					{
						mBounds = bounds2;
					}
					return bounds2;
				}
				return mBounds.Value;
			}
		}

		public int Count => Segments.Count;

		public int ControlPointCount => ControlPoints.Count;

		public int CacheSize
		{
			get
			{
				if (mCacheSize < 0)
				{
					int num = 0;
					List<CurvySplineSegment> segments = Segments;
					for (int i = 0; i < segments.Count; i++)
					{
						num += segments[i].CacheSize;
					}
					if (!Dirty)
					{
						mCacheSize = num;
					}
					return num;
				}
				return mCacheSize;
			}
		}

		public float Length
		{
			get
			{
				if (length < 0f)
				{
					float result = ((Segments.Count != 0) ? (Closed ? (this[Count - 1].Distance + this[Count - 1].Length) : LastVisibleControlPoint.Distance) : 0f);
					if (!Dirty)
					{
						length = result;
					}
					return result;
				}
				return length;
			}
		}

		public bool Dirty
		{
			get
			{
				if (!allControlPointsAreDirty)
				{
					return dirtyControlPointsMinimalSet.Count > 0;
				}
				return true;
			}
		}

		public CurvySplineSegment this[int idx] => Segments[idx];

		public ReadOnlyCollection<CurvySplineSegment> ControlPointsList
		{
			get
			{
				if (readOnlyControlPoints == null)
				{
					readOnlyControlPoints = ControlPoints.AsReadOnly();
				}
				return readOnlyControlPoints;
			}
		}

		[CanBeNull]
		public CurvySplineSegment FirstVisibleControlPoint
		{
			get
			{
				if (!isCpsRelationshipCacheValid)
				{
					RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
				}
				return firstVisibleControlPoint;
			}
		}

		[CanBeNull]
		public CurvySplineSegment LastVisibleControlPoint
		{
			get
			{
				if (!isCpsRelationshipCacheValid)
				{
					RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
				}
				return lastVisibleControlPoint;
			}
		}

		public CurvySplineSegment FirstSegment
		{
			get
			{
				if (!isCpsRelationshipCacheValid)
				{
					RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
				}
				return firstSegment;
			}
		}

		public CurvySplineSegment LastSegment
		{
			get
			{
				if (!isCpsRelationshipCacheValid)
				{
					RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
				}
				return lastSegment;
			}
		}

		public CurvySpline NextSpline
		{
			get
			{
				CurvySplineSegment curvySplineSegment = LastVisibleControlPoint;
				if (!curvySplineSegment || !curvySplineSegment.FollowUp)
				{
					return null;
				}
				return curvySplineSegment.FollowUp.Spline;
			}
		}

		public CurvySpline PreviousSpline
		{
			get
			{
				CurvySplineSegment curvySplineSegment = FirstVisibleControlPoint;
				if (!curvySplineSegment || !curvySplineSegment.FollowUp)
				{
					return null;
				}
				return curvySplineSegment.FollowUp.Spline;
			}
		}

		public bool GlobalCoordinatesChangedThisFrame => globalCoordinatesChangedThisFrame;

		public Action<CurvySpline> OnGlobalCoordinatesChanged { get; set; }

		public CurvySplineEvent OnRefresh
		{
			get
			{
				return m_OnRefresh;
			}
			set
			{
				if (m_OnRefresh != value)
				{
					m_OnRefresh = value;
				}
			}
		}

		public CurvySplineEvent OnAfterControlPointChanges
		{
			get
			{
				return m_OnAfterControlPointChanges;
			}
			set
			{
				if (m_OnAfterControlPointChanges != value)
				{
					m_OnAfterControlPointChanges = value;
				}
			}
		}

		public CurvyControlPointEvent OnBeforeControlPointAdd
		{
			get
			{
				return m_OnBeforeControlPointAdd;
			}
			set
			{
				if (m_OnBeforeControlPointAdd != value)
				{
					m_OnBeforeControlPointAdd = value;
				}
			}
		}

		public CurvyControlPointEvent OnAfterControlPointAdd
		{
			get
			{
				return m_OnAfterControlPointAdd;
			}
			set
			{
				if (m_OnAfterControlPointAdd != value)
				{
					m_OnAfterControlPointAdd = value;
				}
			}
		}

		public CurvyControlPointEvent OnBeforeControlPointDelete
		{
			get
			{
				return m_OnBeforeControlPointDelete;
			}
			set
			{
				if (m_OnBeforeControlPointDelete != value)
				{
					m_OnBeforeControlPointDelete = value;
				}
			}
		}

		private List<CurvySplineSegment> controlPoints => ControlPoints;

		private List<CurvySplineSegment> Segments
		{
			get
			{
				if (!isCpsRelationshipCacheValid)
				{
					RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
				}
				return mSegments;
			}
		}

		public CurvySpline()
		{
			refreshCurveAction = delegate(CurvySplineSegment controlPoint)
			{
				controlPoint.refreshCurveINTERNAL(Interpolation, IsControlPointASegment(controlPoint), this);
			};
			defaultSplineEventArgs = new CurvySplineEventArgs(this, this);
			defaultAddAfterEventArgs = new CurvyControlPointEventArgs(this, this, null, CurvyControlPointEventArgs.ModeEnum.AddAfter);
			defaultDeleteEventArgs = new CurvyControlPointEventArgs(this, this, null, CurvyControlPointEventArgs.ModeEnum.Delete);
		}

		public static CurvySpline Create()
		{
			CurvySpline component = new GameObject("Curvy Spline", typeof(CurvySpline)).GetComponent<CurvySpline>();
			component.gameObject.layer = CurvyGlobalManager.SplineLayer;
			component.Start();
			return component;
		}

		public static CurvySpline Create(CurvySpline takeOptionsFrom)
		{
			CurvySpline curvySpline = Create();
			if ((bool)takeOptionsFrom)
			{
				curvySpline.RestrictTo2D = takeOptionsFrom.RestrictTo2D;
				curvySpline.GizmoColor = takeOptionsFrom.GizmoColor;
				curvySpline.GizmoSelectionColor = takeOptionsFrom.GizmoSelectionColor;
				curvySpline.Interpolation = takeOptionsFrom.Interpolation;
				curvySpline.Closed = takeOptionsFrom.Closed;
				curvySpline.AutoEndTangents = takeOptionsFrom.AutoEndTangents;
				curvySpline.CacheDensity = takeOptionsFrom.CacheDensity;
				curvySpline.MaxPointsPerUnit = takeOptionsFrom.MaxPointsPerUnit;
				curvySpline.Orientation = takeOptionsFrom.Orientation;
				curvySpline.CheckTransform = takeOptionsFrom.CheckTransform;
			}
			return curvySpline;
		}

		public static int CalculateCacheSize(int density, float splineLength, float maxPointsPerUnit)
		{
			return Mathf.FloorToInt(CalculateSamplingPointsPerUnit(density, maxPointsPerUnit) * splineLength) + 1;
		}

		public static float CalculateSamplingPointsPerUnit(int density, float maxPointsPerUnit)
		{
			int num = Mathf.Clamp(density, 1, 100);
			if (num != density)
			{
				DTLog.LogWarning("[Curvy] CalculateSamplingPointsPerUnit got an invalid density parameter. It should be between 1 and 100. The parameter value was " + density);
				density = num;
			}
			return DTTween.QuadIn(density - 1, 0f, maxPointsPerUnit, 99f);
		}

		public static Vector3 Bezier(Vector3 T0, Vector3 P0, Vector3 P1, Vector3 T1, float f)
		{
			double num = (double)(0f - P0.x) + 3.0 * (double)T0.x + -3.0 * (double)T1.x + (double)P1.x;
			double num2 = 3.0 * (double)P0.x + -6.0 * (double)T0.x + 3.0 * (double)T1.x;
			double num3 = -3.0 * (double)P0.x + 3.0 * (double)T0.x;
			double num4 = P0.x;
			double num5 = (double)(0f - P0.y) + 3.0 * (double)T0.y + -3.0 * (double)T1.y + (double)P1.y;
			double num6 = 3.0 * (double)P0.y + -6.0 * (double)T0.y + 3.0 * (double)T1.y;
			double num7 = -3.0 * (double)P0.y + 3.0 * (double)T0.y;
			double num8 = P0.y;
			double num9 = (double)(0f - P0.z) + 3.0 * (double)T0.z + -3.0 * (double)T1.z + (double)P1.z;
			double num10 = 3.0 * (double)P0.z + -6.0 * (double)T0.z + 3.0 * (double)T1.z;
			double num11 = -3.0 * (double)P0.z + 3.0 * (double)T0.z;
			double num12 = P0.z;
			float x = (float)(((num * (double)f + num2) * (double)f + num3) * (double)f + num4);
			float y = (float)(((num5 * (double)f + num6) * (double)f + num7) * (double)f + num8);
			float z = (float)(((num9 * (double)f + num10) * (double)f + num11) * (double)f + num12);
			return new Vector3(x, y, z);
		}

		[Obsolete("This methods is not used and will be removed in an upcoming version")]
		public static float BezierTangent(float T0, float P0, float P1, float T1, float t)
		{
			float num = P1 - 3f * T1 + 3f * T0 - P0;
			float num2 = 3f * T1 - 6f * T0 + 3f * P0;
			float num3 = 3f * T0 - 3f * P0;
			return 3f * num * t * t + 2f * num2 * t + num3;
		}

		public static Vector3 BezierTangent(Vector3 T0, Vector3 P0, Vector3 P1, Vector3 T1, float f)
		{
			Vector3 vector = P1 - 3f * T1 + 3f * T0 - P0;
			Vector3 vector2 = 3f * T1 - 6f * T0 + 3f * P0;
			Vector3 vector3 = 3f * T0 - 3f * P0;
			return 3f * vector * f * f + 2f * vector2 * f + vector3;
		}

		public static Vector3 CatmullRom(Vector3 T0, Vector3 P0, Vector3 P1, Vector3 T1, float f)
		{
			double num = -0.5 * (double)T0.x + 1.5 * (double)P0.x + -1.5 * (double)P1.x + 0.5 * (double)T1.x;
			double num2 = (double)T0.x + -2.5 * (double)P0.x + 2.0 * (double)P1.x + -0.5 * (double)T1.x;
			double num3 = -0.5 * (double)T0.x + 0.5 * (double)P1.x;
			double num4 = P0.x;
			double num5 = -0.5 * (double)T0.y + 1.5 * (double)P0.y + -1.5 * (double)P1.y + 0.5 * (double)T1.y;
			double num6 = (double)T0.y + -2.5 * (double)P0.y + 2.0 * (double)P1.y + -0.5 * (double)T1.y;
			double num7 = -0.5 * (double)T0.y + 0.5 * (double)P1.y;
			double num8 = P0.y;
			double num9 = -0.5 * (double)T0.z + 1.5 * (double)P0.z + -1.5 * (double)P1.z + 0.5 * (double)T1.z;
			double num10 = (double)T0.z + -2.5 * (double)P0.z + 2.0 * (double)P1.z + -0.5 * (double)T1.z;
			double num11 = -0.5 * (double)T0.z + 0.5 * (double)P1.z;
			double num12 = P0.z;
			float x = (float)(((num * (double)f + num2) * (double)f + num3) * (double)f + num4);
			float y = (float)(((num5 * (double)f + num6) * (double)f + num7) * (double)f + num8);
			float z = (float)(((num9 * (double)f + num10) * (double)f + num11) * (double)f + num12);
			return new Vector3(x, y, z);
		}

		public static Vector3 TCB(Vector3 T0, Vector3 P0, Vector3 P1, Vector3 T1, float f, float FT0, float FC0, float FB0, float FT1, float FC1, float FB1)
		{
			double num = (1f - FT0) * (1f + FC0) * (1f + FB0);
			double num2 = (1f - FT0) * (1f - FC0) * (1f - FB0);
			double num3 = (1f - FT1) * (1f - FC1) * (1f + FB1);
			double num4 = (1f - FT1) * (1f + FC1) * (1f - FB1);
			double num5 = 2.0;
			double num6 = (0.0 - num) / num5;
			double num7 = (4.0 + num - num2 - num3) / num5;
			double num8 = (-4.0 + num2 + num3 - num4) / num5;
			double num9 = num4 / num5;
			double num10 = 2.0 * num / num5;
			double num11 = (-6.0 - 2.0 * num + 2.0 * num2 + num3) / num5;
			double num12 = (6.0 - 2.0 * num2 - num3 + num4) / num5;
			double num13 = (0.0 - num4) / num5;
			double num14 = (0.0 - num) / num5;
			double num15 = (num - num2) / num5;
			double num16 = num2 / num5;
			double num17 = 2.0 / num5;
			double num18 = num6 * (double)T0.x + num7 * (double)P0.x + num8 * (double)P1.x + num9 * (double)T1.x;
			double num19 = num10 * (double)T0.x + num11 * (double)P0.x + num12 * (double)P1.x + num13 * (double)T1.x;
			double num20 = num14 * (double)T0.x + num15 * (double)P0.x + num16 * (double)P1.x;
			double num21 = num17 * (double)P0.x;
			double num22 = num6 * (double)T0.y + num7 * (double)P0.y + num8 * (double)P1.y + num9 * (double)T1.y;
			double num23 = num10 * (double)T0.y + num11 * (double)P0.y + num12 * (double)P1.y + num13 * (double)T1.y;
			double num24 = num14 * (double)T0.y + num15 * (double)P0.y + num16 * (double)P1.y;
			double num25 = num17 * (double)P0.y;
			double num26 = num6 * (double)T0.z + num7 * (double)P0.z + num8 * (double)P1.z + num9 * (double)T1.z;
			double num27 = num10 * (double)T0.z + num11 * (double)P0.z + num12 * (double)P1.z + num13 * (double)T1.z;
			double num28 = num14 * (double)T0.z + num15 * (double)P0.z + num16 * (double)P1.z;
			double num29 = num17 * (double)P0.z;
			float x = (float)(((num18 * (double)f + num19) * (double)f + num20) * (double)f + num21);
			float y = (float)(((num22 * (double)f + num23) * (double)f + num24) * (double)f + num25);
			float z = (float)(((num26 * (double)f + num27) * (double)f + num28) * (double)f + num29);
			return new Vector3(x, y, z);
		}

		public Vector3 Interpolate(float tf)
		{
			return Interpolate(tf, Interpolation);
		}

		public Vector3 Interpolate(float tf, CurvyInterpolation interpolation)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return Vector3.zero;
			}
			return curvySplineSegment.Interpolate(localF, interpolation);
		}

		public Vector3 InterpolateFast(float tf)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return Vector3.zero;
			}
			return curvySplineSegment.InterpolateFast(localF);
		}

		public T GetMetadata<T>(float tf) where T : Component, ICurvyMetadata
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return null;
			}
			return curvySplineSegment.GetMetadata<T>();
		}

		[Obsolete("Use GetMetaData<T> instead")]
		public Component GetMetadata(Type type, float tf)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return null;
			}
			return curvySplineSegment.GetMetaData(type);
		}

		public U GetInterpolatedMetadata<T, U>(float tf) where T : CurvyInterpolatableMetadataBase<U>
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return default(U);
			}
			return curvySplineSegment.GetInterpolatedMetadata<T, U>(localF);
		}

		[Obsolete("Use GetInterpolatedMetadata<T, U> instead")]
		public U InterpolateMetadata<T, U>(float tf) where T : Component, ICurvyInterpolatableMetadata<U>
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return default(U);
			}
			return curvySplineSegment.InterpolateMetadata<T, U>(localF);
		}

		[Obsolete("Use GetInterpolatedMetadata<T, U> instead")]
		public object InterpolateMetadata(Type type, float tf)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return null;
			}
			return curvySplineSegment.InterpolateMetadata(type, localF);
		}

		public Vector3 InterpolateScale(float tf)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return Vector3.zero;
			}
			return curvySplineSegment.InterpolateScale(localF);
		}

		public Vector3 GetOrientationUpFast(float tf)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return Vector3.zero;
			}
			return curvySplineSegment.GetOrientationUpFast(localF);
		}

		public Quaternion GetOrientationFast(float tf, bool inverse = false)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return Quaternion.identity;
			}
			return curvySplineSegment.GetOrientationFast(localF, inverse);
		}

		public Vector3 GetTangent(float tf)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return Vector3.zero;
			}
			return curvySplineSegment.GetTangent(localF);
		}

		public Vector3 GetTangent(float tf, Vector3 localPosition)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return Vector3.zero;
			}
			return curvySplineSegment.GetTangent(localF, localPosition);
		}

		public Vector3 GetTangentFast(float tf)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if (!curvySplineSegment)
			{
				return Vector3.zero;
			}
			return curvySplineSegment.GetTangentFast(localF);
		}

		public void InterpolateAndGetTangent(float tf, out Vector3 localPosition, out Vector3 localTangent)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if ((bool)curvySplineSegment)
			{
				curvySplineSegment.InterpolateAndGetTangent(localF, out localPosition, out localTangent);
				return;
			}
			localPosition = Vector3.zero;
			localTangent = Vector3.zero;
		}

		public void InterpolateAndGetTangentFast(float tf, out Vector3 localPosition, out Vector3 localTangent)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF);
			if ((bool)curvySplineSegment)
			{
				curvySplineSegment.InterpolateAndGetTangentFast(localF, out localPosition, out localTangent);
				return;
			}
			localPosition = Vector3.zero;
			localTangent = Vector3.zero;
		}

		public Vector3 GetTangentByDistance(float distance)
		{
			return GetTangent(DistanceToTF(distance));
		}

		public Vector3 GetTangentByDistanceFast(float distance)
		{
			return GetTangentFast(DistanceToTF(distance));
		}

		public Vector3 InterpolateByDistance(float distance)
		{
			return Interpolate(DistanceToTF(distance));
		}

		public Vector3 InterpolateByDistanceFast(float distance)
		{
			return InterpolateFast(DistanceToTF(distance));
		}

		public float ExtrapolateDistanceToTF(float tf, float distance, float stepSize)
		{
			stepSize = Mathf.Max(0.0001f, stepSize);
			Vector3 vector = Interpolate(tf);
			float num = ((tf != 1f) ? Mathf.Min(1f, tf + stepSize) : Mathf.Min(1f, tf - stepSize));
			stepSize = Mathf.Abs(num - tf);
			float magnitude = (Interpolate(num) - vector).magnitude;
			if (magnitude != 0f)
			{
				return 1f / magnitude * stepSize * distance;
			}
			return 0f;
		}

		public float ExtrapolateDistanceToTFFast(float tf, float distance, float stepSize)
		{
			stepSize = Mathf.Max(0.0001f, stepSize);
			Vector3 vector = InterpolateFast(tf);
			float num = ((tf != 1f) ? Mathf.Min(1f, tf + stepSize) : Mathf.Min(1f, tf - stepSize));
			stepSize = Mathf.Abs(num - tf);
			float magnitude = (InterpolateFast(num) - vector).magnitude;
			if (magnitude != 0f)
			{
				return 1f / magnitude * stepSize * distance;
			}
			return 0f;
		}

		public float TFToDistance(float tf, CurvyClamping clamping = CurvyClamping.Clamp)
		{
			float num = Length;
			if (num == 0f)
			{
				return 0f;
			}
			if (tf == 0f)
			{
				return 0f;
			}
			if (tf == 1f)
			{
				return num;
			}
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(tf, out localF, clamping);
			return curvySplineSegment ? (curvySplineSegment.Distance + curvySplineSegment.LocalFToDistance(localF)) : 0f;
		}

		public CurvySplineSegment TFToSegment(float tf, out float localF, CurvyClamping clamping)
		{
			tf = CurvyUtility.ClampTF(tf, clamping);
			localF = 0f;
			int count = Count;
			if (count == 0)
			{
				return null;
			}
			float num = tf * (float)count;
			double num2 = Math.Round(num);
			int num3;
			if (num.Approximately((float)num2))
			{
				num3 = (int)num2;
				localF = 0f;
			}
			else
			{
				num3 = (int)num;
				localF = num - (float)num3;
			}
			if (num3 == count)
			{
				num3--;
				localF = 1f;
			}
			return this[num3];
		}

		public CurvySplineSegment TFToSegment(float tf, CurvyClamping clamping)
		{
			float localF;
			return TFToSegment(tf, out localF, clamping);
		}

		public CurvySplineSegment TFToSegment(float tf)
		{
			float localF;
			return TFToSegment(tf, out localF, CurvyClamping.Clamp);
		}

		public CurvySplineSegment TFToSegment(float tf, out float localF)
		{
			return TFToSegment(tf, out localF, CurvyClamping.Clamp);
		}

		public float SegmentToTF(CurvySplineSegment segment)
		{
			return SegmentToTF(segment, 0f);
		}

		public float SegmentToTF(CurvySplineSegment segment, float localF)
		{
			int segmentIndex = GetSegmentIndex(segment);
			if (segmentIndex != -1)
			{
				return (float)segmentIndex / (float)Count + 1f / (float)Count * localF;
			}
			if (!Closed && segment == LastVisibleControlPoint)
			{
				return 1f;
			}
			if (Count == 0)
			{
				return 0f;
			}
			short controlPointIndex = GetControlPointIndex(segment);
			if (!AutoEndTangents && controlPointIndex == 0)
			{
				return 0f;
			}
			if (!AutoEndTangents && controlPointIndex == ControlPointCount - 1)
			{
				return 1f;
			}
			DTLog.LogError("[Curvy] SegmentToTF reached an unexpected case. Please raise a bug report.");
			return -1f;
		}

		public float DistanceToTF(float distance, CurvyClamping clamping = CurvyClamping.Clamp)
		{
			if (Length == 0f)
			{
				return 0f;
			}
			if (distance == 0f)
			{
				return 0f;
			}
			if (distance == Length)
			{
				return 1f;
			}
			float localDistance;
			CurvySplineSegment curvySplineSegment = DistanceToSegment(distance, out localDistance, clamping);
			return curvySplineSegment ? SegmentToTF(curvySplineSegment, curvySplineSegment.DistanceToLocalF(localDistance)) : 0f;
		}

		public CurvySplineSegment DistanceToSegment(float distance, CurvyClamping clamping = CurvyClamping.Clamp)
		{
			float localDistance;
			return DistanceToSegment(distance, out localDistance, clamping);
		}

		public CurvySplineSegment DistanceToSegment(float distance, out float localDistance, CurvyClamping clamping = CurvyClamping.Clamp)
		{
			distance = CurvyUtility.ClampDistance(distance, clamping, Length);
			CurvySplineSegment curvySplineSegment;
			if (Count > 0)
			{
				if (distance >= LastSegment.Distance)
				{
					curvySplineSegment = LastSegment;
					localDistance = (Mathf.Approximately(distance, Length) ? curvySplineSegment.Length : (distance - curvySplineSegment.Distance));
				}
				else
				{
					CurvySplineSegment curvySplineSegment2 = ((!(_lastDistToSeg != null) || !(distance >= _lastDistToSeg.Distance)) ? Segments[0] : _lastDistToSeg);
					int count = Count;
					CurvySplineSegment nextSegment = GetNextSegment(curvySplineSegment2);
					while ((bool)nextSegment && distance >= nextSegment.Distance && count-- > 0)
					{
						curvySplineSegment2 = nextSegment;
						nextSegment = GetNextSegment(curvySplineSegment2);
					}
					if (count <= 0)
					{
						DTLog.LogError("[Curvy] CurvySpline.DistanceToSegment() caused a deadloop! This shouldn't happen at all. Please raise a bug report.");
					}
					curvySplineSegment = curvySplineSegment2;
					localDistance = (distance.Approximately(curvySplineSegment.Distance) ? 0f : (distance - curvySplineSegment.Distance));
					if (!Dirty)
					{
						_lastDistToSeg = curvySplineSegment;
					}
				}
			}
			else
			{
				curvySplineSegment = null;
				localDistance = -1f;
			}
			return curvySplineSegment;
		}

		public Vector3 Move(ref float tf, ref int direction, float fDistance, CurvyClamping clamping)
		{
			tf = CurvyUtility.ClampTF(tf + fDistance * (float)direction, ref direction, clamping);
			return Interpolate(tf);
		}

		public Vector3 MoveFast(ref float tf, ref int direction, float fDistance, CurvyClamping clamping)
		{
			tf = CurvyUtility.ClampTF(tf + fDistance * (float)direction, ref direction, clamping);
			return InterpolateFast(tf);
		}

		public Vector3 MoveBy(ref float tf, ref int direction, float distance, CurvyClamping clamping, float stepSize = 0.002f)
		{
			return Move(ref tf, ref direction, ExtrapolateDistanceToTF(tf, distance, stepSize), clamping);
		}

		public Vector3 MoveByFast(ref float tf, ref int direction, float distance, CurvyClamping clamping, float stepSize = 0.002f)
		{
			return MoveFast(ref tf, ref direction, ExtrapolateDistanceToTFFast(tf, distance, stepSize), clamping);
		}

		public Vector3 MoveByLengthFast(ref float tf, ref int direction, float distance, CurvyClamping clamping)
		{
			float distance2 = ClampDistance(TFToDistance(tf) + distance * (float)direction, ref direction, clamping);
			tf = DistanceToTF(distance2);
			return InterpolateFast(tf);
		}

		public Vector3 MoveByAngle(ref float tf, ref int direction, float angle, CurvyClamping clamping, float stepSize = 0.002f)
		{
			if (clamping == CurvyClamping.PingPong)
			{
				DTLog.LogError("[Curvy] MoveByAngle does not support PingPong clamping");
				return Vector3.zero;
			}
			stepSize = Mathf.Max(0.0001f, stepSize);
			float num = tf;
			InterpolateAndGetTangent(tf, out var localPosition, out var localTangent);
			Vector3 vector = Vector3.zero;
			int num2 = 10000;
			while (num2-- > 0)
			{
				tf += stepSize * (float)direction;
				if (tf > 1f)
				{
					if (clamping != CurvyClamping.Loop)
					{
						tf = 1f;
						return Interpolate(1f);
					}
					tf -= 1f;
				}
				else if (tf < 0f)
				{
					if (clamping != CurvyClamping.Loop)
					{
						tf = 0f;
						return Interpolate(0f);
					}
					tf += 1f;
				}
				vector = Interpolate(tf);
				Vector3 to = vector - localPosition;
				float num3 = Vector3.Angle(localTangent, to);
				if (num3 >= angle)
				{
					tf = num + (tf - num) * angle / num3;
					return Interpolate(tf);
				}
			}
			return vector;
		}

		public Vector3 MoveByAngleFast(ref float tf, ref int direction, float angle, CurvyClamping clamping, float stepSize)
		{
			if (clamping == CurvyClamping.PingPong)
			{
				DTLog.LogError("[Curvy] MoveByAngleFast does not support PingPong clamping");
				return Vector3.zero;
			}
			stepSize = Mathf.Max(0.0001f, stepSize);
			float num = tf;
			InterpolateAndGetTangentFast(tf, out var localPosition, out var localTangent);
			Vector3 vector = Vector3.zero;
			int num2 = 10000;
			while (num2-- > 0)
			{
				tf += stepSize * (float)direction;
				if (tf > 1f)
				{
					if (clamping != CurvyClamping.Loop)
					{
						tf = 1f;
						return InterpolateFast(1f);
					}
					tf -= 1f;
				}
				else if (tf < 0f)
				{
					if (clamping != CurvyClamping.Loop)
					{
						tf = 0f;
						return InterpolateFast(0f);
					}
					tf += 1f;
				}
				vector = InterpolateFast(tf);
				Vector3 to = vector - localPosition;
				float num3 = Vector3.Angle(localTangent, to);
				if (num3 >= angle)
				{
					tf = num + (tf - num) * angle / num3;
					return InterpolateFast(tf);
				}
			}
			return vector;
		}

		public float ClampDistance(float distance, CurvyClamping clamping)
		{
			return CurvyUtility.ClampDistance(distance, clamping, Length);
		}

		public float ClampDistance(float distance, CurvyClamping clamping, float min, float max)
		{
			return CurvyUtility.ClampDistance(distance, clamping, Length, min, max);
		}

		public float ClampDistance(float distance, ref int dir, CurvyClamping clamping)
		{
			return CurvyUtility.ClampDistance(distance, ref dir, clamping, Length);
		}

		public float ClampDistance(float distance, ref int dir, CurvyClamping clamping, float min, float max)
		{
			return CurvyUtility.ClampDistance(distance, ref dir, clamping, Length, min, max);
		}

		public CurvySplineSegment Add()
		{
			return InsertAfter(null);
		}

		public CurvySplineSegment[] Add(params Vector3[] controlPointsLocalPositions)
		{
			OnBeforeControlPointAddEvent(defaultAddAfterEventArgs);
			CurvySplineSegment[] array = new CurvySplineSegment[controlPointsLocalPositions.Length];
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			for (int i = 0; i < controlPointsLocalPositions.Length; i++)
			{
				array[i] = InsertAfter(null, localToWorldMatrix.MultiplyPoint3x4(controlPointsLocalPositions[i]), skipRefreshingAndEvents: true);
			}
			Refresh();
			OnAfterControlPointAddEvent(defaultAddAfterEventArgs);
			OnAfterControlPointChangesEvent(defaultSplineEventArgs);
			return array;
		}

		public CurvySplineSegment InsertBefore(CurvySplineSegment controlPoint, bool skipRefreshingAndEvents = false)
		{
			Vector3 globalPosition;
			if ((bool)controlPoint)
			{
				CurvySplineSegment previousControlPoint = GetPreviousControlPoint(controlPoint);
				globalPosition = ((!IsControlPointASegment(previousControlPoint)) ? (previousControlPoint ? Vector3.LerpUnclamped(previousControlPoint.transform.position, controlPoint.transform.position, 0.5f) : controlPoint.transform.position) : base.transform.localToWorldMatrix.MultiplyPoint3x4(previousControlPoint.Interpolate(0.5f)));
			}
			else
			{
				globalPosition = base.transform.position;
			}
			return InsertBefore(controlPoint, globalPosition, skipRefreshingAndEvents);
		}

		public CurvySplineSegment InsertBefore(CurvySplineSegment controlPoint, Vector3 globalPosition, bool skipRefreshingAndEvents = false)
		{
			return InsertAt(controlPoint, globalPosition, controlPoint ? Mathf.Max(0, GetControlPointIndex(controlPoint)) : 0, CurvyControlPointEventArgs.ModeEnum.AddBefore, skipRefreshingAndEvents);
		}

		public CurvySplineSegment InsertAfter(CurvySplineSegment controlPoint, bool skipRefreshingAndEvents = false)
		{
			Vector3 globalPosition;
			if ((bool)controlPoint)
			{
				if (IsControlPointASegment(controlPoint))
				{
					globalPosition = base.transform.localToWorldMatrix.MultiplyPoint3x4(controlPoint.Interpolate(0.5f));
				}
				else
				{
					CurvySplineSegment nextControlPoint = GetNextControlPoint(controlPoint);
					globalPosition = (nextControlPoint ? Vector3.LerpUnclamped(nextControlPoint.transform.position, controlPoint.transform.position, 0.5f) : controlPoint.transform.position);
				}
			}
			else
			{
				globalPosition = base.transform.position;
			}
			return InsertAfter(controlPoint, globalPosition, skipRefreshingAndEvents);
		}

		public CurvySplineSegment InsertAfter(CurvySplineSegment controlPoint, Vector3 globalPosition, bool skipRefreshingAndEvents = false)
		{
			return InsertAt(controlPoint, globalPosition, controlPoint ? (GetControlPointIndex(controlPoint) + 1) : ControlPoints.Count, CurvyControlPointEventArgs.ModeEnum.AddAfter, skipRefreshingAndEvents);
		}

		public void Clear()
		{
			OnBeforeControlPointDeleteEvent(defaultDeleteEventArgs);
			for (int num = ControlPointCount - 1; num >= 0; num--)
			{
				if (UsePooling && Application.isPlaying)
				{
					CurvyGlobalManager instance = DTSingleton<CurvyGlobalManager>.Instance;
					if (instance == null)
					{
						DTLog.LogError("[Curvy] Couldn't find Curvy Global Manager. Please raise a bug report.");
					}
					else
					{
						instance.ControlPointPool.Push(ControlPoints[num]);
					}
				}
				else
				{
					UnityEngine.Object.Destroy(controlPoints[num].gameObject);
				}
			}
			ClearControlPoints();
			Refresh();
			OnAfterControlPointChangesEvent(defaultSplineEventArgs);
		}

		public void Delete(CurvySplineSegment controlPoint, bool skipRefreshingAndEvents = false)
		{
			if (!controlPoint)
			{
				return;
			}
			if (!skipRefreshingAndEvents)
			{
				OnBeforeControlPointDeleteEvent(new CurvyControlPointEventArgs(this, this, controlPoint, CurvyControlPointEventArgs.ModeEnum.Delete));
			}
			RemoveControlPoint(controlPoint);
			controlPoint.transform.SetAsLastSibling();
			if (UsePooling && Application.isPlaying)
			{
				CurvyGlobalManager instance = DTSingleton<CurvyGlobalManager>.Instance;
				if (instance == null)
				{
					DTLog.LogError("[Curvy] Couldn't find Curvy Global Manager. Please raise a bug report.");
				}
				else
				{
					instance.ControlPointPool.Push(controlPoint);
				}
			}
			else
			{
				UnityEngine.Object.Destroy(controlPoint.gameObject);
			}
			if (!skipRefreshingAndEvents)
			{
				Refresh();
				OnAfterControlPointChangesEvent(defaultSplineEventArgs);
			}
		}

		public Vector3[] GetApproximation(Space space = Space.Self)
		{
			Vector3[] array = new Vector3[CacheSize + 1];
			int num = 0;
			for (int i = 0; i < Count; i++)
			{
				this[i].Approximation.CopyTo(array, num);
				num += Mathf.Max(0, this[i].Approximation.Length - 1);
			}
			if (space == Space.World)
			{
				Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = localToWorldMatrix.MultiplyPoint3x4(array[j]);
				}
			}
			return array;
		}

		public Vector3[] GetApproximationT()
		{
			Vector3[] array = new Vector3[CacheSize + 1];
			int num = 0;
			for (int i = 0; i < Count; i++)
			{
				this[i].ApproximationT.CopyTo(array, num);
				num += Mathf.Max(0, this[i].ApproximationT.Length - 1);
			}
			return array;
		}

		public Vector3[] GetApproximationUpVectors()
		{
			Vector3[] array = new Vector3[CacheSize + 1];
			int num = 0;
			for (int i = 0; i < Count; i++)
			{
				this[i].ApproximationUp.CopyTo(array, num);
				num += Mathf.Max(0, this[i].ApproximationUp.Length - 1);
			}
			return array;
		}

		public float GetNearestPointTF(Vector3 localPosition)
		{
			Vector3 nearest;
			return GetNearestPointTF(localPosition, out nearest, 0, -1);
		}

		public float GetNearestPointTF(Vector3 localPosition, out Vector3 nearest)
		{
			return GetNearestPointTF(localPosition, out nearest, 0, -1);
		}

		public float GetNearestPointTF(Vector3 localPosition, int startSegmentIndex = 0, int stopSegmentIndex = -1)
		{
			Vector3 nearest;
			return GetNearestPointTF(localPosition, out nearest, startSegmentIndex, stopSegmentIndex);
		}

		public float GetNearestPointTF(Vector3 localPosition, out Vector3 nearest, int startSegmentIndex = 0, int stopSegmentIndex = -1)
		{
			CurvySplineSegment nearestSegment;
			float nearestSegmentF;
			return GetNearestPointTF(localPosition, out nearest, out nearestSegment, out nearestSegmentF, startSegmentIndex, stopSegmentIndex);
		}

		public float GetNearestPointTF(Vector3 localPosition, out Vector3 nearestPoint, [CanBeNull] out CurvySplineSegment nearestSegment, out float nearestSegmentF, int startSegmentIndex = 0, int stopSegmentIndex = -1)
		{
			nearestPoint = Vector3.zero;
			if (Count == 0)
			{
				nearestSegment = null;
				nearestSegmentF = -1f;
				return -1f;
			}
			float num = float.MaxValue;
			float num2 = 0f;
			CurvySplineSegment curvySplineSegment = null;
			if (stopSegmentIndex == -1)
			{
				stopSegmentIndex = Count - 1;
			}
			startSegmentIndex = Mathf.Clamp(startSegmentIndex, 0, Count - 1);
			stopSegmentIndex = Mathf.Clamp(stopSegmentIndex + 1, startSegmentIndex + 1, Count);
			for (int i = startSegmentIndex; i < stopSegmentIndex; i++)
			{
				float nearestPointF = this[i].GetNearestPointF(localPosition);
				Vector3 vector = this[i].Interpolate(nearestPointF);
				float sqrMagnitude = (vector - localPosition).sqrMagnitude;
				if (sqrMagnitude <= num)
				{
					curvySplineSegment = this[i];
					num2 = nearestPointF;
					nearestPoint = vector;
					num = sqrMagnitude;
				}
			}
			nearestSegment = curvySplineSegment;
			nearestSegmentF = num2;
			return curvySplineSegment.LocalFToTF(num2);
		}

		public void Refresh()
		{
			ProcessDirtyControlPoints();
			OnRefreshEvent(defaultSplineEventArgs);
		}

		public void SetDirtyAll()
		{
			SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: true);
		}

		public void SetDirtyAll(SplineDirtyingType dirtyingType, bool dirtyConnectedControlPoints)
		{
			allControlPointsAreDirty = true;
			SetDirtyingFlags(dirtyingType);
			if (!dirtyConnectedControlPoints)
			{
				return;
			}
			for (int i = 0; i < ControlPoints.Count; i++)
			{
				CurvySplineSegment curvySplineSegment = ControlPoints[i];
				if (!curvySplineSegment || !curvySplineSegment.Connection)
				{
					continue;
				}
				ReadOnlyCollection<CurvySplineSegment> controlPointsList = curvySplineSegment.Connection.ControlPointsList;
				for (int j = 0; j < controlPointsList.Count; j++)
				{
					CurvySplineSegment curvySplineSegment2 = controlPointsList[j];
					CurvySpline curvySpline = ((curvySplineSegment2 != null) ? curvySplineSegment2.Spline : null);
					if ((bool)curvySpline && curvySpline != this)
					{
						curvySpline.dirtyControlPointsMinimalSet.Add(curvySplineSegment2);
						curvySpline.SetDirtyingFlags(dirtyingType);
					}
				}
			}
		}

		public void SetDirty(CurvySplineSegment dirtyControlPoint, SplineDirtyingType dirtyingType)
		{
			SetDirty(dirtyControlPoint, dirtyingType, GetPreviousControlPoint(dirtyControlPoint), GetNextControlPoint(dirtyControlPoint), ignoreConnectionOfInputControlPoint: false);
		}

		public void SetDirtyPartial(CurvySplineSegment dirtyControlPoint, SplineDirtyingType dirtyingType)
		{
			SetDirty(dirtyControlPoint, dirtyingType, GetPreviousControlPoint(dirtyControlPoint), GetNextControlPoint(dirtyControlPoint), ignoreConnectionOfInputControlPoint: true);
		}

		public Vector3 ToWorldPosition(Vector3 localPosition)
		{
			return base.transform.TransformPoint(localPosition);
		}

		public void SyncSplineFromHierarchy()
		{
			ClearControlPoints();
			for (int i = 0; i < base.transform.childCount; i++)
			{
				CurvySplineSegment component = base.transform.GetChild(i).GetComponent<CurvySplineSegment>();
				if ((bool)component)
				{
					AddControlPoint(component);
				}
			}
		}

		public Vector3[] GetApproximationPoints(float fromTF, float toTF, bool includeEndPoint = true)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = TFToSegment(fromTF, out localF);
			float frag;
			int num = curvySplineSegment.getApproximationIndexINTERNAL(localF, out frag);
			float localF2;
			CurvySplineSegment curvySplineSegment2 = TFToSegment(toTF, out localF2);
			float frag2;
			int approximationIndexINTERNAL = curvySplineSegment2.getApproximationIndexINTERNAL(localF2, out frag2);
			CurvySplineSegment curvySplineSegment3 = curvySplineSegment;
			Vector3[] array = new Vector3[1] { Vector3.Lerp(curvySplineSegment3.Approximation[num], curvySplineSegment3.Approximation[num + 1], frag) };
			while ((bool)curvySplineSegment3 && curvySplineSegment3 != curvySplineSegment2)
			{
				array = array.AddRange(curvySplineSegment3.Approximation.SubArray(num + 1, curvySplineSegment3.Approximation.Length - 1));
				num = 1;
				curvySplineSegment3 = curvySplineSegment3.Spline.GetNextSegment(curvySplineSegment3);
			}
			if ((bool)curvySplineSegment3)
			{
				int num2 = ((!(curvySplineSegment == curvySplineSegment3)) ? 1 : (num + 1));
				array = array.AddRange(curvySplineSegment3.Approximation.SubArray(num2, approximationIndexINTERNAL - num2));
				if (includeEndPoint && (frag2 > 0f || frag2 < 1f))
				{
					return array.Add(Vector3.Lerp(curvySplineSegment3.Approximation[approximationIndexINTERNAL], curvySplineSegment3.Approximation[approximationIndexINTERNAL + 1], frag2));
				}
			}
			return array;
		}

		public bool IsPlanar(out int ignoreAxis)
		{
			bool xplanar;
			bool yplanar;
			bool zplanar;
			bool result = IsPlanar(out xplanar, out yplanar, out zplanar);
			if (xplanar)
			{
				ignoreAxis = 0;
				return result;
			}
			if (yplanar)
			{
				ignoreAxis = 1;
				return result;
			}
			ignoreAxis = 2;
			return result;
		}

		public bool IsPlanar(out bool xplanar, out bool yplanar, out bool zplanar)
		{
			xplanar = true;
			yplanar = true;
			zplanar = true;
			if (ControlPointCount == 0)
			{
				return true;
			}
			Vector3 localPosition = ControlPoints[0].transform.localPosition;
			for (int i = 1; i < ControlPointCount; i++)
			{
				if (!Mathf.Approximately(ControlPoints[i].transform.localPosition.x, localPosition.x))
				{
					xplanar = false;
				}
				if (!Mathf.Approximately(ControlPoints[i].transform.localPosition.y, localPosition.y))
				{
					yplanar = false;
				}
				if (!Mathf.Approximately(ControlPoints[i].transform.localPosition.z, localPosition.z))
				{
					zplanar = false;
				}
				if (!xplanar && !yplanar && !zplanar)
				{
					return false;
				}
			}
			return true;
		}

		public bool IsPlanar(CurvyPlane plane)
		{
			switch (plane)
			{
			case CurvyPlane.XY:
			{
				for (int j = 0; j < ControlPointCount; j++)
				{
					if (ControlPoints[j].transform.localPosition.z != 0f)
					{
						return false;
					}
				}
				break;
			}
			case CurvyPlane.XZ:
			{
				for (int k = 0; k < ControlPointCount; k++)
				{
					if (ControlPoints[k].transform.localPosition.y != 0f)
					{
						return false;
					}
				}
				break;
			}
			case CurvyPlane.YZ:
			{
				for (int i = 0; i < ControlPointCount; i++)
				{
					if (ControlPoints[i].transform.localPosition.x != 0f)
					{
						return false;
					}
				}
				break;
			}
			}
			return true;
		}

		public void MakePlanar(CurvyPlane plane)
		{
			switch (plane)
			{
			case CurvyPlane.XY:
			{
				for (int j = 0; j < ControlPointCount; j++)
				{
					if (ControlPoints[j].transform.localPosition.z != 0f)
					{
						ControlPoints[j].SetLocalPosition(new Vector3(ControlPoints[j].transform.localPosition.x, ControlPoints[j].transform.localPosition.y, 0f));
					}
				}
				break;
			}
			case CurvyPlane.XZ:
			{
				for (int k = 0; k < ControlPointCount; k++)
				{
					if (ControlPoints[k].transform.localPosition.y != 0f)
					{
						ControlPoints[k].SetLocalPosition(new Vector3(ControlPoints[k].transform.localPosition.x, 0f, ControlPoints[k].transform.localPosition.z));
					}
				}
				break;
			}
			case CurvyPlane.YZ:
			{
				for (int i = 0; i < ControlPointCount; i++)
				{
					if (ControlPoints[i].transform.localPosition.x != 0f)
					{
						ControlPoints[i].SetLocalPosition(new Vector3(0f, ControlPoints[i].transform.localPosition.y, ControlPoints[i].transform.localPosition.z));
					}
				}
				break;
			}
			default:
				throw new NotImplementedException();
			}
		}

		public void Subdivide(CurvySplineSegment fromCP = null, CurvySplineSegment toCP = null)
		{
			if (!fromCP)
			{
				fromCP = FirstVisibleControlPoint;
			}
			if (!toCP)
			{
				toCP = LastVisibleControlPoint;
			}
			if (fromCP == null || toCP == null || fromCP.Spline != this || toCP.Spline != this)
			{
				Debug.Log("CurvySpline.Subdivide: Not a valid range selection!");
				return;
			}
			int num = Mathf.Clamp(fromCP.Spline.GetControlPointIndex(fromCP), 0, ControlPointCount - 2);
			int num2 = Mathf.Clamp(toCP.Spline.GetControlPointIndex(toCP), num + 1, ControlPointCount - 1);
			if (num2 - num < 1)
			{
				Debug.Log("CurvySpline.Subdivide: Not a valid range selection!");
				return;
			}
			OnBeforeControlPointAddEvent(defaultAddAfterEventArgs);
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			for (int num3 = num2 - 1; num3 >= num; num3--)
			{
				CurvySplineSegment curvySplineSegment = ControlPoints[num3];
				CurvySplineSegment curvySplineSegment2 = ControlPoints[num3 + 1];
				CurvySplineSegment curvySplineSegment3 = InsertAfter(ControlPoints[num3], localToWorldMatrix.MultiplyPoint3x4(ControlPoints[num3].Interpolate(0.5f)), skipRefreshingAndEvents: true);
				if (Interpolation == CurvyInterpolation.Bezier)
				{
					Vector3 position = curvySplineSegment.transform.position;
					Vector3 handleOutPosition = curvySplineSegment.HandleOutPosition;
					Vector3 handleInPosition = curvySplineSegment2.HandleInPosition;
					Vector3 position2 = curvySplineSegment2.transform.position;
					Vector3 vector = (position + handleOutPosition) / 2f;
					Vector3 vector2 = (handleOutPosition + handleInPosition) / 2f;
					Vector3 vector3 = (handleInPosition + position2) / 2f;
					Vector3 handleInPosition2 = (vector + vector2) / 2f;
					Vector3 handleOutPosition2 = (vector2 + vector3) / 2f;
					curvySplineSegment.AutoHandles = false;
					curvySplineSegment.HandleOutPosition = vector;
					curvySplineSegment2.AutoHandles = false;
					curvySplineSegment2.HandleInPosition = vector3;
					curvySplineSegment3.AutoHandles = false;
					curvySplineSegment3.HandleInPosition = handleInPosition2;
					curvySplineSegment3.HandleOutPosition = handleOutPosition2;
				}
			}
			Refresh();
			OnAfterControlPointAddEvent(defaultAddAfterEventArgs);
			OnAfterControlPointChangesEvent(defaultSplineEventArgs);
		}

		public void Simplify(CurvySplineSegment fromCP = null, CurvySplineSegment toCP = null)
		{
			if (!fromCP)
			{
				fromCP = FirstVisibleControlPoint;
			}
			if (!toCP)
			{
				toCP = LastVisibleControlPoint;
			}
			if (fromCP == null || toCP == null || fromCP.Spline != this || toCP.Spline != this)
			{
				Debug.Log("CurvySpline.Simplify: Not a valid range selection!");
				return;
			}
			int num = Mathf.Clamp(fromCP.Spline.GetControlPointIndex(fromCP), 0, ControlPointCount - 2);
			int num2 = Mathf.Clamp(toCP.Spline.GetControlPointIndex(toCP), num + 2, ControlPointCount - 1);
			if (num2 - num < 2)
			{
				Debug.Log("CurvySpline.Simplify: Not a valid range selection!");
				return;
			}
			OnBeforeControlPointDeleteEvent(defaultDeleteEventArgs);
			for (int num3 = num2 - 2; num3 >= num; num3 -= 2)
			{
				Delete(ControlPoints[num3 + 1], skipRefreshingAndEvents: true);
			}
			Refresh();
			OnAfterControlPointChangesEvent(defaultSplineEventArgs);
		}

		public void Equalize(CurvySplineSegment fromCP = null, CurvySplineSegment toCP = null)
		{
			if (!fromCP)
			{
				fromCP = FirstVisibleControlPoint;
			}
			if (!toCP)
			{
				toCP = LastVisibleControlPoint;
			}
			if (fromCP == null || toCP == null || fromCP.Spline != this || toCP.Spline != this)
			{
				Debug.Log("CurvySpline.Equalize: Not a valid range selection!");
				return;
			}
			int num = Mathf.Clamp(fromCP.Spline.GetControlPointIndex(fromCP), 0, ControlPointCount - 2);
			int num2 = Mathf.Clamp(toCP.Spline.GetControlPointIndex(toCP), num + 2, ControlPointCount - 1);
			if (num2 - num < 2)
			{
				Debug.Log("CurvySpline.Equalize: Not a valid range selection!");
				return;
			}
			float num3 = (ControlPoints[num2].Distance - ControlPoints[num].Distance) / (float)(num2 - num);
			float num4 = ControlPoints[num].Distance;
			for (int i = num + 1; i < num2; i++)
			{
				num4 += num3;
				ControlPoints[i].SetLocalPosition(InterpolateByDistance(num4));
			}
		}

		public void Normalize()
		{
			Vector3 localScale = base.transform.localScale;
			if (localScale != Vector3.one)
			{
				base.transform.localScale = Vector3.one;
				for (int i = 0; i < ControlPointCount; i++)
				{
					CurvySplineSegment curvySplineSegment = ControlPoints[i];
					curvySplineSegment.SetLocalPosition(Vector3.Scale(curvySplineSegment.transform.localPosition, localScale));
					curvySplineSegment.HandleIn = Vector3.Scale(curvySplineSegment.HandleIn, localScale);
					curvySplineSegment.HandleOut = Vector3.Scale(curvySplineSegment.HandleOut, localScale);
				}
			}
		}

		public void MakePlanar(int axis)
		{
			Vector3 localPosition = ControlPoints[0].transform.localPosition;
			for (int i = 1; i < ControlPointCount; i++)
			{
				Vector3 localPosition2 = ControlPoints[i].transform.localPosition;
				switch (axis)
				{
				case 0:
					localPosition2.x = localPosition.x;
					break;
				case 1:
					localPosition2.y = localPosition.y;
					break;
				case 2:
					localPosition2.z = localPosition.z;
					break;
				}
				ControlPoints[i].transform.localPosition = localPosition2;
			}
			SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: true);
		}

		public Vector3 SetPivot(float xRel = 0f, float yRel = 0f, float zRel = 0f, bool preview = false)
		{
			Bounds bounds = Bounds;
			Vector3 vector = new Vector3(bounds.min.x + bounds.size.x * ((xRel + 1f) / 2f), bounds.max.y - bounds.size.y * ((yRel + 1f) / 2f), bounds.min.z + bounds.size.z * ((zRel + 1f) / 2f));
			Vector3 vector2 = base.transform.position - vector;
			if (preview)
			{
				return base.transform.position - vector2;
			}
			for (int i = 0; i < ControlPoints.Count; i++)
			{
				ControlPoints[i].transform.position += vector2;
			}
			base.transform.position -= vector2;
			SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: true);
			return base.transform.position;
		}

		public void Flip()
		{
			if (ControlPointCount <= 1)
			{
				return;
			}
			switch (Interpolation)
			{
			case CurvyInterpolation.TCB:
			{
				Bias *= -1f;
				for (int num2 = ControlPointCount - 1; num2 >= 0; num2--)
				{
					CurvySplineSegment curvySplineSegment2 = ControlPoints[num2];
					int num3 = num2 - 1;
					if (num3 >= 0)
					{
						CurvySplineSegment curvySplineSegment3 = ControlPoints[num3];
						curvySplineSegment2.EndBias = curvySplineSegment3.StartBias * -1f;
						curvySplineSegment2.EndContinuity = curvySplineSegment3.StartContinuity;
						curvySplineSegment2.EndTension = curvySplineSegment3.StartTension;
						curvySplineSegment2.StartBias = curvySplineSegment3.EndBias * -1f;
						curvySplineSegment2.StartContinuity = curvySplineSegment3.EndContinuity;
						curvySplineSegment2.StartTension = curvySplineSegment3.EndTension;
						curvySplineSegment2.OverrideGlobalBias = curvySplineSegment3.OverrideGlobalBias;
						curvySplineSegment2.OverrideGlobalContinuity = curvySplineSegment3.OverrideGlobalContinuity;
						curvySplineSegment2.OverrideGlobalTension = curvySplineSegment3.OverrideGlobalTension;
						curvySplineSegment2.SynchronizeTCB = curvySplineSegment3.SynchronizeTCB;
					}
				}
				break;
			}
			case CurvyInterpolation.Bezier:
			{
				for (int num = ControlPointCount - 1; num >= 0; num--)
				{
					CurvySplineSegment curvySplineSegment = ControlPoints[num];
					Vector3 handleIn = curvySplineSegment.HandleIn;
					curvySplineSegment.HandleIn = curvySplineSegment.HandleOut;
					curvySplineSegment.HandleOut = handleIn;
				}
				break;
			}
			}
			ReverseControlPoints();
			Refresh();
		}

		public void MoveControlPoints(int startIndex, int count, CurvySplineSegment destCP)
		{
			if ((bool)destCP && !(this == destCP.Spline) && destCP.Spline.GetControlPointIndex(destCP) != -1)
			{
				startIndex = Mathf.Clamp(startIndex, 0, ControlPointCount - 1);
				count = Mathf.Clamp(count, startIndex, ControlPointCount - startIndex);
				for (int i = 0; i < count; i++)
				{
					CurvySplineSegment curvySplineSegment = ControlPoints[startIndex];
					RemoveControlPoint(curvySplineSegment);
					curvySplineSegment.transform.SetParent(destCP.Spline.transform, worldPositionStays: true);
					destCP.Spline.InsertControlPoint(destCP.Spline.GetControlPointIndex(destCP) + i + 1, curvySplineSegment);
				}
				Refresh();
				destCP.Spline.Refresh();
			}
		}

		public void JoinWith(CurvySplineSegment destCP)
		{
			if (!(destCP.Spline == this))
			{
				MoveControlPoints(0, ControlPointCount, destCP);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		public CurvySpline Split(CurvySplineSegment controlPoint)
		{
			CurvySpline curvySpline = Create(this);
			curvySpline.transform.SetParent(base.transform.parent, worldPositionStays: true);
			curvySpline.name = base.name + "_parted";
			int segmentIndex = GetSegmentIndex(controlPoint);
			List<CurvySplineSegment> list = new List<CurvySplineSegment>(ControlPointCount - segmentIndex);
			for (int i = segmentIndex; i < ControlPointCount; i++)
			{
				list.Add(ControlPoints[i]);
			}
			for (int j = 0; j < list.Count; j++)
			{
				CurvySplineSegment curvySplineSegment = list[j];
				RemoveControlPoint(curvySplineSegment);
				if (Application.isPlaying)
				{
					curvySplineSegment.transform.SetParent(curvySpline.transform, worldPositionStays: true);
				}
				curvySpline.AddControlPoint(curvySplineSegment);
			}
			Refresh();
			curvySpline.Refresh();
			return curvySpline;
		}

		public void SetFirstControlPoint(CurvySplineSegment controlPoint)
		{
			short controlPointIndex = GetControlPointIndex(controlPoint);
			CurvySplineSegment[] array = new CurvySplineSegment[controlPointIndex];
			for (int i = 0; i < controlPointIndex; i++)
			{
				array[i] = ControlPoints[i];
			}
			foreach (CurvySplineSegment item in array)
			{
				RemoveControlPoint(item);
				AddControlPoint(item);
			}
			Refresh();
		}

		public bool IsControlPointAnOrientationAnchor(CurvySplineSegment controlPoint)
		{
			if (IsControlPointVisible(controlPoint))
			{
				if (!controlPoint.SerializedOrientationAnchor)
				{
					if (!(controlPoint == FirstVisibleControlPoint))
					{
						return controlPoint == LastVisibleControlPoint;
					}
					return true;
				}
				return true;
			}
			return false;
		}

		public bool CanControlPointHaveFollowUp(CurvySplineSegment controlPoint)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			return controlPoint.GetExtrinsicPropertiesINTERNAL().CanHaveFollowUp;
		}

		public short GetControlPointIndex(CurvySplineSegment controlPoint)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			return controlPoint.GetExtrinsicPropertiesINTERNAL().ControlPointIndex;
		}

		public short GetSegmentIndex(CurvySplineSegment segment)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			return segment.GetExtrinsicPropertiesINTERNAL().SegmentIndex;
		}

		[CanBeNull]
		public CurvySplineSegment GetNextControlPoint(CurvySplineSegment controlPoint)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			short nextControlPointIndex = controlPoint.GetExtrinsicPropertiesINTERNAL().NextControlPointIndex;
			if (nextControlPointIndex != -1)
			{
				return ControlPoints[nextControlPointIndex];
			}
			return null;
		}

		[CanBeNull]
		public short GetNextControlPointIndex(CurvySplineSegment controlPoint)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			return controlPoint.GetExtrinsicPropertiesINTERNAL().NextControlPointIndex;
		}

		[CanBeNull]
		public CurvySplineSegment GetNextControlPointUsingFollowUp(CurvySplineSegment controlPoint)
		{
			if (!(controlPoint.FollowUp != null) || !(LastVisibleControlPoint == controlPoint))
			{
				return GetNextControlPoint(controlPoint);
			}
			return GetFollowUpNextControlPoint(controlPoint.FollowUp, controlPoint.FollowUpHeading);
		}

		[CanBeNull]
		public CurvySplineSegment GetPreviousControlPoint(CurvySplineSegment controlPoint)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			short previousControlPointIndex = controlPoint.GetExtrinsicPropertiesINTERNAL().PreviousControlPointIndex;
			if (previousControlPointIndex != -1)
			{
				return ControlPoints[previousControlPointIndex];
			}
			return null;
		}

		[CanBeNull]
		public short GetPreviousControlPointIndex(CurvySplineSegment controlPoint)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			return controlPoint.GetExtrinsicPropertiesINTERNAL().PreviousControlPointIndex;
		}

		[CanBeNull]
		public CurvySplineSegment GetPreviousControlPointUsingFollowUp(CurvySplineSegment controlPoint)
		{
			if (!(controlPoint.FollowUp != null) || !(FirstVisibleControlPoint == controlPoint))
			{
				return GetPreviousControlPoint(controlPoint);
			}
			return GetFollowUpNextControlPoint(controlPoint.FollowUp, controlPoint.FollowUpHeading);
		}

		[CanBeNull]
		public CurvySplineSegment GetNextSegment(CurvySplineSegment segment)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			CurvySplineSegment.ControlPointExtrinsicProperties extrinsicPropertiesINTERNAL = segment.GetExtrinsicPropertiesINTERNAL();
			if (!extrinsicPropertiesINTERNAL.NextControlPointIsSegment)
			{
				return null;
			}
			return ControlPoints[extrinsicPropertiesINTERNAL.NextControlPointIndex];
		}

		[CanBeNull]
		public CurvySplineSegment GetPreviousSegment(CurvySplineSegment segment)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			CurvySplineSegment.ControlPointExtrinsicProperties extrinsicPropertiesINTERNAL = segment.GetExtrinsicPropertiesINTERNAL();
			if (!extrinsicPropertiesINTERNAL.PreviousControlPointIsSegment)
			{
				return null;
			}
			return ControlPoints[extrinsicPropertiesINTERNAL.PreviousControlPointIndex];
		}

		public bool IsControlPointASegment(CurvySplineSegment controlPoint)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			return controlPoint.GetExtrinsicPropertiesINTERNAL().IsSegment;
		}

		public bool IsControlPointVisible(CurvySplineSegment controlPoint)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			return controlPoint.GetExtrinsicPropertiesINTERNAL().IsVisible;
		}

		public short GetControlPointOrientationAnchorIndex(CurvySplineSegment controlPoint)
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			return controlPoint.GetExtrinsicPropertiesINTERNAL().OrientationAnchorIndex;
		}

		public void SetFromString(string fieldAndValue)
		{
			string[] array = fieldAndValue.Split('=');
			if (array.Length != 2)
			{
				return;
			}
			FieldInfo fieldInfo = GetType().FieldByName(array[0], includeInherited: true);
			if (fieldInfo != null)
			{
				try
				{
					if (fieldInfo.FieldType.IsEnum)
					{
						fieldInfo.SetValue(this, Enum.Parse(fieldInfo.FieldType, array[1]));
					}
					else
					{
						fieldInfo.SetValue(this, Convert.ChangeType(array[1], fieldInfo.FieldType, CultureInfo.InvariantCulture));
					}
					return;
				}
				catch (Exception ex)
				{
					Debug.LogWarning(base.name + ".SetFromString(): " + ex.ToString());
					return;
				}
			}
			PropertyInfo propertyInfo = GetType().PropertyByName(array[0], includeInherited: true);
			if (!(propertyInfo != null))
			{
				return;
			}
			try
			{
				if (propertyInfo.PropertyType.IsEnum)
				{
					propertyInfo.SetValue(this, Enum.Parse(propertyInfo.PropertyType, array[1]), null);
				}
				else
				{
					propertyInfo.SetValue(this, Convert.ChangeType(array[1], propertyInfo.PropertyType, CultureInfo.InvariantCulture), null);
				}
			}
			catch (Exception ex2)
			{
				Debug.LogWarning(base.name + ".SetFromString(): " + ex2.ToString());
			}
		}

		private void Awake()
		{
			if (UsePooling)
			{
				_ = DTSingleton<CurvyGlobalManager>.Instance;
			}
		}

		private void OnEnable()
		{
			SyncSplineFromHierarchy();
			if (isStarted)
			{
				Initialize();
			}
		}

		public void Start()
		{
			if (!isStarted)
			{
				Initialize();
				isStarted = true;
			}
			Refresh();
		}

		private void OnDisable()
		{
			mIsInitialized = false;
		}

		private void OnDestroy()
		{
			if (true)
			{
				if (UsePooling && Application.isPlaying)
				{
					CurvyGlobalManager instance = DTSingleton<CurvyGlobalManager>.Instance;
					if (instance != null)
					{
						for (int i = 0; i < ControlPointCount; i++)
						{
							instance.ControlPointPool.Push(ControlPoints[i]);
						}
					}
				}
				else
				{
					mThreadWorker.Dispose();
				}
			}
			ClearControlPoints();
			isStarted = false;
		}

		protected virtual void Reset()
		{
			Interpolation = CurvyGlobalManager.DefaultInterpolation;
			RestrictTo2D = false;
			AutoHandleDistance = 0.39f;
			Closed = false;
			AutoEndTangents = true;
			Orientation = CurvyOrientation.Dynamic;
			GizmoColor = CurvyGlobalManager.DefaultGizmoColor;
			GizmoSelectionColor = CurvyGlobalManager.DefaultGizmoSelectionColor;
			CacheDensity = 50;
			MaxPointsPerUnit = 8f;
			CheckTransform = true;
			Tension = 0f;
			Continuity = 0f;
			Bias = 0f;
			SyncSplineFromHierarchy();
		}

		private void Update()
		{
			if (Application.isPlaying && UpdateIn == CurvyUpdateMethod.Update)
			{
				doUpdate();
			}
		}

		private void LateUpdate()
		{
			if (Application.isPlaying && UpdateIn == CurvyUpdateMethod.LateUpdate)
			{
				doUpdate();
			}
		}

		private void FixedUpdate()
		{
			if (Application.isPlaying && UpdateIn == CurvyUpdateMethod.FixedUpdate)
			{
				doUpdate();
			}
		}

		private void Initialize()
		{
			SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: false);
			ProcessDirtyControlPoints();
			UpdatedLastProcessedGlobalCoordinates();
			mIsInitialized = true;
		}

		private void doUpdate()
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			globalCoordinatesChangedThisFrame = false;
			if (base.transform.hasChanged)
			{
				base.transform.hasChanged = false;
				if (base.transform.position.NotApproximately(lastProcessedPosition) || base.transform.rotation.DifferentOrientation(lastProcessedRotation) || base.transform.lossyScale != lastProcessedScale)
				{
					globalCoordinatesChangedThisFrame = true;
					UpdatedLastProcessedGlobalCoordinates();
					mBounds = null;
					for (int i = 0; i < ControlPointCount; i++)
					{
						ControlPoints[i].ClearBoundsINTERNAL();
					}
				}
			}
			if ((CheckTransform || !Application.isPlaying) && !allControlPointsAreDirty)
			{
				for (int j = 0; j < ControlPointCount; j++)
				{
					CurvySplineSegment curvySplineSegment = ControlPoints[j];
					bool hasUnprocessedLocalPosition = curvySplineSegment.HasUnprocessedLocalPosition;
					if (hasUnprocessedLocalPosition || (curvySplineSegment.HasUnprocessedLocalOrientation && curvySplineSegment.OrientatinInfluencesSpline))
					{
						curvySplineSegment.Spline.SetDirty(curvySplineSegment, hasUnprocessedLocalPosition ? SplineDirtyingType.Everything : SplineDirtyingType.OrientationOnly);
					}
				}
			}
			if (Dirty)
			{
				Refresh();
			}
			else if (sendOnRefreshEventNextUpdate)
			{
				OnRefreshEvent(defaultSplineEventArgs);
			}
			sendOnRefreshEventNextUpdate = false;
			if (globalCoordinatesChangedThisFrame && OnGlobalCoordinatesChanged != null)
			{
				OnGlobalCoordinatesChanged(this);
			}
		}

		private bool canHaveManualEndCP()
		{
			if (!Closed)
			{
				if (Interpolation != CurvyInterpolation.CatmullRom)
				{
					return Interpolation == CurvyInterpolation.TCB;
				}
				return true;
			}
			return false;
		}

		private void SetDirty(CurvySplineSegment controlPoint, SplineDirtyingType dirtyingType, CurvySplineSegment previousControlPoint, CurvySplineSegment nextControlPoint, bool ignoreConnectionOfInputControlPoint)
		{
			if (!ignoreConnectionOfInputControlPoint && (bool)controlPoint.Connection)
			{
				ReadOnlyCollection<CurvySplineSegment> controlPointsList = controlPoint.Connection.ControlPointsList;
				for (int i = 0; i < controlPointsList.Count; i++)
				{
					CurvySplineSegment curvySplineSegment = controlPointsList[i];
					CurvySpline spline = curvySplineSegment.Spline;
					if ((bool)spline)
					{
						spline.dirtyControlPointsMinimalSet.Add(curvySplineSegment);
						spline.SetDirtyingFlags(dirtyingType);
					}
				}
			}
			else
			{
				dirtyControlPointsMinimalSet.Add(controlPoint);
				SetDirtyingFlags(dirtyingType);
			}
			if ((bool)previousControlPoint && (bool)previousControlPoint.Connection)
			{
				ReadOnlyCollection<CurvySplineSegment> controlPointsList2 = previousControlPoint.Connection.ControlPointsList;
				for (int j = 0; j < controlPointsList2.Count; j++)
				{
					CurvySplineSegment curvySplineSegment2 = controlPointsList2[j];
					CurvySpline spline2 = curvySplineSegment2.Spline;
					if ((bool)spline2 && curvySplineSegment2.FollowUp == previousControlPoint)
					{
						spline2.dirtyControlPointsMinimalSet.Add(curvySplineSegment2);
						spline2.SetDirtyingFlags(dirtyingType);
					}
				}
			}
			if (!nextControlPoint || !nextControlPoint.Connection)
			{
				return;
			}
			ReadOnlyCollection<CurvySplineSegment> controlPointsList3 = nextControlPoint.Connection.ControlPointsList;
			for (int k = 0; k < controlPointsList3.Count; k++)
			{
				CurvySplineSegment curvySplineSegment3 = controlPointsList3[k];
				CurvySpline spline3 = curvySplineSegment3.Spline;
				if ((bool)spline3 && curvySplineSegment3.FollowUp == nextControlPoint)
				{
					spline3.dirtyControlPointsMinimalSet.Add(curvySplineSegment3);
					spline3.SetDirtyingFlags(dirtyingType);
				}
			}
		}

		private void SetDirtyingFlags(SplineDirtyingType dirtyingType)
		{
			mDirtyCurve = mDirtyCurve || dirtyingType == SplineDirtyingType.Everything;
			mDirtyOrientation = true;
			if (mDirtyCurve)
			{
				mCacheSize = -1;
				length = -1f;
				mBounds = null;
				_lastDistToSeg = null;
			}
		}

		private void ReverseControlPoints()
		{
			ControlPoints.Reverse();
			InvalidateControlPointsRelationshipCacheINTERNAL();
			SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: true);
		}

		private static short GetNextControlPointIndex(short controlPointIndex, bool isSplineClosed, int controlPointsCount)
		{
			return (short)((controlPointIndex + 1 < controlPointsCount) ? (controlPointIndex + 1) : ((!isSplineClosed) ? (-1) : 0));
		}

		private static short GetPreviousControlPointIndex(short controlPointIndex, bool isSplineClosed, int controlPointsCount)
		{
			return (short)((controlPointIndex - 1 >= 0) ? (controlPointIndex - 1) : (isSplineClosed ? (controlPointsCount - 1) : (-1)));
		}

		private static bool IsControlPointASegment(int controlPointIndex, int controlPointCount, bool isClosed, bool notAutoEndTangentsAndIsCatmullRomOrTCB)
		{
			if (!isClosed)
			{
				if (!notAutoEndTangentsAndIsCatmullRomOrTCB)
				{
					return controlPointIndex < controlPointCount - 1;
				}
				if (controlPointIndex > 0)
				{
					return controlPointIndex < controlPointCount - 2;
				}
				return false;
			}
			return true;
		}

		[NotNull]
		private static CurvySplineSegment GetFollowUpNextControlPoint(CurvySplineSegment followUp, ConnectionHeadingEnum headToDirection)
		{
			return headToDirection.ResolveAuto(followUp) switch
			{
				ConnectionHeadingEnum.Minus => followUp.Spline.GetPreviousControlPoint(followUp), 
				ConnectionHeadingEnum.Plus => followUp.Spline.GetNextControlPoint(followUp), 
				ConnectionHeadingEnum.Sharp => followUp, 
				_ => throw new ArgumentOutOfRangeException(), 
			};
		}

		private void AddControlPoint(CurvySplineSegment item)
		{
			ControlPoints.Add(item);
			item.LinkToSpline(this);
			InvalidateControlPointsRelationshipCacheINTERNAL();
			short previousControlPointIndex = GetPreviousControlPointIndex((short)(ControlPoints.Count - 1), Closed, ControlPoints.Count);
			short nextControlPointIndex = GetNextControlPointIndex((short)(ControlPoints.Count - 1), Closed, ControlPoints.Count);
			SetDirty(item, SplineDirtyingType.Everything, (previousControlPointIndex != -1) ? ControlPoints[previousControlPointIndex] : null, (nextControlPointIndex != -1) ? ControlPoints[nextControlPointIndex] : null, ignoreConnectionOfInputControlPoint: false);
		}

		private void InsertControlPoint(int index, CurvySplineSegment item)
		{
			ControlPoints.Insert(index, item);
			item.LinkToSpline(this);
			InvalidateControlPointsRelationshipCacheINTERNAL();
			short previousControlPointIndex = GetPreviousControlPointIndex((short)index, Closed, ControlPoints.Count);
			short nextControlPointIndex = GetNextControlPointIndex((short)index, Closed, ControlPoints.Count);
			SetDirty(item, SplineDirtyingType.Everything, (previousControlPointIndex == -1) ? null : ControlPoints[previousControlPointIndex], (nextControlPointIndex == -1) ? null : ControlPoints[nextControlPointIndex], ignoreConnectionOfInputControlPoint: false);
		}

		private void RemoveControlPoint(CurvySplineSegment item)
		{
			int controlPointIndex = GetControlPointIndex(item);
			if (ControlPoints.Count == 1)
			{
				SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: true);
			}
			else
			{
				short previousControlPointIndex = GetPreviousControlPointIndex((short)controlPointIndex, Closed, ControlPoints.Count);
				short nextControlPointIndex = GetNextControlPointIndex((short)controlPointIndex, Closed, ControlPoints.Count);
				if (previousControlPointIndex != -1)
				{
					SetDirty(ControlPoints[previousControlPointIndex], SplineDirtyingType.Everything);
				}
				if (nextControlPointIndex != -1)
				{
					SetDirty(ControlPoints[nextControlPointIndex], SplineDirtyingType.Everything);
				}
			}
			ControlPoints.RemoveAt(controlPointIndex);
			dirtyControlPointsMinimalSet.Remove(item);
			if (item.Spline == this)
			{
				item.UnlinkFromSpline();
			}
			InvalidateControlPointsRelationshipCacheINTERNAL();
		}

		private void ClearControlPoints()
		{
			SetDirtyAll(SplineDirtyingType.Everything, dirtyConnectedControlPoints: true);
			for (int i = 0; i < ControlPoints.Count; i++)
			{
				CurvySplineSegment curvySplineSegment = ControlPoints[i];
				if ((bool)curvySplineSegment && curvySplineSegment.Spline == this)
				{
					curvySplineSegment.UnlinkFromSpline();
				}
			}
			ControlPoints.Clear();
			dirtyControlPointsMinimalSet.Clear();
			InvalidateControlPointsRelationshipCacheINTERNAL();
		}

		internal void InvalidateControlPointsRelationshipCacheINTERNAL()
		{
			if (isCpsRelationshipCacheValid)
			{
				lock (controlPointsRelationshipCacheLock)
				{
					isCpsRelationshipCacheValid = false;
					firstSegment = (lastSegment = (firstVisibleControlPoint = (lastVisibleControlPoint = null)));
				}
			}
		}

		private void RebuildControlPointsRelationshipCache(bool fixNonCoherentControlPoints)
		{
			lock (controlPointsRelationshipCacheLock)
			{
				if (isCpsRelationshipCacheValid)
				{
					return;
				}
				int count = ControlPoints.Count;
				mSegments.Clear();
				mSegments.Capacity = count;
				if (count > 0)
				{
					CurvySplineSegment curvySplineSegment = null;
					bool flag = false;
					CurvySplineSegment curvySplineSegment2 = null;
					CurvySplineSegment.ControlPointExtrinsicProperties extrinsicPropertiesINTERNAL = new CurvySplineSegment.ControlPointExtrinsicProperties(isVisible: false, -1, -1, -1, -1, previousControlPointIsSegment: false, nextControlPointIsSegment: false, canHaveFollowUp: false, -1);
					bool closed = Closed;
					bool flag2 = Interpolation == CurvyInterpolation.CatmullRom || Interpolation == CurvyInterpolation.TCB;
					bool notAutoEndTangentsAndIsCatmullRomOrTCB = !AutoEndTangents && flag2;
					short num = 0;
					short num2 = -1;
					for (short num3 = 0; num3 < count; num3++)
					{
						CurvySplineSegment curvySplineSegment3 = ControlPoints[num3];
						short previousControlPointIndex = GetPreviousControlPointIndex(num3, closed, count);
						short nextControlPointIndex = GetNextControlPointIndex(num3, closed, count);
						bool flag3 = IsControlPointASegment(num3, count, closed, notAutoEndTangentsAndIsCatmullRomOrTCB);
						bool flag4 = flag3 || extrinsicPropertiesINTERNAL.IsSegment;
						if (flag4 && (num2 == -1 || curvySplineSegment3.SerializedOrientationAnchor || !flag3))
						{
							num2 = num3;
						}
						bool flag5 = flag4 && (nextControlPointIndex == -1 || previousControlPointIndex == -1);
						extrinsicPropertiesINTERNAL = new CurvySplineSegment.ControlPointExtrinsicProperties(flag4, (short)(flag3 ? num : (-1)), num3, previousControlPointIndex, nextControlPointIndex, previousControlPointIndex != -1 && IsControlPointASegment(previousControlPointIndex, count, closed, notAutoEndTangentsAndIsCatmullRomOrTCB), nextControlPointIndex != -1 && IsControlPointASegment(nextControlPointIndex, count, closed, notAutoEndTangentsAndIsCatmullRomOrTCB), flag5, (short)(flag4 ? num2 : (-1)));
						curvySplineSegment3.SetExtrinsicPropertiesINTERNAL(extrinsicPropertiesINTERNAL);
						if (flag3)
						{
							mSegments.Add(curvySplineSegment3);
							num++;
							if (!flag)
							{
								flag = true;
								curvySplineSegment = curvySplineSegment3;
							}
							curvySplineSegment2 = curvySplineSegment3;
						}
						if (fixNonCoherentControlPoints && !flag5)
						{
							curvySplineSegment3.UnsetFollowUpWithoutDirtyingINTERNAL();
						}
					}
					firstSegment = curvySplineSegment;
					lastSegment = curvySplineSegment2;
					firstVisibleControlPoint = firstSegment;
					lastVisibleControlPoint = ((lastSegment != null) ? ControlPoints[lastSegment.GetExtrinsicPropertiesINTERNAL().NextControlPointIndex] : null);
				}
				else
				{
					firstSegment = (lastSegment = (firstVisibleControlPoint = (lastVisibleControlPoint = null)));
				}
				isCpsRelationshipCacheValid = true;
			}
		}

		private void ProcessDirtyControlPoints()
		{
			if (!isCpsRelationshipCacheValid)
			{
				RebuildControlPointsRelationshipCache(fixNonCoherentControlPoints: true);
			}
			FillDirtyCpsExtendedList();
			dirtyControlPointsMinimalSet.Clear();
			allControlPointsAreDirty = false;
			if (dirtyCpsExtendedList.Count > 0)
			{
				if (!mDirtyOrientation && !mDirtyCurve)
				{
					Debug.LogError("Invalid dirtying flags");
				}
				PrepareTTransforms();
				int controlPointCount = ControlPointCount;
				if (mDirtyCurve)
				{
					if (Interpolation == CurvyInterpolation.Bezier)
					{
						for (int i = 0; i < dirtyCpsExtendedList.Count; i++)
						{
							CurvySplineSegment curvySplineSegment = dirtyCpsExtendedList[i];
							if (curvySplineSegment.AutoHandles)
							{
								curvySplineSegment.SetBezierHandles(-1f, setIn: true, setOut: true, noDirtying: true);
							}
						}
					}
					if (UseThreading)
					{
						mThreadWorker.ParralelFor(refreshCurveAction, dirtyCpsExtendedList);
					}
					else
					{
						CurvyInterpolation interpolation = Interpolation;
						for (int j = 0; j < dirtyCpsExtendedList.Count; j++)
						{
							CurvySplineSegment curvySplineSegment2 = dirtyCpsExtendedList[j];
							curvySplineSegment2.refreshCurveINTERNAL(interpolation, IsControlPointASegment(curvySplineSegment2), this);
						}
					}
					if (controlPointCount > 0)
					{
						ControlPoints[0].Distance = 0f;
						for (int k = 1; k < controlPointCount; k++)
						{
							ControlPoints[k].Distance = ControlPoints[k - 1].Distance + ControlPoints[k - 1].Length;
						}
						List<CurvySplineSegment> segments = Segments;
						for (int l = 0; l < segments.Count; l++)
						{
							CurvySplineSegment curvySplineSegment3 = segments[l];
							CurvySplineSegment nextSegment = GetNextSegment(curvySplineSegment3);
							if ((bool)nextSegment)
							{
								curvySplineSegment3.ApproximationT[curvySplineSegment3.CacheSize] = nextSegment.ApproximationT[0];
							}
							else
							{
								GetNextControlPoint(curvySplineSegment3).ApproximationT[0] = curvySplineSegment3.ApproximationT[curvySplineSegment3.CacheSize];
							}
						}
					}
				}
				if (mDirtyOrientation && Count > 0)
				{
					switch (Orientation)
					{
					case CurvyOrientation.None:
					{
						for (int n = 0; n < dirtyCpsExtendedList.Count; n++)
						{
							dirtyCpsExtendedList[n].refreshOrientationNoneINTERNAL();
						}
						break;
					}
					case CurvyOrientation.Static:
						if (UseThreading)
						{
							Action<CurvySplineSegment> action = delegate(CurvySplineSegment controlPoint)
							{
								controlPoint.refreshOrientationStaticINTERNAL();
							};
							mThreadWorker.ParralelFor(action, dirtyCpsExtendedList);
						}
						else
						{
							for (int num13 = 0; num13 < dirtyCpsExtendedList.Count; num13++)
							{
								dirtyCpsExtendedList[num13].refreshOrientationStaticINTERNAL();
							}
						}
						break;
					case CurvyOrientation.Dynamic:
					{
						int num = controlPointCount + 1;
						do
						{
							CurvySplineSegment curvySplineSegment4 = dirtyCpsExtendedList[0];
							if (!IsControlPointASegment(curvySplineSegment4))
							{
								curvySplineSegment4.refreshOrientationDynamicINTERNAL(curvySplineSegment4.getOrthoUp0INTERNAL());
								dirtyCpsExtendedList.RemoveAt(0);
								continue;
							}
							short controlPointOrientationAnchorIndex = GetControlPointOrientationAnchorIndex(curvySplineSegment4);
							CurvySplineSegment curvySplineSegment5 = ControlPoints[controlPointOrientationAnchorIndex];
							int num2 = 0;
							short num3 = controlPointOrientationAnchorIndex;
							CurvySplineSegment curvySplineSegment6 = curvySplineSegment5;
							int num4 = 0;
							float num5 = 0f;
							Vector3 vector = curvySplineSegment5.getOrthoUp0INTERNAL();
							do
							{
								num2 += curvySplineSegment6.CacheSize;
								num4++;
								num5 += curvySplineSegment6.Length;
								curvySplineSegment6.refreshOrientationDynamicINTERNAL(vector);
								vector = curvySplineSegment6.ApproximationUp[curvySplineSegment6.ApproximationUp.Length - 1];
								num3 = GetNextControlPointIndex(num3, m_Closed, controlPointCount);
								curvySplineSegment6 = ControlPoints[num3];
							}
							while (!IsControlPointAnOrientationAnchor(curvySplineSegment6));
							short num6 = num3;
							float num7 = vector.AngleSigned(curvySplineSegment6.getOrthoUp0INTERNAL(), curvySplineSegment6.ApproximationT[0]) / (float)num2;
							float num8;
							switch (curvySplineSegment5.Swirl)
							{
							case CurvyOrientationSwirl.Segment:
								num8 = curvySplineSegment5.SwirlTurns * 360f;
								break;
							case CurvyOrientationSwirl.AnchorGroup:
								num8 = curvySplineSegment5.SwirlTurns * 360f / (float)num4;
								break;
							case CurvyOrientationSwirl.AnchorGroupAbs:
								num8 = curvySplineSegment5.SwirlTurns * 360f / num5;
								break;
							case CurvyOrientationSwirl.None:
								num8 = 0f;
								break;
							default:
								num8 = 0f;
								DTLog.LogError("[Curvy] Invalid Swirl value " + curvySplineSegment5.Swirl);
								break;
							}
							float num9 = num7;
							short num10 = controlPointOrientationAnchorIndex;
							bool flag = curvySplineSegment5.Swirl == CurvyOrientationSwirl.AnchorGroupAbs;
							Vector3 vector2 = curvySplineSegment5.ApproximationUp[0];
							do
							{
								CurvySplineSegment curvySplineSegment7 = ControlPoints[num10];
								float num11 = (flag ? (num7 + num8 * curvySplineSegment7.Length / (float)curvySplineSegment7.CacheSize) : (num7 + num8 / (float)curvySplineSegment7.CacheSize));
								Vector3[] approximationT = curvySplineSegment7.ApproximationT;
								Vector3[] approximationUp = curvySplineSegment7.ApproximationUp;
								int num12 = approximationUp.Length;
								approximationUp[0] = vector2;
								for (int m = 1; m < num12; m++)
								{
									approximationUp[m] = Quaternion.AngleAxis(num9, approximationT[m]) * approximationUp[m];
									num9 += num11;
								}
								vector2 = approximationUp[num12 - 1];
								dirtyCpsExtendedList.Remove(curvySplineSegment7);
								num10 = GetNextControlPointIndex(num10, m_Closed, controlPointCount);
							}
							while (num10 != num6);
						}
						while (dirtyCpsExtendedList.Count > 0 && num-- > 0);
						if (num <= 0)
						{
							DTLog.LogWarning("[Curvy] Deadloop in CurvySpline.Refresh! Please raise a bugreport!");
						}
						break;
					}
					default:
						DTLog.LogError("[Curvy] Invalid Orientation value " + Orientation);
						break;
					}
					if (!Closed)
					{
						CurvySplineSegment previousControlPoint = GetPreviousControlPoint(LastVisibleControlPoint);
						LastVisibleControlPoint.ApproximationUp[0] = previousControlPoint.ApproximationUp[previousControlPoint.CacheSize];
					}
				}
			}
			mDirtyCurve = false;
			mDirtyOrientation = false;
		}

		private void PrepareTTransforms()
		{
			int controlPointCount = ControlPointCount;
			for (int i = 0; i < controlPointCount; i++)
			{
				ControlPoints[i].PrepareThreadSafeTransfromINTERNAL();
			}
			if (Count > 0)
			{
				CurvySplineSegment previousControlPointUsingFollowUp = GetPreviousControlPointUsingFollowUp(FirstVisibleControlPoint);
				if ((bool)previousControlPointUsingFollowUp && previousControlPointUsingFollowUp.Spline != this)
				{
					previousControlPointUsingFollowUp.PrepareThreadSafeTransfromINTERNAL();
				}
				CurvySplineSegment nextControlPointUsingFollowUp = GetNextControlPointUsingFollowUp(LastVisibleControlPoint);
				if ((bool)nextControlPointUsingFollowUp && nextControlPointUsingFollowUp.Spline != this)
				{
					nextControlPointUsingFollowUp.PrepareThreadSafeTransfromINTERNAL();
				}
			}
		}

		private void FillDirtyCpsExtendedList()
		{
			int count = ControlPoints.Count;
			dirtyCpsExtendedList.Clear();
			if (allControlPointsAreDirty)
			{
				for (int i = 0; i < count; i++)
				{
					dirtyCpsExtendedList.Add(ControlPoints[i]);
				}
				return;
			}
			int count2 = dirtyControlPointsMinimalSet.Count;
			for (int j = 0; j < count2; j++)
			{
				CurvySplineSegment controlPoint = dirtyControlPointsMinimalSet.ElementAt(j);
				CurvySplineSegment previousControlPoint = GetPreviousControlPoint(controlPoint);
				if ((bool)previousControlPoint)
				{
					dirtyControlPointsMinimalSet.Add(previousControlPoint);
				}
				if (Interpolation == CurvyInterpolation.Linear)
				{
					continue;
				}
				if ((bool)previousControlPoint)
				{
					CurvySplineSegment previousControlPoint2 = GetPreviousControlPoint(previousControlPoint);
					if ((bool)previousControlPoint2)
					{
						dirtyControlPointsMinimalSet.Add(previousControlPoint2);
					}
				}
				CurvySplineSegment nextControlPoint = GetNextControlPoint(controlPoint);
				if ((bool)nextControlPoint)
				{
					dirtyControlPointsMinimalSet.Add(nextControlPoint);
				}
			}
			dirtyCpsExtendedList.AddRange(dirtyControlPointsMinimalSet);
		}

		internal void NotifyMetaDataModification()
		{
			sendOnRefreshEventNextUpdate = true;
		}

		private void SyncHierarchyFromSpline(bool renameControlPoints = true)
		{
		}

		private void UpdatedLastProcessedGlobalCoordinates()
		{
			lastProcessedPosition = base.transform.position;
			lastProcessedRotation = base.transform.rotation;
			lastProcessedScale = base.transform.lossyScale;
		}

		private CurvySplineSegment InsertAt(CurvySplineSegment controlPoint, Vector3 globalPosition, int insertionIndex, CurvyControlPointEventArgs.ModeEnum insertionMode, bool skipRefreshingAndEvents)
		{
			if (!skipRefreshingAndEvents)
			{
				OnBeforeControlPointAddEvent(new CurvyControlPointEventArgs(this, this, controlPoint, insertionMode));
			}
			CurvySplineSegment curvySplineSegment;
			GameObject gameObject;
			if (UsePooling && Application.isPlaying)
			{
				CurvyGlobalManager instance = DTSingleton<CurvyGlobalManager>.Instance;
				if (instance != null)
				{
					curvySplineSegment = instance.ControlPointPool.Pop<CurvySplineSegment>(base.transform);
					gameObject = curvySplineSegment.gameObject;
				}
				else
				{
					DTLog.LogError("[Curvy] Couldn't find Curvy Global Manager. Please raise a bug report.");
					gameObject = new GameObject("NewCP", typeof(CurvySplineSegment));
					curvySplineSegment = gameObject.GetComponent<CurvySplineSegment>();
				}
			}
			else
			{
				gameObject = new GameObject("NewCP", typeof(CurvySplineSegment));
				curvySplineSegment = gameObject.GetComponent<CurvySplineSegment>();
			}
			gameObject.layer = base.gameObject.layer;
			gameObject.transform.SetParent(base.transform);
			InsertControlPoint(insertionIndex, curvySplineSegment);
			curvySplineSegment.AutoHandleDistance = AutoHandleDistance;
			curvySplineSegment.transform.position = globalPosition;
			curvySplineSegment.transform.rotation = Quaternion.identity;
			curvySplineSegment.transform.localScale = Vector3.one;
			if (!skipRefreshingAndEvents)
			{
				Refresh();
				OnAfterControlPointAddEvent(new CurvyControlPointEventArgs(this, this, curvySplineSegment, insertionMode));
				OnAfterControlPointChangesEvent(defaultSplineEventArgs);
			}
			return curvySplineSegment;
		}

		private CurvySplineEventArgs OnRefreshEvent(CurvySplineEventArgs e)
		{
			if (OnRefresh != null)
			{
				OnRefresh.Invoke(e);
			}
			return e;
		}

		private CurvyControlPointEventArgs OnBeforeControlPointAddEvent(CurvyControlPointEventArgs e)
		{
			if (OnBeforeControlPointAdd != null)
			{
				OnBeforeControlPointAdd.Invoke(e);
			}
			return e;
		}

		private CurvyControlPointEventArgs OnAfterControlPointAddEvent(CurvyControlPointEventArgs e)
		{
			if (OnAfterControlPointAdd != null)
			{
				OnAfterControlPointAdd.Invoke(e);
			}
			return e;
		}

		private CurvyControlPointEventArgs OnBeforeControlPointDeleteEvent(CurvyControlPointEventArgs e)
		{
			if (OnBeforeControlPointDelete != null)
			{
				OnBeforeControlPointDelete.Invoke(e);
			}
			return e;
		}

		private CurvySplineEventArgs OnAfterControlPointChangesEvent(CurvySplineEventArgs e)
		{
			if (OnAfterControlPointChanges != null)
			{
				OnAfterControlPointChanges.Invoke(e);
			}
			return e;
		}
	}
}
