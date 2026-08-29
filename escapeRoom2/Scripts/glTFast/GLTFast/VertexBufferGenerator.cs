using System.Runtime.InteropServices;
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
	internal class VertexBufferGenerator<TMainBuffer> : VertexBufferGeneratorBase where TMainBuffer : struct
	{
		private NativeArray<TMainBuffer> m_Data;

		private bool m_HasNormals;

		private bool m_HasTangents;

		private bool m_HasColors;

		private bool m_HasBones;

		private VertexBufferTexCoordsBase m_TexCoords;

		private VertexBufferColors m_Colors;

		private VertexBufferBones m_Bones;

		private AccessorBase[] m_PositionAccessors;

		public override int VertexCount
		{
			get
			{
				if (VertexIntervals == null)
				{
					return 0;
				}
				return VertexIntervals[VertexIntervals.Length - 1];
			}
		}

		public override int[] VertexIntervals { get; protected set; }

		public override void GetVertexRange(int subMesh, out int baseVertex, out int vertexCount)
		{
			baseVertex = VertexIntervals[subMesh];
			vertexCount = VertexIntervals[subMesh + 1] - baseVertex;
		}

		public override bool TryGetBounds(int subMesh, out Bounds bounds)
		{
			Bounds? bounds2 = m_PositionAccessors[subMesh].TryGetBounds();
			if (bounds2.HasValue)
			{
				bounds = bounds2.Value;
				return true;
			}
			m_GltfImport.Logger?.Error(LogCode.MeshBoundsMissing, m_Attributes[subMesh].POSITION.ToString());
			bounds = default(Bounds);
			return false;
		}

		public VertexBufferGenerator(int primitiveCount, GltfImportBase gltfImport)
			: base(primitiveCount, gltfImport)
		{
		}

		public override void AddPrimitive(Attributes att)
		{
			m_Attributes[m_AttributeCount++] = att;
		}

		public override void Initialize()
		{
			int num = 0;
			m_PositionAccessors = new AccessorBase[m_Attributes.Length];
			VertexIntervals = new int[m_Attributes.Length + 1];
			for (int i = 0; i < m_Attributes.Length; i++)
			{
				VertexIntervals[i] = num;
				m_PositionAccessors[i] = ((IGltfBuffers)m_GltfImport).GetAccessor(m_Attributes[i].POSITION);
				num += m_PositionAccessors[i].count;
			}
			VertexIntervals[m_Attributes.Length] = num;
		}

		public unsafe override JobHandle? CreateVertexBuffer()
		{
			m_Data = new NativeArray<TMainBuffer>(VertexCount, Allocator.Persistent);
			byte* unsafeReadOnlyPtr = (byte*)m_Data.GetUnsafeReadOnlyPtr();
			int num = 0;
			Attributes attributes = m_Attributes[0];
			int texCoordsCount = attributes.GetTexCoordsCount();
			if (texCoordsCount > 0)
			{
				if (texCoordsCount > 8)
				{
					m_GltfImport.Logger?.Warning(LogCode.UVLimit);
				}
				num += texCoordsCount * m_Attributes.Length;
				m_TexCoords = texCoordsCount switch
				{
					1 => new VertexBufferTexCoords<VTexCoord1>(texCoordsCount, VertexCount, m_GltfImport.Logger), 
					2 => new VertexBufferTexCoords<VTexCoord2>(texCoordsCount, VertexCount, m_GltfImport.Logger), 
					3 => new VertexBufferTexCoords<VTexCoord3>(texCoordsCount, VertexCount, m_GltfImport.Logger), 
					4 => new VertexBufferTexCoords<VTexCoord4>(texCoordsCount, VertexCount, m_GltfImport.Logger), 
					5 => new VertexBufferTexCoords<VTexCoord5>(texCoordsCount, VertexCount, m_GltfImport.Logger), 
					6 => new VertexBufferTexCoords<VTexCoord6>(texCoordsCount, VertexCount, m_GltfImport.Logger), 
					7 => new VertexBufferTexCoords<VTexCoord7>(texCoordsCount, VertexCount, m_GltfImport.Logger), 
					_ => new VertexBufferTexCoords<VTexCoord8>(texCoordsCount, VertexCount, m_GltfImport.Logger), 
				};
			}
			m_HasColors = attributes.COLOR_0 >= 0;
			if (m_HasColors)
			{
				num += m_Attributes.Length;
				m_Colors = new VertexBufferColors(VertexCount, m_GltfImport.Logger);
			}
			m_HasBones = attributes.WEIGHTS_0 >= 0 && attributes.JOINTS_0 >= 0;
			if (m_HasBones)
			{
				num += m_Attributes.Length;
				m_Bones = new VertexBufferBones(VertexCount, m_GltfImport.Logger);
			}
			for (int i = 0; i < m_Attributes.Length; i++)
			{
				num++;
				Attributes obj = m_Attributes[i];
				if (m_PositionAccessors[i].IsSparse && m_PositionAccessors[i].bufferView >= 0)
				{
					num++;
				}
				if (obj.NORMAL >= 0)
				{
					num++;
					m_HasNormals = true;
				}
				m_HasNormals |= calculateNormals;
				if (obj.TANGENT >= 0)
				{
					num++;
					m_HasTangents = true;
				}
				m_HasTangents |= calculateTangents;
			}
			NativeArray<JobHandle> nativeArray = new NativeArray<JobHandle>(num, Allocator.Persistent);
			int handleIndex = 0;
			int outputByteStride = Marshal.SizeOf(typeof(TMainBuffer));
			for (int j = 0; j < m_Attributes.Length; j++)
			{
				Attributes attributes2 = m_Attributes[j];
				if (!SchedulePositionsJobs(j, unsafeReadOnlyPtr, outputByteStride, nativeArray, ref handleIndex))
				{
					return null;
				}
				if (attributes2.NORMAL >= 0 && !ScheduleNormalsJobs(attributes2, unsafeReadOnlyPtr, outputByteStride, j, nativeArray, ref handleIndex))
				{
					return null;
				}
				if (attributes2.TANGENT >= 0 && !ScheduleTangentsJobs(attributes2, unsafeReadOnlyPtr, outputByteStride, j, nativeArray, ref handleIndex))
				{
					return null;
				}
				if (m_TexCoords != null)
				{
					handleIndex = ScheduleTexCoordJobs(attributes2, texCoordsCount, j, nativeArray, handleIndex);
				}
				if (m_HasColors && !ScheduleColorsJobs(attributes2, j, nativeArray, ref handleIndex))
				{
					return null;
				}
				if (m_HasBones && !ScheduleVertexBonesJobs(attributes2, j, nativeArray, handleIndex))
				{
					return null;
				}
			}
			JobHandle value = ((num > 1) ? JobHandle.CombineDependencies(nativeArray) : nativeArray[0]);
			nativeArray.Dispose();
			return value;
		}

		private unsafe bool SchedulePositionsJobs(int i, byte* vDataPtr, int outputByteStride, NativeArray<JobHandle> handles, ref int handleIndex)
		{
			JobHandle? dependsOn = null;
			if (m_PositionAccessors[i].bufferView >= 0)
			{
				((IGltfBuffers)m_GltfImport).GetAccessorDataAndByteStride(m_Attributes[i].POSITION, out NativeSlice<byte> data, out int byteStride);
				dependsOn = VertexBufferGeneratorBase.GetVector3Job(data.GetUnsafeReadOnlyPtr(), m_PositionAccessors[i].count, m_PositionAccessors[i].componentType, byteStride, (float3*)(vDataPtr + outputByteStride * VertexIntervals[i]), outputByteStride, m_PositionAccessors[i].normalized, ensureUnitLength: false);
			}
			if (m_PositionAccessors[i].IsSparse)
			{
				m_GltfImport.GetAccessorSparseIndices(m_PositionAccessors[i].Sparse.Indices, out var data2);
				m_GltfImport.GetAccessorSparseValues(m_PositionAccessors[i].Sparse.Values, out var data3);
				JobHandle? vector3SparseJob = VertexBufferGeneratorBase.GetVector3SparseJob(data2, data3, m_PositionAccessors[i].Sparse.count, m_PositionAccessors[i].Sparse.Indices.componentType, m_PositionAccessors[i].componentType, (float3*)(vDataPtr + outputByteStride * VertexIntervals[i]), outputByteStride, ref dependsOn, m_PositionAccessors[i].normalized);
				if (!vector3SparseJob.HasValue)
				{
					return false;
				}
				handles[handleIndex] = vector3SparseJob.Value;
				handleIndex++;
			}
			if (dependsOn.HasValue)
			{
				handles[handleIndex] = dependsOn.Value;
				handleIndex++;
				return true;
			}
			return false;
		}

		private unsafe bool ScheduleNormalsJobs(Attributes att, byte* vDataPtr, int outputByteStride, int i, NativeArray<JobHandle> handles, ref int handleIndex)
		{
			((IGltfBuffers)m_GltfImport).GetAccessorAndData(att.NORMAL, out AccessorBase accessor, out void* data, out int byteStride);
			if (accessor.IsSparse)
			{
				m_GltfImport.Logger?.Error(LogCode.SparseAccessor, "normals");
			}
			JobHandle? vector3Job = VertexBufferGeneratorBase.GetVector3Job(data, accessor.count, accessor.componentType, byteStride, (float3*)(vDataPtr + outputByteStride * VertexIntervals[i] + 12), outputByteStride, accessor.normalized);
			if (vector3Job.HasValue)
			{
				handles[handleIndex] = vector3Job.Value;
				handleIndex++;
				return true;
			}
			return false;
		}

		private unsafe bool ScheduleTangentsJobs(Attributes att, byte* vDataPtr, int outputByteStride, int i, NativeArray<JobHandle> handles, ref int handleIndex)
		{
			((IGltfBuffers)m_GltfImport).GetAccessorAndData(att.TANGENT, out AccessorBase accessor, out void* data, out int byteStride);
			if (accessor.IsSparse)
			{
				m_GltfImport.Logger?.Error(LogCode.SparseAccessor, "tangents");
			}
			JobHandle? tangentsJob = GetTangentsJob(data, accessor.count, accessor.componentType, byteStride, (float4*)(vDataPtr + outputByteStride * VertexIntervals[i] + 24), outputByteStride, accessor.normalized);
			if (tangentsJob.HasValue)
			{
				handles[handleIndex] = tangentsJob.Value;
				handleIndex++;
				return true;
			}
			return false;
		}

		private int ScheduleTexCoordJobs(Attributes att, int uvSetCount, int i, NativeArray<JobHandle> handles, int handleIndex)
		{
			att.TryGetAllUVAccessors(out var uvAccessors, out var _);
			m_TexCoords.ScheduleVertexUVJobs(VertexIntervals[i], uvAccessors, handles.Slice(handleIndex, uvAccessors.Length), m_GltfImport);
			handleIndex += uvAccessors.Length;
			return handleIndex;
		}

		private bool ScheduleColorsJobs(Attributes att, int i, NativeArray<JobHandle> handles, ref int handleIndex)
		{
			if (!m_Colors.ScheduleVertexColorJob(att.COLOR_0, VertexIntervals[i], handles.Slice(handleIndex, 1), m_GltfImport))
			{
				return false;
			}
			handleIndex++;
			return true;
		}

		private bool ScheduleVertexBonesJobs(Attributes att, int i, NativeArray<JobHandle> handles, int handleIndex)
		{
			JobHandle? jobHandle = m_Bones.ScheduleVertexBonesJob(att.WEIGHTS_0, att.JOINTS_0, VertexIntervals[i], m_GltfImport);
			if (jobHandle.HasValue)
			{
				handles[handleIndex] = jobHandle.Value;
				return true;
			}
			return false;
		}

		private void CreateDescriptors()
		{
			int num = 1;
			if (m_HasNormals)
			{
				num++;
			}
			if (m_HasTangents)
			{
				num++;
			}
			if (m_TexCoords != null)
			{
				num += m_TexCoords.UVSetCount;
			}
			if (m_Colors != null)
			{
				num++;
			}
			if (m_Bones != null)
			{
				num += 2;
			}
			m_Descriptors = new VertexAttributeDescriptor[num];
			int num2 = 0;
			int num3 = 0;
			m_Descriptors[num2] = new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, num3);
			num2++;
			if (m_HasNormals)
			{
				m_Descriptors[num2] = new VertexAttributeDescriptor(VertexAttribute.Normal, VertexAttributeFormat.Float32, 3, num3);
				num2++;
			}
			if (m_HasTangents)
			{
				m_Descriptors[num2] = new VertexAttributeDescriptor(VertexAttribute.Tangent, VertexAttributeFormat.Float32, 4, num3);
				num2++;
			}
			num3++;
			if (m_Colors != null)
			{
				m_Colors.AddDescriptors(m_Descriptors, num2, num3);
				num2++;
				num3++;
			}
			if (m_TexCoords != null)
			{
				m_TexCoords.AddDescriptors(m_Descriptors, ref num2, num3);
				num3++;
			}
			if (m_Bones != null)
			{
				m_Bones.AddDescriptors(m_Descriptors, num2, num3);
			}
		}

		public override void ApplyOnMesh(UnityEngine.Mesh msh, MeshUpdateFlags flags = MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers | MeshUpdateFlags.DontRecalculateBounds)
		{
			if (m_Descriptors == null)
			{
				CreateDescriptors();
			}
			msh.SetVertexBufferParams(m_Data.Length, m_Descriptors);
			int num = 0;
			msh.SetVertexBufferData(m_Data, 0, 0, m_Data.Length, num, flags);
			num++;
			if (m_Colors != null)
			{
				m_Colors.ApplyOnMesh(msh, num, flags);
				num++;
			}
			if (m_TexCoords != null)
			{
				m_TexCoords.ApplyOnMesh(msh, num, flags);
				num++;
			}
			if (m_Bones != null)
			{
				m_Bones.ApplyOnMesh(msh, num, flags);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (m_Data.IsCreated)
			{
				m_Data.Dispose();
			}
			if (disposing)
			{
				m_Colors?.Dispose();
				m_TexCoords?.Dispose();
				m_Bones?.Dispose();
			}
		}
	}
}
