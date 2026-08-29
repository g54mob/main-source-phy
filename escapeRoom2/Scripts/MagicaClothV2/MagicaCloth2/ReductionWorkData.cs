using System;
using Unity.Collections;
using Unity.Mathematics;

namespace MagicaCloth2
{
	public class ReductionWorkData : IDisposable
	{
		public VirtualMesh vmesh;

		public NativeArray<int> vertexJoinIndices;

		public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

		public NativeArray<int> vertexRemapIndices;

		public NativeParallelHashMap<int, int> useSkinBoneMap;

		public NativeParallelMultiHashMap<ushort, ushort> newVertexToVertexMap;

		public NativeParallelHashSet<int2> edgeSet;

		public NativeParallelHashSet<int3> triangleSet;

		public int oldVertexCount;

		public int newVertexCount;

		public int removeVertexCount;

		public ExSimpleNativeArray<VertexAttribute> newAttributes;

		public ExSimpleNativeArray<float3> newLocalPositions;

		public ExSimpleNativeArray<float3> newLocalNormals;

		public ExSimpleNativeArray<float3> newLocalTangents;

		public ExSimpleNativeArray<float2> newUv;

		public ExSimpleNativeArray<VirtualMeshBoneWeight> newBoneWeights;

		public NativeReference<int> newSkinBoneCount;

		public NativeList<int> newSkinBoneTransformIndices;

		public NativeList<float4x4> newSkinBoneBindPoseList;

		public NativeList<int2> newLineList;

		public NativeList<int3> newTriangleList;

		public ReductionWorkData(VirtualMesh vmesh)
		{
			this.vmesh = vmesh;
		}

		public void Dispose()
		{
			if (vertexJoinIndices.IsCreated)
			{
				vertexJoinIndices.Dispose();
			}
			if (vertexToVertexMap.IsCreated)
			{
				vertexToVertexMap.Dispose();
			}
			if (vertexRemapIndices.IsCreated)
			{
				vertexRemapIndices.Dispose();
			}
			if (useSkinBoneMap.IsCreated)
			{
				useSkinBoneMap.Dispose();
			}
			if (newVertexToVertexMap.IsCreated)
			{
				newVertexToVertexMap.Dispose();
			}
			if (edgeSet.IsCreated)
			{
				edgeSet.Dispose();
			}
			if (triangleSet.IsCreated)
			{
				triangleSet.Dispose();
			}
			newAttributes?.Dispose();
			newLocalPositions?.Dispose();
			newLocalNormals?.Dispose();
			newLocalTangents?.Dispose();
			newUv?.Dispose();
			newBoneWeights?.Dispose();
			if (newSkinBoneCount.IsCreated)
			{
				newSkinBoneCount.Dispose();
			}
			if (newSkinBoneTransformIndices.IsCreated)
			{
				newSkinBoneTransformIndices.Dispose();
			}
			if (newSkinBoneBindPoseList.IsCreated)
			{
				newSkinBoneBindPoseList.Dispose();
			}
			if (newLineList.IsCreated)
			{
				newLineList.Dispose();
			}
			if (newTriangleList.IsCreated)
			{
				newTriangleList.Dispose();
			}
		}
	}
}
