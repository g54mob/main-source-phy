using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMDebugMenuDebugTab : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CScrollToLogBottomCo_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMDebugMenuDebugTab _003C_003E4__this;

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
			public _003CScrollToLogBottomCo_003Ed__14(int _003C_003E1__state)
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

		public ScrollRect DebugScrollRect;

		public Text DebugText;

		public InputField CommandPrompt;

		public Text CommandPromptCharacter;

		public bool TouchScreenVisible;

		protected TouchScreenKeyboard _touchScreenKeyboard;

		protected RectTransform _rectTransform;

		protected float _mobileMenuOffset;

		protected bool _touchScreenVisibleLastFrame;

		protected virtual void Awake()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnMMDebugLogEvent(MMDebug.DebugLogItem item)
		{
		}

		[IteratorStateMachine(typeof(_003CScrollToLogBottomCo_003Ed__14))]
		protected virtual IEnumerator ScrollToLogBottomCo()
		{
			return null;
		}

		public virtual void OnDestroy()
		{
		}
	}
}
