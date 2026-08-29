using System;
using GLTFast.Logging;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Rendering;

namespace GLTFast
{
	internal abstract class VertexBufferTexCoordsBase : IDisposable
	{
		protected ICodeLogger m_Logger;

		public int UVSetCount { get; protected set; }

		protected VertexBufferTexCoordsBase(ICodeLogger logger)
		{
			m_Logger = logger;
		}

		public abstract bool ScheduleVertexUVJobs(int offset, int[] uvAccessorIndices, NativeSlice<JobHandle> handles, IGltfBuffers buffers);

		public abstract void AddDescriptors(VertexAttributeDescriptor[] dst, ref int offset, int stream);

		public abstract void ApplyOnMesh(Mesh msh, int stream, MeshUpdateFlags flags = MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers | MeshUpdateFlags.DontRecalculateBounds);

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected abstract void Dispose(bool disposing);
	}
}
