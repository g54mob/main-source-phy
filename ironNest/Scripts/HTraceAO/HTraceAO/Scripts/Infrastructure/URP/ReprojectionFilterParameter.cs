using System;
using HTraceAO.Scripts.Globals;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Infrastructure.URP
{
	[Serializable]
	public sealed class ReprojectionFilterParameter : VolumeParameter<ReprojectionFilter>
	{
		public ReprojectionFilterParameter(ReprojectionFilter value, bool overrideState = false)
		{
		}
	}
}
