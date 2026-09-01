using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

namespace FMODUnity
{
	[Serializable]
	public class FMODEventPlayableBehavior : PlayableBehaviour
	{
		public class EventArgs : System.EventArgs
		{
			public EventInstance eventInstance { get; set; }
		}

		[FormerlySerializedAs("eventReference")]
		public EventReference EventReference;

		[FormerlySerializedAs("stopType")]
		public STOP_MODE StopType;

		[FormerlySerializedAs("parameters")]
		[NotKeyable]
		public ParamRef[] Parameters;

		[FormerlySerializedAs("parameterLinks")]
		public List<ParameterAutomationLink> ParameterLinks;

		[NonSerialized]
		public GameObject TrackTargetObject;

		[NonSerialized]
		public TimelineClip OwningClip;

		[FormerlySerializedAs("parameterAutomation")]
		public AutomatableSlots ParameterAutomation;

		private bool isPlayheadInside;

		private EventInstance eventInstance;

		public float ClipStartTime { get; private set; }

		public float CurrentVolume { get; private set; }

		public static event EventHandler<EventArgs> Enter
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

		public static event EventHandler<EventArgs> Exit
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

		public static event EventHandler<EventArgs> GraphStop
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

		protected void PlayEvent()
		{
		}

		protected virtual void OnEnter()
		{
		}

		protected virtual void OnExit()
		{
		}

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}

		public void UpdateBehavior(float time, float volume)
		{
		}

		public override void OnGraphStop(Playable playable)
		{
		}
	}
}
