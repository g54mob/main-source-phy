using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class SimpleRipplePlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public SimpleRippleBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<SimpleRippleBehaviour>.Create(graph, template);
		}
	}
}
