using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EspressoCupDrinkHandler : MonoBehaviour
{
	private InputActionReference drinkAction;

	private bool enableActionOnEnable = true;

	private DynamicCursorManager cursorManager;

	private HoverTooltip tooltip;

	private UnityEvent<GameObject> onDrinkTriggered;

	private UnityEvent<GameObject> onDrinkBlocked;

	private bool debugLogs;

	private DraggableItem _tooltipTarget;

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
			string message = "[EspressoCupDrinkHandler:" + text + "] DynamicCursorManager not found on this GameObject and none assigned in the Inspector. Drink will not function.";
			Debug.LogError(message, this);
		}
	}

	private void OnEnable()
	{
		if (drinkAction != null)
		{
			InputAction action = drinkAction.action;
			if (action != null)
			{
				if (enableActionOnEnable)
				{
					InputAction action2 = drinkAction.action;
					if (!action2.enabled)
					{
						InputAction action3 = drinkAction.action;
						action3.Enable();
					}
				}
				InputAction action4 = drinkAction.action;
				Action<InputAction.CallbackContext> value = OnDrinkPerformed;
				action4.performed += value;
				goto IL_013a;
			}
		}
		string text = base.name;
		string message = "[EspressoCupDrinkHandler:" + text + "] 'Drink Action' is not assigned. Drink input will never fire.";
		Debug.LogWarning(message, this);
		goto IL_013a;
		IL_013a:
		if (cursorManager != null)
		{
			Action<Interactable> value2 = OnCursorTargetChanged;
			cursorManager.OnCursorTargetChanged += value2;
		}
	}

	private void OnDisable()
	{
		if (drinkAction != null)
		{
			InputAction action = drinkAction.action;
			if (action != null)
			{
				InputAction action2 = drinkAction.action;
				Action<InputAction.CallbackContext> value = OnDrinkPerformed;
				action2.performed -= value;
			}
		}
		if (cursorManager != null)
		{
			Action<Interactable> value2 = OnCursorTargetChanged;
			cursorManager.OnCursorTargetChanged -= value2;
		}
		_tooltipTarget = null;
		if ((object)tooltip != null)
		{
			tooltip.Hide();
		}
	}

	private void OnCursorTargetChanged(Interactable hovered)
	{
		if (hovered != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DraggableItem draggableItem = default(DraggableItem);
			bool flag = (object)draggableItem != null;
			DraggableItem draggableItem2 = draggableItem;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				DraggableItem draggableItem3 = default(DraggableItem);
				draggableItem2 = draggableItem3;
			}
			if (!IsValidDrinkCandidate(draggableItem2))
			{
				_tooltipTarget = null;
				if ((object)tooltip != null)
				{
					tooltip.Hide();
				}
				return;
			}
			_tooltipTarget = draggableItem2;
			if ((object)tooltip != null)
			{
				Transform worldAnchor = draggableItem2.transform;
				tooltip.Show(worldAnchor);
			}
		}
		else
		{
			_tooltipTarget = null;
			if ((object)tooltip != null)
			{
				tooltip.Hide();
			}
		}
	}

	private void Update()
	{
		if (_tooltipTarget != null && !IsValidDrinkCandidate(_tooltipTarget))
		{
			_tooltipTarget = null;
			if ((object)tooltip != null)
			{
				tooltip.Hide();
			}
		}
	}

	private void ShowTooltip(DraggableItem item)
	{
		_tooltipTarget = item;
		if ((object)tooltip != null)
		{
			Transform worldAnchor = item.transform;
			tooltip.Show(worldAnchor);
		}
	}

	private void HideTooltip()
	{
		_tooltipTarget = null;
		if ((object)tooltip != null)
		{
			tooltip.Hide();
		}
	}

	private bool IsValidDrinkCandidate(DraggableItem item)
	{
		//IL_01c4: Expected I4, but got O
		if ((bool)item && cursorManager != null)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			if ((object)cursorManager == null)
			{
				goto IL_01b6;
			}
			if (!dynamicCursorManager._suppressedByLockBroker)
			{
				if ((object)item == null)
				{
					goto IL_01b6;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				UnityEngine.Object obj = default(UnityEngine.Object);
				if ((bool)obj)
				{
					if ((object)obj == null)
					{
						goto IL_01b6;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ stack_10_v3 (UnityEngine.Object)+20]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						if ((bool)obj)
						{
							if ((object)obj == null)
							{
								goto IL_01b6;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ stack_10_v3 (UnityEngine.Object)+E0]");
							if ((nint)0 == 0)
							{
								return true;
							}
						}
					}
				}
			}
		}
		return false;
		IL_01b6:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void OnDrinkPerformed(InputAction.CallbackContext ctx)
	{
		TryDrink();
	}

	public void TryDrink()
	{
		UnityEngine.Object obj2;
		UnityEvent<GameObject> unityEvent;
		if ((bool)cursorManager)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			if (!dynamicCursorManager._suppressedByLockBroker)
			{
				if ((bool)dynamicCursorManager._currentHover)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
					UnityEngine.Object obj = default(UnityEngine.Object);
					bool flag = (object)obj != null;
					obj2 = obj;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
						UnityEngine.Object obj3 = default(UnityEngine.Object);
						obj2 = obj3;
					}
					if ((bool)obj2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						UnityEngine.Object obj4 = default(UnityEngine.Object);
						string text2;
						string text3;
						if ((bool)obj4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ stack_20_v3 (UnityEngine.Object)+20]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
								if ((bool)obj4)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ stack_20_v3 (UnityEngine.Object)+E0]");
									if ((nint)0 == 0)
									{
										string text = obj2.name;
										string message = "Success — triggering drink on '" + text + "'.";
										Log(message);
										HideTooltip();
										((EspressoCupDrinker)obj4).DrinkCoffee();
										unityEvent = onDrinkTriggered;
										goto IL_032b;
									}
									text2 = obj2.name;
									text3 = "' EspressoCupDrinker is already animating.";
								}
								else
								{
									text2 = obj2.name;
									text3 = "' has no EspressoCupDrinker component.";
								}
							}
							else
							{
								text2 = obj2.name;
								text3 = "' EspressoCup is empty.";
							}
						}
						else
						{
							text2 = obj2.name;
							text3 = "' has no EspressoCup component.";
						}
						string message2 = "Blocked — '" + text2 + text3;
						Log(message2);
						unityEvent = onDrinkBlocked;
						goto IL_032b;
					}
					string text4 = dynamicCursorManager._currentHover.name;
					string message3 = "Blocked — hovered Interactable '" + text4 + "' has no DraggableItem.";
					Log(message3);
					return;
				}
				Log("Blocked — no Interactable is currently hovered.");
				return;
			}
			Log("Blocked — DynamicCursorManager is suppressed.");
			if (onDrinkBlocked != null)
			{
				onDrinkBlocked.Invoke(null);
			}
			return;
		}
		Log("Blocked — cursorManager is null.");
		return;
		IL_032b:
		if (unityEvent != null)
		{
			GameObject arg = ((Component)obj2).gameObject;
			unityEvent.Invoke(arg);
		}
	}

	private void Log(string message)
	{
		if (debugLogs)
		{
			string text = base.name;
			string message2 = "[EspressoCupDrinkHandler:" + text + "] " + message;
			Debug.Log(message2, this);
		}
	}
}
