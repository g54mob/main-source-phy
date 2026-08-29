using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public abstract class VignettableBehaviourBase : PlayableBehaviour
	{
		[Range(0f, 1f)]
		public float masterVolume;

		[Range(0f, 1f)]
		public float vignetteIntensity = 0.5f;

		public Vector2 vignetteCenterUV = Vector2.one * 0.5f;

		public float vignetteAspectRatio = 1.6f;

		public float vignetteGradationPower = 0.2f;

		public Color vignetteColor = Color.black;

		public float maskRotation;

		public int maskAlphaSign;

		public float maskReductionRatio = 30f;

		public Vector2 maskScale = Vector2.one;
	}
}
