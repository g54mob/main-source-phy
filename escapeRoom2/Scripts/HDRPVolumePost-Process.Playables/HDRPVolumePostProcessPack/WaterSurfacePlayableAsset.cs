using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class WaterSurfacePlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public WaterSurfaceBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<WaterSurfaceBehaviour>.Create(graph, template);
		}
	}
}
