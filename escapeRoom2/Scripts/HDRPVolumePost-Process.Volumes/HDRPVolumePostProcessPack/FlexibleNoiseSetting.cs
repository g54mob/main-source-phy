using UnityEngine;

namespace HDRPVolumePostProcessPack
{
	[CreateAssetMenu(menuName = "HDRP Volume PostProcess Pack/Create Flexible Noise", fileName = "FlexibleNoiseSetting")]
	public class FlexibleNoiseSetting : ScriptableObject
	{
		public VignetteSetting vignette;

		public TextureSetting surface;

		public float offsetTime;

		public float surfaceThreshold = 0.4f;

		public float edgeThreshold = 0.2f;

		public Color edgeColor;

		public TextureSetting noise;

		public void Interp(FlexibleNoiseSetting stateParam, FlexibleNoiseSetting toParam, float interpFactor)
		{
			vignette.intensity = Mathf.Lerp(0f, stateParam.vignette.intensity, interpFactor);
		}

		public void Copy(FlexibleNoiseSetting src)
		{
		}
	}
}
