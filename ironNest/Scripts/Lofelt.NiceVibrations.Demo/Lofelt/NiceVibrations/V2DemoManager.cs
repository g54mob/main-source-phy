using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Lofelt.NiceVibrations
{
	public class V2DemoManager : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CTransitionCoroutine_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public V2DemoManager _003C_003E4__this;

			public int previous;

			public int next;

			public bool goingRight;

			private float _003CtimeSpent_003E5__2;

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
			public _003CTransitionCoroutine_003Ed__16(int _003C_003E1__state)
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

		public List<RectTransform> Pages;

		public int CurrentPage;

		public float PageTransitionDuration;

		public AnimationCurve TransitionCurve;

		public Color ActiveColor;

		public Color InactiveColor;

		public bool SoundActive;

		protected Vector3 _position;

		protected List<Pagination> _paginations;

		protected Coroutine _transitionCoroutine;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void PreviousPage()
		{
		}

		public virtual void NextPage()
		{
		}

		protected virtual void SetCurrentPage()
		{
		}

		protected virtual void Transition(int previous, int next, bool goingRight)
		{
		}

		[IteratorStateMachine(typeof(_003CTransitionCoroutine_003Ed__16))]
		protected virtual IEnumerator TransitionCoroutine(int previous, int next, bool goingRight)
		{
			return null;
		}

		public virtual void TurnHapticsOn()
		{
		}

		public virtual void TurnHapticsOff()
		{
		}

		public virtual void TurnSoundsOn()
		{
		}

		public virtual void TurnSoundsOff()
		{
		}
	}
}
