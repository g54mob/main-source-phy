using System;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomContentFactionButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
	public static Action<object> onClickOverride;

	public Color DarkColor;

	public Image BackgroundRenderer;

	public Image IconRenderer;

	public GameObject NewContentGraphic;

	public TextMeshProUGUI FactionNameText;

	public TextMeshProUGUI FactionUnitCountText;

	public UnitCreatorFactionBrowser factionBrowser;

	private Faction faction;

	private Color factionColor;

	public CustomContentFactionButton Setup(Faction faction)
	{
		this.faction = faction;
		factionColor = faction.CustomFactionColor.m_Color;
		SetFactionIcon(faction);
		FactionNameText.text = faction.Entity.Name;
		FactionUnitCountText.text = faction.Units.Length.ToString();
		DMNewContentManager.IsContentNew(faction.modID, faction.Entity.Name, !faction.IsModFaction, WorkshopContentType.Faction, delegate(bool isContentNew)
		{
			if (NewContentGraphic != null)
			{
				NewContentGraphic.SetActive(isContentNew);
			}
		});
		DeHighlightButton();
		return this;
	}

	private void SetFactionIcon(Faction faction)
	{
		faction.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
		{
			if (IconRenderer != null && sprite != null)
			{
				IconRenderer.sprite = sprite;
			}
		});
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
		BackgroundRenderer.color = Color.white;
		IconRenderer.color = factionColor;
		FactionNameText.color = DarkColor;
		if (NewContentGraphic != null && NewContentGraphic.activeSelf)
		{
			NewContentGraphic.SetActive(value: false);
			DMNewContentManager.RemoveNewContentID(faction.modID, faction.Entity.Name, !faction.IsModFaction, WorkshopContentType.Faction);
		}
	}

	private void DeHighlightButton()
	{
		BackgroundRenderer.color = factionColor;
		IconRenderer.color = Color.white;
		FactionNameText.color = Color.white;
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
			onClickOverride(faction);
		}
		else
		{
			UnityEngine.Object.FindObjectOfType<UnitCreatorFactionBrowser>().ShowFaction(faction);
		}
	}
}
