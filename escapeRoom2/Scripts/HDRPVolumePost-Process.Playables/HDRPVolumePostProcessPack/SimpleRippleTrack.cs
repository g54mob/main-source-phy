using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(SimpleRipplePlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class SimpleRippleTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<SimpleRippleMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
