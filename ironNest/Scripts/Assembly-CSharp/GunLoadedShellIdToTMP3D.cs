using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class GunLoadedShellIdToTMP3D : MonoBehaviour
{
	[Header("Target References")]
	[Tooltip("GunController to read the chambered shell from.\nIf left NULL and 'Auto Find Gun On Validate' is enabled, the script will attempt to assign the GunController from the SAME GameObject.\nPrefab-friendly: does not search the scene.")]
	[SerializeField]
	private GunController gun;

	[Tooltip("TextMeshPro (3D) component to write the loaded shell ID into.\nImportant: This is the 3D TMP component type: TMPro.TextMeshPro.\nIf you are using a Canvas UI element, use a TextMeshProUGUI-specific script instead.")]
	[SerializeField]
	private TextMeshPro targetText;

	[Header("Read Rules")]
	[Tooltip("If true, the shell ID is shown ONLY when gun.CanFire is true.\nIf false, the shell ID is shown whenever a chambered shell exists, even if the gun is currently reloading.\nRecommended for strict 'ready-to-fire' readout: true.")]
	[SerializeField]
	private bool requireCanFire;

	[Tooltip("If true, when resolving the ShellBlueprint, the script will try GetComponentInChildren<ShellBlueprint>() as a fallback.\nEnable this only if your chambered shell prefab may place ShellBlueprint on a child object.\nIf false, only GetComponent<ShellBlueprint>() is used (fastest/strictest).")]
	[SerializeField]
	private bool allowBlueprintOnChildren;

	[Header("TMP Output")]
	[Tooltip("Text to display when the shell ID cannot be resolved.\nExamples: \"None\", \"---\", \"EMPTY\".\nApplied when:\n- gun is null\n- (requireCanFire == true) and gun.CanFire is false\n- chamberedShell is null/unassigned\n- ShellBlueprint is missing\n- ShellBlueprint.shellDefinition is missing\n- ShellDefinition.shellId is empty")]
	[SerializeField]
	private string fallbackTextForTMP;

	[Tooltip("If true, the TMP text will be updated every frame (useful while debugging).\nIf false, it updates only when the resolved text changes (less overhead).")]
	[SerializeField]
	private bool updateEveryFrame;

	[Header("Inspector Output (Read Only)")]
	[Tooltip("Resolved text currently being displayed in the TMP object.\nThis is either the ShellDefinition.shellId (when available) or the fallback text.\n\nInspector Reference:\n- Source: gun -> artilleryReloadController -> chamberedShell (runtime)\n- Blueprint: ShellBlueprint component on that shell object (or children if enabled)\n- Identity: ShellBlueprint.shellDefinition.shellId")]
	[SerializeField]
	private string resolvedText;

	[Header("Editor Convenience")]
	[Tooltip("If true, in the Editor (OnValidate) this component will auto-assign:\n- gun = GetComponent<GunController>() if gun is null.\nSafe for prefabs; does not search the scene.")]
	[SerializeField]
	private bool autoFindGunOnValidate;

	[Tooltip("If true, in Edit Mode this script will write fallbackTextForTMP into the TMP object.\nIt will NOT attempt to resolve a shell ID in Edit Mode (avoids UnassignedReferenceException during OnValidate / scene restore).")]
	[SerializeField]
	private bool writeFallbackInEditMode;

	private string lastAppliedText;

	private void OnValidate()
	{
	}

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void RefreshAndApply(bool forceApply)
	{
	}

	private string ResolveShellIdOrFallback_Safe()
	{
		return null;
	}
}
