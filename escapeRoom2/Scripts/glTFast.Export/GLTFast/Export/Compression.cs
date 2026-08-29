using System;

namespace GLTFast.Export
{
	[Flags]
	public enum Compression
	{
		Uncompressed = 1,
		MeshOpt = 2,
		Draco = 4
	}
}
