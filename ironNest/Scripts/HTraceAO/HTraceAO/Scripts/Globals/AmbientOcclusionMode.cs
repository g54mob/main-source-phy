using UnityEngine;

namespace HTraceAO.Scripts.Globals
{
	public enum AmbientOcclusionMode
	{
		[InspectorName("SSAO")]
		[Tooltip("Screen Space Ambient Occlusion")]
		SSAO = 0,
		[InspectorName("GTAO")]
		[Tooltip("Ground Truth Ambient Occlusion")]
		GTAO = 1,
		[InspectorName("RTAO")]
		[Tooltip("Ray Traced Ambient Occlusion")]
		RTAO = 2
	}
}
