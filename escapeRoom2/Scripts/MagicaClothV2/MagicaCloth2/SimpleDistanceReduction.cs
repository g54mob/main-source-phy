using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace MagicaCloth2
{
	public class SimpleDistanceReduction : StepReductionBase
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
		private struct SearchJoinEdgeJob : IJob
		{
			public int vcnt;

			public float gridSize;

			public float radius;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<int> joinIndices;

			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			[WriteOnly]
			public NativeList<JoinEdge> joinEdgeList;

			public void Execute()
			{
				for (int i = 0; i < vcnt; i++)
				{
					if (joinIndices[i] >= 0)
					{
						continue;
					}
					float3 float5 = localPositions[i];
					float num = math.max(vertexToVertexMap.CountValuesForKey((ushort)i) - 1, 1);
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
								float num2 = math.distance(float5, y);
								if (!(num2 > radius))
								{
									float num3 = math.max(vertexToVertexMap.CountValuesForKey((ushort)item2) - 1, 1);
									float cost = num2 * (1f + (num + num3) / 2f);
									JoinEdge value = new JoinEdge
									{
										vertexPair = new int2(i, item2),
										cost = cost
									};
									joinEdgeList.Add(in value);
								}
							}
						}
					}
				}
			}
		}

		private GridMap<int> gridMap;

		public SimpleDistanceReduction(string name, VirtualMesh mesh, ReductionWorkData workingData, float startMergeLength, float endMergeLength, int maxStep, bool dontMakeLine, float joinPositionAdjustment)
			: base("SimpleDistanceReduction [" + name + "]", mesh, workingData, startMergeLength, endMergeLength, maxStep, dontMakeLine, joinPositionAdjustment)
		{
		}

		public override void Dispose()
		{
			base.Dispose();
			gridMap.Dispose();
		}

		protected override void StepInitialize()
		{
			base.StepInitialize();
			gridMap = new GridMap<int>(vmesh.VertexCount);
		}

		protected override void CustomReductionStep()
		{
			float gridSize = nowMergeLength * 2f;
			gridMap.GetMultiHashMap().Clear();
			new InitGridJob
			{
				vcnt = vmesh.VertexCount,
				gridSize = gridSize,
				localPositions = vmesh.localPositions.GetNativeArray(),
				joinIndices = workData.vertexJoinIndices,
				gridMap = gridMap.GetMultiHashMap()
			}.Run();
			new SearchJoinEdgeJob
			{
				vcnt = vmesh.VertexCount,
				gridSize = gridSize,
				radius = nowMergeLength,
				localPositions = vmesh.localPositions.GetNativeArray(),
				joinIndices = workData.vertexJoinIndices,
				vertexToVertexMap = workData.vertexToVertexMap,
				gridMap = gridMap.GetMultiHashMap(),
				joinEdgeList = joinEdgeList
			}.Run();
		}
	}
}
