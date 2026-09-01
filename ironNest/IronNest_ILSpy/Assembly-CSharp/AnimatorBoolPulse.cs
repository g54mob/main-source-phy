using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class AnimatorBoolPulse : MonoBehaviour
{
	private sealed class _003CRevertAfterDelay_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AnimatorBoolPulse _003C_003E4__this;

		public uint token;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRevertAfterDelay_003Ed__17(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_00bc: Expected I4, but got I8
			//IL_038d: Expected I4, but got O
			//IL_0041: Invalid comparison between F4 and I4
			AnimatorBoolPulse animatorBoolPulse = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (animatorBoolPulse.pulseDuration > 0f)
					{
						WaitForSeconds waitForSeconds = new WaitForSeconds(animatorBoolPulse.pulseDuration);
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 1;
						return true;
					}
					goto IL_00db;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0379;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00db;
				}
			}
			goto IL_037f;
			IL_036a:
			animatorBoolPulse._revertCoroutine = null;
			goto IL_0379;
			IL_0379:
			return false;
			IL_03b9:
			string format;
			string arg;
			object parameterName;
			object arg2 = default(object);
			string message = string.Format(format, arg, parameterName, arg2);
			goto IL_0356;
			IL_037f:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00db:
			if (token != animatorBoolPulse._pulseToken || !_003C_003E4__this.EnsureReady())
			{
				goto IL_0379;
			}
			if ((object)animatorBoolPulse.animator != null)
			{
				bool flag = animatorBoolPulse.animator.GetBool(animatorBoolPulse._paramHash);
				if (!animatorBoolPulse.revertOnlyIfUnchanged)
				{
					if ((object)animatorBoolPulse.animator == null)
					{
						goto IL_037f;
					}
					animatorBoolPulse.animator.SetBool(animatorBoolPulse._paramHash, animatorBoolPulse.inactiveState);
					if (animatorBoolPulse.verboseLogging)
					{
						string name = _003C_003E4__this.name;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						parameterName = animatorBoolPulse.parameterName;
						format = "{0}: Auto-reverted '{1}' to {2} (forced).";
						arg = name;
						goto IL_03b9;
					}
				}
				else if (flag != animatorBoolPulse.activeState)
				{
					if (animatorBoolPulse.verboseLogging)
					{
						string name2 = _003C_003E4__this.name;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg3 = default(object);
						message = $"{name2}: Revert skipped (value changed externally). Current={arg3}";
						goto IL_0356;
					}
				}
				else
				{
					if ((object)animatorBoolPulse.animator == null)
					{
						goto IL_037f;
					}
					animatorBoolPulse.animator.SetBool(animatorBoolPulse._paramHash, animatorBoolPulse.inactiveState);
					if (animatorBoolPulse.verboseLogging)
					{
						string name3 = _003C_003E4__this.name;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						parameterName = animatorBoolPulse.parameterName;
						format = "{0}: Auto-reverted '{1}' to {2} (unchanged condition met).";
						arg = name3;
						goto IL_03b9;
					}
				}
				goto IL_036a;
			}
			goto IL_037f;
			IL_0356:
			Debug.Log(message, _003C_003E4__this);
			goto IL_036a;
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

	private Animator animator;

	private string parameterName;

	private bool inactiveState;

	private bool activeState;

	private float pulseDuration;

	private bool revertOnlyIfUnchanged;

	private bool logWarnings;

	private bool verboseLogging;

	private int _paramHash;

	private bool _paramHashValid;

	private Coroutine _revertCoroutine;

	private uint _pulseToken;

	private void Awake()
	{
		if (this.animator == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Animator animator = default(Animator);
			this.animator = animator;
		}
		ValidateParameter();
	}

	private void OnValidate()
	{
		//IL_0072: Invalid comparison between I4 and F4
		if (0f > pulseDuration)
		{
			pulseDuration = 0f;
		}
		if (animator != null && !string.IsNullOrEmpty(parameterName))
		{
			ValidateParameter();
		}
	}

	public void TriggerPulse()
	{
		//IL_00f8: Invalid comparison between F4 and I4
		if (!EnsureReady())
		{
			return;
		}
		bool flag = animator.GetBool(_paramHash);
		if (flag == inactiveState)
		{
			animator.SetBool(_paramHash, activeState);
			if (verboseLogging)
			{
				string arg = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message = $"{arg}: Pulse activated. Set '{parameterName}' to {arg2}";
				Debug.Log(message, this);
			}
			if (_revertCoroutine != null)
			{
				StopCoroutine(_revertCoroutine);
			}
			if (pulseDuration > 0f)
			{
				uint token = ++_pulseToken;
				_003CRevertAfterDelay_003Ed__17 obj = new _003CRevertAfterDelay_003Ed__17(0);
				obj._003C_003E1__state = 0;
				obj._003C_003E4__this = this;
				obj.token = token;
				Coroutine revertCoroutine = StartCoroutine(obj);
				_revertCoroutine = revertCoroutine;
			}
		}
		else if (verboseLogging)
		{
			string arg3 = base.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg4 = default(object);
			string message2 = $"{arg3}: Pulse skipped (already in active state or not matching inactiveState). Current={arg4}";
			Debug.Log(message2, this);
		}
	}

	public void CancelPendingRevert()
	{
		if (_revertCoroutine != null)
		{
			StopCoroutine(_revertCoroutine);
		}
		_revertCoroutine = null;
		if (verboseLogging)
		{
			string text = base.name;
			string message = text + ": Pending revert canceled.";
			Debug.Log(message, this);
		}
	}

	public void ForceRevert()
	{
		if (!EnsureReady())
		{
			return;
		}
		bool flag = animator.GetBool(_paramHash);
		if (flag == activeState)
		{
			animator.SetBool(_paramHash, inactiveState);
			if (verboseLogging)
			{
				string arg = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message = $"{arg}: Force reverted '{parameterName}' to {arg2}";
				Debug.Log(message, this);
			}
		}
		if (_revertCoroutine != null)
		{
			StopCoroutine(_revertCoroutine);
		}
		_revertCoroutine = null;
	}

	private IEnumerator RevertAfterDelay(uint token)
	{
		_003CRevertAfterDelay_003Ed__17 obj = new _003CRevertAfterDelay_003Ed__17(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.token = token;
		return obj;
	}

	private bool EnsureReady()
	{
		bool flag = animator == null;
		string message;
		if (!flag)
		{
			if (_paramHashValid != flag)
			{
				return true;
			}
			if (logWarnings)
			{
				string text = base.name;
				message = text + ": AnimatorBoolPulse parameter '" + parameterName + "' invalid or not a Bool.";
				goto IL_00dc;
			}
		}
		else if (logWarnings)
		{
			string text2 = base.name;
			message = text2 + ": AnimatorBoolPulse has no Animator assigned.";
			goto IL_00dc;
		}
		goto IL_00eb;
		IL_00dc:
		Debug.LogWarning(message, this);
		goto IL_00eb;
		IL_00eb:
		return false;
	}

	private void ValidateParameter()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_006b: Expected O, but got I4
		//IL_0074: Expected O, but got I4
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		_paramHashValid = false;
		if (!(animator != null) || string.IsNullOrEmpty(parameterName))
		{
			return;
		}
		AnimatorControllerParameter[] parameters = animator.parameters;
		object obj = parameters + 32;
		object obj2 = 0;
		object obj3 = 0;
		string text = default(string);
		object obj4 = default(object);
		string text2;
		string text3;
		string text4;
		while (true)
		{
			if ((nint)obj3 < parameters.Length)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808746E0");
				if (text != parameterName)
				{
					obj2++;
					obj += 8;
					obj3 = obj2;
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CF520");
				if ((nint)obj4 != 4)
				{
					if (logWarnings)
					{
						text2 = base.name;
						text3 = "' exists but is NOT a Bool.";
						text4 = ": Parameter '";
						break;
					}
				}
				else
				{
					int paramHash = Animator.StringToHash(parameterName);
					_paramHash = paramHash;
					_paramHashValid = true;
				}
				return;
			}
			if (logWarnings)
			{
				text2 = base.name;
				text3 = "' not found on Animator.";
				text4 = ": Bool parameter '";
				break;
			}
			return;
		}
		string message = text2 + text4 + parameterName + text3;
		Debug.LogWarning(message, this);
	}

	public AnimatorBoolPulse()
	{
		//IL_006e: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F78]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		parameterName = "isVisible";
		activeState = true;
		pulseDuration = 2f;
		revertOnlyIfUnchanged = true;
		_paramHash = -1;
		base._002Ector();
	}
}
