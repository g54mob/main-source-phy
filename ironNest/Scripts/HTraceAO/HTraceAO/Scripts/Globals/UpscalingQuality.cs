using UnityEngine;

namespace HTraceAO.Scripts.Globals
{
	public enum UpscalingQuality
	{
		[InspectorName("Linear 5 Taps")]
		[Tooltip("Uses 4 neighbors (in a cross pattern) and the center pixel to reconstruct the full-resolution output.")]
		Linear5Taps = 0,
		[InspectorName("Linear 9 Taps")]
		[Tooltip("Uses 8 neighbors surrounding the center pixel, along with the center pixel itself, to reconstruct the full-resolution output. Marginally better than 5 taps, it provides slightly more accurate reconstruction for very small details.")]
		Linear9Taps = 1,
		[InspectorName("Lanczos 12 Taps")]
		[Tooltip("Employs an FSR 1.0-inspired approach with an adaptive Lanczos filter. Delivers the best and sharpest reconstruction at the cost of performance.")]
		Lanczos12Taps = 2
	}
}
