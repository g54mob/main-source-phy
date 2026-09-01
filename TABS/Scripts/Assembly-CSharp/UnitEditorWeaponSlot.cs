using Landfall.TABS.UnitEditor;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEditorWeaponSlot : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
{
	public bool isRight = true;

	public Image image;

	public Image handIcon;

	public Image outline;

	public TextMeshProUGUI text;

	public LocalizeText nameText;

	private UIScaleJiggle scaleJiggle;

	public void UpdateUI(UnitEditorManager.EquipedWeaponWrapper item)
	{
		scaleJiggle = GetComponent<UIScaleJiggle>();
		if (item != null)
		{
			image.enabled = true;
			image.sprite = item.prop.Entity.SpriteIcon;
			nameText.LocaleID = item.prop.DisplayName;
		}
		else
		{
			image.enabled = false;
			nameText.LocaleID = "";
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Object.FindObjectOfType<UnitEditorUIManager>().ClickedUnitEditorWeaponSlot(isRight);
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (!UnitEditorManager.isTestingUnit)
		{
			Object.FindObjectOfType<UnitEditorUIManager>().ClickedUnitEditorWeaponSlot(isRight);
		}
	}

	public void Disable()
	{
		Color color = image.color;
		color.a = 0.25f;
		image.color = color;
		Color color2 = outline.color;
		color2.a = 0.15f;
		outline.color = color2;
		text.color = color2;
		handIcon.color = color2;
	}

	public void Enable()
	{
		Color color = image.color;
		color.a = 1f;
		image.color = color;
		Color color2 = outline.color;
		color2.a = 1f;
		outline.color = color2;
		text.color = color2;
		handIcon.color = color2;
	}
}
