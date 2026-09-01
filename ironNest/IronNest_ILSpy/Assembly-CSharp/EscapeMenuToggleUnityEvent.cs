using System;
using System.Collections.Generic;
using System.Text;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public sealed class EscapeMenuToggleUnityEvent : MonoBehaviour
{
	private InputActionReference toggleAction;

	private bool initialOpenState;

	private bool isOpen;

	private bool robustLogs;

	private UnityEvent onKeyPressed;

	private UnityEvent onOpened;

	private UnityEvent onClosed;

	private UnityEvent onOpenBlocked;

	private readonly HashSet<EscapeMenuOpenBlocker> activeBlockers;

	private InputAction subscribedAction;

	public IReadOnlyCollection<EscapeMenuOpenBlocker> ActiveBlockers => activeBlockers;

	public UnityEvent OnKeyPressedEvent => onKeyPressed;

	public UnityEvent OnOpenedEvent => onOpened;

	public UnityEvent OnClosedEvent => onClosed;

	public UnityEvent OnOpenBlockedEvent => onOpenBlocked;

	public bool IsOpen => isOpen;

	public bool IsBlocked
	{
		get
		{
			//IL_009e: Expected I4, but got O
			HashSet<EscapeMenuOpenBlocker> hashSet = activeBlockers;
			if (activeBlockers != null)
			{
				int num = hashSet._count ^ hashSet._count;
				int num2 = hashSet._count & num;
				bool flag = num2 < 0;
				bool flag2 = hashSet._count < 0;
				bool flag3 = hashSet._count == 0;
				bool flag4 = flag2 == flag;
				bool flag5 = !flag3;
				return flag5 & flag4;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private void OnEnable()
	{
		isOpen = initialOpenState;
		Subscribe();
	}

	private void OnDisable()
	{
		Unsubscribe();
	}

	private void Subscribe()
	{
		Unsubscribe();
		if (toggleAction != null)
		{
			InputAction action = toggleAction.action;
			if (action != null)
			{
				InputAction action2 = toggleAction.action;
				subscribedAction = action2;
				Action<InputAction.CallbackContext> value = HandleActionStarted;
				subscribedAction.started += value;
			}
		}
	}

	private void Unsubscribe()
	{
		if (subscribedAction != null)
		{
			Action<InputAction.CallbackContext> value = HandleActionStarted;
			subscribedAction.started -= value;
		}
		subscribedAction = null;
	}

	private void HandleActionStarted(InputAction.CallbackContext ctx)
	{
		onKeyPressed.Invoke();
		UnityEvent unityEvent;
		if (!isOpen)
		{
			HashSet<EscapeMenuOpenBlocker> hashSet = activeBlockers;
			if (hashSet._count <= 0)
			{
				unityEvent = onOpened;
				isOpen = true;
			}
			else
			{
				if (robustLogs)
				{
					LogBlockers();
				}
				unityEvent = onOpenBlocked;
			}
		}
		else
		{
			unityEvent = onClosed;
			isOpen = false;
		}
		unityEvent.Invoke();
	}

	private void LogBlockers()
	{
		//IL_0112: Expected O, but got I
		StringBuilder stringBuilder = new StringBuilder();
		if (stringBuilder != null)
		{
			StringBuilder stringBuilder2 = stringBuilder.Append("[EscapeMenuToggleUnityEvent] Escape menu open BLOCKED by ");
			HashSet<EscapeMenuOpenBlocker> hashSet = activeBlockers;
			if (activeBlockers != null)
			{
				StringBuilder stringBuilder3 = stringBuilder.Append(hashSet._count);
				StringBuilder stringBuilder4 = stringBuilder.Append(" blocker(s):");
				if (activeBlockers != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18087B860");
					HashSet<EscapeMenuOpenBlocker>.Enumerator enumerator = default(HashSet<EscapeMenuOpenBlocker>.Enumerator);
					UnityEngine.Object obj = default(UnityEngine.Object);
					while (true)
					{
						if (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							StringBuilder stringBuilder5 = stringBuilder.Append("\n  • ");
							if (obj != null)
							{
								if ((object)obj == null)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ stack_18_v4 (UnityEngine.Object)+20]");
								StringBuilder stringBuilder6 = stringBuilder.Append((string)0);
								StringBuilder stringBuilder7 = stringBuilder.Append(" (GameObject: ");
								GameObject gameObject = ((Component)obj).gameObject;
								if ((object)gameObject == null)
								{
									break;
								}
								string value = gameObject.name;
								StringBuilder stringBuilder8 = stringBuilder.Append(value);
								StringBuilder stringBuilder9 = stringBuilder.Append(")");
							}
							else
							{
								StringBuilder stringBuilder10 = stringBuilder.Append("<null blocker — will be cleaned up>");
							}
							continue;
						}
						enumerator.Dispose();
						string message = stringBuilder.ToString();
						Debug.Log(message, this);
						return;
					}
					throw new NullReferenceException();
				}
			}
		}
		throw new NullReferenceException();
	}

	public void RegisterBlocker(EscapeMenuOpenBlocker blocker)
	{
		if (blocker != null)
		{
			activeBlockers.Add(blocker);
		}
	}

	public void UnregisterBlocker(EscapeMenuOpenBlocker blocker)
	{
		if (blocker != null)
		{
			bool flag = activeBlockers.Remove(blocker);
		}
	}

	public void SetToggleAction(InputActionReference actionReference)
	{
		if (toggleAction != actionReference)
		{
			toggleAction = actionReference;
			if (base.isActiveAndEnabled)
			{
				Subscribe();
			}
		}
	}

	public void SetOpenState(bool open, bool invokeEvent = false)
	{
		if (isOpen != open)
		{
			isOpen = open;
			if (!invokeEvent)
			{
				return;
			}
			if (!open)
			{
				goto IL_004f;
			}
		}
		else
		{
			if (!invokeEvent)
			{
				return;
			}
			if (!isOpen)
			{
				goto IL_004f;
			}
		}
		UnityEvent unityEvent = onOpened;
		goto IL_005e;
		IL_004f:
		unityEvent = onClosed;
		goto IL_005e;
		IL_005e:
		unityEvent.Invoke();
	}

	public void ForceOpen(bool invokeEvent = true)
	{
		UnityEvent unityEvent;
		if (!isOpen)
		{
			isOpen = true;
			if (!invokeEvent)
			{
				return;
			}
		}
		else
		{
			if (!invokeEvent)
			{
				return;
			}
			if (!isOpen)
			{
				unityEvent = onClosed;
				goto IL_0052;
			}
		}
		unityEvent = onOpened;
		goto IL_0052;
		IL_0052:
		unityEvent.Invoke();
	}

	public void ForceClose(bool invokeEvent = true)
	{
		UnityEvent unityEvent;
		if (isOpen)
		{
			isOpen = false;
			if (!invokeEvent)
			{
				return;
			}
		}
		else
		{
			if (!invokeEvent)
			{
				return;
			}
			if (isOpen)
			{
				unityEvent = onOpened;
				goto IL_0052;
			}
		}
		unityEvent = onClosed;
		goto IL_0052;
		IL_0052:
		unityEvent.Invoke();
	}

	public void RefreshSubscriptions()
	{
		if (base.isActiveAndEnabled)
		{
			Subscribe();
		}
	}

	public EscapeMenuToggleUnityEvent()
	{
		UnityEvent unityEvent = new UnityEvent();
		onKeyPressed = unityEvent;
		onOpened = new UnityEvent();
		onClosed = new UnityEvent();
		onOpenBlocked = new UnityEvent();
		activeBlockers = new HashSet<EscapeMenuOpenBlocker>();
		base._002Ector();
	}
}
