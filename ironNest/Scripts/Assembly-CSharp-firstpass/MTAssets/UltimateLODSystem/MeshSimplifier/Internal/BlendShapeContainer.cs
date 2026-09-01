using System.Runtime.CompilerServices;
using UnityEngine;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal
{
	internal class BlendShapeContainer
	{
		private readonly string shapeName;

		private readonly BlendShapeFrameContainer[] frames;

		public BlendShapeContainer(BlendShape blendShape)
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

		public BlendShape ToBlendShape()
		{
			return default(BlendShape);
		}
	}
}
