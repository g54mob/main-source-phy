using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimatorBoolToggler : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CSetBoolCoroutine_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AnimatorBoolToggler _003C_003E4__this;

		public bool value;

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
		public _003CSetBoolCoroutine_003Ed__9(int _003C_003E1__state)
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
	private sealed class _003CToggleBoolCoroutine_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AnimatorBoolToggler _003C_003E4__this;

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
		public _003CToggleBoolCoroutine_003Ed__6(int _003C_003E1__state)
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

	public Animator animator;

	public string parameterName;

	public float delay;

	public void ToggleBool()
	{
	}

	public void SetBool(bool value)
	{
	}

	public void ToggleBoolDelayed()
	{
	}

	[IteratorStateMachine(typeof(_003CToggleBoolCoroutine_003Ed__6))]
	private IEnumerator ToggleBoolCoroutine()
	{
		return null;
	}

	public void SetBoolDelayed(bool value)
	{
	}

	public bool GetBool()
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CSetBoolCoroutine_003Ed__9))]
	private IEnumerator SetBoolCoroutine(bool value)
	{
		return null;
	}
}
