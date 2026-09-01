using System.Collections.Generic;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.Serialization;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorContentGrid : UIComponentMainMenu
	{
		public enum ContentType
		{
			Clothes = 0,
			WeaponsRight = 1,
			WeaponsLeft = 2,
			SpecialAbility = 3
		}

		public GameObject m_CatagoryPrefab;

		[FormerlySerializedAs("ItemCell")]
		public GameObject ClothingItemCell;

		public GameObject WeaponItemCell;

		public GameObject AbilityItemCell;

		[SerializeField]
		protected Transform contentContainer;

		[SerializeField]
		private NavigableTMPTextInput m_searchField;

		private const string Back = "BUTTON_BACK";

		private CharacterItem[] itemData;

		private ContentType currentContenctType;

		private GameObjectPool<UnitEditorClothingCell> m_propPool;

		private GameObjectPool<UnitEditorWeaponCell> m_weaponPool;

		private GameObjectPool<UnitEditorAbilityCell> m_specialAbilityPool;

		private GameObjectPool<ContentCatagoryUI> m_categoryPool;

		private Dictionary<string, ContentCatagoryUI> m_categoryDictionary = new Dictionary<string, ContentCatagoryUI>();

		private string filter = "";

		private UnitEditorManager m_unitEditorManager;

		private PlayerActions m_playerActions;

		private List<UnitEditorItemCell> currentCells = new List<UnitEditorItemCell>();

		private List<UnitEditorItemCell> filteredItems = new List<UnitEditorItemCell>();

		private static char[] _split = new char[1] { ' ' };

		private ISystemKeyboard keyboard;

		protected override void Awake()
		{
			base.Awake();
			m_unitEditorManager = Object.FindObjectOfType<UnitEditorManager>();
			m_playerActions = PlayerActions.Instance;
			if (contentContainer != null)
			{
				m_propPool = new GameObjectPool<UnitEditorClothingCell>(ClothingItemCell, deactivateOnRelease: true, contentContainer);
				m_weaponPool = new GameObjectPool<UnitEditorWeaponCell>(WeaponItemCell, deactivateOnRelease: true, contentContainer);
				m_specialAbilityPool = new GameObjectPool<UnitEditorAbilityCell>(AbilityItemCell, deactivateOnRelease: true, contentContainer);
				m_categoryPool = new GameObjectPool<ContentCatagoryUI>(m_CatagoryPrefab, deactivateOnRelease: true, contentContainer);
			}
		}

		protected override void OnOpen()
		{
			if (keyboard == null)
			{
				keyboard = ServiceLocator.GetService<SystemKeyboardProvider>().Keyboard;
			}
			if (keyboard != null)
			{
				keyboard.InputCompleted += OnKeyboardInputCompleted;
			}
			base.OnOpen();
			SelectFirstChild(ignoreIfAlreadyHasSelected: true);
			if (stateManager is UnitEditorUIManager unitEditorUIManager)
			{
				UnitEditorGamepadGlyphs gamepadGlyphs = unitEditorUIManager.GamepadGlyphs;
				if (!(gamepadGlyphs == null))
				{
					gamepadGlyphs.UpdateActionNames("Back", "BUTTON_BACK", UnitEditorGamepadGlyphs.Position.Left);
					gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Middle);
					gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Right);
				}
			}
		}

		private void OnKeyboardInputCompleted(string searchString)
		{
			EnterFilter(searchString);
			ApplyFilterOnSubmit();
		}

		protected override void OnClose()
		{
			if (keyboard == null)
			{
				keyboard = ServiceLocator.GetService<SystemKeyboardProvider>().Keyboard;
			}
			if (keyboard != null)
			{
				keyboard.InputCompleted -= OnKeyboardInputCompleted;
			}
			base.OnClose();
		}

		public void PopulateCells(CharacterItem[] characterItems, ContentType contentType, int page = 1)
		{
			currentContenctType = contentType;
			m_propPool.ReleaseAll();
			m_weaponPool.ReleaseAll();
			m_specialAbilityPool.ReleaseAll();
			m_categoryPool.ReleaseAll();
			m_categoryDictionary.Clear();
			currentCells.Clear();
			m_searchField.SetTextNoNotify("");
			itemData = characterItems;
			for (int i = 0; i < itemData.Length; i++)
			{
				CharacterItem characterItem = itemData[i];
				CharacterItem.Tag tag;
				string text = (characterItem.GetTag(CharacterItem.TagType.Faction, out tag) ? tag.value : "MISSING FACTION");
				if (!m_categoryDictionary.ContainsKey(text))
				{
					ContentCatagoryUI contentCatagoryUI = SpawnCatagory(text);
					m_categoryDictionary.Add(text, contentCatagoryUI);
					contentCatagoryUI.transform.SetAsLastSibling();
					SpawnCell(contentCatagoryUI, contentType, characterItem, m_unitEditorManager);
				}
				else
				{
					ContentCatagoryUI root = m_categoryDictionary[text];
					SpawnCell(root, contentType, characterItem, m_unitEditorManager);
				}
			}
			if (currentCells != null && currentCells.Count > 0)
			{
				currentCells[0].Select();
			}
		}

		private ContentCatagoryUI SpawnCatagory(string category)
		{
			ContentCatagoryUI contentCatagoryUI = m_categoryPool.GetObject();
			if (contentCatagoryUI != null)
			{
				contentCatagoryUI.Initialize(category);
				return contentCatagoryUI;
			}
			return null;
		}

		private void SpawnCell(ContentCatagoryUI root, ContentType contentType, CharacterItem data, UnitEditorManager manager)
		{
			UnitEditorItemCell unitEditorItemCell = null;
			switch (contentType)
			{
			case ContentType.Clothes:
				unitEditorItemCell = m_propPool.GetObject();
				break;
			case ContentType.WeaponsRight:
				unitEditorItemCell = m_weaponPool.GetObject();
				if (unitEditorItemCell is UnitEditorWeaponCell unitEditorWeaponCell2)
				{
					unitEditorWeaponCell2.SetRight(isRight: true);
				}
				break;
			case ContentType.WeaponsLeft:
				unitEditorItemCell = m_weaponPool.GetObject();
				if (unitEditorItemCell is UnitEditorWeaponCell unitEditorWeaponCell)
				{
					unitEditorWeaponCell.SetRight(isRight: false);
				}
				break;
			case ContentType.SpecialAbility:
				unitEditorItemCell = m_specialAbilityPool.GetObject();
				break;
			}
			if (unitEditorItemCell != null)
			{
				unitEditorItemCell.Initialize(data, manager);
				unitEditorItemCell.transform.SetParent(root.transform);
				unitEditorItemCell.transform.SetSiblingIndex(root.GetChildIndex());
				currentCells.Add(unitEditorItemCell);
			}
		}

		public void EnterFilter(string filter)
		{
			this.filter = filter;
			ApplyFilter();
		}

		protected override void Update()
		{
			base.Update();
			if (base.IsActive && m_searchField != null && m_playerActions.m_itemSelectSearch.WasPressed)
			{
				if (m_searchField.IsTextInputEnabled)
				{
					m_searchField.DisableTextInput();
					ApplyFilterOnSubmit();
				}
				else
				{
					m_searchField.EnableTextInput();
				}
			}
		}

		public void ApplyFilter()
		{
			filteredItems.Clear();
			filteredItems.AddRange(m_propPool.UsedObjects);
			filteredItems.AddRange(m_weaponPool.UsedObjects);
			filteredItems.AddRange(m_specialAbilityPool.UsedObjects);
			filteredItems.ApplyFilter(filter);
			foreach (ContentCatagoryUI usedObject in m_categoryPool.UsedObjects)
			{
				bool flag = usedObject.GetComponentInChildren<UnitEditorItemCell>() == null;
				usedObject.gameObject.SetActive(!flag);
			}
		}

		public void ApplyFilterOnSubmit()
		{
			ApplyFilter();
			SelectFirstEnabledFilteredItem();
		}

		private void SelectFirstEnabledFilteredItem()
		{
			if (filteredItems == null || filteredItems.Count <= 0)
			{
				return;
			}
			foreach (UnitEditorItemCell filteredItem in filteredItems)
			{
				if (filteredItem.gameObject.activeSelf)
				{
					filteredItem.Select();
					break;
				}
			}
		}
	}
}
