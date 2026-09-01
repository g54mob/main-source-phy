using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator.Examples
{
	public class SettingsUIToolkitDemo : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CwaitForUIDocumentToLoad_003Ed__3 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			private UIDocument _003Cdocument_003E5__2;

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
			public _003CwaitForUIDocumentToLoad_003Ed__3(int _003C_003E1__state)
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

		public SettingsProvider SettingsProvider;

		public void Awake()
		{
		}

		public void Start()
		{
		}

		[IteratorStateMachine(typeof(_003CwaitForUIDocumentToLoad_003Ed__3))]
		public IEnumerator waitForUIDocumentToLoad()
		{
			return null;
		}

		private void onPlayerNameChanged(string playerName)
		{
		}

		public void Apply()
		{
		}

		public void Save()
		{
		}

		public void Reset()
		{
		}
	}
}
