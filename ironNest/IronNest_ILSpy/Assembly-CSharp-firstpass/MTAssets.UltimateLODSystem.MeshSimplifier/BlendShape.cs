using System;
using System.Runtime.InteropServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

[Serializable]
[StructLayout((LayoutKind)3)]
public struct BlendShape(string shapeName, BlendShapeFrame[] frames)
{
	public string ShapeName = shapeName;

	public BlendShapeFrame[] Frames = frames;
}
