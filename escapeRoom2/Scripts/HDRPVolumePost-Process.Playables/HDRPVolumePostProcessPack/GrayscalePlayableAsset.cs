using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class GrayscalePlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public GrayscaleBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<GrayscaleBehaviour>.Create(graph, template);
		}
	}
}
