using System;
using HTraceAO.Scripts.Globals;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Infrastructure.URP
{
	[Serializable]
	public sealed class HBufferParameter : VolumeParameter<HBuffer>
	{
		public HBufferParameter(HBuffer value, bool overrideState = false)
		{
		}
	}
}
