using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Sequencing/MMSequencer")]
	public class MMSequencer : MonoBehaviour
	{
		public enum TimeScales
		{
			Scaled = 0,
			Unscaled = 1
		}

		[Header("Sequence")]
		[Tooltip("the sequence to design on or to play")]
		public MMSequence Sequence;

		[Tooltip("the intended BPM for playback and design")]
		public int BPM;

		[Tooltip("the number of notes in the sequence")]
		public int SequencerLength;

		[Header("Playback")]
		[Tooltip("the timescale on which this sequencer should play")]
		public TimeScales TimeScale;

		[Tooltip("whether the sequence should loop or not when played back")]
		public bool Loop;

		[Tooltip("if this is true the sequence will play in random order")]
		public bool RandomSequence;

		[Tooltip("whether that sequencer should start playing on application start")]
		public bool PlayOnStart;

		[Header("Metronome")]
		[Tooltip("a sound to play every beat")]
		public AudioClip MetronomeSound;

		[Tooltip("the volume of the metronome sound")]
		[Range(0f, 1f)]
		public float MetronomeVolume;

		[Header("Events")]
		[Tooltip("a list of events to play every time an active beat is found on each track (one event per track)")]
		public List<UnityEvent> TrackEvents;

		[Header("Monitor")]
		[Tooltip("true if the sequencer is playing right now")]
		[MMFReadOnly]
		public bool Playing;

		[Tooltip("true if the sequencer has been played once")]
		[HideInInspector]
		public bool PlayedOnce;

		[Tooltip("true if a perfect beat was found this frame")]
		[MMFReadOnly]
		public bool BeatThisFrame;

		[Tooltip("the index of the last played bit (our position in the playing sequence)")]
		[MMFReadOnly]
		public int LastBeatIndex;

		[HideInInspector]
		public int LastBPM;

		[HideInInspector]
		public int LastTracksCount;

		[HideInInspector]
		public int LastSequencerLength;

		[HideInInspector]
		public MMSequence LastSequence;

		[HideInInspector]
		public int CurrentSequenceIndex;

		[HideInInspector]
		public float LastBeatTimestamp;

		protected float _beatInterval;

		protected AudioSource _beatSoundAudiosource;

		public float InternalTime => 0f;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void ToggleSequence()
		{
		}

		public virtual void PlaySequence()
		{
		}

		public virtual void StopSequence()
		{
		}

		public virtual void ClearSequence()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void HandleBeat()
		{
		}

		public virtual void PlayBeat()
		{
		}

		protected virtual void OnBeat()
		{
		}

		public virtual void PlayTrackEvent(int index)
		{
		}

		public virtual void ToggleActive(int trackIndex)
		{
		}

		public virtual void ToggleStep(int stepIndex)
		{
		}

		protected virtual void PlayMetronomeSound()
		{
		}

		public virtual void IncrementLength()
		{
		}

		public virtual void DecrementLength()
		{
		}

		public virtual void UpdateTimestampsToMatchNewBPM()
		{
		}

		public virtual void ApplySequencerLengthToSequence()
		{
		}

		public virtual void EditorMaintenance()
		{
		}

		public virtual void SetupTrackEvents()
		{
		}
	}
}
