using System;
using Poly.Math;
using Poly.Solver;
using UnityEngine;

namespace Poly.Collide
{
	public class ProcessCollision
	{
		private static bool warnOnce = true;

		public static void Collide_CircleCircle(ref HandlerInput input, ref HandlerOutput output)
		{
			Collide_CircleCircle(ref input, ref output.info0);
			TryAllowPointIntoOnePointManifold(ref input, ref output.info0);
		}

		public static void Collide_CircleCircle(ref HandlerInput input, ref CollisionInfo output)
		{
			Transform2 wTa = input.wTa;
			Transform2 wTb = input.wTb;
			Vec2 vec = wTb.position - wTa.position;
			float magnitude = vec.magnitude;
			if (1E-06f < magnitude)
			{
				output.normal = vec / magnitude;
			}
			else
			{
				output.normal = Vec2.down;
			}
			Circle c = (Circle)input.a;
			Circle c2 = (Circle)input.b;
			output.contactPoint0 = output.normal * c.radius + wTa.position;
			output.contactPoint1 = -output.normal * c2.radius + wTb.position;
			output.distance = magnitude - (c.radius + c2.radius);
			output.debug_type = CollisionType.TwoCircles;
			input.rotationState.angleNormal = (float)System.Math.Atan2(output.normal.y, output.normal.x);
			Collide_PLACEHOLDER_GetSomeCacheValuesSet(in c, in c2, ref output, in input.rotationState);
		}

		public static void Collide_PLACEHOLDER_GetSomeCacheValuesSet(in Circle c0, in Circle c1, ref CollisionInfo output, in RotationStateProcess rotationState)
		{
			Feature feature = default(Feature);
			feature.key = 0u;
			feature.type = Feature.Type.PointPoint;
			feature.vert0 = 0;
			feature.vert1 = 0;
			feature.vert2 = 0;
			if (output.cacheValue.numContactPoints == 0)
			{
				output.cacheValue.numContactPoints = 1;
				output.cacheValue.feature0 = feature;
				output.cacheValue.pointCache0.InitNewPoint();
				output.cacheValue.pointCache0.tOnEdge = 0f;
				output.cacheValue.pointCache0.tEdgeInvLen = 0f;
				output.cacheValue.pointCache0.refSurfaceDistance = 0f;
				output.cacheValue.pointCache0.tDistMultiplier = 0f;
			}
		}

		public static void Collide_SegmentCircle(ref HandlerInput input, ref HandlerOutput output)
		{
			Collide_SegmentCircle(ref input, ref output.info0);
			TryAllowPointIntoOnePointManifold(ref input, ref output.info0);
		}

		public static void TryAllowPointIntoOnePointManifold(ref HandlerInput input, ref CollisionInfo info)
		{
			float maxDistForNewPoint = input.maxDistForNewPoint;
			if (info.distance <= maxDistForNewPoint || 0 < info.cacheValue.numContactPoints)
			{
				info.cacheValue.numContactPoints = 1;
			}
			else if (info.distance > input.collisionTolerance)
			{
				info.cacheValue.numContactPoints = 0;
			}
			if (info.cacheValue.numContactPoints == 0)
			{
				info.distance = float.MaxValue;
			}
		}

		public static void Collide_SegmentCircle(ref HandlerInput input, ref CollisionInfo output)
		{
			Segment segment = (Segment)input.a;
			Circle c = (Circle)input.b;
			ref Transform2 wTa = ref input.wTa;
			Vec2 a = wTa.position - wTa.right * segment.halfLengthX;
			Vec2 b = wTa.position + wTa.right * segment.halfLengthX;
			Vec2 position = input.wTb.position;
			Vec2 a2 = b - a;
			Vec2 b2 = position - a;
			float num = Mathf.Clamp01(Vec2.Dot(in a2, in b2) / a2.sqrMagnitude);
			output.contactPoint0 = Vec2.LerpUnclamped(in a, in b, num);
			float magnitude = (position - output.contactPoint0).magnitude;
			if (1E-06f < magnitude)
			{
				output.normal = (position - output.contactPoint0) / magnitude;
			}
			else
			{
				output.normal = Vec2.up;
			}
			output.contactPoint0 += output.normal * segment.radius;
			output.contactPoint1 = position - output.normal * c.radius;
			output.distance = magnitude - (segment.radius + c.radius);
			output.debug_type = CollisionType.SegmentCircle;
			float num2 = Vec2.Dot(a2.rotated90, in b2);
			num2 = ((0f <= num2) ? 1f : (-1f));
			input.rotationState.angleNormal = (float)System.Math.Atan2(output.normal.y, output.normal.x);
			Collide_PLACEHOLDER_GetSomeCacheValuesSet(in wTa, in c, num, segment.halfLengthX, num2, ref output, in input.rotationState);
		}

		public static void Collide_PLACEHOLDER_GetSomeCacheValuesSet(in Transform2 wTa, in Circle c1, float tOnEdge, float edgeHalfLength, float tDistMultiplier, ref CollisionInfo output, in RotationStateProcess rotationState)
		{
			Feature feature = default(Feature);
			feature.key = 0u;
			feature.type = Feature.Type.EdgePoint;
			feature.vert0 = 0;
			feature.vert1 = 1;
			feature.vert2 = 0;
			if (output.cacheValue.numContactPoints == 0)
			{
				output.cacheValue.numContactPoints = 1;
				output.cacheValue.feature0 = feature;
				output.cacheValue.pointCache0.InitNewPoint();
				output.cacheValue.pointCache0.tOnEdge = tOnEdge;
				output.cacheValue.pointCache0.tEdgeInvLen = 1f / (edgeHalfLength * 2f + 5.877472E-39f);
				output.cacheValue.pointCache0.tDistMultiplier = tDistMultiplier;
				output.cacheValue.pointCache0.refSurfaceDistance = 0f;
				return;
			}
			ref ContactPointCache pointCache = ref output.cacheValue.pointCache0;
			if (0.011f < output.distance)
			{
				pointCache.refSurfaceDistance = 0f;
			}
			if (tDistMultiplier != 0f)
			{
				float num = pointCache.tOnEdge / pointCache.tEdgeInvLen;
				float num2 = tOnEdge * (2f * edgeHalfLength);
				pointCache.refSurfaceDistance += tDistMultiplier * (num2 - num);
				pointCache.tOnEdge = tOnEdge;
				pointCache.tEdgeInvLen = 1f / (edgeHalfLength * 2f + 5.877472E-39f);
				pointCache.tDistMultiplier = tDistMultiplier;
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RuntimeInitialize()
		{
			warnOnce = true;
		}

		public static void Collide_SegmentSegment(ref HandlerInput input, ref HandlerOutput output)
		{
			if (warnOnce)
			{
				Debug.LogWarning("Segment-segment collision: Doesn't handle ref-points, and position-based friction, callbacks, and messes up feature id's. Probably not used since early prototypes before Rigidbodies were added.");
				warnOnce = false;
			}
			Collide_SegmentSegment(ref input, ref output.info0);
			TryAllowPointIntoOnePointManifold(ref input, ref output.info0);
		}

		public static void Collide_SegmentSegment(ref HandlerInput input, ref CollisionInfo output)
		{
			Segment segment = (Segment)input.a;
			Segment segment2 = (Segment)input.b;
			Transform2 wTa = input.wTa;
			Transform2 wTb = input.wTb;
			Vec2 vec = wTa.position - wTa.right * segment.halfLengthX;
			Vec2 vec2 = wTa.position + wTa.right * segment.halfLengthX;
			Vec2 vec3 = wTb.position - wTb.right * segment2.halfLengthX;
			Vec2 vec4 = wTb.position + wTb.right * segment2.halfLengthX;
			Vec2 dir = vec2 - vec;
			Vec2 dir2 = vec4 - vec3;
			float magnitude = dir.magnitude;
			float magnitude2 = dir2.magnitude;
			dir /= magnitude;
			dir2 /= magnitude2;
			Vec2 closest;
			Vec2 closest2;
			Vector2 vector = CalcClosestPoint_Approx(vec, dir, magnitude, vec3, dir2, magnitude2, out closest, out closest2);
			output.contactPoint0 = closest;
			output.contactPoint1 = closest2;
			Vec2 vec5 = closest2 - closest;
			float separatingDistance = vec5.magnitude;
			if (1E-06f < separatingDistance)
			{
				output.normal = vec5 / separatingDistance;
			}
			else
			{
				CalcSeparatingNormal_Approx(dir, magnitude, vector.x, dir2, magnitude2, vector.y, out output.normal, out separatingDistance);
			}
			output.contactPoint0 += output.normal * segment.radius;
			output.contactPoint1 -= output.normal * segment2.radius;
			output.distance = separatingDistance - (segment.radius + segment2.radius);
			output.debug_type = CollisionType.TwoSegments;
		}

		public static Vector2 CalcClosestPoint_Approx(Vec2 start0, Vec2 dir0, float len0, Vec2 start1, Vec2 dir1, float len1, out Vec2 closest0, out Vec2 closest1)
		{
			float num = Vec2.Dot(in dir0, in dir1);
			Vec2 b = start0 - start1;
			float num2 = Vec2.Dot(in dir0, in b);
			float num3 = Vec2.Dot(in dir1, in b);
			float value = (num * num3 - num2) / (1f - num * num + 1E-12f);
			value = Mathf.Clamp(value, 0f, len0);
			float value2 = num * value + num3;
			value2 = Mathf.Clamp(value2, 0f, len1);
			value = num * value2 - num2;
			value = Mathf.Clamp(value, 0f, len0);
			closest0 = start0 + value * dir0;
			closest1 = start1 + value2 * dir1;
			return new Vector2(value / (len0 + 2.938736E-39f), value2 / (len1 + 2.938736E-39f));
		}

		public static void CalcSeparatingNormal_Approx(Vec2 dir0, float len0, float t0, Vec2 dir1, float len1, float t1, out Vec2 separatingNormal, out float separatingDistance)
		{
			float num = Mathf.Min(t0, 1f - t0);
			float num2 = Mathf.Min(t1, 1f - t1);
			float num3 = num * len0;
			float num4 = num2 * len1;
			bool flag;
			float num6;
			Vec2 a;
			if (num3 <= num4)
			{
				a = dir1.rotated90;
				float num5 = Vec2.Dot(in a, in dir0);
				flag = 0f <= num5 * (t0 - 0.5f);
				num6 = (0f - Mathf.Abs(num5)) * num3;
				a = -a;
			}
			else
			{
				a = dir0.rotated90;
				float num7 = Vec2.Dot(in a, in dir1);
				flag = 0f <= num7 * (t1 - 0.5f);
				num6 = (0f - Mathf.Abs(num7)) * num4;
			}
			if (flag)
			{
				a = -a;
			}
			separatingNormal = a;
			separatingDistance = num6;
		}

		public static Vec2 CalcClosestPoint_older_unused(Vec2 start0, Vec2 end0, Vec2 start1, Vec2 end1, out Vec2 closest0, out Vec2 closest1)
		{
			Vec2 a = end0 - start0;
			Vec2 b = end1 - start1;
			Vec2 b2 = start0 - start1;
			float num = Vec2.Dot(in a, in a);
			float num2 = Vec2.Dot(in a, in b);
			float num3 = Vec2.Dot(in b, in b);
			float num4 = Vec2.Dot(in a, in b2);
			float num5 = Vec2.Dot(in b, in b2);
			float num6 = num * num3 - num2 * num2;
			float num7 = num2 * num5 - num3 * num4;
			float value = ((!(num6 > Mathf.Abs(num7) / float.MaxValue)) ? 0.5f : (num7 / num6));
			value = Mathf.Clamp01(value);
			float num8 = (num2 * value + num5) / num3;
			if (num8 < 0f)
			{
				num8 = 0f;
				value = (0f - num4) / num;
				value = Mathf.Clamp01(value);
			}
			else if (num8 > 1f)
			{
				num8 = 1f;
				value = (0f - num4 + num2) / num;
				value = Mathf.Clamp01(value);
			}
			closest0 = start0 + value * a;
			closest1 = start1 + num8 * b;
			return new Vector2(value, num8);
		}
	}
}
