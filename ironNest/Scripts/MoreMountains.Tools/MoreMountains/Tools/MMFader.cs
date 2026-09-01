using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(Image))]
	[AddComponentMenu("More Mountains/Tools/GUI/MMFader")]
	public class MMFader : MMMonoBehaviour, MMEventListener<MMFadeEvent>, MMEventListenerBase, MMEventListener<MMFadeInEvent>, MMEventListener<MMFadeOutEvent>, MMEventListener<MMFadeStopEvent>
	{
		public enum ForcedInitStates
		{
			None = 0,
			Active = 1,
			Inactive = 2
		}

		[MMInspectorGroup("Identification", true, 122, false)]
		[Tooltip("the ID for this fader (0 is default), set more IDs if you need more than one fader")]
		public int ID;

		[MMInspectorGroup("Opacity", true, 123, false)]
		[Tooltip("the opacity the fader should be at when inactive")]
		public float InactiveAlpha;

		[Tooltip("the opacity the fader should be at when active")]
		public float ActiveAlpha;

		[Tooltip("determines whether a state should be forced on init")]
		public ForcedInitStates ForcedInitState;

		[MMInspectorGroup("Timing", true, 124, false)]
		[Tooltip("the default duration of the fade in/out")]
		public float DefaultDuration;

		[Tooltip("the default curve to use for this fader")]
		public MMTweenType DefaultTween;

		[Tooltip("whether or not the fade should happen in unscaled time")]
		public bool IgnoreTimescale;

		[Tooltip("whether or not this fader can cause a fade if the requested final alpha is the same as the current one")]
		public bool CanFadeToCurrentAlpha;

		[MMInspectorGroup("Interaction", true, 125, false)]
		[Tooltip("whether or not the fader should block raycasts when visible")]
		public bool ShouldBlockRaycasts;

		[MMInspectorGroup("Debug", true, 126, false)]
		[MMInspectorButtonBar(new string[] { "FadeIn1Second", "FadeOut1Second", "DefaultFade", "ResetFader" }, new string[] { "FadeIn1Second", "FadeOut1Second", "DefaultFade", "ResetFader" }, new bool[] { true, true, true, true }, new string[] { "main-call-to-action", null, null, null })]
		public bool DebugToolbar;

		protected CanvasGroup _canvasGroup;

		protected Image _image;

		protected float _initialAlpha;

		protected float _currentTargetAlpha;

		protected float _currentDuration;

		protected MMTweenType _currentCurve;

		protected bool _fading;

		protected float _fadeStartedAt;

		protected bool _frameCountOne;

		protected virtual void ResetFader()
		{
		}

		protected virtual void DefaultFade()
		{
		}

		protected virtual void FadeIn1Second()
		{
		}

		protected virtual void FadeOut1Second()
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void Fade()
		{
		}

		protected virtual void StopFading()
		{
		}

		protected virtual void DisableFader()
		{
		}

		protected virtual void EnableFader()
		{
		}

		protected virtual void StartFading(float initialAlpha, float endAlpha, float duration, MMTweenType curve, bool ignoreTimeScale)
		{
		}

		public virtual void OnMMEvent(MMFadeEvent fadeEvent)
		{
		}

		public virtual void OnMMEvent(MMFadeInEvent fadeEvent)
		{
		}

		public virtual void OnMMEvent(MMFadeOutEvent fadeEvent)
		{
		}

		public virtual void Fade(float targetAlpha, float duration, MMTweenType curve, bool ignoreTimeScale)
		{
		}

		public virtual void FadeIn(float duration, MMTweenType curve, bool ignoreTimeScale = true)
		{
		}

		public virtual void FadeOut(float duration, MMTweenType curve, bool ignoreTimeScale = true)
		{
		}

		public virtual void OnMMEvent(MMFadeStopEvent fadeStopEvent)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
