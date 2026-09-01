using UnityEngine;

namespace TND.Upscaling.Framework
{
	public enum UpscalerQuality
	{
		Off = -1,
		[InspectorName(null)]
		Custom = 0,
		[InspectorName("Native AA (1x)")]
		NativeAA = 1,
		[InspectorName("Ultra Quality (1.2x)")]
		UltraQuality = 2,
		[InspectorName("Quality (1.5x)")]
		Quality = 3,
		[InspectorName("Balanced (1.7x)")]
		Balanced = 4,
		[InspectorName("Performance (2x)")]
		Performance = 5,
		[InspectorName("Ultra Performance (3x)")]
		UltraPerformance = 6
	}
}
