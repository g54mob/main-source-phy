using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class SimpleRippleBehaviour : PlayableBehaviour
	{
		[Range(0f, 1f)]
		public float masterVolume;

		[Range(0f, 10f)]
		public float intensity;

		[Range(0f, 100f)]
		public float frequency = 10f;

		[Range(0f, 100f)]
		public float propagation;

		public Vector2 rippleCenterUV = Vector2.one * 0.5f;
	}
}
