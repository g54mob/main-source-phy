using System;
using Poly.Extension;
using UnityEngine;

namespace Poly.Physics
{
	public struct CollisionEvent : IDisposable
	{
		public ShapeHandle? a;

		public ShapeHandle? b;

		public ShapeHandleIndex idxA;

		public ShapeHandleIndex idxB;

		public bool isReversed;

		public int numPoints;

		public ReceivingHandle receivingHandle;

		public ContactPointInfo point0;

		public ContactPointInfo point1;

		public Vec2 relativeLinearVelocityBeforeCollision;

		public float relativeAngularVelocityBeforeCollisionInDeg;

		public int collisionInfoIdx_debug;

		public Vec2 avgPosition
		{
			get
			{
				Vec2 result = Vec2.zero;
				if (numPoints == 1)
				{
					result = point0.position;
				}
				else if (numPoints == 2)
				{
					result = 0.5f * (point0.position + point1.position);
				}
				return result;
			}
		}

		public float avgTangentVelocity
		{
			get
			{
				float num = 0f;
				if (numPoints > 0)
				{
					num = point0.tangentVelocity;
				}
				if (numPoints > 1)
				{
					num += point1.tangentVelocity;
				}
				return num / ((float)numPoints + 1E-24f);
			}
		}

		public float sumNormalImpulsesApplied
		{
			get
			{
				float num = 0f;
				if (numPoints > 0)
				{
					num = Vec2.Dot(in point0.normal, in point0.impulseApplied);
				}
				if (numPoints > 1)
				{
					num += Vec2.Dot(in point1.normal, in point1.impulseApplied);
				}
				return num;
			}
		}

		public Component GetUnityComponent(int idx)
		{
			return idx switch
			{
				0 => a.Value.GetUnityComponent(), 
				1 => b.Value.GetUnityComponent(), 
				_ => null, 
			};
		}

		public Vec2Short GetContactID()
		{
			ReceivingHandle receivingHandle = this.receivingHandle;
			if (isReversed)
			{
				receivingHandle = 1 - receivingHandle;
			}
			if (!isReversed)
			{
				return GetContactID(idxA, idxB, receivingHandle);
			}
			return GetContactID(idxB, idxA, receivingHandle);
		}

		public static Vec2Short GetContactID(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle)
		{
			if (receivingHandle != ReceivingHandle.A)
			{
				return new Vec2Short(b, a);
			}
			return new Vec2Short(a, b);
		}

		public void Dispose()
		{
			a = null;
			b = null;
		}
	}
}
