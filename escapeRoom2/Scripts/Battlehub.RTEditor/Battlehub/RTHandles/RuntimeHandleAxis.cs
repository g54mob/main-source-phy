using System;

namespace Battlehub.RTHandles
{
	[Flags]
	public enum RuntimeHandleAxis
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 4,
		XY = 3,
		XZ = 5,
		YZ = 6,
		XYZ = 7,
		Screen = 8,
		Free = 0x10,
		Snap = 0x20,
		Custom = 0x10000
	}
}
