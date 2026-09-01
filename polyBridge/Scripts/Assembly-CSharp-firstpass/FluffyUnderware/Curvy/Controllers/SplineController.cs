using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluffyUnderware.Curvy.Utils;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Controllers
{
	[AddComponentMenu("Curvy/Controller/Spline Controller", 5)]
	[HelpURL("https://curvyeditor.com/doclink/splinecontroller")]
	public class SplineController : CurvyController
	{
		[Section("General", true, false, 100, Sort = 0)]
		[FieldCondition("m_Spline", null, false, ActionAttribute.ActionEnum.ShowError, "Missing source Spline", ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		protected CurvySpline m_Spline;

		[SerializeField]
		[Tooltip("Whether spline's cache data should be used. Set this to true to gain performance if precision is not required.")]
		private bool m_UseCache;

		[Section("Connections handling", true, false, 100, Sort = 250, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_move")]
		[SerializeField]
		[Label("At connection, use", "What spline should the controller use when reaching a Connection")]
		private SplineControllerConnectionBehavior connectionBehavior;

		[SerializeField]
		[Label("Allow direction change", "When true, the controller will modify its direction to best fit the connected spline")]
		private bool allowDirectionChange = true;

		[SerializeField]
		[Label("Reject current spline", "Whether the current spline should be excluded from the randomly selected splines")]
		[FieldCondition("ShowRandomConnectionOptions", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		private bool rejectCurrentSpline = true;

		[SerializeField]
		[Label("Reject divergent splines", "Whether splines that diverge from the current spline with more than a specific angle should be excluded from the randomly selected splines")]
		[FieldCondition("ShowRandomConnectionOptions", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		private bool rejectTooDivergentSplines;

		[SerializeField]
		[Label("Max allowed angle", "Maximum allowed divergence angle in degrees")]
		[Range(0f, 180f)]
		private float maxAllowedDivergenceAngle = 90f;

		[SerializeField]
		[Label("Custom Selector", "A custom logic to select which connected spline to follow. Select a Script inheriting from SplineControllerConnectionBehavior")]
		[FieldCondition("connectionBehavior", SplineControllerConnectionBehavior.Custom, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[FieldCondition("connectionCustomSelector", null, false, ActionAttribute.ActionEnum.ShowWarning, "Missing custom selector", ActionAttribute.ActionPositionEnum.Below)]
		private ConnectedControlPointsSelector connectionCustomSelector;

		[Section("Events", false, false, 1000, HelpURL = "https://curvyeditor.com/doclink/splinecontroller_events")]
		[SerializeField]
		private CurvySplineMoveEvent m_OnControlPointReached;

		[SerializeField]
		private CurvySplineMoveEvent m_OnEndReached;

		[SerializeField]
		private CurvySplineMoveEvent m_OnSwitch;

		private const float StepSize = 0.002f;

		private CurvySpline prePlaySpline;

		private float switchStartTime;

		private float switchDuration;

		private CurvySpline switchTarget;

		private float tfOnSwitchTarget;

		private MovementDirection directionOnSwitchTarget;

		private readonly CurvySplineMoveEventArgs preAllocatedEventArgs;

		public virtual CurvySpline Spline
		{
			get
			{
				return m_Spline;
			}
			set
			{
				m_Spline = value;
			}
		}

		public bool UseCache
		{
			get
			{
				return m_UseCache;
			}
			set
			{
				if (m_UseCache != value)
				{
					m_UseCache = value;
				}
			}
		}

		public SplineControllerConnectionBehavior ConnectionBehavior
		{
			get
			{
				return connectionBehavior;
			}
			set
			{
				connectionBehavior = value;
			}
		}

		public ConnectedControlPointsSelector ConnectionCustomSelector
		{
			get
			{
				return connectionCustomSelector;
			}
			set
			{
				connectionCustomSelector = value;
			}
		}

		public bool AllowDirectionChange
		{
			get
			{
				return allowDirectionChange;
			}
			set
			{
				allowDirectionChange = value;
			}
		}

		public bool RejectCurrentSpline
		{
			get
			{
				return rejectCurrentSpline;
			}
			set
			{
				rejectCurrentSpline = value;
			}
		}

		public bool RejectTooDivergentSplines
		{
			get
			{
				return rejectTooDivergentSplines;
			}
			set
			{
				rejectTooDivergentSplines = value;
			}
		}

		public float MaxAllowedDivergenceAngle
		{
			get
			{
				return maxAllowedDivergenceAngle;
			}
			set
			{
				maxAllowedDivergenceAngle = value;
			}
		}

		public CurvySplineMoveEvent OnControlPointReached
		{
			get
			{
				return m_OnControlPointReached;
			}
			set
			{
				m_OnControlPointReached = value;
			}
		}

		public CurvySplineMoveEvent OnEndReached
		{
			get
			{
				return m_OnEndReached;
			}
			set
			{
				m_OnEndReached = value;
			}
		}

		public CurvySplineMoveEvent OnSwitch
		{
			get
			{
				return m_OnSwitch;
			}
			set
			{
				m_OnSwitch = value;
			}
		}

		public bool IsSwitching { get; private set; }

		public override float Length
		{
			get
			{
				if (!Spline)
				{
					return 0f;
				}
				return Spline.Length;
			}
		}

		private float SwitchProgress
		{
			get
			{
				if (!IsSwitching)
				{
					return 0f;
				}
				return Mathf.Clamp01((Time.time - switchStartTime) / switchDuration);
			}
		}

		public override bool IsReady
		{
			get
			{
				if (Spline != null)
				{
					return Spline.IsInitialized;
				}
				return false;
			}
		}

		private bool ShowRandomConnectionOptions
		{
			get
			{
				if (ConnectionBehavior != SplineControllerConnectionBehavior.FollowUpOtherwiseRandom)
				{
					return ConnectionBehavior == SplineControllerConnectionBehavior.RandomSpline;
				}
				return true;
			}
		}

		public SplineController()
		{
			preAllocatedEventArgs = new CurvySplineMoveEventArgs(this, Spline, null, float.NaN, usingWorldUnits: false, float.NaN, MovementDirection.Forward);
		}

		public virtual void SwitchTo(CurvySpline destinationSpline, float destinationTf, float duration)
		{
			if (base.PlayState == CurvyControllerState.Stopped)
			{
				DTLog.LogError("[Curvy] Contoller can not switch when stopped. The switch call will be ignored");
				return;
			}
			switchStartTime = Time.time;
			switchDuration = duration;
			switchTarget = destinationSpline;
			tfOnSwitchTarget = destinationTf;
			directionOnSwitchTarget = base.MovementDirection;
			IsSwitching = true;
		}

		public void FinishCurrentSwitch()
		{
			if (IsSwitching)
			{
				IsSwitching = false;
				Spline = switchTarget;
				base.RelativePosition = tfOnSwitchTarget;
			}
		}

		public void CancelCurrentSwitch()
		{
			if (IsSwitching)
			{
				IsSwitching = false;
			}
		}

		public static float GetAngleBetweenConnectedSplines(CurvySplineSegment before, MovementDirection movementMode, CurvySplineSegment after, bool allowMovementModeChange)
		{
			Vector3 vector = before.GetTangentFast(0f) * movementMode.ToInt();
			Vector3 to = after.GetTangentFast(0f) * GetPostConnectionDirection(after, movementMode, allowMovementModeChange).ToInt();
			return Vector3.Angle(vector, to);
		}

		protected override void SavePrePlayState()
		{
			prePlaySpline = Spline;
			base.SavePrePlayState();
		}

		protected override void RestorePrePlayState()
		{
			Spline = prePlaySpline;
			base.RestorePrePlayState();
		}

		protected override float RelativeToAbsolute(float relativeDistance)
		{
			return Spline.TFToDistance(relativeDistance, base.Clamping);
		}

		protected override float AbsoluteToRelative(float worldUnitDistance)
		{
			return Spline.DistanceToTF(worldUnitDistance, base.Clamping);
		}

		protected override Vector3 GetInterpolatedSourcePosition(float tf)
		{
			Vector3 position = (UseCache ? Spline.InterpolateFast(tf) : Spline.Interpolate(tf));
			return Spline.transform.TransformPoint(position);
		}

		protected override void GetInterpolatedSourcePosition(float tf, out Vector3 interpolatedPosition, out Vector3 tangent, out Vector3 up)
		{
			CurvySpline spline = Spline;
			Transform transform = spline.transform;
			if (UseCache)
			{
				spline.InterpolateAndGetTangentFast(tf, out interpolatedPosition, out tangent);
			}
			else
			{
				spline.InterpolateAndGetTangent(tf, out interpolatedPosition, out tangent);
			}
			up = spline.GetOrientationUpFast(tf);
			interpolatedPosition = transform.TransformPoint(interpolatedPosition);
			tangent = transform.TransformDirection(tangent);
			up = transform.TransformDirection(up);
		}

		protected override Vector3 GetTangent(float tf)
		{
			Vector3 direction = (UseCache ? Spline.GetTangentFast(tf) : Spline.GetTangent(tf));
			return Spline.transform.TransformDirection(direction);
		}

		protected override Vector3 GetOrientation(float tf)
		{
			return Spline.transform.TransformDirection(Spline.GetOrientationUpFast(tf));
		}

		protected override void Advance(float speed, float deltaTime)
		{
			float distance = speed * deltaTime;
			if (Spline.Count != 0)
			{
				EventAwareMove(distance);
			}
			if (IsSwitching && switchTarget.Count > 0)
			{
				SimulateAdvanceOnSpline(ref tfOnSwitchTarget, ref directionOnSwitchTarget, switchTarget, speed * deltaTime);
				preAllocatedEventArgs.Set_INTERNAL(this, switchTarget, null, tfOnSwitchTarget, SwitchProgress, directionOnSwitchTarget, usingWorldUnits: false);
				OnSwitch.Invoke(preAllocatedEventArgs);
				if (preAllocatedEventArgs.Cancel)
				{
					CancelCurrentSwitch();
				}
			}
		}

		protected override void SimulateAdvance(ref float tf, ref MovementDirection curyDirection, float speed, float deltaTime)
		{
			SimulateAdvanceOnSpline(ref tf, ref curyDirection, Spline, speed * deltaTime);
		}

		private void SimulateAdvanceOnSpline(ref float tf, ref MovementDirection curyDirection, CurvySpline spline, float distance)
		{
			if (spline.Count > 0)
			{
				int dir = curyDirection.ToInt();
				switch (base.MoveMode)
				{
				case MoveModeEnum.AbsolutePrecise:
					tf = spline.DistanceToTF(spline.ClampDistance(spline.TFToDistance(tf) + distance * (float)dir, ref dir, base.Clamping));
					break;
				case MoveModeEnum.Relative:
					tf = CurvyUtility.ClampTF(tf + distance * (float)dir, ref dir, base.Clamping);
					break;
				default:
					throw new NotSupportedException();
				}
				curyDirection = MovementDirectionMethods.FromInt(dir);
			}
		}

		protected override void InitializedApplyDeltaTime(float deltaTime)
		{
			if (Spline.Dirty)
			{
				Spline.Refresh();
			}
			base.InitializedApplyDeltaTime(deltaTime);
			if (IsSwitching && SwitchProgress >= 1f)
			{
				FinishCurrentSwitch();
			}
		}

		protected override void ComputeTargetPositionAndRotation(out Vector3 targetPosition, out Vector3 targetUp, out Vector3 targetForward)
		{
			base.ComputeTargetPositionAndRotation(out var targetPosition2, out var targetUp2, out var targetForward2);
			Quaternion a = Quaternion.LookRotation(targetForward2, targetUp2);
			if (IsSwitching)
			{
				CurvySpline spline = Spline;
				float relativePosition = base.RelativePosition;
				m_Spline = switchTarget;
				base.RelativePosition = tfOnSwitchTarget;
				base.ComputeTargetPositionAndRotation(out var targetPosition3, out var targetUp3, out var targetForward3);
				Quaternion b = Quaternion.LookRotation(targetForward3, targetUp3);
				m_Spline = spline;
				base.RelativePosition = relativePosition;
				targetPosition = Vector3.LerpUnclamped(targetPosition2, targetPosition3, SwitchProgress);
				Quaternion quaternion = Quaternion.LerpUnclamped(a, b, SwitchProgress);
				targetUp = quaternion * Vector3.up;
				targetForward = quaternion * Vector3.forward;
			}
			else
			{
				targetPosition = targetPosition2;
				targetUp = targetUp2;
				targetForward = targetForward2;
			}
		}

		private static float MovementCompatibleGetPosition(SplineController controller, CurvyPositionMode positionMode, out CurvySplineSegment controlPoint, out bool isOnControlPoint)
		{
			CurvySpline spline = controller.Spline;
			float position = controller.m_Position;
			float num;
			switch (controller.PositionMode)
			{
			case CurvyPositionMode.Relative:
				num = ((controller.Clamping == CurvyClamping.Loop && position.Approximately(1f)) ? 1f : CurvyUtility.ClampTF(position, controller.Clamping));
				break;
			case CurvyPositionMode.WorldUnits:
			{
				float length = controller.Length;
				num = ((controller.Clamping == CurvyClamping.Loop && position.Approximately(length)) ? length : CurvyUtility.ClampDistance(position, controller.Clamping, length));
				break;
			}
			default:
				throw new NotSupportedException();
			}
			float localF;
			bool flag;
			bool flag2;
			switch (controller.PositionMode)
			{
			case CurvyPositionMode.Relative:
				controlPoint = spline.TFToSegment(num, out localF, CurvyClamping.Clamp);
				flag = localF.Approximately(0f);
				flag2 = localF.Approximately(1f);
				break;
			case CurvyPositionMode.WorldUnits:
				controlPoint = spline.DistanceToSegment(num, out localF);
				flag = localF.Approximately(0f);
				flag2 = localF.Approximately(controlPoint.Length);
				break;
			default:
				throw new NotSupportedException();
			}
			float result = ((positionMode == controller.PositionMode) ? num : (positionMode switch
			{
				CurvyPositionMode.Relative => spline.SegmentToTF(controlPoint, controlPoint.DistanceToLocalF(localF)), 
				CurvyPositionMode.WorldUnits => controlPoint.Distance + controlPoint.LocalFToDistance(localF), 
				_ => throw new ArgumentOutOfRangeException(), 
			}));
			if (flag2)
			{
				controlPoint = spline.GetNextControlPoint(controlPoint);
			}
			isOnControlPoint = flag || flag2;
			return result;
		}

		private static void MovementCompatibleSetPosition(SplineController controller, CurvyPositionMode positionMode, float position)
		{
			CurvyPositionMode positionMode2 = controller.PositionMode;
			CurvyClamping clamping = controller.Clamping;
			float num;
			switch (positionMode)
			{
			case CurvyPositionMode.Relative:
				num = ((clamping == CurvyClamping.Loop && position.Approximately(1f)) ? 1f : CurvyUtility.ClampTF(position, clamping));
				break;
			case CurvyPositionMode.WorldUnits:
			{
				float length = controller.Length;
				num = ((clamping == CurvyClamping.Loop && position.Approximately(length)) ? length : CurvyUtility.ClampDistance(position, clamping, length));
				break;
			}
			default:
				throw new NotSupportedException();
			}
			if (positionMode == positionMode2)
			{
				controller.m_Position = num;
				return;
			}
			switch (positionMode)
			{
			case CurvyPositionMode.Relative:
				controller.m_Position = controller.Spline.TFToDistance(num, controller.Clamping);
				break;
			case CurvyPositionMode.WorldUnits:
				controller.m_Position = controller.Spline.DistanceToTF(num, controller.Clamping);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private void EventAwareMove(float distance)
		{
			CurvyPositionMode positionMode = base.MoveMode switch
			{
				MoveModeEnum.AbsolutePrecise => CurvyPositionMode.WorldUnits, 
				MoveModeEnum.Relative => CurvyPositionMode.Relative, 
				_ => throw new NotSupportedException(), 
			};
			float num = distance;
			bool cancelMovement = false;
			if ((base.MovementDirection == MovementDirection.Backward && base.RelativePosition.Approximately(0f)) || (base.MovementDirection == MovementDirection.Forward && base.RelativePosition.Approximately(1f)))
			{
				switch (base.Clamping)
				{
				case CurvyClamping.Clamp:
					num = 0f;
					break;
				case CurvyClamping.PingPong:
					base.MovementDirection = base.MovementDirection.GetOpposite();
					break;
				}
			}
			int num2 = 50;
			while (!cancelMovement && num > 0f && num2-- > 0)
			{
				bool isOnControlPoint;
				float position;
				CurvySplineSegment currentControlPoint = GetCurrentControlPoint(out isOnControlPoint, out position, positionMode);
				CurvySplineSegment curvySplineSegment = ((base.MovementDirection != MovementDirection.Forward) ? (isOnControlPoint ? Spline.GetPreviousControlPoint(currentControlPoint) : currentControlPoint) : Spline.GetNextControlPoint(currentControlPoint));
				if (curvySplineSegment != null && Spline.IsControlPointVisible(curvySplineSegment))
				{
					float num3 = Mathf.Abs(GetControlPointPosition(curvySplineSegment, positionMode, base.MovementDirection) - position);
					if (num3 > num)
					{
						MovementCompatibleSetPosition(this, positionMode, position + num * (float)base.MovementDirection.ToInt());
						break;
					}
					num -= num3;
					HandleReachingNewControlPoint(curvySplineSegment, positionMode, num, ref cancelMovement);
				}
				bool isOnControlPoint2;
				float position2;
				CurvySplineSegment currentControlPoint2 = GetCurrentControlPoint(out isOnControlPoint2, out position2, positionMode);
				if (isOnControlPoint2 && (bool)currentControlPoint2.Connection && currentControlPoint2.Connection.ControlPointsList.Count > 1)
				{
					CurvySplineSegment curvySplineSegment2;
					MovementDirection newDirection;
					switch (ConnectionBehavior)
					{
					case SplineControllerConnectionBehavior.CurrentSpline:
						curvySplineSegment2 = currentControlPoint2;
						newDirection = base.MovementDirection;
						break;
					case SplineControllerConnectionBehavior.FollowUpSpline:
						curvySplineSegment2 = HandleFolloUpConnectionBahavior(currentControlPoint2, base.MovementDirection, out newDirection);
						break;
					case SplineControllerConnectionBehavior.FollowUpOtherwiseRandom:
						curvySplineSegment2 = (currentControlPoint2.FollowUp ? HandleFolloUpConnectionBahavior(currentControlPoint2, base.MovementDirection, out newDirection) : HandleRandomConnectionBehavior(currentControlPoint2, base.MovementDirection, out newDirection, currentControlPoint2.Connection.ControlPointsList));
						break;
					case SplineControllerConnectionBehavior.RandomSpline:
						curvySplineSegment2 = HandleRandomConnectionBehavior(currentControlPoint2, base.MovementDirection, out newDirection, currentControlPoint2.Connection.ControlPointsList);
						break;
					case SplineControllerConnectionBehavior.Custom:
						if (ConnectionCustomSelector == null)
						{
							DTLog.LogError("[Curvy] You need to set a non null ConnectionCustomSelector when using SplineControllerConnectionBehavior.Custom");
							curvySplineSegment2 = currentControlPoint2;
						}
						else
						{
							curvySplineSegment2 = ConnectionCustomSelector.SelectConnectedControlPoint(this, currentControlPoint2.Connection, currentControlPoint2);
						}
						newDirection = base.MovementDirection;
						break;
					default:
						throw new ArgumentOutOfRangeException();
					}
					if (curvySplineSegment2 != currentControlPoint2)
					{
						base.MovementDirection = newDirection;
						HandleReachingNewControlPoint(curvySplineSegment2, positionMode, num, ref cancelMovement);
					}
				}
				bool isOnControlPoint3;
				float position3;
				CurvySplineSegment currentControlPoint3 = GetCurrentControlPoint(out isOnControlPoint3, out position3, positionMode);
				if (isOnControlPoint3)
				{
					switch (base.Clamping)
					{
					case CurvyClamping.Loop:
						if (!Spline.Closed)
						{
							if (base.MovementDirection == MovementDirection.Backward && currentControlPoint3 == Spline.FirstVisibleControlPoint)
							{
								HandleReachingNewControlPoint(Spline.LastVisibleControlPoint, positionMode, num, ref cancelMovement);
							}
							else if (base.MovementDirection == MovementDirection.Forward && currentControlPoint3 == Spline.LastVisibleControlPoint)
							{
								HandleReachingNewControlPoint(Spline.FirstVisibleControlPoint, positionMode, num, ref cancelMovement);
							}
						}
						break;
					case CurvyClamping.Clamp:
						if ((base.MovementDirection == MovementDirection.Backward && currentControlPoint3 == Spline.FirstVisibleControlPoint) || (base.MovementDirection == MovementDirection.Forward && currentControlPoint3 == Spline.LastVisibleControlPoint))
						{
							num = 0f;
						}
						break;
					case CurvyClamping.PingPong:
						if ((base.MovementDirection == MovementDirection.Backward && currentControlPoint3 == Spline.FirstVisibleControlPoint) || (base.MovementDirection == MovementDirection.Forward && currentControlPoint3 == Spline.LastVisibleControlPoint))
						{
							base.MovementDirection = base.MovementDirection.GetOpposite();
						}
						break;
					default:
						throw new ArgumentOutOfRangeException();
					}
				}
				bool isOnControlPoint4;
				CurvySplineSegment currentControlPoint4 = GetCurrentControlPoint(out isOnControlPoint4, out position, positionMode);
				if (!(currentControlPoint == currentControlPoint4) || isOnControlPoint != isOnControlPoint4)
				{
					continue;
				}
				CurvySplineSegment curvySplineSegment3 = null;
				for (int i = 0; i < Spline.Count; i++)
				{
					if (Spline[i].Length == 0f)
					{
						curvySplineSegment3 = Spline[i];
						break;
					}
				}
				if (curvySplineSegment3 != null)
				{
					DTLog.LogError($"[Curvy] Spline Controller '{base.name}' is stuck at control point '{currentControlPoint4}'. This is probably caused by the presence of a segment with a length of 0. Please remove control point '{curvySplineSegment3}' to proceed");
				}
				else
				{
					DTLog.LogError($"[Curvy] Spline Controller '{base.name}' is stuck at control point '{currentControlPoint4}'. Please raise a bug report");
				}
				break;
			}
			if (num2 <= 0)
			{
				DTLog.LogError($"[Curvy] Unexpected behavior in Spline Controller '{base.name}'. Please raise a Bug Report.");
			}
		}

		private CurvySplineSegment GetCurrentControlPoint(out bool isOnControlPoint, out float position, CurvyPositionMode positionMode)
		{
			position = MovementCompatibleGetPosition(this, positionMode, out var controlPoint, out isOnControlPoint);
			return controlPoint;
		}

		private void HandleReachingNewControlPoint(CurvySplineSegment newControlPoint, CurvyPositionMode positionMode, float currentDelta, ref bool cancelMovement)
		{
			Spline = newControlPoint.Spline;
			float controlPointPosition = GetControlPointPosition(newControlPoint, positionMode, base.MovementDirection);
			MovementCompatibleSetPosition(this, positionMode, controlPointPosition);
			bool usingWorldUnits = positionMode switch
			{
				CurvyPositionMode.Relative => false, 
				CurvyPositionMode.WorldUnits => true, 
				_ => throw new ArgumentOutOfRangeException(), 
			};
			preAllocatedEventArgs.Set_INTERNAL(this, Spline, newControlPoint, controlPointPosition, currentDelta, base.MovementDirection, usingWorldUnits);
			OnControlPointReached.Invoke(preAllocatedEventArgs);
			if (preAllocatedEventArgs.Spline.FirstVisibleControlPoint == preAllocatedEventArgs.ControlPoint || preAllocatedEventArgs.Spline.LastVisibleControlPoint == preAllocatedEventArgs.ControlPoint)
			{
				OnEndReached.Invoke(preAllocatedEventArgs);
			}
			cancelMovement |= preAllocatedEventArgs.Cancel;
		}

		private CurvySplineSegment HandleRandomConnectionBehavior(CurvySplineSegment currentControlPoint, MovementDirection currentDirection, out MovementDirection newDirection, ReadOnlyCollection<CurvySplineSegment> connectedControlPoints)
		{
			List<CurvySplineSegment> list = new List<CurvySplineSegment>(connectedControlPoints.Count);
			for (int i = 0; i < connectedControlPoints.Count; i++)
			{
				CurvySplineSegment curvySplineSegment = connectedControlPoints[i];
				if ((!RejectCurrentSpline || !(curvySplineSegment == currentControlPoint)) && (!RejectTooDivergentSplines || !(GetAngleBetweenConnectedSplines(currentControlPoint, currentDirection, curvySplineSegment, AllowDirectionChange) > MaxAllowedDivergenceAngle)))
				{
					list.Add(curvySplineSegment);
				}
			}
			CurvySplineSegment curvySplineSegment2 = ((list.Count == 0) ? currentControlPoint : list[UnityEngine.Random.Range(0, list.Count)]);
			newDirection = GetPostConnectionDirection(curvySplineSegment2, currentDirection, AllowDirectionChange);
			return curvySplineSegment2;
		}

		private static MovementDirection GetPostConnectionDirection(CurvySplineSegment connectedControlPoint, MovementDirection currentDirection, bool directionChangeAllowed)
		{
			if (!directionChangeAllowed || connectedControlPoint.Spline.Closed)
			{
				return currentDirection;
			}
			return HeadingToDirection(ConnectionHeadingEnum.Auto, connectedControlPoint, currentDirection);
		}

		private CurvySplineSegment HandleFolloUpConnectionBahavior(CurvySplineSegment currentControlPoint, MovementDirection currentDirection, out MovementDirection newDirection)
		{
			CurvySplineSegment result = (currentControlPoint.FollowUp ? currentControlPoint.FollowUp : currentControlPoint);
			newDirection = ((AllowDirectionChange && (bool)currentControlPoint.FollowUp) ? HeadingToDirection(currentControlPoint.FollowUpHeading, currentControlPoint.FollowUp, currentDirection) : currentDirection);
			return result;
		}

		private static MovementDirection HeadingToDirection(ConnectionHeadingEnum heading, CurvySplineSegment controlPoint, MovementDirection currentDirection)
		{
			return heading.ResolveAuto(controlPoint) switch
			{
				ConnectionHeadingEnum.Minus => MovementDirection.Backward, 
				ConnectionHeadingEnum.Sharp => currentDirection, 
				ConnectionHeadingEnum.Plus => MovementDirection.Forward, 
				_ => throw new ArgumentOutOfRangeException(), 
			};
		}

		private static float GetControlPointPosition(CurvySplineSegment controlPoint, CurvyPositionMode positionMode, MovementDirection movementDirection)
		{
			CurvySpline spline = controlPoint.Spline;
			float num = positionMode switch
			{
				CurvyPositionMode.Relative => spline.SegmentToTF(controlPoint), 
				CurvyPositionMode.WorldUnits => controlPoint.Distance, 
				_ => throw new ArgumentOutOfRangeException(), 
			};
			float result = positionMode switch
			{
				CurvyPositionMode.Relative => 1f, 
				CurvyPositionMode.WorldUnits => spline.Length, 
				_ => throw new ArgumentOutOfRangeException(), 
			};
			if (movementDirection != MovementDirection.Forward || !num.Approximately(0f) || !spline.Closed)
			{
				return num;
			}
			return result;
		}
	}
}
