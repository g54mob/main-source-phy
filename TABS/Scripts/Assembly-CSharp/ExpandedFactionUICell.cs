using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExpandedFactionUICell : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public Image colorImage;

	public Image iconImage;

	public TextMeshProUGUI text;

	public RawImage gradient;

	public void Setup(Faction faction)
	{
		text.text = faction.Entity.Name;
		float a = colorImage.color.a;
		Color factionColor = faction.m_FactionColor;
		if (factionColor.a > 0.1f)
		{
			factionColor.a = a;
			colorImage.color = factionColor;
		}
		else
		{
			colorImage.enabled = false;
		}
		faction.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
		{
			if (!(iconImage == null))
			{
				if (sprite != null)
				{
					iconImage.sprite = sprite;
				}
				else
				{
					iconImage.enabled = false;
				}
			}
		});
	}

	public void OnSelect()
	{
		gradient.enabled = true;
		text.color = Color.black;
	}

	public void OnDeselect()
	{
		gradient.enabled = false;
		text.color = Color.white;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OnSelect();
	}
}
