using System;

namespace GLTFast
{
	[Flags]
	internal enum AccessorUsage
	{
		Unknown = 0,
		Ignore = 1,
		Index = 2,
		IndexFlipped = 4,
		Position = 8,
		Normal = 0x10,
		Tangent = 0x20,
		UV = 0x40,
		Color = 0x80,
		InverseBindMatrix = 0x100,
		AnimationTimes = 0x200,
		Translation = 0x400,
		Rotation = 0x800,
		Scale = 0x1000,
		Weight = 0x2000,
		RequiredForInstantiation = 0x4000
	}
}
