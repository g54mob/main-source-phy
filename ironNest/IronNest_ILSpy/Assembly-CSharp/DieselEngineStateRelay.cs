using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DieselEngineStateRelay : MonoBehaviour
{
	public enum OnEnableTrigger
	{
		None,
		ForceOn,
		ForceOff
	}

	private sealed class _003CClearForceFlagsNextFrame_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DieselEngineStateRelay _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CClearForceFlagsNextFrame_003Ed__21(int _003C_003E1__state)
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
			//IL_005d: Expected I4, but got I8
			//IL_013c: Expected I4, but got O
			DieselEngineStateRelay dieselEngineStateRelay = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				if (dieselEngineStateRelay._engine != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object value = default(object);
					dieselEngineStateRelay._fieldForceOn.SetValue(dieselEngineStateRelay._engine, value);
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object value2 = default(object);
					dieselEngineStateRelay._fieldForceOff.SetValue(dieselEngineStateRelay._engine, value2);
					return false;
				}
			}
			return false;
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

	private string engineTag;

	private float retrySearchInterval;

	private OnEnableTrigger triggerOnEnable;

	private UnityEvent OnRelayEngineOn;

	private UnityEvent OnRelayEngineOff;

	private UnityEvent OnEngineNotFound;

	private bool debugLog;

	private DieselEngineController _engine;

	private FieldInfo _fieldForceOn;

	private FieldInfo _fieldForceOff;

	private float _retryTimer;

	private void OnEnable()
	{
		TryFindEngine();
		if (triggerOnEnable == OnEnableTrigger.ForceOn)
		{
			ForceEngineOn();
		}
		else if (triggerOnEnable == OnEnableTrigger.ForceOff)
		{
			ForceEngineOff();
		}
	}

	private void Update()
	{
		//IL_002e: Invalid comparison between I4 and F4
		//IL_0072: Invalid comparison between I4 and F4
		if (_engine == null && 0f < retrySearchInterval)
		{
			float deltaTime = Time.deltaTime;
			if (!(0f < (_retryTimer -= deltaTime)))
			{
				_retryTimer = retrySearchInterval;
				TryFindEngine();
			}
		}
	}

	public void ForceEngineOn()
	{
		if (EnsureEngine())
		{
			if (debugLog)
			{
				Debug.Log("[DieselEngineStateRelay] → ForceEngineOn dispatched.", this);
			}
			SetForceFields(forceOn: true, forceOff: false);
			if (OnRelayEngineOn != null)
			{
				OnRelayEngineOn.Invoke();
			}
		}
	}

	public void ForceEngineOff()
	{
		if (EnsureEngine())
		{
			if (debugLog)
			{
				Debug.Log("[DieselEngineStateRelay] → ForceEngineOff dispatched.", this);
			}
			SetForceFields(forceOn: false, forceOff: true);
			if (OnRelayEngineOff != null)
			{
				OnRelayEngineOff.Invoke();
			}
		}
	}

	public void ToggleEngine()
	{
		if (EnsureEngine())
		{
			DieselEngineController engine = _engine;
			if (!engine._003CEnginesRunning_003Ek__BackingField)
			{
				ForceEngineOn();
			}
			else
			{
				ForceEngineOff();
			}
		}
	}

	public void RefreshEngineReference()
	{
		_engine = null;
		TryFindEngine();
	}

	private void TryFindEngine()
	{
		GameObject gameObject = GameObject.FindWithTag(engineTag);
		string message2;
		if (gameObject != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			DieselEngineController engine = default(DieselEngineController);
			_engine = engine;
			if (_engine != null)
			{
				Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(DieselEngineController));
				FieldInfo field = typeFromHandle.GetField("forceEngineOn", (BindingFlags)36);
				_fieldForceOn = field;
				Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(DieselEngineController));
				FieldInfo field2 = typeFromHandle2.GetField("forceEngineOff", (BindingFlags)36);
				_fieldForceOff = field2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
					object obj2 = default(object);
					if (obj2 == null)
					{
						if ((debugLog ? 1 : 0) != (nint)obj2)
						{
							string text = gameObject.name;
							string message = "[DieselEngineStateRelay] Bound to DieselEngineController on '" + text + "'.";
							Debug.Log(message, this);
						}
						return;
					}
				}
				Debug.LogError("[DieselEngineStateRelay] Reflection failed — could not find 'forceEngineOn' or 'forceEngineOff' fields on DieselEngineController. Field names may have changed. Check DieselEngineController.cs.", this);
				_engine = null;
				return;
			}
			if (debugLog)
			{
				string text2 = gameObject.name;
				message2 = "[DieselEngineStateRelay] GameObject '" + text2 + "' is tagged '" + engineTag + "' but has no DieselEngineController.";
				goto IL_0298;
			}
		}
		else if (debugLog)
		{
			message2 = "[DieselEngineStateRelay] No GameObject with tag '" + engineTag + "' found.";
			goto IL_0298;
		}
		goto IL_02a7;
		IL_0298:
		Debug.LogWarning(message2, this);
		goto IL_02a7;
		IL_02a7:
		if (OnEngineNotFound != null)
		{
			OnEngineNotFound.Invoke();
		}
		_retryTimer = retrySearchInterval;
	}

	private bool EnsureEngine()
	{
		if (_engine == null)
		{
			TryFindEngine();
			if (_engine == null)
			{
				Debug.LogWarning("[DieselEngineStateRelay] Cannot dispatch command — no DieselEngineController found.", this);
				return false;
			}
		}
		return true;
	}

	private void SetForceFields(bool forceOn, bool forceOff)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value = default(object);
		_fieldForceOn.SetValue(_engine, value);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value2 = default(object);
		_fieldForceOff.SetValue(_engine, value2);
		_003CClearForceFlagsNextFrame_003Ed__21 obj = new _003CClearForceFlagsNextFrame_003Ed__21(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator ClearForceFlagsNextFrame()
	{
		_003CClearForceFlagsNextFrame_003Ed__21 obj = new _003CClearForceFlagsNextFrame_003Ed__21(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public DieselEngineStateRelay()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AA4D]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		engineTag = "DieselEngine";
		retrySearchInterval = 0.5f;
		base._002Ector();
	}
}
