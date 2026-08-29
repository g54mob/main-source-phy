using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace MagicaCloth2
{
	public class ShapeDistanceReduction : StepReductionBase
	{
		[BurstCompile]
		private struct SearchJoinEdgeJob : IJob
		{
			public int vcnt;

			public float radius;

			public bool dontMakeLine;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<int> joinIndices;

			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

			public NativeList<JoinEdge> joinEdgeList;

			public void Execute()
			{
				for (int i = 0; i < vcnt; i++)
				{
					if (joinIndices[i] >= 0)
					{
						continue;
					}
					int num = vertexToVertexMap.CountValuesForKey((ushort)i);
					if (num == 0)
					{
						continue;
					}
					float3 x = localPositions[i];
					float num2 = math.max(num - 1, 1);
					float num3 = float.MaxValue;
					int num4 = -1;
					foreach (ushort item in vertexToVertexMap.GetValuesForKey((ushort)i))
					{
						float3 y = localPositions[item];
						float num5 = math.distance(x, y);
						if (num5 > radius)
						{
							continue;
						}
						float num6 = math.max(vertexToVertexMap.CountValuesForKey(item) - 1, 1);
						if (StepReductionBase.CheckJoin2(in vertexToVertexMap, i, item, dontMakeLine))
						{
							float num7 = num5 * (1f + (num2 + num6) / 2f);
							if (num7 < num3)
							{
								num3 = num7;
								num4 = item;
							}
						}
					}
					if (num4 >= 0)
					{
						JoinEdge value = new JoinEdge
						{
							vertexPair = new int2(i, num4),
							cost = num3
						};
						joinEdgeList.Add(in value);
					}
				}
			}
		}

		public ShapeDistanceReduction(string name, VirtualMesh mesh, ReductionWorkData workingData, float startMergeLength, float endMergeLength, int maxStep, bool dontMakeLine, float joinPositionAdjustment)
			: base("ShapeReduction [" + name + "]", mesh, workingData, startMergeLength, endMergeLength, maxStep, dontMakeLine, joinPositionAdjustment)
		{
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		protected override void StepInitialize()
		{
			base.StepInitialize();
		}

		protected override void CustomReductionStep()
		{
			new SearchJoinEdgeJob
			{
				vcnt = vmesh.VertexCount,
				radius = nowMergeLength,
				dontMakeLine = dontMakeLine,
				localPositions = vmesh.localPositions.GetNativeArray(),
				joinIndices = workData.vertexJoinIndices,
				vertexToVertexMap = workData.vertexToVertexMap,
				joinEdgeList = joinEdgeList
			}.Run();
		}
	}
}
