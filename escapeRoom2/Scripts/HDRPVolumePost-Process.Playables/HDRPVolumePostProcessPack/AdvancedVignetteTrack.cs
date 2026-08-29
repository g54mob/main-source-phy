using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(AdvancedVignettePlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class AdvancedVignetteTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<AdvancedVignetteMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
