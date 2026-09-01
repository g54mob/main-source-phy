using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class InteractionLockStateRelay : MonoBehaviour
{
	private sealed class _003CRetryFindRoutine_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InteractionLockStateRelay _003C_003E4__this;

		private WaitForSecondsRealtime _003Cwait_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRetryFindRoutine_003Ed__31(int _003C_003E1__state)
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
			//IL_00af: Expected I4, but got I8
			//IL_01f3: Expected I4, but got O
			//IL_0044: Invalid comparison between F4 and I
			//IL_0074: Expected F4, but got I
			//IL_010d: Expected O, but got I
			//IL_0141: Expected O, but got I
			//IL_01a5: Expected O, but got I
			Behaviour behaviour = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Behaviour)+34]");
					bool flag = !(0.05f < 0f);
					float time = 0.05f;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Behaviour)+34]");
						time = 0f;
					}
					WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(time);
					_003Cwait_003E5__2 = waitForSecondsRealtime;
					goto IL_00ce;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_01df;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00ce;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00ce:
			if (_003C_003E4__this.isActiveAndEnabled)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Behaviour)+68]");
				if ((UnityEngine.Object)0 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Behaviour)+28]");
					if ((UnityEngine.Object)0 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Behaviour)+30]");
						if ((nint)0 != 0)
						{
							_003C_003E4__this.TryResolveBroker();
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Behaviour)+68]");
							if (!((UnityEngine.Object)0 == null))
							{
								goto IL_01df;
							}
							_003C_003E2__current = _003Cwait_003E5__2;
							_003C_003E1__state = 1;
							return true;
						}
					}
				}
			}
			_ = 0;
			goto IL_01df;
			IL_01df:
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

	private string brokerTag;

	private InteractionLockBroker explicitBroker;

	private bool autoFindBroker;

	private bool retryFindIfMissing;

	private float retryFindIntervalSeconds;

	private bool acquireOnEnable;

	private bool releaseOnDisable;

	private bool freezePlayerController;

	private bool useFreeMouse;

	private bool useUIActionMap;

	private bool hideVirtualCursorAndBlockWorld;

	private string debugLabel;

	private bool logWarnings;

	private UnityEvent onAcquired;

	private UnityEvent onReleased;

	private UnityEvent onBrokerFound;

	private InteractionLockBroker _broker;

	private bool _hadBroker;

	private InteractionLockBroker.LockHandle _handle;

	private Coroutine _retryRoutine;

	public bool IsLocked
	{
		get
		{
			if ((object)_handle == null)
			{
				return false;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (InteractionLockStateRelay)+78]");
			bool flag = (nint)0 < (nint)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (InteractionLockStateRelay)+78]");
			bool flag2 = (nint)0 == 0;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	private void OnEnable()
	{
		TryResolveBroker();
		if (retryFindIfMissing && _broker == null && _retryRoutine == null)
		{
			_003CRetryFindRoutine_003Ed__31 obj = new _003CRetryFindRoutine_003Ed__31(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine retryRoutine = StartCoroutine(obj);
			_retryRoutine = retryRoutine;
		}
		if (acquireOnEnable)
		{
			Acquire();
		}
	}

	private void OnDisable()
	{
		if (_retryRoutine != null)
		{
			StopCoroutine(_retryRoutine);
			_retryRoutine = null;
		}
		if (releaseOnDisable)
		{
			Release();
		}
	}

	public unsafe void Acquire()
	{
		//IL_0062: Expected O, but got Ref
		if ((object)_handle != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (InteractionLockStateRelay)+78]");
			if ((nint)0 > (nint)0)
			{
				return;
			}
		}
		InteractionLockBroker broker = GetBroker();
		if (!(broker == null))
		{
			object obj = default(object);
			InteractionLockBroker.LockHandle handle = broker.Acquire((InteractionLockBroker.LockRequest)(&obj));
			_handle = handle;
			if (onAcquired != null)
			{
				onAcquired.Invoke();
			}
		}
		else if (logWarnings)
		{
			string text = base.name;
			string message = text + ": InteractionLockStateRelay could not find InteractionLockBroker (tag='" + brokerTag + "'). Acquire ignored.";
			Debug.LogWarning(message, this);
		}
	}

	public void Release()
	{
		//IL_00cf: Expected O, but got I4
		if ((object)_handle == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (InteractionLockStateRelay)+78]");
		if ((nint)0 > (nint)0)
		{
			InteractionLockBroker broker = GetBroker();
			if (!(broker == null))
			{
				bool flag = broker.Release(_handle);
			}
			else if (logWarnings)
			{
				string text = base.name;
				string message = text + ": InteractionLockStateRelay broker missing during Release(). Handle cleared locally.";
				Debug.LogWarning(message, this);
			}
			_handle = (InteractionLockBroker.LockHandle)0;
			if (onReleased != null)
			{
				onReleased.Invoke();
			}
		}
	}

	public void Toggle()
	{
		if ((object)_handle != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (InteractionLockStateRelay)+78]");
			if ((nint)0 > (nint)0)
			{
				Release();
				return;
			}
		}
		Acquire();
	}

	public void SetLocked(bool locked)
	{
		if (!locked)
		{
			Release();
		}
		else
		{
			Acquire();
		}
	}

	private InteractionLockBroker GetBroker()
	{
		if (_broker == null)
		{
			TryResolveBroker();
		}
		return _broker;
	}

	private void TryResolveBroker()
	{
		bool flag = explicitBroker != null;
		if (!flag)
		{
			if (autoFindBroker == flag || string.IsNullOrWhiteSpace(brokerTag))
			{
				return;
			}
			InteractionLockBroker broker = InteractionLockBroker.FindOrNull(brokerTag);
			_broker = broker;
			if (!(_broker != null))
			{
				return;
			}
		}
		else
		{
			_broker = explicitBroker;
		}
		NotifyBrokerFoundIfNeeded();
	}

	private void NotifyBrokerFoundIfNeeded()
	{
		bool flag = _broker == null;
		if (!flag && _hadBroker == flag)
		{
			_hadBroker = true;
			if (onBrokerFound != null)
			{
				onBrokerFound.Invoke();
			}
		}
	}

	private IEnumerator RetryFindRoutine()
	{
		_003CRetryFindRoutine_003Ed__31 obj = new _003CRetryFindRoutine_003Ed__31(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public InteractionLockStateRelay()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC54]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		brokerTag = "LockBroker";
		autoFindBroker = true;
		retryFindIntervalSeconds = 0.5f;
		releaseOnDisable = true;
		useFreeMouse = true;
		debugLabel = "InteractionLockStateRelay";
		logWarnings = true;
		base._002Ector();
	}
}
