using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[Serializable]
	public class CellularNoisePlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		public CellularNoiseBehaviour template;

		public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<CellularNoiseBehaviour>.Create(graph, template);
		}
	}
}
