using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class AnalogTVPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public AnalogTVBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<AnalogTVBehaviour>.Create(graph, template);
		}
	}
}
