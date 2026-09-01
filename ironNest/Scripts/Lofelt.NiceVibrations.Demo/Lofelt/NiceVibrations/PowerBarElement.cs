using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations
{
	public class PowerBarElement : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CColorBump_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public PowerBarElement _003C_003E4__this;

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
			public _003CColorBump_003Ed__11(int _003C_003E1__state)
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

		public float BumpDuration;

		public Color NormalColor;

		public Color InactiveColor;

		public AnimationCurve Curve;

		protected Image _image;

		protected float _bumpDuration;

		protected bool _active;

		protected bool _activeLastFrame;

		protected virtual void Awake()
		{
		}

		public virtual void SetActive(bool status)
		{
		}

		protected virtual void Update()
		{
		}

		[IteratorStateMachine(typeof(_003CColorBump_003Ed__11))]
		protected virtual IEnumerator ColorBump()
		{
			return null;
		}
	}
}
