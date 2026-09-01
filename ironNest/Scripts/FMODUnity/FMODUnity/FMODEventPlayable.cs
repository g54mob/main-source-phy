using System;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

namespace FMODUnity
{
	[Serializable]
	public class FMODEventPlayable : PlayableAsset, ITimelineClipAsset
	{
		[FormerlySerializedAs("template")]
		public FMODEventPlayableBehavior Template;

		[FormerlySerializedAs("eventLength")]
		public float EventLength;

		[Obsolete("Use the eventReference field instead")]
		[SerializeField]
		public string eventName;

		[FormerlySerializedAs("eventReference")]
		[SerializeField]
		public EventReference EventReference;

		[FormerlySerializedAs("stopType")]
		[SerializeField]
		public STOP_MODE StopType;

		[FormerlySerializedAs("parameters")]
		[SerializeField]
		public ParamRef[] Parameters;

		[NonSerialized]
		public bool CachedParameters;

		private FMODEventPlayableBehavior behavior;

		public GameObject TrackTargetObject { get; set; }

		public override double duration => 0.0;

		public ClipCaps clipCaps => default(ClipCaps);

		public TimelineClip OwningClip { get; set; }

		public static event EventHandler<EventArgs> OnCreatePlayable
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public void LinkParameters(EventDescription eventDescription)
		{
		}

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return default(Playable);
		}
	}
}
