using UnityEngine;

namespace HTraceAO.Scripts.Globals
{
	public enum DebugModeSSAO
	{
		[InspectorName("None")]
		None = 0,
		[InspectorName("Main Buffers")]
		MainBuffers = 1,
		[InspectorName("Ambient Occlusion")]
		AmbientOcclusion = 2
	}
}
