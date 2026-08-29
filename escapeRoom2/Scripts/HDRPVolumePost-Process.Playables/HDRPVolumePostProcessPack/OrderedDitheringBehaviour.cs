using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class OrderedDitheringBehaviour : PlayableBehaviour
	{
		[Range(0f, 1f)]
		public float intensity;

		[Range(2f, 256f)]
		public int colorStep;

		[Range(0f, 63f)]
		public int ditherThreshold;
	}
}
