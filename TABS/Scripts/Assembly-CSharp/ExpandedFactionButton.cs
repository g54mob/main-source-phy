using Landfall.TABS;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExpandedFactionButton : MonoBehaviour, IPointerUpHandler, IEventSystemHandler, IDragHandler
{
	public LocalizeText factionName;

	public Image icon;

	public Image m_factionColorImage;

	public float size = 75f;

	private Faction faction;

	private ExpandedFactionUI expandedFactionUI;

	private float alpha;

	private bool available = true;

	public MonoBehaviour[] destoryOnDragClone;

	private bool isDragging;

	private void OnEnable()
	{
		isDragging = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left && !isDragging)
		{
			isDragging = true;
			StartDragging();
		}
	}

	private void Click()
	{
		if (CharacterItemExtensions.ShouldCustomContentBeValidated() && !faction.ValidateFactionUnitPropsAndWeapons())
		{
			ServiceLocator.GetService<ModalPanel>().PopUp("CUSTOM_CONTENT_VALIDATION_FAILED_ADDITIONAL_INFO");
		}
		expandedFactionUI.ExpandedButtonClicked(faction);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (!isDragging)
			{
				Click();
			}
			else
			{
				StopDragging();
			}
		}
	}

	private void StartDragging()
	{
		if (!Input.GetMouseButton(1) && available)
		{
			expandedFactionUI.BeginDragging(this);
		}
	}

	private void StopDragging()
	{
		isDragging = false;
	}

	public void Setup(Faction faction, ExpandedFactionUI expandedFactionUI)
	{
		this.expandedFactionUI = expandedFactionUI;
		this.faction = faction;
		factionName.Localized = !faction.IsCustom;
		factionName.LocaleID = (string.IsNullOrEmpty(faction.Entity.Name) ? faction.name : faction.Entity.Name);
		m_factionColorImage.color = faction.m_FactionColor;
		alpha = faction.m_FactionColor.a;
		faction.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
		{
			if (sprite != null && icon != null)
			{
				icon.sprite = sprite;
			}
		});
	}

	public void SetAvailability(bool available)
	{
		this.available = available;
		try
		{
			Color color = m_factionColorImage.color;
			Color color2 = icon.color;
			Color color3 = factionName.Text.color;
			color.a = (available ? 1f : 0.4f) * alpha;
			color2.a = (available ? 1f : 0.4f);
			color3.a = (available ? 1f : 0.4f);
			m_factionColorImage.color = color;
			icon.color = color2;
			factionName.Text.color = color3;
		}
		catch
		{
		}
	}

	public ExpandedFactionButton SetupDragClone()
	{
		for (int i = 0; i < destoryOnDragClone.Length; i++)
		{
			Object.Destroy(destoryOnDragClone[i]);
		}
		return this;
	}

	public Faction GetFaction()
	{
		return faction;
	}

	public void ToggleFaction()
	{
		if (PlayerActions.Instance.InputType == InputType.Controller)
		{
			Click();
		}
	}
}
