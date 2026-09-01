using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;

public class OperationLoadRelay : MonoBehaviour
{
	public enum StartupAction
	{
		None,
		StartAssignedOperation,
		ReturnToMainMenu
	}

	private sealed class _003CCoReturnToMainMenu_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public OperationLoadRelay _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoReturnToMainMenu_003Ed__19(int _003C_003E1__state)
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
			//IL_001f: Invalid comparison between F4 and I4
			//IL_0097: Expected I4, but got I8
			//IL_0245: Expected I4, but got O
			//IL_00d2: Expected O, but got I
			//IL_0226: Expected O, but got I
			//IL_0196: Expected O, but got I
			UnityEngine.Object obj = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if (delay > 0f)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_01ab;
				}
				_003C_003E1__state = -1;
			}
			if ((object)_003C_003E4__this != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+40]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+40]");
					((UnityEvent)0).Invoke();
				}
				if (!(MissionManager._003CInstance_003Ek__BackingField != null))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+58]");
					if ((nint)0 != 0)
					{
						Debug.LogWarning("[OperationLoadRelay] MissionManager.Instance not found. Cannot load Main Menu.", _003C_003E4__this);
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+50]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+50]");
						((UnityEvent)0).Invoke();
					}
					_ = 0;
					return false;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+58]");
				if ((nint)0 != 0)
				{
					Debug.Log("[OperationLoadRelay] Returning to Main Menu...", _003C_003E4__this);
				}
				if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
				{
					MissionManager._003CInstance_003Ek__BackingField.LoadMainMenu();
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+48]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+48]");
						((UnityEvent)0).Invoke();
					}
					_003C_003E4__this.Succeed();
					goto IL_01ab;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_01ab:
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

	private sealed class _003CCoStartOperation_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public OperationLoadRelay _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoStartOperation_003Ed__18(int _003C_003E1__state)
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
			//IL_001f: Invalid comparison between F4 and I4
			//IL_0097: Expected I4, but got I8
			//IL_00d2: Expected O, but got I
			//IL_0110: Expected O, but got I
			//IL_0133: Expected O, but got I
			//IL_0165: Expected O, but got I
			//IL_038d: Expected O, but got I
			//IL_01a1: Expected O, but got I
			//IL_029e: Expected O, but got I
			//IL_01e2: Expected O, but got I4
			//IL_021e: Expected O, but got I
			//IL_02d4: Expected O, but got I
			//IL_0407: Expected I4, but got O
			//IL_0267: Expected O, but got I
			UnityEngine.Object obj = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if (delay > 0f)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_03c9;
				}
				_003C_003E1__state = -1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+40]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+40]");
				((UnityEvent)0).Invoke();
			}
			object message2;
			if (MissionManager._003CInstance_003Ek__BackingField != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+20]");
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+20]");
					List<MissionNode> missions = ((OperationGraph)0).Missions;
					if (missions != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+20]");
						List<MissionNode> missions2 = ((OperationGraph)0).Missions;
						if (missions2._size != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+20]");
							List<MissionNode> missions3 = ((OperationGraph)0).Missions;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+38]");
							if ((nint)0 >= (nint)0)
							{
								object obj2 = missions3._size - 1;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+38]");
								if (0 <= (nint)obj2)
								{
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+58]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+20]");
								object obj3 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+20]");
								if ((nint)0 == 0)
								{
									NullReferenceException ex = new NullReferenceException();
									return (byte)(int)ex != 0;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rbx_v10+60]");
								object arg = default(object);
								string message = $"[OperationLoadRelay] Starting operation '{0}' at mission index {arg}...";
								Debug.Log(message, obj);
							}
							MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+20]");
							missionManager.StartOperation((OperationGraph)0, null);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+48]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+48]");
								((UnityEvent)0).Invoke();
							}
							((OperationLoadRelay)obj).Succeed();
							goto IL_03c9;
						}
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+58]");
				if ((nint)0 == 0)
				{
					goto IL_0357;
				}
				message2 = "[OperationLoadRelay] Assigned operation is null or empty. Cannot start.";
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+58]");
				if ((nint)0 == 0)
				{
					goto IL_0357;
				}
				message2 = "[OperationLoadRelay] MissionManager.Instance not found. Cannot start operation.";
			}
			Debug.LogWarning(message2, obj);
			goto IL_0357;
			IL_03c9:
			return false;
			IL_0357:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+50]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Object)+50]");
				((UnityEvent)0).Invoke();
			}
			_ = 0;
			goto IL_03c9;
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

	public OperationGraph operation;

	public bool invokeOnStart;

	public float delaySeconds;

	public bool preventReentry = true;

	public bool disableAfterSuccess;

	public StartupAction startupAction;

	public int startMissionIndex;

	public UnityEvent onBeforeAction;

	public UnityEvent onAfterAction;

	public UnityEvent onActionFailed;

	public bool verbose;

	private bool _busy;

	private void Start()
	{
		//IL_0034: Expected O, but got I4
		//IL_00ed: Invalid comparison between I4 and F4
		//IL_00ff: Expected F4, but got I4
		//IL_0090: Invalid comparison between I4 and F4
		//IL_00a2: Expected F4, but got I4
		if (!invokeOnStart)
		{
			return;
		}
		bool flag = startupAction == StartupAction.None;
		if (!flag)
		{
			object obj = startupAction - 1;
			if (flag)
			{
				if (TryBegin())
				{
					bool flag2 = !(0f < delaySeconds);
					float delay = 0f;
					if (!flag2)
					{
						delay = delaySeconds;
					}
					IEnumerator routine = CoStartOperation(delay);
					Coroutine coroutine = StartCoroutine(routine);
				}
				return;
			}
			if ((nint)obj == 1)
			{
				if (TryBegin())
				{
					bool flag3 = !(0f < delaySeconds);
					float delay2 = 0f;
					if (!flag3)
					{
						delay2 = delaySeconds;
					}
					IEnumerator routine2 = CoReturnToMainMenu(delay2);
					Coroutine coroutine2 = StartCoroutine(routine2);
				}
				return;
			}
		}
		if (verbose)
		{
			string text = base.name;
			string message = "[OperationLoadRelay] invokeOnStart is true but StartupAction=None on '" + text + "'. No action taken.";
			Debug.Log(message, this);
		}
	}

	public void StartAssignedOperation()
	{
		if (TryBegin())
		{
			IEnumerator routine = CoStartOperation(0f);
			Coroutine coroutine = StartCoroutine(routine);
		}
	}

	public void StartAssignedOperationWithDelay(float delay)
	{
		//IL_002c: Invalid comparison between I4 and F4
		//IL_003e: Expected F4, but got I4
		if (TryBegin())
		{
			bool flag = !(0f < delay);
			float delay2 = 0f;
			if (!flag)
			{
				delay2 = delay;
			}
			IEnumerator routine = CoStartOperation(delay2);
			Coroutine coroutine = StartCoroutine(routine);
		}
	}

	public void ReturnToMainMenu()
	{
		if (TryBegin())
		{
			IEnumerator routine = CoReturnToMainMenu(0f);
			Coroutine coroutine = StartCoroutine(routine);
		}
	}

	public void ReturnToMainMenuWithDelay(float delay)
	{
		//IL_002c: Invalid comparison between I4 and F4
		//IL_003e: Expected F4, but got I4
		if (TryBegin())
		{
			bool flag = !(0f < delay);
			float delay2 = 0f;
			if (!flag)
			{
				delay2 = delay;
			}
			IEnumerator routine = CoReturnToMainMenu(delay2);
			Coroutine coroutine = StartCoroutine(routine);
		}
	}

	private IEnumerator CoStartOperation(float delay)
	{
		_003CCoStartOperation_003Ed__18 obj = new _003CCoStartOperation_003Ed__18(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.delay = delay;
		return obj;
	}

	private IEnumerator CoReturnToMainMenu(float delay)
	{
		_003CCoReturnToMainMenu_003Ed__19 obj = new _003CCoReturnToMainMenu_003Ed__19(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.delay = delay;
		return obj;
	}

	private bool TryBegin()
	{
		if (preventReentry)
		{
			if (_busy)
			{
				if (verbose)
				{
					string text = base.name;
					string message = "[OperationLoadRelay] Action already in progress on '" + text + "'. Reentry prevented.";
					Debug.Log(message, this);
				}
				return false;
			}
			_busy = true;
		}
		return true;
	}

	private void Succeed()
	{
		if (disableAfterSuccess)
		{
			if (verbose)
			{
				string text = base.name;
				string message = "[OperationLoadRelay] Success. Disabling component on '" + text + "' to prevent re-use.";
				Debug.Log(message, this);
			}
			base.enabled = false;
		}
		_busy = false;
	}

	private void End()
	{
		_busy = false;
	}
}
