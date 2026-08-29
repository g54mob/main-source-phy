using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class ConcentrationLinePlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public ConcentrationLineBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<ConcentrationLineBehaviour>.Create(graph, template);
		}
	}
}
