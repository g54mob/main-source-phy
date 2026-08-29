using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class NoisyDistortionBehaviour : PlayableBehaviour
	{
		[Range(0f, 1f)]
		public float intensity;

		public Vector2 runtimeNoiseScale = Vector2.zero;

		public Vector2 runtimeNoiseVector = Vector2.zero;

		[Range(0f, 1f)]
		public float trancateNoiseValue;
	}
}
