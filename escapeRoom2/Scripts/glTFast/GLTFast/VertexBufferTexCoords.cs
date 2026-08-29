using GLTFast.Jobs;
using GLTFast.Logging;
using GLTFast.Schema;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace GLTFast
{
	internal class VertexBufferTexCoords<T> : VertexBufferTexCoordsBase where T : struct
	{
		private NativeArray<T> m_Data;

		public VertexBufferTexCoords(int uvSetCount, int vertexCount, ICodeLogger logger)
			: base(logger)
		{
			base.UVSetCount = uvSetCount;
			m_Data = new NativeArray<T>(vertexCount, Allocator.Persistent);
		}

		public unsafe override bool ScheduleVertexUVJobs(int offset, int[] uvAccessorIndices, NativeSlice<JobHandle> handles, IGltfBuffers buffers)
		{
			byte* unsafeReadOnlyPtr = (byte*)m_Data.GetUnsafeReadOnlyPtr();
			int num = base.UVSetCount * sizeof(float2);
			for (int i = 0; i < base.UVSetCount; i++)
			{
				int index = uvAccessorIndices[i];
				buffers.GetAccessorAndData(index, out var accessor, out var data, out var byteStride);
				if (accessor.IsSparse)
				{
					m_Logger?.Error(LogCode.SparseAccessor, "UVs");
					return false;
				}
				JobHandle? uvsJob = GetUvsJob(data, accessor.count, accessor.componentType, byteStride, (float2*)(unsafeReadOnlyPtr + num * offset + i * sizeof(float2)), num, accessor.normalized);
				if (uvsJob.HasValue)
				{
					handles[i] = uvsJob.Value;
					continue;
				}
				return false;
			}
			return true;
		}

		public override void AddDescriptors(VertexAttributeDescriptor[] dst, ref int offset, int stream)
		{
			for (int i = 0; i < base.UVSetCount; i++)
			{
				VertexAttribute attribute = (VertexAttribute)(4 + i);
				dst[offset] = new VertexAttributeDescriptor(attribute, VertexAttributeFormat.Float32, 2, stream);
				offset++;
			}
		}

		public override void ApplyOnMesh(UnityEngine.Mesh msh, int stream, MeshUpdateFlags flags = MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers | MeshUpdateFlags.DontRecalculateBounds)
		{
			msh.SetVertexBufferData(m_Data, 0, 0, m_Data.Length, stream, flags);
		}

		protected override void Dispose(bool disposing)
		{
			if (m_Data.IsCreated)
			{
				m_Data.Dispose();
			}
		}

		private unsafe JobHandle? GetUvsJob(void* input, int count, GltfComponentType inputType, int inputByteStride, float2* output, int outputByteStride, bool normalized = false)
		{
			JobHandle? result = null;
			switch (inputType)
			{
			case GltfComponentType.Float:
			{
				ConvertUVsFloatToFloatInterleavedJob jobData7 = new ConvertUVsFloatToFloatInterleavedJob
				{
					inputByteStride = ((inputByteStride > 0) ? inputByteStride : sizeof(float2)),
					input = (byte*)input,
					outputByteStride = outputByteStride,
					result = output
				};
				result = jobData7.ScheduleBatch(count, 512);
				break;
			}
			case GltfComponentType.UnsignedByte:
				if (normalized)
				{
					ConvertUVsUInt8ToFloatInterleavedNormalizedJob jobData5 = new ConvertUVsUInt8ToFloatInterleavedNormalizedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 2),
						input = (byte*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = IJobParallelForExtensions.Schedule(jobData5, count, 512);
				}
				else
				{
					ConvertUVsUInt8ToFloatInterleavedJob jobData6 = new ConvertUVsUInt8ToFloatInterleavedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 2),
						input = (byte*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData6.ScheduleBatch(count, 512);
				}
				break;
			case GltfComponentType.UnsignedShort:
				if (normalized)
				{
					ConvertUVsUInt16ToFloatInterleavedNormalizedJob jobData = new ConvertUVsUInt16ToFloatInterleavedNormalizedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 4),
						input = (byte*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = IJobParallelForExtensions.Schedule(jobData, count, 512);
				}
				else
				{
					ConvertUVsUInt16ToFloatInterleavedJob jobData2 = new ConvertUVsUInt16ToFloatInterleavedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 4),
						input = (byte*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData2.ScheduleBatch(count, 512);
				}
				break;
			case GltfComponentType.Short:
				if (normalized)
				{
					ConvertUVsInt16ToFloatInterleavedNormalizedJob jobData8 = new ConvertUVsInt16ToFloatInterleavedNormalizedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 4),
						input = (short*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData8.ScheduleBatch(count, 512);
				}
				else
				{
					ConvertUVsInt16ToFloatInterleavedJob jobData9 = new ConvertUVsInt16ToFloatInterleavedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 4),
						input = (short*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData9.ScheduleBatch(count, 512);
				}
				break;
			case GltfComponentType.Byte:
				if (normalized)
				{
					ConvertUVsInt8ToFloatInterleavedNormalizedJob jobData3 = new ConvertUVsInt8ToFloatInterleavedNormalizedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 2),
						input = (sbyte*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData3.ScheduleBatch(count, 512);
				}
				else
				{
					ConvertUVsInt8ToFloatInterleavedJob jobData4 = new ConvertUVsInt8ToFloatInterleavedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 2),
						input = (sbyte*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData4.ScheduleBatch(count, 512);
				}
				break;
			default:
				m_Logger?.Error(LogCode.TypeUnsupported, "UV", inputType.ToString());
				break;
			}
			return result;
		}
	}
}
