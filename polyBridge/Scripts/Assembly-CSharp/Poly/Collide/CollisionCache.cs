using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Poly.Extension;
using Poly.Physics;
using Poly.Solver;

namespace Poly.Collide
{
	public struct CollisionCache
	{
		public byte numContactPoints;

		public bool hasListeners;

		public byte closestFeatureIdx;

		public bool isReversed;

		public Feature feature0;

		public Feature feature1;

		public float oneLess_highSpeedFactor;

		public float highSpeedBlendTimeLeft;

		public ContactPointCache pointCache0;

		public ContactPointCache pointCache1;

		private static List<ICollisionListener> emptyList = new List<ICollisionListener>();

		public static ref ContactPointCache GetPointCache(ref CollisionCache cache, int idx)
		{
			if (idx == 0)
			{
				return ref cache.pointCache0;
			}
			return ref cache.pointCache1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddPoint(ref CollisionInfo temp_info, ref CollisionEvent collisionEvent)
		{
			if (numContactPoints == 0)
			{
				World.TriggerCollisionCallbacks_Enter(ref temp_info, ref collisionEvent);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear_AndTriggerExitCallbacks(in Vec2Short bpPair)
		{
			if (hasListeners && 0 < numContactPoints)
			{
				ShapeHandleIndex a = bpPair.x;
				ShapeHandleIndex b = bpPair.y;
				ref ShapeHandle reference = ref a.Get();
				ref ShapeHandle reference2 = ref b.Get();
				List<ICollisionListener> list = ((reference.entity != null) ? ((Rigidbody)reference.entity).collisionListeners : emptyList);
				List<ICollisionListener> list2 = ((reference2.entity != null) ? ((Rigidbody)reference2.entity).collisionListeners : emptyList);
				for (int i = 0; i < list.Count; i++)
				{
					list[i].OnPolyCollisionExit(a, b, ReceivingHandle.A, in this);
				}
				for (int j = 0; j < list2.Count; j++)
				{
					list2[j].OnPolyCollisionExit(a, b, ReceivingHandle.B, in this);
				}
			}
			numContactPoints = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Stay(ref CollisionInfo temp_info, ref CollisionEvent collisionEvent)
		{
			World.TriggerCollisionCallbacks_Stay(ref temp_info, ref collisionEvent);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Notice_CorrectFrictionAnglesOnly(in Vec2Short bpPair, Dictionary<int, float> bodyIdxToAngleCorrection, bool resetX, bool resetY)
		{
			if (0 >= numContactPoints)
			{
				return;
			}
			ShapeHandleIndex shapeHandleIndex = bpPair.x;
			ShapeHandleIndex shapeHandleIndex2 = bpPair.y;
			ref ShapeHandle reference = ref shapeHandleIndex.Get();
			ref ShapeHandle reference2 = ref shapeHandleIndex2.Get();
			int a = ((reference.entity != null) ? ((Rigidbody)reference.entity).worldIdx : (-1));
			int b = ((reference2.entity != null) ? ((Rigidbody)reference2.entity).worldIdx : (-1));
			if (isReversed)
			{
				Values.Swap(ref a, ref b);
			}
			if (bodyIdxToAngleCorrection.TryGetValue(a, out var value))
			{
				pointCache0.CorrectRefAngleA(value);
				if (2 == numContactPoints)
				{
					pointCache1.CorrectRefAngleA(value);
				}
			}
			if (bodyIdxToAngleCorrection.TryGetValue(b, out var value2))
			{
				pointCache0.CorrectRefAngleB(value2);
				if (2 == numContactPoints)
				{
					pointCache1.CorrectRefAngleB(value2);
				}
			}
		}
	}
}
