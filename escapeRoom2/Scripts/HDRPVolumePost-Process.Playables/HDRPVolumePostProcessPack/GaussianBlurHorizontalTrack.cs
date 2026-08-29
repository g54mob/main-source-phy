using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(GaussianBlurHorizontalPlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class GaussianBlurHorizontalTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<GaussianBlurHoriaontalMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
