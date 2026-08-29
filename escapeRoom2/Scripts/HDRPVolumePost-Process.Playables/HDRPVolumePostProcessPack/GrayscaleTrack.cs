using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(GrayscalePlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class GrayscaleTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<GrayscaleMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
