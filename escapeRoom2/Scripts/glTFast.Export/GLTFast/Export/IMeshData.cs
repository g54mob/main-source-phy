using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace GLTFast.Export
{
	internal interface IMeshData
	{
		int subMeshCount { get; }

		MeshTopology GetTopology(int subMesh);

		int GetIndexCount(int subMesh);

		Task<NativeArray<byte>> GetVertexData(int stream);
	}
	internal interface IMeshData<TIndex> : IMeshData where TIndex : struct
	{
		Task<NativeArray<TIndex>> GetIndexData();
	}
}
