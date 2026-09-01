using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SplitFlipDisplaySimple : MonoBehaviour, ISplitFlipDisplay, IFloatValueProvider
{
	[CompilerGenerated]
	private sealed class _003CFlipCoroutine_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SplitFlipDisplaySimple _003C_003E4__this;

		private int _003CcurrentIndex_003E5__2;

		private int _003CsymbolCount_003E5__3;

		private int _003Csteps_003E5__4;

		private int _003CactualSteps_003E5__5;

		private string _003Ctrigger_003E5__6;

		private int _003CdirectionMultiplier_003E5__7;

		private int _003Ci_003E5__8;

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
		public _003CFlipCoroutine_003Ed__29(int _003C_003E1__state)
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
	private List<TMP_Text> _oldTexts;

	[SerializeField]
	private List<TMP_Text> _newTexts;

	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private string _flipUpTrigger;

	[SerializeField]
	private string _flipDownTrigger;

	[SerializeField]
	private float _baselineAnimatorSpeedOverride;

	[SerializeField]
	private string _orderedSymbols;

	[SerializeField]
	private string _initialValue;

	[SerializeField]
	[Min(1f)]
	private int _maxFlipsUntilDesired;

	[SerializeField]
	private SplitFlipDisplay.DirectionMode _directionMode;

	[SerializeField]
	private bool _preferDownOnTie;

	[SerializeField]
	private UnityEvent onFlip;

	private string _desiredValue;

	private string _currentCommittedValue;

	private string _pendingValue;

	private bool _isFlipping;

	private Coroutine _flipCoroutine;

	float IFloatValueProvider.GetFloatValue()
	{
		return 0f;
	}

	private void Awake()
	{
	}

	public void SetDesiredValueAndApply(string value)
	{
	}

	public void SetDesiredCharAndApply(char c)
	{
	}

	public void SetValueInstant(string value)
	{
	}

	public void OnFlipAnimationFinished()
	{
	}

	public void OnFlip()
	{
	}

	private void CommitOld(string value)
	{
	}

	private void StageNew(string value)
	{
	}

	private void ClearNew()
	{
	}

	private void SetTexts(List<TMP_Text> texts, string value)
	{
	}

	private static char FirstCharOrNull(string s)
	{
		return '\0';
	}

	[IteratorStateMachine(typeof(_003CFlipCoroutine_003Ed__29))]
	private IEnumerator FlipCoroutine()
	{
		return null;
	}

	private void StopFlipping()
	{
	}

	private void Reset()
	{
	}
}
