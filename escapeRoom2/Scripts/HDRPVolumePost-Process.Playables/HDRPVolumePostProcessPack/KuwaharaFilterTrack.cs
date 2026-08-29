using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(KuwaharaFilterPlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class KuwaharaFilterTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<KuwaharaFilterMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
