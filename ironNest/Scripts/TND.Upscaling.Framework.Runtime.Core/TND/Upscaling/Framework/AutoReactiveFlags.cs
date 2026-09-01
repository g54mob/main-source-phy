using System;

namespace TND.Upscaling.Framework
{
	[Flags]
	public enum AutoReactiveFlags
	{
		ApplyTonemap = 1,
		ApplyInverseTonemap = 2,
		ApplyThreshold = 4,
		UseComponentsMax = 8
	}
}
