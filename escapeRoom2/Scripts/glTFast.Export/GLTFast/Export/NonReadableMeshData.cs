using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace GLTFast.Export
{
	internal class NonReadableMeshData<TIndex> : IMeshData<TIndex>, IMeshData where TIndex : struct
	{
		private Mesh m_Mesh;

		private NativeArray<TIndex> m_IndexData;

		private NativeArray<byte>[] m_VertexData;

		public int subMeshCount => m_Mesh.subMeshCount;

		public NonReadableMeshData(Mesh mesh)
		{
			m_Mesh = mesh;
		}

		public MeshTopology GetTopology(int subMesh)
		{
			return m_Mesh.GetTopology(subMesh);
		}

		public int GetIndexCount(int subMesh)
		{
			return (int)m_Mesh.GetIndexCount(subMesh);
		}

		public async Task<NativeArray<TIndex>> GetIndexData()
		{
			if (!m_IndexData.IsCreated)
			{
				using GraphicsBuffer indexBuffer = m_Mesh.GetIndexBuffer();
				m_IndexData = new NativeArray<TIndex>(indexBuffer.count, Allocator.Persistent);
				await AsyncGPUReadback.RequestIntoNativeArrayAsync(ref m_IndexData, indexBuffer);
			}
			return m_IndexData;
		}

		public async Task<NativeArray<byte>> GetVertexData(int stream)
		{
			if (m_VertexData == null)
			{
				m_VertexData = new NativeArray<byte>[4];
			}
			if (!m_VertexData[stream].IsCreated)
			{
				using GraphicsBuffer vertexBuffer = m_Mesh.GetVertexBuffer(stream);
				m_VertexData[stream] = new NativeArray<byte>(vertexBuffer.count * vertexBuffer.stride, Allocator.Persistent);
				await AsyncGPUReadback.RequestIntoNativeArrayAsync(ref m_VertexData[stream], vertexBuffer);
			}
			return m_VertexData[stream];
		}
	}
}
