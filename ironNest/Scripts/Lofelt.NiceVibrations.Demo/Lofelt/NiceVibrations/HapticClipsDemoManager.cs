using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations
{
	public class HapticClipsDemoManager : DemoManager
	{
		[CompilerGenerated]
		private sealed class _003CBackToIdle_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public HapticClipsDemoManager _003C_003E4__this;

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
			public _003CBackToIdle_003Ed__8(int _003C_003E1__state)
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
		private sealed class _003CChangeIcon_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public HapticClipsDemoManager _003C_003E4__this;

			public Sprite newSprite;

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
			public _003CChangeIcon_003Ed__7(int _003C_003E1__state)
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

		[Header("Image")]
		public Image IconImage;

		public Animator IconImageAnimator;

		public List<HapticClipsDemoItem> DemoItems;

		protected WaitForSeconds _iconChangeDelay;

		protected int _idleAnimationParameter;

		protected virtual void Awake()
		{
		}

		public virtual void PlayHapticClip(int index)
		{
		}

		[IteratorStateMachine(typeof(_003CChangeIcon_003Ed__7))]
		protected virtual IEnumerator ChangeIcon(Sprite newSprite)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CBackToIdle_003Ed__8))]
		protected virtual IEnumerator BackToIdle()
		{
			return null;
		}

		private void OnHapticsStopped()
		{
		}

		private void OnDisable()
		{
		}

		private void OnEnable()
		{
		}

		private void OnApplicationFocus(bool hasFocus)
		{
		}
	}
}
