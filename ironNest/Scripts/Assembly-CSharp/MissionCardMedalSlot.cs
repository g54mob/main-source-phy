using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Campaign Map/Mission Card Medal Slot (SpriteRenderer)")]
public class MissionCardMedalSlot : MonoBehaviour
{
	[Header("Category")]
	[Tooltip("Medal category definition (visual library asset) for this slot.\nThis determines which sprites are used for:\n- Unearned (hint icon)\n- Bronze\n- Silver\n- Gold\n\nAuthoring workflow:\n- Create category assets once in the project.\n- Drag the chosen category onto each slot in the mission card prefab.")]
	public MedalCategoryDefinition category;

	[Header("Renderer")]
	[Tooltip("SpriteRenderer that displays the medal sprite in world space.\nSafe default:\n- If left empty, this component will auto-assign the SpriteRenderer on the same GameObject (if found).")]
	public SpriteRenderer targetRenderer;

	[Header("Phase 1 Testing (Inspector-Driven)")]
	[Tooltip("Tier to display for this slot during Phase 1 visual prototyping.\nThis is intended for testing/polish only.\n\nLater, the parent card can override this by calling SetTier(...) based on progression/save data.")]
	public MedalTier testTier;

	[Tooltip("If true, the slot will automatically refresh its SpriteRenderer in OnValidate and OnEnable.\nRecommended while prototyping.\nYou can disable this later if you prefer the parent card to manage all refresh calls.")]
	public bool autoRefresh;

	private bool _hasRuntimeTier;

	private MedalTier _runtimeTier;

	private void Reset()
	{
	}

	private void OnEnable()
	{
	}

	public void SetTier(MedalTier tier)
	{
	}

	public void ClearRuntimeTier()
	{
	}

	public void Refresh()
	{
	}
}
