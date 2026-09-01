using System;
using Pb;
using Poly.Base;
using Poly.Collide;
using Poly.Draw;
using Poly.Extension;
using Poly.Math;
using Poly.Solver;
using Poly.UI;
using UnityEngine;

namespace Poly.Physics
{
	public class RefDistanceTracking : MonoBehaviour, ICollisionListener, IWorldListener
	{
		public bool freezeOnStart;

		public bool track2ndPoint;

		public Rigidbody bodyA;

		public Rigidbody bodyB;

		public InspectorButton freeze;

		public InspectorButton reset;

		private bool hasPoint;

		private ContactPointInfo cpInfo;

		private float refAngleA;

		private float refAngleB;

		private float accDeltaAngleA;

		private float accDeltaAngleB;

		private Vec2 refPosInA;

		private Vec2 refPosInB;

		private float validate_accDistanceA;

		private float validate_accDistanceB;

		public RefDistanceTracking()
		{
			freeze = new InspectorButton("Freeze", Freeze);
			reset = new InspectorButton("Reset timer", ResetDistance);
		}

		private void Freeze()
		{
			SingletonBehaviour<World>.instance.settings.scaledGravity = Vec2.zero;
			if ((bool)bodyA)
			{
				bodyA.motion.linVel = Vec2.zero;
			}
			if ((bool)bodyB)
			{
				bodyB.motion.linVel = Vec2.zero;
			}
			freeze.text = "Unfreeze";
			freeze.action = Unfreeze;
		}

		private void Unfreeze()
		{
			SingletonBehaviour<World>.instance.settings.scaledGravity = SingletonBehaviour<World>.instance.settings.gravity;
			freeze.text = "Freeze";
			freeze.action = Freeze;
		}

		private void ResetDistance()
		{
			hasPoint = false;
			refAngleA = (refAngleB = 0f);
			accDeltaAngleA = (accDeltaAngleB = 0f);
			refPosInA = (refPosInB = Vec2.zero);
			validate_accDistanceA = (validate_accDistanceB = 0f);
		}

		private void OnEnable()
		{
			bodyA.collisionListeners.Add(this);
			SingletonBehaviour<World>.instance.worldListeners.Add(this);
		}

		private void OnDisable()
		{
			if ((bool)bodyA)
			{
				bodyA.collisionListeners.Remove(this);
			}
			if (SingletonBehaviour<World>.instanceExists && SingletonBehaviour<World>.instance.worldListeners != null)
			{
				SingletonBehaviour<World>.instance.worldListeners.Remove(this);
			}
		}

		private void Start()
		{
			if (freezeOnStart)
			{
				Invoke("Freeze", 0.1f);
			}
		}

		public void OnPolyCollisionEnter(in CollisionEvent e)
		{
			OnPolyCollisionStay(in e);
		}

		public void OnPolyCollisionStay(in CollisionEvent e)
		{
		}

		public void OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
			hasPoint = false;
		}

		public void VerifyReset()
		{
		}

		public void OnPolyCollisionProcess_Internal(in CollisionEvent e, ref CollisionInfo info)
		{
			ValidateBodies();
			Component unityComponent = e.GetUnityComponent(0);
			Component unityComponent2 = e.GetUnityComponent(1);
			if ((!(unityComponent == bodyA) || !(unityComponent2 == bodyB)) && (!(unityComponent == bodyB) || !(unityComponent2 == bodyA)) && ((!(unityComponent == bodyA) && !(unityComponent2 == bodyA)) || (bool)bodyB))
			{
				return;
			}
			int numPoints = e.numPoints;
			if ((numPoints == 0 && !track2ndPoint) || (numPoints == 1 && track2ndPoint))
			{
				cpInfo = default(ContactPointInfo);
				cpInfo.position = info.contactPoint0;
				cpInfo.normal = info.normal;
				Transform2 t = e.a.Value.t2;
				Transform2 t2 = e.b.Value.t2;
				Vec2 vec = t.InvMul(info.contactPoint0);
				Vec2 vec2 = t2.InvMul(info.contactPoint1);
				Vec2 b = vec - refPosInA;
				Vec2 b2 = vec2 - refPosInB;
				refPosInA = vec;
				refPosInB = vec2;
				Vec2 a = t.rotation.InvMul(-info.tangent_slow);
				Vec2 a2 = t2.rotation.InvMul(info.tangent_slow);
				float num = Vec2.Dot(in a, in b);
				float num2 = Vec2.Dot(in a2, in b2);
				validate_accDistanceA += num;
				validate_accDistanceB += num2;
				if (!hasPoint)
				{
					validate_accDistanceA = (validate_accDistanceB = 0f);
				}
				float num3 = (float)System.Math.Atan2(info.normal.y, info.normal.x);
				float num4 = ((unityComponent.GetType() == typeof(Rigidbody)) ? ((Rigidbody)unityComponent).motion.angle : 0f);
				float num5 = ((unityComponent2.GetType() == typeof(Rigidbody)) ? ((Rigidbody)unityComponent2).motion.angle : 0f);
				num4 -= num3;
				num5 -= num3;
				float num6 = Pb.Mathf.WrapAngleToOnePi_Slow(num4 - refAngleA);
				float num7 = Pb.Mathf.WrapAngleToOnePi_Slow(num5 - refAngleB);
				refAngleA = num4;
				refAngleB = num5;
				_ = e.a.Value.shape.radius;
				_ = e.b.Value.shape.radius;
				accDeltaAngleA += num6;
				accDeltaAngleB += num7;
				if (!hasPoint)
				{
					accDeltaAngleA = (accDeltaAngleB = 0f);
				}
				float num8 = validate_accDistanceA + validate_accDistanceB;
				hasPoint = 0 < info.cacheValue.numContactPoints;
				ref ContactPointCache reference = ref info.cacheValue.pointCache0;
				if (1 == info.featureIdxInCache)
				{
					reference = ref info.cacheValue.pointCache1;
				}
				UnityEngine.Debug.Log($"({(track2ndPoint ? 1 : 0)}) Acc ang: {accDeltaAngleA * 57.29578f:#.000}  {accDeltaAngleB * 57.29578f:#.000} Acc pos: {validate_accDistanceA:#.000} {validate_accDistanceB:#.000} Combined pos: {num8:#.000} RefInCache: {reference.persistent_refSurfaceDistance2}");
			}
		}

		private void ValidateBodies()
		{
			if ((bool)bodyA)
			{
				float num = Pb.Mathf.WrapAngleOnceToOnePi(bodyA.t2.angle_slow * (MathF.PI / 180f));
				float num2 = Pb.Mathf.WrapAngleToOnePi_Slow(bodyA.motion.angle);
				UnityEngine.Mathf.DeltaAngle(num * 57.29578f, num2 * 57.29578f);
			}
			if ((bool)bodyB)
			{
				float num3 = Pb.Mathf.WrapAngleOnceToOnePi(bodyB.t2.angle_slow * (MathF.PI / 180f));
				float num4 = Pb.Mathf.WrapAngleToOnePi_Slow(bodyB.motion.angle);
				UnityEngine.Mathf.DeltaAngle(num3 * 57.29578f, num4 * 57.29578f);
			}
		}

		public void BeforeStep()
		{
			ValidateBodies();
		}

		public void AfterWorldCleared()
		{
		}

		public void AfterWorldFrameUpdate()
		{
		}

		public void AfterWorldFixedUpdate()
		{
		}

		private void OnDrawGizmos()
		{
			if (hasPoint)
			{
				GlDrawer.color = ColorEx.cableGray;
				GlDrawer.DrawCircle(cpInfo.position, 0.2f);
				GlDrawer.DrawArrow(cpInfo.position, cpInfo.normal);
				GlDrawer.color = ColorEx.lightGray;
				GlDrawer.DrawArrow(cpInfo.position, cpInfo.normal.rotated90);
			}
		}

		void ICollisionListener.OnPolyCollisionEnter(in CollisionEvent e)
		{
			OnPolyCollisionEnter(in e);
		}

		void ICollisionListener.OnPolyCollisionStay(in CollisionEvent e)
		{
			OnPolyCollisionStay(in e);
		}

		void ICollisionListener.OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
			OnPolyCollisionExit(a, b, receivingHandle, in cache);
		}

		void ICollisionListener.OnPolyCollisionProcess_Internal(in CollisionEvent ePartial, ref CollisionInfo info)
		{
			OnPolyCollisionProcess_Internal(in ePartial, ref info);
		}
	}
}
