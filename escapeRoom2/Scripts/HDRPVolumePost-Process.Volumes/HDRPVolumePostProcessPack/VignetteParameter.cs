using System;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class VignetteParameter : VolumeParameter<VignetteSetting>
	{
		public VignetteParameter(VignetteSetting value, bool overrideState)
			: base(value, overrideState)
		{
		}
	}
}
