using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class KuwaharaFilterPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public KuwaharaFilterBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<KuwaharaFilterBehaviour>.Create(graph, template);
		}
	}
}
