using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TutorialCutaways;

public class TutorialCutawayForceEndInput : MonoBehaviour
{
	public enum ForceEndMode
	{
		Interrupt,
		Complete
	}

	public TutorialCutawayService serviceReference;

	public string serviceTag;

	public InputActionReference forceEndAction;

	public ForceEndMode endMode;

	public bool autoManageActionLifecycle;

	public bool debugLogging;

	public bool ignoreWithoutActive;

	private TutorialCutawayService _cachedService;

	private void Awake()
	{
		//IL_003c: Expected O, but got I
		//IL_004c: Expected O, but got I
		string text = serviceTag;
		if (serviceTag == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rax_v4+B8]");
			object obj2 = 0;
			text = (string)obj2;
		}
		string text2 = text.Trim();
		serviceTag = text2;
	}

	private void OnEnable()
	{
		TutorialCutawayService tutorialCutawayService = ResolveServiceIfNeeded();
		if (forceEndAction != null)
		{
			InputAction action = forceEndAction.action;
			if (action != null)
			{
				if (autoManageActionLifecycle)
				{
					InputAction action2 = forceEndAction.action;
					if (!action2.enabled)
					{
						InputAction action3 = forceEndAction.action;
						action3.Enable();
					}
				}
				InputAction action4 = forceEndAction.action;
				Action<InputAction.CallbackContext> value = OnForceEndPerformed;
				action4.performed += value;
				return;
			}
		}
		if (debugLogging)
		{
			string text = base.name;
			string message = "[TutorialCutawayForceEndInput:" + text + "] forceEndAction not assigned.";
			Debug.LogWarning(message);
		}
	}

	private void OnDisable()
	{
		if (!(forceEndAction != null))
		{
			return;
		}
		InputAction action = forceEndAction.action;
		if (action == null)
		{
			return;
		}
		InputAction action2 = forceEndAction.action;
		Action<InputAction.CallbackContext> value = OnForceEndPerformed;
		action2.performed -= value;
		if (autoManageActionLifecycle)
		{
			InputAction action3 = forceEndAction.action;
			if (action3.enabled)
			{
				InputAction action4 = forceEndAction.action;
				action4.Disable();
			}
		}
	}

	private unsafe void OnForceEndPerformed(InputAction.CallbackContext ctx)
	{
		InputActionPhase phase = ((InputAction.CallbackContext*)ctx)->phase;
		if (phase != InputActionPhase.Performed)
		{
			return;
		}
		TutorialCutawayService tutorialCutawayService = ResolveServiceIfNeeded();
		string message;
		if (tutorialCutawayService != null)
		{
			string text3;
			string text4;
			if (tutorialCutawayService._active != null)
			{
				bool interrupt = endMode == ForceEndMode.Interrupt;
				bool flag = tutorialCutawayService.ForceEndActive(interrupt);
				if (!debugLogging)
				{
					return;
				}
				if (flag)
				{
					string[] array = new string[7] { "[TutorialCutawayForceEndInput:", null, null, null, null, null, null };
					string text = base.name;
					array[1] = text;
					array[2] = "] Forced ";
					bool flag2 = endMode != ForceEndMode.Interrupt;
					object obj = "completion";
					if (!flag2)
					{
						obj = "interrupt";
					}
					array[3] = (string)obj;
					array[4] = " of active cue '";
					string text2 = tutorialCutawayService._active.name;
					array[5] = text2;
					array[6] = "'.";
					message = string.Concat(array);
					goto IL_0290;
				}
				text3 = base.name;
				text4 = "] Force end attempted but service reported no active (race condition?).";
			}
			else
			{
				if (!debugLogging)
				{
					return;
				}
				if (!ignoreWithoutActive)
				{
					text3 = base.name;
					text4 = "] Input received but no active cutaway.";
				}
				else
				{
					if (!debugLogging || !ignoreWithoutActive)
					{
						return;
					}
					text3 = base.name;
					text4 = "] Input ignored (no active cutaway).";
				}
			}
			message = "[TutorialCutawayForceEndInput:" + text3 + text4;
			goto IL_0290;
		}
		if (debugLogging)
		{
			string text5 = base.name;
			string message2 = "[TutorialCutawayForceEndInput:" + text5 + "] Force end input received but service not found.";
			Debug.LogWarning(message2);
		}
		return;
		IL_0290:
		Debug.Log(message);
	}

	private TutorialCutawayService ResolveServiceIfNeeded()
	{
		TutorialCutawayService cachedService;
		if (serviceReference == null)
		{
			if (!TutorialCutawayService.HasInstance)
			{
				if (_cachedService == null)
				{
					if (!string.IsNullOrEmpty(serviceTag))
					{
						GameObject gameObject = GameObject.FindWithTag(serviceTag);
						if ((bool)gameObject)
						{
							if ((object)gameObject == null)
							{
								goto IL_026d;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							TutorialCutawayService tutorialCutawayService = default(TutorialCutawayService);
							bool flag = tutorialCutawayService != null;
							cachedService = tutorialCutawayService;
							if (flag)
							{
								goto IL_022d;
							}
							if (debugLogging != flag)
							{
								string[] array = new string[5];
								if (array == null)
								{
									goto IL_026d;
								}
								array[0] = "[TutorialCutawayForceEndInput:";
								string text = base.name;
								array[1] = text;
								array[2] = "] GameObject with tag '";
								array[3] = serviceTag;
								array[4] = "' lacks TutorialCutawayService.";
								string message = string.Concat(array);
								Debug.LogWarning(message);
							}
						}
					}
					TutorialCutawayService tutorialCutawayService2 = UnityEngine.Object.FindObjectOfType<TutorialCutawayService>(includeInactive: true);
					bool flag2 = tutorialCutawayService2 != null;
					cachedService = tutorialCutawayService2;
					if (!flag2)
					{
						return null;
					}
					goto IL_022d;
				}
			}
			else
			{
				_cachedService = TutorialCutawayService._003CInstance_003Ek__BackingField;
				serviceReference = _cachedService;
			}
		}
		else
		{
			_cachedService = serviceReference;
		}
		goto IL_025c;
		IL_026d:
		return (TutorialCutawayService)(object)new NullReferenceException();
		IL_025c:
		return _cachedService;
		IL_022d:
		_cachedService = cachedService;
		serviceReference = cachedService;
		goto IL_025c;
	}

	private void Context_ForceEndInterrupt()
	{
		TutorialCutawayService tutorialCutawayService = ResolveServiceIfNeeded();
		if (tutorialCutawayService == null)
		{
			string text = base.name;
			string message = "[TutorialCutawayForceEndInput:" + text + "] Service not found for context interrupt test.";
			Debug.LogWarning(message);
		}
		else
		{
			bool flag = tutorialCutawayService.ForceEndActive(interrupt: true);
		}
	}

	private void Context_ForceEndComplete()
	{
		TutorialCutawayService tutorialCutawayService = ResolveServiceIfNeeded();
		if (tutorialCutawayService == null)
		{
			string text = base.name;
			string message = "[TutorialCutawayForceEndInput:" + text + "] Service not found for context completion test.";
			Debug.LogWarning(message);
		}
		else
		{
			bool flag = tutorialCutawayService.ForceEndActive(interrupt: false);
		}
	}

	private void Context_LogServiceResolution()
	{
		TutorialCutawayService tutorialCutawayService = ResolveServiceIfNeeded();
		if (!(tutorialCutawayService != null))
		{
			string text = base.name;
			string message = "[TutorialCutawayForceEndInput:" + text + "] Service resolution failed.";
			Debug.LogWarning(message);
			return;
		}
		string text2 = base.name;
		string text3 = tutorialCutawayService.name;
		string message2 = "[TutorialCutawayForceEndInput:" + text2 + "] Resolved service: '" + text3 + "'";
		Debug.Log(message2);
	}

	public TutorialCutawayForceEndInput()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A804]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		serviceTag = "TutorialCutawayService";
		autoManageActionLifecycle = true;
		ignoreWithoutActive = true;
		base._002Ector();
	}
}
