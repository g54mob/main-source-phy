using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Gameplay/Interactable")]
public class Interactable : MonoBehaviour
{
	[Header("Prompt")]
	[Tooltip("User-facing prompt string supporting tokens.\n\nSupported Tokens:\n- {objectName} => Replaced with this GameObject's name (if Auto Replace Object Name is true).\n- {prompt} => Echoes the entire original promptText.\n\nFormat Rules:\n- Tokens are case-sensitive.\n- Unknown tokens are left unchanged.\n\nSafe Examples:\n- \"Inspect {objectName}\"\n- \"Use {objectName}\"")]
	[SerializeField]
	private string promptText;

	[Tooltip("If true, automatically replaces the {objectName} token (if present) with this GameObject.name.\n\nNotes:\n- This does not affect other tokens.\n- Unknown tokens are left unchanged.")]
	[SerializeField]
	private bool autoReplaceObjectName;

	[Header("Cursor Texture Overrides (No Hotspots)")]
	[Tooltip("Texture used while cursor is hovering this Interactable (if assigned).\n\nNotes:\n- Hotspots are not supported. UnifiedCursorUI always centers the cursor graphic on the VirtualCursor position.\n- If null, the UI cursor falls back to its shared hover/default textures.")]
	[SerializeField]
	private Texture2D cursorOverride;

	[Tooltip("Texture used WHILE this Interactable is actively grabbed/dragged (if assigned).\n\nNotes:\n- Hotspots are not supported. UnifiedCursorUI always centers the cursor graphic on the VirtualCursor position.\n- If null, the UI cursor falls back to its shared grab/hover/default textures.")]
	[SerializeField]
	private Texture2D cursorGrabOverride;

	[Header("State")]
	[Tooltip("Soft enable/disable without removing component.\n\nWhen false:\n- Hover detection treats this as non-interactable.\n- Cursor override textures are ignored.\n- MatchesHitCollider returns false.")]
	[SerializeField]
	private bool isInteractable;

	[Header("Passive Mode")]
	[Tooltip("If true, this Interactable is detected by the raycast and fires\nDynamicCursorManager.OnPassiveTargetChanged, but does NOT become\nCurrentHover and does NOT steal hover from non-passive Interactables\nbehind it in the ray.\n\nUse cases:\n- Medal slots sitting in front of a mission card: the card keeps its\n  hover state and cursor visuals while the medal slot still reports\n  which slot the cursor is over via OnPassiveTargetChanged.\n\nNotes:\n- Passive Interactables do not affect cursor visual state (no Hover/Grab).\n- Passive Interactables cannot be grabbed or clicked through the manager.\n- isInteractable must also be true for passive detection to fire.")]
	[SerializeField]
	private bool isPassive;

	[Header("Collider Filtering")]
	[Tooltip("If true, this Interactable only responds to ray hits coming from colliders listed in 'Allowed Colliders'.\n\nWhen false:\n- Any collider under this Interactable's transform hierarchy may trigger it (backward compatible).\n\nUse cases:\n- Prevent 'child detail colliders' from being clickable.\n- Restrict interaction to a specific surface collider.")]
	[SerializeField]
	private bool restrictToAllowedColliders;

	[Tooltip("Explicit list of Collider components that are considered valid hit sources for this Interactable WHEN\n'Restrict To Allowed Colliders' is true.\n\nPopulate manually or use 'Populate From Children (One Shot)'.\n\nTip:\n- Include colliders on this object and its children that should act as the click/hover surface for this Interactable.")]
	[SerializeField]
	private List<Collider> allowedColliders;

	[Tooltip("One-shot utility.\n\nToggle this to true in the Inspector to populate 'Allowed Colliders' with ALL Collider components found on this GameObject\nand its children (including inactive).\n\nAutomatically resets to false after running.")]
	[SerializeField]
	private bool populateFromChildrenOneShot;

	public bool IsInteractable => false;

	public bool IsPassive => false;

	[Tooltip("Sets whether this Interactable can be hovered/clicked by systems like DynamicCursorManager.\n\nNotes:\n- This is a soft enable/disable that does not remove the component.")]
	public void SetInteractable(bool value)
	{
	}

	public string GetResolvedPrompt()
	{
		return null;
	}

	public bool TryGetCursor(out Texture2D texture, out Vector2 hotspot)
	{
		texture = null;
		hotspot = default(Vector2);
		return false;
	}

	public bool TryGetGrabCursor(out Texture2D texture, out Vector2 hotspot)
	{
		texture = null;
		hotspot = default(Vector2);
		return false;
	}

	public bool MatchesHitCollider(Collider hitCollider)
	{
		return false;
	}
}
