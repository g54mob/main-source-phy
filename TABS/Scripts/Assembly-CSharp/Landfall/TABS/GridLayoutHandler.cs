using System;
using System.Collections.Generic;
using Landfall.TABS.Workshop;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class GridLayoutHandler : MonoBehaviour
	{
		public struct GridDataWrapper
		{
			public object obj;

			public BattleCreatorAssetUICellBase.CellType cellType;

			public GridDataWrapper(object obj, BattleCreatorAssetUICellBase.CellType cellType)
			{
				this.obj = obj;
				this.cellType = cellType;
			}
		}

		[SerializeField]
		private GameObject m_GridCell;

		[SerializeField]
		private GameObject m_SaveGridCell;

		[SerializeField]
		private int m_rows = 3;

		[SerializeField]
		private int m_collums = 4;

		[SerializeField]
		private Button LeftButton;

		[SerializeField]
		private Button SmallLeftButton;

		[SerializeField]
		private Button RightButton;

		[SerializeField]
		private Button SmallRightButton;

		[SerializeField]
		private float buttonSwapAspectRatioThreshold = 1.7f;

		private bool usingSquareAspect;

		private int pageIndex;

		private int m_pageCount;

		private Dictionary<int, GridDataWrapper[]> pageDicionary;

		private Button[,] gridButtons;

		private bool addSaveButton;

		private SaveButtonInfo saveButtonInfo;

		public event Action OnGridItemsFinishedSpawning;

		private void Awake()
		{
			InitListeners();
		}

		private void OnEnable()
		{
			UpdateButtonPageBounds();
		}

		private void InitListeners()
		{
			if (RightButton != null)
			{
				RightButton.onClick.AddListener(PageRight);
			}
			if (SmallRightButton != null)
			{
				SmallRightButton.onClick.AddListener(PageRight);
			}
			if (LeftButton != null)
			{
				LeftButton.onClick.AddListener(PageLeft);
			}
			if (SmallLeftButton != null)
			{
				SmallLeftButton.onClick.AddListener(PageLeft);
			}
		}

		private void UpdateButtonPageBounds()
		{
			bool flag = pageIndex != 0;
			bool flag2 = pageIndex < m_pageCount - 1;
			usingSquareAspect = ScreenHelpers.GetAspectRatio() < buttonSwapAspectRatioThreshold;
			if (LeftButton != null)
			{
				LeftButton.gameObject.SetActive(!usingSquareAspect && flag);
			}
			if (SmallLeftButton != null)
			{
				SmallLeftButton.gameObject.SetActive(usingSquareAspect && flag);
			}
			if (RightButton != null)
			{
				RightButton.gameObject.SetActive(!usingSquareAspect && flag2);
			}
			if (SmallRightButton != null)
			{
				SmallRightButton.gameObject.SetActive(usingSquareAspect && flag2);
			}
		}

		private float GetAspectRatio()
		{
			return (float)Screen.width / (float)Screen.height;
		}

		public void Feed(GridDataWrapper[] data, bool withSaveButton = false, SaveButtonInfo saveButtonInfo = default(SaveButtonInfo))
		{
			gridButtons = new Button[m_rows, m_collums];
			addSaveButton = withSaveButton;
			this.saveButtonInfo = saveButtonInfo;
			int num = data.Length;
			num = (addSaveButton ? (num + 1) : num);
			int num2 = m_rows * m_collums;
			m_pageCount = Mathf.CeilToInt((float)num / (float)num2);
			if (m_pageCount == 0)
			{
				m_pageCount = 1;
			}
			pageDicionary = new Dictionary<int, GridDataWrapper[]>();
			int num3 = 0;
			for (int i = 0; i < m_pageCount; i++)
			{
				int num4 = ((i == 0 && addSaveButton) ? (num2 - 1) : num2);
				num3 = (addSaveButton ? (i * num2 - 1) : (i * num2));
				num3 = Mathf.Clamp(num3, 0, num2 * m_pageCount);
				List<GridDataWrapper> list = new List<GridDataWrapper>();
				for (int j = num3; j < num3 + num4 && j < data.Length; j++)
				{
					list.Add(data[j]);
				}
				pageDicionary.Add(i, list.ToArray());
			}
			pageIndex = 0;
			Populate(pageIndex);
			UpdateButtonPageBounds();
		}

		public void PageRight()
		{
			pageIndex++;
			if (pageIndex >= m_pageCount)
			{
				pageIndex = m_pageCount - 1;
			}
			else
			{
				Populate(pageIndex);
			}
			UpdateButtonPageBounds();
		}

		public void PageLeft()
		{
			pageIndex--;
			if (pageIndex < 0)
			{
				pageIndex = 0;
			}
			else
			{
				Populate(pageIndex);
			}
			UpdateButtonPageBounds();
		}

		private void Populate(int page = 0)
		{
			Clear();
			SpawnPopulate(page);
		}

		private void SpawnPopulate(int page)
		{
			GridDataWrapper[] array = pageDicionary[page];
			int num = 0;
			int num2 = 0;
			if (array.Length == 0 && addSaveButton)
			{
				AddSaveCellToGrid(num, num2);
			}
			for (int i = 0; i < array.Length; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(m_GridCell);
				object obj = array[i].obj;
				BattleCreatorAssetUICellBase component = gameObject.GetComponent<BattleCreatorAssetUICellBase>();
				switch (array[i].cellType)
				{
				case BattleCreatorAssetUICellBase.CellType.LevelContent:
					component.Init((BattleCreatorAssetUICellBase.CampaignLevelData)obj);
					break;
				case BattleCreatorAssetUICellBase.CellType.CampaignContent:
					component.Init((BattleCreatorAssetUICellBase.CampaignData)obj);
					break;
				case BattleCreatorAssetUICellBase.CellType.UnitContent:
					component.Init((BattleCreatorAssetUICellBase.UnitData)obj);
					break;
				case BattleCreatorAssetUICellBase.CellType.UpdateContent:
					component.Init((BattleCreatorAssetUICellBase.UpdateContentData)obj);
					break;
				}
				if (page == 0 && i == 0 && addSaveButton)
				{
					AddSaveCellToGrid(num, num2);
					num2++;
				}
				AddCellToGrid(gameObject, num, num2);
				num2++;
				if (num2 == m_collums)
				{
					num++;
					num2 = 0;
				}
			}
			int numButtons = (addSaveButton ? (array.Length + 1) : array.Length);
			SetUpNavigation(gridButtons, numButtons);
			this.OnGridItemsFinishedSpawning?.Invoke();
		}

		private void AddSaveCellToGrid(int row, int col)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(m_SaveGridCell);
			gameObject.GetComponent<ButtonWithText>().InitLocalized(saveButtonInfo.Title, saveButtonInfo.OnPressAction);
			AddCellToGrid(gameObject, row, col);
		}

		private void AddCellToGrid(GameObject cell, int row, int col)
		{
			cell.transform.SetParent(base.transform, worldPositionStays: true);
			cell.transform.localScale = Vector3.one;
			cell.SetActive(value: true);
			gridButtons[row, col] = cell.GetComponent<Button>();
		}

		private void SetUpNavigation(Button[,] gridButtons, int numButtons)
		{
			Navigation navigation = default(Navigation);
			int num = 0;
			for (int i = 0; i < m_rows; i++)
			{
				if (num >= numButtons)
				{
					break;
				}
				for (int j = 0; j < m_collums; j++)
				{
					if (num >= numButtons)
					{
						break;
					}
					Button button = gridButtons[i, j];
					navigation = button.navigation;
					navigation.mode = Navigation.Mode.Explicit;
					navigation.selectOnUp = CheckGridButtonDirection(i, j, gridButtons, -1);
					navigation.selectOnDown = CheckGridButtonDirection(i, j, gridButtons, 1);
					navigation.selectOnLeft = CheckGridButtonDirection(i, j, gridButtons, 0, -1);
					navigation.selectOnRight = CheckGridButtonDirection(i, j, gridButtons, 0, 1);
					button.navigation = navigation;
					num++;
				}
			}
		}

		private Button CheckGridButtonDirection(int row, int col, Button[,] gridButtons, int vertical = 0, int horizontal = 0)
		{
			if (vertical != 0)
			{
				int num = row + vertical;
				for (int i = 0; i < m_rows; i++)
				{
					int num2 = ((num < 0) ? (m_rows + num) : num);
					num2 = ((num >= m_rows) ? (num % m_rows) : num2);
					Button button = gridButtons[num2, col];
					if (button != null)
					{
						return button;
					}
					num += vertical;
				}
			}
			if (horizontal != 0)
			{
				int num3 = col + horizontal;
				for (int j = 0; j < m_collums; j++)
				{
					int num4 = ((num3 < 0) ? (m_collums + num3) : num3);
					num4 = ((num3 >= m_collums) ? (num3 % m_collums) : num4);
					Button button2 = gridButtons[row, num4];
					if (button2 != null)
					{
						return button2;
					}
					num3 += horizontal;
				}
			}
			return null;
		}

		private void Clear()
		{
			StopAllCoroutines();
			for (int num = base.transform.childCount - 1; num >= 0; num--)
			{
				UnityEngine.Object.Destroy(base.transform.GetChild(num).gameObject);
			}
		}

		private BattleCreatorGridButtonUI GetSelectedButton()
		{
			Button[,] array = gridButtons;
			foreach (Button button in array)
			{
				if (button != null && button.gameObject == EventSystem.current.currentSelectedGameObject)
				{
					return button.GetComponent<BattleCreatorGridButtonUI>();
				}
			}
			return null;
		}

		public void SelectFirstButton()
		{
			if (gridButtons != null)
			{
				Button button = gridButtons[0, 0];
				if (button != null)
				{
					button.Select();
				}
			}
		}

		public void PressCogOfSelectedButton()
		{
			BattleCreatorGridButtonUI selectedButton = GetSelectedButton();
			if (selectedButton != null)
			{
				selectedButton.PressCog();
			}
		}

		public void PressContextofSelectedButton()
		{
			BattleCreatorGridButtonUI selectedButton = GetSelectedButton();
			if (selectedButton != null)
			{
				selectedButton.PressContext();
			}
		}

		public void PressUploadOfSelectedButton()
		{
			BattleCreatorGridButtonUI selectedButton = GetSelectedButton();
			if (selectedButton != null)
			{
				selectedButton.PressUpload();
			}
		}

		public void PressDeleteOfSelectedButton()
		{
			BattleCreatorGridButtonUI selectedButton = GetSelectedButton();
			if (selectedButton != null)
			{
				selectedButton.PressDelete();
			}
		}

		public void PressSelectedButton()
		{
			BattleCreatorGridButtonUI selectedButton = GetSelectedButton();
			if (selectedButton != null)
			{
				selectedButton.PressButton();
			}
		}

		public void PressLoadOfSelectedButton()
		{
			BattleCreatorGridButtonUI selectedButton = GetSelectedButton();
			if (selectedButton != null)
			{
				selectedButton.PressLoad();
			}
		}
	}
}
