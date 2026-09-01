using System;
using Poly.Math;
using Poly.Solver;
using UnityEngine;

namespace Poly.Collide
{
	public static class CollidePolygon
	{
		private static Vec2[] circleVerts = new Vec2[1] { Vec2.zero };

		private static float[] circleInvLengths = new float[1];

		private static Vec2[] segmentVerts = new Vec2[2]
		{
			0.5f * Vec2.left,
			0.5f * Vec2.right
		};

		private static float[] segmentInvLengths = new float[2] { 1f, 1f };

		private static PolygonShape polyB_buffer = new PolygonShape();

		public static void Collide_PolygonPolygon(ref HandlerInput input, ref HandlerOutput output)
		{
			Collide_PolygonPolygon(ref input, ref output.info0, ref output);
		}

		public static void Collide_PolygonPolygon(ref HandlerInput input, ref CollisionInfo output, ref HandlerOutput handlerOutput)
		{
			Transform2 wTa = input.wTa;
			Transform2 wTb = input.wTb;
			PolygonShape polyA = (PolygonShape)input.a;
			PolygonShape polyB = (PolygonShape)input.b;
			PolygonCollisionProcess.Init(ref polyA, ref wTa, ref polyB, ref wTb, out var process);
			PolygonIntersection.CalcClosestPoint(ref process, out var closestPoint);
			output.normal = wTa.rotation * closestPoint.normalInLocalA;
			output.contactPoint0 = wTa * closestPoint.pointInLocalA;
			output.contactPoint1 = wTb * closestPoint.pointInLocalB;
			output.distance = closestPoint.distance;
			output.debug_type = CollisionType.TwoPolygons;
			Feature feature = closestPoint.feature;
			input.rotationState.angleNormal = (float)System.Math.Atan2(output.normal.y, output.normal.x);
			Collide_TryAddPointToManifold(ref output.cacheValue, ref process, ref closestPoint, ref input, output.entityTypes);
			handlerOutput.closestFeatureIdx = ((feature == output.cacheValue.feature1 && output.cacheValue.numContactPoints == 2) ? 1 : 0);
			output.cacheValue.closestFeatureIdx = (byte)handlerOutput.closestFeatureIdx;
			Collide_BuildDoublePoints(ref input, ref handlerOutput, ref process, ref closestPoint, ref wTa, ref wTb);
		}

		public static void Collide_PolygonSegment(ref HandlerInput input, ref HandlerOutput output)
		{
			Collide_PolygonSegment(ref input, ref output.info0, ref output);
		}

		public static void Collide_PolygonSegment(ref HandlerInput input, ref CollisionInfo output, ref HandlerOutput handlerOutput)
		{
			Transform2 wTa = input.wTa;
			Transform2 wTb = input.wTb;
			PolygonShape polyA = (PolygonShape)input.a;
			Segment segment = (Segment)input.b;
			polyB_buffer.verts = segmentVerts;
			segmentVerts[0] = new Vec2(0f - segment.halfLengthX, 0f);
			segmentVerts[1] = new Vec2(segment.halfLengthX, 0f);
			float num = 0.5f / segment.halfLengthX;
			polyB_buffer.invLengths = segmentInvLengths;
			segmentInvLengths[0] = num;
			segmentInvLengths[1] = num;
			polyB_buffer.radius = input.b.radius;
			PolygonCollisionProcess.Init(ref polyA, ref wTa, ref polyB_buffer, ref wTb, out var process);
			PolygonIntersection.CalcClosestPoint(ref process, out var closestPoint);
			output.normal = wTa.rotation * closestPoint.normalInLocalA;
			output.contactPoint0 = wTa * closestPoint.pointInLocalA;
			output.contactPoint1 = wTb * closestPoint.pointInLocalB;
			output.distance = closestPoint.distance;
			output.debug_type = CollisionType.PolygonSegment;
			Feature feature = closestPoint.feature;
			input.rotationState.angleNormal = (float)System.Math.Atan2(output.normal.y, output.normal.x);
			Collide_TryAddPointToManifold(ref output.cacheValue, ref process, ref closestPoint, ref input, output.entityTypes);
			handlerOutput.closestFeatureIdx = ((feature == output.cacheValue.feature1 && output.cacheValue.numContactPoints == 2) ? 1 : 0);
			output.cacheValue.closestFeatureIdx = (byte)handlerOutput.closestFeatureIdx;
			Collide_BuildDoublePoints(ref input, ref handlerOutput, ref process, ref closestPoint, ref wTa, ref wTb);
		}

		public static void Collide_BuildDoublePoints(ref HandlerInput input, ref HandlerOutput output, ref PolygonCollisionProcess process, ref ClosestPointProcess closestPoint, ref Transform2 wTa, ref Transform2 wTb)
		{
			int num = (output.numInfos = output.info0.cacheValue.numContactPoints);
			output.info0.onlyAIsWheel = process.vA.Length == 1 && 1 < process.vB_Count;
			output.info0.onlyBIsWheel = process.vB_Count == 1 && 1 < process.vA.Length;
			if (num == 2)
			{
				output.secondaryInfo1 = output.info0;
				output.secondaryInfo1.featureIdxInCache = 1;
				if (output.info0.cacheValue.feature0.key != closestPoint.feature.key)
				{
					float num2 = PolygonIntersection.CalcFeatureDistance(output.info0.cacheValue.feature0, ref process, out var _, out var _);
					num2 += process.radiusA + process.radiusB;
					PolygonIntersection.CalcClosestPoint_FromEdgeFeature(output.info0.cacheValue.feature0, num2, in process, out var closestPoint2);
					output.info0.normal = wTa.rotation * closestPoint2.normalInLocalA;
					output.info0.contactPoint0 = wTa * closestPoint2.pointInLocalA;
					output.info0.contactPoint1 = wTb * closestPoint2.pointInLocalB;
					output.info0.distance = closestPoint2.distance;
					input.rotationState.angleNormal = (float)System.Math.Atan2(output.info0.normal.y, output.info0.normal.x);
					output.info0.cacheValue.pointCache0.UpdateRefPoint(in closestPoint2, in input.rotationState, output.info0.entityTypes);
					output.secondaryInfo1.cacheValue.pointCache0 = output.info0.cacheValue.pointCache0;
				}
				if (output.secondaryInfo1.cacheValue.feature1.key != closestPoint.feature.key)
				{
					float num3 = PolygonIntersection.CalcFeatureDistance(output.secondaryInfo1.cacheValue.feature1, ref process, out var _, out var _);
					num3 += process.radiusA + process.radiusB;
					PolygonIntersection.CalcClosestPoint_FromEdgeFeature(output.secondaryInfo1.cacheValue.feature1, num3, in process, out var closestPoint3);
					output.secondaryInfo1.normal = wTa.rotation * closestPoint3.normalInLocalA;
					output.secondaryInfo1.contactPoint0 = wTa * closestPoint3.pointInLocalA;
					output.secondaryInfo1.contactPoint1 = wTb * closestPoint3.pointInLocalB;
					output.secondaryInfo1.distance = closestPoint3.distance;
					input.rotationState.angleNormal = (float)System.Math.Atan2(output.secondaryInfo1.normal.y, output.secondaryInfo1.normal.x);
					output.secondaryInfo1.cacheValue.pointCache1.UpdateRefPoint(in closestPoint3, in input.rotationState, output.secondaryInfo1.entityTypes);
					output.info0.cacheValue.pointCache1 = output.secondaryInfo1.cacheValue.pointCache1;
				}
			}
			else
			{
				_ = 1;
			}
		}

		public static float FeatureNormalDotNormal_NotNormalized(Feature f, ref PolygonCollisionProcess process, in Vector2 closestPointNormal)
		{
			return Vector2.Dot(PolygonIntersection.FeatureEdgeNormal_NotNormalized(f, ref process), closestPointNormal);
		}

		public static float FeatureNormalDotNormal_YesNormalized_InputMustNormalMustBeNormalized(Feature f, ref PolygonCollisionProcess process, in Vector2 closestPointNormal)
		{
			return Vector2.Dot(PolygonIntersection.FeatureEdgeNormal_YesNormalized(f, ref process), closestPointNormal);
		}

		public static void Collide_TryAddPointToManifold(ref CollisionCache cache, ref PolygonCollisionProcess process, ref ClosestPointProcess closestPoint, ref HandlerInput input, EntityTypes debug_entityTypes)
		{
			float maxDistForNewPoint = input.maxDistForNewPoint;
			float num = closestPoint.distance - 1E-06f - 0.0001f;
			float num2 = float.PositiveInfinity;
			float num3 = float.PositiveInfinity;
			ref RotationStateProcess rotationState = ref input.rotationState;
			switch (cache.numContactPoints)
			{
			case 0:
				if (closestPoint.distance <= maxDistForNewPoint)
				{
					cache.numContactPoints = 1;
					cache.feature0 = closestPoint.feature;
					num2 = closestPoint.distance;
					cache.pointCache0.InitNewPoint();
					cache.pointCache0.StoreRefPoint(in closestPoint, in rotationState);
				}
				break;
			case 1:
			{
				if (closestPoint.feature.key == cache.feature0.key)
				{
					if (input.collisionTolerance < closestPoint.distance)
					{
						cache.pointCache0.ClearOnRemoval();
						cache.pointCache0.ClearRefPoint();
						num2 = float.PositiveInfinity;
						cache.numContactPoints = 0;
						cache.feature0 = Feature.invalid;
					}
					else
					{
						num2 = closestPoint.distance;
						cache.pointCache0.UpdateRefPoint(in closestPoint, in rotationState, debug_entityTypes);
					}
					break;
				}
				if (Feature.AreFeaturesMatchingAndRelatedByShapeSidewaysMovement(in closestPoint.feature, in cache.feature0))
				{
					if (input.collisionTolerance < closestPoint.distance)
					{
						cache.pointCache0.ClearOnRemoval();
						cache.pointCache0.ClearRefPoint();
						num2 = float.PositiveInfinity;
						cache.numContactPoints = 0;
						cache.feature0 = Feature.invalid;
					}
					else
					{
						cache.pointCache0.ShiftFeature_Point();
						cache.pointCache0.ShiftFeature_RefPoint_AndUpdate(in closestPoint, in rotationState);
						cache.feature0 = closestPoint.feature;
						num2 = closestPoint.distance;
					}
					break;
				}
				num2 = PolygonIntersection.CalcFeatureDistance(cache.feature0, ref process, out var normal7, out var _);
				float num4 = FeatureNormalDotNormal_NotNormalized(cache.feature0, ref process, (Vector2)closestPoint.normalInLocalA);
				float num5 = FeatureNormalDotNormal_NotNormalized(cache.feature0, ref process, in normal7);
				if (input.collisionTolerance < num2 || num2 < num || num4 < 0f || num5 < 0f)
				{
					if (closestPoint.distance <= maxDistForNewPoint)
					{
						cache.pointCache0.ReplaceWithNewPoint();
						cache.pointCache0.ReplaceRefPoint(in closestPoint, in rotationState);
						cache.feature0 = closestPoint.feature;
						num2 = closestPoint.distance;
					}
					else
					{
						cache.pointCache0.ClearOnRemoval();
						cache.pointCache0.ClearRefPoint();
						cache.numContactPoints = 0;
						cache.feature0 = Feature.invalid;
						num2 = float.PositiveInfinity;
					}
				}
				else if (closestPoint.distance <= maxDistForNewPoint)
				{
					cache.numContactPoints = 2;
					cache.feature1 = closestPoint.feature;
					num3 = closestPoint.distance;
					cache.pointCache1.InitNewPoint();
					cache.pointCache1.StoreRefPoint(in closestPoint, in rotationState);
				}
				break;
			}
			case 2:
			{
				float dot2;
				float dotSelf2;
				float dot;
				float dotSelf;
				if (closestPoint.feature.key == cache.feature0.key)
				{
					num2 = closestPoint.distance;
					num3 = PolygonIntersection.CalcFeatureDistance(cache.feature1, ref process, out var normal, out var t);
					dot = 1f;
					dot2 = FeatureNormalDotNormal_NotNormalized(cache.feature1, ref process, (Vector2)closestPoint.normalInLocalA);
					dotSelf = 1f;
					dotSelf2 = FeatureNormalDotNormal_NotNormalized(cache.feature1, ref process, in normal);
					cache.pointCache0.UpdateRefPoint(in closestPoint, in rotationState, debug_entityTypes);
					if (dotSelf2 < 0f && VerifyAndMaybeShiftFeature(ref cache.feature1, ref process, in closestPoint.normalInLocalA, t, ref num3, ref dot2, ref dotSelf2))
					{
						cache.pointCache1.ShiftFeature_Point();
						cache.pointCache1.ShiftFeature_RefPoint_AndUpdate(in closestPoint, in rotationState);
					}
				}
				else if (closestPoint.feature.key == cache.feature1.key)
				{
					num2 = PolygonIntersection.CalcFeatureDistance(cache.feature0, ref process, out var normal2, out var t2);
					num3 = closestPoint.distance;
					dot = FeatureNormalDotNormal_NotNormalized(cache.feature0, ref process, (Vector2)closestPoint.normalInLocalA);
					dot2 = 1f;
					dotSelf = FeatureNormalDotNormal_NotNormalized(cache.feature0, ref process, in normal2);
					dotSelf2 = 1f;
					cache.pointCache1.UpdateRefPoint(in closestPoint, in rotationState, debug_entityTypes);
					if (dotSelf < 0f && VerifyAndMaybeShiftFeature(ref cache.feature0, ref process, in closestPoint.normalInLocalA, t2, ref num2, ref dot, ref dotSelf))
					{
						cache.pointCache0.ShiftFeature_Point();
						cache.pointCache0.ShiftFeature_RefPoint_AndUpdate(in closestPoint, in rotationState);
					}
				}
				else if (Feature.AreFeaturesMatchingAndRelatedByShapeSidewaysMovement(in cache.feature0, in closestPoint.feature))
				{
					cache.pointCache0.ShiftFeature_Point();
					cache.pointCache0.ShiftFeature_RefPoint_AndUpdate(in closestPoint, in rotationState);
					cache.feature0 = closestPoint.feature;
					num2 = closestPoint.distance;
					dot = 1f;
					dotSelf = 1f;
					num3 = PolygonIntersection.CalcFeatureDistance(cache.feature1, ref process, out var normal3, out var t3);
					dot2 = FeatureNormalDotNormal_NotNormalized(cache.feature1, ref process, (Vector2)closestPoint.normalInLocalA);
					dotSelf2 = FeatureNormalDotNormal_NotNormalized(cache.feature1, ref process, in normal3);
					if (dotSelf2 < 0f && VerifyAndMaybeShiftFeature(ref cache.feature1, ref process, in closestPoint.normalInLocalA, t3, ref num3, ref dot2, ref dotSelf2))
					{
						cache.pointCache1.ShiftFeature_Point();
						cache.pointCache1.ShiftFeature_RefPoint_AndUpdate(in closestPoint, in rotationState);
					}
				}
				else if (Feature.AreFeaturesMatchingAndRelatedByShapeSidewaysMovement(in cache.feature1, in closestPoint.feature))
				{
					cache.pointCache1.ShiftFeature_Point();
					cache.pointCache1.ShiftFeature_RefPoint_AndUpdate(in closestPoint, in rotationState);
					cache.feature1 = closestPoint.feature;
					num3 = closestPoint.distance;
					dot2 = 1f;
					dotSelf2 = 1f;
					num2 = PolygonIntersection.CalcFeatureDistance(cache.feature0, ref process, out var normal4, out var t4);
					dot = FeatureNormalDotNormal_NotNormalized(cache.feature0, ref process, (Vector2)closestPoint.normalInLocalA);
					dotSelf = FeatureNormalDotNormal_NotNormalized(cache.feature0, ref process, in normal4);
					if (dotSelf < 0f && VerifyAndMaybeShiftFeature(ref cache.feature0, ref process, in closestPoint.normalInLocalA, t4, ref num2, ref dot, ref dotSelf))
					{
						cache.pointCache0.ShiftFeature_Point();
						cache.pointCache0.ShiftFeature_RefPoint_AndUpdate(in closestPoint, in rotationState);
					}
				}
				else
				{
					num2 = PolygonIntersection.CalcFeatureDistance(cache.feature0, ref process, out var normal5, out var t5);
					num3 = PolygonIntersection.CalcFeatureDistance(cache.feature1, ref process, out var normal6, out var t6);
					dot = FeatureNormalDotNormal_YesNormalized_InputMustNormalMustBeNormalized(cache.feature0, ref process, (Vector2)closestPoint.normalInLocalA);
					dot2 = FeatureNormalDotNormal_YesNormalized_InputMustNormalMustBeNormalized(cache.feature1, ref process, (Vector2)closestPoint.normalInLocalA);
					dotSelf = FeatureNormalDotNormal_NotNormalized(cache.feature0, ref process, in normal5);
					dotSelf2 = FeatureNormalDotNormal_NotNormalized(cache.feature1, ref process, in normal6);
					if (dotSelf < 0f && VerifyAndMaybeShiftFeature(ref cache.feature0, ref process, in closestPoint.normalInLocalA, t5, ref num2, ref dot, ref dotSelf))
					{
						cache.pointCache0.ShiftFeature_Point();
						cache.pointCache0.ShiftFeature_RefPoint_AndUpdate(in closestPoint, in rotationState);
						dot = FeatureNormalDotNormal_YesNormalized_InputMustNormalMustBeNormalized(cache.feature0, ref process, (Vector2)closestPoint.normalInLocalA);
					}
					if (dotSelf2 < 0f && VerifyAndMaybeShiftFeature(ref cache.feature1, ref process, in closestPoint.normalInLocalA, t6, ref num3, ref dot2, ref dotSelf2))
					{
						cache.pointCache1.ShiftFeature_Point();
						cache.pointCache1.ShiftFeature_RefPoint_AndUpdate(in closestPoint, in rotationState);
						dot2 = FeatureNormalDotNormal_YesNormalized_InputMustNormalMustBeNormalized(cache.feature1, ref process, (Vector2)closestPoint.normalInLocalA);
					}
					if (closestPoint.distance < maxDistForNewPoint)
					{
						if (dot2 <= dot)
						{
							cache.pointCache1.ReplaceWithNewPoint();
							cache.pointCache1.ReplaceRefPoint(in closestPoint, in rotationState);
							cache.feature1 = closestPoint.feature;
							num3 = closestPoint.distance;
							dot2 = 1f;
							dotSelf2 = 1f;
						}
						else
						{
							cache.pointCache0.ReplaceWithNewPoint();
							cache.pointCache0.ReplaceRefPoint(in closestPoint, in rotationState);
							cache.feature0 = closestPoint.feature;
							num2 = closestPoint.distance;
							dot = 1f;
							dotSelf = 1f;
						}
					}
				}
				if (input.collisionTolerance < num2 || num2 < num || dot < 0f || dotSelf < 0f)
				{
					cache.pointCache0 = cache.pointCache1;
					cache.pointCache0.MoveRefPoint_FromOther(in cache.pointCache1);
					cache.pointCache1.ClearOnRemoval();
					cache.pointCache1.ClearRefPoint();
					num2 = num3;
					cache.feature0 = cache.feature1;
					cache.numContactPoints = 1;
					cache.feature1 = Feature.invalid;
					dot = dot2;
					dotSelf = dotSelf2;
					if (input.collisionTolerance < num2 || num2 < num || dot < 0f || dotSelf < 0f)
					{
						cache.pointCache0.ClearOnRemoval();
						cache.pointCache0.ClearRefPoint();
						num2 = float.PositiveInfinity;
						cache.numContactPoints = 0;
						cache.feature0 = Feature.invalid;
					}
				}
				else if (input.collisionTolerance < num3 || num3 < num || dot2 < 0f || dotSelf2 < 0f)
				{
					cache.pointCache1.ClearOnRemoval();
					cache.pointCache1.ClearRefPoint();
					num3 = float.PositiveInfinity;
					cache.numContactPoints = 1;
					cache.feature1 = Feature.invalid;
				}
				break;
			}
			}
		}

		public static bool VerifyAndMaybeShiftFeature(ref Feature feature, ref PolygonCollisionProcess process, in Vec2 closestPointNormalInLocalA, float t0, ref float featureDistance, ref float dot, ref float dotSelf)
		{
			Feature feature2 = feature;
			bool flag = false;
			if (feature2.type == Feature.Type.PointEdge)
			{
				if (1 < process.vA.Length)
				{
					if (t0 == 0f)
					{
						feature2.vert2 = feature2.vert1;
						feature2.vert1 = feature2.vert0;
						feature2.vert0 = (byte)((feature2.vert0 - 1 + process.vA.Length) % process.vA.Length);
					}
					else
					{
						feature2.vert1 = (byte)((feature2.vert0 + 1) % process.vA.Length);
					}
					feature2.type = Feature.Type.EdgePoint;
					flag = true;
				}
			}
			else if (1 < process.vB_Count)
			{
				if (t0 == 0f)
				{
					feature2.vert1 = (byte)((feature2.vert2 - 1 + process.vB_Count) % process.vB_Count);
				}
				else
				{
					feature2.vert0 = feature2.vert1;
					feature2.vert1 = feature2.vert2;
					feature2.vert2 = (byte)((feature2.vert2 + 1) % process.vB_Count);
				}
				feature2.type = Feature.Type.PointEdge;
				flag = true;
			}
			if (flag)
			{
				Vector2 normal;
				float t1;
				float num = PolygonIntersection.CalcFeatureDistance(feature2, ref process, out normal, out t1);
				float num2 = FeatureNormalDotNormal_NotNormalized(feature2, ref process, (Vector2)closestPointNormalInLocalA);
				float num3 = FeatureNormalDotNormal_NotNormalized(feature2, ref process, in normal);
				if (0f <= num3)
				{
					feature = feature2;
					featureDistance = num;
					dot = num2;
					dotSelf = num3;
					return true;
				}
			}
			return false;
		}

		public static void Collide_PolygonCircle(ref HandlerInput input, ref HandlerOutput output)
		{
			Collide_PolygonCircle(ref input, ref output.info0, ref output);
		}

		public static void Collide_PolygonCircle(ref HandlerInput input, ref CollisionInfo output, ref HandlerOutput handlerOutput)
		{
			Transform2 wTa = input.wTa;
			Transform2 wTb = input.wTb;
			PolygonShape polyA = (PolygonShape)input.a;
			polyB_buffer.verts = circleVerts;
			polyB_buffer.invLengths = circleInvLengths;
			polyB_buffer.radius = input.b.radius;
			PolygonCollisionProcess.Init(ref polyA, ref wTa, ref polyB_buffer, ref wTb, out var process);
			PolygonIntersection.CalcClosestPoint(ref process, out var closestPoint);
			output.normal = wTa.rotation * closestPoint.normalInLocalA;
			output.contactPoint0 = wTa * closestPoint.pointInLocalA;
			output.contactPoint1 = wTb * closestPoint.pointInLocalB;
			output.distance = closestPoint.distance;
			output.debug_type = CollisionType.PolygonCircle;
			Feature feature = closestPoint.feature;
			input.rotationState.angleNormal = (float)System.Math.Atan2(output.normal.y, output.normal.x);
			Collide_TryAddPointToManifold(ref output.cacheValue, ref process, ref closestPoint, ref input, output.entityTypes);
			handlerOutput.closestFeatureIdx = ((feature == output.cacheValue.feature1 && output.cacheValue.numContactPoints == 2) ? 1 : 0);
			output.cacheValue.closestFeatureIdx = (byte)handlerOutput.closestFeatureIdx;
			Collide_BuildDoublePoints(ref input, ref handlerOutput, ref process, ref closestPoint, ref wTa, ref wTb);
		}
	}
}
