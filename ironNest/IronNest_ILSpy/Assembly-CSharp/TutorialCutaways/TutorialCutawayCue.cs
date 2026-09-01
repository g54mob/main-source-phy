using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace TutorialCutaways;

public class TutorialCutawayCue : MonoBehaviour
{
	public enum OverlapMode
	{
		Ignore,
		Queue,
		Preempt
	}

	public enum DenialReason
	{
		None,
		UnknownKey,
		KeyUsageExceeded,
		ActiveIgnoreOverlap,
		PreemptPriorityInsufficient
	}

	private sealed class _003CCoro_ActivatedDelay_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TutorialCutawayCue _003C_003E4__this;

		private float _003Ct_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoro_ActivatedDelay_003Ed__50(int _003C_003E1__state)
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
			//IL_00f2: Expected I4, but got I8
			//IL_0260: Expected I4, but got O
			//IL_0067: Expected F4, but got I4
			//IL_0148: Invalid comparison between I and F4
			//IL_0247: Expected O, but got I
			UnityEngine.Object obj = _003C_003E4__this;
			string message;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+70]");
					if ((nint)0 < (nint)0)
					{
						_003Ct_003E5__2 = _003C_003E1__state;
						goto IL_0111;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+30]");
					if ((nint)0 != 0)
					{
						string name = _003C_003E4__this.name;
						message = "[TutorialCutawayCue:" + name + "] onCutawayActivatedDelayed (no delay).";
						goto IL_0203;
					}
					goto IL_0211;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_024c;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_0111;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0211:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+78]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+78]");
				((UnityEvent)0).Invoke();
			}
			goto IL_024c;
			IL_024c:
			return false;
			IL_0203:
			Debug.Log(message);
			goto IL_0211;
			IL_0111:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+B0]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+70]");
				if (0f > _003Ct_003E5__2)
				{
					float unscaledDeltaTime = Time.unscaledDeltaTime;
					float num = unscaledDeltaTime + _003Ct_003E5__2;
					_003C_003E2__current = null;
					_003Ct_003E5__2 = num;
					_003C_003E1__state = 1;
					return true;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+30]");
				if ((nint)0 != 0)
				{
					string name2 = _003C_003E4__this.name;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					message = $"[TutorialCutawayCue:{name2}] onCutawayActivatedDelayed fired after {arg:0.###}s.";
					goto IL_0203;
				}
				goto IL_0211;
			}
			goto IL_024c;
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

	private sealed class _003CCoro_Duration_003Ed__51 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TutorialCutawayCue _003C_003E4__this;

		private float _003Ct_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoro_Duration_003Ed__51(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0129: Expected I4, but got I8
			//IL_02fd: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_00f6: Expected I4, but got I8
			//IL_0212: Invalid comparison between I4 and F4
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0082: Expected I4, but got I8
			//IL_006e: Expected I4, but got I8
			TutorialCutawayCue tutorialCutawayCue = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						if ((nint)obj2 != 1)
						{
							goto IL_02d1;
						}
						_003C_003E1__state = -1;
						goto IL_0326;
					}
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null)
					{
						goto IL_00a1;
					}
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null)
					{
						goto IL_0345;
					}
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (!tutorialCutawayCue.manualDurationTrigger)
					{
						goto IL_0204;
					}
					if (tutorialCutawayCue.debugLogging)
					{
						string name = _003C_003E4__this.name;
						string message = "[TutorialCutawayCue:" + name + "] Waiting for manual duration trigger...";
						Debug.Log(message);
					}
					goto IL_0345;
				}
			}
			goto IL_02ef;
			IL_0204:
			if (0f < tutorialCutawayCue.durationSeconds)
			{
				_003Ct_003E5__2 = 0f;
				goto IL_0326;
			}
			_003C_003E2__current = null;
			_003C_003E1__state = 2;
			return true;
			IL_02ef:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0326:
			if ((object)_003C_003E4__this != null)
			{
				if (tutorialCutawayCue.durationSeconds > _003Ct_003E5__2)
				{
					float unscaledDeltaTime = Time.unscaledDeltaTime;
					float num = unscaledDeltaTime + _003Ct_003E5__2;
					_003C_003E2__current = null;
					_003Ct_003E5__2 = num;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_00a1;
			}
			goto IL_02ef;
			IL_02d1:
			return false;
			IL_0345:
			if (tutorialCutawayCue._durationCountdownTriggered)
			{
				goto IL_0204;
			}
			if (tutorialCutawayCue._003CIsActive_003Ek__BackingField)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_02d1;
			IL_00a1:
			TutorialCutawayService tutorialCutawayService = _003C_003E4__this.ResolveService();
			if (!(tutorialCutawayService != null))
			{
				_003C_003E4__this.Internal_End(interrupted: false);
				return false;
			}
			if ((object)tutorialCutawayService != null)
			{
				tutorialCutawayService.CompleteActive(_003C_003E4__this);
				goto IL_02d1;
			}
			goto IL_02ef;
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

	public TutorialCutawayService serviceReference;

	public string serviceTag;

	public bool debugLogging;

	public string key;

	public int priority;

	public OverlapMode overlapMode;

	public bool ActivationTrigger;

	public bool autoResetTrigger;

	public bool autoRequestOnEnable;

	public bool manualDurationTrigger;

	public float durationSeconds;

	public UnityEvent onCutawayActivated;

	public UnityEvent onCutawayCompleted;

	public UnityEvent onCutawayDenied;

	public UnityEvent onCutawayInterrupted;

	public float activatedDelaySeconds;

	public UnityEvent onCutawayActivatedDelayed;

	private Coroutine _lifecycleCoro;

	private Coroutine _delayedActivatedCoro;

	private bool _lastActivationTrigger;

	private bool _idInitialized;

	private bool _durationCountdownTriggered;

	private string _persistentId;

	private DenialReason _lastDenialReason;

	private string _lastDenialExtra;

	private bool _003CIsActive_003Ek__BackingField;

	public string PersistentId
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7EA]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			if (!_idInitialized || string.IsNullOrEmpty(_persistentId))
			{
				Guid guid = Guid.NewGuid();
				Guid guid2 = default(Guid);
				string persistentId = guid2.ToString("N");
				_persistentId = persistentId;
				_idInitialized = true;
			}
			return _persistentId;
		}
	}

	public bool IsActive
	{
		get
		{
			return _003CIsActive_003Ek__BackingField;
		}
		private set
		{
			_003CIsActive_003Ek__BackingField = value;
		}
	}

	public DenialReason lastDenialReason => _lastDenialReason;

	public string lastDenialExtra => _lastDenialExtra;

	private void Awake()
	{
		//IL_00a5: Expected O, but got I
		//IL_00b5: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7EA]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!_idInitialized || string.IsNullOrEmpty(_persistentId))
		{
			Guid guid = Guid.NewGuid();
			Guid guid2 = default(Guid);
			string persistentId = guid2.ToString("N");
			_persistentId = persistentId;
			_idInitialized = true;
		}
		string text = serviceTag;
		_lastActivationTrigger = ActivationTrigger;
		if (serviceTag == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rax_v7+B8]");
			object obj2 = 0;
			text = (string)obj2;
		}
		string text2 = text.Trim();
		serviceTag = text2;
	}

	private void OnEnable()
	{
		TryRegisterWithService();
		if (autoRequestOnEnable)
		{
			if (debugLogging)
			{
				string text = base.name;
				string message = "[TutorialCutawayCue:" + text + "] autoRequestOnEnable -> RequestActivate()";
				Debug.Log(message);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 81 Invalid \"Jump target not found in method: 0x1804F58A0\"");
		}
	}

	private void OnDisable()
	{
		TutorialCutawayService tutorialCutawayService = ResolveService();
		if (_003CIsActive_003Ek__BackingField && tutorialCutawayService != null)
		{
			tutorialCutawayService.CancelActive(this);
		}
		else if ((object)tutorialCutawayService == null)
		{
			return;
		}
		tutorialCutawayService.UnregisterCue(this);
	}

	private void Update()
	{
		if (!_lastActivationTrigger && ActivationTrigger)
		{
			if (debugLogging)
			{
				string text = base.name;
				string message = "[TutorialCutawayCue:" + text + "] ActivationTrigger rising-edge -> RequestActivate()";
				Debug.Log(message);
			}
			bool flag = RequestActivate();
			if (autoResetTrigger)
			{
				ActivationTrigger = false;
			}
		}
		_lastActivationTrigger = ActivationTrigger;
	}

	private void TryRegisterWithService()
	{
		TutorialCutawayService tutorialCutawayService = ResolveService();
		if (!(tutorialCutawayService != null))
		{
			string text = base.name;
			string message = "[TutorialCutawayCue] Service not found for '" + text + "'. Will attempt resolution on activation request.";
			Debug.LogWarning(message);
			return;
		}
		tutorialCutawayService.RegisterCue(this);
		if (debugLogging)
		{
			string text2 = base.name;
			string text3 = tutorialCutawayService.name;
			string message2 = "[TutorialCutawayCue:" + text2 + "] Registered with service '" + text3 + "'.";
			Debug.Log(message2);
		}
	}

	private TutorialCutawayService ResolveService()
	{
		TutorialCutawayService tutorialCutawayService2;
		if (serviceReference == null)
		{
			if (!TutorialCutawayService.HasInstance)
			{
				if (!string.IsNullOrEmpty(serviceTag))
				{
					GameObject gameObject = GameObject.FindWithTag(serviceTag);
					if ((bool)gameObject)
					{
						if ((object)gameObject == null)
						{
							goto IL_022f;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
						TutorialCutawayService tutorialCutawayService = default(TutorialCutawayService);
						bool flag = tutorialCutawayService != null;
						tutorialCutawayService2 = tutorialCutawayService;
						if (flag)
						{
							goto IL_020a;
						}
						if (debugLogging != flag)
						{
							string[] array = new string[5];
							if (array == null)
							{
								goto IL_022f;
							}
							array[0] = "[TutorialCutawayCue:";
							string text = base.name;
							array[1] = text;
							array[2] = "] GameObject with tag '";
							array[3] = serviceTag;
							array[4] = "' has no TutorialCutawayService component.";
							string message = string.Concat(array);
							Debug.LogWarning(message);
						}
					}
				}
				TutorialCutawayService tutorialCutawayService3 = UnityEngine.Object.FindObjectOfType<TutorialCutawayService>(includeInactive: true);
				bool flag2 = tutorialCutawayService3 != null;
				tutorialCutawayService2 = tutorialCutawayService3;
				if (!flag2)
				{
					return null;
				}
				goto IL_020a;
			}
			serviceReference = TutorialCutawayService._003CInstance_003Ek__BackingField;
		}
		goto IL_021e;
		IL_020a:
		serviceReference = tutorialCutawayService2;
		goto IL_021e;
		IL_021e:
		return serviceReference;
		IL_022f:
		return (TutorialCutawayService)(object)new NullReferenceException();
	}

	public bool RequestActivate()
	{
		//IL_01e3: Expected I4, but got O
		TutorialCutawayService tutorialCutawayService = ResolveService();
		bool flag = tutorialCutawayService == null;
		if (!flag)
		{
			if (debugLogging != flag)
			{
				string[] array = new string[5];
				if (array != null)
				{
					array[0] = "[TutorialCutawayCue:";
					string text = base.name;
					array[1] = text;
					array[2] = "] RequestActivate -> service '";
					if ((object)tutorialCutawayService != null)
					{
						string text2 = tutorialCutawayService.name;
						array[3] = text2;
						array[4] = "'";
						string message = string.Concat(array);
						Debug.Log(message);
						return tutorialCutawayService.RequestActivation(this);
					}
				}
			}
			else if ((object)tutorialCutawayService != null)
			{
				return tutorialCutawayService.RequestActivation(this);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		string text3 = base.name;
		string message2 = "[TutorialCutawayCue] No TutorialCutawayService found when '" + text3 + "' requested activation.";
		Debug.LogWarning(message2);
		Internal_Denied(DenialReason.UnknownKey, "ServiceMissing");
		return false;
	}

	public void StartDurationCountdown()
	{
		if (_003CIsActive_003Ek__BackingField && !_durationCountdownTriggered)
		{
			bool flag = !debugLogging;
			_durationCountdownTriggered = true;
			if (!flag)
			{
				string text = base.name;
				string message = "[TutorialCutawayCue:" + text + "] Duration countdown started manually.";
				Debug.Log(message);
			}
		}
	}

	public void CompleteEarly()
	{
		if (_003CIsActive_003Ek__BackingField)
		{
			TutorialCutawayService tutorialCutawayService = ResolveService();
			if (!(tutorialCutawayService == null))
			{
				tutorialCutawayService.CompleteActive(this);
			}
			else
			{
				Internal_End(interrupted: false);
			}
		}
	}

	public void Cancel()
	{
		if (_003CIsActive_003Ek__BackingField)
		{
			TutorialCutawayService tutorialCutawayService = ResolveService();
			if (!(tutorialCutawayService == null))
			{
				tutorialCutawayService.CancelActive(this);
			}
			else
			{
				Internal_End(interrupted: true);
			}
		}
	}

	internal void Internal_Begin()
	{
		if (!_003CIsActive_003Ek__BackingField)
		{
			_003CIsActive_003Ek__BackingField = true;
			_lastDenialReason = DenialReason.None;
			_lastDenialExtra = "";
			bool flag = !debugLogging;
			_durationCountdownTriggered = false;
			if (!flag)
			{
				string text = base.name;
				string message = "[TutorialCutawayCue:" + text + "] Activated.";
				Debug.Log(message);
			}
			if (onCutawayActivated != null)
			{
				onCutawayActivated.Invoke();
			}
			if (_delayedActivatedCoro != null)
			{
				StopCoroutine(_delayedActivatedCoro);
			}
			_003CCoro_ActivatedDelay_003Ed__50 obj = new _003CCoro_ActivatedDelay_003Ed__50(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine delayedActivatedCoro = StartCoroutine(obj);
			_delayedActivatedCoro = delayedActivatedCoro;
			if (_lifecycleCoro != null)
			{
				StopCoroutine(_lifecycleCoro);
			}
			_003CCoro_Duration_003Ed__51 obj2 = new _003CCoro_Duration_003Ed__51(0);
			obj2._003C_003E1__state = 0;
			obj2._003C_003E4__this = this;
			Coroutine lifecycleCoro = StartCoroutine(obj2);
			_lifecycleCoro = lifecycleCoro;
		}
	}

	internal void Internal_Denied(DenialReason reason, string reasonExtra)
	{
		//IL_0025: Expected I4, but got O
		_lastDenialReason = reason;
		bool flag = reasonExtra == null;
		string text = "";
		if (!flag)
		{
			text = reasonExtra;
		}
		_lastDenialExtra = text;
		if (debugLogging)
		{
			string arg = base.name;
			object obj = default(object);
			object arg2 = (DenialReason)obj;
			string message = $"[TutorialCutawayCue:{arg}] Denied. Reason={arg2} Extra={_lastDenialExtra}";
			Debug.Log(message);
		}
		if (onCutawayDenied != null)
		{
			onCutawayDenied.Invoke();
		}
	}

	internal void Internal_End(bool interrupted)
	{
		if (!_003CIsActive_003Ek__BackingField)
		{
			return;
		}
		if (_lifecycleCoro != null)
		{
			StopCoroutine(_lifecycleCoro);
			_lifecycleCoro = null;
		}
		if (_delayedActivatedCoro != null)
		{
			StopCoroutine(_delayedActivatedCoro);
			_delayedActivatedCoro = null;
		}
		_003CIsActive_003Ek__BackingField = false;
		UnityEvent unityEvent;
		if (!interrupted)
		{
			if (~(debugLogging ? 1u : 0u) == 0)
			{
				string text = base.name;
				string message = "[TutorialCutawayCue:" + text + "] Completed.";
				Debug.Log(message);
			}
			unityEvent = onCutawayCompleted;
		}
		else
		{
			if (~(debugLogging ? 1u : 0u) == 0)
			{
				string text2 = base.name;
				string message2 = "[TutorialCutawayCue:" + text2 + "] Interrupted.";
				Debug.Log(message2);
			}
			unityEvent = onCutawayInterrupted;
		}
		unityEvent?.Invoke();
	}

	private IEnumerator Coro_ActivatedDelay()
	{
		_003CCoro_ActivatedDelay_003Ed__50 obj = new _003CCoro_ActivatedDelay_003Ed__50(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator Coro_Duration()
	{
		_003CCoro_Duration_003Ed__51 obj = new _003CCoro_Duration_003Ed__51(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void Context_RequestActivate()
	{
		bool flag = RequestActivate();
	}

	private void Context_StartDurationCountdown()
	{
		if (_003CIsActive_003Ek__BackingField && !_durationCountdownTriggered)
		{
			bool flag = !debugLogging;
			_durationCountdownTriggered = true;
			if (!flag)
			{
				string text = base.name;
				string message = "[TutorialCutawayCue:" + text + "] Duration countdown started manually.";
				Debug.Log(message);
			}
		}
	}

	private void Context_CompleteEarly()
	{
		if (_003CIsActive_003Ek__BackingField)
		{
			TutorialCutawayService tutorialCutawayService = ResolveService();
			if (!(tutorialCutawayService == null))
			{
				tutorialCutawayService.CompleteActive(this);
			}
			else
			{
				Internal_End(interrupted: false);
			}
		}
	}

	private void Context_Cancel()
	{
		if (_003CIsActive_003Ek__BackingField)
		{
			TutorialCutawayService tutorialCutawayService = ResolveService();
			if (!(tutorialCutawayService == null))
			{
				tutorialCutawayService.CancelActive(this);
			}
			else
			{
				Internal_End(interrupted: true);
			}
		}
	}

	private void Context_LogServiceResolution()
	{
		TutorialCutawayService tutorialCutawayService = ResolveService();
		if (!(tutorialCutawayService != null))
		{
			string text = base.name;
			string message = "[TutorialCutawayCue:" + text + "] Service resolution failed. Check tag '" + serviceTag + "', singleton, or assign 'serviceReference'.";
			Debug.LogWarning(message);
		}
		else
		{
			string text2 = base.name;
			string text3 = tutorialCutawayService.name;
			string message2 = "[TutorialCutawayCue:" + text2 + "] Resolved service: '" + text3 + "'";
			Debug.Log(message2);
		}
	}

	public TutorialCutawayCue()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7FA]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		serviceTag = "TutorialCutawayService";
		key = "Default";
		overlapMode = OverlapMode.Queue;
		autoResetTrigger = true;
		durationSeconds = 3f;
		_lastDenialExtra = "";
		base._002Ector();
	}
}
