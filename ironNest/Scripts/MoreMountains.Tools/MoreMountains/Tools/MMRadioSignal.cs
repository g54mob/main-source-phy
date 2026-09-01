using UnityEngine;

namespace MoreMountains.Tools
{
	public abstract class MMRadioSignal : MonoBehaviour
	{
		public enum SignalModes
		{
			OneTime = 0,
			Persistent = 1,
			Driven = 2
		}

		public enum TimeScales
		{
			Unscaled = 0,
			Scaled = 1
		}

		[Header("Signal")]
		public SignalModes SignalMode;

		public TimeScales TimeScale;

		public float Duration;

		public float GlobalMultiplier;

		[MMReadOnly]
		public float CurrentLevel;

		[Header("Play Settings")]
		[MMReadOnly]
		public bool Playing;

		[Range(0f, 1f)]
		public float DriverTime;

		public bool PlayOnStart;

		public MMRadioSignalOnValueChange OnValueChange;

		[Header("Debug")]
		[MMInspectorButton("StartShaking")]
		public bool StartShakingButton;

		protected float _signalTime;

		protected float _shakeStartedTimestamp;

		protected float _levelLastFrame;

		public virtual float Level => 0f;

		public virtual float TimescaleTime => 0f;

		public virtual float TimescaleDeltaTime => 0f;

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void StartShaking()
		{
		}

		protected virtual void ShakeStarts()
		{
		}

		protected virtual void Update()
		{
		}

		public virtual void ApplyLevel(float level)
		{
		}

		protected virtual void ProcessDrivenMode()
		{
		}

		protected virtual void ProcessUpdate()
		{
		}

		protected virtual void Shake()
		{
		}

		public virtual float GraphValue(float time)
		{
			return 0f;
		}

		protected virtual void ShakeComplete()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDestroy()
		{
		}

		protected virtual void OnDisable()
		{
		}

		public virtual void Play()
		{
		}

		public virtual void Stop()
		{
		}

		public virtual float ApplyBias(float t, float bias)
		{
			return 0f;
		}
	}
}
