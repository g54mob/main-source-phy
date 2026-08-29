using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class KuwaharaFilterBehaviour : PlayableBehaviour
	{
		[Range(2f, 8f)]
		public float intensity;
	}
}
