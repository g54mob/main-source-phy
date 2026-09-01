using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[AddComponentMenu("Espresso/Item Slot Drink Trigger")]
public class ItemSlotDrinkTrigger : MonoBehaviour
{
	[Header("Slot Reference")]
	[Tooltip("The ItemSlot whose CurrentItem will be targeted when TriggerDrink() is called.\n\nIf left unassigned, this component will attempt to find an ItemSlot on the\nsame GameObject during Awake.\n\nSafe default: unassigned (auto-fetched from this GameObject).")]
	[SerializeField]
	private ItemSlot targetSlot;

	[Header("Events")]
	[Tooltip("Fired when TriggerDrink() successfully begins a drink animation.\n\nFired after DrinkCoffee() is called on the EspressoCupDrinker.\nThe cup's own OnDrinkStarted event will also fire as normal.")]
	public UnityEvent OnDrinkTriggered;

	[Tooltip("Fired when TriggerDrink() is called but cannot start a drink.\n\nFailure reasons:\n  - The slot has no item (CurrentItem is null).\n  - The item has no EspressoCupDrinker component.\n  - The cup is empty (IsFull == false).\n  - The cup is already mid-animation.\n\nThe cup's own OnDrinkFailed event may also fire (from EspressoCupDrinker).")]
	public UnityEvent OnDrinkFailed;

	[Header("Debug")]
	[Tooltip("If true, logs the result of each TriggerDrink() call to the Console,\nincluding the reason for any failure.\n\nSafe default: false.")]
	[SerializeField]
	private bool debugLog;

	private void Awake()
	{
	}

	public void TriggerDrink()
	{
	}
}
