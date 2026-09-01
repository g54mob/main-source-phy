using System;

namespace BitCode.MeshTool.Enums
{
	[Flags]
	public enum CombinerSettingsFlag
	{
		GeometryOnly = 0,
		Normals = 1,
		Tangents = 2,
		VertColors = 4,
		UV0 = 8,
		UV1 = 0x10,
		UV2 = 0x20,
		UV3 = 0x40,
		Skinning = 0x80,
		BlendShapes = 0x100
	}
}
