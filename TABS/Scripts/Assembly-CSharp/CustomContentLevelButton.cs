using System;
using Landfall.TABS.Workshop;
using LevelCreator;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomContentLevelButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
	public static Action<object> onClickOverride;

	public Color DarkColor;

	public Color BrightColor;

	public RawImage IconRenderer;

	public TextMeshProUGUI LevelNameText;

	public Image LevelNameTextBG;

	public GameObject NewContentGraphic;

	public Image Shadow;

	public UnitCreatorFactionBrowser browserManager;

	public CustomMap customMap;

	public CustomContentLevelButton Setup(CustomMap customMap)
	{
		this.customMap = customMap;
		LevelNameText.text = customMap.Entity.Name;
		SetCustomMapIcon(customMap);
		DMNewContentManager.IsContentNew(customMap.ModID, customMap.Entity.Name, !customMap.IsModLevel(), WorkshopContentType.Map, delegate(bool isContentNew)
		{
			if (NewContentGraphic != null)
			{
				NewContentGraphic.SetActive(isContentNew);
			}
		});
		return this;
	}

	private void SetCustomMapIcon(CustomMap customMap)
	{
		customMap.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
		{
			if (IconRenderer != null && sprite != null)
			{
				IconRenderer.texture = sprite.texture;
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
		LevelNameText.color = DarkColor;
		LevelNameTextBG.color = BrightColor;
		if (NewContentGraphic != null && NewContentGraphic.activeSelf && customMap != null)
		{
			NewContentGraphic.SetActive(value: false);
			DMNewContentManager.RemoveNewContentID(customMap.ModID, customMap.Entity.Name, !customMap.IsModLevel(), WorkshopContentType.Map);
		}
	}

	private void DeHighlightButton()
	{
		LevelNameText.color = BrightColor;
		LevelNameTextBG.color = DarkColor;
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
			onClickOverride(customMap);
		}
		else
		{
			UnityEngine.Object.FindObjectOfType<UnitCreatorFactionBrowser>().ShowLevel(customMap);
		}
	}

	public void EnableShadow(bool enable)
	{
		Shadow.gameObject.SetActive(enable);
	}
}
