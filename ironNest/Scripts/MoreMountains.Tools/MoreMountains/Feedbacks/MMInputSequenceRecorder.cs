using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Sequencing/MMInputSequenceRecorder")]
	[ExecuteAlways]
	public class MMInputSequenceRecorder : MonoBehaviour
	{
		[Header("Target")]
		[Tooltip("the target scriptable object to write to")]
		public MMSequence SequenceScriptableObject;

		[Header("Recording")]
		[MMFReadOnly]
		[Tooltip("whether this recorder is recording right now or not")]
		public bool Recording;

		[Tooltip("whether any silence between the start of the recording and the first press should be removed or not")]
		public bool RemoveInitialSilence;

		[Tooltip("whether this recording should write on top of existing entries or not")]
		public bool AdditiveRecording;

		[Tooltip("whether this recorder should start recording when entering play mode")]
		public bool StartRecordingOnGameStart;

		[Tooltip("the offset to apply to entries")]
		public float RecordingStartOffset;

		[Header("Recorder Keys")]
		[Tooltip("the key binding for recording start")]
		public KeyCode StartRecordingHotkey;

		[Tooltip("the key binding for recording stop")]
		public KeyCode StopRecordingHotkey;

		protected MMSequenceNote _note;

		protected float _recordingStartedAt;

		protected virtual void Awake()
		{
		}

		public virtual void Initialization()
		{
		}

		protected virtual void Start()
		{
		}

		public virtual void StartRecording()
		{
		}

		public virtual void StopRecording()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void DetectStartAndEnd()
		{
		}

		protected virtual void DetectRecording()
		{
		}

		public virtual void AddNoteToTrack(MMSequenceTrack track)
		{
		}
	}
}
