using System;
using UnityEngine;
using UnityEngine.Playables;

namespace FMODUnity
{
	[Serializable]
	public class FMODEventMixerBehaviour : PlayableBehaviour
	{
		[Range(0f, 1f)]
		public float volume;

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}
	}
}
