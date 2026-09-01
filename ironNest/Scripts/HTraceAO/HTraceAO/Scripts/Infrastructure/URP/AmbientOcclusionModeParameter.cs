using System;
using HTraceAO.Scripts.Globals;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Infrastructure.URP
{
	[Serializable]
	public sealed class AmbientOcclusionModeParameter : VolumeParameter<AmbientOcclusionMode>
	{
		public AmbientOcclusionModeParameter(AmbientOcclusionMode value, bool overrideState = false)
		{
		}
	}
}
