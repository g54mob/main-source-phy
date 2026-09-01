using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Serialization;

namespace FMODUnity
{
	[AddComponentMenu("FMOD Studio/FMOD Studio Event Emitter")]
	public class StudioEventEmitter : EventHandler
	{
		public EventReference EventReference;

		[Obsolete("Use the EventReference field instead")]
		public string Event;

		[FormerlySerializedAs("PlayEvent")]
		public EmitterGameEvent EventPlayTrigger;

		[FormerlySerializedAs("StopEvent")]
		public EmitterGameEvent EventStopTrigger;

		public bool AllowFadeout;

		public bool TriggerOnce;

		public bool Preload;

		[FormerlySerializedAs("AllowNonRigidbodyDoppler")]
		public bool NonRigidbodyVelocity;

		public ParamRef[] Params;

		public bool OverrideAttenuation;

		public float OverrideMinDistance;

		public float OverrideMaxDistance;

		protected EventDescription eventDescription;

		protected EventInstance instance;

		private bool hasTriggered;

		private bool isQuitting;

		private bool isOneshot;

		private List<ParamRef> cachedParams;

		private static List<StudioEventEmitter> activeEmitters;

		private const string SnapshotString = "snapshot";

		[Obsolete("Use the EventPlayTrigger field instead")]
		public EmitterGameEvent PlayEvent
		{
			get
			{
				return default(EmitterGameEvent);
			}
			set
			{
			}
		}

		[Obsolete("Use the EventStopTrigger field instead")]
		public EmitterGameEvent StopEvent
		{
			get
			{
				return default(EmitterGameEvent);
			}
			set
			{
			}
		}

		public EventDescription EventDescription => default(EventDescription);

		public EventInstance EventInstance => default(EventInstance);

		public bool IsActive { get; private set; }

		private float MaxDistance => 0f;

		public static void UpdateActiveEmitters()
		{
		}

		private static void RegisterActiveEmitter(StudioEventEmitter emitter)
		{
		}

		private static void DeregisterActiveEmitter(StudioEventEmitter emitter)
		{
		}

		private void UpdatePlayingStatus(bool force = false)
		{
		}

		protected override void Start()
		{
		}

		private void OnApplicationQuit()
		{
		}

		protected override void OnDestroy()
		{
		}

		protected override void HandleGameEvent(EmitterGameEvent gameEvent)
		{
		}

		private void Lookup()
		{
		}

		public void Play()
		{
		}

		private void PlayInstance()
		{
		}

		public void Stop()
		{
		}

		private void StopInstance()
		{
		}

		public void SetParameter(string name, float value, bool ignoreseekspeed = false)
		{
		}

		public void SetParameter(PARAMETER_ID id, float value, bool ignoreseekspeed = false)
		{
		}

		public bool IsPlaying()
		{
			return false;
		}
	}
}
