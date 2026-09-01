using Poly.Collide;
using Poly.Draw;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	public class DrawingTestCollisionListener : MonoBehaviour, ICollisionListener
	{
		public bool alwaysEnableVisualization;

		public float impulseScale = 50f;

		public float normalScale = 0.5f;

		public bool showDelayedImpulse;

		public bool showEstimatedImpulse;

		public bool showCollisionInfoIdx;

		private const float velocityThresholdForLoggingImpact = 0.01f;

		private static DrawingTestCollisionListener _instance;

		private bool enableVisualization => UberCollisionListener.enableVisualization;

		public static DrawingTestCollisionListener instance => _instance ?? (_instance = Object.FindObjectOfType<DrawingTestCollisionListener>());

		public void OnPolyCollisionEnter(in CollisionEvent e)
		{
			if (enableVisualization || alwaysEnableVisualization)
			{
				GlDrawer.color = Color.yellow;
				GlDrawer.DrawCircle(e.avgPosition, 0.5f);
				OnPolyCollisionStay(in e);
			}
		}

		public void OnPolyCollisionStay(in CollisionEvent e)
		{
			if (!enableVisualization && !alwaysEnableVisualization)
			{
				return;
			}
			for (int i = 0; i < e.numPoints; i++)
			{
				ref readonly ContactPointInfo reference = ref e.point0;
				if (i == 1)
				{
					reference = ref e.point1;
				}
				GlDrawer.color = Color.red;
				GlDrawer.DrawCircle(reference.position, 0.2f);
				float num = 0.101936795f;
				Vec2 vec = Vec2.right * 0.05f;
				if (showDelayedImpulse)
				{
					GlDrawer.color = Color.white;
					GlDrawer.DrawArrow(reference.position + vec, reference.delayedImpactImpulse * num * impulseScale);
				}
				else
				{
					GlDrawer.color = Color.yellow;
					_ = new Vec2(1f, 1f) * 2f;
					_ = new Vector3(1f, 1f, 1f) * 2f;
					GlDrawer.DrawArrow(reference.position + vec, reference.impulseApplied * num * impulseScale);
				}
				if (normalScale > 0f)
				{
					GlDrawer.color = Color.green;
					GlDrawer.DrawArrow(reference.position + vec, reference.normal * normalScale);
				}
				if (showEstimatedImpulse && Vec2.Dot(in reference.normal, in reference.impulseApplied) != 0f)
				{
					float estimatedImpactImpulseMultiplier = reference.estimatedImpactImpulseMultiplier;
					GlDrawer.color = Color.gray;
					GlDrawer.DrawLine(reference.position + Vec2.right * 0.5f, reference.position + Vec2.right * 0.5f + reference.impulseApplied * estimatedImpactImpulseMultiplier * impulseScale * num);
					if (Vec2.Dot(in e.relativeLinearVelocityBeforeCollision, in reference.normal) < -0.01f)
					{
						_ = reference.isNewImpact;
					}
				}
				if (showCollisionInfoIdx)
				{
					Vector3 position = reference.position + Vec2.down * 0.05f;
					int collisionInfoIdx_debug = e.collisionInfoIdx_debug;
					GlDrawer.DrawLabel(position, collisionInfoIdx_debug.ToString(), Color.gray);
				}
			}
		}

		public void OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
		}

		public void VerifyReset()
		{
		}

		public void OnPolyCollisionProcess_Internal(in CollisionEvent ePartial, ref CollisionInfo info)
		{
		}

		public void Clear()
		{
		}

		private void OnEnable()
		{
			UberCollisionListener.instance.listeners.Add(this);
		}

		private void OnDisable()
		{
			UberCollisionListener.instance.listeners.Remove(this);
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
