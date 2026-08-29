using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class GaussianBlurVerticalPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public GaussianBlurVerticalBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<GaussianBlurVerticalBehaviour>.Create(graph, template);
		}
	}
}
