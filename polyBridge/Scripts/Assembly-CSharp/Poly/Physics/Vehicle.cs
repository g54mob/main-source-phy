using System;
using System.Linq;
using Poly.Base;
using Poly.Collide;
using Poly.Game;
using UnityEngine;

namespace Poly.Physics
{
	[SelectionBase]
	public class Vehicle : Action
	{
		public enum StrengthMethod
		{
			Acceleration = 0,
			MaxSlope = 1,
			TorquePerWheel = 2
		}

		[Header("Designer Balanced")]
		[Range(0.1f, 100f)]
		[SerializeField]
		[Tooltip("MASS: Scales mass of wheels & chassis proportionally, to reach this total mass.")]
		private float _mass;

		[Range(-30f, 30f)]
		[SerializeField]
		[Tooltip("SPEED: Vehicle will try to achieve this velocity, if engine is strong enough.")]
		public float _targetVelocity;

		[Range(0.1f, 100f)]
		[SerializeField]
		[Tooltip("ACC: Desired acceleration & deceleration till target velocity is achieved. Effective acceleration is limited by engine strength.")]
		private float _desiredAcceleration;

		[Range(0.1f, 100f)]
		[SerializeField]
		[Tooltip("HP: Define strength of engine, by max acceleration on level ground.")]
		private float _acceleration;

		[Range(0.1f, 100f)]
		[SerializeField]
		[Tooltip("BREAKING: Multiplies desired deceleration and also defines strength of brakes as a multiplier of engine strength.")]
		private float _brakingForceMultiplier = 1f;

		[Range(0.1f, 10f)]
		[SerializeField]
		[Tooltip("SHOCKS: Scale shocks strength & damping relatively to vehicle mass.")]
		private float _shocksMultiplier = 1f;

		[SerializeField]
		[Tooltip("IDLE: If acceleration/deceleration & velocity can be increased above given desired values by using gravity, it is done. When above target velocity, engine is disengaged and gravity is still used for acceleration.")]
		private bool _idleOnDownhill;

		[Header("Velocity control")]
		[SerializeField]
		[Tooltip("Multiplies desired deceleration and also defines strength of brakes as a multiplier of engine strength. Applied at high speed (>= 2x topSpeed)")]
		private float _highSpeedBrakingForceMultiplier = 1f;

		[Header("Engine strength & torque profile")]
		[SerializeField]
		[Tooltip("Define max engine strength by max acceleration, or max slope the vehicle can climb.")]
		private StrengthMethod _method;

		[Range(0.1f, 90f)]
		[SerializeField]
		[Tooltip("Define strength of engine, by max slope the car can continue climbing at constant speed.")]
		private float _maxSlope;

		[Range(0.1f, 100f)]
		[SerializeField]
		[Obsolete]
		private float _torquePerWheel;

		[Range(0.1f, 10000f)]
		[SerializeField]
		[Tooltip("Determines speed at which torque is zero. Torque is linearly interpolated from max value at zero speed, to zero value at top speed.")]
		private float _topSpeed = float.PositiveInfinity;

		[Range(0f, 10f)]
		[SerializeField]
		[Tooltip("If non-zero, used in the gameplay code to set top-speed as a multiple of targetSpeed.")]
		public float _topSpeedMultiplier;

		[Header("Suspension control")]
		[Tooltip("Automatically scale shocks strength & damping proportionally to vehicle mass.")]
		public bool scaleShocksWithMass = true;

		[Header("Gameplay orientation")]
		[SerializeField]
		private bool _isFlipped;

		[Header("Stop engines in air")]
		private bool _holdClutch;

		[NonSerialized]
		public bool isVisible = true;

		[NonSerialized]
		public Vector3 m_OriginalScale;

		public static bool debug_AlwaysRunEngine;

		internal WheelJoint[] allJoints;

		private WheelJoint[] allWheelJoints;

		private WheelJoint[] poweredWheelJoints;

		private float[] wheelRadii;

		internal Rigidbody[] allBodies;

		internal Rigidbody[] chassis;

		internal Rigidbody[] wheels;

		private bool _wasFlipped;

		private float _oldShocksMultiplier = 1f;

		private bool applyEditorFlip;

		private Vec2 flipPoint;

		private Vec2 flipDirection;

		public float targetVelocity
		{
			get
			{
				return _targetVelocity;
			}
			set
			{
				_targetVelocity = value;
				SetTargetVelocity(value);
			}
		}

		public float mass
		{
			get
			{
				return _mass;
			}
			set
			{
				_mass = SetMass(value);
				SetMethod(_method);
			}
		}

		public StrengthMethod strengthMethod
		{
			get
			{
				return _method;
			}
			set
			{
				_method = value;
				SetMethod(value);
			}
		}

		public float acceleration
		{
			get
			{
				return _acceleration;
			}
			set
			{
				_acceleration = value;
				SetMethod(StrengthMethod.Acceleration);
			}
		}

		public float maxSlope
		{
			get
			{
				return _maxSlope;
			}
			set
			{
				_maxSlope = value;
				SetMethod(StrengthMethod.MaxSlope);
			}
		}

		[Obsolete]
		public float torquePerWheel
		{
			get
			{
				return _torquePerWheel;
			}
			set
			{
				_torquePerWheel = value;
				SetMethod(StrengthMethod.TorquePerWheel);
			}
		}

		public bool idleOnDownhill
		{
			get
			{
				return _idleOnDownhill;
			}
			set
			{
				_idleOnDownhill = value;
				SetIdleOnDownhill(value);
			}
		}

		public float brakingForceMultiplier
		{
			get
			{
				return _brakingForceMultiplier;
			}
			set
			{
				_brakingForceMultiplier = value;
				SetBrakingForceMultiplier(value);
			}
		}

		public float highSpeedBrakingForceMultiplier
		{
			get
			{
				return _highSpeedBrakingForceMultiplier;
			}
			set
			{
				_highSpeedBrakingForceMultiplier = value;
				SetHighSpeedBrakingForceMultiplier(value);
			}
		}

		public bool isFlipped
		{
			get
			{
				return _isFlipped;
			}
			set
			{
				_isFlipped = value;
				if (_wasFlipped != _isFlipped)
				{
					FlipChassisAndJoints();
				}
			}
		}

		public float shocksMultiplier
		{
			get
			{
				return _shocksMultiplier;
			}
			set
			{
				_shocksMultiplier = value;
				SetShocksMultiplier(value);
			}
		}

		public float desiredAcceleration
		{
			get
			{
				return _desiredAcceleration;
			}
			set
			{
				_desiredAcceleration = value;
				SetDesiredAcceleration(value);
			}
		}

		public float topSpeed
		{
			get
			{
				return _topSpeed;
			}
			set
			{
				_topSpeed = value;
				SetTopSpeed(value);
			}
		}

		public bool isStarted => targetVelocity != 0f;

		public bool holdClutch
		{
			get
			{
				if (_holdClutch)
				{
					return !debug_AlwaysRunEngine;
				}
				return false;
			}
			set
			{
				_holdClutch = value && !debug_AlwaysRunEngine;
				if (debug_AlwaysRunEngine)
				{
					return;
				}
				if (_holdClutch && 0f != targetVelocity)
				{
					if (allBodies != null)
					{
						SetAcceleration(_acceleration / 1000f);
					}
				}
				else
				{
					acceleration = acceleration;
				}
			}
		}

		public float currentEngineRpm
		{
			get
			{
				float num = 0f;
				int num2 = poweredWheelJoints.Length;
				for (int i = 0; i < num2; i++)
				{
					num += poweredWheelJoints[i].currentVelocity * (MathF.PI / 180f) * wheelRadii[i];
				}
				num /= (float)num2;
				return Mathf.Clamp(num, 1f, 10f) * 500f;
			}
		}

		public float currentEngineTorqueFraction
		{
			get
			{
				float num = 0f;
				float num2 = 0f;
				WheelJoint[] array = poweredWheelJoints;
				foreach (WheelJoint wheelJoint in array)
				{
					num2 += wheelJoint.currentTorque;
					num += wheelJoint.maxMotorTorque;
				}
				if (1E-06f < num)
				{
					return Mathf.Abs(num2) / num;
				}
				return 0f;
			}
		}

		private bool isInitialized { get; set; }

		private void SetTargetVelocity(float newTargetVelocity)
		{
			if (isFlipped)
			{
				newTargetVelocity *= -1f;
			}
			if (base.isAddedToWorld)
			{
				if (poweredWheelJoints != null)
				{
					WheelJoint[] array = poweredWheelJoints;
					foreach (WheelJoint wheelJoint in array)
					{
						if ((bool)wheelJoint && (bool)wheelJoint.connectedBody && wheelJoint.connectedBody.isAddedToWorld && wheelJoint.connectedBody._shapeHandleIndices != null && wheelJoint.connectedBody._shapeHandleIndices.Length != 0 && World.shapeHandleArray != null && wheelJoint.connectedBody._shapeHandleIndices[0].index < World.shapeHandleArray.Length && (bool)SingletonBehaviour<World>.instance && SingletonBehaviour<World>.instance.collide != null && SingletonBehaviour<World>.instance.collide.shapeHandles != null && wheelJoint.connectedBody._shapeHandleIndices[0].index < SingletonBehaviour<World>.instance.collide.shapeHandles.Count)
						{
							AssertBodyIsWheel(wheelJoint.connectedBody);
							float radius = wheelJoint.connectedBody._shapeHandleIndices[0].Get().shape.radius;
							wheelJoint.targetMotorVelocity = newTargetVelocity / (radius + 1E-06f) * 57.29578f;
						}
						else
						{
							Debug.LogWarning("Earlier possible crash in release?");
						}
					}
				}
				else
				{
					Debug.LogWarning("Earlier possible crash in release?");
				}
			}
			holdClutch = holdClutch;
		}

		private void SetDesiredAcceleration(float desiredAcceleration)
		{
			if (!base.isAddedToWorld)
			{
				return;
			}
			if (poweredWheelJoints != null)
			{
				WheelJoint[] array = poweredWheelJoints;
				foreach (WheelJoint wheelJoint in array)
				{
					if ((bool)wheelJoint && (bool)wheelJoint.connectedBody && wheelJoint.connectedBody.isAddedToWorld && wheelJoint.connectedBody._shapeHandleIndices != null && wheelJoint.connectedBody._shapeHandleIndices.Length != 0 && World.shapeHandleArray != null && wheelJoint.connectedBody._shapeHandleIndices[0].index < World.shapeHandleArray.Length && (bool)SingletonBehaviour<World>.instance && SingletonBehaviour<World>.instance.collide != null && SingletonBehaviour<World>.instance.collide.shapeHandles != null && wheelJoint.connectedBody._shapeHandleIndices[0].index < SingletonBehaviour<World>.instance.collide.shapeHandles.Count)
					{
						AssertBodyIsWheel(wheelJoint.connectedBody);
						float radius = wheelJoint.connectedBody._shapeHandleIndices[0].Get().shape.radius;
						wheelJoint.desiredAcceleration = desiredAcceleration / (radius + 1E-06f) * 57.29578f;
					}
					else
					{
						Debug.LogWarning("Earlier possible crash in release?");
					}
				}
			}
			else
			{
				Debug.LogWarning("Earlier possible crash in release?");
			}
		}

		private void SetTopSpeed(float topSpeed)
		{
			if (!base.isAddedToWorld)
			{
				return;
			}
			if (poweredWheelJoints != null)
			{
				WheelJoint[] array = poweredWheelJoints;
				foreach (WheelJoint wheelJoint in array)
				{
					if ((bool)wheelJoint && (bool)wheelJoint.connectedBody && wheelJoint.connectedBody.isAddedToWorld && wheelJoint.connectedBody._shapeHandleIndices != null && wheelJoint.connectedBody._shapeHandleIndices.Length != 0 && World.shapeHandleArray != null && wheelJoint.connectedBody._shapeHandleIndices[0].index < World.shapeHandleArray.Length && (bool)SingletonBehaviour<World>.instance && SingletonBehaviour<World>.instance.collide != null && SingletonBehaviour<World>.instance.collide.shapeHandles != null && wheelJoint.connectedBody._shapeHandleIndices[0].index < SingletonBehaviour<World>.instance.collide.shapeHandles.Count)
					{
						AssertBodyIsWheel(wheelJoint.connectedBody);
						float radius = wheelJoint.connectedBody._shapeHandleIndices[0].Get().shape.radius;
						wheelJoint.topSpeed = topSpeed / (radius + 1E-06f) * 57.29578f;
					}
					else
					{
						Debug.LogWarning("Earlier possible crash in release?");
					}
				}
			}
			else
			{
				Debug.LogWarning("Earlier possible crash in release?");
			}
		}

		private float SetMass(float newMass)
		{
			if (allBodies != null)
			{
				float num = newMass / CalcCurrentMass_slow();
				newMass = 0f;
				Rigidbody[] array = allBodies;
				foreach (Rigidbody rigidbody in array)
				{
					rigidbody.mass *= num;
					rigidbody.inertia *= num;
					newMass += rigidbody.mass;
				}
				if (scaleShocksWithMass)
				{
					MultiplyShocksStrength_AfterMassUpdated(num);
				}
			}
			return newMass;
		}

		private void SetShocksMultiplier(float newMultiplier)
		{
			if (poweredWheelJoints != null)
			{
				float multiplier = newMultiplier / _oldShocksMultiplier;
				MultiplyShocksStrength_AfterMassUpdated(multiplier);
				_oldShocksMultiplier = newMultiplier;
			}
		}

		private void MultiplyShocksStrength_AfterMassUpdated(float multiplier)
		{
			WheelJoint[] array = allWheelJoints;
			foreach (WheelJoint obj in array)
			{
				obj.springConstant *= multiplier;
				obj.dampingConstant *= multiplier;
			}
		}

		private void SetMethod(StrengthMethod newMethod)
		{
			_method = newMethod;
			if (allBodies != null)
			{
				switch (newMethod)
				{
				case StrengthMethod.Acceleration:
					SetAcceleration(_acceleration);
					break;
				case StrengthMethod.MaxSlope:
					SetMaxSlope(_maxSlope);
					break;
				case StrengthMethod.TorquePerWheel:
					SetTorquePerWheel(_torquePerWheel);
					break;
				}
			}
		}

		private static void AssertBodyIsWheel(Rigidbody body)
		{
			_ = body.isAddedToWorld;
		}

		private void SetAcceleration(float newAcceleration, bool ignoreInertia = false)
		{
			newAcceleration = Mathf.Max(0f, newAcceleration);
			if (allBodies == null || poweredWheelJoints.Length == 0)
			{
				return;
			}
			float num = ComputeVirtualMassOfVehicle();
			float num2 = newAcceleration * (ignoreInertia ? _mass : num);
			float num3 = poweredWheelJoints.Sum((WheelJoint pwj) => pwj.maxMotorTorque / pwj.connectedBody._shapeHandleIndices[0].Get().shape.radius);
			WheelJoint[] array = poweredWheelJoints;
			foreach (WheelJoint wheelJoint in array)
			{
				AssertBodyIsWheel(wheelJoint.connectedBody);
				_ = wheelJoint.connectedBody._shapeHandleIndices[0].Get().shape.radius;
				if (1E-12f < num3 * num3)
				{
					wheelJoint.maxMotorTorque *= num2 / num3;
				}
				else
				{
					wheelJoint.maxMotorTorque = 0f;
				}
			}
		}

		private void SetMaxSlope(float newMaxSlope)
		{
			float b = Mathf.Sin(newMaxSlope * (MathF.PI / 180f)) * SingletonBehaviour<World>.instance.settings.gravityMagnitude;
			b = Mathf.Max(0f, b);
			SetAcceleration(b, ignoreInertia: true);
		}

		private void SetTorquePerWheel(float newTorquePerWheel)
		{
			newTorquePerWheel = Mathf.Max(0f, newTorquePerWheel);
			if (poweredWheelJoints != null)
			{
				WheelJoint[] array = poweredWheelJoints;
				foreach (WheelJoint obj in array)
				{
					AssertBodyIsWheel(obj.connectedBody);
					obj.maxMotorTorque = newTorquePerWheel;
				}
			}
		}

		private void SetIdleOnDownhill(bool value)
		{
			if (poweredWheelJoints != null)
			{
				WheelJoint[] array = poweredWheelJoints;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].idleOnDownhill = value;
				}
			}
		}

		private void SetBrakingForceMultiplier(float value)
		{
			if (poweredWheelJoints != null)
			{
				WheelJoint[] array = poweredWheelJoints;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].brakingForceMultiplier = value;
				}
			}
		}

		private void SetHighSpeedBrakingForceMultiplier(float value)
		{
			if (poweredWheelJoints != null)
			{
				WheelJoint[] array = poweredWheelJoints;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].highSpeedBrakingForceMultiplier = value;
				}
			}
		}

		public void FlipChassisAndJoints_EditorMode(bool doFlip, Vec2 flipPoint, Vec2 flipDirection)
		{
			if (doFlip)
			{
				applyEditorFlip = true;
				this.flipPoint = flipPoint;
				this.flipDirection = flipDirection;
				isFlipped = doFlip;
			}
		}

		private void FlipChassisAndJoints()
		{
			Rigidbody[] array;
			if (base.isAddedToWorld)
			{
				Vec2 vec = chassis[0].motion.com;
				Vec2 a = chassis[0].t2.right;
				if (wheels.Length >= 2)
				{
					vec = 0.5f * (wheels[0].motion.com + wheels.Last().motion.com);
				}
				if (chassis.Length >= 2)
				{
					a = (wheels.Last().motion.com - wheels[0].motion.com).normalized;
				}
				if (applyEditorFlip)
				{
					applyEditorFlip = false;
					vec = flipPoint;
					a = flipDirection;
				}
				array = chassis;
				foreach (Rigidbody rigidbody in array)
				{
					ShapeHandleIndex[] shapeHandleIndices = rigidbody._shapeHandleIndices;
					foreach (short num in shapeHandleIndices)
					{
						PolygonShape polygonShape = World.shapeHandleArray[num].shape as PolygonShape;
						if ((bool)polygonShape)
						{
							polygonShape.FlipX();
							base.world.collide.invalidateShapeIndices.Add(num);
						}
					}
					rigidbody.comTbody.position.x *= -1f;
					Vec2 linVel = rigidbody.motion.linVel;
					rigidbody.motion.com += -2f * Vec2.Dot(in a, rigidbody.motion.com - vec) * a;
					rigidbody.motion.linVel = linVel;
					rigidbody.flipScaleX = !rigidbody.flipScaleX;
				}
				if (chassis.Length > 1)
				{
					float[] array2 = chassis.Select((Rigidbody b) => b.motion.angle).Reverse().ToArray();
					for (int num2 = 0; num2 < chassis.Length; num2++)
					{
						chassis[num2].motion.angle = array2[num2];
					}
				}
				array = chassis;
				foreach (Rigidbody obj in array)
				{
					obj.PostFixedUpdate_Manual();
					obj.CacheTransform2();
					obj.CacheTransform2InShapeHandles_Util();
				}
				WheelJoint[] array3 = allJoints;
				foreach (WheelJoint obj2 in array3)
				{
					obj2.anchor.x *= -1f;
					obj2.pivot.x *= -1f;
					obj2.connectedAnchor.x *= -1f;
					obj2.connectedPivot.x *= -1f;
				}
				array = wheels;
				foreach (Rigidbody rigidbody2 in array)
				{
					Vec2 linVel2 = rigidbody2.motion.linVel;
					rigidbody2.motion.com += -2f * Vec2.Dot(in a, rigidbody2.motion.com - vec) * a;
					rigidbody2.motion.linVel = linVel2;
					float angVel = rigidbody2.motion.angVel;
					rigidbody2.motion.angle *= -1f;
					rigidbody2.motion.angVel = angVel;
					rigidbody2.CacheTransform2();
					rigidbody2.CacheTransform2InShapeHandles_Util();
				}
				_wasFlipped = _isFlipped;
			}
			SetTargetVelocity(targetVelocity);
			if (allBodies == null)
			{
				return;
			}
			array = allBodies;
			for (int i = 0; i < array.Length; i++)
			{
				ShapeHandleIndex[] shapeHandleIndices = array[i]._shapeHandleIndices;
				foreach (short item in shapeHandleIndices)
				{
					base.world.collide.invalidateShapeIndices.Add(item);
				}
			}
		}

		private new void OnValidate()
		{
			base.OnValidate();
			_highSpeedBrakingForceMultiplier = Mathf.Max(_highSpeedBrakingForceMultiplier, _brakingForceMultiplier);
			if (Application.isPlaying && allBodies != null && CalcCurrentMass_slow() != 0f)
			{
				targetVelocity = _targetVelocity;
				if (_mass != 0f)
				{
					mass = _mass;
				}
				else
				{
					_mass = CalcCurrentMass_slow();
				}
				strengthMethod = _method;
				idleOnDownhill = _idleOnDownhill;
				brakingForceMultiplier = _brakingForceMultiplier;
				highSpeedBrakingForceMultiplier = _highSpeedBrakingForceMultiplier;
				isFlipped = _isFlipped;
				shocksMultiplier = _shocksMultiplier;
				desiredAcceleration = _desiredAcceleration;
				topSpeed = _topSpeed;
				wheelRadii = new float[poweredWheelJoints.Length];
				for (int i = 0; i < poweredWheelJoints.Length; i++)
				{
					wheelRadii[i] = poweredWheelJoints[i].connectedBody._shapeHandleIndices[0].Get().shape.radius;
				}
				holdClutch = holdClutch;
				isInitialized = true;
			}
		}

		private new void Awake()
		{
			base.Awake();
			allJoints = GetComponentsInChildren<WheelJoint>();
			allWheelJoints = allJoints.Where((WheelJoint wj) => wj.enablePrismaticMovement).ToArray();
			poweredWheelJoints = allJoints.Where((WheelJoint wj) => wj.enablePrismaticMovement && wj.enableMotor).ToArray();
			m_OriginalScale = base.transform.localScale;
			Array.ForEach(allJoints, delegate(WheelJoint wj)
			{
				wj.isBreakable = true;
			});
			allJoints.Where((WheelJoint wj) => !wj.enablePrismaticMovement).ToList().ForEach(delegate(WheelJoint wj)
			{
				wj.applyAngularFriction = true;
			});
			if (!GetComponentInChildren<VehicleShutEngineWhileAirborneListener>())
			{
				base.gameObject.AddComponent<VehicleShutEngineWhileAirborneListener>();
			}
			if (!GetComponentInChildren<FallingRoadCheatDetectionListener>())
			{
				base.gameObject.AddComponent<FallingRoadCheatDetectionListener>();
			}
			_holdClutch = true;
		}

		public override void OnAddedToWorld()
		{
			Init_GatherChassisAndWheels();
			AddVehicleWithStuckRoadListener();
			OnValidate();
		}

		public override void Execute()
		{
			if (!isInitialized)
			{
				OnValidate();
			}
		}

		public void Init_GatherChassisAndWheels()
		{
			Rigidbody[] refBodies = allJoints.Select((WheelJoint wj) => wj.body).ToArray();
			allBodies = GetComponentsInChildren<Rigidbody>();
			chassis = allBodies.Where((Rigidbody b) => refBodies.Contains(b)).ToArray();
			wheels = allBodies.Where((Rigidbody b) => !refBodies.Contains(b)).ToArray();
			if (allBodies.Length > 1)
			{
				int num = int.MaxValue;
				int num2 = -1;
				for (int num3 = 0; num3 < chassis.Length; num3++)
				{
					int siblingIndex = chassis[num3].transform.GetSiblingIndex();
					if (siblingIndex < num)
					{
						num = siblingIndex;
						num2 = num3;
					}
				}
				if (0 < num2)
				{
					Values.Swap(ref chassis[0], ref chassis[num2]);
				}
			}
			Rigidbody[] array = wheels;
			for (int num4 = 0; num4 < array.Length; num4++)
			{
				array[num4].gameplayType_unused = GameplayType_Unused.VehicleWheel;
			}
		}

		private void AddVehicleWithStuckRoadListener()
		{
			VehicleWithStuckRoadListener vehicleWithStuckRoadListener = base.gameObject.AddComponent<VehicleWithStuckRoadListener>();
			vehicleWithStuckRoadListener.createDebugObjects = false;
			if (vehicleWithStuckRoadListener.isActiveAndEnabled)
			{
				vehicleWithStuckRoadListener.OnDisable();
			}
			vehicleWithStuckRoadListener.allBodies.AddRange(chassis);
			vehicleWithStuckRoadListener.allBodies.AddRange(wheels);
			if (vehicleWithStuckRoadListener.isActiveAndEnabled)
			{
				vehicleWithStuckRoadListener.OnEnable();
			}
		}

		private float CalcCurrentMass_slow()
		{
			float num = 0f;
			Rigidbody[] array = allBodies;
			foreach (Rigidbody rigidbody in array)
			{
				num += rigidbody.mass;
			}
			return num;
		}

		private float ComputeVirtualMassOfVehicle()
		{
			float num = 0f;
			Rigidbody[] array = chassis;
			foreach (Rigidbody rigidbody in array)
			{
				num += rigidbody.mass;
			}
			array = wheels;
			foreach (Rigidbody rigidbody2 in array)
			{
				num += rigidbody2.mass + rigidbody2.inertia / (rigidbody2._shapeHandleIndices[0].Get().shape.radius + 1E-12f);
			}
			return num;
		}

		public void DisableCollisions()
		{
			Rigidbody[] array = allBodies;
			foreach (Rigidbody rigidbody in array)
			{
				if ((bool)rigidbody && rigidbody.isAddedToWorld && rigidbody._shapeHandleIndices != null)
				{
					ShapeHandleIndex[] shapeHandleIndices = rigidbody._shapeHandleIndices;
					for (int j = 0; j < shapeHandleIndices.Length; j++)
					{
						ShapeHandleIndex shapeHandleIndex = shapeHandleIndices[j];
						if (World.shapeHandleArray != null && shapeHandleIndex.index < World.shapeHandleArray.Length && (bool)SingletonBehaviour<World>.instance && SingletonBehaviour<World>.instance.collide != null && SingletonBehaviour<World>.instance.collide.shapeHandles != null && shapeHandleIndex.index < SingletonBehaviour<World>.instance.collide.shapeHandles.Count)
						{
							shapeHandleIndex.Get().layer = Layer.CollideNothing;
						}
						else
						{
							Debug.LogWarning("Earlier possible crash in release?");
						}
					}
				}
				else
				{
					Debug.LogWarning("Earlier possible crash in release?");
				}
			}
		}
	}
}
