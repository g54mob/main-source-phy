using System;

namespace GLTFast
{
	[Flags]
	public enum ComponentType
	{
		None = 0,
		Mesh = 2,
		Animation = 4,
		Camera = 8,
		Light = 0x10,
		All = -1
	}
}
