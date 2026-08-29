using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(SketchPlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class SketchTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<SketchMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
