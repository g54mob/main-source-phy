using System;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public abstract class GaussianBlurBehaviourBase : PlayableBehaviour
	{
		public float intensity;

		public float sampleCount = 8f;

		public float sampleInterval = 1f;

		public float standardDeviation = 4f;

		public float brightness;
	}
}
