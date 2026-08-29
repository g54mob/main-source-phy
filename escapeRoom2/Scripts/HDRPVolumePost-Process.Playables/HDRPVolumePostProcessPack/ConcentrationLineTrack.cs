using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(ConcentrationLinePlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class ConcentrationLineTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<ConcentrationLineMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
