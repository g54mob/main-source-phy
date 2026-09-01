using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace FluffyUnderware.Curvy
{
	[ExecuteInEditMode]
	[HelpURL("https://curvyeditor.com/doclink/curvysplinesegment")]
	public class CurvySplineSegment : MonoBehaviour, IPoolable
	{
		internal readonly struct ControlPointExtrinsicProperties : IEquatable<ControlPointExtrinsicProperties>
		{
			private readonly bool isVisible;

			private readonly short segmentIndex;

			private readonly short controlPointIndex;

			private readonly short nextControlPointIndex;

			private readonly short previousControlPointIndex;

			private readonly bool previousControlPointIsSegment;

			private readonly bool nextControlPointIsSegment;

			private readonly bool canHaveFollowUp;

			private readonly short orientationAnchorIndex;

			internal bool IsVisible => isVisible;

			internal short SegmentIndex => segmentIndex;

			internal short ControlPointIndex => controlPointIndex;

			internal short NextControlPointIndex => nextControlPointIndex;

			internal short PreviousControlPointIndex => previousControlPointIndex;

			internal bool PreviousControlPointIsSegment => previousControlPointIsSegment;

			internal bool NextControlPointIsSegment => nextControlPointIsSegment;

			internal bool CanHaveFollowUp => canHaveFollowUp;

			internal bool IsSegment => SegmentIndex != -1;

			internal short OrientationAnchorIndex => orientationAnchorIndex;

			internal ControlPointExtrinsicProperties(bool isVisible, short segmentIndex, short controlPointIndex, short previousControlPointIndex, short nextControlPointIndex, bool previousControlPointIsSegment, bool nextControlPointIsSegment, bool canHaveFollowUp, short orientationAnchorIndex)
			{
				this.isVisible = isVisible;
				this.segmentIndex = segmentIndex;
				this.controlPointIndex = controlPointIndex;
				this.nextControlPointIndex = nextControlPointIndex;
				this.previousControlPointIndex = previousControlPointIndex;
				this.previousControlPointIsSegment = previousControlPointIsSegment;
				this.nextControlPointIsSegment = nextControlPointIsSegment;
				this.canHaveFollowUp = canHaveFollowUp;
				this.orientationAnchorIndex = orientationAnchorIndex;
			}

			public bool Equals(ControlPointExtrinsicProperties other)
			{
				if (IsVisible == other.IsVisible && SegmentIndex == other.SegmentIndex && ControlPointIndex == other.ControlPointIndex && NextControlPointIndex == other.NextControlPointIndex && PreviousControlPointIndex == other.PreviousControlPointIndex && PreviousControlPointIsSegment == other.PreviousControlPointIsSegment && NextControlPointIsSegment == other.NextControlPointIsSegment && CanHaveFollowUp == other.CanHaveFollowUp)
				{
					return OrientationAnchorIndex == other.OrientationAnchorIndex;
				}
				return false;
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is ControlPointExtrinsicProperties)
				{
					return Equals((ControlPointExtrinsicProperties)obj);
				}
				return false;
			}

			public override int GetHashCode()
			{
				return (((((((((((((((IsVisible.GetHashCode() * 397) ^ SegmentIndex.GetHashCode()) * 397) ^ ControlPointIndex.GetHashCode()) * 397) ^ NextControlPointIndex.GetHashCode()) * 397) ^ PreviousControlPointIndex.GetHashCode()) * 397) ^ PreviousControlPointIsSegment.GetHashCode()) * 397) ^ NextControlPointIsSegment.GetHashCode()) * 397) ^ CanHaveFollowUp.GetHashCode()) * 397) ^ OrientationAnchorIndex.GetHashCode();
			}

			public static bool operator ==(ControlPointExtrinsicProperties left, ControlPointExtrinsicProperties right)
			{
				return left.Equals(right);
			}

			public static bool operator !=(ControlPointExtrinsicProperties left, ControlPointExtrinsicProperties right)
			{
				return !left.Equals(right);
			}
		}

		public static readonly Color GizmoTangentColor = new Color(0f, 0.7f, 0f);

		[NonSerialized]
		public Vector3[] Approximation = new Vector3[0];

		[NonSerialized]
		public float[] ApproximationDistances = new float[0];

		[NonSerialized]
		public Vector3[] ApproximationUp = new Vector3[0];

		[NonSerialized]
		public Vector3[] ApproximationT = new Vector3[0];

		[Group("General")]
		[FieldAction("CBBakeOrientation", ActionAttribute.ActionEnum.Callback, Position = ActionAttribute.ActionPositionEnum.Below)]
		[Label("Bake Orientation", "Automatically apply orientation to CP transforms?")]
		[SerializeField]
		private bool m_AutoBakeOrientation;

		[Group("General")]
		[Tooltip("Check to use this transform's rotation")]
		[FieldCondition("IsOrientationAnchorEditable", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private bool m_OrientationAnchor;

		[Label("Swirl", "Add Swirl to orientation?")]
		[Group("General")]
		[FieldCondition("canHaveSwirl", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private CurvyOrientationSwirl m_Swirl;

		[Label("Turns", "Number of swirl turns")]
		[Group("General")]
		[FieldCondition("canHaveSwirl", true, false, ConditionalAttribute.OperatorEnum.AND, "m_Swirl", CurvyOrientationSwirl.None, true)]
		[SerializeField]
		private float m_SwirlTurns;

		[Section("Bezier Options", true, false, 100, Sort = 1, HelpURL = "https://curvyeditor.com/doclink/curvysplinesegment_bezier")]
		[GroupCondition("interpolation", CurvyInterpolation.Bezier, false)]
		[SerializeField]
		private bool m_AutoHandles = true;

		[RangeEx(0f, 1f, "Distance %", "Handle length by distance to neighbours")]
		[FieldCondition("m_AutoHandles", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		[SerializeField]
		private float m_AutoHandleDistance = 0.39f;

		[VectorEx("", "", Precision = 3, Options = (AttributeOptionsFlags)1152, Color = "#FFFF00")]
		[SerializeField]
		[FormerlySerializedAs("HandleIn")]
		private Vector3 m_HandleIn = CurvySplineSegmentDefaultValues.HandleIn;

		[VectorEx("", "", Precision = 3, Options = (AttributeOptionsFlags)1152, Color = "#00FF00")]
		[SerializeField]
		[FormerlySerializedAs("HandleOut")]
		private Vector3 m_HandleOut = CurvySplineSegmentDefaultValues.HandleOut;

		[Section("TCB Options", true, false, 100, Sort = 1, HelpURL = "https://curvyeditor.com/doclink/curvysplinesegment_tcb")]
		[GroupCondition("interpolation", CurvyInterpolation.TCB, false)]
		[GroupAction("TCBOptionsGUI", ActionAttribute.ActionEnum.Callback, Position = ActionAttribute.ActionPositionEnum.Below)]
		[Label("Local Tension", "Override Spline Tension?")]
		[SerializeField]
		[FormerlySerializedAs("OverrideGlobalTension")]
		private bool m_OverrideGlobalTension;

		[Label("Local Continuity", "Override Spline Continuity?")]
		[SerializeField]
		[FormerlySerializedAs("OverrideGlobalContinuity")]
		private bool m_OverrideGlobalContinuity;

		[Label("Local Bias", "Override Spline Bias?")]
		[SerializeField]
		[FormerlySerializedAs("OverrideGlobalBias")]
		private bool m_OverrideGlobalBias;

		[Tooltip("Synchronize Start and End Values")]
		[SerializeField]
		[FormerlySerializedAs("SynchronizeTCB")]
		private bool m_SynchronizeTCB = true;

		[Label("Tension", "")]
		[FieldCondition("m_OverrideGlobalTension", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[FormerlySerializedAs("StartTension")]
		private float m_StartTension;

		[Label("Tension (End)", "")]
		[FieldCondition("m_OverrideGlobalTension", true, false, ConditionalAttribute.OperatorEnum.AND, "m_SynchronizeTCB", false, false)]
		[SerializeField]
		[FormerlySerializedAs("EndTension")]
		private float m_EndTension;

		[Label("Continuity", "")]
		[FieldCondition("m_OverrideGlobalContinuity", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[FormerlySerializedAs("StartContinuity")]
		private float m_StartContinuity;

		[Label("Continuity (End)", "")]
		[FieldCondition("m_OverrideGlobalContinuity", true, false, ConditionalAttribute.OperatorEnum.AND, "m_SynchronizeTCB", false, false)]
		[SerializeField]
		[FormerlySerializedAs("EndContinuity")]
		private float m_EndContinuity;

		[Label("Bias", "")]
		[FieldCondition("m_OverrideGlobalBias", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[FormerlySerializedAs("StartBias")]
		private float m_StartBias;

		[Label("Bias (End)", "")]
		[FieldCondition("m_OverrideGlobalBias", true, false, ConditionalAttribute.OperatorEnum.AND, "m_SynchronizeTCB", false, false)]
		[SerializeField]
		[FormerlySerializedAs("EndBias")]
		private float m_EndBias;

		[SerializeField]
		[HideInInspector]
		private CurvySplineSegment m_FollowUp;

		[SerializeField]
		[HideInInspector]
		private ConnectionHeadingEnum m_FollowUpHeading = ConnectionHeadingEnum.Auto;

		[SerializeField]
		[HideInInspector]
		private bool m_ConnectionSyncPosition;

		[SerializeField]
		[HideInInspector]
		private bool m_ConnectionSyncRotation;

		[SerializeField]
		[HideInInspector]
		private CurvyConnection m_Connection;

		private int cacheSize = -1;

		private Vector3 threadSafeLocalPosition;

		private Quaternion threadSafeLocalRotation;

		private CurvySpline mSpline;

		private float mStepSize;

		private Bounds? mBounds;

		private int mCacheLastDistanceToLocalFIndex;

		private readonly HashSet<Component> mMetaData = new HashSet<Component>();

		private Vector3 lastProcessedLocalPosition;

		private Quaternion lastProcessedLocalRotation;

		private ControlPointExtrinsicProperties extrinsicPropertiesINTERNAL;

		public bool AutoBakeOrientation
		{
			get
			{
				return m_AutoBakeOrientation;
			}
			set
			{
				if (m_AutoBakeOrientation != value)
				{
					m_AutoBakeOrientation = value;
				}
			}
		}

		public bool SerializedOrientationAnchor
		{
			get
			{
				return m_OrientationAnchor;
			}
			set
			{
				if (m_OrientationAnchor != value)
				{
					m_OrientationAnchor = value;
					Spline.SetDirty(this, SplineDirtyingType.OrientationOnly);
					Spline.InvalidateControlPointsRelationshipCacheINTERNAL();
				}
			}
		}

		public CurvyOrientationSwirl Swirl
		{
			get
			{
				return m_Swirl;
			}
			set
			{
				if (m_Swirl != value)
				{
					m_Swirl = value;
					Spline.SetDirty(this, SplineDirtyingType.OrientationOnly);
				}
			}
		}

		public float SwirlTurns
		{
			get
			{
				return m_SwirlTurns;
			}
			set
			{
				if (m_SwirlTurns != value)
				{
					m_SwirlTurns = value;
					Spline.SetDirty(this, SplineDirtyingType.OrientationOnly);
				}
			}
		}

		public Vector3 HandleIn
		{
			get
			{
				return m_HandleIn;
			}
			set
			{
				if (m_HandleIn != value)
				{
					m_HandleIn = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public Vector3 HandleOut
		{
			get
			{
				return m_HandleOut;
			}
			set
			{
				if (m_HandleOut != value)
				{
					m_HandleOut = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public Vector3 HandleInPosition
		{
			get
			{
				return base.transform.position + Spline.transform.rotation * HandleIn;
			}
			set
			{
				HandleIn = Spline.transform.InverseTransformDirection(value - base.transform.position);
			}
		}

		public Vector3 HandleOutPosition
		{
			get
			{
				return base.transform.position + Spline.transform.rotation * HandleOut;
			}
			set
			{
				HandleOut = Spline.transform.InverseTransformDirection(value - base.transform.position);
			}
		}

		public bool AutoHandles
		{
			get
			{
				return m_AutoHandles;
			}
			set
			{
				if (SetAutoHandles(value))
				{
					Spline.SetDirty(this, SplineDirtyingType.Everything);
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
				if (m_AutoHandleDistance != value)
				{
					float num = Mathf.Clamp01(value);
					if (m_AutoHandleDistance != num)
					{
						m_AutoHandleDistance = num;
						Spline.SetDirty(this, SplineDirtyingType.Everything);
					}
				}
			}
		}

		public bool SynchronizeTCB
		{
			get
			{
				return m_SynchronizeTCB;
			}
			set
			{
				if (m_SynchronizeTCB != value)
				{
					m_SynchronizeTCB = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public bool OverrideGlobalTension
		{
			get
			{
				return m_OverrideGlobalTension;
			}
			set
			{
				if (m_OverrideGlobalTension != value)
				{
					m_OverrideGlobalTension = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public bool OverrideGlobalContinuity
		{
			get
			{
				return m_OverrideGlobalContinuity;
			}
			set
			{
				if (m_OverrideGlobalContinuity != value)
				{
					m_OverrideGlobalContinuity = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public bool OverrideGlobalBias
		{
			get
			{
				return m_OverrideGlobalBias;
			}
			set
			{
				if (m_OverrideGlobalBias != value)
				{
					m_OverrideGlobalBias = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float StartTension
		{
			get
			{
				return m_StartTension;
			}
			set
			{
				if (m_StartTension != value)
				{
					m_StartTension = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float StartContinuity
		{
			get
			{
				return m_StartContinuity;
			}
			set
			{
				if (m_StartContinuity != value)
				{
					m_StartContinuity = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float StartBias
		{
			get
			{
				return m_StartBias;
			}
			set
			{
				if (m_StartBias != value)
				{
					m_StartBias = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float EndTension
		{
			get
			{
				return m_EndTension;
			}
			set
			{
				if (m_EndTension != value)
				{
					m_EndTension = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float EndContinuity
		{
			get
			{
				return m_EndContinuity;
			}
			set
			{
				if (m_EndContinuity != value)
				{
					m_EndContinuity = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float EndBias
		{
			get
			{
				return m_EndBias;
			}
			set
			{
				if (m_EndBias != value)
				{
					m_EndBias = value;
					Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public CurvySplineSegment FollowUp
		{
			get
			{
				return m_FollowUp;
			}
			private set
			{
				if (m_FollowUp != value)
				{
					m_FollowUp = value;
					if (mSpline != null)
					{
						mSpline.SetDirty(this, SplineDirtyingType.Everything);
					}
				}
			}
		}

		public ConnectionHeadingEnum FollowUpHeading
		{
			get
			{
				return m_FollowUpHeading;
			}
			set
			{
				if (m_FollowUpHeading != value)
				{
					m_FollowUpHeading = value;
					if (mSpline != null)
					{
						mSpline.SetDirty(this, SplineDirtyingType.Everything);
					}
				}
			}
		}

		public bool ConnectionSyncPosition
		{
			get
			{
				return m_ConnectionSyncPosition;
			}
			set
			{
				if (m_ConnectionSyncPosition != value)
				{
					m_ConnectionSyncPosition = value;
				}
			}
		}

		public bool ConnectionSyncRotation
		{
			get
			{
				return m_ConnectionSyncRotation;
			}
			set
			{
				if (m_ConnectionSyncRotation != value)
				{
					m_ConnectionSyncRotation = value;
				}
			}
		}

		public CurvyConnection Connection
		{
			get
			{
				return m_Connection;
			}
			internal set
			{
				if (SetConnection(value) && mSpline != null)
				{
					mSpline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public int CacheSize
		{
			get
			{
				return cacheSize;
			}
			private set
			{
				cacheSize = value;
			}
		}

		public Bounds Bounds
		{
			get
			{
				if (!mBounds.HasValue)
				{
					Bounds value;
					if (Approximation.Length == 0)
					{
						value = new Bounds(base.transform.position, Vector3.zero);
					}
					else
					{
						Matrix4x4 localToWorldMatrix = Spline.transform.localToWorldMatrix;
						value = new Bounds(localToWorldMatrix.MultiplyPoint3x4(Approximation[0]), Vector3.zero);
						int num = Approximation.Length;
						for (int i = 1; i < num; i++)
						{
							value.Encapsulate(localToWorldMatrix.MultiplyPoint(Approximation[i]));
						}
					}
					mBounds = value;
				}
				return mBounds.Value;
			}
		}

		public float Length { get; private set; }

		public float Distance { get; internal set; }

		public float TF => LocalFToTF(0f);

		public bool IsFirstControlPoint => Spline.GetControlPointIndex(this) == 0;

		public bool IsLastControlPoint => Spline.GetControlPointIndex(this) == Spline.ControlPointCount - 1;

		[Obsolete("Use MetaDataSet instead")]
		public List<Component> MetaData => MetaDataSet.ToList();

		public HashSet<Component> MetaDataSet => mMetaData;

		public CurvySpline Spline => mSpline;

		public bool HasUnprocessedLocalPosition => base.transform.localPosition != lastProcessedLocalPosition;

		public bool HasUnprocessedLocalOrientation => base.transform.localRotation.DifferentOrientation(lastProcessedLocalRotation);

		public bool OrientatinInfluencesSpline
		{
			get
			{
				if (mSpline != null)
				{
					if (mSpline.Orientation != CurvyOrientation.Static)
					{
						return mSpline.IsControlPointAnOrientationAnchor(this);
					}
					return true;
				}
				return false;
			}
		}

		private CurvyInterpolation interpolation
		{
			get
			{
				if (!Spline)
				{
					return CurvyInterpolation.Linear;
				}
				return Spline.Interpolation;
			}
		}

		private bool isDynamicOrientation
		{
			get
			{
				if ((bool)Spline)
				{
					return Spline.Orientation == CurvyOrientation.Dynamic;
				}
				return false;
			}
		}

		private bool IsOrientationAnchorEditable
		{
			get
			{
				CurvySpline spline = Spline;
				if (isDynamicOrientation && spline.IsControlPointVisible(this) && spline.FirstVisibleControlPoint != this)
				{
					return spline.LastVisibleControlPoint != this;
				}
				return false;
			}
		}

		private bool canHaveSwirl
		{
			get
			{
				CurvySpline spline = Spline;
				if (isDynamicOrientation && (bool)spline && spline.IsControlPointAnOrientationAnchor(this))
				{
					if (!spline.Closed)
					{
						return spline.LastVisibleControlPoint != this;
					}
					return true;
				}
				return false;
			}
		}

		public void SetBezierHandleIn(Vector3 position, Space space = Space.Self, CurvyBezierModeEnum mode = CurvyBezierModeEnum.None)
		{
			if (space == Space.Self)
			{
				HandleIn = position;
			}
			else
			{
				HandleInPosition = position;
			}
			bool flag = (mode & CurvyBezierModeEnum.Direction) == CurvyBezierModeEnum.Direction;
			bool flag2 = (mode & CurvyBezierModeEnum.Length) == CurvyBezierModeEnum.Length;
			bool flag3 = (mode & CurvyBezierModeEnum.Connections) == CurvyBezierModeEnum.Connections;
			if (flag)
			{
				HandleOut = HandleOut.magnitude * (HandleIn.normalized * -1f);
			}
			if (flag2)
			{
				HandleOut = HandleIn.magnitude * ((HandleOut == Vector3.zero) ? (HandleIn.normalized * -1f) : HandleOut.normalized);
			}
			if (!((bool)Connection && flag3) || !(flag || flag2))
			{
				return;
			}
			ReadOnlyCollection<CurvySplineSegment> controlPointsList = Connection.ControlPointsList;
			for (int i = 0; i < controlPointsList.Count; i++)
			{
				CurvySplineSegment curvySplineSegment = controlPointsList[i];
				if (!(curvySplineSegment == this))
				{
					if (curvySplineSegment.HandleIn.magnitude == 0f)
					{
						curvySplineSegment.HandleIn = HandleIn;
					}
					if (flag)
					{
						curvySplineSegment.SetBezierHandleIn(curvySplineSegment.HandleIn.magnitude * HandleIn.normalized * Mathf.Sign(Vector3.Dot(HandleIn, curvySplineSegment.HandleIn)), Space.Self, CurvyBezierModeEnum.Direction);
					}
					if (flag2)
					{
						curvySplineSegment.SetBezierHandleIn(curvySplineSegment.HandleIn.normalized * HandleIn.magnitude, Space.Self, CurvyBezierModeEnum.Length);
					}
				}
			}
		}

		public void SetBezierHandleOut(Vector3 position, Space space = Space.Self, CurvyBezierModeEnum mode = CurvyBezierModeEnum.None)
		{
			if (space == Space.Self)
			{
				HandleOut = position;
			}
			else
			{
				HandleOutPosition = position;
			}
			bool flag = (mode & CurvyBezierModeEnum.Direction) == CurvyBezierModeEnum.Direction;
			bool flag2 = (mode & CurvyBezierModeEnum.Length) == CurvyBezierModeEnum.Length;
			bool flag3 = (mode & CurvyBezierModeEnum.Connections) == CurvyBezierModeEnum.Connections;
			if (flag)
			{
				HandleIn = HandleIn.magnitude * (HandleOut.normalized * -1f);
			}
			if (flag2)
			{
				HandleIn = HandleOut.magnitude * ((HandleIn == Vector3.zero) ? (HandleOut.normalized * -1f) : HandleIn.normalized);
			}
			if (!((bool)Connection && flag3) || !(flag || flag2))
			{
				return;
			}
			for (int i = 0; i < Connection.ControlPointsList.Count; i++)
			{
				CurvySplineSegment curvySplineSegment = Connection.ControlPointsList[i];
				if (!(curvySplineSegment == this))
				{
					if (curvySplineSegment.HandleOut.magnitude == 0f)
					{
						curvySplineSegment.HandleOut = HandleOut;
					}
					if (flag)
					{
						curvySplineSegment.SetBezierHandleOut(curvySplineSegment.HandleOut.magnitude * HandleOut.normalized * Mathf.Sign(Vector3.Dot(HandleOut, curvySplineSegment.HandleOut)), Space.Self, CurvyBezierModeEnum.Direction);
					}
					if (flag2)
					{
						curvySplineSegment.SetBezierHandleOut(curvySplineSegment.HandleOut.normalized * HandleOut.magnitude, Space.Self, CurvyBezierModeEnum.Length);
					}
				}
			}
		}

		public void SetBezierHandles(float distanceFrag = -1f, bool setIn = true, bool setOut = true, bool noDirtying = false)
		{
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			if (distanceFrag == -1f)
			{
				distanceFrag = AutoHandleDistance;
			}
			if (distanceFrag > 0f)
			{
				CurvySpline spline = Spline;
				Transform transform = base.transform;
				CurvySplineSegment nextControlPoint = spline.GetNextControlPoint(this);
				Transform transform2 = (nextControlPoint ? nextControlPoint.transform : transform);
				CurvySplineSegment previousControlPoint = spline.GetPreviousControlPoint(this);
				Transform obj = (previousControlPoint ? previousControlPoint.transform : transform);
				Vector3 localPosition = transform.localPosition;
				Vector3 p = obj.localPosition - localPosition;
				Vector3 n = transform2.localPosition - localPosition;
				SetBezierHandles(distanceFrag, p, n, setIn, setOut, noDirtying);
				return;
			}
			if (setIn)
			{
				if (noDirtying)
				{
					m_HandleIn = zero;
				}
				else
				{
					HandleIn = zero;
				}
			}
			if (setOut)
			{
				if (noDirtying)
				{
					m_HandleOut = zero2;
				}
				else
				{
					HandleOut = zero2;
				}
			}
		}

		public void SetBezierHandles(float distanceFrag, Vector3 p, Vector3 n, bool setIn = true, bool setOut = true, bool noDirtying = false)
		{
			float magnitude = p.magnitude;
			float magnitude2 = n.magnitude;
			Vector3 handleIn = Vector3.zero;
			Vector3 handleOut = Vector3.zero;
			if (magnitude != 0f || magnitude2 != 0f)
			{
				Vector3 normalized = (magnitude / magnitude2 * n - p).normalized;
				handleIn = -normalized * (magnitude * distanceFrag);
				handleOut = normalized * (magnitude2 * distanceFrag);
			}
			if (setIn)
			{
				if (noDirtying)
				{
					m_HandleIn = handleIn;
				}
				else
				{
					HandleIn = handleIn;
				}
			}
			if (setOut)
			{
				if (noDirtying)
				{
					m_HandleOut = handleOut;
				}
				else
				{
					HandleOut = handleOut;
				}
			}
		}

		public void SetFollowUp(CurvySplineSegment target, ConnectionHeadingEnum heading = ConnectionHeadingEnum.Auto)
		{
			if (target == null || Spline.CanControlPointHaveFollowUp(this))
			{
				FollowUp = target;
				FollowUpHeading = heading;
			}
			else
			{
				DTLog.LogError("[Curvy] Setting a Follow-Up to a Control Point that can't have one");
			}
		}

		public void Disconnect()
		{
			if ((bool)Connection)
			{
				Connection.RemoveControlPoint(this);
			}
			ResetConnectionRelatedData();
		}

		public void ResetConnectionRelatedData()
		{
			Connection = null;
			FollowUp = null;
			FollowUpHeading = ConnectionHeadingEnum.Auto;
			ConnectionSyncPosition = false;
			ConnectionSyncRotation = false;
		}

		public Vector3 Interpolate(float localF)
		{
			return Interpolate(localF, Spline.Interpolation);
		}

		public Vector3 Interpolate(float localF, CurvyInterpolation interpolation)
		{
			localF = Mathf.Clamp01(localF);
			CurvySplineSegment nextControlPoint = mSpline.GetNextControlPoint(this);
			Vector3 localPosition = nextControlPoint.transform.localPosition;
			Vector3 localPosition2 = base.transform.localPosition;
			switch (interpolation)
			{
			case CurvyInterpolation.Bezier:
				return CurvySpline.Bezier(localPosition2 + HandleOut, localPosition2, localPosition, localPosition + nextControlPoint.HandleIn, localF);
			case CurvyInterpolation.CatmullRom:
			case CurvyInterpolation.TCB:
			{
				CurvySplineSegment previousControlPointUsingFollowUp = mSpline.GetPreviousControlPointUsingFollowUp(this);
				CurvySplineSegment nextControlPointUsingFollowUp = nextControlPoint.Spline.GetNextControlPointUsingFollowUp(nextControlPoint);
				Vector3 t = (previousControlPointUsingFollowUp ? previousControlPointUsingFollowUp.transform.localPosition : localPosition2);
				Vector3 t2 = (nextControlPointUsingFollowUp ? nextControlPointUsingFollowUp.transform.localPosition : localPosition);
				if (interpolation == CurvyInterpolation.TCB)
				{
					float fT = StartTension;
					float fT2 = EndTension;
					float fC = StartContinuity;
					float fC2 = EndContinuity;
					float fB = StartBias;
					float fB2 = EndBias;
					if (!OverrideGlobalTension)
					{
						fT = (fT2 = mSpline.Tension);
					}
					if (!OverrideGlobalContinuity)
					{
						fC = (fC2 = mSpline.Continuity);
					}
					if (!OverrideGlobalBias)
					{
						fB = (fB2 = mSpline.Bias);
					}
					return CurvySpline.TCB(t, localPosition2, localPosition, t2, localF, fT, fC, fB, fT2, fC2, fB2);
				}
				return CurvySpline.CatmullRom(t, localPosition2, localPosition, t2, localF);
			}
			case CurvyInterpolation.Linear:
				return Vector3.LerpUnclamped(localPosition2, nextControlPoint ? localPosition : localPosition2, localF);
			default:
				DTLog.LogError("[Curvy] Invalid interpolation value " + interpolation);
				return Vector3.zero;
			}
		}

		public Vector3 InterpolateFast(float localF)
		{
			float frag;
			int approximationIndexINTERNAL = getApproximationIndexINTERNAL(localF, out frag);
			int num = Mathf.Min(Approximation.Length - 1, approximationIndexINTERNAL + 1);
			return Vector3.LerpUnclamped(Approximation[approximationIndexINTERNAL], Approximation[num], frag);
		}

		public void ReloadMetaData()
		{
			MetaDataSet.Clear();
			Component[] components = GetComponents(typeof(ICurvyMetadata));
			foreach (Component item in components)
			{
				MetaDataSet.Add(item);
			}
			CheckAgainstMetaDataDuplication();
		}

		public void RegisterMetaData(CurvyMetadataBase metaData)
		{
			MetaDataSet.Add(metaData);
			CheckAgainstMetaDataDuplication();
		}

		public void UnregisterMetaData(CurvyMetadataBase metaData)
		{
			MetaDataSet.Remove(metaData);
		}

		[Obsolete("Use GetMetadata<T> instead")]
		public Component GetMetaData(Type type, bool autoCreate = false)
		{
			if (type.IsSubclassOf(typeof(Component)) && typeof(ICurvyMetadata).IsAssignableFrom(type))
			{
				foreach (Component item in MetaDataSet)
				{
					if (item != null && item.GetType() == type)
					{
						return item;
					}
				}
			}
			Component component = null;
			if (autoCreate)
			{
				component = base.gameObject.AddComponent(type);
				MetaDataSet.Add(component);
			}
			return component;
		}

		public T GetMetadata<T>(bool autoCreate = false) where T : Component, ICurvyMetadata
		{
			return (T)GetMetaData(typeof(T), autoCreate);
		}

		public U GetInterpolatedMetadata<T, U>(float f) where T : CurvyInterpolatableMetadataBase<U>
		{
			T metadata = GetMetadata<T>();
			if (metadata != null)
			{
				CurvySplineSegment nextControlPointUsingFollowUp = Spline.GetNextControlPointUsingFollowUp(this);
				CurvyInterpolatableMetadataBase<U> nextMetadata = null;
				if ((bool)nextControlPointUsingFollowUp)
				{
					nextMetadata = nextControlPointUsingFollowUp.GetMetadata<T>();
				}
				return metadata.Interpolate(nextMetadata, f);
			}
			return default(U);
		}

		[Obsolete("Use GetInterpolatedMetadata<T, U> instead")]
		public U InterpolateMetadata<T, U>(float f) where T : Component, ICurvyInterpolatableMetadata<U>
		{
			T metadata = GetMetadata<T>();
			if (metadata != null)
			{
				CurvySplineSegment nextControlPointUsingFollowUp = Spline.GetNextControlPointUsingFollowUp(this);
				ICurvyInterpolatableMetadata<U> b = null;
				if ((bool)nextControlPointUsingFollowUp)
				{
					b = nextControlPointUsingFollowUp.GetMetadata<T>();
				}
				return metadata.Interpolate(b, f);
			}
			return default(U);
		}

		[Obsolete("Use GetInterpolatedMetadata<T, U> instead")]
		public object InterpolateMetadata(Type type, float f)
		{
			if (GetMetaData(type) is ICurvyInterpolatableMetadata curvyInterpolatableMetadata)
			{
				CurvySplineSegment nextControlPointUsingFollowUp = Spline.GetNextControlPointUsingFollowUp(this);
				ICurvyInterpolatableMetadata curvyInterpolatableMetadata2 = null;
				if ((bool)nextControlPointUsingFollowUp && nextControlPointUsingFollowUp.GetMetaData(type) is ICurvyInterpolatableMetadata b)
				{
					return curvyInterpolatableMetadata.InterpolateObject(b, f);
				}
			}
			return null;
		}

		public void DeleteMetadata()
		{
			List<Component> list = MetaDataSet.ToList();
			for (int num = list.Count - 1; num >= 0; num--)
			{
				list[num].Destroy();
			}
		}

		public Vector3 InterpolateScale(float localF)
		{
			CurvySplineSegment nextControlPoint = Spline.GetNextControlPoint(this);
			if (!nextControlPoint)
			{
				return base.transform.lossyScale;
			}
			return Vector3.Lerp(base.transform.lossyScale, nextControlPoint.transform.lossyScale, localF);
		}

		public Vector3 GetTangent(float localF)
		{
			localF = Mathf.Clamp01(localF);
			Vector3 position = Interpolate(localF);
			return GetTangent(localF, position);
		}

		public Vector3 GetTangent(float localF, Vector3 position)
		{
			CurvySpline spline = Spline;
			int num = 2;
			Vector3 vector;
			do
			{
				float num2 = localF + 0.01f;
				if (num2 > 1f)
				{
					CurvySplineSegment nextSegment = spline.GetNextSegment(this);
					if (!nextSegment)
					{
						num2 = localF - 0.01f;
						return (position - Interpolate(num2)).normalized;
					}
					vector = nextSegment.Interpolate(num2 - 1f);
				}
				else
				{
					vector = Interpolate(num2);
				}
				localF += 0.01f;
			}
			while (vector == position && --num > 0);
			return (vector - position).normalized;
		}

		public Vector3 GetTangentFast(float localF)
		{
			float frag;
			int approximationIndexINTERNAL = getApproximationIndexINTERNAL(localF, out frag);
			int num = Mathf.Min(ApproximationT.Length - 1, approximationIndexINTERNAL + 1);
			return Vector3.SlerpUnclamped(ApproximationT[approximationIndexINTERNAL], ApproximationT[num], frag);
		}

		public void InterpolateAndGetTangent(float localF, out Vector3 localPosition, out Vector3 localTangent)
		{
			localF = Mathf.Clamp01(localF);
			localPosition = Interpolate(localF, Spline.Interpolation);
			localTangent = GetTangent(localF, localPosition);
		}

		public void InterpolateAndGetTangentFast(float localF, out Vector3 localPosition, out Vector3 localTangent)
		{
			float frag;
			int approximationIndexINTERNAL = getApproximationIndexINTERNAL(localF, out frag);
			int num = Mathf.Min(Approximation.Length - 1, approximationIndexINTERNAL + 1);
			localPosition = Vector3.LerpUnclamped(Approximation[approximationIndexINTERNAL], Approximation[num], frag);
			localTangent = Vector3.SlerpUnclamped(ApproximationT[approximationIndexINTERNAL], ApproximationT[num], frag);
		}

		public Quaternion GetOrientationFast(float localF)
		{
			return GetOrientationFast(localF, inverse: false);
		}

		public Quaternion GetOrientationFast(float localF, bool inverse)
		{
			Vector3 tangentFast = GetTangentFast(localF);
			if (tangentFast != Vector3.zero)
			{
				if (inverse)
				{
					tangentFast *= -1f;
				}
				return Quaternion.LookRotation(tangentFast, GetOrientationUpFast(localF));
			}
			return Quaternion.identity;
		}

		public Vector3 GetOrientationUpFast(float localF)
		{
			float frag;
			int approximationIndexINTERNAL = getApproximationIndexINTERNAL(localF, out frag);
			int num = Mathf.Min(ApproximationUp.Length - 1, approximationIndexINTERNAL + 1);
			return Vector3.SlerpUnclamped(ApproximationUp[approximationIndexINTERNAL], ApproximationUp[num], frag);
		}

		public float GetNearestPointF(Vector3 p)
		{
			int num = CacheSize + 1;
			float num2 = float.MaxValue;
			int num3 = 0;
			Vector3 vector = default(Vector3);
			for (int i = 0; i < num; i++)
			{
				vector.x = Approximation[i].x - p.x;
				vector.y = Approximation[i].y - p.y;
				vector.z = Approximation[i].z - p.z;
				float num4 = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
				if (num4 <= num2)
				{
					num2 = num4;
					num3 = i;
				}
			}
			int num5 = ((num3 > 0) ? (num3 - 1) : (-1));
			int num6 = ((num3 < CacheSize) ? (num3 + 1) : (-1));
			float frag = 0f;
			float frag2 = 0f;
			float num7 = float.MaxValue;
			float num8 = float.MaxValue;
			if (num5 > -1)
			{
				num7 = DTMath.LinePointDistanceSqr(Approximation[num5], Approximation[num3], p, out frag);
			}
			if (num6 > -1)
			{
				num8 = DTMath.LinePointDistanceSqr(Approximation[num3], Approximation[num6], p, out frag2);
			}
			if (num7 < num8)
			{
				return getApproximationLocalF(num5) + frag * mStepSize;
			}
			return getApproximationLocalF(num3) + frag2 * mStepSize;
		}

		public float DistanceToLocalF(float localDistance)
		{
			localDistance = Mathf.Clamp(localDistance, 0f, Length);
			if (ApproximationDistances.Length <= 1 || localDistance == 0f)
			{
				return 0f;
			}
			if (Mathf.Approximately(localDistance, Length))
			{
				return 1f;
			}
			int num = Mathf.Min(ApproximationDistances.Length - 1, mCacheLastDistanceToLocalFIndex);
			if (ApproximationDistances[num] < localDistance)
			{
				num = ApproximationDistances.Length - 1;
			}
			while (ApproximationDistances[num] > localDistance)
			{
				num--;
			}
			mCacheLastDistanceToLocalFIndex = num + 1;
			float num2 = (localDistance - ApproximationDistances[num]) / (ApproximationDistances[num + 1] - ApproximationDistances[num]);
			float approximationLocalF = getApproximationLocalF(num);
			float approximationLocalF2 = getApproximationLocalF(num + 1);
			return approximationLocalF + (approximationLocalF2 - approximationLocalF) * num2;
		}

		public float LocalFToDistance(float localF)
		{
			localF = Mathf.Clamp01(localF);
			if (ApproximationDistances.Length <= 1 || localF == 0f)
			{
				return 0f;
			}
			if (Mathf.Approximately(localF, 1f))
			{
				return Length;
			}
			float frag;
			int approximationIndexINTERNAL = getApproximationIndexINTERNAL(localF, out frag);
			float num = ApproximationDistances[approximationIndexINTERNAL + 1] - ApproximationDistances[approximationIndexINTERNAL];
			return ApproximationDistances[approximationIndexINTERNAL] + num * frag;
		}

		public float LocalFToTF(float localF)
		{
			return Spline.SegmentToTF(this, localF);
		}

		public override string ToString()
		{
			if (Spline != null)
			{
				return Spline.name + "." + base.name;
			}
			return base.ToString();
		}

		public void BakeOrientationToTransform()
		{
			Quaternion orientationFast = GetOrientationFast(0f);
			if (base.transform.localRotation.DifferentOrientation(orientationFast))
			{
				SetLocalRotation(orientationFast);
			}
		}

		public int getApproximationIndexINTERNAL(float localF, out float frag)
		{
			localF = Mathf.Clamp01(localF);
			if (localF == 1f)
			{
				frag = 1f;
				return Mathf.Max(0, Approximation.Length - 2);
			}
			float num = localF / mStepSize;
			int num2 = (int)num;
			frag = num - (float)num2;
			return num2;
		}

		public void LinkToSpline(CurvySpline spline)
		{
			mSpline = spline;
		}

		public void UnlinkFromSpline()
		{
			mSpline = null;
		}

		public void SetLocalPosition(Vector3 newPosition)
		{
			Transform transform = base.transform;
			if (transform.localPosition != newPosition)
			{
				transform.localPosition = newPosition;
				Spline.SetDirtyPartial(this, SplineDirtyingType.Everything);
				if ((ConnectionSyncPosition || ConnectionSyncRotation) && Connection != null)
				{
					Connection.SetSynchronisationPositionAndRotation(ConnectionSyncPosition ? transform.position : Connection.transform.position, ConnectionSyncRotation ? transform.rotation : Connection.transform.rotation);
				}
			}
		}

		public void SetPosition(Vector3 value)
		{
			Transform transform = base.transform;
			if (transform.position != value)
			{
				transform.position = value;
				Spline.SetDirtyPartial(this, SplineDirtyingType.Everything);
				if ((ConnectionSyncPosition || ConnectionSyncRotation) && Connection != null)
				{
					Connection.SetSynchronisationPositionAndRotation(ConnectionSyncPosition ? transform.position : Connection.transform.position, ConnectionSyncRotation ? transform.rotation : Connection.transform.rotation);
				}
			}
		}

		public void SetLocalRotation(Quaternion value)
		{
			Transform transform = base.transform;
			if (transform.localRotation != value)
			{
				transform.localRotation = value;
				if (OrientatinInfluencesSpline)
				{
					Spline.SetDirtyPartial(this, SplineDirtyingType.OrientationOnly);
				}
				if ((ConnectionSyncPosition || ConnectionSyncRotation) && Connection != null)
				{
					Connection.SetSynchronisationPositionAndRotation(ConnectionSyncPosition ? transform.position : Connection.transform.position, ConnectionSyncRotation ? transform.rotation : Connection.transform.rotation);
				}
			}
		}

		public void SetRotation(Quaternion value)
		{
			Transform transform = base.transform;
			if (transform.rotation != value)
			{
				transform.rotation = value;
				if (OrientatinInfluencesSpline)
				{
					Spline.SetDirtyPartial(this, SplineDirtyingType.OrientationOnly);
				}
				if ((ConnectionSyncPosition || ConnectionSyncRotation) && Connection != null)
				{
					Connection.SetSynchronisationPositionAndRotation(ConnectionSyncPosition ? transform.position : Connection.transform.position, ConnectionSyncRotation ? transform.rotation : Connection.transform.rotation);
				}
			}
		}

		public void OnBeforePush()
		{
			this.StripComponents();
			Disconnect();
			DeleteMetadata();
		}

		public void OnAfterPop()
		{
			Reset();
		}

		private void Awake()
		{
			ReloadMetaData();
		}

		private void OnEnable()
		{
			ReloadMetaData();
		}

		private void OnDisable()
		{
		}

		private void Update()
		{
			if (Application.isPlaying)
			{
				DoUpdate();
			}
		}

		private void LateUpdate()
		{
			if (Application.isPlaying)
			{
				DoUpdate();
			}
		}

		private void FixedUpdate()
		{
			if (Application.isPlaying)
			{
				DoUpdate();
			}
		}

		private void OnDestroy()
		{
			if (true)
			{
				Disconnect();
			}
		}

		public void Reset()
		{
			m_OrientationAnchor = false;
			m_Swirl = CurvyOrientationSwirl.None;
			m_SwirlTurns = 0f;
			m_AutoHandles = true;
			m_AutoHandleDistance = 0.39f;
			m_HandleIn = new Vector3(-1f, 0f, 0f);
			m_HandleOut = new Vector3(1f, 0f, 0f);
			m_SynchronizeTCB = true;
			m_OverrideGlobalTension = false;
			m_OverrideGlobalContinuity = false;
			m_OverrideGlobalBias = false;
			m_StartTension = 0f;
			m_EndTension = 0f;
			m_StartContinuity = 0f;
			m_EndContinuity = 0f;
			m_StartBias = 0f;
			m_EndBias = 0f;
			if ((bool)mSpline)
			{
				Spline.SetDirty(this, SplineDirtyingType.Everything);
				Spline.InvalidateControlPointsRelationshipCacheINTERNAL();
			}
		}

		internal void SetExtrinsicPropertiesINTERNAL(ControlPointExtrinsicProperties value)
		{
			extrinsicPropertiesINTERNAL = value;
		}

		internal ref readonly ControlPointExtrinsicProperties GetExtrinsicPropertiesINTERNAL()
		{
			return ref extrinsicPropertiesINTERNAL;
		}

		private void CheckAgainstMetaDataDuplication()
		{
			if (MetaDataSet.Count <= 1)
			{
				return;
			}
			HashSet<Type> hashSet = new HashSet<Type>();
			foreach (Component item in MetaDataSet)
			{
				Type type = item.GetType();
				if (hashSet.Contains(type))
				{
					DTLog.LogWarning($"[Curvy] Game object '{ToString()}' has multiple Components of type '{type}'. Control Points should have no more than one Component instance for each MetaData type.");
				}
				else
				{
					hashSet.Add(type);
				}
			}
		}

		private void DoUpdate()
		{
			if (AutoBakeOrientation && ApproximationUp.Length != 0)
			{
				BakeOrientationToTransform();
			}
		}

		private bool SetConnection(CurvyConnection newConnection)
		{
			bool result = false;
			if (m_Connection != newConnection)
			{
				result = true;
				m_Connection = newConnection;
			}
			if (m_Connection == null && m_FollowUp != null)
			{
				result = true;
				m_FollowUp = null;
			}
			return result;
		}

		private bool SetAutoHandles(bool newValue)
		{
			bool flag = false;
			if ((bool)Connection)
			{
				ReadOnlyCollection<CurvySplineSegment> controlPointsList = Connection.ControlPointsList;
				for (int i = 0; i < controlPointsList.Count; i++)
				{
					CurvySplineSegment curvySplineSegment = controlPointsList[i];
					flag = flag || curvySplineSegment.m_AutoHandles != newValue;
					curvySplineSegment.m_AutoHandles = newValue;
				}
			}
			else
			{
				flag = m_AutoHandles != newValue;
				m_AutoHandles = newValue;
			}
			return flag;
		}

		private float getApproximationLocalF(int idx)
		{
			return (float)idx * mStepSize;
		}

		internal void refreshCurveINTERNAL(CurvyInterpolation splineInterpolation, bool isControlPointASegment, CurvySpline spline)
		{
			short nextControlPointIndex = spline.GetNextControlPointIndex(this);
			CurvySplineSegment curvySplineSegment = ((nextControlPointIndex == -1) ? null : spline.ControlPointsList[nextControlPointIndex]);
			int num = (CacheSize = (isControlPointASegment ? CurvySpline.CalculateCacheSize(spline.CacheDensity, (curvySplineSegment.threadSafeLocalPosition - threadSafeLocalPosition).magnitude, spline.MaxPointsPerUnit) : 0));
			Array.Resize(ref Approximation, num + 1);
			Array.Resize(ref ApproximationT, num + 1);
			Array.Resize(ref ApproximationDistances, num + 1);
			Array.Resize(ref ApproximationUp, num + 1);
			Approximation[0] = threadSafeLocalPosition;
			ApproximationDistances[0] = 0f;
			mBounds = null;
			Length = 0f;
			mStepSize = 1f / (float)num;
			if (num != 0)
			{
				Approximation[num] = ((nextControlPointIndex != -1) ? curvySplineSegment.threadSafeLocalPosition : threadSafeLocalPosition);
			}
			if (isControlPointASegment)
			{
				float length = 0f;
				switch (splineInterpolation)
				{
				case CurvyInterpolation.Bezier:
					length = InterpolateBezierSegment(spline, num);
					break;
				case CurvyInterpolation.CatmullRom:
					length = InterpolateCatmullSegment(spline, curvySplineSegment, num);
					break;
				case CurvyInterpolation.TCB:
					length = InterpolateTCBSegment(spline, curvySplineSegment, num);
					break;
				case CurvyInterpolation.Linear:
					length = InterpolateLinearSegment(spline, num);
					break;
				default:
					DTLog.LogError("[Curvy] Invalid interpolation value " + splineInterpolation);
					break;
				}
				Length = length;
				Vector3 vector = Approximation[num] - Approximation[num - 1];
				Length += vector.magnitude;
				ApproximationDistances[num] = Length;
				ApproximationT[num - 1] = vector.normalized;
				ApproximationT[num] = ApproximationT[num - 1];
			}
			else if (nextControlPointIndex != -1)
			{
				ApproximationT[0] = (curvySplineSegment.threadSafeLocalPosition - Approximation[0]).normalized;
			}
			else
			{
				short previousControlPointIndex = spline.GetPreviousControlPointIndex(this);
				if (previousControlPointIndex != -1)
				{
					ApproximationT[0] = (Approximation[0] - spline.ControlPointsList[previousControlPointIndex].threadSafeLocalPosition).normalized;
				}
				else
				{
					ApproximationT[0] = threadSafeLocalRotation * Vector3.forward;
				}
			}
			lastProcessedLocalPosition = threadSafeLocalPosition;
		}

		private float InterpolateBezierSegment(CurvySpline spline, int newCacheSize)
		{
			float num = 0f;
			CurvySplineSegment nextControlPoint = spline.GetNextControlPoint(this);
			Vector3 vector = threadSafeLocalPosition;
			Vector3 vector2 = vector + HandleOut;
			Vector3 vector3 = nextControlPoint.threadSafeLocalPosition;
			Vector3 vector4 = vector3 + nextControlPoint.HandleIn;
			double num2 = (double)(0f - vector.x) + 3.0 * (double)vector2.x + -3.0 * (double)vector4.x + (double)vector3.x;
			double num3 = 3.0 * (double)vector.x + -6.0 * (double)vector2.x + 3.0 * (double)vector4.x;
			double num4 = -3.0 * (double)vector.x + 3.0 * (double)vector2.x;
			double num5 = vector.x;
			double num6 = (double)(0f - vector.y) + 3.0 * (double)vector2.y + -3.0 * (double)vector4.y + (double)vector3.y;
			double num7 = 3.0 * (double)vector.y + -6.0 * (double)vector2.y + 3.0 * (double)vector4.y;
			double num8 = -3.0 * (double)vector.y + 3.0 * (double)vector2.y;
			double num9 = vector.y;
			double num10 = (double)(0f - vector.z) + 3.0 * (double)vector2.z + -3.0 * (double)vector4.z + (double)vector3.z;
			double num11 = 3.0 * (double)vector.z + -6.0 * (double)vector2.z + 3.0 * (double)vector4.z;
			double num12 = -3.0 * (double)vector.z + 3.0 * (double)vector2.z;
			double num13 = vector.z;
			Vector3 vector5 = default(Vector3);
			for (int i = 1; i < newCacheSize; i++)
			{
				float num14 = (float)i * mStepSize;
				Approximation[i].x = (float)(((num2 * (double)num14 + num3) * (double)num14 + num4) * (double)num14 + num5);
				Approximation[i].y = (float)(((num6 * (double)num14 + num7) * (double)num14 + num8) * (double)num14 + num9);
				Approximation[i].z = (float)(((num10 * (double)num14 + num11) * (double)num14 + num12) * (double)num14 + num13);
				vector5.x = Approximation[i].x - Approximation[i - 1].x;
				vector5.y = Approximation[i].y - Approximation[i - 1].y;
				vector5.z = Approximation[i].z - Approximation[i - 1].z;
				float num15 = Mathf.Sqrt(vector5.x * vector5.x + vector5.y * vector5.y + vector5.z * vector5.z);
				num += num15;
				ApproximationDistances[i] = num;
				if ((double)num15 > 9.99999974737875E-06)
				{
					float num16 = 1f / num15;
					ApproximationT[i - 1].x = vector5.x * num16;
					ApproximationT[i - 1].y = vector5.y * num16;
					ApproximationT[i - 1].z = vector5.z * num16;
				}
				else
				{
					ApproximationT[i - 1].x = 0f;
					ApproximationT[i - 1].y = 0f;
					ApproximationT[i - 1].z = 0f;
				}
			}
			return num;
		}

		private float InterpolateTCBSegment(CurvySpline spline, CurvySplineSegment nextControlPoint, int newCacheSize)
		{
			float num = 0f;
			float num2 = StartTension;
			float num3 = EndTension;
			float num4 = StartContinuity;
			float num5 = EndContinuity;
			float num6 = StartBias;
			float num7 = EndBias;
			if (!OverrideGlobalTension)
			{
				num2 = (num3 = spline.Tension);
			}
			if (!OverrideGlobalContinuity)
			{
				num4 = (num5 = spline.Continuity);
			}
			if (!OverrideGlobalBias)
			{
				num6 = (num7 = spline.Bias);
			}
			CurvySplineSegment previousControlPointUsingFollowUp = spline.GetPreviousControlPointUsingFollowUp(this);
			CurvySplineSegment nextControlPointUsingFollowUp = nextControlPoint.Spline.GetNextControlPointUsingFollowUp(nextControlPoint);
			Vector3 vector = threadSafeLocalPosition;
			Vector3 vector2 = nextControlPoint.threadSafeLocalPosition;
			Vector3 vector3 = (previousControlPointUsingFollowUp ? previousControlPointUsingFollowUp.threadSafeLocalPosition : vector);
			Vector3 vector4 = (nextControlPointUsingFollowUp ? nextControlPointUsingFollowUp.threadSafeLocalPosition : vector2);
			double num8 = (1f - num2) * (1f + num4) * (1f + num6);
			double num9 = (1f - num2) * (1f - num4) * (1f - num6);
			double num10 = (1f - num3) * (1f - num5) * (1f + num7);
			double num11 = (1f - num3) * (1f + num5) * (1f - num7);
			double num12 = 2.0;
			double num13 = (0.0 - num8) / num12;
			double num14 = (4.0 + num8 - num9 - num10) / num12;
			double num15 = (-4.0 + num9 + num10 - num11) / num12;
			double num16 = num11 / num12;
			double num17 = 2.0 * num8 / num12;
			double num18 = (-6.0 - 2.0 * num8 + 2.0 * num9 + num10) / num12;
			double num19 = (6.0 - 2.0 * num9 - num10 + num11) / num12;
			double num20 = (0.0 - num11) / num12;
			double num21 = (0.0 - num8) / num12;
			double num22 = (num8 - num9) / num12;
			double num23 = num9 / num12;
			double num24 = 2.0 / num12;
			double num25 = num13 * (double)vector3.x + num14 * (double)vector.x + num15 * (double)vector2.x + num16 * (double)vector4.x;
			double num26 = num17 * (double)vector3.x + num18 * (double)vector.x + num19 * (double)vector2.x + num20 * (double)vector4.x;
			double num27 = num21 * (double)vector3.x + num22 * (double)vector.x + num23 * (double)vector2.x;
			double num28 = num24 * (double)vector.x;
			double num29 = num13 * (double)vector3.y + num14 * (double)vector.y + num15 * (double)vector2.y + num16 * (double)vector4.y;
			double num30 = num17 * (double)vector3.y + num18 * (double)vector.y + num19 * (double)vector2.y + num20 * (double)vector4.y;
			double num31 = num21 * (double)vector3.y + num22 * (double)vector.y + num23 * (double)vector2.y;
			double num32 = num24 * (double)vector.y;
			double num33 = num13 * (double)vector3.z + num14 * (double)vector.z + num15 * (double)vector2.z + num16 * (double)vector4.z;
			double num34 = num17 * (double)vector3.z + num18 * (double)vector.z + num19 * (double)vector2.z + num20 * (double)vector4.z;
			double num35 = num21 * (double)vector3.z + num22 * (double)vector.z + num23 * (double)vector2.z;
			double num36 = num24 * (double)vector.z;
			Vector3 vector5 = default(Vector3);
			for (int i = 1; i < newCacheSize; i++)
			{
				float num37 = (float)i * mStepSize;
				Approximation[i].x = (float)(((num25 * (double)num37 + num26) * (double)num37 + num27) * (double)num37 + num28);
				Approximation[i].y = (float)(((num29 * (double)num37 + num30) * (double)num37 + num31) * (double)num37 + num32);
				Approximation[i].z = (float)(((num33 * (double)num37 + num34) * (double)num37 + num35) * (double)num37 + num36);
				vector5.x = Approximation[i].x - Approximation[i - 1].x;
				vector5.y = Approximation[i].y - Approximation[i - 1].y;
				vector5.z = Approximation[i].z - Approximation[i - 1].z;
				float num38 = Mathf.Sqrt(vector5.x * vector5.x + vector5.y * vector5.y + vector5.z * vector5.z);
				num += num38;
				ApproximationDistances[i] = num;
				if ((double)num38 > 9.99999974737875E-06)
				{
					float num39 = 1f / num38;
					ApproximationT[i - 1].x = vector5.x * num39;
					ApproximationT[i - 1].y = vector5.y * num39;
					ApproximationT[i - 1].z = vector5.z * num39;
				}
				else
				{
					ApproximationT[i - 1].x = 0f;
					ApproximationT[i - 1].y = 0f;
					ApproximationT[i - 1].z = 0f;
				}
			}
			return num;
		}

		private float InterpolateCatmullSegment(CurvySpline spline, CurvySplineSegment nextControlPoint, int newCacheSize)
		{
			float num = 0f;
			CurvySplineSegment previousControlPointUsingFollowUp = spline.GetPreviousControlPointUsingFollowUp(this);
			CurvySplineSegment nextControlPointUsingFollowUp = nextControlPoint.Spline.GetNextControlPointUsingFollowUp(nextControlPoint);
			Vector3 vector = threadSafeLocalPosition;
			Vector3 vector2 = nextControlPoint.threadSafeLocalPosition;
			Vector3 vector3 = (previousControlPointUsingFollowUp ? previousControlPointUsingFollowUp.threadSafeLocalPosition : vector);
			Vector3 vector4 = (nextControlPointUsingFollowUp ? nextControlPointUsingFollowUp.threadSafeLocalPosition : vector2);
			double num2 = -0.5 * (double)vector3.x + 1.5 * (double)vector.x + -1.5 * (double)vector2.x + 0.5 * (double)vector4.x;
			double num3 = (double)vector3.x + -2.5 * (double)vector.x + 2.0 * (double)vector2.x + -0.5 * (double)vector4.x;
			double num4 = -0.5 * (double)vector3.x + 0.5 * (double)vector2.x;
			double num5 = vector.x;
			double num6 = -0.5 * (double)vector3.y + 1.5 * (double)vector.y + -1.5 * (double)vector2.y + 0.5 * (double)vector4.y;
			double num7 = (double)vector3.y + -2.5 * (double)vector.y + 2.0 * (double)vector2.y + -0.5 * (double)vector4.y;
			double num8 = -0.5 * (double)vector3.y + 0.5 * (double)vector2.y;
			double num9 = vector.y;
			double num10 = -0.5 * (double)vector3.z + 1.5 * (double)vector.z + -1.5 * (double)vector2.z + 0.5 * (double)vector4.z;
			double num11 = (double)vector3.z + -2.5 * (double)vector.z + 2.0 * (double)vector2.z + -0.5 * (double)vector4.z;
			double num12 = -0.5 * (double)vector3.z + 0.5 * (double)vector2.z;
			double num13 = vector.z;
			Vector3 vector5 = default(Vector3);
			for (int i = 1; i < newCacheSize; i++)
			{
				float num14 = (float)i * mStepSize;
				Approximation[i].x = (float)(((num2 * (double)num14 + num3) * (double)num14 + num4) * (double)num14 + num5);
				Approximation[i].y = (float)(((num6 * (double)num14 + num7) * (double)num14 + num8) * (double)num14 + num9);
				Approximation[i].z = (float)(((num10 * (double)num14 + num11) * (double)num14 + num12) * (double)num14 + num13);
				vector5.x = Approximation[i].x - Approximation[i - 1].x;
				vector5.y = Approximation[i].y - Approximation[i - 1].y;
				vector5.z = Approximation[i].z - Approximation[i - 1].z;
				float num15 = Mathf.Sqrt(vector5.x * vector5.x + vector5.y * vector5.y + vector5.z * vector5.z);
				num += num15;
				ApproximationDistances[i] = num;
				if ((double)num15 > 9.99999974737875E-06)
				{
					float num16 = 1f / num15;
					ApproximationT[i - 1].x = vector5.x * num16;
					ApproximationT[i - 1].y = vector5.y * num16;
					ApproximationT[i - 1].z = vector5.z * num16;
				}
				else
				{
					ApproximationT[i - 1].x = 0f;
					ApproximationT[i - 1].y = 0f;
					ApproximationT[i - 1].z = 0f;
				}
			}
			return num;
		}

		private float InterpolateLinearSegment(CurvySpline spline, int newCacheSize)
		{
			float num = 0f;
			Vector3 a = threadSafeLocalPosition;
			Vector3 b = spline.GetNextControlPoint(this).threadSafeLocalPosition;
			Vector3 vector = default(Vector3);
			for (int i = 1; i < newCacheSize; i++)
			{
				float t = (float)i * mStepSize;
				Approximation[i] = Vector3.LerpUnclamped(a, b, t);
				vector.x = Approximation[i].x - Approximation[i - 1].x;
				vector.y = Approximation[i].y - Approximation[i - 1].y;
				vector.z = Approximation[i].z - Approximation[i - 1].z;
				float num2 = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
				num += num2;
				ApproximationDistances[i] = num;
				if ((double)num2 > 9.99999974737875E-06)
				{
					float num3 = 1f / num2;
					ApproximationT[i - 1].x = vector.x * num3;
					ApproximationT[i - 1].y = vector.y * num3;
					ApproximationT[i - 1].z = vector.z * num3;
				}
				else
				{
					ApproximationT[i - 1].x = 0f;
					ApproximationT[i - 1].y = 0f;
					ApproximationT[i - 1].z = 0f;
				}
			}
			return num;
		}

		internal void refreshOrientationNoneINTERNAL()
		{
			Array.Clear(ApproximationUp, 0, ApproximationUp.Length);
			lastProcessedLocalRotation = threadSafeLocalRotation;
		}

		internal void refreshOrientationStaticINTERNAL()
		{
			Vector3 a = (ApproximationUp[0] = getOrthoUp0INTERNAL());
			if (Approximation.Length > 1)
			{
				int num = CacheSize;
				Vector3 b = (ApproximationUp[num] = getOrthoUp1INTERNAL());
				float num2 = 1f / (float)num;
				for (int i = 1; i < num; i++)
				{
					ApproximationUp[i] = Vector3.SlerpUnclamped(a, b, (float)i * num2);
				}
			}
			lastProcessedLocalRotation = threadSafeLocalRotation;
		}

		internal void refreshOrientationDynamicINTERNAL(Vector3 initialUp)
		{
			int num = ApproximationUp.Length;
			ApproximationUp[0] = initialUp;
			Vector3 axis = default(Vector3);
			for (int i = 1; i < num; i++)
			{
				Vector3 vector = ApproximationT[i - 1];
				Vector3 vector2 = ApproximationT[i];
				axis.x = vector.y * vector2.z - vector.z * vector2.y;
				axis.y = vector.z * vector2.x - vector.x * vector2.z;
				axis.z = vector.x * vector2.y - vector.y * vector2.x;
				float num2 = (float)Math.Atan2(Math.Sqrt(axis.x * axis.x + axis.y * axis.y + axis.z * axis.z), vector.x * vector2.x + vector.y * vector2.y + vector.z * vector2.z);
				ApproximationUp[i] = Quaternion.AngleAxis(57.29578f * num2, axis) * ApproximationUp[i - 1];
			}
			lastProcessedLocalRotation = threadSafeLocalRotation;
		}

		internal void ClearBoundsINTERNAL()
		{
			mBounds = null;
		}

		internal Vector3 getOrthoUp0INTERNAL()
		{
			Vector3 tangent = threadSafeLocalRotation * Vector3.up;
			Vector3.OrthoNormalize(ref ApproximationT[0], ref tangent);
			return tangent;
		}

		private Vector3 getOrthoUp1INTERNAL()
		{
			CurvySplineSegment nextControlPoint = Spline.GetNextControlPoint(this);
			Vector3 tangent = (nextControlPoint ? nextControlPoint.threadSafeLocalRotation : threadSafeLocalRotation) * Vector3.up;
			Vector3.OrthoNormalize(ref ApproximationT[CacheSize], ref tangent);
			return tangent;
		}

		internal void UnsetFollowUpWithoutDirtyingINTERNAL()
		{
			m_FollowUp = null;
			m_FollowUpHeading = ConnectionHeadingEnum.Auto;
		}

		private bool SnapToFitSplineLength(float newSplineLength, float stepSize)
		{
			CurvySpline spline = Spline;
			if (stepSize == 0f || Mathf.Approximately(newSplineLength, spline.Length))
			{
				return true;
			}
			Transform transform = base.transform;
			float length = spline.Length;
			Vector3 position = transform.position;
			Vector3 vector = transform.up * stepSize;
			transform.position += vector;
			spline.SetDirty(this, SplineDirtyingType.Everything);
			spline.Refresh();
			bool flag = spline.Length > length;
			int num = 30000;
			transform.position = position;
			if (newSplineLength > length)
			{
				if (!flag)
				{
					vector *= -1f;
				}
				while (spline.Length < newSplineLength)
				{
					num--;
					length = spline.Length;
					transform.position += vector;
					spline.SetDirty(this, SplineDirtyingType.Everything);
					spline.Refresh();
					if (length > spline.Length)
					{
						return false;
					}
					if (num == 0)
					{
						Debug.LogError("CurvySplineSegment.SnapToFitSplineLength exceeds 30000 loops, considering this a dead loop! This shouldn't happen, please send a bug report.");
						return false;
					}
				}
			}
			else
			{
				if (flag)
				{
					vector *= -1f;
				}
				while (spline.Length > newSplineLength)
				{
					num--;
					length = spline.Length;
					transform.position += vector;
					spline.SetDirty(this, SplineDirtyingType.Everything);
					spline.Refresh();
					if (length < spline.Length)
					{
						return false;
					}
					if (num == 0)
					{
						Debug.LogError("CurvySplineSegment.SnapToFitSplineLength exceeds 30000 loops, considering this a dead loop! This shouldn't happen, please send a bug report.");
						return false;
					}
				}
			}
			return true;
		}

		internal void PrepareThreadSafeTransfromINTERNAL()
		{
			Transform transform = base.transform;
			threadSafeLocalPosition = transform.localPosition;
			threadSafeLocalRotation = transform.localRotation;
		}
	}
}
