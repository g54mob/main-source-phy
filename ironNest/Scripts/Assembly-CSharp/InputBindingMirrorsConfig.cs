using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "InputBindingMirrorsConfig", menuName = "Input/InputBindingMirrorsConfig")]
public class InputBindingMirrorsConfig : ScriptableObject
{
	[CompilerGenerated]
	private sealed class _003Cget_BindingMirrors_003Ed__2 : IEnumerable<InputBindingMirror>, IEnumerable, IEnumerator<InputBindingMirror>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private InputBindingMirror _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public InputBindingMirrorsConfig _003C_003E4__this;

		private InputBindingMirror[] _003C_003E7__wrap1;

		private int _003C_003E7__wrap2;

		InputBindingMirror IEnumerator<InputBindingMirror>.Current
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
		public _003Cget_BindingMirrors_003Ed__2(int _003C_003E1__state)
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
		IEnumerator<InputBindingMirror> IEnumerable<InputBindingMirror>.GetEnumerator()
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
	private InputBindingMirror[] _bindingMirrors;

	public IEnumerable<InputBindingMirror> BindingMirrors
	{
		[IteratorStateMachine(typeof(_003Cget_BindingMirrors_003Ed__2))]
		get
		{
			return null;
		}
	}
}
