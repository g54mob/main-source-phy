using System.Collections;
using System.Collections.Generic;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorListSelectScreen : UIComponentMainMenu
	{
		private enum ListType
		{
			VoiceBundles = 0,
			Projectiles = 1,
			Units = 2
		}

		public Transform contentParent;

		public GameObject prefab;

		public GameObject projectilePrefab;

		public GameObject unitPrefab;

		public LocalizeText header;

		public NavigableTMPTextInput searchField;

		private List<UnitEditorSelectableListItem> spawnedItems = new List<UnitEditorSelectableListItem>();

		private UnitEditorManager.EquipedWeaponWrapper weaponToGiveProjectile;

		private string voiceBundleHeader = "LABEL_SELECTVOICE";

		private string projectileHeader = "LABEL_SELECTPROJECTILE";

		private string unitHeader = "LABEL_SELECTRIDER";

		private string voiceBundleBackMenu = "LABEL_UNIT";

		private string projectileBackMenu = "LABEL_EQUIPEDCLOTHING";

		private string unitBackMenu = "LABEL_UNIT";

		private const string BackText = "BUTTON_BACK";

		private const string Play = "BUTTON_PLAY";

		private static readonly int[] ExcludeProjectileGUIds = new int[2] { 774409235, 1312399695 };

		private ListType currentListType;

		private PlayerActions playerActions;

		private ISystemKeyboard keyboard;

		protected override void Awake()
		{
			base.Awake();
			playerActions = PlayerActions.Instance;
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

		private void OnKeyboardInputCompleted(string searchString)
		{
			Search(searchString);
		}

		protected override void Update()
		{
			base.Update();
			if (base.IsActive && searchField != null && playerActions.m_itemSelectSearch.WasPressed)
			{
				if (searchField.IsTextInputEnabled)
				{
					searchField.DisableTextInput();
					SelectFirstEnabledFilteredItem();
				}
				else
				{
					searchField.EnableTextInput();
				}
			}
		}

		public void Setup(VoiceBundle[] voiceBundles)
		{
			ClearList();
			SetUI(ListType.VoiceBundles);
			for (int i = 0; i < voiceBundles.Length; i++)
			{
				UnitEditorVoiceSelectCell component = Object.Instantiate(prefab, contentParent).GetComponent<UnitEditorVoiceSelectCell>();
				if (component != null)
				{
					component.Init(voiceBundles[i]);
					spawnedItems.Add(component);
				}
			}
			SetupNavigation();
		}

		public void Setup(ProjectileEntity[] projectiles)
		{
			ClearList();
			SetUI(ListType.Projectiles);
			for (int i = 0; i < projectiles.Length; i++)
			{
				UnitEditorProjectileSelectCell component = Object.Instantiate(projectilePrefab, contentParent).GetComponent<UnitEditorProjectileSelectCell>();
				if (component != null)
				{
					ProjectileEntity projectileEntity = projectiles[i];
					if (!CheckEntityExcluded(projectileEntity.Entity.GUID.m_ID, ExcludeProjectileGUIds))
					{
						component.Init(projectileEntity, weaponToGiveProjectile);
						spawnedItems.Add(component);
					}
				}
			}
			SetupNavigation();
		}

		private bool CheckEntityExcluded(int m_id, IEnumerable<int> exclusionIds)
		{
			foreach (int exclusionId in exclusionIds)
			{
				if (m_id == exclusionId)
				{
					return true;
				}
			}
			return false;
		}

		public IEnumerator Setup(UnitBlueprint[] u)
		{
			ClearList();
			SetUI(ListType.Units);
			foreach (UnitBlueprint unit in u)
			{
				UnitEditorRiderSelectCell component = Object.Instantiate(unitPrefab, contentParent).GetComponent<UnitEditorRiderSelectCell>();
				if (component != null)
				{
					component.Setup(unit);
					spawnedItems.Add(component);
				}
				yield return null;
			}
			SetupNavigation();
		}

		private void SetupNavigation()
		{
			UIHelpers.CreateExplicitLinearNavigation(contentParent.GetSelectableChildren(), horizontal: false);
			foreach (UnitEditorSelectableListItem spawnedItem in spawnedItems)
			{
				if (spawnedItem.gameObject.activeSelf && spawnedItem.interactable)
				{
					spawnedItem.Select();
					break;
				}
			}
		}

		private void SetUI(ListType type)
		{
			currentListType = type;
			if (stateManager is UnitEditorUIManager unitEditorUIManager)
			{
				UnitEditorGamepadGlyphs gamepadGlyphs = unitEditorUIManager.GamepadGlyphs;
				gamepadGlyphs.UpdateActionNames("Back", "BUTTON_BACK", UnitEditorGamepadGlyphs.Position.Left);
				gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Middle);
				gamepadGlyphs.UpdateActionNames(string.Empty, string.Empty, UnitEditorGamepadGlyphs.Position.Right);
				switch (currentListType)
				{
				case ListType.VoiceBundles:
					header.LocaleID = voiceBundleHeader;
					gamepadGlyphs.UpdateActionNames("Preview Unit Voice", "BUTTON_PLAY", UnitEditorGamepadGlyphs.Position.Middle);
					break;
				case ListType.Projectiles:
					header.LocaleID = projectileHeader;
					break;
				case ListType.Units:
					header.LocaleID = unitHeader;
					break;
				}
			}
		}

		private void ClearList()
		{
			if (searchField != null)
			{
				searchField.SetTextNoNotify(string.Empty);
			}
			if (spawnedItems != null)
			{
				for (int i = 0; i < spawnedItems.Count; i++)
				{
					Object.Destroy(spawnedItems[i].gameObject);
				}
			}
			spawnedItems = new List<UnitEditorSelectableListItem>();
		}

		public void SetWeapon(UnitEditorManager.EquipedWeaponWrapper weapon)
		{
			weaponToGiveProjectile = weapon;
		}

		public void Back()
		{
			UnitEditorUIManager unitEditorUIManager = Object.FindObjectOfType<UnitEditorUIManager>();
			switch (currentListType)
			{
			case ListType.VoiceBundles:
				unitEditorUIManager.NavigateToPage(voiceBundleBackMenu);
				break;
			case ListType.Projectiles:
				unitEditorUIManager.NavigateToPage(projectileBackMenu);
				break;
			case ListType.Units:
				unitEditorUIManager.NavigateToPage(unitBackMenu);
				break;
			}
		}

		public void Search(string filter)
		{
			for (int i = 0; i < spawnedItems.Count; i++)
			{
				bool active = spawnedItems[i].ValidInFilter(filter);
				spawnedItems[i].gameObject.SetActive(active);
			}
		}

		private void SelectFirstEnabledFilteredItem()
		{
			if (spawnedItems == null || spawnedItems.Count <= 0)
			{
				return;
			}
			SetupNavigation();
			foreach (UnitEditorSelectableListItem spawnedItem in spawnedItems)
			{
				if (spawnedItem.gameObject.activeSelf && spawnedItem.gameObject.activeInHierarchy)
				{
					spawnedItem.Select();
					break;
				}
			}
		}
	}
}
