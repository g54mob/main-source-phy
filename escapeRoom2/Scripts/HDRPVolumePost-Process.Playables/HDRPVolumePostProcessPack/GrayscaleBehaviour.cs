using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class GrayscaleBehaviour : PlayableBehaviour
	{
		[Range(0f, 1f)]
		public float intensity;
	}
}
