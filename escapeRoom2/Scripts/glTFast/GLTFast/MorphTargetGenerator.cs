using System;
using System.Runtime.InteropServices;
using GLTFast.Schema;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast
{
	internal sealed class MorphTargetGenerator : IDisposable
	{
		private Vector3[] m_Positions;

		private Vector3[] m_Normals;

		private Vector3[] m_Tangents;

		private GCHandle m_PositionsHandle;

		private GCHandle m_NormalsHandle;

		private GCHandle m_TangentsHandle;

		public MorphTargetGenerator(int vertexCount, bool hasNormals, bool hasTangents)
		{
			m_Positions = new Vector3[vertexCount];
			m_PositionsHandle = GCHandle.Alloc(m_Positions, GCHandleType.Pinned);
			if (hasNormals)
			{
				m_Normals = new Vector3[vertexCount];
				m_NormalsHandle = GCHandle.Alloc(m_Normals, GCHandleType.Pinned);
			}
			if (hasTangents)
			{
				m_Tangents = new Vector3[vertexCount];
				m_TangentsHandle = GCHandle.Alloc(m_Tangents, GCHandleType.Pinned);
			}
		}

		public unsafe JobHandle? ScheduleMorphTargetJobs(MorphTarget morphTarget, int offset, IGltfBuffers buffers)
		{
			buffers.GetAccessorAndData(morphTarget.POSITION, out var accessor, out var data, out var byteStride);
			int num = 1;
			if (accessor.IsSparse && accessor.bufferView >= 0)
			{
				num++;
			}
			AccessorBase accessor2 = null;
			void* data2 = null;
			int byteStride2 = 0;
			if (morphTarget.NORMAL >= 0)
			{
				buffers.GetAccessorAndData(morphTarget.NORMAL, out accessor2, out data2, out byteStride2);
				num += ((!accessor2.IsSparse || accessor2.bufferView < 0) ? 1 : 2);
			}
			AccessorBase accessor3 = null;
			void* data3 = null;
			int byteStride3 = 0;
			if (morphTarget.TANGENT >= 0)
			{
				buffers.GetAccessorAndData(morphTarget.TANGENT, out accessor3, out data3, out byteStride3);
				num += ((!accessor3.IsSparse || accessor3.bufferView < 0) ? 1 : 2);
			}
			NativeArray<JobHandle> nativeArray = new NativeArray<JobHandle>(num, Allocator.Persistent);
			int handleIndex = 0;
			if (!SchedulePositionsJobs(offset, buffers, data, accessor, byteStride, nativeArray, ref handleIndex))
			{
				return null;
			}
			if (accessor2 != null && !ScheduleNormalsJobs(offset, buffers, accessor2, data2, byteStride2, nativeArray, ref handleIndex))
			{
				return null;
			}
			if (accessor3 != null && !ScheduleTangentsJobs(offset, buffers, accessor3, data3, byteStride3, nativeArray, handleIndex))
			{
				return null;
			}
			JobHandle value = ((num > 1) ? JobHandle.CombineDependencies(nativeArray) : nativeArray[0]);
			nativeArray.Dispose();
			return value;
		}

		private unsafe bool SchedulePositionsJobs(int offset, IGltfBuffers buffers, void* posData, AccessorBase posAcc, int posByteStride, NativeArray<JobHandle> handles, ref int handleIndex)
		{
			fixed (Vector3* ptr = &m_Positions[offset])
			{
				void* output = ptr;
				JobHandle? dependsOn = null;
				if (posData != null)
				{
					dependsOn = VertexBufferGeneratorBase.GetVector3Job(posData, posAcc.count, posAcc.componentType, posByteStride, (float3*)output, 12, posAcc.normalized, ensureUnitLength: false);
					if (!dependsOn.HasValue)
					{
						return false;
					}
					handles[handleIndex] = dependsOn.Value;
					handleIndex++;
				}
				if (posAcc.IsSparse)
				{
					buffers.GetAccessorSparseIndices(posAcc.Sparse.Indices, out var data);
					buffers.GetAccessorSparseValues(posAcc.Sparse.Values, out var data2);
					JobHandle? vector3SparseJob = VertexBufferGeneratorBase.GetVector3SparseJob(data, data2, posAcc.Sparse.count, posAcc.Sparse.Indices.componentType, posAcc.componentType, (float3*)output, 12, ref dependsOn, posAcc.normalized);
					if (!vector3SparseJob.HasValue)
					{
						return false;
					}
					handles[handleIndex] = vector3SparseJob.Value;
					handleIndex++;
				}
			}
			return true;
		}

		private unsafe bool ScheduleNormalsJobs(int offset, IGltfBuffers buffers, AccessorBase nrmAcc, void* nrmInput, int nrmInputByteStride, NativeArray<JobHandle> handles, ref int handleIndex)
		{
			fixed (Vector3* ptr = &m_Normals[offset])
			{
				void* output = ptr;
				JobHandle? dependsOn = null;
				if (nrmAcc.bufferView >= 0)
				{
					dependsOn = VertexBufferGeneratorBase.GetVector3Job(nrmInput, nrmAcc.count, nrmAcc.componentType, nrmInputByteStride, (float3*)output, 12, nrmAcc.normalized, ensureUnitLength: false);
					if (!dependsOn.HasValue)
					{
						return false;
					}
					handles[handleIndex] = dependsOn.Value;
					handleIndex++;
				}
				if (nrmAcc.IsSparse)
				{
					buffers.GetAccessorSparseIndices(nrmAcc.Sparse.Indices, out var data);
					buffers.GetAccessorSparseValues(nrmAcc.Sparse.Values, out var data2);
					JobHandle? vector3SparseJob = VertexBufferGeneratorBase.GetVector3SparseJob(data, data2, nrmAcc.Sparse.count, nrmAcc.Sparse.Indices.componentType, nrmAcc.componentType, (float3*)output, 12, ref dependsOn, nrmAcc.normalized);
					if (!vector3SparseJob.HasValue)
					{
						return false;
					}
					handles[handleIndex] = vector3SparseJob.Value;
					handleIndex++;
				}
			}
			return true;
		}

		private unsafe bool ScheduleTangentsJobs(int offset, IGltfBuffers buffers, AccessorBase tanAcc, void* tanInput, int tanInputByteStride, NativeArray<JobHandle> handles, int handleIndex)
		{
			fixed (Vector3* ptr = &m_Tangents[offset])
			{
				void* output = ptr;
				JobHandle? dependsOn = null;
				if (tanAcc.bufferView >= 0)
				{
					dependsOn = VertexBufferGeneratorBase.GetVector3Job(tanInput, tanAcc.count, tanAcc.componentType, tanInputByteStride, (float3*)output, 12, tanAcc.normalized, ensureUnitLength: false);
					if (!dependsOn.HasValue)
					{
						return false;
					}
					handles[handleIndex] = dependsOn.Value;
					handleIndex++;
				}
				if (tanAcc.IsSparse)
				{
					buffers.GetAccessorSparseIndices(tanAcc.Sparse.Indices, out var data);
					buffers.GetAccessorSparseValues(tanAcc.Sparse.Values, out var data2);
					JobHandle? vector3SparseJob = VertexBufferGeneratorBase.GetVector3SparseJob(data, data2, tanAcc.Sparse.count, tanAcc.Sparse.Indices.componentType, tanAcc.componentType, (float3*)output, 12, ref dependsOn, tanAcc.normalized);
					if (!vector3SparseJob.HasValue)
					{
						return false;
					}
					handles[handleIndex] = vector3SparseJob.Value;
				}
			}
			return true;
		}

		public void AddToMesh(UnityEngine.Mesh mesh, string name)
		{
			mesh.AddBlendShapeFrame(name, 1f, m_Positions, m_Normals, m_Tangents);
		}

		public void Dispose()
		{
			m_PositionsHandle.Free();
			m_Positions = null;
			if (m_Normals != null)
			{
				m_NormalsHandle.Free();
				m_Normals = null;
			}
			if (m_Tangents != null)
			{
				m_TangentsHandle.Free();
				m_Tangents = null;
			}
		}
	}
}
