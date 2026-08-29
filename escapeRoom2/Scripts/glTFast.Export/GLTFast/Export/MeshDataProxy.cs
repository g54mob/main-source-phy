using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace GLTFast.Export
{
	internal class MeshDataProxy<TIndex> : IMeshData<TIndex>, IMeshData where TIndex : struct
	{
		private Mesh.MeshData m_MeshData;

		public int subMeshCount => m_MeshData.subMeshCount;

		public MeshDataProxy(Mesh.MeshData meshData)
		{
			m_MeshData = meshData;
		}

		public MeshTopology GetTopology(int subMesh)
		{
			return m_MeshData.GetSubMesh(subMesh).topology;
		}

		public int GetIndexCount(int subMesh)
		{
			return m_MeshData.GetSubMesh(subMesh).indexCount;
		}

		public Task<NativeArray<TIndex>> GetIndexData()
		{
			return Task.FromResult(m_MeshData.GetIndexData<TIndex>());
		}

		public Task<NativeArray<byte>> GetVertexData(int stream)
		{
			return Task.FromResult(m_MeshData.GetVertexData<byte>(stream));
		}
	}
}
