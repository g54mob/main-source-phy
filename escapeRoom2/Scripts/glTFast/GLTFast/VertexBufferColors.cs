using System;
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
	internal sealed class VertexBufferColors : IDisposable
	{
		private readonly ICodeLogger m_Logger;

		private NativeArray<float4> m_Data;

		public VertexBufferColors(int vertexCount, ICodeLogger logger)
		{
			m_Logger = logger;
			m_Data = new NativeArray<float4>(vertexCount, Allocator.Persistent);
		}

		public unsafe bool ScheduleVertexColorJob(int colorAccessorIndex, int offset, NativeSlice<JobHandle> handles, IGltfBuffers buffers)
		{
			buffers.GetAccessorAndData(colorAccessorIndex, out var accessor, out var data, out var byteStride);
			if (accessor.IsSparse)
			{
				m_Logger?.Error(LogCode.SparseAccessor, "color");
			}
			NativeArray<float4> subArray = m_Data.GetSubArray(offset, accessor.count);
			JobHandle? colors32Job = GetColors32Job(data, accessor.componentType, accessor.GetAttributeType(), byteStride, subArray);
			if (colors32Job.HasValue)
			{
				handles[0] = colors32Job.Value;
				return true;
			}
			return false;
		}

		public void AddDescriptors(VertexAttributeDescriptor[] dst, int offset, int stream)
		{
			dst[offset] = new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.Float32, 4, stream);
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

		private unsafe JobHandle? GetColors32Job(void* input, GltfComponentType inputType, GltfAccessorAttributeType attributeType, int inputByteStride, NativeArray<float4> output)
		{
			JobHandle? result = null;
			switch (attributeType)
			{
			case GltfAccessorAttributeType.VEC3:
				switch (inputType)
				{
				case GltfComponentType.UnsignedByte:
				{
					ConvertColorsRgbUInt8ToRGBAFloatJob jobData = new ConvertColorsRgbUInt8ToRGBAFloatJob
					{
						input = (byte*)input,
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 3),
						result = output
					};
					result = IJobParallelForExtensions.Schedule(jobData, output.Length, 512);
					break;
				}
				case GltfComponentType.Float:
				{
					ConvertColorsRGBFloatToRGBAFloatJob jobData3 = new ConvertColorsRGBFloatToRGBAFloatJob
					{
						input = (byte*)input,
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 12),
						result = output
					};
					result = IJobParallelForExtensions.Schedule(jobData3, output.Length, 512);
					break;
				}
				case GltfComponentType.UnsignedShort:
				{
					ConvertColorsRgbUInt16ToRGBAFloatJob jobData2 = new ConvertColorsRgbUInt16ToRGBAFloatJob
					{
						input = (ushort*)input,
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 6),
						result = output
					};
					result = IJobParallelForExtensions.Schedule(jobData2, output.Length, 512);
					break;
				}
				default:
					m_Logger?.Error(LogCode.ColorFormatUnsupported, attributeType.ToString());
					break;
				}
				break;
			case GltfAccessorAttributeType.VEC4:
				switch (inputType)
				{
				case GltfComponentType.UnsignedByte:
				{
					ConvertColorsRgbaUInt8ToRGBAFloatJob jobData4 = new ConvertColorsRgbaUInt8ToRGBAFloatJob
					{
						input = (byte*)input,
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 4),
						result = output
					};
					result = IJobParallelForExtensions.Schedule(jobData4, output.Length, 512);
					break;
				}
				case GltfComponentType.Float:
					if (inputByteStride == 16 || inputByteStride <= 0)
					{
						MemCopyJob jobData6 = new MemCopyJob
						{
							bufferSize = output.Length * 16,
							input = input,
							result = output.GetUnsafeReadOnlyPtr()
						};
						result = jobData6.Schedule();
					}
					else
					{
						ConvertColorsRGBAFloatToRGBAFloatJob jobData7 = new ConvertColorsRGBAFloatToRGBAFloatJob
						{
							input = (byte*)input,
							inputByteStride = inputByteStride,
							result = output
						};
						result = jobData7.ScheduleBatch(output.Length, 512);
					}
					break;
				case GltfComponentType.UnsignedShort:
				{
					ConvertColorsRgbaUInt16ToRGBAFloatJob jobData5 = new ConvertColorsRgbaUInt16ToRGBAFloatJob
					{
						input = (ushort*)input,
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 8),
						result = output
					};
					result = jobData5.ScheduleBatch(output.Length, 512);
					break;
				}
				default:
					m_Logger?.Error(LogCode.ColorFormatUnsupported, attributeType.ToString());
					break;
				}
				break;
			default:
				m_Logger?.Error(LogCode.TypeUnsupported, "color accessor", inputType.ToString());
				break;
			}
			return result;
		}
	}
}
