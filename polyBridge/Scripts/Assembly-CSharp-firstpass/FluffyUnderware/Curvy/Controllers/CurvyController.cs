using System;
using System.Globalization;
using System.Reflection;
using FluffyUnderware.Curvy.Utils;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace FluffyUnderware.Curvy.Controllers
{
	[ExecuteInEditMode]
	public abstract class CurvyController : DTVersionedMonoBehaviour, ISerializationCallbackReceiver
	{
		public enum MoveModeEnum
		{
			Relative = 0,
			AbsolutePrecise = 1
		}

		public enum CurvyControllerState
		{
			Stopped = 0,
			Playing = 1,
			Paused = 2
		}

		[Section("General", true, false, 100, Sort = 0, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_general")]
		[Label(Tooltip = "Determines when to update")]
		public CurvyUpdateMethod UpdateIn;

		[Section("Position", true, false, 100, Sort = 100, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_position")]
		[SerializeField]
		private CurvyPositionMode m_PositionMode;

		[RangeEx(0f, "maxPosition", "", "")]
		[SerializeField]
		[FormerlySerializedAs("m_InitialPosition")]
		protected float m_Position;

		[Section("Move", true, false, 100, Sort = 200, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_move")]
		[SerializeField]
		private MoveModeEnum m_MoveMode = MoveModeEnum.AbsolutePrecise;

		[Positive]
		[SerializeField]
		private float m_Speed;

		[SerializeField]
		private MovementDirection m_Direction;

		[SerializeField]
		private CurvyClamping m_Clamping = CurvyClamping.Loop;

		[SerializeField]
		[Tooltip("Start playing automatically when entering play mode")]
		private bool m_PlayAutomatically = true;

		[Section("Orientation", true, false, 100, Sort = 300, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_orientation")]
		[Label("Source", "Source Vector")]
		[FieldCondition("ShowOrientationSection", false, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private OrientationModeEnum m_OrientationMode = OrientationModeEnum.Orientation;

		[Label("Lock Rotation", "When set, the controller will enforce the rotation to not change")]
		[SerializeField]
		private bool m_LockRotation = true;

		[Label("Target", "Target Vector3")]
		[FieldCondition("m_OrientationMode", OrientationModeEnum.None, false, ConditionalAttribute.OperatorEnum.OR, "ShowOrientationSection", false, false, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private OrientationAxisEnum m_OrientationAxis;

		[Tooltip("Should the orientation ignore the movement direction?")]
		[FieldCondition("m_OrientationMode", OrientationModeEnum.None, false, ConditionalAttribute.OperatorEnum.OR, "ShowOrientationSection", false, false, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private bool m_IgnoreDirection;

		[FluffyUnderware.DevTools.Min(0f, "Direction Damping Time", "If non zero, the direction vector will not be updated instantly, but using a damping effect that will last the specified amount of time.")]
		[FieldCondition("ShowOrientationSection", false, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private float m_DampingDirection;

		[FluffyUnderware.DevTools.Min(0f, "Up Damping Time", "If non zero, the up vector will not be updated instantly, but using a damping effect that will last the specified amount of time.")]
		[FieldCondition("ShowOrientationSection", false, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private float m_DampingUp;

		[Section("Offset", true, false, 100, Sort = 400, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_orientation")]
		[FieldCondition("m_OrientationMode", OrientationModeEnum.None, false, ConditionalAttribute.OperatorEnum.OR, "ShowOffsetSection", false, false, Action = ActionAttribute.ActionEnum.Hide)]
		[RangeEx(-180f, 180f, "", "")]
		[SerializeField]
		private float m_OffsetAngle;

		[FieldCondition("m_OrientationMode", OrientationModeEnum.None, false, ConditionalAttribute.OperatorEnum.OR, "ShowOffsetSection", false, false, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private float m_OffsetRadius;

		[FieldCondition("m_OrientationMode", OrientationModeEnum.None, false, ConditionalAttribute.OperatorEnum.OR, "ShowOffsetSection", false, false, Action = ActionAttribute.ActionEnum.Hide)]
		[Label("Compensate Offset", "")]
		[SerializeField]
		private bool m_OffsetCompensation = true;

		[Section("Events", true, false, 100, Sort = 500)]
		[SerializeField]
		private ControllerEvent onInitialized;

		protected const string ControllerNotReadyMessage = "The controller is not yet ready";

		private CurvyControllerState state;

		private Vector3 directionDampingVelocity;

		private Vector3 upDampingVelocity;

		private float prePlayPosition;

		private Quaternion lockedRotation;

		private MovementDirection prePlayDirection;

		public ControllerEvent OnInitialized => onInitialized;

		public CurvyPositionMode PositionMode
		{
			get
			{
				return m_PositionMode;
			}
			set
			{
				m_PositionMode = value;
			}
		}

		public MoveModeEnum MoveMode
		{
			get
			{
				return m_MoveMode;
			}
			set
			{
				if (m_MoveMode != value)
				{
					m_MoveMode = value;
				}
			}
		}

		public bool PlayAutomatically
		{
			get
			{
				return m_PlayAutomatically;
			}
			set
			{
				if (m_PlayAutomatically != value)
				{
					m_PlayAutomatically = value;
				}
			}
		}

		public CurvyClamping Clamping
		{
			get
			{
				return m_Clamping;
			}
			set
			{
				if (m_Clamping != value)
				{
					m_Clamping = value;
				}
			}
		}

		public OrientationModeEnum OrientationMode
		{
			get
			{
				return m_OrientationMode;
			}
			set
			{
				if (m_OrientationMode != value)
				{
					m_OrientationMode = value;
				}
			}
		}

		public bool LockRotation
		{
			get
			{
				return m_LockRotation;
			}
			set
			{
				if (m_LockRotation != value)
				{
					m_LockRotation = value;
				}
				if (m_LockRotation)
				{
					lockedRotation = Transform.rotation;
				}
			}
		}

		public OrientationAxisEnum OrientationAxis
		{
			get
			{
				return m_OrientationAxis;
			}
			set
			{
				if (m_OrientationAxis != value)
				{
					m_OrientationAxis = value;
				}
			}
		}

		public float DirectionDampingTime
		{
			get
			{
				return m_DampingDirection;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (m_DampingDirection != num)
				{
					m_DampingDirection = num;
				}
			}
		}

		public float UpDampingTime
		{
			get
			{
				return m_DampingUp;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (m_DampingUp != num)
				{
					m_DampingUp = num;
				}
			}
		}

		public bool IgnoreDirection
		{
			get
			{
				return m_IgnoreDirection;
			}
			set
			{
				if (m_IgnoreDirection != value)
				{
					m_IgnoreDirection = value;
				}
			}
		}

		public float OffsetAngle
		{
			get
			{
				return m_OffsetAngle;
			}
			set
			{
				if (m_OffsetAngle != value)
				{
					m_OffsetAngle = value;
				}
			}
		}

		public float OffsetRadius
		{
			get
			{
				return m_OffsetRadius;
			}
			set
			{
				if (m_OffsetRadius != value)
				{
					m_OffsetRadius = value;
				}
			}
		}

		public bool OffsetCompensation
		{
			get
			{
				return m_OffsetCompensation;
			}
			set
			{
				m_OffsetCompensation = value;
			}
		}

		public float Speed
		{
			get
			{
				return m_Speed;
			}
			set
			{
				if (value < 0f)
				{
					value = 0f - value;
				}
				m_Speed = value;
			}
		}

		public float RelativePosition
		{
			get
			{
				return PositionMode switch
				{
					CurvyPositionMode.Relative => GetClampedPosition(m_Position, CurvyPositionMode.Relative, Clamping, Length), 
					CurvyPositionMode.WorldUnits => AbsoluteToRelative(GetClampedPosition(m_Position, CurvyPositionMode.WorldUnits, Clamping, Length)), 
					_ => throw new NotSupportedException(), 
				};
			}
			set
			{
				float clampedPosition = GetClampedPosition(value, CurvyPositionMode.Relative, Clamping, Length);
				switch (PositionMode)
				{
				case CurvyPositionMode.Relative:
					m_Position = clampedPosition;
					break;
				case CurvyPositionMode.WorldUnits:
					m_Position = RelativeToAbsolute(clampedPosition);
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		public float AbsolutePosition
		{
			get
			{
				return PositionMode switch
				{
					CurvyPositionMode.Relative => RelativeToAbsolute(GetClampedPosition(m_Position, CurvyPositionMode.Relative, Clamping, Length)), 
					CurvyPositionMode.WorldUnits => GetClampedPosition(m_Position, CurvyPositionMode.WorldUnits, Clamping, Length), 
					_ => throw new NotSupportedException(), 
				};
			}
			set
			{
				float clampedPosition = GetClampedPosition(value, CurvyPositionMode.WorldUnits, Clamping, Length);
				switch (PositionMode)
				{
				case CurvyPositionMode.Relative:
					m_Position = AbsoluteToRelative(clampedPosition);
					break;
				case CurvyPositionMode.WorldUnits:
					m_Position = clampedPosition;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		public float Position
		{
			get
			{
				return PositionMode switch
				{
					CurvyPositionMode.Relative => RelativePosition, 
					CurvyPositionMode.WorldUnits => AbsolutePosition, 
					_ => throw new NotSupportedException(), 
				};
			}
			set
			{
				switch (PositionMode)
				{
				case CurvyPositionMode.Relative:
					RelativePosition = value;
					break;
				case CurvyPositionMode.WorldUnits:
					AbsolutePosition = value;
					break;
				default:
					throw new NotSupportedException();
				}
			}
		}

		public MovementDirection MovementDirection
		{
			get
			{
				return m_Direction;
			}
			set
			{
				m_Direction = value;
			}
		}

		public CurvyControllerState PlayState => state;

		public abstract bool IsReady { get; }

		protected bool isInitialized { get; private set; }

		public virtual Transform Transform => base.transform;

		protected virtual bool ShowOrientationSection => true;

		protected virtual bool ShowOffsetSection => true;

		public abstract float Length { get; }

		private float TimeSinceLastUpdate => Time.deltaTime;

		private float maxPosition => PositionMode switch
		{
			CurvyPositionMode.Relative => 1f, 
			CurvyPositionMode.WorldUnits => IsReady ? Length : 0f, 
			_ => throw new NotSupportedException(), 
		};

		protected virtual void OnEnable()
		{
			if (!isInitialized && IsReady)
			{
				Initialize();
				InitializedApplyDeltaTime(0f);
			}
		}

		protected virtual void Start()
		{
			if (!isInitialized && IsReady)
			{
				Initialize();
				InitializedApplyDeltaTime(0f);
			}
			if (PlayAutomatically && Application.isPlaying)
			{
				Play();
			}
		}

		protected virtual void OnDisable()
		{
			if (isInitialized)
			{
				Deinitialize();
			}
		}

		protected virtual void Update()
		{
			if (UpdateIn == CurvyUpdateMethod.Update)
			{
				ApplyDeltaTime(TimeSinceLastUpdate);
			}
		}

		protected virtual void LateUpdate()
		{
			if (UpdateIn == CurvyUpdateMethod.LateUpdate || (!Application.isPlaying && UpdateIn == CurvyUpdateMethod.FixedUpdate))
			{
				ApplyDeltaTime(TimeSinceLastUpdate);
			}
		}

		protected virtual void FixedUpdate()
		{
			if (UpdateIn == CurvyUpdateMethod.FixedUpdate)
			{
				ApplyDeltaTime(TimeSinceLastUpdate);
			}
		}

		protected virtual void Reset()
		{
			UpdateIn = CurvyUpdateMethod.Update;
			PositionMode = CurvyPositionMode.Relative;
			m_Position = 0f;
			PlayAutomatically = true;
			MoveMode = MoveModeEnum.AbsolutePrecise;
			Speed = 0f;
			LockRotation = true;
			Clamping = CurvyClamping.Loop;
			OrientationMode = OrientationModeEnum.Orientation;
			OrientationAxis = OrientationAxisEnum.Up;
			IgnoreDirection = false;
		}

		protected virtual void InitializedApplyDeltaTime(float deltaTime)
		{
			if (state == CurvyControllerState.Playing && Speed * deltaTime != 0f)
			{
				float num = (OffsetCompensation ? ComputeOffsetCompensatedSpeed(deltaTime) : Speed);
				if (num * deltaTime != 0f)
				{
					Advance(num, deltaTime);
				}
			}
			Vector3 position = Transform.position;
			Quaternion rotation = Transform.rotation;
			ComputeTargetPositionAndRotation(out var targetPosition, out var targetUp, out var targetForward);
			Vector3 forward = ((!(DirectionDampingTime > 0f) || state != CurvyControllerState.Playing) ? targetForward : ((deltaTime > 0f) ? Vector3.SmoothDamp(Transform.forward, targetForward, ref directionDampingVelocity, DirectionDampingTime, float.PositiveInfinity, deltaTime) : Transform.forward));
			Vector3 upwards = ((!(UpDampingTime > 0f) || state != CurvyControllerState.Playing) ? targetUp : ((deltaTime > 0f) ? Vector3.SmoothDamp(Transform.up, targetUp, ref upDampingVelocity, UpDampingTime, float.PositiveInfinity, deltaTime) : Transform.up));
			Transform.rotation = Quaternion.LookRotation(forward, upwards);
			Transform.position = targetPosition;
			if (position.NotApproximately(Transform.position) || rotation.DifferentOrientation(Transform.rotation))
			{
				UserAfterUpdate();
			}
		}

		protected virtual void ComputeTargetPositionAndRotation(out Vector3 targetPosition, out Vector3 targetUp, out Vector3 targetForward)
		{
			GetInterpolatedSourcePosition(RelativePosition, out var interpolatedPosition, out var tangent, out var up);
			if (tangent == Vector3.zero || up == Vector3.zero)
			{
				GetOrientationNoneUpAndForward(out targetUp, out targetForward);
			}
			else
			{
				switch (OrientationMode)
				{
				case OrientationModeEnum.None:
					GetOrientationNoneUpAndForward(out targetUp, out targetForward);
					break;
				case OrientationModeEnum.Orientation:
				{
					Vector3 vector2 = ((m_Direction == MovementDirection.Backward && !IgnoreDirection) ? (-tangent) : tangent);
					switch (OrientationAxis)
					{
					case OrientationAxisEnum.Up:
						targetUp = up;
						targetForward = vector2;
						break;
					case OrientationAxisEnum.Down:
						targetUp = -up;
						targetForward = vector2;
						break;
					case OrientationAxisEnum.Forward:
						targetUp = -vector2;
						targetForward = up;
						break;
					case OrientationAxisEnum.Backward:
						targetUp = vector2;
						targetForward = -up;
						break;
					case OrientationAxisEnum.Left:
						targetUp = Vector3.Cross(up, vector2);
						targetForward = vector2;
						break;
					case OrientationAxisEnum.Right:
						targetUp = Vector3.Cross(vector2, up);
						targetForward = vector2;
						break;
					default:
						throw new NotSupportedException();
					}
					break;
				}
				case OrientationModeEnum.Tangent:
				{
					Vector3 vector = ((m_Direction == MovementDirection.Backward && !IgnoreDirection) ? (-tangent) : tangent);
					switch (OrientationAxis)
					{
					case OrientationAxisEnum.Up:
						targetUp = vector;
						targetForward = -up;
						break;
					case OrientationAxisEnum.Down:
						targetUp = -vector;
						targetForward = up;
						break;
					case OrientationAxisEnum.Forward:
						targetUp = up;
						targetForward = vector;
						break;
					case OrientationAxisEnum.Backward:
						targetUp = up;
						targetForward = -vector;
						break;
					case OrientationAxisEnum.Left:
						targetUp = up;
						targetForward = Vector3.Cross(up, vector);
						break;
					case OrientationAxisEnum.Right:
						targetUp = up;
						targetForward = Vector3.Cross(vector, up);
						break;
					default:
						throw new NotSupportedException();
					}
					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
			targetPosition = ApplyOffset(interpolatedPosition, tangent, up, OffsetAngle, OffsetRadius);
		}

		private void GetOrientationNoneUpAndForward(out Vector3 targetUp, out Vector3 targetForward)
		{
			if (LockRotation)
			{
				targetUp = lockedRotation * Vector3.up;
				targetForward = lockedRotation * Vector3.forward;
			}
			else
			{
				targetUp = Transform.up;
				targetForward = Transform.forward;
			}
		}

		protected virtual void Initialize()
		{
			isInitialized = true;
			lockedRotation = Transform.rotation;
			directionDampingVelocity = (upDampingVelocity = Vector3.zero);
			BindEvents();
			UserAfterInit();
			onInitialized.Invoke(this);
		}

		protected virtual void Deinitialize()
		{
			UnbindEvents();
			isInitialized = false;
		}

		protected virtual void BindEvents()
		{
		}

		protected virtual void UnbindEvents()
		{
		}

		protected virtual void SavePrePlayState()
		{
			prePlayPosition = m_Position;
			prePlayDirection = m_Direction;
		}

		protected virtual void RestorePrePlayState()
		{
			m_Position = prePlayPosition;
			m_Direction = prePlayDirection;
		}

		protected virtual void UserAfterInit()
		{
		}

		protected virtual void UserAfterUpdate()
		{
		}

		protected abstract void Advance(float speed, float deltaTime);

		protected abstract void SimulateAdvance(ref float tf, ref MovementDirection curyDirection, float speed, float deltaTime);

		protected abstract float AbsoluteToRelative(float worldUnitDistance);

		protected abstract float RelativeToAbsolute(float relativeDistance);

		protected abstract Vector3 GetInterpolatedSourcePosition(float tf);

		protected abstract void GetInterpolatedSourcePosition(float tf, out Vector3 interpolatedPosition, out Vector3 tangent, out Vector3 up);

		protected abstract Vector3 GetOrientation(float tf);

		protected abstract Vector3 GetTangent(float tf);

		public void Play()
		{
			if (PlayState == CurvyControllerState.Stopped)
			{
				SavePrePlayState();
			}
			state = CurvyControllerState.Playing;
		}

		public void Stop()
		{
			if (PlayState != CurvyControllerState.Stopped)
			{
				RestorePrePlayState();
			}
			state = CurvyControllerState.Stopped;
		}

		public void Pause()
		{
			if (PlayState == CurvyControllerState.Playing)
			{
				state = CurvyControllerState.Paused;
			}
		}

		public void Refresh()
		{
			ApplyDeltaTime(0f);
		}

		public void ApplyDeltaTime(float deltaTime)
		{
			if (!isInitialized && IsReady)
			{
				Initialize();
			}
			else if (isInitialized && !IsReady)
			{
				Deinitialize();
			}
			if (isInitialized)
			{
				InitializedApplyDeltaTime(deltaTime);
			}
		}

		public void TeleportTo(float newPosition)
		{
			float distance = Mathf.Abs(Position - newPosition);
			MovementDirection direction = ((!(Position < newPosition)) ? MovementDirection.Backward : MovementDirection.Forward);
			TeleportBy(distance, direction);
		}

		public void TeleportBy(float distance, MovementDirection direction)
		{
			float speed = Speed;
			MovementDirection movementDirection = MovementDirection;
			Speed = Mathf.Abs(distance) * 1000f;
			MovementDirection = direction;
			ApplyDeltaTime(0.001f);
			Speed = speed;
			MovementDirection = movementDirection;
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

		private static Vector3 ApplyOffset(Vector3 pos, Vector3 tan, Vector3 up, float angle, float radius)
		{
			Quaternion quaternion = Quaternion.AngleAxis(angle, tan);
			return pos + quaternion * up * radius;
		}

		protected static float GetClampedPosition(float position, CurvyPositionMode positionMode, CurvyClamping clampingMode, float length)
		{
			return positionMode switch
			{
				CurvyPositionMode.Relative => CurvyUtility.ClampTF(position, clampingMode), 
				CurvyPositionMode.WorldUnits => CurvyUtility.ClampDistance(position, clampingMode, length), 
				_ => throw new NotSupportedException(), 
			};
		}

		private float ComputeOffsetCompensatedSpeed(float deltaTime)
		{
			if (OrientationMode == OrientationModeEnum.None || OffsetRadius.Approximately(0f))
			{
				return Speed;
			}
			GetInterpolatedSourcePosition(RelativePosition, out var interpolatedPosition, out var tangent, out var up);
			Vector3 vector = ApplyOffset(interpolatedPosition, tangent, up, OffsetAngle, OffsetRadius);
			float tf = RelativePosition;
			MovementDirection curyDirection = m_Direction;
			SimulateAdvance(ref tf, ref curyDirection, Speed, deltaTime);
			GetInterpolatedSourcePosition(tf, out var interpolatedPosition2, out var tangent2, out var up2);
			Vector3 vector2 = ApplyOffset(interpolatedPosition2, tangent2, up2, OffsetAngle, OffsetRadius);
			float magnitude = (interpolatedPosition2 - interpolatedPosition).magnitude;
			float magnitude2 = (vector - vector2).magnitude;
			float num = magnitude / magnitude2;
			return Speed * (float.IsNaN(num) ? 1f : num);
		}

		public virtual void OnAfterDeserialize()
		{
			if (m_Speed < 0f)
			{
				m_Speed = Mathf.Abs(m_Speed);
				m_Direction = MovementDirection.Backward;
			}
			if ((short)MoveMode == 2)
			{
				MoveMode = MoveModeEnum.AbsolutePrecise;
			}
		}

		public void OnBeforeSerialize()
		{
		}
	}
}
