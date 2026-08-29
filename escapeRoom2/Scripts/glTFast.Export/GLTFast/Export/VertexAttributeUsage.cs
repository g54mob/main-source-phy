using System;

namespace GLTFast.Export
{
	[Flags]
	public enum VertexAttributeUsage
	{
		None = 0,
		Position = 1,
		Normal = 2,
		Tangent = 4,
		Color = 8,
		TexCoord0 = 0x10,
		TexCoord1 = 0x20,
		TexCoord2 = 0x40,
		TexCoord3 = 0x80,
		TexCoord4 = 0x100,
		TexCoord5 = 0x200,
		TexCoord6 = 0x400,
		TexCoord7 = 0x800,
		BlendWeight = 0x1000,
		BlendIndices = 0x2000,
		TwoTexCoords = 0x30,
		AllTexCoords = 0xFF0,
		Skinning = 0x3000
	}
}
