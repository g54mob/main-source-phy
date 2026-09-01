using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace FMODUnity
{
	[TrackColor(0.066f, 0.134f, 0.244f)]
	[TrackClipType(typeof(FMODEventPlayable))]
	[TrackBindingType(typeof(GameObject))]
	[DisplayName("FMOD/Event Track")]
	public class FMODEventTrack : TrackAsset
	{
		public FMODEventMixerBehaviour template;

		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return default(Playable);
		}
	}
}
