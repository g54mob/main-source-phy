using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(CellularNoisePlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class CellularNoiseTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<CellularNoiseMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
