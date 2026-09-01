using System;
using HTraceAO.Scripts.Globals;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Infrastructure.URP
{
	[Serializable]
	public sealed class UpscalingQualityParameter : VolumeParameter<UpscalingQuality>
	{
		public UpscalingQualityParameter(UpscalingQuality value, bool overrideState = false)
		{
		}
	}
}
