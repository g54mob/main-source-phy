using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace GLTFast
{
	internal abstract class MeshGeneratorBase : IDisposable
	{
		public const MeshUpdateFlags defaultMeshUpdateFlags = MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers | MeshUpdateFlags.DontRecalculateBounds;

		protected Task<Mesh> m_CreationTask;

		protected MorphTargetsGenerator m_MorphTargetsGenerator;

		protected string m_MeshName;

		public bool IsCompleted
		{
			get
			{
				if (m_CreationTask != null)
				{
					return m_CreationTask.IsCompleted;
				}
				return true;
			}
		}

		protected MeshGeneratorBase(string meshName)
		{
			m_MeshName = meshName;
		}

		public async Task<Mesh> CreateMeshResult()
		{
			while (!IsCompleted)
			{
				await Task.Yield();
			}
			return m_CreationTask?.Result;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				m_CreationTask?.Dispose();
			}
		}
	}
}
