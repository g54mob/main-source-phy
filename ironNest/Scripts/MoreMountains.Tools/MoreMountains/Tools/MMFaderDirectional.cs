using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(CanvasGroup))]
	[AddComponentMenu("More Mountains/Tools/GUI/MMFaderDirectional")]
	public class MMFaderDirectional : MMMonoBehaviour, MMEventListener<MMFadeEvent>, MMEventListenerBase, MMEventListener<MMFadeInEvent>, MMEventListener<MMFadeOutEvent>, MMEventListener<MMFadeStopEvent>
	{
		public enum Directions
		{
			TopToBottom = 0,
			LeftToRight = 1,
			RightToLeft = 2,
			BottomToTop = 3
		}

		[CompilerGenerated]
		private sealed class _003CStartFading_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public int id;

			public MMFaderDirectional _003C_003E4__this;

			public MMTweenType curve;

			public bool ignoreTimeScale;

			public float duration;

			public bool fadingIn;

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
			public _003CStartFading_003Ed__36(int _003C_003E1__state)
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

		[MMInspectorGroup("Identification", true, 122, false)]
		[Tooltip("the ID for this fader (0 is default), set more IDs if you need more than one fader")]
		public int ID;

		[MMInspectorGroup("Directional Fader", true, 123, false)]
		[Tooltip("the direction this fader should move in when fading in")]
		public Directions FadeInDirection;

		[Tooltip("the direction this fader should move in when fading out")]
		public Directions FadeOutDirection;

		[MMInspectorGroup("Timing", true, 124, false)]
		[Tooltip("the default duration of the fade in/out")]
		public float DefaultDuration;

		[Tooltip("the default curve to use for this fader")]
		public MMTweenType DefaultTween;

		[Tooltip("whether or not the fade should happen in unscaled time")]
		public bool IgnoreTimescale;

		[Tooltip("whether or not to automatically disable this fader on init")]
		public bool DisableOnInit;

		[MMInspectorGroup("Delay", true, 127, false)]
		[Tooltip("a delay (in seconds) to apply before playing this fade")]
		public float InitialDelay;

		[MMInspectorGroup("Interaction", true, 125, false)]
		[Tooltip("whether or not the fader should block raycasts when visible")]
		public bool ShouldBlockRaycasts;

		[MMInspectorGroup("Debug", true, 126, false)]
		[MMInspectorButtonBar(new string[] { "FadeIn1Second", "FadeOut1Second", "DefaultFade", "ResetFader" }, new string[] { "FadeIn1Second", "FadeOut1Second", "DefaultFade", "ResetFader" }, new bool[] { true, true, true, true }, new string[] { "main-call-to-action", null, null, null })]
		public bool DebugToolbar;

		protected RectTransform _rectTransform;

		protected CanvasGroup _canvasGroup;

		protected float _currentDuration;

		protected MMTweenType _currentCurve;

		protected bool _fading;

		protected float _fadeStartedAt;

		protected Vector2 _initialPosition;

		protected Vector2 _fromPosition;

		protected Vector2 _toPosition;

		protected Vector2 _newPosition;

		protected bool _active;

		protected bool _initialized;

		public virtual float Width => 0f;

		public virtual float Height => 0f;

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

		protected virtual void Start()
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

		[IteratorStateMachine(typeof(_003CStartFading_003Ed__36))]
		protected virtual IEnumerator StartFading(bool fadingIn, float duration, MMTweenType curve, int id, bool ignoreTimeScale, Vector3 worldPosition)
		{
			return null;
		}

		protected virtual Vector2 BeforeEntryPosition()
		{
			return default(Vector2);
		}

		protected virtual Vector2 ExitPosition()
		{
			return default(Vector2);
		}

		protected virtual void DisableFader()
		{
		}

		protected virtual void EnableFader()
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

		protected virtual void OnDestroy()
		{
		}
	}
}
