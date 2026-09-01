using System;
using System.Collections.Generic;
using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorItemList : MonoBehaviour
{
	[SerializeField]
	private UnitEditorItemButton m_templateButton;

	[SerializeField]
	private TMP_InputField m_searchField;

	public CodeAnimation anim;

	public GameObject listObj;

	public GameObject gridObj;

	public bool itemListIsOut;

	public int gridChildren;

	private List<UnitEditorItemButton> m_buttons = new List<UnitEditorItemButton>();

	private Stack<UnitEditorItemButton> m_cachedButtons = new Stack<UnitEditorItemButton>();

	private static char[] _split = new char[1] { ' ' };

	public event Action Opened;

	public event Action Closed;

	private void Awake()
	{
		m_templateButton.gameObject.SetActive(value: false);
		m_searchField.onValueChanged.AddListener(UpdateSearchField);
		for (int i = 0; i < 50; i++)
		{
			UnitEditorItemButton unitEditorItemButton = UnityEngine.Object.Instantiate(m_templateButton, m_templateButton.transform.parent);
			unitEditorItemButton.gameObject.SetActive(value: false);
			m_cachedButtons.Push(unitEditorItemButton);
		}
	}

	private void Start()
	{
		gridChildren = gridObj.transform.childCount;
	}

	public void UpdateSearchField(string newSearch)
	{
		string[] array = newSearch.ToLower().Split(_split, StringSplitOptions.RemoveEmptyEntries);
		int num = array.Length;
		bool flag = string.IsNullOrWhiteSpace(newSearch);
		int count = m_buttons.Count;
		for (int i = 0; i < count; i++)
		{
			bool active = true;
			if (!flag)
			{
				string text = m_buttons[i].PropName.ToLower();
				for (int j = 0; j < num; j++)
				{
					if (!text.Contains(array[j]))
					{
						active = false;
						break;
					}
				}
			}
			m_buttons[i].gameObject.SetActive(active);
		}
	}

	public void UpdateItemButtons(List<CharacterItem> newItems)
	{
		Vector3 localPosition = m_templateButton.transform.parent.localPosition;
		localPosition.y = 0f;
		m_templateButton.transform.parent.localPosition = localPosition;
		for (int i = 0; i < m_buttons.Count; i++)
		{
			m_buttons[i].gameObject.SetActive(value: false);
			m_cachedButtons.Push(m_buttons[i]);
		}
		m_buttons.Clear();
		for (int j = 0; j < newItems.Count; j++)
		{
			UnitEditorItemButton unitEditorItemButton = null;
			unitEditorItemButton = ((m_cachedButtons.Count <= 0) ? UnityEngine.Object.Instantiate(m_templateButton, m_templateButton.transform.parent) : m_cachedButtons.Pop());
			unitEditorItemButton.transform.SetAsLastSibling();
			unitEditorItemButton.gameObject.SetActive(value: true);
			unitEditorItemButton.UpdateButton(newItems[j]);
			m_buttons.Add(unitEditorItemButton);
		}
		UpdateSearchField(m_searchField.text);
	}

	private void Update()
	{
		if (gridChildren == gridObj.transform.childCount && itemListIsOut)
		{
			Close();
		}
	}

	private void Close()
	{
		Debug.Log("Trying to get out");
		itemListIsOut = false;
		anim.PlayOut();
		Button[] componentsInChildren = listObj.GetComponentsInChildren<Button>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].interactable = false;
		}
		UnitEditorHandler.Instance.RemoveTemporary();
		UnitEditorHandler.Instance.ResetCameraPosition();
		this.Closed?.Invoke();
	}

	public void OpenItemList()
	{
		if (itemListIsOut)
		{
			anim.PlayBoop();
			return;
		}
		itemListIsOut = true;
		anim.PlayIn();
		Button[] componentsInChildren = listObj.GetComponentsInChildren<Button>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].interactable = true;
		}
		this.Opened?.Invoke();
	}
}
