using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class NewTeleprinterStartTrigger : MonoBehaviour
{
	private sealed class _003CStartSequence_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NewTeleprinterStartTrigger _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CStartSequence_003Ed__24(int _003C_003E1__state)
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
			//IL_01a2: Expected I4, but got O
			//IL_0041: Invalid comparison between F4 and I4
			NewTeleprinterStartTrigger newTeleprinterStartTrigger = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (newTeleprinterStartTrigger.delayAfterTrigger > 0f)
					{
						WaitForSeconds waitForSeconds = new WaitForSeconds(newTeleprinterStartTrigger.delayAfterTrigger);
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 1;
						return true;
					}
					goto IL_00e0;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_018e;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00e0;
				}
			}
			goto IL_0194;
			IL_00e0:
			Teleprinter teleprinter = Teleprinter.GetTeleprinter(newTeleprinterStartTrigger.PrinterType);
			if (teleprinter != null)
			{
				Teleprinter teleprinter2 = Teleprinter.GetTeleprinter(newTeleprinterStartTrigger.PrinterType);
				if ((object)teleprinter2 == null)
				{
					goto IL_0194;
				}
				teleprinter2.TryStart(newTeleprinterStartTrigger.bypassTypewriterInitialDelay);
				if (newTeleprinterStartTrigger.OnTriggered != null)
				{
					newTeleprinterStartTrigger.OnTriggered.Invoke();
				}
			}
			goto IL_018e;
			IL_018e:
			return false;
			IL_0194:
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

	public Teleprinter.Teleprinters PrinterType;

	public float delayAfterTrigger;

	public bool bypassTypewriterInitialDelay = true;

	public bool startOnNextJobIfEmpty;

	public bool oneShot = true;

	public List<string> allowedTags;

	public LayerMask allowedLayers;

	public bool debugLogging;

	public bool subscribeForDeferredStart;

	public UnityEvent OnTriggered;

	private bool _triggered;

	private bool _armedForNextJobs;

	private bool _deferredSubscribed;

	private Teleprinter Printer => Teleprinter.GetTeleprinter(PrinterType);

	private void Awake()
	{
	}

	private void OnEnable()
	{
		if (_armedForNextJobs)
		{
			TrySubscribeDeferred();
		}
	}

	private void OnDisable()
	{
		UnsubscribeDeferred();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (IsAllowed(other))
		{
			GameObject activator = other.gameObject;
			HandleTrigger(activator);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (IsAllowed2D(other))
		{
			GameObject activator = other.gameObject;
			HandleTrigger(activator);
		}
	}

	private bool IsAllowed(Collider other)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected I4, but got Unknown
		//IL_00de: Expected O, but got I4
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		List<string>.Enumerator enumerator = default(List<string>.Enumerator);
		if ((object)other != null)
		{
			GameObject gameObject = other.gameObject;
			if ((object)gameObject != null)
			{
				int layer = gameObject.layer;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
				object obj = default(object);
				if (obj != null)
				{
					object obj2 = this + 56;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
					int num = layer & 0x1F;
					int num2 = 1 << num;
					object obj3 = default(object);
					int num3 = obj3 & num2;
					bool flag = num3 == 0;
					bool flag2 = num3 < 0;
					bool flag3 = !flag2;
					object obj4 = !flag3;
					object obj5 = obj4 | flag;
					if (obj5 != null)
					{
						goto IL_01c9;
					}
				}
				if (allowedTags != null)
				{
					List<string> list = allowedTags;
					if (list._size > 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						string value = default(string);
						while (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if (string.IsNullOrEmpty(value) || !other.CompareTag(value))
							{
								continue;
							}
							goto IL_01ad;
						}
						enumerator.Dispose();
						goto IL_01c9;
					}
				}
				goto IL_01cf;
			}
		}
		throw new NullReferenceException();
		IL_01c9:
		return false;
		IL_01cf:
		return true;
		IL_01ad:
		enumerator.Dispose();
		goto IL_01cf;
	}

	private bool IsAllowed2D(Collider2D other)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected I4, but got Unknown
		//IL_00de: Expected O, but got I4
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		List<string>.Enumerator enumerator = default(List<string>.Enumerator);
		if ((object)other != null)
		{
			GameObject gameObject = other.gameObject;
			if ((object)gameObject != null)
			{
				int layer = gameObject.layer;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
				object obj = default(object);
				if (obj != null)
				{
					object obj2 = this + 56;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
					int num = layer & 0x1F;
					int num2 = 1 << num;
					object obj3 = default(object);
					int num3 = obj3 & num2;
					bool flag = num3 == 0;
					bool flag2 = num3 < 0;
					bool flag3 = !flag2;
					object obj4 = !flag3;
					object obj5 = obj4 | flag;
					if (obj5 != null)
					{
						goto IL_01c9;
					}
				}
				if (allowedTags != null)
				{
					List<string> list = allowedTags;
					if (list._size > 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						string value = default(string);
						while (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if (string.IsNullOrEmpty(value) || !other.CompareTag(value))
							{
								continue;
							}
							goto IL_01ad;
						}
						enumerator.Dispose();
						goto IL_01c9;
					}
				}
				goto IL_01cf;
			}
		}
		throw new NullReferenceException();
		IL_01c9:
		return false;
		IL_01cf:
		return true;
		IL_01ad:
		enumerator.Dispose();
		goto IL_01cf;
	}

	private bool LayerAllowed(int layer)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected I4, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		object obj = default(object);
		if (obj != null)
		{
			object obj2 = this + 56;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
			int num = layer & 0x1F;
			int num2 = 1 << num;
			object obj3 = default(object);
			int num3 = num2 & obj3;
			bool flag = num3 == 0;
			return !flag;
		}
		return true;
	}

	private void HandleTrigger(GameObject activator)
	{
		if (_triggered && oneShot)
		{
			return;
		}
		Teleprinter teleprinter = Teleprinter.GetTeleprinter(PrinterType);
		if (teleprinter != null)
		{
			_triggered = true;
			Teleprinter teleprinter2 = Teleprinter.GetTeleprinter(PrinterType);
			bool hasJobs = teleprinter2.HasJobs;
			if (!hasJobs)
			{
				if (startOnNextJobIfEmpty == hasJobs)
				{
					if (~(debugLogging ? 1u : 0u) == 0)
					{
						string text = activator.name;
						string message = "[TeleprinterStartTrigger] Triggered by " + text + "; no jobs and startOnNextJobIfEmpty is false. Nothing done.";
						Debug.Log(message, this);
					}
					return;
				}
				_armedForNextJobs = true;
				if (~(debugLogging ? 1u : 0u) == 0)
				{
					string text2 = activator.name;
					string message2 = "[TeleprinterStartTrigger] Triggered by " + text2 + "; no jobs yet. Armed to start when jobs arrive.";
					Debug.Log(message2, this);
				}
				TrySubscribeDeferred();
			}
			else
			{
				if (~(debugLogging ? 1u : 0u) == 0)
				{
					string text3 = activator.name;
					string message3 = "[TeleprinterStartTrigger] Triggered by " + text3 + "; starting now (jobs present).";
					Debug.Log(message3, this);
				}
				IEnumerator routine = StartSequence();
				Coroutine coroutine = StartCoroutine(routine);
			}
		}
		else if (debugLogging)
		{
			Debug.LogWarning("[TeleprinterStartTrigger] No targetTypewriter assigned; ignoring trigger.", this);
		}
	}

	private IEnumerator StartSequence()
	{
		_003CStartSequence_003Ed__24 obj = new _003CStartSequence_003Ed__24(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void TrySubscribeDeferred()
	{
		if (_armedForNextJobs && subscribeForDeferredStart && !_deferredSubscribed)
		{
			Teleprinter printer = Printer;
			if (printer != null)
			{
				Teleprinter printer2 = Printer;
				UnityAction call = OnJobsEnqueuedDeferred;
				printer2.onJobsEnqueued.AddListener(call);
				_deferredSubscribed = true;
			}
		}
	}

	private void UnsubscribeDeferred()
	{
		if (_deferredSubscribed)
		{
			Teleprinter teleprinter = Teleprinter.GetTeleprinter(PrinterType);
			if (teleprinter != null)
			{
				Teleprinter teleprinter2 = Teleprinter.GetTeleprinter(PrinterType);
				UnityAction call = OnJobsEnqueuedDeferred;
				teleprinter2.onJobsEnqueued.RemoveListener(call);
				_deferredSubscribed = false;
			}
		}
	}

	private void OnJobsEnqueuedDeferred()
	{
		if (!_armedForNextJobs)
		{
			return;
		}
		Teleprinter teleprinter = Teleprinter.GetTeleprinter(PrinterType);
		if (!(teleprinter != null))
		{
			return;
		}
		Teleprinter teleprinter2 = Teleprinter.GetTeleprinter(PrinterType);
		if (teleprinter2.HasJobs)
		{
			if (debugLogging)
			{
				Debug.Log("[TeleprinterStartTrigger] Jobs arrived after arming; starting typewriter.", this);
			}
			_armedForNextJobs = false;
			UnsubscribeDeferred();
			IEnumerator routine = StartSequence();
			Coroutine coroutine = StartCoroutine(routine);
		}
	}

	public void ResetTrigger()
	{
		_triggered = false;
		UnsubscribeDeferred();
		if (debugLogging)
		{
			Debug.Log("[TeleprinterStartTrigger] ResetTrigger called; trigger is ready again.", this);
		}
	}

	public void ArmProgrammatically()
	{
		if (_triggered && oneShot)
		{
			return;
		}
		Teleprinter teleprinter = Teleprinter.GetTeleprinter(PrinterType);
		if (!(teleprinter != null))
		{
			return;
		}
		_triggered = true;
		Teleprinter teleprinter2 = Teleprinter.GetTeleprinter(PrinterType);
		bool hasJobs = teleprinter2.HasJobs;
		if (!hasJobs)
		{
			if (startOnNextJobIfEmpty != hasJobs)
			{
				if (debugLogging != hasJobs)
				{
					Debug.Log("[TeleprinterStartTrigger] Programmatically armed; waiting for jobs.", this);
				}
				_armedForNextJobs = true;
				TrySubscribeDeferred();
			}
		}
		else
		{
			if (debugLogging)
			{
				Debug.Log("[TeleprinterStartTrigger] Programmatically armed; jobs present, starting.", this);
			}
			IEnumerator routine = StartSequence();
			Coroutine coroutine = StartCoroutine(routine);
		}
	}

	public NewTeleprinterStartTrigger()
	{
		List<string> list = new List<string>();
		allowedTags = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		LayerMask layerMask = default(LayerMask);
		allowedLayers = layerMask;
		subscribeForDeferredStart = true;
		base._002Ector();
	}
}
