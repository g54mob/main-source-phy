using System;
using Poly.Extension;
using Poly.Math;
using Poly.Physics;
using UnityEngine;

namespace Poly.Collide
{
	public static class PolygonIntersection
	{
		private static Vec2[] vB_buffer = new Vec2[32];

		private static Vec2[] segmentVerts = new Vec2[2]
		{
			0.5f * Vec2.left,
			0.5f * Vec2.right
		};

		private static float[] segmentInvLengths = new float[2] { 1f, 1f };

		private static PolygonShape polyB_buffer = new PolygonShape();

		public const float LargeCollisionEpsilon = 0.0001f;

		public static bool Overlap(PolygonShape polygonA, ref Transform2 wTa, PolygonShape polygonB, ref Transform2 wTb)
		{
			if (polygonA.verts.Length == 1 && polygonB.verts.Length == 1)
			{
				return false;
			}
			Transform2 transform = wTa.InvMul(wTb);
			int a = polygonA.verts.Length;
			int b = polygonB.verts.Length;
			Vec2[] a2 = polygonA.verts;
			Vec2[] b2 = vB_buffer;
			for (int i = 0; i < b; i++)
			{
				b2[i] = transform * polygonB.verts[i];
			}
			bool flag = false;
			for (int j = 0; j < 2; j++)
			{
				if (flag)
				{
					break;
				}
				if (a > 1)
				{
					Vector2 vector = a2[a - 1];
					for (int k = 0; k < a; k++)
					{
						Vector2 vector2 = a2[k];
						Vector2 lhs = (vector2 - vector).Rotated90();
						float num = Vector2.Dot(lhs, vector2);
						bool flag2 = true;
						for (int l = 0; l < b; l++)
						{
							if (Vector2.Dot(lhs, b2[l]) < num)
							{
								flag2 = false;
								break;
							}
						}
						if (flag2)
						{
							flag = true;
							break;
						}
						vector = vector2;
					}
				}
				Values.Swap(ref a2, ref b2);
				Values.Swap(ref a, ref b);
			}
			return !flag;
		}

		public static float CalcFeatureDistance(Feature f, ref PolygonCollisionProcess process, out Vector2 normal, out float t)
		{
			float num = 0f;
			normal = Vector2.zero;
			t = 0f;
			switch (f.type)
			{
			case Feature.Type.PointPoint:
			{
				Vector2 vector = process.vB[f.vert1] - process.vA[f.vert0];
				num = vector.magnitude;
				normal = vector / (num + 1E-12f);
				break;
			}
			case Feature.Type.PointEdge:
			{
				Vector2 cir2 = process.vA[f.vert0];
				Vector2 segA2 = process.vB[f.vert1];
				Vector2 segB2 = process.vB[f.vert2];
				float invSegLength2 = process.invLengthsB[f.vert2];
				num = DistRaw_AndNormal_Slow(segA2, segB2, invSegLength2, cir2, out normal, out t);
				normal = -normal;
				break;
			}
			case Feature.Type.EdgePoint:
			{
				Vector2 segA = process.vA[f.vert0];
				Vector2 segB = process.vA[f.vert1];
				Vector2 cir = process.vB[f.vert2];
				float invSegLength = process.invLengthsA[f.vert1];
				num = DistRaw_AndNormal_Slow(segA, segB, invSegLength, cir, out normal, out t);
				break;
			}
			}
			return num - process.radiusA - process.radiusB;
		}

		public static Vector2 FeatureEdgeNormal_NotNormalized(Feature f, ref PolygonCollisionProcess process)
		{
			Vector2 result = Vector2.zero;
			switch (f.type)
			{
			case Feature.Type.PointPoint:
				result = process.vB[f.vert1] - process.vA[f.vert0];
				break;
			case Feature.Type.PointEdge:
			{
				_ = (Vector2)process.vA[f.vert0];
				Vector2 vector3 = process.vB[f.vert1];
				result = -((Vector2)process.vB[f.vert2] - vector3).Rotated90();
				break;
			}
			case Feature.Type.EdgePoint:
			{
				Vector2 vector = process.vA[f.vert0];
				Vector2 vector2 = process.vA[f.vert1];
				_ = (Vector2)process.vB[f.vert2];
				result = (vector2 - vector).Rotated90();
				break;
			}
			}
			return result;
		}

		public static Vector2 FeatureEdgeNormal_YesNormalized(Feature f, ref PolygonCollisionProcess process)
		{
			Vector2 result = Vector2.zero;
			switch (f.type)
			{
			case Feature.Type.PointEdge:
			{
				_ = (Vector2)process.vA[f.vert0];
				Vector2 vector3 = process.vB[f.vert1];
				result = -((Vector2)process.vB[f.vert2] - vector3).Rotated90() * process.invLengthsB[f.vert2];
				break;
			}
			case Feature.Type.EdgePoint:
			{
				Vector2 vector = process.vA[f.vert0];
				Vector2 vector2 = process.vA[f.vert1];
				_ = (Vector2)process.vB[f.vert2];
				result = (vector2 - vector).Rotated90() * process.invLengthsA[f.vert1];
				break;
			}
			}
			return result;
		}

		public static float DistRaw_AndNormal_Slow(Vector2 segA, Vector2 segB, float invSegLength, Vector2 cir, out Vector2 normal, out float t)
		{
			Vector2 vector = segB - segA;
			Vector2 rhs = cir - segA;
			float num = Vector2.Dot(vector, rhs) / vector.sqrMagnitude;
			t = Mathf.Clamp01(num);
			Vector2 vector2 = Vector2.LerpUnclamped(segA, segB, t);
			Vector2 vector3;
			float num2;
			if (t == num)
			{
				vector3 = vector.Rotated90() * invSegLength;
				num2 = Vector2.Dot(vector3, cir - segB);
			}
			else
			{
				num2 = (vector2 - cir).magnitude;
				if (num2 > 1E-06f)
				{
					vector3 = -(vector2 - cir) / num2;
				}
				else
				{
					vector3 = vector.Rotated90() * invSegLength;
					num2 = Vector2.Dot(vector3, cir - segB);
				}
			}
			normal = vector3;
			return num2;
		}

		private static PolygonShape CreatePolygon_FromSegment(float lengthX)
		{
			float num = 0.5f * lengthX;
			polyB_buffer.verts = segmentVerts;
			segmentVerts[0] = new Vec2(0f - num, 0f);
			segmentVerts[1] = new Vec2(num, 0f);
			float num2 = 0.5f / num;
			polyB_buffer.invLengths = segmentInvLengths;
			segmentInvLengths[0] = num2;
			segmentInvLengths[1] = num2;
			polyB_buffer.radius = 0.5f;
			return polyB_buffer;
		}

		internal static PolygonShape CreatePolygon_LOCAL_ONLY(Segment segment)
		{
			polyB_buffer.verts = segmentVerts;
			segmentVerts[0] = new Vec2(0f - segment.halfLengthX, 0f);
			segmentVerts[1] = new Vec2(segment.halfLengthX, 0f);
			float num = 0.5f / segment.halfLengthX;
			polyB_buffer.invLengths = segmentInvLengths;
			segmentInvLengths[0] = num;
			segmentInvLengths[1] = num;
			polyB_buffer.radius = segment.radius;
			return polyB_buffer;
		}

		public static PolygonShape CreatePolygon_LOCAL_ONLY_PolyB_Only(EdgeHandle e, out Transform2 wTe)
		{
			if (e.shapeHandleIndex.isValid)
			{
				ref ShapeHandle reference = ref e.shapeHandleIndex.Get();
				wTe = reference.t2;
				return CreatePolygon_LOCAL_ONLY((Segment)reference.shape);
			}
			Vector2 vector = e.node0.pos;
			Vector2 vector2 = e.node1.pos;
			wTe.position = 0.5f * (vector + vector2);
			Vector2 vector3 = vector2 - vector;
			if (vector3.sqrMagnitude < 1E-12f)
			{
				vector3 = Vector2.right;
			}
			else
			{
				vector3.Normalize();
			}
			wTe.rotation = new Rotation2(vector3);
			return CreatePolygon_FromSegment((vector2 - vector).magnitude);
		}

		public static void CalcClosestPoint(ref PolygonCollisionProcess process, out ClosestPointProcess closestPoint, bool doAveragePointPositions = true)
		{
			if (process.vA.Length == 1 && process.vB_Count == 1)
			{
				CalcClosestPoint_TwoCircles(ref process, out closestPoint);
			}
			else
			{
				CalcClosestPoint_FromEdgeFeature(CalcClosestFeature_Faster(ref process, out var signedDistanceAlongBestEdgeNormal), signedDistanceAlongBestEdgeNormal, in process, out closestPoint, isClosestPoint: true, doAveragePointPositions);
			}
		}

		public static Feature CalcClosestFeature_Faster(ref PolygonCollisionProcess process, out float signedDistanceAlongBestEdgeNormal)
		{
			int num = process.vA.Length;
			int num2 = process.vB_Count;
			Vec2[] array = process.vA;
			Vec2[] array2 = process.vB;
			float[] array3 = process.invLengthsA;
			int num3 = -1;
			int num4 = -1;
			int num5 = -1;
			int num6 = -1;
			float num7 = float.NegativeInfinity;
			float num8 = float.PositiveInfinity;
			int num9 = 0;
			while (true)
			{
				if (num > 1)
				{
					int num10 = num - 1;
					float num11 = array[num10].x;
					float num12 = array[num10].y;
					for (int i = 0; i < num; i++)
					{
						float x = array[i].x;
						float y = array[i].y;
						float num13 = num12 - y;
						float num14 = x - num11;
						int num15 = 0;
						float num16 = num13 * array2[0].x + num14 * array2[0].y;
						float num17 = (0f - num14) * array3[i] * array3[i];
						float num18 = num13 * array3[i] * array3[i];
						float num19 = 0.5f * (num11 + x);
						float num20 = 0.5f * (num12 + y);
						float num21 = num17 * (array2[0].x - num19) + num18 * (array2[0].y - num20);
						for (int j = 1; j < num2; j++)
						{
							float num22 = num13 * array2[j].x + num14 * array2[j].y;
							if (num22 < num16 - 1E-06f * array3[i])
							{
								num16 = num22;
								num15 = j;
								num21 = num17 * (array2[j].x - num19) + num18 * (array2[j].y - num20);
							}
							else
							{
								if (!(num22 < num16 + 1E-06f * array3[i]))
								{
									continue;
								}
								float num23 = num17 * (array2[j].x - num19) + num18 * (array2[j].y - num20);
								if (num23 * num23 < num21 * num21)
								{
									if (num22 < num16)
									{
										num16 = num22;
									}
									num15 = j;
									num21 = num23;
								}
							}
						}
						float num24 = num13 * x + num14 * y;
						float num25 = (num16 - num24) * array3[i];
						if (num7 < num25 - 0.0001f || (num7 < num25 + 0.0001f && num21 * num21 < num8 * num8))
						{
							num7 = num25;
							num3 = i;
							num4 = num9;
							num6 = num15;
							num5 = num10;
							num8 = num21;
						}
						num11 = x;
						num12 = y;
						num10 = i;
					}
				}
				if (num9 == 1)
				{
					break;
				}
				num2 = process.vA.Length;
				num = process.vB_Count;
				array2 = process.vA;
				array = process.vB;
				array3 = process.invLengthsB;
				num9++;
			}
			Feature result = default(Feature);
			result.key = 0u;
			if (num4 == 0)
			{
				result.type = Feature.Type.EdgePoint;
				result.vert0 = (byte)num5;
				result.vert1 = (byte)num3;
				result.vert2 = (byte)num6;
			}
			else
			{
				result.type = Feature.Type.PointEdge;
				result.vert0 = (byte)num6;
				result.vert1 = (byte)num5;
				result.vert2 = (byte)num3;
			}
			signedDistanceAlongBestEdgeNormal = num7;
			return result;
		}

		[Obsolete]
		public static Feature CalcClosestFeature_old_unused(ref PolygonCollisionProcess process, out float signedDistance)
		{
			int num = process.vA.Length;
			int num2 = process.vB_Count;
			Vec2[] array = process.vA;
			Vec2[] array2 = process.vB;
			float[] array3 = process.invLengthsA;
			float[] array4 = process.invLengthsB;
			int num3 = -1;
			int num4 = -1;
			int num5 = -1;
			int num6 = -1;
			float num7 = float.NegativeInfinity;
			float num8 = float.PositiveInfinity;
			bool flag = false;
			for (int i = 0; i < 2; i++)
			{
				if (flag)
				{
					break;
				}
				if (num > 1)
				{
					float num9 = array[num - 1].x;
					float num10 = array[num - 1].y;
					int num11 = num - 1;
					for (int j = 0; j < num; j++)
					{
						float x = array[j].x;
						float y = array[j].y;
						float num12 = array3[j];
						float num13 = (x - num9) * num12;
						float num14 = (y - num10) * num12;
						float num15 = 0f - num14;
						float num16 = num13;
						if (num15 * num15 + num16 * num16 <= 1E-12f)
						{
							num15 = 0f;
							num16 = 1f;
						}
						float num17 = num15 * x + num16 * y;
						int num18 = -1;
						float num19 = float.PositiveInfinity;
						float num20 = float.PositiveInfinity;
						for (int k = 0; k < num2; k++)
						{
							float x2 = array2[k].x;
							float y2 = array2[k].y;
							float num21 = num15 * x2 + num16 * y2;
							if (num21 < num19)
							{
								num19 = num21;
								num18 = k;
								float num22 = (num13 * (x2 - num9) + num14 * (y2 - num10)) * num12;
								num20 = (((double)num22 >= 0.5) ? (num22 - 0.5f) : (0.5f - num22));
							}
							else if (num21 == num19)
							{
								float num23 = (num13 * (x2 - num9) + num14 * (y2 - num10)) * num12;
								float num24 = (((double)num23 >= 0.5) ? (num23 - 0.5f) : (0.5f - num23));
								if (num24 < num20)
								{
									num19 = num21;
									num18 = k;
									num20 = num24;
								}
							}
						}
						float num25 = num19 - num17;
						if (num7 < num25 || (num7 == num25 && num20 < num8))
						{
							num7 = num25;
							num3 = j;
							num4 = i;
							num6 = num18;
							num5 = num11;
							num8 = num20;
						}
						num9 = x;
						num10 = y;
						num11 = j;
					}
				}
				Vec2[] array5 = array;
				array = array2;
				array2 = array5;
				float[] array6 = array3;
				array3 = array4;
				array4 = array6;
				int num26 = num;
				num = num2;
				num2 = num26;
			}
			int num27 = num;
			num = num2;
			num2 = num27;
			Feature result = default(Feature);
			result.key = 0u;
			if (num4 == 0)
			{
				result.type = Feature.Type.EdgePoint;
				result.vert0 = (byte)num5;
				result.vert1 = (byte)num3;
				result.vert2 = (byte)num6;
			}
			else
			{
				result.type = Feature.Type.PointEdge;
				result.vert0 = (byte)num6;
				result.vert1 = (byte)num5;
				result.vert2 = (byte)num3;
			}
			signedDistance = num7;
			return result;
		}

		public static void CalcClosestPoint_FromEdgeFeature(Feature f, float signedDistanceAlongBestEdgeNormal, in PolygonCollisionProcess process, out ClosestPointProcess closestPoint, bool isClosestPoint = false, bool doAveragePointPositions = true)
		{
			Vec2 v;
			Vec2 v2;
			Vec2 v3;
			float num;
			if (f.type == Feature.Type.EdgePoint)
			{
				v = process.vA[f.vert0];
				v2 = process.vA[f.vert1];
				v3 = process.vB[f.vert2];
				num = process.invLengthsA[f.vert1];
			}
			else
			{
				v3 = process.vA[f.vert0];
				v = process.vB[f.vert1];
				v2 = process.vB[f.vert2];
				num = process.invLengthsB[f.vert2];
			}
			Vec2.setSub(in v2, in v, out var v4);
			Vec2.setSub(in v3, in v, out var v5);
			float num2 = Vec2.Dot(in v4, in v5) / (v4.sqrMagnitude + 1E-24f);
			float num3 = num2;
			num3 = ((num3 < 0f) ? 0f : ((1f < num3) ? 1f : num3));
			Vec2 vec = Vec2.LerpUnclamped(in v, in v2, num3);
			float sqrMagnitude = (vec - v3).sqrMagnitude;
			Vec2 vec2;
			float num4;
			if (num3 == num2 || (isClosestPoint && sqrMagnitude < 1E-12f) || (signedDistanceAlongBestEdgeNormal < -0.000101f && 3 < process.vA.Length + process.vB_Count) || sqrMagnitude <= 1E-12f)
			{
				vec2 = v4.rotated90 * num;
				num4 = signedDistanceAlongBestEdgeNormal;
			}
			else
			{
				num4 = Mathf.Sqrt(sqrMagnitude);
				vec2 = -(vec - v3) / num4;
			}
			float tDistMultiplier = 1f;
			if (f.type == Feature.Type.EdgePoint)
			{
				closestPoint.pointInLocalA = vec;
				closestPoint.pointInLocalB = process.vB[f.vert2];
				closestPoint.normalInLocalA = vec2;
			}
			else
			{
				closestPoint.pointInLocalA = process.vA[f.vert0];
				closestPoint.pointInLocalB = vec;
				closestPoint.normalInLocalA = -vec2;
			}
			closestPoint.distance = num4;
			closestPoint.distance -= process.radiusA + process.radiusB;
			closestPoint.pointInLocalA += closestPoint.normalInLocalA * process.radiusA;
			closestPoint.pointInLocalB -= closestPoint.normalInLocalA * process.radiusB;
			if (closestPoint.distance < -0f && doAveragePointPositions)
			{
				if ((process.vA.Length == 1) ^ (process.vB_Count == 1))
				{
					if (process.vA.Length == 1)
					{
						closestPoint.pointInLocalB = closestPoint.pointInLocalA;
					}
					else
					{
						closestPoint.pointInLocalA = closestPoint.pointInLocalB;
					}
				}
				else
				{
					closestPoint.pointInLocalB = (closestPoint.pointInLocalA = 0.5f * (closestPoint.pointInLocalA + closestPoint.pointInLocalB));
				}
			}
			closestPoint.pointInLocalB = process.aTb.InvMul(closestPoint.pointInLocalB);
			closestPoint.feature = f;
			closestPoint.tOnEdge = num3;
			closestPoint.tEdgeInvLen = num;
			closestPoint.tDistMultiplier = tDistMultiplier;
		}

		public static void CalcClosestPoint_TwoCircles(ref PolygonCollisionProcess process, out ClosestPointProcess closestPoint)
		{
			Feature feature = default(Feature);
			feature.key = 0u;
			feature.type = Feature.Type.PointPoint;
			feature.vert0 = 0;
			feature.vert1 = 0;
			feature.vert2 = 0;
			Vec2 vec = process.vB[0] - process.vA[0];
			float magnitude = vec.magnitude;
			closestPoint.normalInLocalA = vec / (magnitude + 1E-12f);
			closestPoint.pointInLocalA = process.vA[0] + closestPoint.normalInLocalA * process.radiusA;
			closestPoint.pointInLocalB = process.aTb.InvMul(process.vB[0] - closestPoint.normalInLocalA * process.radiusB);
			closestPoint.distance = magnitude - process.radiusA - process.radiusB;
			closestPoint.feature = feature;
			closestPoint.tOnEdge = 0f;
			closestPoint.tEdgeInvLen = 0f;
			closestPoint.tDistMultiplier = 0f;
		}
	}
}
