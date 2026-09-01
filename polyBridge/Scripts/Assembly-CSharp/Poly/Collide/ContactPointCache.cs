using System.Runtime.CompilerServices;
using Pb;

namespace Poly.Collide
{
	public struct ContactPointCache
	{
		public float referencePenetrationDistance;

		public float sumVelImpulses_PrevFrame;

		public float sumFullImpulses_PrevFrame;

		public float sumFrictionImpulses_PrevFrame;

		public float velImpulse_SinceIntegration;

		public float fullImpulse_SinceIntegration;

		public float frictionImpulse_SinceIntegration;

		public float tOnEdge;

		public float tEdgeInvLen;

		public float tDistMultiplier;

		public float refAngleA;

		public float refAngleB;

		public float refAngleN;

		public float refSurfaceDistance;

		public float persistent_refSurfaceDistance;

		public float persistent_refSurfaceDistance2;

		public int numFramesWithNonZeroImpulse;

		public bool impulseEventWithheld;

		public bool depthInitialized
		{
			get
			{
				return referencePenetrationDistance != float.MaxValue;
			}
			set
			{
				if (!value)
				{
					referencePenetrationDistance = float.MaxValue;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Reset()
		{
			depthInitialized = false;
			sumVelImpulses_PrevFrame = 0f;
			sumFullImpulses_PrevFrame = 0f;
			sumFrictionImpulses_PrevFrame = 0f;
			velImpulse_SinceIntegration = 0f;
			fullImpulse_SinceIntegration = 0f;
			frictionImpulse_SinceIntegration = 0f;
			impulseEventWithheld = false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InitNewPoint()
		{
			Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ShiftFeature_Point()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ReplaceWithNewPoint()
		{
			Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClearOnRemoval()
		{
			Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void StoreRefPoint(in ClosestPointProcess closestPoint, in RotationStateProcess rotationProcess)
		{
			tOnEdge = closestPoint.tOnEdge;
			tEdgeInvLen = closestPoint.tEdgeInvLen;
			tDistMultiplier = closestPoint.tDistMultiplier;
			refAngleA = rotationProcess.angleA;
			refAngleB = rotationProcess.angleB;
			refAngleN = rotationProcess.angleNormal;
			refSurfaceDistance = 0f;
			persistent_refSurfaceDistance = 0f;
			persistent_refSurfaceDistance2 = 0f;
			numFramesWithNonZeroImpulse = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ShiftFeature_RefPoint_AndUpdate(in ClosestPointProcess closestPoint, in RotationStateProcess rotationProcess)
		{
			tOnEdge = closestPoint.tOnEdge;
			tEdgeInvLen = closestPoint.tEdgeInvLen;
			tDistMultiplier = closestPoint.tDistMultiplier;
			refAngleA = rotationProcess.angleA;
			refAngleB = rotationProcess.angleB;
			refAngleN = rotationProcess.angleNormal;
			refSurfaceDistance = persistent_refSurfaceDistance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ReplaceRefPoint(in ClosestPointProcess closestPoint, in RotationStateProcess rotationProcess)
		{
			tOnEdge = closestPoint.tOnEdge;
			tEdgeInvLen = closestPoint.tEdgeInvLen;
			tDistMultiplier = closestPoint.tDistMultiplier;
			refAngleA = rotationProcess.angleA;
			refAngleB = rotationProcess.angleB;
			refAngleN = rotationProcess.angleNormal;
			refSurfaceDistance = 0f;
			persistent_refSurfaceDistance = 0f;
			persistent_refSurfaceDistance2 = 0f;
			numFramesWithNonZeroImpulse = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveRefPoint_FromOther(in ContactPointCache other)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void UpdateRefPoint(in ClosestPointProcess closestPoint, in RotationStateProcess rotationProcess, EntityTypes debug_entityTypes)
		{
			if (tDistMultiplier != 0f)
			{
				float num = tOnEdge / tEdgeInvLen;
				float num2 = closestPoint.tOnEdge / closestPoint.tEdgeInvLen;
				refSurfaceDistance += closestPoint.tDistMultiplier * (num2 - num);
				persistent_refSurfaceDistance += closestPoint.tDistMultiplier * (num2 - num);
				persistent_refSurfaceDistance2 += closestPoint.tDistMultiplier * (num2 - num);
				tOnEdge = closestPoint.tOnEdge;
				tEdgeInvLen = closestPoint.tEdgeInvLen;
				tDistMultiplier = closestPoint.tDistMultiplier;
			}
			float num3 = rotationProcess.angleA - refAngleA;
			float num4 = rotationProcess.angleB - refAngleB;
			float num5 = Mathf.WrapAngleOnceToOnePi(rotationProcess.angleNormal - refAngleN);
			refAngleA = rotationProcess.angleA;
			refAngleB = rotationProcess.angleB;
			refAngleN = rotationProcess.angleNormal;
			num3 -= num5;
			num4 -= num5;
			persistent_refSurfaceDistance += num3 * rotationProcess.radiusA + num4 * rotationProcess.radiusB;
			persistent_refSurfaceDistance2 += num3 * rotationProcess.radiusA + num4 * rotationProcess.radiusB;
			refSurfaceDistance = persistent_refSurfaceDistance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClearRefPoint()
		{
			tOnEdge = 0f;
			tEdgeInvLen = 0f;
			tDistMultiplier = 0f;
			refAngleA = 0f;
			refAngleB = 0f;
			refAngleN = 0f;
			refSurfaceDistance = 0f;
			persistent_refSurfaceDistance = 0f;
			persistent_refSurfaceDistance2 = 0f;
			numFramesWithNonZeroImpulse = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CorrectRefAngleA(float correction)
		{
			refAngleA -= correction;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CorrectRefAngleB(float correction)
		{
			refAngleB -= correction;
		}
	}
}
