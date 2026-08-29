using System;
using UnityEngine;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class VignetteSetting
	{
		public const float MIN_ASPECT = 0.1f;

		public const float MAX_ASPECT = 10f;

		public const float MIN_MASK_REDUCTION_RADIO = 0f;

		public const float MAX_MASK_REDUCTION_RADIO = 50f;

		[Range(0f, 1f)]
		public float intensity = 0.5f;

		public Vector2 centerUV = Vector2.one * 0.5f;

		[Range(0.1f, 10f)]
		public float aspectRatio = 1f;

		[Range(0f, 1f)]
		public float gradationPower = 0.3f;

		public Color color = Color.black;

		public AdvancedVignetteType vignetteMode;

		[Header("Mask Texture Setting")]
		public Texture2D maskTexture;

		public float maskRotation;

		[Range(0f, 1f)]
		public int inverseAlpha;

		[Range(0f, 50f)]
		public float maskReductionRatio = 30f;

		public Vector2 maskScale = Vector2.one;

		public VignetteSetting()
		{
		}

		public VignetteSetting(VignetteSetting src)
		{
			intensity = src.intensity;
			centerUV = src.centerUV;
			aspectRatio = src.aspectRatio;
			gradationPower = src.gradationPower;
			color = src.color;
			vignetteMode = src.vignetteMode;
			maskTexture = src.maskTexture;
			maskRotation = src.maskRotation;
			inverseAlpha = src.inverseAlpha;
			maskScale = src.maskScale;
			maskReductionRatio = src.maskReductionRatio;
		}

		public void Interp(VignetteSetting stateParam, VignetteSetting toParam, float interpFactor)
		{
			intensity = Mathf.Lerp(0f, stateParam.intensity, interpFactor);
		}
	}
}
