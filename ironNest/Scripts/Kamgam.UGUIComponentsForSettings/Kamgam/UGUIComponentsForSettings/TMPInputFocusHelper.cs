using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kamgam.UGUIComponentsForSettings
{
	[RequireComponent(typeof(TMP_InputField))]
	public class TMPInputFocusHelper : MonoBehaviour, ISelectHandler, IEventSystemHandler, ISubmitHandler
	{
		[CompilerGenerated]
		private sealed class _003CUnFocusByDefault_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public TMPInputFocusHelper _003C_003E4__this;

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
			public _003CUnFocusByDefault_003Ed__5(int _003C_003E1__state)
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

		[SerializeField]
		[Header("Touch Settings")]
		[Tooltip("Which keyboard type should be opened on touch devices?")]
		private TouchScreenKeyboardType keyboardType;

		protected TMP_InputField inputTf;

		public TMP_InputField InputTf => null;

		public void OnSelect(BaseEventData eventData)
		{
		}

		[IteratorStateMachine(typeof(_003CUnFocusByDefault_003Ed__5))]
		private IEnumerator UnFocusByDefault()
		{
			return null;
		}

		public void OnSubmit(BaseEventData eventData)
		{
		}

		public void Update()
		{
		}
	}
}
