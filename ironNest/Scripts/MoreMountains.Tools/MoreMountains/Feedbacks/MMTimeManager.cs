using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Various/MMTimeManager")]
	public class MMTimeManager : MMSingleton<MMTimeManager>
	{
		[Header("Default Values")]
		[Tooltip("The reference time scale, to which the system will go back to after all time is changed")]
		public float NormalTimeScale;

		[Header("Impacted Values")]
		[Tooltip("whether or not to update Time.timeScale when changing time scale")]
		public bool UpdateTimescale;

		[Tooltip("whether or not to update Time.fixedDeltaTime when changing time scale")]
		public bool UpdateFixedDeltaTime;

		[Tooltip("whether or not to update Time.maximumDeltaTime when changing time scale")]
		public bool UpdateMaximumDeltaTime;

		[Header("Debug")]
		[Tooltip("the current, real time, time scale")]
		[MMReadOnly]
		public float CurrentTimeScale;

		[Tooltip("the time scale the system is lerping towards")]
		[MMReadOnly]
		public float TargetTimeScale;

		[MMInspectorButton("TestButtonToSlowDownTime")]
		public bool TestButton;

		protected Stack<TimeScaleProperties> _timeScaleProperties;

		protected TimeScaleProperties _currentProperty;

		protected TimeScaleProperties _resetProperty;

		protected float _initialFixedDeltaTime;

		protected float _initialMaximumDeltaTime;

		protected float _startedAt;

		protected bool _lerpingBackToNormal;

		protected float _timeScaleLastTime;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		protected static void InitializeStatics()
		{
		}

		protected virtual void TestButtonToSlowDownTime()
		{
		}

		protected override void Awake()
		{
		}

		public virtual void PreInitialization()
		{
		}

		protected virtual void Start()
		{
		}

		public virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void ApplyTimeScale(float newValue)
		{
		}

		protected virtual void SetTimeScale(float newTimeScale)
		{
		}

		protected virtual void SetTimeScale(TimeScaleProperties timeScaleProperties)
		{
		}

		protected virtual void ResetTimeScale()
		{
		}

		protected virtual void Unfreeze()
		{
		}

		public virtual void SetTimeScaleTo(float newNormalTimeScale)
		{
		}

		public virtual void OnTimeScaleEvent(MMTimeScaleMethods timeScaleMethod, float timeScale, float duration, bool lerp, float lerpSpeed, bool infinite, MMTimeScaleLerpModes timeScaleLerpMode = MMTimeScaleLerpModes.Speed, MMTweenType timeScaleLerpCurve = null, float timeScaleLerpDuration = 0.2f, bool timeScaleLerpOnReset = false, MMTweenType timeScaleLerpCurveOnReset = null, float timeScaleLerpDurationOnReset = 0.2f)
		{
		}

		public virtual void OnMMFreezeFrameEvent(float duration)
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}
	}
}
