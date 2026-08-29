using System;
using GLTFast.Jobs;
using GLTFast.Logging;
using GLTFast.Schema;
using GLTFast.Vertex;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace GLTFast
{
	internal sealed class VertexBufferBones : IDisposable
	{
		private readonly ICodeLogger m_Logger;

		private NativeArray<VBones> m_Data;

		public VertexBufferBones(int vertexCount, ICodeLogger logger)
		{
			m_Logger = logger;
			m_Data = new NativeArray<VBones>(vertexCount, Allocator.Persistent);
		}

		public unsafe JobHandle? ScheduleVertexBonesJob(int weightsAccessorIndex, int jointsAccessorIndex, int offset, IGltfBuffers buffers)
		{
			buffers.GetAccessorAndData(weightsAccessorIndex, out var accessor, out var data, out var byteStride);
			if (accessor.IsSparse)
			{
				m_Logger?.Error(LogCode.SparseAccessor, "bone weights");
			}
			byte* unsafeReadOnlyPtr = (byte*)m_Data.GetUnsafeReadOnlyPtr();
			JobHandle? weightsJob = GetWeightsJob(data, accessor.count, accessor.componentType, byteStride, (float4*)(unsafeReadOnlyPtr + offset * sizeof(VBones)), 32);
			if (weightsJob.HasValue)
			{
				JobHandle value = weightsJob.Value;
				buffers.GetAccessorAndData(jointsAccessorIndex, out var accessor2, out var data2, out var byteStride2);
				if (accessor2.IsSparse)
				{
					m_Logger?.Error(LogCode.SparseAccessor, "bone joints");
				}
				JobHandle? jointsJob = GetJointsJob(data2, accessor2.count, accessor2.componentType, byteStride2, (uint4*)(unsafeReadOnlyPtr + offset * sizeof(VBones) + sizeof(float4)), 32, m_Logger);
				if (jointsJob.HasValue)
				{
					JobHandle value2 = jointsJob.Value;
					JobHandle jobHandle = JobHandle.CombineDependencies(value, value2);
					int skinWeights = (int)QualitySettings.skinWeights;
					if (skinWeights < 4)
					{
						jobHandle = IJobParallelForExtensions.Schedule(new SortAndNormalizeBoneWeightsJob
						{
							bones = m_Data,
							skinWeights = math.max(1, skinWeights)
						}, m_Data.Length, 512, jobHandle);
					}
					return jobHandle;
				}
				return null;
			}
			return null;
		}

		public void AddDescriptors(VertexAttributeDescriptor[] dst, int offset, int stream)
		{
			dst[offset] = new VertexAttributeDescriptor(VertexAttribute.BlendWeight, VertexAttributeFormat.Float32, 4, stream);
			dst[offset + 1] = new VertexAttributeDescriptor(VertexAttribute.BlendIndices, VertexAttributeFormat.UInt32, 4, stream);
		}

		public void ApplyOnMesh(UnityEngine.Mesh msh, int stream, MeshUpdateFlags flags = MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers | MeshUpdateFlags.DontRecalculateBounds)
		{
			msh.SetVertexBufferData(m_Data, 0, 0, m_Data.Length, stream, flags);
		}

		public void Dispose()
		{
			if (m_Data.IsCreated)
			{
				m_Data.Dispose();
			}
		}

		private unsafe JobHandle? GetWeightsJob(void* input, int count, GltfComponentType inputType, int inputByteStride, float4* output, int outputByteStride)
		{
			JobHandle? result;
			switch (inputType)
			{
			case GltfComponentType.Float:
				result = new ConvertBoneWeightsFloatToFloatInterleavedJob
				{
					inputByteStride = ((inputByteStride > 0) ? inputByteStride : 16),
					input = (byte*)input,
					outputByteStride = outputByteStride,
					result = output
				}.ScheduleBatch(count, 512);
				break;
			case GltfComponentType.UnsignedShort:
			{
				ConvertBoneWeightsUInt16ToFloatInterleavedJob jobData = new ConvertBoneWeightsUInt16ToFloatInterleavedJob
				{
					inputByteStride = ((inputByteStride > 0) ? inputByteStride : 8),
					input = (byte*)input,
					outputByteStride = outputByteStride,
					result = output
				};
				result = jobData.ScheduleBatch(count, 512);
				break;
			}
			case GltfComponentType.UnsignedByte:
			{
				ConvertBoneWeightsUInt8ToFloatInterleavedJob jobData2 = new ConvertBoneWeightsUInt8ToFloatInterleavedJob
				{
					inputByteStride = ((inputByteStride > 0) ? inputByteStride : 4),
					input = (byte*)input,
					outputByteStride = outputByteStride,
					result = output
				};
				result = jobData2.ScheduleBatch(count, 512);
				break;
			}
			default:
				m_Logger?.Error(LogCode.TypeUnsupported, "Weights", inputType.ToString());
				result = null;
				break;
			}
			return result;
		}

		private unsafe static JobHandle? GetJointsJob(void* input, int count, GltfComponentType inputType, int inputByteStride, uint4* output, int outputByteStride, ICodeLogger logger)
		{
			JobHandle? result;
			switch (inputType)
			{
			case GltfComponentType.UnsignedByte:
				result = IJobParallelForExtensions.Schedule(new ConvertBoneJointsUInt8ToUInt32Job
				{
					inputByteStride = ((inputByteStride > 0) ? inputByteStride : 4),
					input = (byte*)input,
					outputByteStride = outputByteStride,
					result = output
				}, count, 512);
				break;
			case GltfComponentType.UnsignedShort:
				result = IJobParallelForExtensions.Schedule(new ConvertBoneJointsUInt16ToUInt32Job
				{
					inputByteStride = ((inputByteStride > 0) ? inputByteStride : 8),
					input = (byte*)input,
					outputByteStride = outputByteStride,
					result = output
				}, count, 512);
				break;
			default:
				logger?.Error(LogCode.TypeUnsupported, "Joints", inputType.ToString());
				result = null;
				break;
			}
			return result;
		}
	}
}
