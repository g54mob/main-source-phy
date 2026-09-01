using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Infrastructure.URP
{
	[Serializable]
	public sealed class LayerEnumParameter : VolumeParameter<LayerMask>
	{
		public LayerEnumParameter(LayerMask value, bool overrideState = false)
		{
		}
	}
}
