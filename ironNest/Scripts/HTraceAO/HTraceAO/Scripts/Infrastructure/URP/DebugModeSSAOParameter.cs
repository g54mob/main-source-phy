using System;
using HTraceAO.Scripts.Globals;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Infrastructure.URP
{
	[Serializable]
	public sealed class DebugModeSSAOParameter : VolumeParameter<DebugModeSSAO>
	{
		public DebugModeSSAOParameter(DebugModeSSAO value, bool overrideState = false)
		{
		}
	}
}
