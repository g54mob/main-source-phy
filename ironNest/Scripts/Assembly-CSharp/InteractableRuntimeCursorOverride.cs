using UnityEngine;

[AddComponentMenu("Gameplay/Interaction/Interactable Runtime Cursor Override")]
[DisallowMultipleComponent]
public class InteractableRuntimeCursorOverride : MonoBehaviour
{
	[Header("Target Interactable (Optional)")]
	[Tooltip("Optional informational reference to the Interactable this override belongs to.\n\nRecommended:\n- Leave empty if this component is on the same GameObject as the Interactable.\n- Enable 'Auto Find Interactable' so it populates automatically.\n\nNotes:\n- UnifiedCursorUI does NOT require this field to be set (it queries this component directly on the hovered Interactable).")]
	[SerializeField]
	private Interactable targetInteractable;

	[Tooltip("If true and 'Target Interactable' is null, this component attempts to find an Interactable on the same GameObject in Awake().\n\nSafe default: true.")]
	[SerializeField]
	private bool autoFindInteractable;

	[Header("Hover Override (Runtime)")]
	[Tooltip("If true, a runtime Hover cursor override is active.\nWhen active, cursor presentation should prefer this over Interactable.TryGetCursor.\n\nRules:\n- If the texture is null, the override is treated as inactive even if this is true.\n\nSet via SetHoverOverride/ClearHoverOverride at runtime.")]
	[SerializeField]
	private bool hasHoverOverride;

	[Tooltip("Runtime Hover override texture.\n\nValid only when 'Has Hover Override' is true.\nIf null, the override is treated as inactive.\n\nNotes:\n- The cursor graphic is always centered; no hotspot is used.")]
	[SerializeField]
	private Texture2D hoverOverrideTexture;

	[Header("Grab Override (Runtime)")]
	[Tooltip("If true, a runtime Grab cursor override is active.\nWhen active, cursor presentation should prefer this over Interactable.TryGetGrabCursor.\n\nRules:\n- If the texture is null, the override is treated as inactive even if this is true.\n\nSet via SetGrabOverride/ClearGrabOverride at runtime.")]
	[SerializeField]
	private bool hasGrabOverride;

	[Tooltip("Runtime Grab override texture.\n\nValid only when 'Has Grab Override' is true.\nIf null, the override is treated as inactive.\n\nNotes:\n- The cursor graphic is always centered; no hotspot is used.")]
	[SerializeField]
	private Texture2D grabOverrideTexture;

	[Header("Diagnostics")]
	[Tooltip("If true, logs when overrides are set/cleared.\n\nSafe to disable in production.")]
	[SerializeField]
	private bool debugLogs;

	public Interactable TargetInteractable => null;

	public bool HasHoverOverride => false;

	public bool HasGrabOverride => false;

	private void Awake()
	{
	}

	public bool TryGetHoverOverride(out Texture2D texture)
	{
		texture = null;
		return false;
	}

	public bool TryGetGrabOverride(out Texture2D texture)
	{
		texture = null;
		return false;
	}

	[Tooltip("Sets (or clears) the runtime Hover cursor override.\n\nRules:\n- If 'Texture' is null, the hover override becomes inactive.\n- If 'Texture' is non-null, the hover override becomes active.\n\nTypical usage:\n- Tool selection sets hover override on the map surface Interactable.\n- Deselect clears it.\n\nNotes:\n- Hotspots are not supported; the cursor is always centered.")]
	public void SetHoverOverride(Texture2D texture)
	{
	}

	[Tooltip("Clears the runtime Hover cursor override (sets it inactive).")]
	public void ClearHoverOverride()
	{
	}

	[Tooltip("Sets (or clears) the runtime Grab cursor override.\n\nRules:\n- If 'Texture' is null, the grab override becomes inactive.\n- If 'Texture' is non-null, the grab override becomes active.\n\nTypical usage:\n- Optional: tool selection sets grab override on the map surface Interactable for consistent drag feedback.\n- Deselect clears it.\n\nNotes:\n- Hotspots are not supported; the cursor is always centered.")]
	public void SetGrabOverride(Texture2D texture)
	{
	}

	[Tooltip("Clears the runtime Grab cursor override (sets it inactive).")]
	public void ClearGrabOverride()
	{
	}

	[Tooltip("Clears BOTH hover and grab overrides.")]
	public void ClearAll()
	{
	}
}
