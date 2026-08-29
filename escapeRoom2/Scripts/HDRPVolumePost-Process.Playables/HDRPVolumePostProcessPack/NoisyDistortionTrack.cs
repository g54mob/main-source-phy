using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(NoisyDistortionPlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class NoisyDistortionTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<NoisyDistortionMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
