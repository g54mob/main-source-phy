using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Lofelt.NiceVibrations
{
	public class MMUIShaker : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CShake_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMUIShaker _003C_003E4__this;

			public float duration;

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
			public _003CShake_003Ed__7(int _003C_003E1__state)
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

		public float Amplitude;

		public float Frequency;

		public bool Shaking;

		protected Vector3 _initialPosition;

		protected Vector3 _shakePosition;

		protected RectTransform _rectTransform;

		protected virtual void Start()
		{
		}

		[IteratorStateMachine(typeof(_003CShake_003Ed__7))]
		public virtual IEnumerator Shake(float duration)
		{
			return null;
		}

		protected virtual void Update()
		{
		}
	}
}
