using System;
using HTraceAO.Scripts.Globals;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Infrastructure.URP
{
	[Serializable]
	public sealed class DebugModeRTAOParameter : VolumeParameter<DebugModeRTAO>
	{
		public DebugModeRTAOParameter(DebugModeRTAO value, bool overrideState = false)
		{
		}
	}
}
