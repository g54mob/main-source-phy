using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(ParticleSystem))]
	public class MMRuntimeParticleControl : MonoBehaviour
	{
		public enum TrackerModes
		{
			Basic = 0,
			ForcedBounds = 1
		}

		[Header("Base Controls")]
		[MMInspectorButton("Play")]
		public bool PlayButton;

		[MMInspectorButton("Pause")]
		public bool PauseButton;

		[MMInspectorButton("Stop")]
		public bool StopButton;

		[Header("Simulate")]
		public float TargetTimestamp;

		[MMInspectorButton("Simulate")]
		public bool FastForwardToTimeButton;

		[Header("Tracker")]
		public TrackerModes TrackerMode;

		[MMEnumCondition("TrackerMode", new int[] { 1 })]
		public float MinBound;

		[MMEnumCondition("TrackerMode", new int[] { 1 })]
		public float MaxBound;

		[Range(0f, 1f)]
		public float Tracker;

		[MMReadOnly]
		public float Timestamp;

		protected ParticleSystem _particleSystem;

		protected ParticleSystem.MainModule _mainModule;

		protected virtual void Awake()
		{
		}

		protected virtual void Play()
		{
		}

		protected virtual void Pause()
		{
		}

		protected virtual void Stop()
		{
		}

		protected virtual void Simulate()
		{
		}

		protected void OnValidate()
		{
		}
	}
}
