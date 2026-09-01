using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InitializationHandler : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CFadeOutCanvasGroup_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CanvasGroup canvasGroup;

		public float duration;

		private float _003CstartAlpha_003E5__2;

		private float _003Ctimer_003E5__3;

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
		public _003CFadeOutCanvasGroup_003Ed__16(int _003C_003E1__state)
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
	private sealed class _003CHandleSplashDuration_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InitializationHandler _003C_003E4__this;

		private float _003Ctimer_003E5__2;

		private float _003CcurrentScale_003E5__3;

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
		public _003CHandleSplashDuration_003Ed__17(int _003C_003E1__state)
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
	private sealed class _003CStart_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InitializationHandler _003C_003E4__this;

		private List<GameObject>.Enumerator _003C_003E7__wrap1;

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
		public _003CStart_003Ed__15(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[Tooltip("Objects to enable frame by frame.")]
	[SerializeField]
	private List<GameObject> _handledObjects;

	[Tooltip("Object containing self-playing intro animation.")]
	[SerializeField]
	private GameObject _introObject;

	[Tooltip("Should wait for Steam initialization (success or failure)?")]
	[SerializeField]
	private bool _shouldWaitForSteam;

	[Tooltip("Should wait until no FMOD audio banks are loading?")]
	[SerializeField]
	private bool _shouldWaitForFmodBanks;

	[Header("Splash config")]
	[Tooltip("Min duration the splash will be displayed so fiction disclosure can be read when loading is fast.")]
	[SerializeField]
	private float _minSplashDisplayDuration;

	[SerializeField]
	private CanvasGroup _splashCanvasGroup;

	[Tooltip("Time for splash to disappear after enabling intro animation underneath.")]
	[SerializeField]
	private float _splashFadeDuration;

	[SerializeField]
	private CanvasGroup _textCanvasGroup;

	[Tooltip("Time for text to disappear after everything is loaded, before starting intro animation.")]
	[SerializeField]
	private float _textFadeDuration;

	[Header("Loading Bar")]
	[SerializeField]
	private RectTransform _loadingBarRect;

	[Tooltip("Target (0-1) the loading bar will reach in _minSplashDisplayDuration.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float _loadingBarMinDurationTarget;

	[Tooltip("Target (0-1) the loading bar will reach after _minSplashDisplayDuration and before all waiting.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float _loadingBarWaitTarget;

	[Tooltip("Speed multiplier for loading bar to reach _loadingBarWaitTarget, keep it small to see something moving.")]
	[SerializeField]
	private float _loadingBarWaitMoveSpeed;

	private bool _hasSplashMinDurationPassed;

	private bool _hasEverythingLoaded;

	[IteratorStateMachine(typeof(_003CStart_003Ed__15))]
	private IEnumerator Start()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFadeOutCanvasGroup_003Ed__16))]
	private IEnumerator FadeOutCanvasGroup(CanvasGroup canvasGroup, float duration)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CHandleSplashDuration_003Ed__17))]
	private IEnumerator HandleSplashDuration()
	{
		return null;
	}

	private void SetLoadingBarXScale(float xScale)
	{
	}
}
