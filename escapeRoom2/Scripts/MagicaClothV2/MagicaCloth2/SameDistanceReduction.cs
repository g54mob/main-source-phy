using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class SameDistanceReduction : IDisposable
	{
		[BurstCompile]
		private struct InitGridJob : IJob
		{
			public int vcnt;

			public float gridSize;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<int> joinIndices;

			public NativeParallelMultiHashMap<int3, int> gridMap;

			public void Execute()
			{
				for (int i = 0; i < vcnt; i++)
				{
					if (joinIndices[i] < 0)
					{
						GridMap<int>.AddGrid(localPositions[i], i, gridMap, gridSize);
					}
				}
			}
		}

		[BurstCompile]
		private struct SearchJoinJob : IJob
		{
			public int vcnt;

			public float gridSize;

			public float radius;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<int> joinIndices;

			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			public NativeParallelMultiHashMap<ushort, ushort> joinPairMap;

			public void Execute()
			{
				for (int i = 0; i < vcnt; i++)
				{
					if (joinIndices[i] >= 0)
					{
						continue;
					}
					float3 float5 = localPositions[i];
					foreach (int3 item in GridMap<int>.GetArea(float5, radius, gridMap, gridSize))
					{
						if (!gridMap.ContainsKey(item))
						{
							continue;
						}
						foreach (int item2 in gridMap.GetValuesForKey(item))
						{
							if (item2 != i)
							{
								float3 y = localPositions[item2];
								if (!(math.distance(float5, y) > radius))
								{
									joinPairMap.Add((ushort)i, (ushort)item2);
								}
							}
						}
					}
				}
			}
		}

		[BurstCompile]
		private struct JoinJob2 : IJob
		{
			public int vertexCount;

			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> joinPairMap;

			public NativeArray<int> joinIndices;

			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			public NativeArray<VertexAttribute> attributes;

			public NativeReference<int> result;

			public NativeList<ushort> tempList;

			public void Execute()
			{
				int num = 0;
				for (ushort num2 = 0; num2 < vertexCount; num2++)
				{
					if (joinIndices[num2] < 0)
					{
						foreach (ushort item in joinPairMap.GetValuesForKey(num2))
						{
							if (joinIndices[item] >= 0)
							{
								continue;
							}
							joinIndices[item] = num2;
							num++;
							vertexToVertexMap.MC2RemoveValue(num2, item);
							tempList.Clear();
							foreach (ushort item2 in vertexToVertexMap.GetValuesForKey(item))
							{
								ushort value = item2;
								tempList.Add(in value);
							}
							foreach (ushort temp in tempList)
							{
								if (joinIndices[temp] < 0 && temp != num2 && temp != item)
								{
									vertexToVertexMap.MC2RemoveValue(temp, item);
									vertexToVertexMap.MC2UniqueAdd(num2, temp);
									vertexToVertexMap.MC2UniqueAdd(temp, num2);
									VirtualMeshBoneWeight value2 = boneWeights[num2];
									value2.AddWeight(boneWeights[item]);
									boneWeights[num2] = value2;
									VertexAttribute attr = attributes[item];
									VertexAttribute attr2 = attributes[num2];
									attributes[num2] = VertexAttribute.JoinAttribute(attr, attr2);
									attributes[item] = VertexAttribute.Invalid;
								}
							}
						}
					}
				}
				result.Value = num;
			}
		}

		[BurstCompile]
		private struct UpdateJoinIndexJob : IJobParallelFor
		{
			[NativeDisableParallelForRestriction]
			public NativeArray<int> joinIndices;

			public void Execute(int vindex)
			{
				int num = joinIndices[vindex];
				if (num >= 0)
				{
					while (joinIndices[num] >= 0)
					{
						num = joinIndices[num];
					}
					joinIndices[vindex] = num;
				}
			}
		}

		[BurstCompile]
		private struct UpdateLinkIndexJob : IJobParallelFor
		{
			[NativeDisableParallelForRestriction]
			public NativeArray<int> joinIndices;

			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

			public void Execute(int vindex)
			{
				if (joinIndices[vindex] >= 0)
				{
					return;
				}
				FixedList512Bytes<ushort> fixedList = default(FixedList512Bytes<ushort>);
				foreach (ushort item in vertexToVertexMap.GetValuesForKey((ushort)vindex))
				{
					int num = item;
					int num2 = joinIndices[num];
					if (num2 >= 0)
					{
						num = num2;
					}
					if (num != vindex)
					{
						fixedList.MC2Set((ushort)num);
					}
				}
				vertexToVertexMap.Remove((ushort)vindex);
				for (int i = 0; i < fixedList.Length; i++)
				{
					vertexToVertexMap.Add((ushort)vindex, fixedList[i]);
				}
			}
		}

		[BurstCompile]
		private struct FinalMergeVertexJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<int> joinIndices;

			public NativeArray<float3> localNormals;

			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			public void Execute(int vindex)
			{
				if (joinIndices[vindex] < 0)
				{
					localNormals[vindex] = math.normalize(localNormals[vindex]);
					VirtualMeshBoneWeight value = boneWeights[vindex];
					value.AdjustWeight();
					boneWeights[vindex] = value;
				}
			}
		}

		private string name = string.Empty;

		private VirtualMesh vmesh;

		private ReductionWorkData workData;

		private ResultCode result;

		private float mergeLength;

		private GridMap<int> gridMap;

		private NativeParallelMultiHashMap<ushort, ushort> joinPairMap;

		private NativeReference<int> resultRef;

		public ResultCode Result => result;

		public SameDistanceReduction()
		{
		}

		public SameDistanceReduction(string name, VirtualMesh mesh, ReductionWorkData workingData, float mergeLength)
		{
			this.name = name;
			vmesh = mesh;
			workData = workingData;
			result = ResultCode.None;
			this.mergeLength = math.max(mergeLength, 1E-09f);
		}

		public virtual void Dispose()
		{
			if (joinPairMap.IsCreated)
			{
				joinPairMap.Dispose();
			}
			if (resultRef.IsCreated)
			{
				resultRef.Dispose();
			}
			gridMap?.Dispose();
		}

		public ResultCode Reduction()
		{
			result.Clear();
			try
			{
				gridMap = new GridMap<int>(vmesh.VertexCount);
				float gridSize = mergeLength * 2f;
				joinPairMap = new NativeParallelMultiHashMap<ushort, ushort>(vmesh.VertexCount, Allocator.Persistent);
				resultRef = new NativeReference<int>(Allocator.Persistent);
				new InitGridJob
				{
					vcnt = vmesh.VertexCount,
					gridSize = gridSize,
					localPositions = vmesh.localPositions.GetNativeArray(),
					joinIndices = workData.vertexJoinIndices,
					gridMap = gridMap.GetMultiHashMap()
				}.Run();
				new SearchJoinJob
				{
					vcnt = vmesh.VertexCount,
					gridSize = gridSize,
					radius = mergeLength,
					localPositions = vmesh.localPositions.GetNativeArray(),
					joinIndices = workData.vertexJoinIndices,
					gridMap = gridMap.GetMultiHashMap(),
					joinPairMap = joinPairMap
				}.Run();
				using NativeList<ushort> tempList = new NativeList<ushort>(2048, Allocator.Persistent);
				new JoinJob2
				{
					vertexCount = vmesh.VertexCount,
					joinPairMap = joinPairMap,
					joinIndices = workData.vertexJoinIndices,
					vertexToVertexMap = workData.vertexToVertexMap,
					boneWeights = vmesh.boneWeights.GetNativeArray(),
					attributes = vmesh.attributes.GetNativeArray(),
					result = resultRef,
					tempList = tempList
				}.Run();
				UpdateJoinAndLink();
				UpdateReductionResultJob();
				int value = resultRef.Value;
				workData.removeVertexCount += value;
				result.SetSuccess();
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				result.SetError(Define.Result.Reduction_SameDistanceException);
			}
			return result;
		}

		private void UpdateJoinAndLink()
		{
			IJobParallelForExtensions.Run(new UpdateJoinIndexJob
			{
				joinIndices = workData.vertexJoinIndices
			}, vmesh.VertexCount);
			IJobParallelForExtensions.Run(new UpdateLinkIndexJob
			{
				joinIndices = workData.vertexJoinIndices,
				vertexToVertexMap = workData.vertexToVertexMap
			}, vmesh.VertexCount);
		}

		private void UpdateReductionResultJob()
		{
			IJobParallelForExtensions.Run(new FinalMergeVertexJob
			{
				joinIndices = workData.vertexJoinIndices,
				localNormals = vmesh.localNormals.GetNativeArray(),
				boneWeights = vmesh.boneWeights.GetNativeArray()
			}, vmesh.VertexCount);
		}
	}
}
