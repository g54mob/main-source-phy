using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class FlexibleNoiseEdgePlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		[NonSerialized]
		public TimelineClip clipPassthrough;

		public FlexibleNoiseEdgeBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			template.Clip = clipPassthrough;
			return ScriptPlayable<FlexibleNoiseEdgeBehaviour>.Create(graph, template);
		}
	}
}
