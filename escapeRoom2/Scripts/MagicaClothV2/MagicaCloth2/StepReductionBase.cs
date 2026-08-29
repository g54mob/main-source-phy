using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public abstract class StepReductionBase : IDisposable
	{
		public struct JoinEdge : IComparable<JoinEdge>
		{
			public int2 vertexPair;

			public float cost;

			public bool Contains(in int2 pair)
			{
				if (vertexPair.x == pair.x || vertexPair.x == pair.y || vertexPair.y == pair.x || vertexPair.y == pair.y)
				{
					return true;
				}
				return false;
			}

			public int CompareTo(JoinEdge other)
			{
				if (cost != other.cost)
				{
					if (!(cost < other.cost))
					{
						return 1;
					}
					return -1;
				}
				return 0;
			}
		}

		[BurstCompile]
		private struct DeterminJoinEdgeJob : IJob
		{
			public int stepIndex;

			public float mergeLength;

			[ReadOnly]
			public NativeList<JoinEdge> joinEdgeList;

			public NativeParallelHashSet<int> completeVertexSet;

			public NativeList<int2> removePairList;

			public NativeArray<int> resultArray;

			public void Execute()
			{
				completeVertexSet.Clear();
				removePairList.Clear();
				int num = 0;
				for (int i = 0; i < joinEdgeList.Length; i++)
				{
					int2 vertexPair = joinEdgeList[i].vertexPair;
					if (!completeVertexSet.Contains(vertexPair.x) && !completeVertexSet.Contains(vertexPair.y))
					{
						removePairList.Add(vertexPair.xy);
						completeVertexSet.Add(vertexPair.x);
						completeVertexSet.Add(vertexPair.y);
						num++;
					}
				}
				resultArray[stepIndex] = num;
			}
		}

		[BurstCompile]
		private struct JoinPairJob : IJob
		{
			public float joinPositionAdjustment;

			[ReadOnly]
			public NativeList<int2> removePairList;

			public NativeArray<float3> localPositions;

			public NativeArray<float3> localNormals;

			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			public NativeArray<VertexAttribute> attributes;

			public NativeArray<int> joinIndices;

			public void Execute()
			{
				for (int i = 0; i < removePairList.Length; i++)
				{
					int2 obj = removePairList[i];
					int x = obj.x;
					int y = obj.y;
					float3 end = localPositions[x];
					float3 start = localPositions[y];
					float3 float5 = localNormals[x];
					float3 float6 = localNormals[y];
					joinIndices[x] = y;
					float num = math.max(vertexToVertexMap.CountValuesForKey((ushort)x) - 1, 1);
					float num2 = math.max(vertexToVertexMap.CountValuesForKey((ushort)y) - 1, 1);
					float start2 = num2 / (num + num2);
					start2 = math.lerp(start2, 0.5f, joinPositionAdjustment);
					float3 value = math.lerp(start, end, start2);
					localPositions[y] = value;
					localNormals[y] = float5 + float6;
					FixedList512Bytes<ushort> fixedList = default(FixedList512Bytes<ushort>);
					foreach (ushort item in vertexToVertexMap.GetValuesForKey((ushort)x))
					{
						if (item != x && item != y)
						{
							fixedList.MC2Set(item);
						}
					}
					foreach (ushort item2 in vertexToVertexMap.GetValuesForKey((ushort)y))
					{
						if (item2 != x && item2 != y)
						{
							fixedList.MC2Set(item2);
						}
					}
					vertexToVertexMap.Remove((ushort)y);
					for (int j = 0; j < fixedList.Length; j++)
					{
						vertexToVertexMap.Add((ushort)y, fixedList[j]);
					}
					VirtualMeshBoneWeight value2 = boneWeights[y];
					value2.AddWeight(boneWeights[x]);
					boneWeights[y] = value2;
					VertexAttribute attr = attributes[x];
					VertexAttribute attr2 = attributes[y];
					attributes[y] = VertexAttribute.JoinAttribute(attr, attr2);
					attributes[x] = VertexAttribute.Invalid;
				}
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

		protected string name = string.Empty;

		protected VirtualMesh vmesh;

		protected ReductionWorkData workData;

		protected ResultCode result;

		protected float startMergeLength;

		protected float endMergeLength;

		protected int maxStep;

		protected bool dontMakeLine;

		protected float joinPositionAdjustment;

		protected int nowStepIndex;

		protected float nowMergeLength;

		protected float nowStepScale;

		protected NativeList<JoinEdge> joinEdgeList;

		private NativeParallelHashSet<int> completeVertexSet;

		private NativeList<int2> removePairList;

		private NativeArray<int> resultArray;

		public ResultCode Result => result;

		public StepReductionBase()
		{
		}

		public StepReductionBase(string name, VirtualMesh mesh, ReductionWorkData workingData, float startMergeLength, float endMergeLength, int maxStep, bool dontMakeLine, float joinPositionAdjustment)
		{
			this.name = name;
			vmesh = mesh;
			workData = workingData;
			result = ResultCode.None;
			this.startMergeLength = math.max(startMergeLength, 1E-09f);
			this.endMergeLength = math.max(endMergeLength, 1E-09f);
			this.maxStep = math.min(maxStep, 100);
			this.dontMakeLine = dontMakeLine;
			this.joinPositionAdjustment = joinPositionAdjustment;
		}

		public virtual void Dispose()
		{
			if (joinEdgeList.IsCreated)
			{
				joinEdgeList.Dispose();
			}
			if (completeVertexSet.IsCreated)
			{
				completeVertexSet.Dispose();
			}
			if (removePairList.IsCreated)
			{
				removePairList.Dispose();
			}
			if (resultArray.IsCreated)
			{
				resultArray.Dispose();
			}
		}

		public ResultCode Reduction()
		{
			result.Clear();
			try
			{
				StepInitialize();
				InitStep();
				while (nowStepIndex < maxStep)
				{
					ReductionStep();
					nowStepIndex++;
					if (IsEndStep())
					{
						break;
					}
					NextStep();
				}
				if (nowStepIndex < maxStep)
				{
					ReductionStep();
					nowStepIndex++;
				}
				UpdateReductionResultJob();
				int num = 0;
				for (int i = 0; i < nowStepIndex; i++)
				{
					num += resultArray[i];
				}
				workData.removeVertexCount += num;
				result.SetSuccess();
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				if (!result.IsError())
				{
					if (this is SimpleDistanceReduction)
					{
						result.SetError(Define.Result.Reduction_SimpleDistanceException);
					}
					else if (this is ShapeDistanceReduction)
					{
						result.SetError(Define.Result.Reduction_ShapeDistanceException);
					}
					else
					{
						result.SetError(Define.Result.Reduction_Exception);
					}
				}
			}
			return result;
		}

		private void InitStep()
		{
			nowStepIndex = 0;
			nowMergeLength = startMergeLength;
			nowStepScale = 2f;
		}

		private bool IsEndStep()
		{
			return nowMergeLength == endMergeLength;
		}

		private void NextStep()
		{
			nowStepScale = math.max(nowStepScale * 0.93f, 1.1f);
			nowMergeLength = math.min(nowMergeLength * nowStepScale, endMergeLength);
		}

		private void ReductionStep()
		{
			PreReductionStep();
			CustomReductionStep();
			PostReductionStep();
		}

		protected virtual void StepInitialize()
		{
			int vertexCount = vmesh.VertexCount;
			joinEdgeList = new NativeList<JoinEdge>(vertexCount / 4, Allocator.Persistent);
			completeVertexSet = new NativeParallelHashSet<int>(vertexCount, Allocator.Persistent);
			removePairList = new NativeList<int2>(vertexCount, Allocator.Persistent);
			resultArray = new NativeArray<int>(maxStep, Allocator.Persistent);
		}

		protected virtual void CustomReductionStep()
		{
		}

		private void PreReductionStep()
		{
			joinEdgeList.Clear();
		}

		private void PostReductionStep()
		{
			SortJoinEdge();
			DetermineJoinEdge();
			RunJoinEdge();
			UpdateJoinAndLink();
		}

		private void SortJoinEdge()
		{
			joinEdgeList.Sort();
		}

		private void DetermineJoinEdge()
		{
			new DeterminJoinEdgeJob
			{
				stepIndex = nowStepIndex,
				mergeLength = nowMergeLength,
				joinEdgeList = joinEdgeList,
				completeVertexSet = completeVertexSet,
				removePairList = removePairList,
				resultArray = resultArray
			}.Run();
		}

		private void RunJoinEdge()
		{
			new JoinPairJob
			{
				joinPositionAdjustment = joinPositionAdjustment,
				removePairList = removePairList,
				localPositions = vmesh.localPositions.GetNativeArray(),
				localNormals = vmesh.localNormals.GetNativeArray(),
				joinIndices = workData.vertexJoinIndices,
				vertexToVertexMap = workData.vertexToVertexMap,
				boneWeights = vmesh.boneWeights.GetNativeArray(),
				attributes = vmesh.attributes.GetNativeArray()
			}.Run();
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

		protected static bool CheckJoin2(in NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap, int vindex, int tvindex, bool dontMakeLine)
		{
			FixedList512Bytes<ushort> fixedList = default(FixedList512Bytes<ushort>);
			foreach (ushort item in vertexToVertexMap.GetValuesForKey((ushort)vindex))
			{
				if (item != vindex && item != tvindex)
				{
					fixedList.MC2Set(item);
				}
			}
			foreach (ushort item2 in vertexToVertexMap.GetValuesForKey((ushort)tvindex))
			{
				if (item2 != vindex && item2 != tvindex)
				{
					fixedList.MC2Set(item2);
				}
			}
			if (fixedList.Length == 0)
			{
				return false;
			}
			if (dontMakeLine)
			{
				FixedList512Bytes<ushort> fixedList2 = default(FixedList512Bytes<ushort>);
				fixedList2.MC2Push(fixedList[0]);
				while (fixedList2.Length > 0)
				{
					ushort num = FixedList512BytesExtensions.MC2Pop(ref fixedList2);
					if (!Unity.Collections.FixedList512BytesExtensions.Contains(ref fixedList, num))
					{
						continue;
					}
					fixedList.MC2RemoveItemAtSwapBack(num);
					foreach (ushort item3 in vertexToVertexMap.GetValuesForKey(num))
					{
						if (Unity.Collections.FixedList512BytesExtensions.Contains(ref fixedList, item3))
						{
							fixedList2.MC2Push(item3);
						}
					}
				}
				if (fixedList.Length > 0)
				{
					return false;
				}
			}
			return true;
		}

		protected static bool CheckJoin(in NativeArray<FixedList128Bytes<ushort>> vertexToVertexArray, int vindex, int tvindex, in FixedList128Bytes<ushort> vlist, in FixedList128Bytes<ushort> tvlist, bool dontMakeLine)
		{
			FixedList128Bytes<ushort> fixedList = default(FixedList128Bytes<ushort>);
			for (int i = 0; i < vlist.Length; i++)
			{
				int num = vlist[i];
				if (num != vindex && num != tvindex)
				{
					fixedList.MC2SetLimit((ushort)num);
				}
			}
			for (int j = 0; j < tvlist.Length; j++)
			{
				int num2 = tvlist[j];
				if (num2 != vindex && num2 != tvindex)
				{
					fixedList.MC2SetLimit((ushort)num2);
				}
			}
			if (fixedList.Length == 0)
			{
				return false;
			}
			if (dontMakeLine)
			{
				FixedList512Bytes<ushort> fixedList2 = default(FixedList512Bytes<ushort>);
				fixedList2.MC2Push(fixedList[0]);
				while (fixedList2.Length > 0)
				{
					ushort num3 = FixedList512BytesExtensions.MC2Pop(ref fixedList2);
					if (!Unity.Collections.FixedList128BytesExtensions.Contains(ref fixedList, num3))
					{
						continue;
					}
					fixedList.MC2RemoveItemAtSwapBack(num3);
					FixedList128Bytes<ushort> fixedList128Bytes = vertexToVertexArray[num3];
					for (int k = 0; k < fixedList128Bytes.Length; k++)
					{
						ushort num4 = fixedList128Bytes[k];
						if (Unity.Collections.FixedList128BytesExtensions.Contains(ref fixedList, num4))
						{
							fixedList2.MC2Push(num4);
						}
					}
				}
				if (fixedList.Length > 0)
				{
					return false;
				}
			}
			return true;
		}
	}
}
