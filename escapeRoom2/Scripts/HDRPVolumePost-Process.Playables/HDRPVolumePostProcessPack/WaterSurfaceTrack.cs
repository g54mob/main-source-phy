using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace HDRPVolumePostProcessPack
{
	[TrackClipType(typeof(WaterSurfacePlayableAsset))]
	[TrackBindingType(typeof(Volume))]
	public class WaterSurfaceTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<WaterSurfaceMixerBehaviour>.Create(graph, inputCount);
		}
	}
}
