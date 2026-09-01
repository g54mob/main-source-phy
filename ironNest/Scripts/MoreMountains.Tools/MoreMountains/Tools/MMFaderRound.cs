using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(CanvasGroup))]
	[AddComponentMenu("More Mountains/Tools/GUI/MMFaderRound")]
	public class MMFaderRound : MMMonoBehaviour, MMEventListener<MMFadeEvent>, MMEventListenerBase, MMEventListener<MMFadeInEvent>, MMEventListener<MMFadeOutEvent>, MMEventListener<MMFadeStopEvent>
	{
		public enum CameraModes
		{
			Main = 0,
			Override = 1
		}

		[MMInspectorGroup("Bindings", true, 121, false)]
		public CameraModes CameraMode;

		[MMEnumCondition("CameraMode", new int[] { 1 })]
		public Camera TargetCamera;

		public RectTransform FaderBackground;

		public RectTransform FaderMask;

		[MMInspectorGroup("Identification", true, 122, false)]
		public int ID;

		[MMInspectorGroup("Mask", true, 127, false)]
		[MMVector(new string[] { "min", "max" })]
		public Vector2 MaskScale;

		[MMInspectorGroup("Timing", true, 124, false)]
		public float DefaultDuration;

		public MMTweenType DefaultTween;

		public bool IgnoreTimescale;

		[MMInspectorGroup("Interaction", true, 125, false)]
		public bool ShouldBlockRaycasts;

		[MMInspectorGroup("Debug", true, 126, false)]
		public Transform DebugWorldPositionTarget;

		[MMInspectorButtonBar(new string[] { "FadeIn1Second", "FadeOut1Second", "DefaultFade", "ResetFader" }, new string[] { "FadeIn1Second", "FadeOut1Second", "DefaultFade", "ResetFader" }, new bool[] { true, true, true, true }, new string[] { "main-call-to-action", null, null, null })]
		public bool DebugToolbar;

		protected CanvasGroup _canvasGroup;

		protected float _initialScale;

		protected float _currentTargetScale;

		protected float _currentDuration;

		protected MMTweenType _currentCurve;

		protected bool _fading;

		protected float _fadeStartedAt;

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

		protected virtual void StartFading(float initialAlpha, float endAlpha, float duration, MMTweenType curve, int id, bool ignoreTimeScale, Vector3 worldPosition)
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
