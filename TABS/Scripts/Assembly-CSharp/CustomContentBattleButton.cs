using System;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomContentBattleButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
	public static Action<object> onClickOverride;

	public Color DarkColor;

	public Color BrightColor;

	public Image IconRenderer;

	public TextMeshProUGUI BattleNameText;

	public Image BattleNameTextBG;

	public GameObject NewContentGraphic;

	public Image Shadow;

	public UnitCreatorFactionBrowser browserManager;

	public TABSCampaignLevelAsset battle;

	public CustomContentBattleButton Setup(TABSCampaignLevelAsset battle)
	{
		this.battle = battle;
		BattleNameText.text = battle.Entity.Name;
		CampaignHandler.GetBattleSprite(battle, delegate(Sprite sprite)
		{
			if (IconRenderer != null && sprite != null)
			{
				IconRenderer.sprite = sprite;
			}
		});
		if (battle != null)
		{
			DMNewContentManager.IsContentNew(battle.ModID, battle.Entity.Name, !battle.IsModIOLevel, WorkshopContentType.Battle, delegate(bool isContentNew)
			{
				if (NewContentGraphic != null)
				{
					NewContentGraphic.SetActive(isContentNew);
				}
			});
		}
		return this;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HighlightButton();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		DeHighlightButton();
	}

	public void OnSelect(BaseEventData eventData)
	{
		HighlightButton();
	}

	public void OnDeselect(BaseEventData eventData)
	{
		DeHighlightButton();
	}

	private void HighlightButton()
	{
		BattleNameText.color = DarkColor;
		BattleNameTextBG.color = BrightColor;
		if (NewContentGraphic != null && NewContentGraphic.activeSelf && battle != null)
		{
			NewContentGraphic.SetActive(value: false);
			DMNewContentManager.RemoveNewContentID(battle.ModID, battle.Entity.Name, !battle.IsModIOLevel, WorkshopContentType.Battle);
		}
	}

	private void DeHighlightButton()
	{
		BattleNameText.color = BrightColor;
		BattleNameTextBG.color = DarkColor;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Click();
	}

	public void OnSubmit(BaseEventData eventData)
	{
		Click();
	}

	private void Click()
	{
		if (onClickOverride != null)
		{
			onClickOverride(battle);
		}
		else
		{
			UnityEngine.Object.FindObjectOfType<UnitCreatorFactionBrowser>().ShowBattle(battle);
		}
	}

	public void EnableShadow(bool enable)
	{
		Shadow.gameObject.SetActive(enable);
	}
}
