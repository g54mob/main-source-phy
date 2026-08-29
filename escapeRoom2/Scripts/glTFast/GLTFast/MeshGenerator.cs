using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
	internal class MeshGenerator : MeshGeneratorBase
	{
		private VertexBufferGeneratorBase m_VertexData;

		private NativeArray<int>[] m_Indices;

		private List<IDisposable> m_Disposables;

		private readonly SubMeshAssignment[] m_SubMeshAssignments;

		private readonly IReadOnlyList<MeshPrimitiveBase> m_Primitives;

		private MeshTopology m_Topology;

		private int SubMeshCount
		{
			get
			{
				SubMeshAssignment[] subMeshAssignments = m_SubMeshAssignments;
				if (subMeshAssignments == null)
				{
					return m_Primitives.Count;
				}
				return subMeshAssignments.Length;
			}
		}

		public MeshGenerator(IReadOnlyList<MeshPrimitiveBase> primitives, SubMeshAssignment[] subMeshAssignments, string[] morphTargetNames, string meshName, GltfImportBase gltfImport)
			: base(meshName)
		{
			m_Primitives = primitives;
			m_SubMeshAssignments = subMeshAssignments;
			if (CreateVertexGenerator(gltfImport, out var hasNormals, out var hasTangents))
			{
				CreateMorphTargetGenerator(morphTargetNames, hasNormals, hasTangents, gltfImport);
				m_CreationTask = GenerateMesh(gltfImport);
			}
		}

		private bool CreateVertexGenerator(GltfImportBase gltfImport, out bool hasNormals, out bool hasTangents)
		{
			DrawMode mode = m_Primitives[0].mode;
			if (!SetTopology(mode))
			{
				gltfImport.Logger?.Error(LogCode.PrimitiveModeUnsupported, mode.ToString());
			}
			MainBufferType mainBufferType = GetMainBufferType(gltfImport, out hasNormals, out hasTangents);
			switch (mainBufferType)
			{
			case MainBufferType.Position:
				m_VertexData = new VertexBufferGenerator<VPos>(m_Primitives.Count, gltfImport);
				break;
			case MainBufferType.PosNorm:
				m_VertexData = new VertexBufferGenerator<VPosNorm>(m_Primitives.Count, gltfImport);
				break;
			case MainBufferType.PosNormTan:
				m_VertexData = new VertexBufferGenerator<VPosNormTan>(m_Primitives.Count, gltfImport);
				break;
			default:
				gltfImport.Logger?.Error(LogCode.BufferMainInvalidType, mainBufferType.ToString());
				return false;
			}
			m_VertexData.calculateNormals = !hasNormals && (mainBufferType & MainBufferType.Normal) > MainBufferType.None;
			m_VertexData.calculateTangents = !hasTangents && (mainBufferType & MainBufferType.Tangent) > MainBufferType.None;
			foreach (MeshPrimitiveBase primitive in m_Primitives)
			{
				m_VertexData.AddPrimitive(primitive.attributes);
			}
			m_VertexData.Initialize();
			return true;
		}

		private MainBufferType GetMainBufferType(GltfImportBase gltfImport, out bool hasNormals, out bool hasTangents)
		{
			MainBufferType mainBufferType = MainBufferType.Position;
			Attributes attributes = m_Primitives[0].attributes;
			hasNormals = attributes.NORMAL >= 0;
			hasTangents = attributes.TANGENT >= 0;
			if (hasTangents)
			{
				mainBufferType = MainBufferType.PosNormTan;
			}
			else if (hasNormals)
			{
				mainBufferType = MainBufferType.PosNorm;
			}
			foreach (MeshPrimitiveBase item in IterateSubMeshes())
			{
				if (item.mode != DrawMode.Triangles && item.mode != DrawMode.TriangleFan && item.mode != DrawMode.TriangleStrip)
				{
					continue;
				}
				if (item.material < 0)
				{
					mainBufferType |= MainBufferType.Normal;
					continue;
				}
				MaterialBase sourceMaterial = gltfImport.GetSourceMaterial(item.material);
				if (sourceMaterial.RequiresTangents)
				{
					mainBufferType |= MainBufferType.Tangent;
				}
				else if (sourceMaterial.RequiresNormals)
				{
					mainBufferType |= MainBufferType.Normal;
				}
			}
			return mainBufferType;
		}

		private bool SetTopology(DrawMode drawMode)
		{
			switch (drawMode)
			{
			case DrawMode.Triangles:
			case DrawMode.TriangleStrip:
			case DrawMode.TriangleFan:
				m_Topology = MeshTopology.Triangles;
				break;
			case DrawMode.Points:
				m_Topology = MeshTopology.Points;
				break;
			case DrawMode.Lines:
				m_Topology = MeshTopology.Lines;
				break;
			case DrawMode.LineLoop:
			case DrawMode.LineStrip:
				m_Topology = MeshTopology.LineStrip;
				break;
			default:
				m_Topology = MeshTopology.Triangles;
				return false;
			}
			return true;
		}

		private void CreateMorphTargetGenerator(string[] morphTargetNames, bool hasNormals, bool hasTangents, GltfImportBase gltfImport)
		{
			MorphTarget[] targets = m_Primitives[0].targets;
			if (targets != null)
			{
				m_MorphTargetsGenerator = new MorphTargetsGenerator(m_VertexData.VertexCount, m_Primitives.Count, targets.Length, morphTargetNames, hasNormals, hasTangents, gltfImport);
			}
		}

		private async Task<UnityEngine.Mesh> GenerateMesh(GltfImportBase gltfImport)
		{
			JobHandle? jh = m_VertexData.CreateVertexBuffer();
			if (!jh.HasValue)
			{
				return null;
			}
			while (!jh.Value.IsCompleted)
			{
				await Task.Yield();
			}
			jh.Value.Complete();
			m_Indices = new NativeArray<int>[SubMeshCount];
			List<JobHandle> tmpList = new List<JobHandle>(SubMeshCount);
			foreach (var (subMeshIndex, meshPrimitiveBase) in IterateSubMeshesIndexed())
			{
				if (meshPrimitiveBase.indices >= 0)
				{
					bool flip = meshPrimitiveBase.mode == DrawMode.Triangles;
					GetIndicesJob(gltfImport, meshPrimitiveBase.indices, out var indices, out var getIndicesJob, flip);
					if (!getIndicesJob.HasValue)
					{
						return null;
					}
					switch (meshPrimitiveBase.mode)
					{
					case DrawMode.LineLoop:
						m_Indices[subMeshIndex] = new NativeArray<int>(indices.Length + 1, Allocator.Persistent);
						while (!getIndicesJob.Value.IsCompleted)
						{
							await Task.Yield();
						}
						getIndicesJob.Value.Complete();
						NativeArray<int>.Copy(indices, m_Indices[subMeshIndex], indices.Length);
						m_Indices[subMeshIndex][indices.Length] = indices[0];
						indices.Dispose();
						break;
					case DrawMode.TriangleStrip:
					{
						int num2 = indices.Length - 2;
						m_Indices[subMeshIndex] = new NativeArray<int>(num2 * 3, Allocator.Persistent);
						JobHandle item2 = IJobParallelForExtensions.Schedule(new RecalculateIndicesForTriangleStripJob
						{
							input = indices,
							result = m_Indices[subMeshIndex]
						}, num2, 512, getIndicesJob.Value);
						tmpList.Add(item2);
						if (m_Disposables == null)
						{
							m_Disposables = new List<IDisposable>();
						}
						m_Disposables.Add(indices);
						break;
					}
					case DrawMode.TriangleFan:
					{
						int num = indices.Length - 2;
						m_Indices[subMeshIndex] = new NativeArray<int>(num * 3, Allocator.Persistent);
						JobHandle item = IJobParallelForExtensions.Schedule(new RecalculateIndicesForTriangleFanJob
						{
							input = indices,
							result = m_Indices[subMeshIndex]
						}, num, 512, getIndicesJob.Value);
						if (m_Disposables == null)
						{
							m_Disposables = new List<IDisposable>();
						}
						m_Disposables.Add(indices);
						tmpList.Add(item);
						break;
					}
					default:
						m_Indices[subMeshIndex] = indices;
						tmpList.Add(getIndicesJob.Value);
						break;
					}
					indices = default(NativeArray<int>);
					getIndicesJob = null;
				}
				else
				{
					int count = ((IGltfBuffers)gltfImport).GetAccessor(meshPrimitiveBase.attributes.POSITION).count;
					CalculateIndicesJob(meshPrimitiveBase, count, out m_Indices[subMeshIndex], out var jobHandle);
					tmpList.Add(jobHandle);
				}
			}
			if (m_MorphTargetsGenerator != null)
			{
				for (int i = 0; i < m_Primitives.Count; i++)
				{
					MeshPrimitiveBase primitive = m_Primitives[i];
					AddMorphTargets(i, primitive, gltfImport.Logger);
				}
				tmpList.Add(m_MorphTargetsGenerator.GetJobHandle());
			}
			await AwaitJobs(tmpList);
			return await CreateMeshResultAsync();
		}

		private void AddMorphTargets(int subMesh, MeshPrimitiveBase primitive, ICodeLogger logger)
		{
			if (m_MorphTargetsGenerator == null)
			{
				return;
			}
			int offset = m_VertexData.VertexIntervals[subMesh];
			for (int i = 0; i < primitive.targets.Length; i++)
			{
				MorphTarget morphTarget = primitive.targets[i];
				if (!m_MorphTargetsGenerator.AddMorphTarget(offset, i, morphTarget))
				{
					logger?.Error(LogCode.MorphTargetContextFail);
				}
			}
		}

		private async Task<UnityEngine.Mesh> CreateMeshResultAsync()
		{
			UnityEngine.Mesh msh = new UnityEngine.Mesh
			{
				name = m_MeshName
			};
			m_VertexData.ApplyOnMesh(msh);
			int num = 0;
			for (int i = 0; i < m_Indices.Length; i++)
			{
				num += m_Indices[i].Length;
			}
			msh.SetIndexBufferParams(num, IndexFormat.UInt32);
			msh.subMeshCount = m_Indices.Length;
			num = 0;
			Bounds bounds = default(Bounds);
			for (int j = 0; j < m_Indices.Length; j++)
			{
				msh.SetIndexBufferData(m_Indices[j], 0, num, m_Indices[j].Length, MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers | MeshUpdateFlags.DontRecalculateBounds);
				int subMesh = ((m_SubMeshAssignments != null) ? m_SubMeshAssignments[j].VertexBufferIndex : j);
				m_VertexData.GetVertexRange(subMesh, out var baseVertex, out var vertexCount);
				Bounds bounds2;
				bool flag = m_VertexData.TryGetBounds(subMesh, out bounds2);
				SubMeshDescriptor desc = new SubMeshDescriptor
				{
					indexStart = num,
					indexCount = m_Indices[j].Length,
					topology = m_Topology,
					baseVertex = baseVertex,
					firstVertex = baseVertex,
					vertexCount = vertexCount,
					bounds = bounds2
				};
				msh.SetSubMesh(j, desc, flag ? (MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers | MeshUpdateFlags.DontRecalculateBounds) : (MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers));
				if (!flag)
				{
					bounds2 = msh.GetSubMesh(j).bounds;
				}
				if (j == 0)
				{
					bounds = bounds2;
				}
				else
				{
					bounds.Encapsulate(bounds2);
				}
				num += m_Indices[j].Length;
			}
			msh.bounds = bounds;
			if (m_Topology == MeshTopology.Triangles || m_Topology == MeshTopology.Quads)
			{
				if (m_VertexData.calculateNormals)
				{
					msh.RecalculateNormals();
				}
				if (m_VertexData.calculateTangents)
				{
					msh.RecalculateTangents();
				}
			}
			if (m_MorphTargetsGenerator != null)
			{
				await m_MorphTargetsGenerator.ApplyOnMeshAndDispose(msh);
			}
			return msh;
		}

		private IEnumerable<(int index, MeshPrimitiveBase primitive)> IterateSubMeshesIndexed()
		{
			if (m_SubMeshAssignments == null)
			{
				for (int index = 0; index < m_Primitives.Count; index++)
				{
					MeshPrimitiveBase item = m_Primitives[index];
					yield return (index: index, primitive: item);
				}
			}
			else
			{
				for (int index = 0; index < m_SubMeshAssignments.Length; index++)
				{
					SubMeshAssignment subMeshAssignment = m_SubMeshAssignments[index];
					yield return (index: index, primitive: subMeshAssignment.Primitive);
				}
			}
		}

		private IEnumerable<MeshPrimitiveBase> IterateSubMeshes()
		{
			if (m_SubMeshAssignments == null)
			{
				foreach (MeshPrimitiveBase primitive in m_Primitives)
				{
					yield return primitive;
				}
				yield break;
			}
			SubMeshAssignment[] subMeshAssignments = m_SubMeshAssignments;
			foreach (SubMeshAssignment subMeshAssignment in subMeshAssignments)
			{
				yield return subMeshAssignment.Primitive;
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				m_VertexData?.Dispose();
				if (m_Indices != null)
				{
					for (int i = 0; i < m_Indices.Length; i++)
					{
						NativeArray<int> nativeArray = m_Indices[i];
						if (nativeArray.IsCreated)
						{
							nativeArray.Dispose();
						}
					}
					m_Indices = null;
				}
			}
			if (m_Disposables == null)
			{
				return;
			}
			foreach (IDisposable disposable in m_Disposables)
			{
				disposable.Dispose();
			}
			m_Disposables = null;
		}

		private unsafe static void GetIndicesJob(GltfImportBase gltfImport, int accessorIndex, out NativeArray<int> indices, out JobHandle? jobHandle, bool flip)
		{
			AccessorBase accessor = ((IGltfBuffers)gltfImport).GetAccessor(accessorIndex);
			int byteStride;
			NativeSlice<byte> bufferView = ((IGltfBuffers)gltfImport).GetBufferView(accessor.bufferView, out byteStride, accessor.byteOffset, 0);
			indices = new NativeArray<int>(accessor.count, Allocator.Persistent);
			if (accessor.IsSparse)
			{
				gltfImport.Logger?.Error(LogCode.SparseAccessor, "indices");
			}
			switch (accessor.componentType)
			{
			case GltfComponentType.UnsignedByte:
				if (flip)
				{
					ConvertIndicesUInt8ToInt32FlippedJob jobData = new ConvertIndicesUInt8ToInt32FlippedJob
					{
						input = (byte*)bufferView.GetUnsafeReadOnlyPtr(),
						result = indices.Reinterpret<int3>(4)
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData, accessor.count / 3, 512);
				}
				else
				{
					ConvertIndicesUInt8ToInt32Job jobData2 = new ConvertIndicesUInt8ToInt32Job
					{
						input = (byte*)bufferView.GetUnsafeReadOnlyPtr(),
						result = indices
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData2, accessor.count, 512);
				}
				break;
			case GltfComponentType.UnsignedShort:
				if (flip)
				{
					ConvertIndicesUInt16ToInt32FlippedJob jobData3 = new ConvertIndicesUInt16ToInt32FlippedJob
					{
						input = (ushort*)bufferView.GetUnsafeReadOnlyPtr(),
						result = indices.Reinterpret<int3>(4)
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData3, accessor.count / 3, 512);
				}
				else
				{
					ConvertIndicesUInt16ToInt32Job jobData4 = new ConvertIndicesUInt16ToInt32Job
					{
						input = (ushort*)bufferView.GetUnsafeReadOnlyPtr(),
						result = indices
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData4, accessor.count, 512);
				}
				break;
			case GltfComponentType.UnsignedInt:
				if (flip)
				{
					ConvertIndicesUInt32ToInt32FlippedJob jobData5 = new ConvertIndicesUInt32ToInt32FlippedJob
					{
						input = (uint*)bufferView.GetUnsafeReadOnlyPtr(),
						result = indices.Reinterpret<int3>(4)
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData5, accessor.count / 3, 512);
				}
				else
				{
					ConvertIndicesUInt32ToInt32Job jobData6 = new ConvertIndicesUInt32ToInt32Job
					{
						input = (uint*)bufferView.GetUnsafeReadOnlyPtr(),
						result = indices
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData6, accessor.count, 512);
				}
				break;
			default:
				gltfImport.Logger?.Error(LogCode.IndexFormatInvalid, accessor.componentType.ToString());
				jobHandle = null;
				break;
			}
		}

		private static void CalculateIndicesJob(MeshPrimitiveBase primitive, int vertexCount, out NativeArray<int> indices, out JobHandle jobHandle)
		{
			switch (primitive.mode)
			{
			case DrawMode.LineLoop:
			{
				indices = new NativeArray<int>(vertexCount + 1, Allocator.Persistent);
				indices[vertexCount] = 0;
				CreateIndicesInt32Job jobData5 = new CreateIndicesInt32Job
				{
					result = indices
				};
				jobHandle = IJobParallelForExtensions.Schedule(jobData5, vertexCount, 512);
				break;
			}
			case DrawMode.Triangles:
			{
				indices = new NativeArray<int>(vertexCount, Allocator.Persistent);
				CreateIndicesInt32FlippedJob jobData4 = new CreateIndicesInt32FlippedJob
				{
					result = indices
				};
				jobHandle = IJobParallelForExtensions.Schedule(jobData4, indices.Length, 512);
				break;
			}
			case DrawMode.TriangleStrip:
			{
				indices = new NativeArray<int>((vertexCount - 2) * 3, Allocator.Persistent);
				CreateIndicesForTriangleStripJob jobData3 = new CreateIndicesForTriangleStripJob
				{
					result = indices
				};
				jobHandle = IJobParallelForExtensions.Schedule(jobData3, indices.Length, 512);
				break;
			}
			case DrawMode.TriangleFan:
			{
				indices = new NativeArray<int>((vertexCount - 2) * 3, Allocator.Persistent);
				CreateIndicesForTriangleFanJob jobData2 = new CreateIndicesForTriangleFanJob
				{
					result = indices
				};
				jobHandle = IJobParallelForExtensions.Schedule(jobData2, indices.Length, 512);
				break;
			}
			default:
			{
				indices = new NativeArray<int>(vertexCount, Allocator.Persistent);
				CreateIndicesInt32Job jobData = new CreateIndicesInt32Job
				{
					result = indices
				};
				jobHandle = IJobParallelForExtensions.Schedule(jobData, vertexCount, 512);
				break;
			}
			}
		}

		private static async Task AwaitJobs(List<JobHandle> tmpList)
		{
			if (tmpList.Count > 0)
			{
				NativeArray<JobHandle> jobs = new NativeArray<JobHandle>(tmpList.ToArray(), Allocator.Persistent);
				JobHandle allJobs = JobHandle.CombineDependencies(jobs);
				jobs.Dispose();
				while (!allJobs.IsCompleted)
				{
					await Task.Yield();
				}
				allJobs.Complete();
			}
		}
	}
}
