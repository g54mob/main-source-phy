using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class FlexibleNoiseEdgeBehaviour : PlayableBehaviour
	{
		[NonSerialized]
		public TimelineClip Clip;

		[Range(0f, 1f)]
		public float masterVolume;

		[Range(0f, 1f)]
		[NonReorderable]
		public float vignetteIntensity = 0.5f;

		public Vector2 vignetteCenterUV = Vector2.one * 0.5f;

		public float vignetteAspectRatio = 1.6f;

		public float vignetteGradationPower = 0.2f;

		public Vector2 surfaceTextureScrollVector = Vector2.zero;
	}
}
