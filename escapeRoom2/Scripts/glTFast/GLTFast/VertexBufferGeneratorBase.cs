using System;
using GLTFast.Jobs;
using GLTFast.Logging;
using GLTFast.Schema;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace GLTFast
{
	internal abstract class VertexBufferGeneratorBase : IDisposable
	{
		public const Allocator defaultAllocator = Allocator.Persistent;

		protected Attributes[] m_Attributes;

		protected int m_AttributeCount;

		public bool calculateNormals;

		public bool calculateTangents;

		protected VertexAttributeDescriptor[] m_Descriptors;

		protected GltfImportBase m_GltfImport;

		public abstract int VertexCount { get; }

		public abstract int[] VertexIntervals { get; protected set; }

		protected VertexBufferGeneratorBase(int primitiveCount, GltfImportBase gltfImport)
		{
			m_Attributes = new Attributes[primitiveCount];
			m_GltfImport = gltfImport;
		}

		public abstract void AddPrimitive(Attributes att);

		public abstract void Initialize();

		public abstract JobHandle? CreateVertexBuffer();

		public abstract void ApplyOnMesh(UnityEngine.Mesh msh, MeshUpdateFlags flags = MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers | MeshUpdateFlags.DontRecalculateBounds);

		public abstract void GetVertexRange(int subMesh, out int baseVertex, out int vertexCount);

		public abstract bool TryGetBounds(int subMesh, out Bounds bounds);

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected abstract void Dispose(bool disposing);

		public unsafe static JobHandle? GetVector3Job(void* input, int count, GltfComponentType inputType, int inputByteStride, float3* output, int outputByteStride, bool normalized = false, bool ensureUnitLength = true)
		{
			JobHandle? result;
			switch (inputType)
			{
			case GltfComponentType.Float:
			{
				ConvertVector3FloatToFloatInterleavedJob jobData11 = new ConvertVector3FloatToFloatInterleavedJob
				{
					inputByteStride = ((inputByteStride > 0) ? inputByteStride : 12),
					input = (byte*)input,
					outputByteStride = outputByteStride,
					result = output
				};
				result = jobData11.ScheduleBatch(count, 512);
				break;
			}
			case GltfComponentType.UnsignedShort:
				if (normalized)
				{
					ConvertPositionsUInt16ToFloatInterleavedNormalizedJob jobData3 = new ConvertPositionsUInt16ToFloatInterleavedNormalizedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 6),
						input = (byte*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData3.ScheduleBatch(count, 512);
				}
				else
				{
					ConvertPositionsUInt16ToFloatInterleavedJob jobData4 = new ConvertPositionsUInt16ToFloatInterleavedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 6),
						input = (byte*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData4.ScheduleBatch(count, 512);
				}
				break;
			case GltfComponentType.Short:
				if (normalized)
				{
					if (ensureUnitLength)
					{
						ConvertNormalsInt16ToFloatInterleavedNormalizedJob jobData5 = new ConvertNormalsInt16ToFloatInterleavedNormalizedJob
						{
							inputByteStride = ((inputByteStride > 0) ? inputByteStride : 6),
							input = (byte*)input,
							outputByteStride = outputByteStride,
							result = output
						};
						result = jobData5.ScheduleBatch(count, 512);
					}
					else
					{
						ConvertVector3Int16ToFloatInterleavedNormalizedJob jobData6 = new ConvertVector3Int16ToFloatInterleavedNormalizedJob
						{
							inputByteStride = ((inputByteStride > 0) ? inputByteStride : 6),
							input = (byte*)input,
							outputByteStride = outputByteStride,
							result = output
						};
						result = jobData6.ScheduleBatch(count, 512);
					}
				}
				else
				{
					ConvertPositionsInt16ToFloatInterleavedJob jobData7 = new ConvertPositionsInt16ToFloatInterleavedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 6),
						input = (byte*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData7.ScheduleBatch(count, 512);
				}
				break;
			case GltfComponentType.Byte:
				if (normalized)
				{
					if (ensureUnitLength)
					{
						ConvertNormalsInt8ToFloatInterleavedNormalizedJob jobData8 = new ConvertNormalsInt8ToFloatInterleavedNormalizedJob
						{
							input = (sbyte*)input,
							inputByteStride = ((inputByteStride > 0) ? inputByteStride : 3),
							outputByteStride = outputByteStride,
							result = output
						};
						result = jobData8.ScheduleBatch(count, 512);
					}
					else
					{
						ConvertVector3Int8ToFloatInterleavedNormalizedJob jobData9 = new ConvertVector3Int8ToFloatInterleavedNormalizedJob
						{
							input = (sbyte*)input,
							inputByteStride = ((inputByteStride > 0) ? inputByteStride : 3),
							outputByteStride = outputByteStride,
							result = output
						};
						result = jobData9.ScheduleBatch(count, 512);
					}
				}
				else
				{
					ConvertPositionsInt8ToFloatInterleavedJob jobData10 = new ConvertPositionsInt8ToFloatInterleavedJob
					{
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 3),
						input = (sbyte*)input,
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData10.ScheduleBatch(count, 512);
				}
				break;
			case GltfComponentType.UnsignedByte:
				if (normalized)
				{
					ConvertPositionsUInt8ToFloatInterleavedNormalizedJob jobData = new ConvertPositionsUInt8ToFloatInterleavedNormalizedJob
					{
						input = (byte*)input,
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 3),
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData.ScheduleBatch(count, 512);
				}
				else
				{
					ConvertPositionsUInt8ToFloatInterleavedJob jobData2 = new ConvertPositionsUInt8ToFloatInterleavedJob
					{
						input = (byte*)input,
						inputByteStride = ((inputByteStride > 0) ? inputByteStride : 3),
						outputByteStride = outputByteStride,
						result = output
					};
					result = jobData2.ScheduleBatch(count, 512);
				}
				break;
			default:
				Debug.LogError("Unknown componentType");
				result = null;
				break;
			}
			return result;
		}

		protected unsafe JobHandle? GetTangentsJob(void* input, int count, GltfComponentType inputType, int inputByteStride, float4* output, int outputByteStride, bool normalized = false)
		{
			JobHandle? result;
			switch (inputType)
			{
			case GltfComponentType.Float:
			{
				ConvertTangentsFloatToFloatInterleavedJob jobData = new ConvertTangentsFloatToFloatInterleavedJob
				{
					inputByteStride = ((inputByteStride > 0) ? inputByteStride : 16),
					input = (byte*)input,
					outputByteStride = outputByteStride,
					result = output
				};
				result = jobData.ScheduleBatch(count, 512);
				break;
			}
			case GltfComponentType.Short:
			{
				ConvertTangentsInt16ToFloatInterleavedNormalizedJob jobData2 = new ConvertTangentsInt16ToFloatInterleavedNormalizedJob
				{
					inputByteStride = ((inputByteStride > 0) ? inputByteStride : 8),
					input = (short*)input,
					outputByteStride = outputByteStride,
					result = output
				};
				result = jobData2.ScheduleBatch(count, 512);
				break;
			}
			case GltfComponentType.Byte:
			{
				ConvertTangentsInt8ToFloatInterleavedNormalizedJob jobData3 = new ConvertTangentsInt8ToFloatInterleavedNormalizedJob
				{
					inputByteStride = ((inputByteStride > 0) ? inputByteStride : 4),
					input = (sbyte*)input,
					outputByteStride = outputByteStride,
					result = output
				};
				result = jobData3.ScheduleBatch(count, 512);
				break;
			}
			default:
				m_GltfImport.Logger?.Error(LogCode.TypeUnsupported, "Tangent", inputType.ToString());
				result = null;
				break;
			}
			return result;
		}

		public unsafe static JobHandle? GetVector3SparseJob(void* indexBuffer, void* valueBuffer, int sparseCount, GltfComponentType indexType, GltfComponentType valueType, float3* output, int outputByteStride, ref JobHandle? dependsOn, bool normalized = false)
		{
			ConvertVector3SparseJob jobData = new ConvertVector3SparseJob
			{
				indexBuffer = indexBuffer,
				indexConverter = CachedFunction.GetIndexConverter(indexType),
				inputByteStride = 3 * AccessorBase.GetComponentTypeSize(valueType),
				input = valueBuffer,
				valueConverter = CachedFunction.GetPositionConverter(valueType, normalized),
				outputByteStride = outputByteStride,
				result = output
			};
			return IJobParallelForExtensions.Schedule(jobData, sparseCount, 512, dependsOn.GetValueOrDefault());
		}
	}
}
