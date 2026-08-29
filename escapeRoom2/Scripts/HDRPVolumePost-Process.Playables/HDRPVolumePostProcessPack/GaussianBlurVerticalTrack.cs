using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(GaussianBlurVerticalPlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class GaussianBlurVerticalTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<GaussianBlurVerticalMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
