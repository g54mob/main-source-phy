using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(DraggableItem))]
[AddComponentMenu("Espresso/Coffee Grounds Can")]
public class CoffeeGroundsCan : MonoBehaviour
{
	[Header("Quality")]
	[Tooltip("Base quality of these coffee grounds, in the range 0..1.\n\nMultiplies the brewer's skill score to produce the final espresso quality.\n\nExamples:\n  0.5 — cheap instant grounds\n  0.8 — good fresh-ground beans\n  1.0 — premium single-origin\n\nSafe default: 0.8")]
	[Range(0f, 1f)]
	public float baseQuality;

	[Tooltip("Human-readable label for this can's coffee variety.\nPassed to the finished EspressoCup for display and downstream event use.\n\nExamples: 'House Blend', 'Dark Roast', 'Single Origin Ethiopia'\n\nSafe default: 'Coffee Grounds'")]
	public string coffeeLabel;

	[Header("Uses")]
	[Tooltip("Total number of brews this can supports before it is considered empty.\nEach completed brew consumes exactly one use.\n\nWhen remainingUses reaches 0 the can fires OnEmpty and is automatically\nejected from the grounds slot by EspressoBrewingController.\n\nSet to -1 for infinite uses (the can is never auto-ejected).\n\nSafe default: 5")]
	public int maxUses;

	[Tooltip("How many brews are left in this can. Decremented by EspressoBrewingController\neach time a brew completes. Read-only at runtime; set automatically from maxUses\non Awake.")]
	public int remainingUses;

	[Header("State — Runtime")]
	[Tooltip("True when this can is currently loaded into the espresso machine. Read-only.")]
	public bool IsLoaded;

	[Header("Events")]
	[Tooltip("Fired when EspressoBrewingController calls Load() on this can.")]
	public UnityEvent OnLoaded;

	[Tooltip("Fired when EspressoBrewingController calls Unload() on this can.")]
	public UnityEvent OnUnloaded;

	[Tooltip("Fired by ConsumeUse() each time a use is consumed.\nParameter: remainingUses after the decrement (0 = can is now empty).")]
	public UnityEvent<int> OnUseConsumed;

	[Tooltip("Fired by ConsumeUse() when remainingUses reaches 0.\nEspressoBrewingController listens to this to trigger auto-eject.\nNot fired when maxUses is -1 (infinite).")]
	public UnityEvent OnEmpty;

	public bool IsEmpty => false;

	public DraggableItem DraggableItem { get; private set; }

	private void Awake()
	{
	}

	public void Load()
	{
	}

	public void Unload()
	{
	}

	public bool ConsumeUse()
	{
		return false;
	}
}
