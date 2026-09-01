using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DistanceWorldTextFader : MonoBehaviour
{
	[Serializable]
	public class TextToRendererPair
	{
		[SerializeField]
		private TMP_Text _text;

		[SerializeField]
		private Renderer _renderer;

		public TMP_Text Text => null;

		public Renderer Renderer => null;
	}

	[CompilerGenerated]
	private sealed class _003CFadeInCoroutine_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DistanceWorldTextFader _003C_003E4__this;

		private float _003Cspeed_003E5__2;

		private float _003Calpha_003E5__3;

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
		public _003CFadeInCoroutine_003Ed__13(int _003C_003E1__state)
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
	private sealed class _003CFadeOutCoroutine_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DistanceWorldTextFader _003C_003E4__this;

		private float _003Cspeed_003E5__2;

		private float _003Calpha_003E5__3;

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
		public _003CFadeOutCoroutine_003Ed__12(int _003C_003E1__state)
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

	[SerializeField]
	private Transform _measuredTransform;

	[SerializeField]
	[Min(0f)]
	private float _distanceToShow;

	[SerializeField]
	[Min(0f)]
	private float _fadeDuration;

	[SerializeField]
	private TextToRendererPair[] _texts;

	private Transform _cameraTransform;

	private Coroutine _fadeCoroutine;

	private float _visibleAlpha;

	private bool _isVisible;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private bool ShouldBeVisible()
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CFadeOutCoroutine_003Ed__12))]
	private IEnumerator FadeOutCoroutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFadeInCoroutine_003Ed__13))]
	private IEnumerator FadeInCoroutine()
	{
		return null;
	}

	private float GetCurrentAlpha(TMP_Text text)
	{
		return 0f;
	}

	private void SetTextAlpha(TMP_Text text, float alpha)
	{
	}

	private void Reset()
	{
	}
}
