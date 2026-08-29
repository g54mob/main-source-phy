using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class GaussianBlurHorizontalPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public GaussianBlurHorizontalBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<GaussianBlurHorizontalBehaviour>.Create(graph, template);
		}
	}
}
