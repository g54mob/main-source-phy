using System.Threading.Tasks;
using GLTFast.Schema;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace GLTFast
{
	internal class MorphTargetsGenerator
	{
		private readonly string[] m_MorphTargetNames;

		private readonly GltfImportBase m_GltfImport;

		private MorphTargetGenerator[] m_Contexts;

		private NativeArray<JobHandle> m_Handles;

		public MorphTargetsGenerator(int vertexCount, int subMeshCount, int morphTargetCount, string[] morphTargetNames, bool hasNormals, bool hasTangents, GltfImportBase gltfImport)
		{
			m_MorphTargetNames = morphTargetNames;
			m_GltfImport = gltfImport;
			m_Contexts = new MorphTargetGenerator[morphTargetCount];
			for (int i = 0; i < morphTargetCount; i++)
			{
				m_Contexts[i] = new MorphTargetGenerator(vertexCount, hasNormals, hasTangents);
			}
			m_Handles = new NativeArray<JobHandle>(morphTargetCount * subMeshCount, Allocator.Persistent);
		}

		public bool AddMorphTarget(int offset, int morphTargetIndex, MorphTarget morphTarget)
		{
			MorphTargetGenerator morphTargetGenerator = m_Contexts[morphTargetIndex];
			JobHandle? jobHandle = morphTargetGenerator.ScheduleMorphTargetJobs(morphTarget, offset, m_GltfImport);
			if (jobHandle.HasValue)
			{
				m_Handles[morphTargetIndex] = jobHandle.Value;
				m_Contexts[morphTargetIndex] = morphTargetGenerator;
				return true;
			}
			return false;
		}

		public JobHandle GetJobHandle()
		{
			JobHandle result = ((m_Contexts.Length > 1) ? JobHandle.CombineDependencies(m_Handles) : m_Handles[0]);
			m_Handles.Dispose();
			return result;
		}

		public async Task ApplyOnMeshAndDispose(UnityEngine.Mesh mesh)
		{
			for (int index = 0; index < m_Contexts.Length; index++)
			{
				MorphTargetGenerator obj = m_Contexts[index];
				string[] morphTargetNames = m_MorphTargetNames;
				obj.AddToMesh(mesh, ((morphTargetNames != null) ? morphTargetNames[index] : null) ?? index.ToString());
				obj.Dispose();
				await m_GltfImport.DeferAgent.BreakPoint();
			}
			m_Contexts = null;
		}
	}
}
