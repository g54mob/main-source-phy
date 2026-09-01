using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations
{
	public class MMProgressBar : MonoBehaviour
	{
		public enum FillModes
		{
			LocalScale = 0,
			FillAmount = 1,
			Width = 2,
			Height = 3
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

		[CompilerGenerated]
		private sealed class _003CBumpCoroutine_003Ed__49 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMProgressBar _003C_003E4__this;

			private float _003Cjourney_003E5__2;

			private float _003CcurrentDeltaTime_003E5__3;

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
			public _003CBumpCoroutine_003Ed__49(int _003C_003E1__state)
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

		[Header("General Settings")]
		public float StartValue;

		public float EndValue;

		public BarDirections BarDirection;

		public FillModes FillMode;

		public TimeScales TimeScale;

		[Header("Foreground Bar Settings")]
		public bool LerpForegroundBar;

		public float LerpForegroundBarSpeed;

		[Header("Delayed Bar Settings")]
		public float Delay;

		public bool LerpDelayedBar;

		public float LerpDelayedBarSpeed;

		[Header("Bindings")]
		public string PlayerID;

		public Transform DelayedBar;

		public Transform ForegroundBar;

		[Header("Bump")]
		public bool BumpScaleOnChange;

		public bool BumpOnIncrease;

		public float BumpDuration;

		public bool ChangeColorWhenBumping;

		public Color BumpColor;

		public AnimationCurve BumpAnimationCurve;

		public AnimationCurve BumpColorAnimationCurve;

		[Header("Realtime")]
		public bool AutoUpdating;

		[Range(0f, 1f)]
		public float BarProgress;

		protected float _targetFill;

		protected Vector3 _targetLocalScale;

		protected float _newPercent;

		protected float _lastPercent;

		protected float _lastUpdateTimestamp;

		protected bool _bump;

		protected Color _initialColor;

		protected Vector3 _initialScale;

		protected Vector3 _newScale;

		protected Image _foregroundImage;

		protected Image _delayedImage;

		protected bool _initialized;

		protected Vector2 _initialFrontBarSize;

		public bool Bumping { get; protected set; }

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void AutoUpdate()
		{
		}

		protected virtual void UpdateFrontBar()
		{
		}

		protected virtual void UpdateDelayedBar()
		{
		}

		public virtual void UpdateBar(float currentValue, float minValue, float maxValue)
		{
		}

		public virtual void Bump()
		{
		}

		[IteratorStateMachine(typeof(_003CBumpCoroutine_003Ed__49))]
		protected virtual IEnumerator BumpCoroutine()
		{
			return null;
		}

		protected virtual float Remap(float x, float A, float B, float C, float D)
		{
			return 0f;
		}
	}
}
