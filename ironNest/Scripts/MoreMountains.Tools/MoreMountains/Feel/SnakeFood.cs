using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class SnakeFood : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CMoveFood_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public SnakeFood _003C_003E4__this;

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
			public _003CMoveFood_003Ed__10(int _003C_003E1__state)
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

		public float OffDelay;

		public GameObject Model;

		public MMFeedbacks EatFeedback;

		public MMFeedbacks AppearFeedback;

		protected Snake _snake;

		public SnakeFoodSpawner Spawner { get; set; }

		protected void OnTriggerEnter2D(Collider2D other)
		{
		}

		[IteratorStateMachine(typeof(_003CMoveFood_003Ed__10))]
		protected virtual IEnumerator MoveFood()
		{
			return null;
		}
	}
}
