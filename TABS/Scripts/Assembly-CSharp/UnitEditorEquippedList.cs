using System;
using System.Collections.Generic;
using System.Linq;
using Landfall.TABS;
using TMPro;
using UnityEngine;

public class UnitEditorEquippedList : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup m_visibilityGroup;

	[SerializeField]
	private UnitEditorItemButton m_equipedItemTemplate;

	[SerializeField]
	private GameObject m_categoryTemplate;

	[SerializeField]
	private CodeAnimation m_equipedItemAnim;

	private Transform m_buttonRoot;

	private List<UnitEditorItemButton> m_itemsInList = new List<UnitEditorItemButton>();

	private GameObject[] m_categoryObjectList;

	private bool m_isOpen = true;

	private void Awake()
	{
		m_equipedItemTemplate.gameObject.SetActive(value: false);
		m_categoryTemplate.SetActive(value: false);
		m_buttonRoot = m_equipedItemTemplate.transform.parent;
		string[] names = Enum.GetNames(typeof(UnitRig.GearType));
		m_categoryObjectList = new GameObject[names.Length];
		for (int i = 0; i < names.Length; i++)
		{
			m_categoryObjectList[i] = UnityEngine.Object.Instantiate(m_categoryTemplate, m_categoryTemplate.transform.parent);
			m_categoryObjectList[i].GetComponentInChildren<TextMeshProUGUI>().text = names[i];
		}
	}

	public void ShowEquipped(bool open)
	{
		m_isOpen = open;
		UpdateVisibility();
	}

	public void AddEquipped(CharacterItem prop)
	{
		UnitEditorItemButton itembutton = UnityEngine.Object.Instantiate(m_equipedItemTemplate, m_equipedItemTemplate.transform.parent);
		itembutton.gameObject.SetActive(value: true);
		itembutton.UpdateButton(prop);
		m_itemsInList.Add(itembutton);
		m_itemsInList = (from b in m_itemsInList
			orderby b.CurrentPropItem.GearT, b.CurrentPropItem.name
			select b).ToList();
		for (int num = 0; num < m_categoryObjectList.Length; num++)
		{
			m_categoryObjectList[num].SetActive(value: false);
		}
		UnitRig.GearType gearType = UnitRig.GearType.HEAD;
		for (int num2 = 0; num2 < m_itemsInList.Count; num2++)
		{
			UnitRig.GearType gearT = m_itemsInList[num2].CurrentPropItem.GearT;
			if (num2 == 0 || gearT != gearType)
			{
				GameObject obj = m_categoryObjectList[(int)gearT];
				obj.SetActive(value: true);
				obj.transform.SetAsLastSibling();
			}
			m_itemsInList[num2].transform.SetAsLastSibling();
			gearType = gearT;
		}
		UpdateVisibility();
		prop.EOnRemove += delegate
		{
			m_itemsInList.Remove(itembutton);
			if (!m_itemsInList.Exists((UnitEditorItemButton b) => b.CurrentPropItem.GearT == itembutton.CurrentPropItem.GearT))
			{
				m_categoryObjectList[(int)itembutton.CurrentPropItem.GearT].SetActive(value: false);
			}
			UnityEngine.Object.Destroy(itembutton.gameObject);
			UpdateVisibility();
		};
	}

	public void UpdateVisibility()
	{
		if (m_itemsInList.Count < 1 || !m_isOpen)
		{
			if (m_equipedItemAnim.currentState != CodeAnimationInstance.AnimationUse.Out)
			{
				m_equipedItemAnim.PlayOut();
			}
			return;
		}
		if (m_equipedItemAnim.currentState != CodeAnimationInstance.AnimationUse.In)
		{
			m_equipedItemAnim.PlayIn();
		}
		m_equipedItemAnim.transform.SetAsLastSibling();
	}
}
