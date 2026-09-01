using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public class MMShaker : MMMonoBehaviour
	{
		[MMInspectorGroup("Shaker Settings", true, 3, false)]
		[Tooltip("whether to listen on a channel defined by an int or by a MMChannel scriptable object. Ints are simple to setup but can get messy and make it harder to remember what int corresponds to what. MMChannel scriptable objects require you to create them in advance, but come with a readable name and are more scalable")]
		public MMChannelModes ChannelMode;

		[Tooltip("the channel to listen to - has to match the one on the feedback")]
		[MMEnumCondition("ChannelMode", new int[] { 0 })]
		public int Channel;

		[Tooltip("the MMChannel definition asset to use to listen for events. The feedbacks targeting this shaker will have to reference that same MMChannel definition to receive events - to create a MMChannel, right click anywhere in your project (usually in a Data folder) and go MoreMountains > MMChannel, then name it with some unique name")]
		[MMEnumCondition("ChannelMode", new int[] { 1 })]
		public MMChannel MMChannelDefinition;

		[Tooltip("the duration of the shake, in seconds")]
		public float ShakeDuration;

		[Tooltip("if this is true this shaker will play on awake")]
		public bool PlayOnAwake;

		[Tooltip("if this is true, the shaker will shake permanently as long as its game object is active")]
		public bool PermanentShake;

		[Tooltip("if this is true, a new shake can happen while shaking")]
		public bool Interruptible;

		[Tooltip("if this is true, this shaker will always reset target values, regardless of how it was called")]
		public bool AlwaysResetTargetValuesAfterShake;

		[Tooltip("if this is true, this shaker will ignore any value passed in an event that triggered it, and will instead use the values set on its inspector")]
		public bool OnlyUseShakerValues;

		[Tooltip("a cooldown, in seconds, after a shake, during which no other shake can start")]
		public float CooldownBetweenShakes;

		[Tooltip("whether or not this shaker is shaking right now")]
		[MMFReadOnly]
		public bool Shaking;

		[HideInInspector]
		public bool ForwardDirection;

		[HideInInspector]
		public TimescaleModes TimescaleMode;

		[HideInInspector]
		internal bool _listeningToEvents;

		protected float _shakeStartedTimestamp;

		protected float _remappedTimeSinceStart;

		protected bool _resetShakerValuesAfterShake;

		protected bool _resetTargetValuesAfterShake;

		protected float _journey;

		public virtual MMChannelData ChannelData => null;

		public virtual bool ListeningToEvents => false;

		public virtual float GetTime()
		{
			return 0f;
		}

		public virtual float GetDeltaTime()
		{
			return 0f;
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void ForceInitialization()
		{
		}

		public virtual void StartShaking()
		{
		}

		protected virtual void ShakeStarts()
		{
		}

		protected virtual void GrabInitialValues()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void Shake()
		{
		}

		protected virtual float ShakeFloat(AnimationCurve curve, float remapMin, float remapMax, bool relativeIntensity, float initialValue)
		{
			return 0f;
		}

		protected virtual Color ShakeGradient(Gradient gradient)
		{
			return default(Color);
		}

		protected virtual void ResetTargetValues()
		{
		}

		protected virtual void ResetShakerValues()
		{
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

		public virtual void StartListening()
		{
		}

		public virtual void StopListening()
		{
		}

		protected virtual bool CheckEventAllowed(MMChannelData channelData, bool useRange = false, float range = 0f, Vector3 eventOriginPosition = default(Vector3))
		{
			return false;
		}

		public virtual float ComputeRangeIntensity(bool useRange, float rangeDistance, bool useRangeFalloff, AnimationCurve rangeFalloff, Vector2 remapRangeFalloff, Vector3 rangePosition)
		{
			return 0f;
		}
	}
}
