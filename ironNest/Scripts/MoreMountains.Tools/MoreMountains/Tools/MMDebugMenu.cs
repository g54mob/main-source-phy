using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMDebugMenu : MonoBehaviour
	{
		public enum ToggleDirections
		{
			TopToBottom = 0,
			LeftToRight = 1,
			RightToLeft = 2,
			BottomToTop = 3
		}

		[CompilerGenerated]
		private sealed class _003CToggleCo_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMDebugMenu _003C_003E4__this;

			public bool active;

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
			public _003CToggleCo_003Ed__26(int _003C_003E1__state)
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

		[Header("Data")]
		public MMDebugMenuData Data;

		[Header("Bindings")]
		public CanvasGroup MenuContainer;

		public RectTransform Contents;

		public Image MenuBackground;

		public Image CloseIcon;

		public RectTransform TabBar;

		public RectTransform TabContainer;

		public MMDebugMenuTabManager TabManager;

		public Image MMLogo;

		[Header("Events")]
		public UnityEvent OnOpenEvent;

		public UnityEvent OnCloseEvent;

		[Header("Test")]
		[MMReadOnly]
		public bool Active;

		[MMInspectorButton("ToggleMenu")]
		public bool ToggleButton;

		protected RectTransform _containerRect;

		protected Vector3 _initialContainerPosition;

		protected Vector3 _offPosition;

		protected Vector3 _newPosition;

		protected bool _toggling;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void FillMenu(bool triggerEvents = false)
		{
		}

		protected virtual void FillTab(MMDebugMenuTabContents tab, int index, bool triggerEvents = false)
		{
		}

		public virtual void OpenMenu()
		{
		}

		public virtual void CloseMenu()
		{
		}

		public virtual void ToggleMenu()
		{
		}

		[IteratorStateMachine(typeof(_003CToggleCo_003Ed__26))]
		protected virtual IEnumerator ToggleCo(bool active)
		{
			return null;
		}

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void CaptureConsoleLog(string logString, string stackTrace, LogType type)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
