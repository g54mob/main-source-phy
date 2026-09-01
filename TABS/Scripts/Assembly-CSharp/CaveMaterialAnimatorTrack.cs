using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(CaveMaterialAnimatorAsset))]
[TrackBindingType(typeof(Material))]
public class CaveMaterialAnimatorTrack : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		return ScriptPlayable<CaveMaterialAnimatorMixerBehaviour>.Create(graph, inputCount);
	}
}
