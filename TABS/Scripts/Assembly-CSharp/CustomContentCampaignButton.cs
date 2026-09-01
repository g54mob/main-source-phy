using System;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomContentCampaignButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
	public static Action<object> onClickOverride;

	public Color DarkColor;

	public Color BrightColor;

	public Image IconRenderer;

	public TextMeshProUGUI CampaignNameText;

	public Image CampaignNameTextBG;

	public GameObject NewContentGraphic;

	public UnitCreatorFactionBrowser browserManager;

	public TABSCampaignAsset campaign;

	public CustomContentCampaignButton Setup(TABSCampaignAsset campaign)
	{
		this.campaign = campaign;
		CampaignNameText.text = campaign.Entity.Name;
		CampaignHandler.GetCampaignSprite(campaign, delegate(Sprite sprite)
		{
			if (IconRenderer != null && sprite != null)
			{
				IconRenderer.sprite = sprite;
			}
		});
		DMNewContentManager.IsContentNew(campaign.ModID, campaign.Entity.Name, !campaign.IsModCampaign, WorkshopContentType.Campaign, delegate(bool isContentNew)
		{
			if (NewContentGraphic != null)
			{
				NewContentGraphic.SetActive(isContentNew);
			}
		});
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
		CampaignNameText.color = DarkColor;
		CampaignNameTextBG.color = BrightColor;
		if (NewContentGraphic != null && NewContentGraphic.activeSelf)
		{
			NewContentGraphic.SetActive(value: false);
			DMNewContentManager.RemoveNewContentID(campaign.ModID, campaign.Entity.Name, !campaign.IsModCampaign, WorkshopContentType.Campaign);
		}
	}

	private void DeHighlightButton()
	{
		CampaignNameText.color = BrightColor;
		CampaignNameTextBG.color = DarkColor;
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
			onClickOverride(campaign);
		}
		else
		{
			UnityEngine.Object.FindObjectOfType<UnitCreatorFactionBrowser>().ShowCampaign(campaign);
		}
	}
}
