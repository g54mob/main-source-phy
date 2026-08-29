using System;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class FlexibleNoiseParameter : VolumeParameter<FlexibleNoiseSetting>
	{
		public FlexibleNoiseParameter(FlexibleNoiseSetting value, bool overrideState)
			: base(value, overrideState)
		{
		}
	}
}
