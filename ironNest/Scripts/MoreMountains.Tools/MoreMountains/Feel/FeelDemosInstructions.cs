using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Feel
{
	public class FeelDemosInstructions : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CDisappearCo_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public FeelDemosInstructions _003C_003E4__this;

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
			public _003CDisappearCo_003Ed__7(int _003C_003E1__state)
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

		[Header("Bindings")]
		public Text TargetText;

		public float DisappearDelay;

		public float DisappearDuration;

		[Header("Texts")]
		public string DesktopText;

		public string MobileText;

		protected CanvasGroup _canvasGroup;

		protected virtual void Awake()
		{
		}

		[IteratorStateMachine(typeof(_003CDisappearCo_003Ed__7))]
		protected virtual IEnumerator DisappearCo()
		{
			return null;
		}
	}
}
