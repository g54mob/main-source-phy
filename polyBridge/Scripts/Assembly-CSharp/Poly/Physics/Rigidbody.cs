using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Poly.Base;
using Poly.Collide;
using Poly.Collide.Unity;
using Poly.Extension;
using Poly.Math;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	[SelectionBase]
	public class Rigidbody : WorldObject, IEntity
	{
		public struct AnchorInfo
		{
			public Vec2 localPosition;

			public float mass;
		}

		internal short worldIdx;

		public float _mass = 1f;

		public float inertiaMultiplierDebug = 1f;

		public bool requestFullRecollision;

		[HideInInspector]
		public bool addUberCollisionListener = true;

		internal Transform3 comTbody = Transform3.identity;

		internal bool flipScaleX;

		internal Poly.Solver.Motion motion;

		internal Vec2 oldCom;

		internal float oldAngle;

		internal ShapeHandleIndex _singleShapeHandleIndex = (short)(-1);

		internal ShapeHandleIndex[] _shapeHandleIndices;

		internal Transform2 _t2 = Transform2.identity;

		public List<ICollisionListener> collisionListeners = new List<ICollisionListener>();

		public List<AnchorInfo> anchorsWithBorrowedMass;

		private short _rigidbodyCollisionGroupStartIndex = (short)Mathf.Max(Enum.GetValues(typeof(CollisionGroup)).Length, 10);

		public Rigidbody _collisionGroupParent;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[HideInInspector]
		internal CustomShapeCollisionInfo validate_CustomShapeCollisionInfo;

		public World world { get; private set; }

		public bool isAddedToWorld => worldIdx >= 0;

		internal Vec2 engine_comTbody => (Vec2)comTbody.position;

		public GameplayType_Unused gameplayType_unused { get; set; }

		public int shapeCount
		{
			get
			{
				if (0 > (short)_singleShapeHandleIndex)
				{
					return _shapeHandleIndices.Length;
				}
				return 1;
			}
		}

		public Transform3 interpolatedTransform
		{
			get
			{
				float currentFractionOfFixedFrame = SingletonBehaviour<World>.instance.currentFractionOfFixedFrame;
				Transform3 result = default(Transform3);
				result.position = Vec2.LerpUnclamped(in oldCom, in motion.com, currentFractionOfFixedFrame);
				result.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpUnclamped(oldAngle, motion.angle, currentFractionOfFixedFrame) * 57.29578f);
				result.position = result.position + world.transform.position + result.rotation * comTbody.position;
				result.rotation *= comTbody.rotation;
				return result;
			}
		}

		public Transform3 discreteTransform
		{
			get
			{
				Transform3 result = new Transform3(motion.com, Quaternion.Euler(0f, 0f, motion.angle * 57.29578f));
				result.position = result.position + world.transform.position + result.rotation * comTbody.position;
				result.rotation *= comTbody.rotation;
				return result;
			}
		}

		public Transform2 t2 => _t2;

		public float mass
		{
			get
			{
				if (motion.invMass == 0f)
				{
					return 0f;
				}
				return 1f / motion.invMass;
			}
			internal set
			{
				motion.invMass = ((0f < value) ? (1f / value) : 0f);
			}
		}

		public float inertia
		{
			get
			{
				if (motion.invInertia == 0f)
				{
					return 0f;
				}
				return 1f / motion.invInertia;
			}
			internal set
			{
				motion.invInertia = ((0f < value) ? (1f / value) : 0f);
			}
		}

		public Vector2 linearVelocity
		{
			get
			{
				return motion.linVel / world.settings.deltaTimeForVelocity;
			}
			set
			{
				motion.linVel = value * world.settings.deltaTimeForVelocity;
			}
		}

		public float angularVelocityDeg
		{
			get
			{
				return motion.angVel / world.settings.deltaTimeForVelocity * 57.29578f;
			}
			set
			{
				motion.angVel = value * world.settings.deltaTimeForVelocity * (MathF.PI / 180f);
			}
		}

		public short worldIndex => worldIdx;

		public short collisionGroup
		{
			get
			{
				if (_collisionGroupParent != null)
				{
					return _collisionGroupParent.collisionGroup;
				}
				if (mass == 0f)
				{
					return 0;
				}
				return (short)(worldIdx + _rigidbodyCollisionGroupStartIndex);
			}
		}

		public virtual void SetWorldAndIndex_Base(World world, int index)
		{
			this.world = world;
			worldIdx = (short)index;
		}

		public Shape GetShape_Slow(int idx)
		{
			return World.shapeHandleArray[(short)_shapeHandleIndices[idx]].shape;
		}

		public void CacheTransform2()
		{
			_t2.position = motion.com;
			Rotation2.SetRotation_Slow(motion.angle * 57.29578f, out _t2.rotation);
			_t2.position = _t2 * engine_comTbody;
		}

		public void CacheTransform2InShapeHandles_Util()
		{
			if ((short)_singleShapeHandleIndex >= 0)
			{
				ref ShapeHandle reference = ref World.shapeHandleArray[(short)_singleShapeHandleIndex];
				reference.t2 = t2;
				reference.fastLinearVel = motion.linVel;
				return;
			}
			ShapeHandleIndex[] shapeHandleIndices = _shapeHandleIndices;
			foreach (ShapeHandleIndex shapeHandleIndex in shapeHandleIndices)
			{
				ref ShapeHandle reference2 = ref World.shapeHandleArray[(short)shapeHandleIndex];
				reference2.t2 = t2;
				reference2.fastLinearVel = motion.linVel;
			}
		}

		[Obsolete]
		public void CacheTransform2_InSolver(in Poly.Solver.Motion motion)
		{
			_t2.position = motion.com;
			_t2.angle_slow = motion.angle * 57.29578f;
			_t2.position = _t2 * engine_comTbody;
		}

		public static void CacheTransform2_InCollide(in Poly.Solver.Motion motion, in Vec2 comTbody, out Transform2 t2_out)
		{
			t2_out.position = motion.com;
			Rotation2.SetRotation_Slow_NoAngleCheck(motion.angle * 57.29578f, out t2_out.rotation);
			t2_out.position = t2_out * comTbody;
		}

		internal void CreateSimulationBody(World world)
		{
			PolygonCollider[] componentsInChildren = GetComponentsInChildren<PolygonCollider>();
			RecollisionType recollisionType = ((!requestFullRecollision) ? RecollisionType.DistanceOnly : ((_mass != 0f) ? RecollisionType.Full_Rigidbody : RecollisionType.Full_RoadSegment));
			List<Shape> list = new List<Shape>();
			List<ShapeHandleIndex> list2 = new List<ShapeHandleIndex>();
			Transform2 shapeOrigin = base.transform;
			foreach (PolygonCollider polygonCollider in componentsInChildren)
			{
				PolygonShape[] array = polygonCollider.CreateConvexPolygons(in shapeOrigin);
				foreach (PolygonShape polygonShape in array)
				{
					polygonShape.SetPhysicsProperties(polygonCollider.physicsMaterial);
					list.Add(polygonShape);
					ShapeHandle h = ShapeHandle.Create();
					h.shape = polygonShape;
					h.layer = polygonCollider.layer;
					h.recollisionType = recollisionType;
					h.entity = this;
					short num = world.collide.AddShapeHandle(ref h);
					list2.Add(num);
				}
				if (polygonCollider.gameObject != base.gameObject)
				{
					UnityEngine.Object.Destroy(polygonCollider.gameObject);
					continue;
				}
				if (!polygonCollider.hasInternalPoints)
				{
					base.transform.DestroyAllChildren();
				}
				UnityEngine.Object.Destroy(polygonCollider);
			}
			if (list2.Count == 1)
			{
				_singleShapeHandleIndex = list2[0];
			}
			_shapeHandleIndices = list2.ToArray();
			InertiaInfo info = InertiaComputer.ComputeInfoFromShapes(list.ToArray());
			bool flag = anchorsWithBorrowedMass != null;
			if (flag)
			{
				InertiaComputer.SubtractInertiaFromAnchors(ref info, shapeOrigin, _mass, anchorsWithBorrowedMass);
				anchorsWithBorrowedMass.Clear();
				anchorsWithBorrowedMass = null;
			}
			CenterOfMassModifier componentInChildren = GetComponentInChildren<CenterOfMassModifier>();
			if ((bool)componentInChildren)
			{
				Vec2 vec = shapeOrigin.InvMul((Vec2)componentInChildren.transform.position);
				Vec2 vec2 = vec - info.com;
				info.inertiaFactorAroundCom += vec2.sqrMagnitude;
				info.com = vec;
			}
			float z = base.transform.rotation.eulerAngles.z;
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
			mass = _mass;
			if (info.inertiaFactorAroundCom * inertiaMultiplierDebug > 5.877472E-39f)
			{
				motion.invInertia = motion.invMass / (info.inertiaFactorAroundCom * inertiaMultiplierDebug);
			}
			else
			{
				motion.invInertia = 0f;
			}
			motion.com = (Vector2)(base.transform.position - world.transform.position) + (Vector2)(base.transform.rotation * info.com);
			motion.angle = z * (MathF.PI / 180f);
			motion.SetZeroVelocity();
			oldCom = motion.com;
			oldAngle = motion.angle;
			Transform3 transform = default(Transform3);
			transform.position = motion.com + world.transform.position;
			transform.rotation = base.transform.rotation;
			comTbody.position = Quaternion.Inverse(transform.rotation) * (base.transform.position - transform.position);
			comTbody.rotation = Quaternion.Inverse(transform.rotation) * base.transform.rotation;
			CacheTransform2();
			ShapeHandleIndex[] shapeHandleIndices = _shapeHandleIndices;
			for (int j = 0; j < shapeHandleIndices.Length; j++)
			{
				ShapeHandleIndex shapeHandleIndex = shapeHandleIndices[j];
				shapeHandleIndex.Get().t2 = t2;
				shapeHandleIndex.Get().fastLinearVel = motion.linVel;
			}
			if ((bool)validate_CustomShapeCollisionInfo && validate_CustomShapeCollisionInfo.validate_motion.HasValue && !flag)
			{
				_ = validate_CustomShapeCollisionInfo.validate_motion.Value;
			}
		}

		internal void DestroySimulationBody()
		{
			if (_shapeHandleIndices != null)
			{
				short[] array = _shapeHandleIndices.Select((ShapeHandleIndex shi) => shi.index).ToArray();
				Array.Sort(array);
				for (int num = array.Length - 1; num >= 0; num--)
				{
					if (array[num] >= 0)
					{
						world.collide.RemoveShapeHandle(array[num]);
					}
				}
				_shapeHandleIndices = null;
				_singleShapeHandleIndex = (short)(-1);
			}
			collisionListeners.Clear();
		}

		public virtual void SetWorldAndIndex(World world, int index)
		{
			SetWorldAndIndex_Base(world, index);
			if (_shapeHandleIndices != null && (bool)world)
			{
				ShapeHandleIndex[] shapeHandleIndices = _shapeHandleIndices;
				foreach (ShapeHandleIndex shapeHandleIndex in shapeHandleIndices)
				{
					World.shapeHandleArray[(short)shapeHandleIndex].motionIdx = (short)index;
				}
			}
		}

		public void UpdateShapeHandleIndex(short oldIndex, short newIndex)
		{
			if ((short)_singleShapeHandleIndex == oldIndex)
			{
				_singleShapeHandleIndex = newIndex;
			}
			for (int i = 0; i < _shapeHandleIndices.Length; i++)
			{
				if ((short)_shapeHandleIndices[i] == oldIndex)
				{
					_shapeHandleIndices[i] = newIndex;
					break;
				}
			}
		}

		public void PostFixedUpdate_Manual()
		{
			Draw_UpdateTransformAndFlip();
		}

		private new void Awake()
		{
			base.Awake();
			worldIdx = -1;
		}

		private new void OnValidate()
		{
			base.OnValidate();
		}

		private new void OnDestroy()
		{
			base.OnDestroy();
		}

		private new void OnEnable()
		{
			base.OnEnable();
			Registry<Rigidbody>.Add(this);
		}

		private new void OnDisable()
		{
			base.OnDisable();
			Registry<Rigidbody>.Remove(this);
		}

		private void Draw_UpdateTransformAndFlip()
		{
			if (isAddedToWorld)
			{
				if (!float.IsNaN(motion.com.sqrMagnitude))
				{
					base.transform.position = motion.com;
					base.transform.rotation = Quaternion.Euler(0f, 0f, motion.angle * 57.29578f);
					base.transform.position = base.transform.position + world.transform.position + base.transform.rotation * comTbody.position;
					base.transform.rotation = base.transform.rotation * comTbody.rotation;
					base.transform.SetLocalScaleX((flipScaleX ? (-1f) : 1f) * Mathf.Abs(base.transform.localScale.x));
					base.transform.hasChanged = false;
				}
				else
				{
					UnityEngine.Debug.Log("Rigidbody exploded to infinity: " + base.gameObject.name);
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		internal static Poly.Solver.Motion CalculateFinalMassInertiaAndCom(Rigidbody body, Vec2 worldTransformPosition)
		{
			PolygonCollider[] componentsInChildren = body.GetComponentsInChildren<PolygonCollider>();
			Transform2 shapeOrigin = body.transform;
			List<Shape> list = new List<Shape>();
			PolygonCollider[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				PolygonShape[] array2 = array[i].CreateConvexPolygons(in shapeOrigin);
				foreach (PolygonShape item in array2)
				{
					list.Add(item);
				}
			}
			InertiaInfo inertiaInfo = InertiaComputer.ComputeInfoFromShapes(list.ToArray());
			Poly.Solver.Motion result = new Poly.Solver.Motion
			{
				invMass = ((0f < body._mass) ? (1f / body._mass) : 0f)
			};
			if (inertiaInfo.inertiaFactorAroundCom * body.inertiaMultiplierDebug > 5.877472E-39f)
			{
				result.invInertia = result.invMass / (inertiaInfo.inertiaFactorAroundCom * body.inertiaMultiplierDebug);
			}
			else
			{
				result.invInertia = 0f;
			}
			result.com = (Vec2)body.transform.position - worldTransformPosition + (Vec2)(body.transform.rotation * inertiaInfo.com);
			return result;
		}
	}
}
