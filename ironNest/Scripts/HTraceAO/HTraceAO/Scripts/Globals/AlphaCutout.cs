using UnityEngine;

namespace HTraceAO.Scripts.Globals
{
	public enum AlphaCutout
	{
		[InspectorName("Evaluate")]
		[Tooltip("Materials will accurately evaluate alpha cutout on hit.")]
		Evaluate = 0,
		[InspectorName("DepthTest")]
		[Tooltip("Alpha cutout will be evaluated in screen space against the depth buffer.")]
		DepthTest = 1
	}
}
