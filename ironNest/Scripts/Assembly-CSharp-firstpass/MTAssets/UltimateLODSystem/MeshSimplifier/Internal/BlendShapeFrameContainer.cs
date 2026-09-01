using System.Runtime.CompilerServices;
using UnityEngine;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal
{
	internal class BlendShapeFrameContainer
	{
		private readonly float frameWeight;

		private readonly ResizableArray<Vector3> deltaVertices;

		private readonly ResizableArray<Vector3> deltaNormals;

		private readonly ResizableArray<Vector3> deltaTangents;

		public BlendShapeFrameContainer(BlendShapeFrame frame)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveVertexElement(int dst, int src)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InterpolateVertexAttributes(int dst, int i0, int i1, int i2, ref Vector3 barycentricCoord)
		{
		}

		public void Resize(int length, bool trimExess = false)
		{
		}

		public BlendShapeFrame ToBlendShapeFrame()
		{
			return default(BlendShapeFrame);
		}
	}
}
