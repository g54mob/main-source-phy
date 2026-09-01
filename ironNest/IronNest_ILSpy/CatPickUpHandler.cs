using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CatPickUpHandler : MonoBehaviour
{
	private InputActionReference pickUpAction;

	private InputActionReference shooAction;

	private InputActionReference dropAction;

	private bool enableActionOnEnable = true;

	private DynamicCursorManager cursorManager;

	private string catParentName;

	private CatCustomizationController catCustomization;

	private HoverTooltip pickUpTooltip;

	private HoverTooltip shooTooltip;

	private HoverTooltip dropTooltip;

	private bool animate = true;

	private float slideDuration = 0.28f;

	private Vector3 position;

	private Vector3 rotation;

	private UnityEvent<GameObject> onCatPickedUp;

	private UnityEvent<GameObject> onCatDropped;

	private UnityEvent<GameObject> onCatShoo;

	private bool debugLogs;

	private CatController heldCat;

	private void Awake()
	{
		if (!cursorManager)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DynamicCursorManager dynamicCursorManager = default(DynamicCursorManager);
			cursorManager = dynamicCursorManager;
		}
		if (!cursorManager)
		{
			string text = base.name;
			string message = "[CatPickUpHandler:" + text + "] DynamicCursorManager not found on this GameObject and none assigned in the Inspector. Pick-up will not function.";
			Debug.LogError(message, this);
		}
	}

	private void OnEnable()
	{
		if (pickUpAction != null)
		{
			InputAction action = pickUpAction.action;
			if (action != null)
			{
				if (enableActionOnEnable)
				{
					InputAction action2 = pickUpAction.action;
					if (!action2.enabled)
					{
						InputAction action3 = pickUpAction.action;
						action3.Enable();
					}
				}
				InputAction action4 = pickUpAction.action;
				Action<InputAction.CallbackContext> value = OnPickUpPerformed;
				action4.performed += value;
				goto IL_013a;
			}
		}
		string text = base.name;
		string message = "[CatPickUpHandler:" + text + "] 'Pick Up Action' is not assigned. Pick-up input will never fire.";
		Debug.LogWarning(message, this);
		goto IL_013a;
		IL_013a:
		if (dropAction != null)
		{
			InputAction action5 = dropAction.action;
			if (action5 != null)
			{
				if (enableActionOnEnable)
				{
					InputAction action6 = dropAction.action;
					if (!action6.enabled)
					{
						InputAction action7 = dropAction.action;
						action7.Enable();
					}
				}
				InputAction action8 = dropAction.action;
				Action<InputAction.CallbackContext> value2 = OnDropPerformed;
				action8.performed += value2;
				goto IL_026f;
			}
		}
		string text2 = base.name;
		string message2 = "[CatPickUpHandler:" + text2 + "] 'Drop Action' is not assigned. Drop input will never fire.";
		Debug.LogWarning(message2, this);
		goto IL_026f;
		IL_026f:
		if (shooAction != null)
		{
			InputAction action9 = shooAction.action;
			if (action9 != null)
			{
				if (enableActionOnEnable)
				{
					InputAction action10 = shooAction.action;
					if (!action10.enabled)
					{
						InputAction action11 = shooAction.action;
						action11.Enable();
					}
				}
				InputAction action12 = shooAction.action;
				Action<InputAction.CallbackContext> value3 = OnShooPerformed;
				action12.performed += value3;
				goto IL_03a4;
			}
		}
		string text3 = base.name;
		string message3 = "[CatPickUpHandler:" + text3 + "] 'Shoo Action' is not assigned. Shoo input will never fire.";
		Debug.LogWarning(message3, this);
		goto IL_03a4;
		IL_03a4:
		if (cursorManager != null)
		{
			Action<Interactable> value4 = OnCursorTargetChanged;
			cursorManager.OnCursorTargetChanged += value4;
		}
	}

	private void OnDisable()
	{
		if (pickUpAction != null)
		{
			InputAction action = pickUpAction.action;
			if (action != null)
			{
				InputAction action2 = pickUpAction.action;
				Action<InputAction.CallbackContext> value = OnPickUpPerformed;
				action2.performed -= value;
			}
		}
		if (dropAction != null)
		{
			InputAction action3 = dropAction.action;
			if (action3 != null)
			{
				InputAction action4 = dropAction.action;
				Action<InputAction.CallbackContext> value2 = OnDropPerformed;
				action4.performed -= value2;
			}
		}
		if (shooAction != null)
		{
			InputAction action5 = shooAction.action;
			if (action5 != null)
			{
				InputAction action6 = shooAction.action;
				Action<InputAction.CallbackContext> value3 = OnShooPerformed;
				action6.performed -= value3;
			}
		}
		if (cursorManager != null)
		{
			Action<Interactable> value4 = OnCursorTargetChanged;
			cursorManager.OnCursorTargetChanged -= value4;
		}
		if ((object)pickUpTooltip != null)
		{
			pickUpTooltip.Hide();
		}
		if ((object)dropTooltip != null)
		{
			dropTooltip.Hide();
		}
		if ((object)shooTooltip != null)
		{
			shooTooltip.Hide();
		}
	}

	private void OnCursorTargetChanged(Interactable hovered)
	{
		if (hovered != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			bool flag = (object)obj != null;
			UnityEngine.Object obj2 = obj;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				UnityEngine.Object obj3 = default(UnityEngine.Object);
				obj2 = obj3;
			}
			bool flag2 = obj2 == null;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rdi_v4 (UnityEngine.Object)+AA]");
				if ((nint)0 == (flag2 ? 1 : 0))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rdi_v4 (UnityEngine.Object)+80]");
					HoverTooltip hoverTooltip;
					if ((nint)0 != 4)
					{
						if ((object)dropTooltip != null)
						{
							dropTooltip.Hide();
						}
						if ((object)pickUpTooltip != null)
						{
							CatCustomizationController catCustomizationController = catCustomization;
							pickUpTooltip.Show(catCustomizationController.catName);
						}
						hoverTooltip = shooTooltip;
					}
					else
					{
						if ((object)pickUpTooltip != null)
						{
							pickUpTooltip.Hide();
						}
						if ((object)shooTooltip != null)
						{
							shooTooltip.Hide();
						}
						hoverTooltip = dropTooltip;
					}
					if ((object)hoverTooltip != null)
					{
						CatCustomizationController catCustomizationController2 = catCustomization;
						hoverTooltip.Show(catCustomizationController2.catName);
					}
					return;
				}
			}
			if ((object)pickUpTooltip != null)
			{
				pickUpTooltip.Hide();
			}
			if ((object)dropTooltip != null)
			{
				dropTooltip.Hide();
			}
			if ((object)shooTooltip != null)
			{
				shooTooltip.Hide();
			}
		}
		else
		{
			if ((object)pickUpTooltip != null)
			{
				pickUpTooltip.Hide();
			}
			if ((object)dropTooltip != null)
			{
				dropTooltip.Hide();
			}
			if ((object)shooTooltip != null)
			{
				shooTooltip.Hide();
			}
		}
	}

	private void Update()
	{
		if (heldCat != null)
		{
			if ((object)pickUpTooltip != null)
			{
				pickUpTooltip.Hide();
			}
			if ((object)shooTooltip != null)
			{
				shooTooltip.Hide();
			}
			if ((object)dropTooltip != null)
			{
				CatCustomizationController catCustomizationController = catCustomization;
				dropTooltip.Show(catCustomizationController.catName);
			}
		}
	}

	private void ShowPickUpTooltip(CatController cat)
	{
		if ((object)pickUpTooltip != null)
		{
			CatCustomizationController catCustomizationController = catCustomization;
			pickUpTooltip.Show(catCustomizationController.catName);
		}
		if ((object)shooTooltip != null)
		{
			CatCustomizationController catCustomizationController2 = catCustomization;
			shooTooltip.Show(catCustomizationController2.catName);
		}
	}

	private void ShowDropTooltip(CatController cat)
	{
		if ((object)dropTooltip != null)
		{
			CatCustomizationController catCustomizationController = catCustomization;
			dropTooltip.Show(catCustomizationController.catName);
		}
	}

	private void HidePickUpTooltip()
	{
		if ((object)pickUpTooltip != null)
		{
			pickUpTooltip.Hide();
		}
		if ((object)shooTooltip != null)
		{
			shooTooltip.Hide();
		}
	}

	private void HideDropTooltip()
	{
		if ((object)dropTooltip != null)
		{
			dropTooltip.Hide();
		}
	}

	private void HideTooltips()
	{
		if ((object)pickUpTooltip != null)
		{
			pickUpTooltip.Hide();
		}
		if ((object)dropTooltip != null)
		{
			dropTooltip.Hide();
		}
		if ((object)shooTooltip != null)
		{
			shooTooltip.Hide();
		}
	}

	private void OnPickUpPerformed(InputAction.CallbackContext ctx)
	{
		TryPickUp();
	}

	private void OnDropPerformed(InputAction.CallbackContext ctx)
	{
		if (heldCat != null)
		{
			heldCat = null;
			heldCat.StopCarrying();
			string text = heldCat.name;
			string message = "Success — dropped '" + text + "'.";
			Log(message);
			if (onCatDropped != null)
			{
				GameObject arg = heldCat.gameObject;
				onCatDropped.Invoke(arg);
			}
		}
	}

	private void OnShooPerformed(InputAction.CallbackContext ctx)
	{
		DynamicCursorManager dynamicCursorManager = cursorManager;
		if ((bool)dynamicCursorManager._currentHover)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			bool flag = (object)obj != null;
			UnityEngine.Object obj2 = obj;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				UnityEngine.Object obj3 = default(UnityEngine.Object);
				obj2 = obj3;
			}
			bool flag2 = obj2 == null;
			if (flag2)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rbx_v4 (UnityEngine.Object)+AA]");
			if ((nint)0 == (flag2 ? 1 : 0))
			{
				((CatController)obj2).ShooCat(true);
				string text = obj2.name;
				string message = "Success — shoo '" + text + "'.";
				Log(message);
				if (onCatShoo != null)
				{
					GameObject arg = ((Component)obj2).gameObject;
					onCatShoo.Invoke(arg);
				}
			}
		}
		else
		{
			Log("Blocked — no Interactable is currently hovered.");
		}
	}

	public void TryPickUp()
	{
		if ((bool)cursorManager)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			string message;
			if (!dynamicCursorManager._suppressedByLockBroker)
			{
				if ((bool)dynamicCursorManager._currentHover)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
					UnityEngine.Object obj = default(UnityEngine.Object);
					bool flag = (object)obj != null;
					UnityEngine.Object obj2 = obj;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
						UnityEngine.Object obj3 = default(UnityEngine.Object);
						obj2 = obj3;
					}
					if ((bool)obj2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rdi_v8 (UnityEngine.Object)+AA]");
						if ((nint)0 == 0)
						{
							ExecutePickUp((CatController)obj2);
						}
						return;
					}
					string text = dynamicCursorManager._currentHover.name;
					string text2 = "Blocked — hovered Interactable '" + text + "' has no CatController.";
					message = text2;
				}
				else
				{
					message = "Blocked — no Interactable is currently hovered.";
				}
			}
			else
			{
				message = "Blocked — DynamicCursorManager is suppressed.";
			}
			Log(message);
		}
		else
		{
			Log("Blocked — cursorManager is null.");
		}
	}

	private unsafe void ExecutePickUp(CatController cat)
	{
		//IL_00b4: Expected O, but got Ref
		//IL_00d3: Expected O, but got Ref
		heldCat = cat;
		heldCat.StartCarrying();
		FirstPersonController firstPersonController = UnityEngine.Object.FindFirstObjectByType<FirstPersonController>();
		Transform transform = firstPersonController.transform;
		Transform parent = transform.Find("CatParent");
		Transform transform2 = cat.transform;
		transform2.SetParent(parent, worldPositionStays: true);
		Transform transform3 = cat.transform;
		Vector3 localScale = transform3.localScale;
		Transform transform4 = cat.transform;
		Vector3 vector = default(Vector3);
		transform4.localPosition = (Vector3)(&vector);
		Transform transform5 = cat.transform;
		transform5.localEulerAngles = (Vector3)(&vector);
		string text = cat.name;
		string message = "Success — picking up '" + text + "'.";
		Log(message);
		if (onCatPickedUp != null)
		{
			GameObject arg = cat.gameObject;
			onCatPickedUp.Invoke(arg);
		}
	}

	private void ExecuteDrop(CatController cat)
	{
		if (cat != null)
		{
			heldCat = null;
			cat.StopCarrying();
			string text = cat.name;
			string message = "Success — dropped '" + text + "'.";
			Log(message);
			if (onCatDropped != null)
			{
				GameObject arg = cat.gameObject;
				onCatDropped.Invoke(arg);
			}
		}
	}

	private void ExecuteShoo()
	{
		DynamicCursorManager dynamicCursorManager = cursorManager;
		if ((bool)dynamicCursorManager._currentHover)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			bool flag = (object)obj != null;
			UnityEngine.Object obj2 = obj;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				UnityEngine.Object obj3 = default(UnityEngine.Object);
				obj2 = obj3;
			}
			bool flag2 = obj2 == null;
			if (flag2)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rbx_v4 (UnityEngine.Object)+AA]");
			if ((nint)0 == (flag2 ? 1 : 0))
			{
				((CatController)obj2).ShooCat(true);
				string text = obj2.name;
				string message = "Success — shoo '" + text + "'.";
				Log(message);
				if (onCatShoo != null)
				{
					GameObject arg = ((Component)obj2).gameObject;
					onCatShoo.Invoke(arg);
				}
			}
		}
		else
		{
			Log("Blocked — no Interactable is currently hovered.");
		}
	}

	public void ExecuteExternalShoo()
	{
		CatController catController = UnityEngine.Object.FindAnyObjectByType<CatController>();
		bool flag = catController == null;
		if (!flag && catController.RecoveryState == flag)
		{
			catController.ShooCat(initiatedByPlayer: false);
			string text = catController.name;
			string message = "Success — shoo '" + text + "'.";
			Log(message);
			if (onCatShoo != null)
			{
				GameObject arg = catController.gameObject;
				onCatShoo.Invoke(arg);
			}
		}
	}

	public void InterruptCat()
	{
		CatController catController = UnityEngine.Object.FindAnyObjectByType<CatController>();
		bool flag = catController == null;
		if (flag || catController.RecoveryState != flag)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A898]");
		if ((nint)0 == (flag ? 1 : 0))
		{
			_ = 1;
		}
		if (catController._currentState != CatState.Carried)
		{
			AgentAnimation agentAnimation = catController._agentAnimation;
			if (!string.IsNullOrEmpty("Carrying"))
			{
				agentAnimation._animator.SetBool("Carrying", value: false);
			}
			AgentAnimation agentAnimation2 = catController._agentAnimation;
			if (!string.IsNullOrEmpty("Idle"))
			{
				agentAnimation2._animator.SetBool("Idle", value: false);
			}
			AgentAnimation agentAnimation3 = catController._agentAnimation;
			if (!string.IsNullOrEmpty("PetIdle"))
			{
				agentAnimation3._animator.SetTrigger("PetIdle");
			}
			catController._currentState = CatState.Idle;
			catController.RecoveryState = false;
		}
		string text = catController.name;
		string message = "Success — interrupt '" + text + "'.";
		Log(message);
	}

	private void Log(string message)
	{
		if (debugLogs)
		{
			string text = base.name;
			string message2 = "[ClipboardPickUpHandler:" + text + "] " + message;
			Debug.Log(message2, this);
		}
	}
}
