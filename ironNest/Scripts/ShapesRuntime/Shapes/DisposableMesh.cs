using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	public class DisposableMesh : IDisposable
	{
		private static int activeMeshCount;

		protected Mesh mesh;

		protected bool meshDirty;

		protected bool hasData;

		private bool hasMesh;

		private bool disposeWhenFullyReleased;

		internal List<DrawCommand> usedByCommands;

		public static int ActiveMeshCount => 0;

		protected void EnsureMeshExists()
		{
		}

		internal void RegisterToCommandBuffer(DrawCommand cmd)
		{
		}

		internal void ReleaseFromCommand(DrawCommand cmd)
		{
		}

		public void Dispose()
		{
		}

		protected void ClearMesh()
		{
		}

		protected virtual bool ExternallyDirty()
		{
			return false;
		}

		protected virtual void UpdateMesh()
		{
		}

		protected bool EnsureMeshIsReadyToRender(out Mesh outMesh, Action updateMesh)
		{
			outMesh = null;
			return false;
		}
	}
}
