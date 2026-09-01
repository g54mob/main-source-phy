using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations
{
	public class RegularPresetsDemoManager : DemoManager
	{
		[CompilerGenerated]
		private sealed class _003CChangeImageCoroutine_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RegularPresetsDemoManager _003C_003E4__this;

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
			public _003CChangeImageCoroutine_003Ed__17(int _003C_003E1__state)
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

		[Header("Sprites")]
		public Sprite IdleSprite;

		public Sprite SelectionSprite;

		public Sprite SuccessSprite;

		public Sprite WarningSprite;

		public Sprite FailureSprite;

		public Sprite RigidSprite;

		public Sprite SoftSprite;

		public Sprite LightSprite;

		public Sprite MediumSprite;

		public Sprite HeavySprite;

		protected WaitForSeconds _turnDelay;

		protected WaitForSeconds _shakeDelay;

		protected int _idleAnimationParameter;

		protected virtual void Awake()
		{
		}

		protected virtual void ChangeImage(Sprite newSprite)
		{
		}

		[IteratorStateMachine(typeof(_003CChangeImageCoroutine_003Ed__17))]
		protected virtual IEnumerator ChangeImageCoroutine(Sprite newSprite)
		{
			return null;
		}

		public virtual void SelectionButton()
		{
		}

		public virtual void SuccessButton()
		{
		}

		public virtual void WarningButton()
		{
		}

		public virtual void FailureButton()
		{
		}

		public virtual void RigidButton()
		{
		}

		public virtual void SoftButton()
		{
		}

		public virtual void LightButton()
		{
		}

		public virtual void MediumButton()
		{
		}

		public virtual void HeavyButton()
		{
		}
	}
}
