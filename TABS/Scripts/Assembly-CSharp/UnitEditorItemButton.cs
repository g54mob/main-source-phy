using System.Collections.Generic;
using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEditorItemButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[SerializeField]
	private Image m_icon;

	[SerializeField]
	private TextMeshProUGUI m_text;

	[SerializeField]
	private GameObject m_equipButtonObject;

	[SerializeField]
	private Button m_itemButton;

	[SerializeField]
	private bool m_equipWhenPressed = true;

	private CharacterItem m_currentPropItem;

	private Button m_equipButton;

	private bool m_hover;

	private UnitEditorContextMenu m_contextMenu;

	private UnitEditorCameraSpinner m_cameraSpinner;

	private string m_propName;

	private static GameObject _currentPropButton;

	public CharacterItem CurrentPropItem => m_currentPropItem;

	public string PropName => m_propName;

	public static void TurnOffCurrentPropButton()
	{
		if (_currentPropButton != null)
		{
			_currentPropButton.SetActive(value: false);
		}
	}

	private void Awake()
	{
		m_contextMenu = UnitEditorHandler.Instance.ContextMenu;
		m_cameraSpinner = m_contextMenu.GetComponent<UnitEditorCameraSpinner>();
		if (m_equipWhenPressed)
		{
			m_itemButton.onClick.AddListener(delegate
			{
				EquipItem();
			});
		}
		else
		{
			m_itemButton.onClick.AddListener(delegate
			{
				SelectItem();
			});
		}
		if (m_equipButtonObject != null)
		{
			m_equipButton = m_equipButtonObject.GetComponent<Button>();
			m_equipButton.onClick.AddListener(delegate
			{
				TurnOffCurrentPropButton();
				UnitEditorHandler.Instance.EquipSpawnedProp();
			});
		}
	}

	public void UpdateButton(CharacterItem newPropItem)
	{
		if ((bool)newPropItem)
		{
			m_icon.sprite = newPropItem.Entity.SpriteIcon;
		}
		m_text.text = newPropItem.PropName;
		m_propName = m_text.text;
		m_currentPropItem = newPropItem;
	}

	public void EquipItem()
	{
		int num = 0;
		List<CharacterItem> equippedProps = UnitEditorHandler.Instance.EquippedProps;
		for (int i = 0; i < equippedProps.Count; i++)
		{
			if (equippedProps[i].Entity.GUID == m_currentPropItem.Entity.GUID)
			{
				SelectItem(equippedProps[i]);
				return;
			}
			if (equippedProps[i].GearT == m_currentPropItem.GearT)
			{
				num++;
			}
		}
		if (num >= 3)
		{
			Debug.Log("Can't equip more than 3 of same type");
			return;
		}
		m_contextMenu.CloseContextMenu();
		UnitEditorHandler.Instance.SetHoverPropTemporary();
		if (m_currentPropItem.GetComponent<Weapon>() != null)
		{
			UnitEditorHandler.Instance.EquipSpawnedProp();
			return;
		}
		if (_currentPropButton != null)
		{
			_currentPropButton.SetActive(value: false);
		}
		_currentPropButton = m_equipButtonObject;
		m_equipButtonObject.SetActive(value: true);
	}

	public void RemoveProp()
	{
		UnitEditorHandler.Instance.RemoveEquipedProp(m_currentPropItem);
	}

	public void SelectItem()
	{
		m_contextMenu.OpenContextMenu(m_currentPropItem);
	}

	public void SelectItem(CharacterItem selectProp)
	{
		m_contextMenu.OpenContextMenu(selectProp);
	}

	private void Update()
	{
		if (m_hover && !m_equipWhenPressed && !m_contextMenu.IsOpen && !m_cameraSpinner.IsSpinning)
		{
			m_currentPropItem.Hover();
		}
	}

	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		m_hover = true;
		if (m_equipWhenPressed && !m_cameraSpinner.IsSpinning)
		{
			UnitEditorHandler.Instance.AddHoverProp(m_currentPropItem);
		}
	}

	public void OnPointerExit(PointerEventData pointerEventData)
	{
		m_hover = false;
		if (m_equipWhenPressed)
		{
			UnitEditorHandler.Instance.RemoveHoverProp();
		}
	}
}
