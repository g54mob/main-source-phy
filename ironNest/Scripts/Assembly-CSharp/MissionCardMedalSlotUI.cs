using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Image))]
[AddComponentMenu("Campaign Map/Mission Card Medal Slot (UI)")]
public class MissionCardMedalSlotUI : MonoBehaviour
{
	[Header("Category")]
	public MedalCategoryDefinition category;

	[Header("Renderer")]
	public Image targetRenderer;

	public TMP_Text Text_MedalTitle;

	public TMP_Text Text_MedalHint;

	private MedalTier currentTier;

	private void Start()
	{
	}

	public void ClearRuntimeTier()
	{
	}

	public void SetTier(MedalTier tier)
	{
	}

	public void UpdateUI()
	{
	}

	public void OnPointerEnter()
	{
	}

	public void OnPointerExit()
	{
	}
}
