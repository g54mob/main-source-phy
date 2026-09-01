using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[AddComponentMenu("Espresso/Espresso Cup Drink Handler")]
public class EspressoCupDrinkHandler : MonoBehaviour
{
	[Header("Input (Action)")]
	[Tooltip("Input Action that triggers a drink attempt.\n\nAction type : Button\nPhase used  : performed\n\nNotes:\n- Fully separate from the pick-up action — can share a key or use a\n  different one entirely.\n- If 'Enable Action On Enable' is true, this component enables the action\n  on its own OnEnable. Set false if PlayerInput already manages it.")]
	[SerializeField]
	private InputActionReference drinkAction;

	[Tooltip("If true, calls action.Enable() in OnEnable when the action is not already enabled.\nSet false if a PlayerInput component or other system owns the action lifecycle.\n\nSafe default: true.")]
	[SerializeField]
	private bool enableActionOnEnable;

	[Header("References")]
	[Tooltip("The DynamicCursorManager used to read CurrentHover and suppression state.\n\nIf left null, auto-fetched from this GameObject in Awake.\n\nRequired: drink and tooltip will not work without this.")]
	[SerializeField]
	private DynamicCursorManager cursorManager;

	[Header("Tooltip (Optional)")]
	[Tooltip("HoverTooltip component on your screen-space tooltip panel.\n\nWhen assigned:\n- Show() is called whenever the cursor hovers a full EspressoCup that\n  has an EspressoCupDrinker and is ready to drink.\n- Hide() is called when hover ends or the cup is no longer eligible\n  (e.g. it was emptied by another system, or starts animating).\n\nLeave null to disable tooltip behaviour entirely.\n\nThis is a separate tooltip instance from the one used by\nClipboardPickUpHandler, so they can show different panel designs.")]
	[SerializeField]
	private HoverTooltip tooltip;

	[Header("Events")]
	[Tooltip("Fired when a drink is successfully triggered (DrinkCoffee() was called).\nNote: DrinkCoffee() may still silently reject the call internally if the cup\nbegins animating between our validity check and the call — in that case the\ncup's own OnDrinkFailed event fires. For tight coupling, poll IsAnimating\nyourself after this event.")]
	[SerializeField]
	private UnityEvent<GameObject> onDrinkTriggered;

	[Tooltip("Fired when a drink attempt is blocked by any guard condition checked here.\nThe DraggableItem's GameObject is passed if one was hovered; otherwise null.\nUse to drive 'cannot drink' feedback (sound, flash, etc.).")]
	[SerializeField]
	private UnityEvent<GameObject> onDrinkBlocked;

	[Header("Debug")]
	[Tooltip("If true, logs all drink attempts — success and blocked — with the reason.\n\nSafe default: false.")]
	[SerializeField]
	private bool debugLogs;

	private DraggableItem _tooltipTarget;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnCursorTargetChanged(Interactable hovered)
	{
	}

	private void Update()
	{
	}

	private void ShowTooltip(DraggableItem item)
	{
	}

	private void HideTooltip()
	{
	}

	private bool IsValidDrinkCandidate(DraggableItem item)
	{
		return false;
	}

	private void OnDrinkPerformed(InputAction.CallbackContext ctx)
	{
	}

	public void TryDrink()
	{
	}

	private void Log(string message)
	{
	}
}
