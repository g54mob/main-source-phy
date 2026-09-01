using Localisation;
using UnityEngine;

[CreateAssetMenu(fileName = "MedalCategory", menuName = "Campaign Map/Medals/Medal Category Definition (Visual)", order = 0)]
public class MedalCategoryDefinition : ScriptableObject
{
	[Header("Identity")]
	[Tooltip("Internal ID used to identify this medal category.\nRecommended:\n- Keep stable once shipped (acts like a key).\n- Use short, consistent IDs (e.g., 'accuracy', 'speed', 'collateral').\n\nNotes:\n- This is NOT shown to players.\n- If left empty, systems may fallback to the asset name.")]
	public string id;

	[Tooltip("Player-facing label for this medal category.\nExamples:\n- 'Accuracy'\n- 'Rate of Fire'\n- 'Collateral'\n\nUI usage:\n- May be shown on or near the medal slot.\n- Can be omitted if you prefer icon-only hints.")]
	public TextIdentifier displayNameV2;

	[Tooltip("Optional short hint text describing how to earn this medal.\nKeep it brief and diegetic.\n\nExamples:\n- 'Tight grouping'\n- 'Work fast'\n- 'Avoid civilian damage'\n\nIf you prefer icon-only hints, leave this empty and hide it in UI.")]
	public TextIdentifier hintTextV2;

	[Header("Tier Sprites (Required)")]
	[Tooltip("Sprite used when the medal is UNEARNED.\nImportant:\n- Dedicated texture swap (not a tinted earned medal).\n- Also acts as the visual 'hint' icon for this category.\n\nExample:\n- A faded crosshair icon for Accuracy.")]
	public Sprite unearnedSprite;

	[Tooltip("Optional: Tint applied to the medal sprite in the world.\nLeave at white (default) for no tint.\n\nUse cases:\n- Slightly age/sepia medals to match paper tone.\n- Differentiate challenge medals visually without changing textures.")]
	public Color worldTint;

	[Header("Bronze")]
	[Tooltip("Sprite used when the medal is earned at BRONZE tier.\nUse a dedicated bronze texture swap.")]
	public Sprite bronzeSprite;

	public MedalConditionSet BronzeConditions;

	[Header("Silver")]
	[Tooltip("Sprite used when the medal is earned at SILVER tier.\nUse a dedicated silver texture swap.")]
	public Sprite silverSprite;

	public MedalConditionSet SilverConditions;

	[Header("Gold")]
	[Tooltip("Sprite used when the medal is earned at GOLD tier.\nUse a dedicated gold texture swap.")]
	public Sprite goldSprite;

	public MedalConditionSet GoldConditions;

	public Sprite GetSpriteForTier(MedalTier tier)
	{
		return null;
	}

	public string GetIdSafe()
	{
		return null;
	}
}
