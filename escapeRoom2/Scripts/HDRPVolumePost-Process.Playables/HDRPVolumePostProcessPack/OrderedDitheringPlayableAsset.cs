using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class OrderedDitheringPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public OrderedDitheringBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<OrderedDitheringBehaviour>.Create(graph, template);
		}
	}
}
