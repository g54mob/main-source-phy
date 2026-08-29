using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class NoisyDistortionPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public NoisyDistortionBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<NoisyDistortionBehaviour>.Create(graph, template);
		}
	}
}
