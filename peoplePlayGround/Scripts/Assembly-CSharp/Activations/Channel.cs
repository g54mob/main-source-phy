using System;

namespace Activations
{
	[Flags]
	public enum Channel : byte
	{
		None = 0,
		Green = 1,
		Red = 2,
		Blue = 4,
		All = byte.MaxValue
	}
}
