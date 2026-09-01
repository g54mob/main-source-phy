using UnityEngine;

namespace HTraceAO.Scripts.Globals
{
	public enum DebugModeGTAO
	{
		[InspectorName("None")]
		None = 0,
		[InspectorName("Main Buffers")]
		MainBuffers = 1,
		[InspectorName("Ambient Occlusion")]
		AmbientOcclusion = 2,
		[InspectorName("Temporal Disocclusion")]
		TemporalDisocclusion = 3
	}
}
