using TMPro;
using UnityEngine;

[AddComponentMenu("Campaign Map/Medal Tooltip Display")]
public class MedalTooltipDisplay : MonoBehaviour
{
	[Header("Text References")]
	[Tooltip("TMP_Text that shows the hovered medal slot's display name.\n\nSource: MedalCategoryDefinition.displayName on the passive-hovered slot's category.\nCleared to empty string when no passive Interactable is hovered.\n\nSetup: drag a TMP_Text child of this card here.")]
	[SerializeField]
	private TMP_Text displayNameText;

	[Tooltip("TMP_Text that shows the hovered medal slot's hint text.\n\nSource: MedalCategoryDefinition.hintText on the passive-hovered slot's category.\nCleared to empty string when no passive Interactable is hovered.\n\nSetup: drag a TMP_Text child of this card here.")]
	[SerializeField]
	private TMP_Text hintText;

	[Header("Cursor Manager")]
	[Tooltip("Tag used to locate the DynamicCursorManager in the scene via GameObject.FindWithTag().\n\nRules:\n- Must exactly match a tag defined in your project's Tag Manager.\n- Only one DynamicCursorManager should carry this tag at a time.\n\nDefault: \"CursorManager\"\n\nSafe examples:\n- \"CursorManager\"\n- \"GameplayCursor\"")]
	[SerializeField]
	private string cursorManagerTag;

	private DynamicCursorManager _cursorManager;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void TrySubscribe()
	{
	}

	private void Unsubscribe()
	{
	}

	private void OnPassiveTargetChanged(Interactable target)
	{
	}

	private void ClearTexts()
	{
	}
}
