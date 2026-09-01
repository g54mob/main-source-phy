using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SplitFlipDisplaySimple : MonoBehaviour, ISplitFlipDisplay, IFloatValueProvider
{
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

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CFlipCoroutine_003Ed__29(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_01bb: Expected I4, but got I8
			//IL_07fa: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_00f3: Expected I4, but got I8
			//IL_0052: Expected I4, but got I8
			//IL_00d5: Expected O, but got I4
			//IL_05bc: Expected O, but got I4
			//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d5: Expected O, but got Unknown
			//IL_05df: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e4: Expected O, but got Unknown
			//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f3: Expected O, but got Unknown
			//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0602: Expected I4, but got Unknown
			//IL_0426: Expected O, but got I4
			//IL_042e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0433: Expected O, but got Unknown
			//IL_044f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0454: Expected O, but got Unknown
			//IL_0463: Expected O, but got I4
			//IL_0470: Unknown result type (might be due to invalid IL or missing references)
			//IL_0475: Expected O, but got Unknown
			//IL_0482: Unknown result type (might be due to invalid IL or missing references)
			//IL_0487: Expected O, but got Unknown
			//IL_03de: Expected O, but got I4
			//IL_04bc: Expected O, but got I4
			//IL_08d8: Expected I4, but got O
			//IL_09c2: Expected I4, but got O
			//IL_09cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d2: Expected O, but got Unknown
			//IL_090c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0911: Expected O, but got Unknown
			//IL_0929: Expected O, but got I4
			//IL_0932: Unknown result type (might be due to invalid IL or missing references)
			//IL_0937: Expected O, but got Unknown
			//IL_0940: Unknown result type (might be due to invalid IL or missing references)
			//IL_0945: Expected I4, but got Unknown
			//IL_0582: Expected O, but got I4
			//IL_0592: Unknown result type (might be due to invalid IL or missing references)
			//IL_0597: Expected O, but got Unknown
			SplitFlipDisplaySimple splitFlipDisplaySimple = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			string text;
			string text2;
			string text3;
			bool flag4;
			SplitFlipDisplaySimple splitFlipDisplaySimple2;
			string currentCommittedValue;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						goto IL_06e7;
					}
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null)
					{
						splitFlipDisplaySimple._currentCommittedValue = splitFlipDisplaySimple._pendingValue;
						currentCommittedValue = splitFlipDisplaySimple._currentCommittedValue;
						_003C_003E4__this.SetTexts(splitFlipDisplaySimple._oldTexts, splitFlipDisplaySimple._currentCommittedValue);
						int num = _003Ci_003E5__8 + 1;
						_003Ci_003E5__8 = num;
						object obj2 = 0;
						splitFlipDisplaySimple2 = _003C_003E4__this;
						goto IL_0823;
					}
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null)
					{
						splitFlipDisplaySimple._isFlipping = true;
						if ((object)splitFlipDisplaySimple._animator != null)
						{
							splitFlipDisplaySimple._animator.SetTrigger(_003Ctrigger_003E5__6);
							Func<bool> predicate = () => _003C_003E4__this._isFlipping;
							WaitWhile waitWhile = new WaitWhile(predicate);
							_003C_003E2__current = waitWhile;
							_003C_003E1__state = 2;
							return true;
						}
					}
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					bool flag2 = splitFlipDisplaySimple._desiredValue == null;
					text = "";
					if (!flag2)
					{
						text = splitFlipDisplaySimple._desiredValue;
					}
					if (!(text != splitFlipDisplaySimple._currentCommittedValue))
					{
						goto IL_06e7;
					}
					char value;
					if (!string.IsNullOrEmpty(text))
					{
						if (text == null)
						{
							goto IL_07ec;
						}
						value = text.get_Chars(0);
					}
					else
					{
						value = '\0';
					}
					if (splitFlipDisplaySimple._orderedSymbols != null)
					{
						int num2 = splitFlipDisplaySimple._orderedSymbols.IndexOf(value);
						if (num2 < 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0B2]");
							if ((nint)0 == 0)
							{
								_ = 1;
							}
							if (splitFlipDisplaySimple._flipCoroutine != null)
							{
								_003C_003E4__this.StopCoroutine(splitFlipDisplaySimple._flipCoroutine);
								splitFlipDisplaySimple._flipCoroutine = null;
							}
							splitFlipDisplaySimple._isFlipping = false;
							if (splitFlipDisplaySimple._pendingValue != null)
							{
								splitFlipDisplaySimple._currentCommittedValue = splitFlipDisplaySimple._pendingValue;
								_003C_003E4__this.SetTexts(splitFlipDisplaySimple._oldTexts, splitFlipDisplaySimple._currentCommittedValue);
								splitFlipDisplaySimple._pendingValue = null;
							}
							if (text == null)
							{
								text = "";
							}
							goto IL_0989;
						}
						char value2;
						if (!string.IsNullOrEmpty(splitFlipDisplaySimple._currentCommittedValue))
						{
							if (splitFlipDisplaySimple._currentCommittedValue == null)
							{
								goto IL_07ec;
							}
							value2 = splitFlipDisplaySimple._currentCommittedValue.get_Chars(0);
						}
						else
						{
							value2 = '\0';
						}
						if (splitFlipDisplaySimple._orderedSymbols != null)
						{
							if ((_003CcurrentIndex_003E5__2 = splitFlipDisplaySimple._orderedSymbols.IndexOf(value2)) < 0)
							{
								if (splitFlipDisplaySimple._orderedSymbols == null)
								{
									goto IL_07ec;
								}
								char c = splitFlipDisplaySimple._orderedSymbols.get_Chars(0);
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
								string currentCommittedValue2 = default(string);
								splitFlipDisplaySimple._currentCommittedValue = currentCommittedValue2;
								_003C_003E4__this.SetTexts(splitFlipDisplaySimple._oldTexts, splitFlipDisplaySimple._currentCommittedValue);
								_003CcurrentIndex_003E5__2 = 0;
								object obj2 = 0;
							}
							if (_003CcurrentIndex_003E5__2 == num2)
							{
								goto IL_0989;
							}
							string orderedSymbols = splitFlipDisplaySimple._orderedSymbols;
							if (splitFlipDisplaySimple._orderedSymbols != null)
							{
								object obj3 = orderedSymbols._stringLength - _003CcurrentIndex_003E5__2;
								object obj4 = obj3 + num2;
								_003CsymbolCount_003E5__3 = orderedSymbols._stringLength;
								text2 = (string)(obj4 % orderedSymbols._stringLength);
								object obj5 = _003CcurrentIndex_003E5__2 - num2;
								object obj6 = obj5 + orderedSymbols._stringLength;
								text3 = (string)(obj6 % orderedSymbols._stringLength);
								bool flag3 = splitFlipDisplaySimple._directionMode == SplitFlipDisplay.DirectionMode.AutoShortest;
								if (!flag3)
								{
									object obj7 = splitFlipDisplaySimple._directionMode - 1;
									if (!flag3)
									{
										if ((nint)obj7 == 1)
										{
											goto IL_04ea;
										}
										goto IL_0530;
									}
								}
								else if (System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref text2) >= System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref text3))
								{
									if (System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref text2) > System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref text3))
									{
										goto IL_04ea;
									}
									goto IL_0530;
								}
								flag4 = false;
								goto IL_08f9;
							}
						}
					}
				}
			}
			goto IL_07ec;
			IL_04ea:
			flag4 = true;
			goto IL_08ce;
			IL_0823:
			if (_003Ci_003E5__8 <= _003CactualSteps_003E5__5)
			{
				int num3 = _003Ci_003E5__8 / _003CactualSteps_003E5__5;
				object obj8 = num3 * _003Csteps_003E5__4;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				object obj10 = default(object);
				object obj9 = obj10 * _003CdirectionMultiplier_003E5__7;
				object obj11 = obj9 + _003CsymbolCount_003E5__3;
				object obj12 = obj11 + _003CcurrentIndex_003E5__2;
				int index = obj12 % _003CsymbolCount_003E5__3;
				if (splitFlipDisplaySimple._orderedSymbols != null)
				{
					char c2 = splitFlipDisplaySimple._orderedSymbols.get_Chars(index);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
					string pendingValue = default(string);
					splitFlipDisplaySimple._pendingValue = pendingValue;
					_003C_003E4__this.SetTexts(splitFlipDisplaySimple._newTexts, splitFlipDisplaySimple._pendingValue);
					Func<bool> predicate2 = () => _003C_003E4__this._isFlipping;
					WaitWhile waitWhile2 = new WaitWhile(predicate2);
					_003C_003E2__current = waitWhile2;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_07ec;
			}
			splitFlipDisplaySimple._pendingValue = null;
			splitFlipDisplaySimple._flipCoroutine = null;
			goto IL_06e7;
			IL_08ce:
			_003Csteps_003E5__4 = (int)text3;
			if ((nint)text3 >= splitFlipDisplaySimple._maxFlipsUntilDesired)
			{
				text3 = (string)splitFlipDisplaySimple._maxFlipsUntilDesired;
			}
			_003CactualSteps_003E5__5 = (int)text3;
			object obj13 = _003C_003E4__this + 56;
			if (flag4)
			{
				obj13 = _003C_003E4__this + 64;
			}
			splitFlipDisplaySimple2 = (SplitFlipDisplaySimple)(this + 56);
			_003Ctrigger_003E5__6 = (string)obj13;
			object obj14 = (flag4 ? 1 : 0) ^ 1;
			object obj15 = obj14 * 2;
			int num4 = obj15 - 1;
			_003CdirectionMultiplier_003E5__7 = num4;
			_003Ci_003E5__8 = 1;
			currentCommittedValue = text2;
			goto IL_0823;
			IL_06e7:
			return false;
			IL_08f9:
			text3 = text2;
			goto IL_08ce;
			IL_0989:
			splitFlipDisplaySimple._currentCommittedValue = text;
			_003C_003E4__this.SetTexts(splitFlipDisplaySimple._oldTexts, splitFlipDisplaySimple._currentCommittedValue);
			goto IL_06e7;
			IL_0530:
			bool flag5 = !splitFlipDisplaySimple._preferDownOnTie;
			flag4 = !flag5;
			if (splitFlipDisplaySimple._preferDownOnTie)
			{
				goto IL_08ce;
			}
			goto IL_08f9;
			IL_07ec:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private List<TMP_Text> _oldTexts;

	private List<TMP_Text> _newTexts;

	private Animator _animator;

	private string _flipUpTrigger;

	private string _flipDownTrigger;

	private float _baselineAnimatorSpeedOverride;

	private string _orderedSymbols;

	private string _initialValue;

	private int _maxFlipsUntilDesired;

	private SplitFlipDisplay.DirectionMode _directionMode;

	private bool _preferDownOnTie;

	private UnityEvent onFlip;

	private string _desiredValue;

	private string _currentCommittedValue;

	private string _pendingValue;

	private bool _isFlipping;

	private Coroutine _flipCoroutine;

	float IFloatValueProvider.GetFloatValue()
	{
		return 0.5f;
	}

	private void Awake()
	{
		//IL_000b: Invalid comparison between F4 and I4
		if (_baselineAnimatorSpeedOverride > 0f)
		{
			_animator.speed = _baselineAnimatorSpeedOverride;
		}
		_desiredValue = _initialValue;
		_currentCommittedValue = _initialValue;
		SetTexts(_oldTexts, _currentCommittedValue);
	}

	public void SetDesiredValueAndApply(string value)
	{
		_desiredValue = value;
		StopFlipping();
		_003CFlipCoroutine_003Ed__29 obj = new _003CFlipCoroutine_003Ed__29(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine flipCoroutine = StartCoroutine(obj);
		_flipCoroutine = flipCoroutine;
	}

	public void SetDesiredCharAndApply(char c)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0B1]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		char c2 = default(char);
		string desiredValue = default(string);
		if (c2 == '\0')
		{
			desiredValue = "";
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
		}
		_desiredValue = desiredValue;
		StopFlipping();
		_003CFlipCoroutine_003Ed__29 obj = new _003CFlipCoroutine_003Ed__29(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine flipCoroutine = StartCoroutine(obj);
		_flipCoroutine = flipCoroutine;
	}

	public void SetValueInstant(string value)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0B2]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		StopFlipping();
		bool flag = value != null;
		string currentCommittedValue = value;
		if (!flag)
		{
			currentCommittedValue = "";
		}
		_currentCommittedValue = currentCommittedValue;
		SetTexts(_oldTexts, _currentCommittedValue);
	}

	public void OnFlipAnimationFinished()
	{
		_isFlipping = false;
	}

	public void OnFlip()
	{
		if (onFlip != null)
		{
			onFlip.Invoke();
		}
	}

	private void CommitOld(string value)
	{
		SetTexts(_oldTexts, value);
	}

	private void StageNew(string value)
	{
		SetTexts(_newTexts, value);
	}

	private void ClearNew()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0B3]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		SetTexts(_newTexts, "");
	}

	private void SetTexts(List<TMP_Text> texts, string value)
	{
		//IL_0070: Expected I, but got O
		if (texts == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<TMP_Text>.Enumerator enumerator = default(List<TMP_Text>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((bool)obj)
				{
					if ((object)obj == null)
					{
						break;
					}
					nint num = (nint)obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v212 @ r8_v5 (Il2CppClass<UnityEngine.Object>)+558] (should have been resolved before IL gen)");
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private static char FirstCharOrNull(string s)
	{
		//IL_0063: Expected I4, but got O
		if (!string.IsNullOrEmpty(s))
		{
			if (s != null)
			{
				return s.get_Chars(0);
			}
			NullReferenceException ex = new NullReferenceException();
			return (char)(int)ex;
		}
		return '\0';
	}

	private IEnumerator FlipCoroutine()
	{
		_003CFlipCoroutine_003Ed__29 obj = new _003CFlipCoroutine_003Ed__29(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void StopFlipping()
	{
		if (_flipCoroutine != null)
		{
			StopCoroutine(_flipCoroutine);
			_flipCoroutine = null;
		}
		bool flag = _pendingValue == null;
		_isFlipping = false;
		if (!flag)
		{
			_currentCommittedValue = _pendingValue;
			SetTexts(_oldTexts, _currentCommittedValue);
			_pendingValue = null;
		}
	}

	private void Reset()
	{
		if (_animator == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
			Animator animator = default(Animator);
			_animator = animator;
		}
	}

	public SplitFlipDisplaySimple()
	{
		List<TMP_Text> oldTexts = new List<TMP_Text>();
		_oldTexts = oldTexts;
		_newTexts = new List<TMP_Text>();
		_flipUpTrigger = "FlipUp";
		_flipDownTrigger = "FlipDown";
		_orderedSymbols = " ABCDEFGHIJKLMNOPQRSTUVWXYZ/";
		_initialValue = "A";
		_maxFlipsUntilDesired = 1;
		_preferDownOnTie = true;
		base._002Ector();
	}

	private bool _003CFlipCoroutine_003Eb__29_0()
	{
		return _isFlipping;
	}

	private bool _003CFlipCoroutine_003Eb__29_1()
	{
		return _isFlipping;
	}
}
