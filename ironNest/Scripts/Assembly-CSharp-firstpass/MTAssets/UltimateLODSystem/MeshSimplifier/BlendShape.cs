using System;
using System.Runtime.InteropServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier
{
	[Serializable]
	[StructLayout((LayoutKind)3)]
	public struct BlendShape
	{
		public string ShapeName;

		public BlendShapeFrame[] Frames;

		public BlendShape(string shapeName, BlendShapeFrame[] frames)
		{
			ShapeName = null;
			Frames = null;
		}
	}
}
