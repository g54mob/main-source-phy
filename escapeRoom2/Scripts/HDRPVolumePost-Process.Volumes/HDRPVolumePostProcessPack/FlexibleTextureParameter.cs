using System;
using UnityEngine.Rendering;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class FlexibleTextureParameter : VolumeParameter<TextureSetting>
	{
		public FlexibleTextureParameter(TextureSetting value, bool overrideState)
			: base(value, overrideState)
		{
		}
	}
}
