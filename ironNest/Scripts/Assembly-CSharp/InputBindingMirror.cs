using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class InputBindingMirror
{
	[CompilerGenerated]
	private sealed class _003Cget_BindingsToMirror_003Ed__5 : IEnumerable<InputBindingReference>, IEnumerable, IEnumerator<InputBindingReference>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private InputBindingReference _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public InputBindingMirror _003C_003E4__this;

		private InputBindingReference[] _003C_003E7__wrap1;

		private int _003C_003E7__wrap2;

		InputBindingReference IEnumerator<InputBindingReference>.Current
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
		public _003Cget_BindingsToMirror_003Ed__5(int _003C_003E1__state)
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

		[DebuggerHidden]
		IEnumerator<InputBindingReference> IEnumerable<InputBindingReference>.GetEnumerator()
		{
			return null;
		}

		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}

	[SerializeField]
	[Tooltip("ID for the setting from Kamgam.SettingsGenerator containing the configured input path")]
	private string _settingsId;

	[SerializeField]
	private InputBindingReference[] _bindingsToMirror;

	public string SettingsId => null;

	public IEnumerable<InputBindingReference> BindingsToMirror
	{
		[IteratorStateMachine(typeof(_003Cget_BindingsToMirror_003Ed__5))]
		get
		{
			return null;
		}
	}
}
