using UnityEngine;

namespace HTraceAO.Scripts.Globals
{
	public enum HBuffer
	{
		[InspectorName("Multi")]
		Multi = 0,
		[InspectorName("Depth")]
		Depth = 1,
		[InspectorName("Normal")]
		Normal = 3,
		[InspectorName("Motion Mask")]
		MotionMask = 4,
		[InspectorName("Motion Vectors")]
		MotionVectors = 5
	}
}
