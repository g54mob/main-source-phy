using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
[AddComponentMenu("Missions/Mutators/Shell Loadout Applier")]
public class ShellLoadoutApplier : MonoBehaviour
{
	[Serializable]
	public class TargetLoadout
	{
		[Tooltip("CylinderShellSelector to receive this loadout.\nAssign the specific cylinder you wish to override when this applier is active.")]
		public CylinderShellSelector selector;

		[Tooltip("Shell prefabs in revolver order for this selector. Null entries create empty slots.\nLength may be <= or >= the selector's slot count:\n- If shorter: remaining slots become empty.\n- If longer: extra entries are ignored.\nSafe Example: [HE_Shell, HE_Shell, AP_Shell, AP_Shell, null, null]")]
		public GameObject[] shellPrefabs;

		[Tooltip("If true (default), the selector's 'shellPrefabs' array is overwritten to match these prefabs (cloned to the selector's slot count).\nSet false to ONLY change the runtime shells while leaving the selector's design-time 'shellPrefabs' untouched.")]
		public bool setAsDesignTimeTemplate;
	}

	[Serializable]
	public class TagLoadout
	{
		[Tooltip("Unity Tag to match. All GameObjects across ALL currently loaded scenes with this Tag AND a CylinderShellSelector component on the SAME GameObject will be targeted.\nNotes:\n- Only matches selectors on the tagged GameObject itself (does not search children).\n- For inactive objects, enable 'Include Inactive in Tag Search'.\nSafe examples: 'CylinderA', 'EnemyBattery', 'Artillery'.")]
		public string tag;

		[Tooltip("Shell prefabs in revolver order for selectors matched by this Tag. Null entries create empty slots.\nLength may be <= or >= the selector's slot count:\n- If shorter: remaining slots become empty.\n- If longer: extra entries are ignored.")]
		public GameObject[] shellPrefabs;

		[Tooltip("If true (default), the selector's 'shellPrefabs' array is overwritten to match these prefabs (cloned to the selector's slot count).\nSet false to ONLY change the runtime shells while leaving the selector's design-time 'shellPrefabs' untouched.")]
		public bool setAsDesignTimeTemplate;
	}

	[Header("Direct Targets")]
	[Tooltip("Explicit CylinderShellSelectors to override when this applier becomes active.\nUse these for precise, per-object loadouts.")]
	public List<TargetLoadout> directTargets;

	[Header("Tag Targets (Across Loaded Scenes)")]
	[Tooltip("Tag-based targets. For each entry, all GameObjects across ALL loaded scenes with the given Tag AND a CylinderShellSelector on the SAME GameObject will receive this loadout.\nUse this to apply a shared loadout to multiple cylinders, possibly across additive scenes.")]
	public List<TagLoadout> tagTargets;

	[Header("Search Options")]
	[Tooltip("If true, the tag-based search will include INACTIVE GameObjects across all loaded scenes.\nIf false (default), only ACTIVE GameObjects are matched.\nNote: Active search uses GameObject.FindGameObjectsWithTag(tag) (fast). Inactive search traverses all loaded scenes' hierarchies (slower, but only runs on activation or scene load if enabled).")]
	public bool includeInactiveInTagSearch;

	[Tooltip("If true, this applier will automatically apply loadouts again whenever any new scene is loaded (e.g., additive scenes).\nIf false (default), loadouts apply only when this component is enabled or when ApplyNow() is called manually.")]
	public bool applyOnSceneLoaded;

	[Header("Apply Behavior")]
	[Tooltip("If true (default), applies the loadouts every time this GameObject or component is enabled.\nIf false, applies only once the first time it becomes enabled (subsequent enables are ignored).")]
	public bool reapplyOnEnable;

	[Tooltip("If true, once a selector is successfully applied, subsequent applies will SKIP that selector within this applier's lifetime (until destroyed).\nUseful if you set 'applyOnSceneLoaded' and expect the same selectors to persist across loads.\nDefault: false (always re-apply).")]
	public bool skipSelectorsAlreadyApplied;

	[Tooltip("If true, prints detailed logs when finding targets and applying loadouts (useful during setup and debugging).")]
	public bool verbose;

	private bool _appliedOnce;

	private HashSet<int> _appliedSelectorIds;

	private static readonly List<CylinderShellSelector> _scratch;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	[ContextMenu("Apply Now")]
	public void ApplyNow()
	{
	}

	private static List<CylinderShellSelector> FindSelectorsByTag(string tag, bool includeInactive)
	{
		return null;
	}

	private static void TraverseAndCollectByTag(Transform t, string tag, List<CylinderShellSelector> results)
	{
	}
}
