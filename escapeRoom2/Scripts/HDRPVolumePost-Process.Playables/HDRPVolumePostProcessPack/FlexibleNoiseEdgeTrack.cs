using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(FlexibleNoiseEdgePlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class FlexibleNoiseEdgeTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			foreach (TimelineClip clip in GetClips())
			{
				(clip.asset as FlexibleNoiseEdgePlayableAsset).clipPassthrough = clip;
			}
			return ScriptPlayable<FlexibleNoiseEdgeMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
