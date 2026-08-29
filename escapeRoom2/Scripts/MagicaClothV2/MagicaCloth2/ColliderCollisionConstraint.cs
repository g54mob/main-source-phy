using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class ColliderCollisionConstraint : IDisposable
	{
		public enum Mode
		{
			None = 0,
			Point = 1,
			Edge = 2
		}

		[Serializable]
		public class SerializeData : IDataValidate, ITransform
		{
			public Mode mode;

			[Range(0f, 0.5f)]
			public float friction;

			public List<ColliderComponent> colliderList = new List<ColliderComponent>();

			public List<Transform> collisionBones = new List<Transform>();

			public CurveSerializeData limitDistance = new CurveSerializeData(0.05f);

			public int ColliderLength => colliderList.Count;

			public SerializeData()
			{
				mode = Mode.Point;
				friction = 0.05f;
			}

			public void DataValidate()
			{
				friction = Mathf.Clamp(friction, 0f, 0.5f);
				limitDistance.DataValidate(0f, 1f);
			}

			public SerializeData Clone()
			{
				return new SerializeData
				{
					mode = mode,
					friction = friction,
					colliderList = new List<ColliderComponent>(colliderList),
					collisionBones = new List<Transform>(collisionBones)
				};
			}

			public override int GetHashCode()
			{
				int num = 0;
				foreach (Transform collisionBone in collisionBones)
				{
					if ((bool)collisionBone)
					{
						num += collisionBone.GetInstanceID();
					}
				}
				return num;
			}

			public void GetUsedTransform(HashSet<Transform> transformSet)
			{
				foreach (Transform collisionBone in collisionBones)
				{
					if ((bool)collisionBone)
					{
						transformSet.Add(collisionBone);
					}
				}
			}

			public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
			{
				for (int i = 0; i < collisionBones.Count; i++)
				{
					Transform transform = collisionBones[i];
					if ((bool)transform && replaceDict.ContainsKey(transform.GetInstanceID()))
					{
						collisionBones[i] = replaceDict[transform.GetInstanceID()];
					}
				}
			}
		}

		public struct ColliderCollisionConstraintParams
		{
			public Mode mode;

			public float dynamicFriction;

			public float staticFriction;

			public float4x4 limitDistance;

			public void Convert(SerializeData sdata, ClothProcess.ClothType clothType)
			{
				switch (clothType)
				{
				case ClothProcess.ClothType.MeshCloth:
				case ClothProcess.ClothType.BoneCloth:
					mode = sdata.mode;
					dynamicFriction = sdata.friction * 1f;
					staticFriction = sdata.friction * 1f;
					break;
				case ClothProcess.ClothType.BoneSpring:
					mode = Mode.Point;
					limitDistance = sdata.limitDistance.ConvertFloatArray();
					dynamicFriction = 0.5f;
					staticFriction = 0.5f;
					break;
				}
			}
		}

		public void Dispose()
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[ColliderCollisionConstraint]");
			return stringBuilder.ToString();
		}

		internal static void SolverPointConstraint(DataChunk chunk, ref TeamManager.TeamData tdata, ref ClothParameters param, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float> vertexDepths, ref NativeArray<float3> nextPosArray, ref NativeArray<float> frictionArray, ref NativeArray<float3> collisionNormalArray, ref NativeArray<float3> velocityPosArray, ref NativeArray<float3> basePosArray, ref NativeArray<ExBitFlag8> colliderFlagArray, ref NativeArray<ColliderManager.WorkData> colliderWorkDataArray)
		{
			if (tdata.ColliderCount == 0 || param.colliderCollisionConstraint.mode != Mode.Point || !chunk.IsValid)
			{
				return;
			}
			bool isSpring = tdata.IsSpring;
			int num = tdata.particleChunk.startIndex + chunk.startIndex;
			int num2 = tdata.proxyCommonChunk.startIndex + chunk.startIndex;
			int num3 = 0;
			while (num3 < chunk.dataLength)
			{
				float3 float5 = nextPosArray[num];
				VertexAttribute vertexAttribute = attributes[num2];
				if (!vertexAttribute.IsInvalid() && !vertexAttribute.IsDisableCollision() && (vertexAttribute.IsMove() || tdata.IsSpring))
				{
					float time = vertexDepths[num2];
					float3 basePos = (isSpring ? basePosArray[num] : float3.zero);
					float num4 = math.max(param.radiusCurveData.MC2EvaluateCurve(time), 0.0001f);
					num4 *= tdata.scaleRatio;
					float num5 = float.MaxValue;
					int num6 = -1;
					float3 float6 = 0;
					float3 normal = 0;
					float3 float7 = 0;
					int num7 = 0;
					float3 x = 0;
					float num8 = num4 * 1f;
					AABB aabb = new AABB(float5 - num4, float5 + num4);
					aabb.Expand(num8);
					float maxLength = (isSpring ? (math.max(param.colliderCollisionConstraint.limitDistance.MC2EvaluateCurve(time), 0.0001f) * tdata.scaleRatio) : (-1f));
					int num9 = tdata.colliderChunk.startIndex;
					int colliderCount = tdata.colliderCount;
					int num10 = 0;
					while (num10 < colliderCount)
					{
						ExBitFlag8 flag = colliderFlagArray[num9];
						if (flag.IsSet(16) && flag.IsSet(32))
						{
							ColliderManager.ColliderType colliderType = DataUtility.GetColliderType(in flag);
							ColliderManager.WorkData cwork = colliderWorkDataArray[num9];
							float num11 = 100f;
							float3 nextpos = float5;
							switch (colliderType)
							{
							case ColliderManager.ColliderType.Sphere:
								num11 = PointSphereColliderDetection(ref nextpos, in basePos, num4, in aabb, in cwork, isSpring, maxLength, out normal);
								break;
							case ColliderManager.ColliderType.CapsuleX_Center:
							case ColliderManager.ColliderType.CapsuleY_Center:
							case ColliderManager.ColliderType.CapsuleZ_Center:
							case ColliderManager.ColliderType.CapsuleX_Start:
							case ColliderManager.ColliderType.CapsuleY_Start:
							case ColliderManager.ColliderType.CapsuleZ_Start:
								num11 = PointCapsuleColliderDetection(ref nextpos, num4, in aabb, in cwork, out normal);
								break;
							case ColliderManager.ColliderType.Plane:
								num11 = PointPlaneColliderDetction(ref nextpos, num4, in cwork, out normal);
								break;
							default:
								Debug.LogError($"unknown collider type:{colliderType}");
								break;
							}
							if (num11 <= 0f)
							{
								float7 += nextpos - float5;
								x += normal;
								num7++;
							}
							if (num11 <= num8)
							{
								num6 = num9;
								float6 += normal;
								num5 = math.min(num5, num11);
							}
						}
						num10++;
						num9++;
					}
					if (num7 > 0)
					{
						x /= (float)num7;
						float num12 = math.length(x);
						if (num12 < 1E-08f)
						{
							float7 = 0;
						}
						else
						{
							float num13 = math.min(num12, 1f);
							float7 /= (float)num7;
							float5 += float7 * num13;
						}
					}
					if (num6 >= 0 && num8 > 0f && math.lengthsq(float6) > 1E-06f)
					{
						float x2 = 1f - math.saturate(num5 / num8);
						frictionArray[num] = math.max(x2, frictionArray[num]);
						float6 = math.normalize(float6);
					}
					collisionNormalArray[num] = float6;
					nextPosArray[num] = float5;
					if (isSpring && num7 > 0)
					{
						velocityPosArray[num] += float7;
					}
				}
				num3++;
				num++;
				num2++;
			}
		}

		private static float PointSphereColliderDetection(ref float3 nextpos, in float3 basePos, float radius, in AABB aabb, in ColliderManager.WorkData cwork, bool isSpring, float maxLength, out float3 normal)
		{
			normal = 0;
			if (!aabb.Overlaps(in cwork.aabb))
			{
				return float.MaxValue;
			}
			float3 end = nextpos;
			float3 c = cwork.oldPos.c0;
			float3 c2 = cwork.nextPos.c0;
			float x = cwork.radius.x;
			float3 planeDir = math.normalize(nextpos - c);
			float3 planePos = c2 + planeDir * (x + radius);
			normal = planeDir;
			float num = MathUtility.IntersectPointPlaneDist(in planePos, in planeDir, in nextpos, out nextpos);
			if (maxLength > 0f)
			{
				nextpos = MathUtility.ClampDistance(basePos, nextpos, maxLength);
				float t = math.saturate(math.distance(basePos, nextpos) / radius);
				t = math.lerp(0f, 0.85f, t);
				nextpos = math.lerp(nextpos, end, t);
				num *= 3f;
			}
			return num;
		}

		private static float PointPlaneColliderDetction(ref float3 nextpos, float radius, in ColliderManager.WorkData cwork, out float3 normal)
		{
			float3 c = cwork.nextPos.c0;
			float3 planeDir = (normal = cwork.oldPos.c0);
			return MathUtility.IntersectPointPlaneDist(c + planeDir * radius, in planeDir, in nextpos, out nextpos);
		}

		private static float PointCapsuleColliderDetection(ref float3 nextpos, float radius, in AABB aabb, in ColliderManager.WorkData cwork, out float3 normal)
		{
			normal = 0;
			if (!aabb.Overlaps(in cwork.aabb))
			{
				return float.MaxValue;
			}
			float3 a = cwork.oldPos.c0;
			float3 b = cwork.oldPos.c1;
			float3 c = cwork.nextPos.c0;
			float3 c2 = cwork.nextPos.c1;
			float x = cwork.radius.x;
			float y = cwork.radius.y;
			float t = MathUtility.ClosestPtPointSegmentRatio(in nextpos, in a, in b);
			float num = math.lerp(x, y, t);
			float3 float5 = math.lerp(a, b, t);
			float3 v = math.mul(v: nextpos - float5, q: cwork.inverseOldRot);
			float5 = math.lerp(c, c2, t);
			float3 x2 = math.mul(cwork.rot, v);
			float3 planeDir = math.normalize(x2);
			float3 planePos = float5 + planeDir * (num + radius);
			normal = planeDir;
			return MathUtility.IntersectPointPlaneDist(in planePos, in planeDir, in nextpos, out nextpos);
		}

		internal unsafe static void SolverEdgeConstraint(DataChunk chunk, ref TeamManager.TeamData tdata, ref ClothParameters param, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float> vertexDepths, ref NativeArray<int2> edges, ref NativeArray<float3> nextPosArray, ref NativeArray<ExBitFlag8> colliderFlagArray, ref NativeArray<ColliderManager.WorkData> colliderWorkDataArray, ref NativeArray<float3> tempVectorBufferA, ref NativeArray<float3> tempVectorBufferB, ref NativeArray<int> tempCountBuffer, ref NativeArray<float> tempFloatBufferA)
		{
			if (tdata.ColliderCount == 0 || param.colliderCollisionConstraint.mode != Mode.Edge || !chunk.IsValid)
			{
				return;
			}
			int* unsafePtr = (int*)tempVectorBufferA.GetUnsafePtr();
			int* unsafePtr2 = (int*)tempVectorBufferB.GetUnsafePtr();
			int* unsafePtr3 = (int*)tempCountBuffer.GetUnsafePtr();
			int* unsafePtr4 = (int*)tempFloatBufferA.GetUnsafePtr();
			int startIndex = tdata.proxyCommonChunk.startIndex;
			int num = tdata.proxyEdgeChunk.startIndex + chunk.startIndex;
			int num2 = 0;
			while (num2 < chunk.dataLength)
			{
				int2 int5 = edges[num];
				int2 int6 = int5 + startIndex;
				VertexAttribute vertexAttribute = attributes[int6.x];
				VertexAttribute vertexAttribute2 = attributes[int6.y];
				if (vertexAttribute.IsMove() || vertexAttribute2.IsMove())
				{
					int startIndex2 = tdata.particleChunk.startIndex;
					int2 int7 = int5 + startIndex2;
					float3x2 float3x5 = new float3x2(nextPosArray[int7.x], nextPosArray[int7.y]);
					float2 float5 = new float2(vertexDepths[int6.x], vertexDepths[int6.y]);
					float2 radiusE = new float2(param.radiusCurveData.MC2EvaluateCurve(float5.x), param.radiusCurveData.MC2EvaluateCurve(float5.y));
					radiusE *= tdata.scaleRatio;
					float num3 = (radiusE.x + radiusE.y) * 0.5f * 1f;
					float num4 = float.MaxValue;
					int num5 = -1;
					float3 x = 0;
					float3 normal = 0;
					AABB aabbE = new AABB(float3x5.c0 - radiusE.x, float3x5.c0 + radiusE.x);
					aabbE.Encapsulate(new AABB(float3x5.c1 - radiusE.y, float3x5.c1 + radiusE.y));
					aabbE.Expand(num3);
					float3x2 float3x6 = 0;
					int num6 = 0;
					float3 x2 = 0;
					int num7 = tdata.colliderChunk.startIndex;
					int colliderCount = tdata.colliderCount;
					int num8 = 0;
					while (num8 < colliderCount)
					{
						ExBitFlag8 flag = colliderFlagArray[num7];
						if (flag.IsSet(16) && flag.IsSet(32))
						{
							ColliderManager.ColliderType colliderType = DataUtility.GetColliderType(in flag);
							ColliderManager.WorkData cwork = colliderWorkDataArray[num7];
							float num9 = 100f;
							float3x2 nextPosE = float3x5;
							switch (colliderType)
							{
							case ColliderManager.ColliderType.Sphere:
								num9 = EdgeSphereColliderDetection(ref nextPosE, in radiusE, in aabbE, num3, in cwork, out normal);
								break;
							case ColliderManager.ColliderType.CapsuleX_Center:
							case ColliderManager.ColliderType.CapsuleY_Center:
							case ColliderManager.ColliderType.CapsuleZ_Center:
							case ColliderManager.ColliderType.CapsuleX_Start:
							case ColliderManager.ColliderType.CapsuleY_Start:
							case ColliderManager.ColliderType.CapsuleZ_Start:
								num9 = EdgeCapsuleColliderDetection(ref nextPosE, in radiusE, in aabbE, num3, in cwork, out normal);
								break;
							case ColliderManager.ColliderType.Plane:
								num9 = EdgePlaneColliderDetection(ref nextPosE, in radiusE, in cwork, out normal);
								break;
							default:
								Debug.LogError($"Unknown collider type:{colliderType}");
								break;
							}
							if (num9 <= 0f)
							{
								float3x6 += nextPosE - float3x5;
								x2 += normal;
								num6++;
							}
							if (num9 <= num3)
							{
								num5 = num7;
								x += normal;
								num4 = math.min(num4, num9);
							}
						}
						num8++;
						num7++;
					}
					if (num6 > 0)
					{
						x2 /= (float)num6;
						float num10 = math.length(x2);
						if (num10 > 1E-08f)
						{
							float num11 = math.min(num10, 1f);
							float3x6 /= (float)num6;
							float3x6 *= num11;
							InterlockUtility.AddFloat3(int7.x, float3x6.c0, unsafePtr3, unsafePtr);
							InterlockUtility.AddFloat3(int7.y, float3x6.c1, unsafePtr3, unsafePtr);
						}
					}
					if (num5 >= 0 && num3 > 0f && math.lengthsq(x) > 1E-06f)
					{
						float value = 1f - math.saturate(num4 / num3);
						InterlockUtility.Max(int7.x, value, unsafePtr4);
						InterlockUtility.Max(int7.y, value, unsafePtr4);
						x = math.normalize(x);
						InterlockUtility.AddFloat3(int7.x, x, unsafePtr2);
						InterlockUtility.AddFloat3(int7.y, x, unsafePtr2);
					}
				}
				num2++;
				num++;
			}
		}

		internal unsafe static void SumEdgeConstraint(DataChunk chunk, ref TeamManager.TeamData tdata, ref ClothParameters param, ref NativeArray<float3> nextPosArray, ref NativeArray<float> frictionArray, ref NativeArray<float3> collisionNormalArray, ref NativeArray<float3> tempVectorBufferA, ref NativeArray<float3> tempVectorBufferB, ref NativeArray<int> tempCountBuffer, ref NativeArray<float> tempFloatBufferA)
		{
			if (tdata.ColliderCount == 0 || param.colliderCollisionConstraint.mode != Mode.Edge || !chunk.IsValid)
			{
				return;
			}
			int* unsafePtr = (int*)tempVectorBufferA.GetUnsafePtr();
			int* unsafePtr2 = (int*)tempVectorBufferB.GetUnsafePtr();
			int* unsafePtr3 = (int*)tempCountBuffer.GetUnsafePtr();
			int* unsafePtr4 = (int*)tempFloatBufferA.GetUnsafePtr();
			int num = tdata.particleChunk.startIndex + chunk.startIndex;
			int num2 = tdata.proxyCommonChunk.startIndex + chunk.startIndex;
			int num3 = 0;
			while (num3 < chunk.dataLength)
			{
				if (tempCountBuffer[num] > 0)
				{
					float3 float5 = InterlockUtility.ReadAverageFloat3(num, unsafePtr3, unsafePtr);
					nextPosArray[num] += float5;
				}
				float num4 = InterlockUtility.ReadFloat(num, unsafePtr4);
				if (num4 > 0f && num4 > frictionArray[num])
				{
					frictionArray[num] = num4;
				}
				float3 x = InterlockUtility.ReadFloat3(num, unsafePtr2);
				if (math.lengthsq(x) > 0f)
				{
					x = math.normalize(x);
					collisionNormalArray[num] = x;
				}
				tempVectorBufferA[num] = 0;
				tempVectorBufferB[num] = 0;
				tempCountBuffer[num] = 0;
				tempFloatBufferA[num] = 0f;
				num3++;
				num++;
				num2++;
			}
		}

		private static float EdgeSphereColliderDetection(ref float3x2 nextPosE, in float2 radiusE, in AABB aabbE, float cfr, in ColliderManager.WorkData cwork, out float3 normal)
		{
			normal = 0;
			if (!aabbE.Overlaps(in cwork.aabb))
			{
				return float.MaxValue;
			}
			float3 c = cwork.oldPos.c0;
			float3 c2 = cwork.nextPos.c0;
			float x = cwork.radius.x;
			float num = MathUtility.ClosestPtPointSegmentRatio(in c, in nextPosE.c0, in nextPosE.c1);
			float3 float5 = math.lerp(nextPosE.c0, nextPosE.c1, num);
			float3 float6 = float5 - c;
			float num2 = math.length(float6);
			if (num2 < 1E-09f)
			{
				return float.MaxValue;
			}
			float3 float7 = (normal = float6 / num2);
			float3 y = c2 - c;
			float num3 = math.dot(float7, y);
			float num4 = num2 - num3;
			float num5 = math.lerp(radiusE.x, radiusE.y, num);
			float num6 = x;
			float num7 = num5 + num6;
			if (num4 > num7 + cfr)
			{
				return float.MaxValue;
			}
			float6 = float5 - c2;
			num4 = math.dot(float7, float6);
			if (num4 > num7)
			{
				return num4 - num7;
			}
			float num8 = num7 - num4;
			float2 float8 = new float2(1f - num, num);
			float3x2 float3x5 = new float3x2(float7 * float8.x, float7 * float8.y);
			float num9 = math.dot(float8, float8);
			if (num9 == 0f)
			{
				return float.MaxValue;
			}
			num9 = num8 / num9;
			float3x2 float3x6 = float3x5 * num9;
			nextPosE += float3x6;
			return 0f - num8;
		}

		private static float EdgeCapsuleColliderDetection(ref float3x2 nextPosE, in float2 radiusE, in AABB aabbE, float cfr, in ColliderManager.WorkData cwork, out float3 normal)
		{
			normal = 0;
			if (!aabbE.Overlaps(in cwork.aabb))
			{
				return float.MaxValue;
			}
			float3 p = cwork.oldPos.c0;
			float3 q = cwork.oldPos.c1;
			float3 c = cwork.nextPos.c0;
			float3 c2 = cwork.nextPos.c1;
			float x = cwork.radius.x;
			float y = cwork.radius.y;
			float s;
			float t;
			float3 c3;
			float3 c4;
			float num = math.sqrt(MathUtility.ClosestPtSegmentSegment(in nextPosE.c0, in nextPosE.c1, in p, in q, out s, out t, out c3, out c4));
			if (num < 1E-09f)
			{
				return float.MaxValue;
			}
			float3 float5 = c3 - c4;
			float3 float6 = (normal = float5 / num);
			if (x != y)
			{
				float3 p2 = p + float6 * x;
				float3 q2 = q + float6 * y;
				MathUtility.ClosestPtSegmentSegment2(in nextPosE.c0, in nextPosE.c1, in p2, in q2, out s, out t);
				c3 = math.lerp(nextPosE.c0, nextPosE.c1, s);
				c4 = math.lerp(p, q, t);
				float5 = c3 - c4;
				num = math.length(float5);
				float6 = (normal = float5 / num);
			}
			float3 start = c - p;
			float3 end = c2 - q;
			float3 y2 = math.lerp(start, end, t);
			float num2 = math.dot(float6, y2);
			float num3 = num - num2;
			float num4 = math.lerp(radiusE.x, radiusE.y, s);
			float num5 = math.lerp(x, y, t);
			float num6 = num4 + num5;
			if (num3 > num6 + cfr)
			{
				return float.MaxValue;
			}
			float3 float7 = math.lerp(c, c2, t);
			float5 = c3 - float7;
			num3 = math.dot(float6, float5);
			if (num3 > num6)
			{
				return num3 - num6;
			}
			float num7 = num6 - num3;
			float2 float8 = new float2(1f - s, s);
			float3x2 float3x5 = new float3x2(float6 * float8.x, float6 * float8.y);
			float num8 = math.dot(float8, float8);
			if (num8 == 0f)
			{
				return float.MaxValue;
			}
			num8 = num7 / num8;
			float3x2 float3x6 = float3x5 * num8;
			nextPosE += float3x6;
			return 0f - num7;
		}

		private static float EdgePlaneColliderDetection(ref float3x2 nextPosE, in float2 radiusE, in ColliderManager.WorkData cwork, out float3 normal)
		{
			float3 c = cwork.nextPos.c0;
			float3 planeDir = (normal = cwork.oldPos.c0);
			float x = MathUtility.IntersectPointPlaneDist(c + planeDir * radiusE.x, in planeDir, in nextPosE.c0, out nextPosE.c0);
			float y = MathUtility.IntersectPointPlaneDist(c + planeDir * radiusE.y, in planeDir, in nextPosE.c1, out nextPosE.c1);
			return math.min(x, y);
		}
	}
}
