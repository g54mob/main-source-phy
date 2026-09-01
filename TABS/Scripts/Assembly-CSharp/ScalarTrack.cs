using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(ScalerAsset))]
[TrackBindingType(typeof(Transform))]
public class ScalarTrack : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		return ScriptPlayable<ScalarMixerBehaviour>.Create(graph, inputCount);
	}
}
