using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InControl;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelCreator
{
	public class Grid : DMUIPanel
	{
		[Header("Grid")]
		[SerializeField]
		private LocalizeText m_gridNameText;

		[SerializeField]
		private GameObject m_grid;

		[SerializeField]
		private GameObject m_gridItemPrefab;

		[SerializeField]
		private CategoryButton m_categoryButtonPrefab;

		[SerializeField]
		private GameObject m_categoryButtonDotPrefab;

		[SerializeField]
		private GameObject m_categoryCycleLeftButtonPrefab;

		[SerializeField]
		private GameObject m_categoryCycleRightButtonPrefab;

		[SerializeField]
		private Transform m_categoryButtonsTransform;

		[SerializeField]
		private Transform m_categoryButtonsDotsTransform;

		[SerializeField]
		private GameObject m_categoryItemsPrefab;

		[SerializeField]
		private Transform m_categoryItemsTransform;

		[SerializeField]
		private GameObject m_groupPrefab;

		[SerializeField]
		private GameObject m_groupScrollbarPrefab;

		[Space]
		[SerializeField]
		private GridIconTable m_gridIconTable;

		private DMEditor m_dmEditor;

		public OnGridItemSelected onItemSelected = new OnGridItemSelected();

		private Dictionary<string, GridCategory> m_gridCategories = new Dictionary<string, GridCategory>();

		private string m_selectedCategory;

		private int m_categoryIndex;

		private List<CategoryButton> m_categoryButtons = new List<CategoryButton>();

		public PlayerAction closeAction;

		public bool Showing { get; private set; }

		public bool Building { get; private set; }

		private void AssertionCheck()
		{
		}

		protected override void Awake()
		{
			base.Awake();
			AssertionCheck();
			base.transform.position += Vector3.down * 10000f;
		}

		private void Start()
		{
			AssignInput();
		}

		private void AssignInput()
		{
			PlayerActions instance = PlayerActions.Instance;
			m_inputState.AddOnKeyDownListener(instance.m_cycleGridCategoryLeft, delegate
			{
				CycleCategory(right: false);
			});
			m_inputState.AddOnKeyDownListener(instance.m_cycleGridCategoryRight, delegate
			{
				CycleCategory(right: true);
			});
			m_inputState.AddOnKeyDownListener(instance.m_scrollToNextGroup, delegate
			{
				ScrollToGroup(toNextGroup: true);
			});
			m_inputState.AddOnKeyDownListener(instance.m_scrollToPreviousGroup, delegate
			{
				ScrollToGroup(toNextGroup: false);
			});
			m_inputState.AddOnKeyDownListener(closeAction ?? instance.m_closeGrid, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(instance.m_enterExitBattle, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(instance.m_back, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(instance.m_toolPrimary, delegate
			{
			});
		}

		private void Update()
		{
			if (!Building)
			{
				UpdateSelectedGroup();
			}
		}

		public void SetGridData(List<GridItem> gridItems)
		{
			m_gridCategories = GenerateGridCategories(gridItems);
			RebuildUI();
		}

		public void SetGridData(List<GridItem> gridItems, string gridName)
		{
			m_gridNameText.LocaleID = gridName;
			SetGridData(gridItems);
		}

		private void RebuildUI()
		{
			Building = true;
			if (m_dmEditor == null)
			{
				m_dmEditor = DMEditor.Instance;
			}
			DestroyUI();
			BuildCategoryTabs();
			StartCoroutine(BuildGrid());
		}

		private void DestroyUI()
		{
			for (int i = 0; i < m_categoryButtonsTransform.childCount; i++)
			{
				Object.DestroyImmediate(m_categoryButtonsTransform.GetChild(0).gameObject);
			}
			for (int j = 0; j < m_categoryItemsTransform.childCount; j++)
			{
				Object.DestroyImmediate(m_categoryItemsTransform.GetChild(0).gameObject);
			}
			m_categoryButtons.Clear();
		}

		private void BuildCategoryTabs()
		{
			bool flag = true;
			Object.Instantiate(m_categoryCycleLeftButtonPrefab, m_categoryButtonsTransform);
			foreach (KeyValuePair<string, GridCategory> gridCategory in m_gridCategories)
			{
				if (flag)
				{
					m_selectedCategory = gridCategory.Value.CategoryName;
				}
				flag = false;
				CategoryButton categoryButton = Object.Instantiate(m_categoryButtonPrefab, m_categoryButtonsTransform);
				m_categoryButtons.Add(categoryButton);
				GridIconRow rowValue = m_gridIconTable.GetRowValue(gridCategory.Value.CategoryName);
				if (rowValue != null)
				{
					categoryButton.Init(rowValue.Icon, rowValue.GetLocalizedRowName());
				}
				categoryButton.GetComponent<Button>().onClick.AddListener(delegate
				{
					ToggleCategories(categoryButton.transform.GetSiblingIndex() - 1);
				});
				if (gridCategory.Key != m_gridCategories.Last().Key)
				{
					Object.Instantiate(m_categoryButtonDotPrefab, m_categoryButtonsDotsTransform);
				}
			}
			Object.Instantiate(m_categoryCycleRightButtonPrefab, m_categoryButtonsTransform);
		}

		private IEnumerator BuildGrid()
		{
			int num = 0;
			foreach (KeyValuePair<string, GridCategory> gridCategory in m_gridCategories)
			{
				GameObject gameObject = Object.Instantiate(m_categoryItemsPrefab, m_categoryItemsTransform);
				gameObject.SetActive(value: false);
				m_categoryButtonsTransform.GetChild(num).gameObject.SetActive(value: true);
				num++;
				foreach (KeyValuePair<string, GridGroup> group in gridCategory.Value.Groups)
				{
					GameObject gameObject2 = Object.Instantiate(m_groupPrefab, gameObject.transform.GetChild(0));
					GridIconRow rowValue = m_gridIconTable.GetRowValue(group.Value.GroupName);
					if (rowValue != null)
					{
						LocalizeText componentInChildren = gameObject2.GetComponentInChildren<LocalizeText>();
						componentInChildren.LocaleID = rowValue.GetLocalizedRowName();
						Image componentInChildren2 = componentInChildren.GetComponentInChildren<Image>();
						componentInChildren2.sprite = rowValue.Icon;
						componentInChildren2.color = Color.white;
					}
					foreach (KeyValuePair<string, GridItem> item in group.Value.Items)
					{
						GameObject obj = Object.Instantiate(m_gridItemPrefab, gameObject2.transform);
						obj.GetComponentsInChildren<Image>()[1].sprite = item.Value.Icon;
						obj.GetComponentInChildren<LocalizeText>().LocaleID = item.Value.DisplayName;
						obj.GetComponentInChildren<Button>().onClick.AddListener(delegate
						{
							onItemSelected.Invoke(item.Value.Id);
							DMUIManager.Instance.PopPanel();
						});
					}
				}
				PopulateScrollbar(gameObject.GetComponentInChildren<ScrollRect>(), gridCategory.Value);
			}
			Building = false;
			yield break;
		}

		private void PopulateScrollbar(ScrollRect scrollRect, GridCategory category)
		{
			if (scrollRect == null || scrollRect.verticalScrollbar == null)
			{
				return;
			}
			foreach (KeyValuePair<string, GridGroup> group in category.Groups)
			{
				GameObject gameObject = Object.Instantiate(m_groupScrollbarPrefab, scrollRect.verticalScrollbar.transform);
				GridIconRow rowValue = m_gridIconTable.GetRowValue(group.Value.GroupName);
				if (rowValue != null)
				{
					gameObject.GetComponent<LocalizeText>().LocaleID = rowValue.GetLocalizedRowName();
				}
			}
		}

		private void TranslateScrollbarGroups(Transform categoryContent)
		{
			Transform child = categoryContent.parent.GetChild(1);
			RectTransform component = child.GetComponent<RectTransform>();
			RectTransform component2 = categoryContent.GetComponent<RectTransform>();
			LayoutRebuilder.ForceRebuildLayoutImmediate(component2.parent.GetComponent<RectTransform>());
			for (int i = 0; i < categoryContent.childCount; i++)
			{
				Transform child2 = categoryContent.GetChild(i);
				int num = -10;
				float y = Mathf.Lerp(component.rect.yMax, component.rect.yMin, child2.localPosition.y / component2.rect.yMin);
				child.GetChild(i + 1).localPosition = new Vector3(num, y, 0f);
			}
		}

		private void UpdateSelectedGroup()
		{
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			int num = 0;
			if (currentSelectedGameObject != null)
			{
				num = currentSelectedGameObject.transform.parent.GetSiblingIndex();
			}
			Scrollbar componentInChildren = m_categoryItemsTransform.GetChild(m_categoryIndex).GetComponentInChildren<Scrollbar>();
			for (int i = 1; i < componentInChildren.transform.childCount; i++)
			{
				if (PlayerActions.Instance.InputType == InputType.Controller)
				{
					componentInChildren.transform.GetChild(i).gameObject.GetComponent<TMP_Text>().color = ((i == num + 1) ? DMEditorColors.NormalColor : DMEditorColors.MutedColor);
					componentInChildren.transform.GetChild(i).GetChild(1).gameObject.SetActive(i == num + 1);
				}
				else
				{
					componentInChildren.transform.GetChild(i).gameObject.GetComponent<TMP_Text>().color = DMEditorColors.MutedColor;
					componentInChildren.transform.GetChild(i).GetChild(1).gameObject.SetActive(value: false);
				}
			}
		}

		private void ScrollToGroup(bool toNextGroup)
		{
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
				if (currentSelectedGameObject != null && currentSelectedGameObject.transform.parent != null)
				{
					int siblingIndex = currentSelectedGameObject.transform.parent.GetSiblingIndex();
					int index = Utility.PositiveModulo(siblingIndex + (toNextGroup ? 1 : (-1)), currentSelectedGameObject.transform.parent.parent.childCount);
					Transform child = currentSelectedGameObject.transform.parent.parent.GetChild(index).GetChild(2);
					EventSystem.current.SetSelectedGameObject(child.gameObject);
				}
			}
		}

		private void ToggleCategories(int index)
		{
			if (index >= m_categoryItemsTransform.childCount)
			{
				return;
			}
			for (int i = 0; i < m_categoryItemsTransform.childCount; i++)
			{
				if (i < 0 || i >= m_categoryButtons.Count)
				{
					continue;
				}
				CategoryButton categoryButton = m_categoryButtons[i];
				if (!(categoryButton == null))
				{
					m_categoryItemsTransform.GetChild(i).gameObject.SetActive(i == index);
					categoryButton.SetState(i == index);
					if (i == index)
					{
						TranslateScrollbarGroups(m_categoryItemsTransform.GetChild(i).GetComponentInChildren<ScrollRect>().transform.GetChild(0));
					}
				}
			}
			GridCategory currentCategory = GetCurrentCategory();
			if (currentCategory != null && currentCategory.selectedItem != null && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.transform.parent == m_categoryItemsTransform)
			{
				currentCategory.selectedItem = EventSystem.current.currentSelectedGameObject;
			}
			m_categoryIndex = index;
			int num = 0;
			foreach (KeyValuePair<string, GridCategory> gridCategory in m_gridCategories)
			{
				if (num == index)
				{
					m_selectedCategory = gridCategory.Key;
				}
				num++;
			}
			Button button;
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				currentCategory = GetCurrentCategory();
				if (currentCategory != null && currentCategory.selectedItem != null)
				{
					button = currentCategory.selectedItem.GetComponentInChildren<Button>();
				}
				else
				{
					button = m_categoryItemsTransform.GetChild(index).GetComponentInChildren<Button>();
				}
				if (button != null)
				{
					button.Select();
					StartCoroutine(DelayedAnimation());
				}
			}
			IEnumerator DelayedAnimation()
			{
				yield return new WaitForSeconds(0.4f);
				Animator component = button.GetComponent<Animator>();
				if (EventSystem.current.currentSelectedGameObject == button.gameObject && component != null)
				{
					component.SetTrigger(button.animationTriggers.highlightedTrigger);
				}
			}
		}

		public void CycleCategory(bool right)
		{
			m_categoryIndex += (right ? 1 : (-1));
			m_categoryIndex = Utility.PositiveModulo(m_categoryIndex, m_categoryButtons.Count);
			ToggleCategories(m_categoryIndex);
		}

		public override void OnOpen()
		{
			base.OnOpen();
			Showing = true;
			ToggleCategories(m_categoryIndex);
			Utility.PlaySound("UI/Swosh", 1f, base.transform);
		}

		public override void OnClose()
		{
			base.OnClose();
			GetCurrentCategory().selectedItem = EventSystem.current.currentSelectedGameObject;
			EventSystem.current.SetSelectedGameObject(null);
			Utility.PlaySound("UI/Swosh", 1f, base.transform);
			Showing = false;
		}

		public GridCategory GetCurrentCategory()
		{
			m_gridCategories.TryGetValue(m_selectedCategory, out var value);
			return value;
		}

		private Dictionary<string, GridCategory> GenerateGridCategories(List<GridItem> gridItems)
		{
			Dictionary<string, GridCategory> dictionary = new Dictionary<string, GridCategory>();
			foreach (GridItem gridItem in gridItems)
			{
				string[] array = gridItem.Path.TrimStart('/').TrimEnd('/').Replace("_", " ")
					.Split('/');
				if (array[0] == RadialMenu.RadialThemes.Hidden.ToString())
				{
					continue;
				}
				if (array.Length != 3)
				{
					Debug.LogError("Invalid radial path: " + gridItem.Path + "in object: " + gridItem.Id);
					continue;
				}
				string text = array[0];
				dictionary.TryGetValue(text, out var value);
				if (value == null)
				{
					value = new GridCategory
					{
						CategoryName = text
					};
					dictionary.Add(text, value);
				}
				string text2 = array[1];
				value.Groups.TryGetValue(text2, out var value2);
				if (value2 == null)
				{
					value2 = new GridGroup
					{
						GroupName = text2
					};
					value.Groups.Add(text2, value2);
				}
				string key = array[2];
				value2.Items.TryGetValue(key, out var value3);
				if (value3 == null)
				{
					value2.Items.Add(key, gridItem);
				}
			}
			dictionary = dictionary.OrderBy((KeyValuePair<string, GridCategory> o) => o.Value.CategoryName).ToDictionary((KeyValuePair<string, GridCategory> pair) => pair.Key, (KeyValuePair<string, GridCategory> pair) => pair.Value);
			foreach (string item in dictionary.Keys.ToList())
			{
				dictionary[item].Groups = dictionary[item].Groups.OrderBy((KeyValuePair<string, GridGroup> o) => o.Value.GroupName).ToDictionary((KeyValuePair<string, GridGroup> pair) => pair.Key, (KeyValuePair<string, GridGroup> pair) => pair.Value);
			}
			return dictionary;
		}
	}
}
