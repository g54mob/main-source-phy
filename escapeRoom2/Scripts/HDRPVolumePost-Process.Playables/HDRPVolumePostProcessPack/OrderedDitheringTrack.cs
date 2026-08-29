using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(OrderedDitheringPlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class OrderedDitheringTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<OrderedDitheringMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
