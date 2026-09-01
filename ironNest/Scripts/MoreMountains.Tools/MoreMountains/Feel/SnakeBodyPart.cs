using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class SnakeBodyPart : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CActivateCollider_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public SnakeBodyPart _003C_003E4__this;

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
			public _003CActivateCollider_003Ed__8(int _003C_003E1__state)
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

		public MMPositionRecorder TargetRecorder;

		public MMFeedbacks EatFeedback;

		public MMFeedbacks NewFeedback;

		public int Offset;

		public int Index;

		protected Snake _snake;

		protected BoxCollider2D _collider2D;

		protected virtual void Awake()
		{
		}

		[IteratorStateMachine(typeof(_003CActivateCollider_003Ed__8))]
		protected virtual IEnumerator ActivateCollider()
		{
			return null;
		}

		protected void Update()
		{
		}

		public virtual void Eat(float intensity)
		{
		}

		public virtual void New()
		{
		}

		protected void OnTriggerEnter2D(Collider2D other)
		{
		}
	}
}
