using System;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitButtonBase : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
	public static Action<object> onClickOverride;

	public Image IconImage;

	public Image NameSelect;

	public TextMeshProUGUI UnitName;

	public TextMeshProUGUI UnitCost;

	public GameObject NewContentGraphic;

	public Color TextSelectedColor;

	public Color OnSelectedIconColor;

	protected float targetIconColor;

	protected UnitBlueprint unitBlueprint;

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnEnter();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnExit();
	}

	public void OnSelect(BaseEventData eventData)
	{
		OnEnter();
	}

	public void OnDeselect(BaseEventData eventData)
	{
		OnExit();
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
			onClickOverride(unitBlueprint);
		}
		else
		{
			OnClick();
		}
	}

	public virtual void OnClick()
	{
	}

	public virtual void OnEnter()
	{
		NameSelect.enabled = true;
		UnitName.color = TextSelectedColor;
		targetIconColor = 1f;
		UnitCost.enabled = true;
		if (NewContentGraphic != null && NewContentGraphic.activeSelf)
		{
			NewContentGraphic.SetActive(value: false);
			DMNewContentManager.RemoveNewContentID(unitBlueprint.ModID, unitBlueprint.Entity.Name, !unitBlueprint.IsModUnit, WorkshopContentType.Unit);
		}
	}

	public virtual UnitButtonBase Setup(UnitBlueprint unit)
	{
		if (unit != null)
		{
			UnitName.text = unit.Name;
			UnitCost.text = unit.GetUnitCost().ToString();
			IconImage.enabled = true;
			unit.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (IconImage != null && sprite != null)
				{
					IconImage.sprite = sprite;
				}
			});
			if (!unit.IsCustomUnit && GlobalSettingsHandler.CurrentPlatform != SettingsInstance.Platform.Switch)
			{
				IconImage.transform.localScale = Vector3.one * 0.7f;
			}
			else
			{
				IconImage.transform.localScale = Vector3.one;
			}
			unitBlueprint = unit;
			if (NewContentGraphic != null)
			{
				DMNewContentManager.IsContentNew(unitBlueprint.ModID, unitBlueprint.Entity.Name, !unitBlueprint.IsModUnit, WorkshopContentType.Unit, delegate(bool isContentNew)
				{
					if (NewContentGraphic != null)
					{
						NewContentGraphic.SetActive(isContentNew);
					}
				});
			}
		}
		else if (IconImage != null)
		{
			IconImage.enabled = false;
		}
		return this;
	}

	public virtual void OnExit()
	{
		NameSelect.enabled = false;
		UnitName.color = Color.white;
		targetIconColor = 0f;
		UnitCost.enabled = false;
	}

	private void Update()
	{
		IconImage.color = Color.Lerp(IconImage.color, Color.Lerp(Color.white, OnSelectedIconColor, targetIconColor), 10f * Time.deltaTime);
	}

	public UnitBlueprint GetUnit()
	{
		return unitBlueprint;
	}
}
