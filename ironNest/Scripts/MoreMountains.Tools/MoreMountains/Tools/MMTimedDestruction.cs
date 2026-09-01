using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Activation/MMTimedDestruction")]
	public class MMTimedDestruction : MonoBehaviour
	{
		public enum TimedDestructionModes
		{
			Destroy = 0,
			Disable = 1
		}

		[CompilerGenerated]
		private sealed class _003CDestruction_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMTimedDestruction _003C_003E4__this;

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
			public _003CDestruction_003Ed__4(int _003C_003E1__state)
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

		public TimedDestructionModes TimeDestructionMode;

		public float TimeBeforeDestruction;

		protected virtual void Start()
		{
		}

		[IteratorStateMachine(typeof(_003CDestruction_003Ed__4))]
		protected virtual IEnumerator Destruction()
		{
			return null;
		}
	}
}
