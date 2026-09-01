using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	[MMRequiresConstantRepaint]
	[AddComponentMenu("More Mountains/Tools/GUI/MMProgressBar")]
	public class MMProgressBar : MMMonoBehaviour
	{
		public enum MMProgressBarStates
		{
			Idle = 0,
			Decreasing = 1,
			Increasing = 2,
			InDecreasingDelay = 3,
			InIncreasingDelay = 4
		}

		public enum FillModes
		{
			LocalScale = 0,
			FillAmount = 1,
			Width = 2,
			Height = 3,
			Anchor = 4
		}

		public enum BarDirections
		{
			LeftToRight = 0,
			RightToLeft = 1,
			UpToDown = 2,
			DownToUp = 3
		}

		public enum TimeScales
		{
			UnscaledTime = 0,
			Time = 1
		}

		public enum BarFillModes
		{
			SpeedBased = 0,
			FixedDuration = 1
		}

		[CompilerGenerated]
		private sealed class _003CBumpCoroutine_003Ed__125 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMProgressBar _003C_003E4__this;

			public float intensityMultiplier;

			private float _003Cjourney_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CBumpCoroutine_003Ed__125(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CHideBarCo_003Ed__128 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public MMProgressBar _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CHideBarCo_003Ed__128(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CUpdateBarsCo_003Ed__118 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMProgressBar _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CUpdateBarsCo_003Ed__118(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[MMInspectorGroup("Bindings", true, 10, false)]
		[Tooltip("optional - the ID of the player associated to this bar")]
		public string PlayerID;

		[Tooltip("the main, foreground bar")]
		public Transform ForegroundBar;

		[Tooltip("the delayed bar that will show when moving from a value to a new, lower value")]
		[FormerlySerializedAs("DelayedBar")]
		public Transform DelayedBarDecreasing;

		[Tooltip("the delayed bar that will show when moving from a value to a new, higher value")]
		public Transform DelayedBarIncreasing;

		[MMInspectorGroup("Fill Settings", true, 11, false)]
		[FormerlySerializedAs("StartValue")]
		[Range(0f, 1f)]
		[Tooltip("the local scale or fillamount value to reach when the value associated to the bar is at 0%")]
		public float MinimumBarFillValue;

		[FormerlySerializedAs("EndValue")]
		[Range(0f, 1f)]
		[Tooltip("the local scale or fillamount value to reach when the bar is full")]
		public float MaximumBarFillValue;

		[Tooltip("whether or not to initialize the value of the bar on start")]
		public bool SetInitialFillValueOnStart;

		[MMCondition("SetInitialFillValueOnStart", true)]
		[Range(0f, 1f)]
		[Tooltip("the initial value of the bar")]
		public float InitialFillValue;

		[Tooltip("the direction this bar moves to")]
		public BarDirections BarDirection;

		[Tooltip("the foreground bar's fill mode")]
		public FillModes FillMode;

		[Tooltip("defines whether the bar will work on scaled or unscaled time (whether or not it'll keep moving if time is slowed down for example)")]
		public TimeScales TimeScale;

		[Tooltip("the selected fill animation mode")]
		public BarFillModes BarFillMode;

		[MMInspectorGroup("Foreground Bar Settings", true, 12, false)]
		[Tooltip("whether or not the foreground bar should lerp")]
		public bool LerpForegroundBar;

		[Tooltip("the speed at which to lerp the foreground bar")]
		[MMCondition("LerpForegroundBar", true)]
		public float LerpForegroundBarSpeedDecreasing;

		[Tooltip("the speed at which to lerp the foreground bar if value is increasing")]
		[FormerlySerializedAs("LerpForegroundBarSpeed")]
		[MMCondition("LerpForegroundBar", true)]
		public float LerpForegroundBarSpeedIncreasing;

		[Tooltip("the speed at which to lerp the foreground bar if speed is decreasing")]
		[MMCondition("LerpForegroundBar", true)]
		public float LerpForegroundBarDurationDecreasing;

		[Tooltip("the duration each update of the foreground bar should take (only if in fixed duration bar fill mode)")]
		[MMCondition("LerpForegroundBar", true)]
		public float LerpForegroundBarDurationIncreasing;

		[Tooltip("the curve to use when animating the foreground bar fill decreasing")]
		[MMCondition("LerpForegroundBar", true)]
		public AnimationCurve LerpForegroundBarCurveDecreasing;

		[Tooltip("the curve to use when animating the foreground bar fill increasing")]
		[MMCondition("LerpForegroundBar", true)]
		public AnimationCurve LerpForegroundBarCurveIncreasing;

		[MMInspectorGroup("Delayed Bar Decreasing", true, 13, false)]
		[Tooltip("the delay before the delayed bar moves (in seconds)")]
		[FormerlySerializedAs("Delay")]
		public float DecreasingDelay;

		[Tooltip("whether or not the delayed bar's animation should lerp")]
		[FormerlySerializedAs("LerpDelayedBar")]
		public bool LerpDecreasingDelayedBar;

		[Tooltip("the speed at which to lerp the delayed bar")]
		[FormerlySerializedAs("LerpDelayedBarSpeed")]
		[MMCondition("LerpDecreasingDelayedBar", true)]
		public float LerpDecreasingDelayedBarSpeed;

		[Tooltip("the duration each update of the foreground bar should take (only if in fixed duration bar fill mode)")]
		[FormerlySerializedAs("LerpDelayedBarDuration")]
		[MMCondition("LerpDecreasingDelayedBar", true)]
		public float LerpDecreasingDelayedBarDuration;

		[Tooltip("the curve to use when animating the delayed bar fill")]
		[FormerlySerializedAs("LerpDelayedBarCurve")]
		[MMCondition("LerpDecreasingDelayedBar", true)]
		public AnimationCurve LerpDecreasingDelayedBarCurve;

		[MMInspectorGroup("Delayed Bar Increasing", true, 18, false)]
		[Tooltip("the delay before the delayed bar moves (in seconds)")]
		public float IncreasingDelay;

		[Tooltip("whether or not the delayed bar's animation should lerp")]
		public bool LerpIncreasingDelayedBar;

		[Tooltip("the speed at which to lerp the delayed bar")]
		[MMCondition("LerpIncreasingDelayedBar", true)]
		public float LerpIncreasingDelayedBarSpeed;

		[Tooltip("the duration each update of the foreground bar should take (only if in fixed duration bar fill mode)")]
		[MMCondition("LerpIncreasingDelayedBar", true)]
		public float LerpIncreasingDelayedBarDuration;

		[Tooltip("the curve to use when animating the delayed bar fill")]
		[MMCondition("LerpIncreasingDelayedBar", true)]
		public AnimationCurve LerpIncreasingDelayedBarCurve;

		[MMInspectorGroup("Bump", true, 14, false)]
		[Tooltip("whether or not the bar should 'bump' when changing value")]
		public bool BumpScaleOnChange;

		[Tooltip("whether or not the bar should bump when its value increases")]
		public bool BumpOnIncrease;

		[Tooltip("whether or not the bar should bump when its value decreases")]
		public bool BumpOnDecrease;

		[Tooltip("the duration of the bump animation")]
		public float BumpDuration;

		[Tooltip("whether or not the bar should flash when bumping")]
		public bool ChangeColorWhenBumping;

		[Tooltip("whether or not to store the initial bar color before a bump")]
		public bool StoreBarColorOnPlay;

		[Tooltip("the color to apply to the bar when bumping")]
		[MMCondition("ChangeColorWhenBumping", true)]
		public Color BumpColor;

		[Tooltip("the curve to map the bump animation on")]
		[FormerlySerializedAs("BumpAnimationCurve")]
		public AnimationCurve BumpScaleAnimationCurve;

		[Tooltip("the curve to map the bump animation color animation on")]
		public AnimationCurve BumpColorAnimationCurve;

		[Tooltip("if this is true, the BumpIntensityMultiplier curve will be evaluated to apply a multiplier to the bump intensity")]
		public bool ApplyBumpIntensityMultiplier;

		[Tooltip("the curve to map the bump's intensity on. x is the normalized delta of the bump (from -1:-100% to 1:100%), y is the associated multiplier")]
		[MMCondition("ApplyBumpIntensityMultiplier", true)]
		public AnimationCurve BumpIntensityMultiplier;

		[MMInspectorGroup("Events", true, 16, false)]
		[Tooltip("an event to trigger every time the bar bumps")]
		public UnityEvent OnBump;

		[Tooltip("an event to trigger every time the bar bumps, with its bump intensity (based on BumpDeltaMultiplier) in parameter")]
		public UnityEvent<float> OnBumpIntensity;

		[Tooltip("an event to trigger every time the bar starts decreasing")]
		public UnityEvent OnBarMovementDecreasingStart;

		[Tooltip("an event to trigger every time the bar stops decreasing")]
		public UnityEvent OnBarMovementDecreasingStop;

		[Tooltip("an event to trigger every time the bar starts increasing")]
		public UnityEvent OnBarMovementIncreasingStart;

		[Tooltip("an event to trigger every time the bar stops increasing")]
		public UnityEvent OnBarMovementIncreasingStop;

		[MMInspectorGroup("Text", true, 20, false)]
		[Tooltip("a Text object to update with the bar's value")]
		public Text PercentageText;

		[Tooltip("a TMPro text object to update with the bar's value")]
		public TMP_Text PercentageTextMeshPro;

		[Tooltip("a prefix to always add to the bar's value display")]
		public string TextPrefix;

		[Tooltip("a suffix to always add to the bar's value display")]
		public string TextSuffix;

		[Tooltip("a value multiplier to always apply to the bar's value when displaying it")]
		public float TextValueMultiplier;

		[Tooltip("the format in which the text should display")]
		public string TextFormat;

		[Tooltip("whether or not to display the total after the current value")]
		public bool DisplayTotal;

		[Tooltip("if DisplayTotal is true, the separator to put between the current value and the total")]
		[MMCondition("DisplayTotal", true)]
		public string TotalSeparator;

		[MMInspectorGroup("Debug", true, 15, false)]
		[Tooltip("the value the bar will move to if you press the DebugSet button")]
		[Range(0f, 1f)]
		public float DebugNewTargetValue;

		[MMInspectorButton("DebugUpdateBar")]
		public bool DebugUpdateBarButton;

		[MMInspectorButton("DebugSetBar")]
		public bool DebugSetBarButton;

		[MMInspectorButton("Bump")]
		public bool TestBumpButton;

		[MMInspectorButton("Plus10Percent")]
		public bool Plus10PercentButton;

		[MMInspectorButton("Minus10Percent")]
		public bool Minus10PercentButton;

		[MMInspectorGroup("Debug Read Only", true, 19, false)]
		[Tooltip("the current progress of the bar, ideally read only")]
		[Range(0f, 1f)]
		public float BarProgress;

		[Tooltip("the value towards which the bar is currently interpolating, ideally read only")]
		[Range(0f, 1f)]
		public float BarTarget;

		[Tooltip("the current progress of the delayed bar increasing")]
		[Range(0f, 1f)]
		public float DelayedBarIncreasingProgress;

		[Tooltip("the current progress of the delayed bar decreasing")]
		[Range(0f, 1f)]
		public float DelayedBarDecreasingProgress;

		protected bool _initialized;

		protected Vector2 _initialBarSize;

		protected Color _initialColor;

		protected Vector3 _initialScale;

		protected Image _foregroundImage;

		protected Image _delayedDecreasingImage;

		protected Image _delayedIncreasingImage;

		protected Vector3 _targetLocalScale;

		protected float _newPercent;

		protected float _percentLastTimeBarWasUpdated;

		protected float _lastUpdateTimestamp;

		protected float _time;

		protected float _deltaTime;

		protected int _direction;

		protected Coroutine _coroutine;

		protected bool _coroutineShouldRun;

		protected bool _isDelayedBarIncreasingNotNull;

		protected bool _isDelayedBarDecreasingNotNull;

		protected bool _actualUpdate;

		protected Vector2 _anchorVector;

		protected float _delayedBarDecreasingProgress;

		protected float _delayedBarIncreasingProgress;

		protected MMProgressBarStates CurrentState;

		protected string _updatedText;

		protected string _totalText;

		protected bool _isForegroundBarNotNull;

		protected bool _isForegroundImageNotNull;

		protected bool _isPercentageTextNotNull;

		protected bool _isPercentageTextMeshProNotNull;

		public virtual bool Bumping { get; protected set; }

		public virtual void UpdateBar01(float normalizedValue)
		{
		}

		public virtual void UpdateBar(float currentValue, float minValue, float maxValue)
		{
		}

		public virtual void SetBar(float currentValue, float minValue, float maxValue)
		{
		}

		public virtual void SetBar01(float newPercent)
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnEnable()
		{
		}

		public virtual void Initialization()
		{
		}

		protected virtual void StoreInitialColor()
		{
		}

		protected virtual void DebugUpdateBar()
		{
		}

		protected virtual void DebugSetBar()
		{
		}

		public virtual void Plus10Percent()
		{
		}

		public virtual void Minus10Percent()
		{
		}

		public virtual void Plus20Percent()
		{
		}

		public virtual void Minus20Percent()
		{
		}

		protected virtual void UpdateText()
		{
		}

		protected virtual void ComputeUpdatedText()
		{
		}

		[IteratorStateMachine(typeof(_003CUpdateBarsCo_003Ed__118))]
		protected virtual IEnumerator UpdateBarsCo()
		{
			return null;
		}

		protected virtual void DetermineDeltaTime()
		{
		}

		protected virtual void DetermineDirection()
		{
		}

		protected virtual void UpdateBars()
		{
		}

		protected virtual float ComputeNewFill(bool lerpBar, float barSpeed, float barDuration, AnimationCurve barCurve, float delay, float lastPercent, out float t)
		{
			t = default(float);
			return 0f;
		}

		protected virtual void SetBarInternal(float newAmount, Transform bar, Image image, Vector2 initialSize)
		{
		}

		public virtual void Bump()
		{
		}

		[IteratorStateMachine(typeof(_003CBumpCoroutine_003Ed__125))]
		protected virtual IEnumerator BumpCoroutine(float intensityMultiplier)
		{
			return null;
		}

		public virtual void ShowBar()
		{
		}

		public virtual void HideBar(float delay)
		{
		}

		[IteratorStateMachine(typeof(_003CHideBarCo_003Ed__128))]
		protected virtual IEnumerator HideBarCo(float delay)
		{
			return null;
		}
	}
}
