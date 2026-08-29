using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(AnalogTVPlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class AnalogTVTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<AnalogTVMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
