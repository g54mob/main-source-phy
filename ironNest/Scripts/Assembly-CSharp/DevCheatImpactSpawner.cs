using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class DevCheatImpactSpawner : MonoBehaviour
{
	[Header("Cheat Enable")]
	[Tooltip("If false, this component ignores input and does not spawn impacts.\nUse this to keep the script in scenes/prefabs but disable the cheat for non-dev builds.")]
	[SerializeField]
	private bool cheatEnabled;

	[Header("Impact Prefab (Must Match Standard Pipeline)")]
	[Tooltip("Impact effect prefab to spawn.\nThis should be the SAME kind of prefab used by real shells (ShellBlueprint.impactEffectPrefab), so that:\n- ImpactLocation runs and reports through LocalSpaceEventLogger\n- ImpactIndicator and any other listeners receive events normally\n\nPrefab expectation (recommended):\n- Has a RectTransform (UI)\n- Has an ImpactLocation component\n- Optionally has an ImpactEffect component (payload for damage/pen/radius/knockback)\n")]
	[SerializeField]
	private GameObject impactEffectPrefab;

	[Header("Shell Emulation (Identity)")]
	[Tooltip("ShellDefinition to emulate for the spawned impact.\nThe script will assign this to ImpactLocation.shell on the spawned prefab.\nTo change which shell is emulated, swap this ScriptableObject reference in the inspector.")]
	[SerializeField]
	private ShellDefinition emulatedShellDefinition;

	[Header("Map / Coordinate Space")]
	[Tooltip("Canvas that contains your map UI (World Space or Screen Space).\nUsed only to choose the correct camera for screen->local conversions.\nMust be the same canvas family as your normal impact effects so ImpactLocation.ResolveRootCanvasRect() behaves consistently.")]
	[SerializeField]
	private Canvas mapCanvas;

	[Tooltip("RectTransform representing the interactive map area.\nThe cheat will only spawn impacts if the VirtualCursor is over this rect.\nThe cursor screen position is converted into a local point on this rect to determine the spawn position.")]
	[SerializeField]
	private RectTransform mapRect;

	[Tooltip("RectTransform parent under which standard shell impacts are normally instantiated.\nYou said impacts are spawned under a CHILD of the map — assign that child container here.\nThis is critical to match your standard pipeline coordinate space and hierarchy.\n\nExample (safe): MapCanvas/MapRoot/ImpactsContainer")]
	[SerializeField]
	private RectTransform standardImpactSpawnContainer;

	[Header("Unified Pointer (VirtualCursor)")]
	[Tooltip("VirtualCursor providing the unified screen-space pointer position.\nThis cheat uses VirtualCursor.ScreenPosition and does not use Mouse.current or other fallbacks.")]
	[SerializeField]
	private VirtualCursor virtualCursor;

	[Header("Input Actions")]
	[Tooltip("Input Action used to spawn a cheat impact.\nThis must be bound in your Input Actions asset (no hardcoded keybinds here).\nThe cheat triggers when this action 'WasPerformedThisFrame()'.")]
	[SerializeField]
	private InputActionReference spawnImpactAction;

	[Tooltip("If true, this script enables 'spawnImpactAction' in OnEnable().\nDisable if a PlayerInput or another system manages action enable/disable.")]
	[SerializeField]
	private bool enableActionOnEnable;

	[Header("ImpactEffect Payload (Optional)")]
	[Tooltip("If true, and if the spawned prefab has an ImpactEffect component, calls ImpactEffect.Initialize(...) using the values below.\nEnable this if any of your downstream systems rely on ImpactEffect fields for damage/penetration/radius/knockback.\n\nNote: This payload radius is NOT the same as ImpactLocation.radius (hit detection radius).")]
	[SerializeField]
	private bool initializeImpactEffectPayload;

	[Tooltip("Damage passed into ImpactEffect.Initialize(...) if initializeImpactEffectPayload is enabled.\nSafe example: 25")]
	[SerializeField]
	private int payloadDamage;

	[Tooltip("Armor penetration passed into ImpactEffect.Initialize(...) if initializeImpactEffectPayload is enabled.\nRange 0..1.\nSafe example: 0.1")]
	[Range(0f, 1f)]
	[SerializeField]
	private float payloadArmorPenetration;

	[Tooltip("Impact radius passed into ImpactEffect.Initialize(...) if initializeImpactEffectPayload is enabled.\nThis is NOT used by your location hit detection (ImpactLocation.radius is).\nSafe example: 2")]
	[SerializeField]
	private float payloadImpactRadius;

	[Tooltip("Knockback force passed into ImpactEffect.Initialize(...) if initializeImpactEffectPayload is enabled.\nSafe example: 50")]
	[SerializeField]
	private float payloadKnockbackForce;

	[Header("Diagnostics")]
	[Tooltip("If true, prints logs when spawning is blocked, when conversions succeed/fail, and when an impact is spawned.")]
	[SerializeField]
	private bool debugLogs;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	private void TrySpawnImpactAtCursor()
	{
	}

	private static Camera GetCameraForCanvas(Canvas canvas)
	{
		return null;
	}
}
