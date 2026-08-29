using System;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class SelfCollisionConstraint : IDisposable
	{
		public enum SelfCollisionMode
		{
			None = 0,
			FullMesh = 2
		}

		[Serializable]
		public class SerializeData : IDataValidate
		{
			public SelfCollisionMode selfMode;

			public CurveSerializeData surfaceThickness = new CurveSerializeData(0.005f, 0.5f, 1f, useCurve: false);

			public SelfCollisionMode syncMode;

			public MagicaCloth syncPartner;

			[Range(0f, 1f)]
			public float clothMass;

			public SerializeData()
			{
				selfMode = SelfCollisionMode.None;
				syncMode = SelfCollisionMode.None;
			}

			public void DataValidate()
			{
				surfaceThickness.DataValidate(0.001f, 0.05f);
				clothMass = Mathf.Clamp01(clothMass);
			}

			public SerializeData Clone()
			{
				return new SerializeData
				{
					selfMode = selfMode,
					surfaceThickness = surfaceThickness.Clone(),
					syncMode = syncMode,
					syncPartner = syncPartner,
					clothMass = clothMass
				};
			}

			public MagicaCloth GetSyncPartner()
			{
				if (syncMode == SelfCollisionMode.None)
				{
					return null;
				}
				return syncPartner;
			}
		}

		public struct SelfCollisionConstraintParams
		{
			public SelfCollisionMode selfMode;

			public float4x4 surfaceThicknessCurveData;

			public SelfCollisionMode syncMode;

			public float clothMass;

			public void Convert(SerializeData sdata, ClothProcess.ClothType clothType)
			{
				selfMode = ((clothType != ClothProcess.ClothType.BoneSpring) ? sdata.selfMode : SelfCollisionMode.None);
				surfaceThicknessCurveData = sdata.surfaceThickness.ConvertFloatArray();
				syncMode = ((clothType != ClothProcess.ClothType.BoneSpring) ? sdata.syncMode : SelfCollisionMode.None);
				clothMass = sdata.clothMass;
			}
		}

		internal struct Primitive : IComparable<Primitive>
		{
			public uint flag;

			public int3 particleIndices;

			public float3 invMass;

			public AABB aabb;

			public int3 grid;

			public float depth;

			public float thickness;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool IsIgnore()
			{
				return (flag & 0x40000000) != 0;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool IsAllFix()
			{
				return (flag & 0x20000000) != 0;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool AnyParticle(ref Primitive pri)
			{
				uint num = ((flag & 0x3000000) >> 24) + 1;
				for (int i = 0; i < num; i++)
				{
					int num2 = particleIndices[i];
					if (!math.all(pri.particleIndices - num2))
					{
						return true;
					}
				}
				return false;
			}

			public int CompareTo(Primitive other)
			{
				if (grid.x != other.grid.x)
				{
					return grid.x - other.grid.x;
				}
				if (grid.y != other.grid.y)
				{
					return grid.y - other.grid.y;
				}
				return grid.z - other.grid.z;
			}
		}

		internal struct GridInfo : IComparable<GridInfo>
		{
			public int hash;

			public int start;

			public int count;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int CompareTo(GridInfo other)
			{
				if (hash < other.hash)
				{
					return -1;
				}
				if (hash > other.hash)
				{
					return 1;
				}
				return 0;
			}
		}

		internal struct ContactInfo
		{
			public int primitiveIndex0;

			public int primitiveIndex1;

			public byte contactType;

			public byte enable;

			public half thickness;

			public half s;

			public half t;

			public half3 n;
		}

		internal struct IntersectInfo
		{
			public int2 edgeParticeIndices;

			public int3 triangleParticleIndices;
		}

		[BurstCompile]
		private struct InitPrimitiveJob : IJobParallelFor
		{
			public int teamId;

			public TeamManager.TeamData tdata;

			public uint kind;

			public int startPrimitive;

			[ReadOnly]
			public NativeArray<int2> edges;

			[ReadOnly]
			public NativeArray<int3> triangles;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> vertexDepths;

			[NativeDisableParallelForRestriction]
			public NativeArray<Primitive> primitiveArrayB;

			public void Execute(int index)
			{
				int index2 = startPrimitive + index;
				Primitive value = primitiveArrayB[index2];
				int startIndex = tdata.particleChunk.startIndex;
				int3 particleIndices = -1;
				if (kind == 0)
				{
					particleIndices[0] = startIndex + index;
				}
				else if (kind == 1)
				{
					int startIndex2 = tdata.proxyEdgeChunk.startIndex;
					particleIndices.xy = edges[startIndex2 + index] + startIndex;
				}
				else if (kind == 2)
				{
					int startIndex3 = tdata.proxyTriangleChunk.startIndex;
					particleIndices.xyz = triangles[startIndex3 + index] + startIndex;
				}
				uint num = 0u;
				uint num2 = 67108864u;
				bool flag = false;
				int num3 = 0;
				float num4 = 0f;
				int num5 = (int)(kind + 1);
				for (int i = 0; i < num5; i++)
				{
					int num6 = particleIndices[i];
					int index3 = tdata.proxyCommonChunk.startIndex + num6 - startIndex;
					VertexAttribute vertexAttribute = attributes[index3];
					if (vertexAttribute.IsMove())
					{
						num &= ~num2;
					}
					else
					{
						num |= num2;
						num3++;
					}
					num2 <<= 1;
					if (vertexAttribute.IsInvalid())
					{
						flag = true;
					}
					num4 += vertexDepths[index3];
				}
				num = ((num3 != num5) ? (num & 0xDFFFFFFFu) : (num | 0x20000000));
				if (flag)
				{
					num |= 0x40000000;
				}
				num4 /= (float)num5;
				value.flag = (kind << 24) | num;
				value.particleIndices = particleIndices;
				value.depth = num4;
				value.grid = 1000000;
				primitiveArrayB[index2] = value;
			}
		}

		[BurstCompile]
		internal struct SelfStep_UpdatePrimitiveJob : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			public float4 simulationPower;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			[ReadOnly]
			public NativeArray<float> frictionArray;

			[NativeDisableParallelForRestriction]
			public NativeArray<Primitive> primitiveArrayB;

			[ReadOnly]
			public NativeArray<byte> intersectFlagArray;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int k = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref ClothParameters param = ref unsafeReadOnlyPtr2[num];
				if (updateIndex < reference.updateCount && reference.IsProcess && reference.ParticleCount != 0)
				{
					UpdatePrimitive(k, num, ref reference, ref param, ref nextPosArray, ref oldPosArray, ref frictionArray, ref primitiveArrayB, ref intersectFlagArray);
				}
			}
		}

		[BurstCompile]
		internal struct SelfStep_UpdateGridJob : IJobParallelFor
		{
			public int kindCount;

			public int updateIndex;

			public float4 simulationPower;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<Primitive> primitiveArrayB;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<GridInfo> uniformGridStartCountBuffer;

			public unsafe void Execute(int index)
			{
				int index2 = index / kindCount;
				int k = index % kindCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				if (updateIndex < reference.updateCount && reference.IsProcess && reference.ParticleCount != 0 && updateIndex == 0)
				{
					UpdateGrid(k, num, ref reference, ref primitiveArrayB, ref uniformGridStartCountBuffer);
				}
			}
		}

		[BurstCompile]
		internal struct SelfStep_DetectionContactJob : IJobParallelFor
		{
			public int updateIndex;

			public int workerCount;

			public int teamCount;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			[ReadOnly]
			public NativeArray<Primitive> primitiveArrayB;

			[ReadOnly]
			public NativeArray<GridInfo> uniformGridStartCountBuffer;

			[NativeDisableParallelForRestriction]
			public NativeQueue<ContactInfo>.ParallelWriter contactQueue;

			public unsafe void Execute(int index)
			{
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				int index2 = index / (6 * workerCount);
				index %= 6 * workerCount;
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				if (updateIndex >= reference.updateCount || !reference.IsProcess || reference.ParticleCount == 0)
				{
					return;
				}
				int workerIndex = index / 6;
				index %= 6;
				int num2 = index / 3;
				index %= 3;
				int num3 = index;
				uint myKind = 0u;
				uint targetKind = 0u;
				switch (num3)
				{
				case 0:
					myKind = 1u;
					targetKind = 1u;
					break;
				case 1:
					myKind = 0u;
					targetKind = 2u;
					break;
				case 2:
					myKind = 2u;
					targetKind = 0u;
					break;
				}
				switch (num2)
				{
				case 0:
					if (num3 != 2)
					{
						int pos2 = 35 + num3 * 3;
						if (reference.flag.IsSet(pos2))
						{
							DetectionContacts(workerCount, workerIndex, num, ref reference, myKind, num, ref reference, targetKind, ref nextPosArray, ref oldPosArray, ref primitiveArrayB, ref uniformGridStartCountBuffer, ref contactQueue);
						}
					}
					break;
				case 1:
					if (reference.syncTeamId > 0)
					{
						ref TeamManager.TeamData targetTeam = ref unsafeReadOnlyPtr[reference.syncTeamId];
						int pos = 36 + num3 * 3;
						if (reference.flag.IsSet(pos))
						{
							DetectionContacts(workerCount, workerIndex, num, ref reference, myKind, reference.syncTeamId, ref targetTeam, targetKind, ref nextPosArray, ref oldPosArray, ref primitiveArrayB, ref uniformGridStartCountBuffer, ref contactQueue);
						}
					}
					break;
				}
			}
		}

		[BurstCompile]
		internal struct SelfStep_ConvertContactListJob : IJob
		{
			[ReadOnly]
			public NativeQueue<ContactInfo> contactQueue;

			[NativeDisableParallelForRestriction]
			public NativeList<ContactInfo> contactList;

			public void Execute()
			{
				contactList.Clear();
				if (contactQueue.Count > 0)
				{
					contactList.AddRange(contactQueue.ToArray(Allocator.Temp));
				}
			}
		}

		[BurstCompile]
		internal struct SelfStep_UpdateContactJob : IJobParallelForDefer
		{
			public bool first;

			[NativeDisableParallelForRestriction]
			public NativeList<ContactInfo> contactList;

			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			[ReadOnly]
			public NativeArray<Primitive> primitiveArrayB;

			public unsafe void Execute(int index)
			{
				Primitive* unsafeReadOnlyPtr = (Primitive*)primitiveArrayB.GetUnsafeReadOnlyPtr();
				UpdateContactInfo(ref contactList.GetUnsafePtr()[index], unsafeReadOnlyPtr, ref nextPosArray, ref oldPosArray, 2f, first);
			}
		}

		[BurstCompile]
		internal struct SelfStep_SolverContactJob : IJobParallelForDefer
		{
			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeArray<Primitive> primitiveArrayB;

			[ReadOnly]
			public NativeList<ContactInfo> contactList;

			[NativeDisableParallelForRestriction]
			public NativeArray<float3> tempVectorBufferA;

			[NativeDisableParallelForRestriction]
			public NativeArray<int> tempCountBuffer;

			public unsafe void Execute(int index)
			{
				ContactInfo* unsafeReadOnlyPtr = contactList.GetUnsafeReadOnlyPtr();
				Primitive* unsafeReadOnlyPtr2 = (Primitive*)primitiveArrayB.GetUnsafeReadOnlyPtr();
				int* unsafePtr = (int*)tempCountBuffer.GetUnsafePtr();
				int* unsafePtr2 = (int*)tempVectorBufferA.GetUnsafePtr();
				ref ContactInfo reference = ref unsafeReadOnlyPtr[index];
				if (reference.enable == 0)
				{
					return;
				}
				ref Primitive reference2 = ref unsafeReadOnlyPtr2[reference.primitiveIndex0];
				ref Primitive reference3 = ref unsafeReadOnlyPtr2[reference.primitiveIndex1];
				float num = reference.thickness;
				if (reference.contactType == 0)
				{
					float3 start = nextPosArray[reference2.particleIndices.x];
					float3 end = nextPosArray[reference2.particleIndices.y];
					float3 start2 = nextPosArray[reference3.particleIndices.x];
					float3 end2 = nextPosArray[reference3.particleIndices.y];
					float num2 = reference.s;
					float num3 = reference.t;
					float3 float5 = reference.n;
					float3 obj = math.lerp(start, end, num2);
					float3 float6 = math.lerp(start2, end2, num3);
					float3 y = obj - float6;
					float num4 = math.dot(float5, y);
					if (num4 > num)
					{
						return;
					}
					float x = reference2.invMass.x;
					float y2 = reference2.invMass.y;
					float x2 = reference3.invMass.x;
					float y3 = reference3.invMass.y;
					float num5 = num - num4;
					float num6 = 1f - num2;
					float num7 = num2;
					float num8 = 1f - num3;
					float num9 = num3;
					float3 float7 = float5 * num6;
					float3 float8 = float5 * num7;
					float3 float9 = -float5 * num8;
					float3 float10 = -float5 * num9;
					float num10 = x * num6 * num6 + y2 * num7 * num7 + x2 * num8 * num8 + y3 * num9 * num9;
					if (num10 != 0f)
					{
						num10 = num5 / num10;
						float3 add = num10 * x * float7;
						float3 add2 = num10 * y2 * float8;
						float3 add3 = num10 * x2 * float9;
						float3 add4 = num10 * y3 * float10;
						if ((reference2.flag & 0x4000001) == 0)
						{
							InterlockUtility.AddFloat3(reference2.particleIndices.x, add, unsafePtr, unsafePtr2);
						}
						if ((reference2.flag & 0x8000002) == 0)
						{
							InterlockUtility.AddFloat3(reference2.particleIndices.y, add2, unsafePtr, unsafePtr2);
						}
						if ((reference3.flag & 0x4000001) == 0)
						{
							InterlockUtility.AddFloat3(reference3.particleIndices.x, add3, unsafePtr, unsafePtr2);
						}
						if ((reference3.flag & 0x8000002) == 0)
						{
							InterlockUtility.AddFloat3(reference3.particleIndices.y, add4, unsafePtr, unsafePtr2);
						}
					}
				}
				else
				{
					if (reference.contactType != 1)
					{
						return;
					}
					float3 p = nextPosArray[reference3.particleIndices.x];
					float3 p2 = nextPosArray[reference3.particleIndices.y];
					float3 p3 = nextPosArray[reference3.particleIndices.z];
					float x3 = reference3.invMass.x;
					float y4 = reference3.invMass.y;
					float z = reference3.invMass.z;
					float3 obj2 = MathUtility.TriangleNormal(in p, in p2, in p3);
					float3 p4 = nextPosArray[reference2.particleIndices.x];
					float x4 = reference2.invMass.x;
					MathUtility.ClosestPtPointTriangle(in p4, in p, in p2, in p3, out var uvw);
					float num11 = reference.s;
					float3 float11 = obj2 * num11;
					float num12 = math.dot(float11, p4 - p);
					if (num12 >= num)
					{
						return;
					}
					float num13 = num;
					float num14 = num12 - num13;
					float3 float12 = float11;
					float3 float13 = -float11 * uvw[0];
					float3 float14 = -float11 * uvw[1];
					float3 float15 = -float11 * uvw[2];
					float num15 = x4 + x3 * uvw.x * uvw.x + y4 * uvw.y * uvw.y + z * uvw.z * uvw.z;
					if (num15 != 0f)
					{
						num15 = num14 / num15;
						float3 add5 = (0f - num15) * x4 * float12;
						float3 add6 = (0f - num15) * x3 * float13;
						float3 add7 = (0f - num15) * y4 * float14;
						float3 add8 = (0f - num15) * z * float15;
						if ((reference2.flag & 0x4000001) == 0)
						{
							InterlockUtility.AddFloat3(reference2.particleIndices.x, add5, unsafePtr, unsafePtr2);
						}
						if ((reference3.flag & 0x4000001) == 0)
						{
							InterlockUtility.AddFloat3(reference3.particleIndices.x, add6, unsafePtr, unsafePtr2);
						}
						if ((reference3.flag & 0x8000002) == 0)
						{
							InterlockUtility.AddFloat3(reference3.particleIndices.y, add7, unsafePtr, unsafePtr2);
						}
						if ((reference3.flag & 0x10000004) == 0)
						{
							InterlockUtility.AddFloat3(reference3.particleIndices.z, add8, unsafePtr, unsafePtr2);
						}
					}
				}
			}
		}

		[BurstCompile]
		internal struct SelfStep_SumContactJob : IJobParallelFor
		{
			public int updateIndex;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			[NativeDisableParallelForRestriction]
			public NativeArray<float3> tempVectorBufferA;

			[NativeDisableParallelForRestriction]
			public NativeArray<int> tempCountBuffer;

			public unsafe void Execute(int localIndex)
			{
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				int* unsafePtr = (int*)tempCountBuffer.GetUnsafePtr();
				int* unsafePtr2 = (int*)tempVectorBufferA.GetUnsafePtr();
				float3* unsafePtr3 = (float3*)nextPosArray.GetUnsafePtr();
				int num = batchSelfTeamList[localIndex];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				if (!reference.IsProcess || reference.ParticleCount == 0)
				{
					return;
				}
				if (updateIndex < reference.updateCount)
				{
					int num2 = reference.particleChunk.startIndex;
					int num3 = num2 * 3;
					int num4 = 0;
					while (num4 < reference.particleChunk.dataLength)
					{
						int num5 = unsafePtr[num2];
						if (num5 > 0)
						{
							float3 float5 = new float3(unsafePtr2[num3], unsafePtr2[num3 + 1], unsafePtr2[num3 + 2]);
							float5 /= (float)num5;
							float5 *= 1E-06f;
							unsafePtr3[num2] += float5;
						}
						num4++;
						num2++;
						num3 += 3;
					}
				}
				int num6 = reference.particleChunk.startIndex;
				int num7 = 0;
				while (num7 < reference.particleChunk.dataLength)
				{
					tempCountBuffer[num6] = 0;
					tempVectorBufferA[num6] = 0;
					num7++;
					num6++;
				}
			}
		}

		[BurstCompile]
		internal struct SelfDetectionIntersectJob : IJobParallelFor
		{
			public int updateIndex;

			public int workerCount;

			public int frameIndex;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<Primitive> primitiveArrayB;

			[ReadOnly]
			public NativeArray<GridInfo> uniformGridStartCountBuffer;

			[NativeDisableParallelForRestriction]
			public NativeQueue<IntersectInfo>.ParallelWriter intersectQueue;

			public unsafe void Execute(int index)
			{
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				int index2 = index / workerCount;
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				if (updateIndex >= reference.updateCount || reference.updateCount == 0 || !reference.IsProcess || reference.ParticleCount == 0)
				{
					return;
				}
				int workerIndex = index % workerCount;
				if (reference.flag.IsSet(44))
				{
					DetectionIntersect(workerCount, workerIndex, frameIndex, num, ref reference, 1u, num, ref reference, 2u, ref primitiveArrayB, ref uniformGridStartCountBuffer, ref intersectQueue);
				}
				if (reference.syncTeamId > 0)
				{
					ref TeamManager.TeamData targetTeam = ref unsafeReadOnlyPtr[reference.syncTeamId];
					if (reference.flag.IsSet(45))
					{
						DetectionIntersect(workerCount, workerIndex, frameIndex, num, ref reference, 1u, reference.syncTeamId, ref targetTeam, 2u, ref primitiveArrayB, ref uniformGridStartCountBuffer, ref intersectQueue);
					}
					if (reference.flag.IsSet(48))
					{
						DetectionIntersect(workerCount, workerIndex, frameIndex, num, ref reference, 2u, reference.syncTeamId, ref targetTeam, 1u, ref primitiveArrayB, ref uniformGridStartCountBuffer, ref intersectQueue);
					}
				}
			}
		}

		[BurstCompile]
		internal struct SelfConvertIntersectListJob : IJob
		{
			[ReadOnly]
			public NativeQueue<IntersectInfo> intersectQueue;

			[NativeDisableParallelForRestriction]
			public NativeList<IntersectInfo> intersectList;

			public void Execute()
			{
				intersectList.Clear();
				if (intersectQueue.Count > 0)
				{
					intersectList.AddRange(intersectQueue.ToArray(Allocator.Temp));
				}
			}
		}

		[BurstCompile]
		internal struct SelfClearIntersectJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[WriteOnly]
			[NativeDisableParallelForRestriction]
			public NativeArray<byte> intersectFlagArray;

			public unsafe void Execute(int index)
			{
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				if (reference.IsProcess && reference.ParticleCount != 0)
				{
					int num2 = reference.particleChunk.startIndex;
					int num3 = 0;
					while (num3 < reference.particleChunk.dataLength)
					{
						intersectFlagArray[num2] = 0;
						num3++;
						num2++;
					}
				}
			}
		}

		[BurstCompile]
		internal struct SelfSolverIntersectJob : IJobParallelForDefer
		{
			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeList<IntersectInfo> intersectList;

			[WriteOnly]
			[NativeDisableParallelForRestriction]
			public NativeArray<byte> intersectFlagArray;

			public unsafe void Execute(int index)
			{
				ref IntersectInfo reference = ref intersectList.GetUnsafeReadOnlyPtr()[index];
				float3 float5 = nextPosArray[reference.edgeParticeIndices.x];
				float3 float6 = nextPosArray[reference.edgeParticeIndices.y];
				float3 float7 = nextPosArray[reference.triangleParticleIndices.x];
				float3 obj = nextPosArray[reference.triangleParticleIndices.y];
				float3 obj2 = nextPosArray[reference.triangleParticleIndices.z];
				float3 float8 = float5 - float6;
				float3 float9 = obj2 - float7;
				float3 x = obj - float7;
				float3 y = math.cross(x, float9);
				float num = math.dot(float8, y);
				if (math.abs(num) < 1E-08f)
				{
					return;
				}
				if (num < 0f)
				{
					float5 = float6;
					float8 = -float8;
					num = 0f - num;
				}
				float3 float10 = float5 - float7;
				float num2 = math.dot(float10, y);
				if (num2 < 0f || num2 > num)
				{
					return;
				}
				float3 y2 = math.cross(float8, float10);
				float num3 = math.dot(float9, y2);
				if (!(num3 < 0f) && !(num3 > num))
				{
					float num4 = 0f - math.dot(x, y2);
					if (!(num4 < 0f) && !(num3 + num4 > num))
					{
						intersectFlagArray[reference.edgeParticeIndices.x] = 1;
						intersectFlagArray[reference.edgeParticeIndices.y] = 1;
					}
				}
			}
		}

		public const uint KindPoint = 0u;

		public const uint KindEdge = 1u;

		public const uint KindTriangle = 2u;

		public const uint Flag_KindMask = 50331648u;

		public const uint Flag_Fix0 = 67108864u;

		public const uint Flag_Fix1 = 134217728u;

		public const uint Flag_Fix2 = 268435456u;

		public const uint Flag_AllFix = 536870912u;

		public const uint Flag_Ignore = 1073741824u;

		public const uint Flag_Enable = 2147483648u;

		public const uint Flag_Intersect0 = 1u;

		public const uint Flag_Intersect1 = 2u;

		public const uint Flag_Intersect2 = 4u;

		public const uint Flag_FixIntersect0 = 67108865u;

		public const uint Flag_FixIntersect1 = 134217730u;

		public const uint Flag_FixIntersect2 = 268435460u;

		internal ExNativeArray<Primitive> primitiveArrayB;

		internal ExNativeArray<GridInfo> uniformGridStartCountBuffer;

		internal const byte ContactType_EdgeEdge = 0;

		internal const byte ContactType_PointTriangle = 1;

		internal const byte ContactType_TrianglePoint = 2;

		internal NativeQueue<ContactInfo> contactQueue;

		internal NativeList<ContactInfo> contactList;

		internal NativeQueue<IntersectInfo> intersectQueue;

		internal NativeList<IntersectInfo> intersectList;

		internal NativeArray<byte> intersectFlagArray;

		public int PointPrimitiveCount { get; private set; }

		public int EdgePrimitiveCount { get; private set; }

		public int TrianglePrimitiveCount { get; private set; }

		public int IntersectCount { get; private set; }

		public SelfCollisionConstraint()
		{
			intersectFlagArray = new NativeArray<byte>(0, Allocator.Persistent);
			primitiveArrayB = new ExNativeArray<Primitive>(0, create: true);
			uniformGridStartCountBuffer = new ExNativeArray<GridInfo>(0, create: true);
			contactQueue = new NativeQueue<ContactInfo>(Allocator.Persistent);
			contactList = new NativeList<ContactInfo>(Allocator.Persistent);
			intersectQueue = new NativeQueue<IntersectInfo>(Allocator.Persistent);
			intersectList = new NativeList<IntersectInfo>(Allocator.Persistent);
		}

		public void Dispose()
		{
			PointPrimitiveCount = 0;
			EdgePrimitiveCount = 0;
			TrianglePrimitiveCount = 0;
			NativeArrayExtensions.MC2DisposeSafe(ref intersectFlagArray);
			primitiveArrayB?.Dispose();
			primitiveArrayB = null;
			uniformGridStartCountBuffer?.Dispose();
			uniformGridStartCountBuffer = null;
			if (contactQueue.IsCreated)
			{
				contactQueue.Dispose();
			}
			if (contactList.IsCreated)
			{
				contactList.Dispose();
			}
			if (intersectQueue.IsCreated)
			{
				intersectQueue.Dispose();
			}
			if (intersectList.IsCreated)
			{
				intersectList.Dispose();
			}
			IntersectCount = 0;
		}

		public bool HasPrimitive()
		{
			return PointPrimitiveCount + EdgePrimitiveCount + TrianglePrimitiveCount > 0;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[SelfCollisionConstraint]");
			stringBuilder.AppendLine($"  -intersectFlagArray:{(intersectFlagArray.IsCreated ? intersectFlagArray.Length : 0)}");
			return stringBuilder.ToString();
		}

		internal void Register(ClothProcess cprocess)
		{
			UpdateTeam(cprocess.TeamId);
		}

		internal void Exit(ClothProcess cprocess)
		{
			if (cprocess != null && cprocess.TeamId > 0)
			{
				UpdateTeam(cprocess.TeamId);
			}
		}

		internal void UpdateTeam(int teamId)
		{
			TeamManager team = MagicaManager.Team;
			if (!team.ContainsTeamData(teamId))
			{
				return;
			}
			ref TeamManager.TeamData teamDataRef = ref team.GetTeamDataRef(teamId);
			BitField64 flag = teamDataRef.flag;
			bool flag2 = teamDataRef.flag.IsSet(8);
			ref ClothParameters parametersRef = ref team.GetParametersRef(teamId);
			SelfCollisionMode num = ((!flag2) ? parametersRef.selfCollisionConstraint.selfMode : SelfCollisionMode.None);
			SelfCollisionMode selfCollisionMode = ((!flag2) ? parametersRef.selfCollisionConstraint.syncMode : SelfCollisionMode.None);
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool value = false;
			bool value2 = false;
			bool value3 = false;
			bool value4 = false;
			bool value5 = false;
			bool value6 = false;
			bool value7 = false;
			bool value8 = false;
			bool value9 = false;
			bool value10 = false;
			bool value11 = false;
			bool value12 = false;
			bool value13 = false;
			bool value14 = false;
			bool value15 = false;
			if (num == SelfCollisionMode.FullMesh)
			{
				if (teamDataRef.EdgeCount > 0)
				{
					flag4 = true;
					value = true;
				}
				if (teamDataRef.TriangleCount > 0)
				{
					flag3 = true;
					flag5 = true;
					value2 = true;
					value3 = true;
				}
				if (teamDataRef.EdgeCount > 0 && teamDataRef.TriangleCount > 0)
				{
					value4 = true;
					value5 = true;
				}
			}
			if (selfCollisionMode != SelfCollisionMode.None && team.ContainsTeamData(teamDataRef.syncTeamId))
			{
				ref TeamManager.TeamData teamDataRef2 = ref team.GetTeamDataRef(teamDataRef.syncTeamId);
				if (selfCollisionMode == SelfCollisionMode.FullMesh)
				{
					if (teamDataRef.EdgeCount > 0 && teamDataRef2.EdgeCount > 0)
					{
						flag4 = true;
						value6 = true;
					}
					if (teamDataRef.TriangleCount > 0)
					{
						flag5 = true;
						value8 = true;
					}
					if (teamDataRef2.TriangleCount > 0)
					{
						flag3 = true;
						value7 = true;
					}
					if (teamDataRef.EdgeCount > 0 && teamDataRef2.TriangleCount > 0)
					{
						value9 = true;
					}
					if (teamDataRef.TriangleCount > 0 && teamDataRef2.EdgeCount > 0)
					{
						value10 = true;
					}
				}
			}
			if (teamDataRef.syncParentTeamId.Length > 0 && !flag2)
			{
				for (int i = 0; i < teamDataRef.syncParentTeamId.Length; i++)
				{
					int teamId2 = teamDataRef.syncParentTeamId[i];
					ref TeamManager.TeamData teamDataRef3 = ref team.GetTeamDataRef(teamId2);
					if (teamDataRef3.IsValid && team.GetParametersRef(teamId2).selfCollisionConstraint.syncMode == SelfCollisionMode.FullMesh)
					{
						if (teamDataRef3.EdgeCount > 0 && teamDataRef.EdgeCount > 0)
						{
							flag4 = true;
							value11 = true;
						}
						if (teamDataRef3.TriangleCount > 0)
						{
							flag3 = true;
							value12 = true;
						}
						if (teamDataRef.TriangleCount > 0)
						{
							flag5 = true;
							value13 = true;
						}
						if (teamDataRef.EdgeCount > 0 && teamDataRef3.TriangleCount > 0)
						{
							value14 = true;
						}
						if (teamDataRef.TriangleCount > 0 && teamDataRef3.EdgeCount > 0)
						{
							value15 = true;
						}
					}
				}
			}
			teamDataRef.flag.SetBits(32, flag3);
			teamDataRef.flag.SetBits(33, flag4);
			teamDataRef.flag.SetBits(34, flag5);
			teamDataRef.flag.SetBits(35, value);
			teamDataRef.flag.SetBits(38, value2);
			teamDataRef.flag.SetBits(41, value3);
			teamDataRef.flag.SetBits(44, value4);
			teamDataRef.flag.SetBits(47, value5);
			teamDataRef.flag.SetBits(36, value6);
			teamDataRef.flag.SetBits(39, value7);
			teamDataRef.flag.SetBits(42, value8);
			teamDataRef.flag.SetBits(45, value9);
			teamDataRef.flag.SetBits(48, value10);
			teamDataRef.flag.SetBits(37, value11);
			teamDataRef.flag.SetBits(40, value12);
			teamDataRef.flag.SetBits(43, value13);
			teamDataRef.flag.SetBits(46, value14);
			teamDataRef.flag.SetBits(49, value15);
			if (flag3 && !teamDataRef.selfPointChunk.IsValid)
			{
				int particleCount = teamDataRef.ParticleCount;
				teamDataRef.selfPointChunk = primitiveArrayB.AddRange(particleCount);
				uniformGridStartCountBuffer.AddRange(particleCount);
				int startIndex = teamDataRef.selfPointChunk.startIndex;
				InitPrimitive(teamId, teamDataRef, 0u, startIndex, particleCount);
				PointPrimitiveCount += particleCount;
			}
			else if (!flag3 && teamDataRef.selfPointChunk.IsValid)
			{
				primitiveArrayB.Remove(teamDataRef.selfPointChunk);
				uniformGridStartCountBuffer.Remove(teamDataRef.selfPointChunk);
				PointPrimitiveCount -= teamDataRef.selfPointChunk.dataLength;
				teamDataRef.selfPointChunk.Clear();
			}
			if (flag4 && !teamDataRef.selfEdgeChunk.IsValid)
			{
				int edgeCount = teamDataRef.EdgeCount;
				teamDataRef.selfEdgeChunk = primitiveArrayB.AddRange(edgeCount);
				uniformGridStartCountBuffer.AddRange(edgeCount);
				int startIndex2 = teamDataRef.selfEdgeChunk.startIndex;
				InitPrimitive(teamId, teamDataRef, 1u, startIndex2, edgeCount);
				EdgePrimitiveCount += edgeCount;
			}
			else if (!flag4 && teamDataRef.selfEdgeChunk.IsValid)
			{
				primitiveArrayB.Remove(teamDataRef.selfEdgeChunk);
				uniformGridStartCountBuffer.Remove(teamDataRef.selfEdgeChunk);
				EdgePrimitiveCount -= teamDataRef.selfEdgeChunk.dataLength;
				teamDataRef.selfEdgeChunk.Clear();
			}
			if (flag5 && !teamDataRef.selfTriangleChunk.IsValid)
			{
				int triangleCount = teamDataRef.TriangleCount;
				teamDataRef.selfTriangleChunk = primitiveArrayB.AddRange(triangleCount);
				uniformGridStartCountBuffer.AddRange(triangleCount);
				int startIndex3 = teamDataRef.selfTriangleChunk.startIndex;
				InitPrimitive(teamId, teamDataRef, 2u, startIndex3, triangleCount);
				TrianglePrimitiveCount += triangleCount;
			}
			else if (!flag5 && teamDataRef.selfTriangleChunk.IsValid)
			{
				primitiveArrayB.Remove(teamDataRef.selfTriangleChunk);
				uniformGridStartCountBuffer.Remove(teamDataRef.selfTriangleChunk);
				TrianglePrimitiveCount -= teamDataRef.selfTriangleChunk.dataLength;
				teamDataRef.selfTriangleChunk.Clear();
			}
			bool flag6 = teamDataRef.flag.TestAny(44, 6);
			bool flag7 = flag.TestAny(44, 6);
			if (flag6 && !flag7)
			{
				IntersectCount++;
			}
			else if (!flag6 && flag7)
			{
				IntersectCount--;
			}
			if (selfCollisionMode != SelfCollisionMode.None && team.ContainsTeamData(teamDataRef.syncTeamId))
			{
				UpdateTeam(teamDataRef.syncTeamId);
			}
		}

		private void InitPrimitive(int teamId, TeamManager.TeamData tdata, uint kind, int startPrimitive, int length)
		{
			VirtualMeshManager vMesh = MagicaManager.VMesh;
			IJobParallelForExtensions.Run(new InitPrimitiveJob
			{
				teamId = teamId,
				tdata = tdata,
				kind = kind,
				startPrimitive = startPrimitive,
				edges = vMesh.edges.GetNativeArray(),
				triangles = vMesh.triangles.GetNativeArray(),
				attributes = vMesh.attributes.GetNativeArray(),
				vertexDepths = vMesh.vertexDepths.GetNativeArray(),
				primitiveArrayB = primitiveArrayB.GetNativeArray()
			}, length);
		}

		internal void WorkBufferUpdate()
		{
			if (IntersectCount > 0)
			{
				int particleCount = MagicaManager.Simulation.ParticleCount;
				NativeArrayExtensions.MC2Resize(ref intersectFlagArray, particleCount);
			}
		}

		private unsafe static void UpdatePrimitive(int k, int teamId, ref TeamManager.TeamData tdata, ref ClothParameters param, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> oldPosArray, ref NativeArray<float> frictionArray, ref NativeArray<Primitive> primitiveArrayB, ref NativeArray<byte> intersectFlagArray)
		{
			Primitive* unsafePtr = (Primitive*)primitiveArrayB.GetUnsafePtr();
			float num = 0f;
			DataChunk dataChunk = ((k == 0L) ? tdata.selfPointChunk : (((long)k == 1) ? tdata.selfEdgeChunk : tdata.selfTriangleChunk));
			if (dataChunk.IsValid)
			{
				float3x3 float3x5 = 0;
				float3x3 float3x6 = 0;
				int num2 = dataChunk.startIndex;
				int num3 = 0;
				while (num3 < dataChunk.dataLength)
				{
					ref Primitive reference = ref unsafePtr[num2];
					if (!reference.IsIgnore())
					{
						int num4 = k + 1;
						uint num5 = 67108864u;
						uint num6 = 0u;
						for (int i = 0; i < num4; i++)
						{
							int index = reference.particleIndices[i];
							float3x5[i] = nextPosArray[index];
							float3x6[i] = oldPosArray[index];
							bool fix = (reference.flag & num5) != 0;
							reference.invMass[i] = MathUtility.CalcSelfCollisionInverseMass(frictionArray[index], fix, param.selfCollisionConstraint.clothMass);
							num5 <<= 1;
							if (intersectFlagArray[index] != 0)
							{
								num6 |= (uint)(1 << i);
							}
						}
						float num7 = param.selfCollisionConstraint.surfaceThicknessCurveData.MC2EvaluateCurve(reference.depth);
						num7 = (reference.thickness = num7 * tdata.scaleRatio);
						AABB aabb = new AABB(math.min(float3x5[0], float3x6[0]), math.max(float3x5[0], float3x6[0]));
						for (int j = 1; j < num4; j++)
						{
							aabb.Encapsulate(in float3x5[j]);
							aabb.Encapsulate(in float3x6[j]);
						}
						float maxSideLength = aabb.MaxSideLength;
						num = math.max(num, maxSideLength);
						aabb.Expand(num7);
						reference.aabb = aabb;
						reference.flag = (reference.flag & 0xFFFFFFF8u) | num6;
					}
					num3++;
					num2++;
				}
			}
			if ((long)k == 1)
			{
				float selfGridSize = num * 3f;
				tdata.selfGridSize = selfGridSize;
				tdata.selfMaxPrimitiveSize = num;
			}
		}

		private unsafe static void UpdateGrid(int k, int teamId, ref TeamManager.TeamData tdata, ref NativeArray<Primitive> primitiveArrayB, ref NativeArray<GridInfo> uniformGridStartCountBuffer)
		{
			Primitive* unsafeReadOnlyPtr = (Primitive*)primitiveArrayB.GetUnsafeReadOnlyPtr();
			GridInfo* unsafePtr = (GridInfo*)uniformGridStartCountBuffer.GetUnsafePtr();
			if (!tdata.flag.IsSet(35) && !tdata.flag.IsSet(38) && !tdata.flag.IsSet(41) && !tdata.flag.IsSet(37) && !tdata.flag.IsSet(40) && !tdata.flag.IsSet(43))
			{
				return;
			}
			DataChunk dataChunk = ((k == 0L) ? tdata.selfPointChunk : (((long)k == 1) ? tdata.selfEdgeChunk : tdata.selfTriangleChunk));
			if (!dataChunk.IsValid)
			{
				return;
			}
			int num = dataChunk.startIndex;
			int num2 = 0;
			while (num2 < dataChunk.dataLength)
			{
				ref Primitive reference = ref unsafeReadOnlyPtr[num];
				if (reference.IsIgnore())
				{
					reference.grid = 1000000;
				}
				else
				{
					reference.grid = GetGrid(reference.aabb.Center, tdata.selfGridSize);
				}
				num2++;
				num++;
			}
			NativeSortExtension.Sort(unsafeReadOnlyPtr + dataChunk.startIndex, dataChunk.dataLength);
			int3 int5 = 0;
			int start = 0;
			int num3 = 0;
			int startIndex = dataChunk.startIndex;
			int num4 = startIndex;
			int num5 = 0;
			num = dataChunk.startIndex;
			int num6 = 0;
			while (num6 < dataChunk.dataLength)
			{
				ref Primitive reference2 = ref unsafeReadOnlyPtr[num];
				if (num6 == 0)
				{
					int5 = reference2.grid;
					start = num;
					num3 = 0;
				}
				else if (!reference2.grid.Equals(int5.xyz))
				{
					uniformGridStartCountBuffer[num4] = new GridInfo
					{
						hash = int5.GetHashCode(),
						start = start,
						count = num3
					};
					num4++;
					num5++;
					int5 = reference2.grid;
					start = num;
					num3 = 0;
				}
				num3++;
				num6++;
				num++;
			}
			if (num3 > 0)
			{
				uniformGridStartCountBuffer[num4] = new GridInfo
				{
					hash = int5.GetHashCode(),
					start = start,
					count = num3
				};
				num4++;
				num5++;
			}
			switch (k)
			{
			case 0:
				tdata.selfPointGridCount = num5;
				break;
			case 1:
				tdata.selfEdgeGridCount = num5;
				break;
			case 2:
				tdata.selfTriangleGridCount = num5;
				break;
			}
			NativeSortExtension.Sort(unsafePtr + startIndex, num5);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int3 GetGrid(float3 pos, float gridSize)
		{
			return new int3(math.floor(pos / gridSize));
		}

		private unsafe static void DetectionContacts(int workerCount, int workerIndex, int myTeamId, ref TeamManager.TeamData myTeam, uint myKind, int targetTeamId, ref TeamManager.TeamData targetTeam, uint targetKind, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> oldPosArray, ref NativeArray<Primitive> primitiveArrayB, ref NativeArray<GridInfo> uniformGridStartCountBuffer, ref NativeQueue<ContactInfo>.ParallelWriter contactQueue)
		{
			Primitive* unsafeReadOnlyPtr = (Primitive*)primitiveArrayB.GetUnsafeReadOnlyPtr();
			GridInfo* unsafeReadOnlyPtr2 = (GridInfo*)uniformGridStartCountBuffer.GetUnsafeReadOnlyPtr();
			DataChunk dataChunk = myKind switch
			{
				1u => myTeam.selfEdgeChunk, 
				0u => myTeam.selfPointChunk, 
				_ => myTeam.selfTriangleChunk, 
			};
			if (!dataChunk.IsValid)
			{
				return;
			}
			int2 int5 = MathUtility.CalcSplitRange(dataChunk.dataLength, workerCount, workerIndex);
			dataChunk.startIndex += int5.x;
			dataChunk.dataLength = int5.y - int5.x;
			DataChunk dataChunk2 = targetKind switch
			{
				1u => targetTeam.selfEdgeChunk, 
				0u => targetTeam.selfPointChunk, 
				_ => targetTeam.selfTriangleChunk, 
			};
			if (!dataChunk2.IsValid)
			{
				return;
			}
			int startIndex = dataChunk2.startIndex;
			int length = 0;
			switch (targetKind)
			{
			case 0u:
				length = targetTeam.selfPointGridCount;
				break;
			case 1u:
				length = targetTeam.selfEdgeGridCount;
				break;
			case 2u:
				length = targetTeam.selfTriangleGridCount;
				break;
			}
			float selfMaxPrimitiveSize = targetTeam.selfMaxPrimitiveSize;
			float selfGridSize = targetTeam.selfGridSize;
			bool flag = myTeamId == targetTeamId && myKind == targetKind;
			bool flag2 = myTeamId == targetTeamId;
			bool flag3 = false;
			byte contactType = 0;
			if (myKind == 0 && targetKind == 2)
			{
				contactType = 1;
			}
			else if (myKind == 2 && targetKind == 0)
			{
				contactType = 1;
				flag3 = true;
			}
			GridInfo value = default(GridInfo);
			int num = dataChunk.startIndex;
			int num2 = 0;
			while (num2 < dataChunk.dataLength)
			{
				ref Primitive reference = ref unsafeReadOnlyPtr[num];
				if (!reference.IsIgnore())
				{
					bool flag4 = (reference.flag & 0x20000000) != 0;
					float3 pos = reference.aabb.Min - selfMaxPrimitiveSize * 0.5f;
					float3 pos2 = reference.aabb.Max + selfMaxPrimitiveSize * 0.5f;
					int3 grid = GetGrid(pos, selfGridSize);
					int3 grid2 = GetGrid(pos2, selfGridSize);
					int3 int6 = grid;
					bool flag5 = false;
					while (!flag5)
					{
						int hashCode = int6.GetHashCode();
						value.hash = hashCode;
						int num3 = NativeSortExtension.BinarySearch(unsafeReadOnlyPtr2 + startIndex, length, value);
						if (num3 >= 0)
						{
							ref GridInfo reference2 = ref (unsafeReadOnlyPtr2 + startIndex)[num3];
							int start = reference2.start;
							int num4 = start + reference2.count;
							if (!flag || num4 >= num)
							{
								for (int i = (flag ? math.max(start, num) : start); i < num4; i++)
								{
									if (flag && num == i)
									{
										continue;
									}
									ref Primitive reference3 = ref unsafeReadOnlyPtr[i];
									if (reference.aabb.Overlaps(in reference3.aabb) && !reference3.IsIgnore() && (!flag4 || (reference3.flag & 0x20000000) == 0) && (!flag2 || !reference.AnyParticle(ref reference3)))
									{
										ContactInfo contact = new ContactInfo
										{
											primitiveIndex0 = ((!flag3) ? num : i),
											primitiveIndex1 = ((!flag3) ? i : num),
											contactType = contactType,
											thickness = (half)(reference.thickness + reference3.thickness)
										};
										UpdateContactInfo(ref contact, unsafeReadOnlyPtr, ref nextPosArray, ref oldPosArray, 2f, first: true);
										if (contact.enable != 0)
										{
											contactQueue.Enqueue(contact);
										}
									}
								}
							}
						}
						int6.x++;
						if (int6.x <= grid2.x)
						{
							continue;
						}
						int6.x = grid.x;
						int6.y++;
						if (int6.y > grid2.y)
						{
							int6.y = grid.y;
							int6.z++;
							if (int6.z > grid2.z)
							{
								flag5 = true;
							}
						}
					}
				}
				num2++;
				num++;
			}
		}

		private unsafe static void UpdateContactInfo(ref ContactInfo contact, Primitive* pt, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> oldPosArray, float scrScale, bool first)
		{
			ref Primitive reference = ref pt[contact.primitiveIndex0];
			ref Primitive reference2 = ref pt[contact.primitiveIndex1];
			float num = contact.thickness;
			float num2 = num * scrScale;
			contact.enable = 0;
			if (contact.contactType == 0)
			{
				float3 float5 = nextPosArray[reference.particleIndices.x];
				float3 float6 = nextPosArray[reference.particleIndices.y];
				float3 float7 = nextPosArray[reference2.particleIndices.x];
				float3 float8 = nextPosArray[reference2.particleIndices.y];
				float3 p = oldPosArray[reference.particleIndices.x];
				float3 q = oldPosArray[reference.particleIndices.y];
				float3 p2 = oldPosArray[reference2.particleIndices.x];
				float3 q2 = oldPosArray[reference2.particleIndices.y];
				float s;
				float t;
				float3 c;
				float3 c2;
				float num3 = math.sqrt(MathUtility.ClosestPtSegmentSegment(in p, in q, in p2, in q2, out s, out t, out c, out c2));
				if (!(num3 < 1E-09f))
				{
					float3 float9 = (c - c2) / num3;
					float3 start = float5 - p;
					float3 end = float6 - q;
					float3 start2 = float7 - p2;
					float3 end2 = float8 - q2;
					float3 y = math.lerp(start, end, s);
					float3 y2 = math.lerp(start2, end2, t);
					float num4 = math.dot(float9, y);
					float num5 = math.dot(float9, y2);
					if (!(num3 + num4 - num5 > num + num2))
					{
						contact.enable = 1;
						contact.s = (half)s;
						contact.t = (half)t;
						contact.n = (half3)float9;
					}
				}
			}
			else
			{
				if (contact.contactType != 1)
				{
					return;
				}
				float3 float10 = nextPosArray[reference.particleIndices.x];
				float3 p3 = oldPosArray[reference.particleIndices.x];
				float3 obj = nextPosArray[reference2.particleIndices.x];
				float3 float11 = nextPosArray[reference2.particleIndices.y];
				float3 float12 = nextPosArray[reference2.particleIndices.z];
				float3 a = oldPosArray[reference2.particleIndices.x];
				float3 b = oldPosArray[reference2.particleIndices.y];
				float3 c3 = oldPosArray[reference2.particleIndices.z];
				float3 y3 = float10 - p3;
				float3 obj2 = obj - a;
				float3 float13 = float11 - b;
				float3 float14 = float12 - c3;
				float3 uvw;
				float3 float15 = MathUtility.ClosestPtPointTriangle(in p3, in a, in b, in c3, out uvw);
				float3 y4 = obj2 * uvw.x + float13 * uvw.y + float14 * uvw.z;
				float3 float16 = float15 - p3;
				float num6 = math.length(float16);
				if (num6 <= 1E-08f)
				{
					return;
				}
				float3 x = float16 / num6;
				float num7 = math.dot(x, y3);
				float num8 = math.dot(x, y4);
				if (num6 - num7 + num8 >= num + num2)
				{
					return;
				}
				float num9 = contact.s;
				if (first)
				{
					float3 x2 = MathUtility.TriangleNormal(in a, in b, in c3);
					x = math.normalize(p3 - float15);
					float x3 = math.dot(x2, x);
					if (!(math.abs(x3) >= Define.System.SelfCollisionPointTriangleAngleCos))
					{
						return;
					}
					num9 = math.sign(x3);
				}
				contact.s = (half)num9;
				contact.enable = 1;
			}
		}

		private unsafe static void DetectionIntersect(int workerCount, int workerIndex, int frameIndex, int myTeamId, ref TeamManager.TeamData myTeam, uint myKind, int targetTeamId, ref TeamManager.TeamData targetTeam, uint targetKind, ref NativeArray<Primitive> primitiveArrayB, ref NativeArray<GridInfo> uniformGridStartCountBuffer, ref NativeQueue<IntersectInfo>.ParallelWriter intersectQueue)
		{
			Primitive* unsafeReadOnlyPtr = (Primitive*)primitiveArrayB.GetUnsafeReadOnlyPtr();
			GridInfo* unsafeReadOnlyPtr2 = (GridInfo*)uniformGridStartCountBuffer.GetUnsafeReadOnlyPtr();
			DataChunk dataChunk = myKind switch
			{
				1u => myTeam.selfEdgeChunk, 
				0u => myTeam.selfPointChunk, 
				_ => myTeam.selfTriangleChunk, 
			};
			if (!dataChunk.IsValid)
			{
				return;
			}
			DataChunk dataChunk2 = targetKind switch
			{
				1u => targetTeam.selfEdgeChunk, 
				0u => targetTeam.selfPointChunk, 
				_ => targetTeam.selfTriangleChunk, 
			};
			if (!dataChunk2.IsValid)
			{
				return;
			}
			int startIndex = dataChunk2.startIndex;
			int length = 0;
			switch (targetKind)
			{
			case 0u:
				length = targetTeam.selfPointGridCount;
				break;
			case 1u:
				length = targetTeam.selfEdgeGridCount;
				break;
			case 2u:
				length = targetTeam.selfTriangleGridCount;
				break;
			}
			float selfMaxPrimitiveSize = targetTeam.selfMaxPrimitiveSize;
			float selfGridSize = targetTeam.selfGridSize;
			bool flag = myTeamId == targetTeamId;
			bool flag2 = myKind != 1;
			DataChunk workerChunk = MathUtility.GetWorkerChunk(dataChunk.dataLength, workerCount, workerIndex);
			if (!workerChunk.IsValid)
			{
				return;
			}
			GridInfo value = default(GridInfo);
			int num = dataChunk.startIndex + workerChunk.startIndex;
			int num2 = 0;
			while (num2 < workerChunk.dataLength)
			{
				if (num % 2 == frameIndex)
				{
					ref Primitive reference = ref unsafeReadOnlyPtr[num];
					if (!reference.IsIgnore())
					{
						bool flag3 = (reference.flag & 0x20000000) != 0;
						float3 pos = reference.aabb.Min - selfMaxPrimitiveSize * 0.5f;
						float3 pos2 = reference.aabb.Max + selfMaxPrimitiveSize * 0.5f;
						int3 grid = GetGrid(pos, selfGridSize);
						int3 grid2 = GetGrid(pos2, selfGridSize);
						int3 int5 = grid;
						bool flag4 = false;
						while (!flag4)
						{
							int hashCode = int5.GetHashCode();
							value.hash = hashCode;
							int num3 = NativeSortExtension.BinarySearch(unsafeReadOnlyPtr2 + startIndex, length, value);
							if (num3 >= 0)
							{
								ref GridInfo reference2 = ref (unsafeReadOnlyPtr2 + startIndex)[num3];
								int start = reference2.start;
								int num4 = start + reference2.count;
								for (int i = start; i < num4; i++)
								{
									ref Primitive reference3 = ref unsafeReadOnlyPtr[i];
									if (reference.aabb.Overlaps(in reference3.aabb) && !reference3.IsIgnore() && (!flag3 || (reference3.flag & 0x20000000) == 0) && (!flag || !reference.AnyParticle(ref reference3)))
									{
										IntersectInfo value2 = new IntersectInfo
										{
											edgeParticeIndices = ((!flag2) ? reference.particleIndices.xy : reference3.particleIndices.xy),
											triangleParticleIndices = ((!flag2) ? reference3.particleIndices : reference.particleIndices)
										};
										intersectQueue.Enqueue(value2);
									}
								}
							}
							int5.x++;
							if (int5.x <= grid2.x)
							{
								continue;
							}
							int5.x = grid.x;
							int5.y++;
							if (int5.y > grid2.y)
							{
								int5.y = grid.y;
								int5.z++;
								if (int5.z > grid2.z)
								{
									flag4 = true;
								}
							}
						}
					}
				}
				num2++;
				num++;
			}
		}
	}
}
