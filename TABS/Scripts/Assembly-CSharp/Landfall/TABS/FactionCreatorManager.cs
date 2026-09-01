using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DM;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TFBGames;
using UIStateManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class FactionCreatorManager : MonoBehaviour
	{
		private struct SpawnedWrapper
		{
			public UnitBlueprint unit;

			public UnitButtonBase spawnedButton;

			public SpawnedWrapper(UnitBlueprint unit, UnitButtonBase spawnedButton)
			{
				this.unit = unit;
				this.spawnedButton = spawnedButton;
			}
		}

		private const int FramesToWait = 5;

		public InterfaceStateManager interfaceManager;

		public FactionCreatorIconBrowser m_IconBrowser;

		public FactionCreatorColorSelect m_ColorSelector;

		public CustomContetnManager m_customContentManager;

		public NavigableTMPTextInput factionNameText;

		private GameObject previousSelection;

		public GameObject UnitButtonPrefab;

		public Transform FactionListParent;

		public FactionIcon defaultFactionIcon;

		public Image iconImage;

		public FactionCreatorUnitBrowser factionCreatorUnitBrowser;

		public GameObject addGlyph;

		public GameObject removeGlyph;

		private List<SpawnedWrapper> addedUnits = new List<SpawnedWrapper>();

		private FactionIcon FactionIcon;

		private CustomFactionColorDatabase.CustomFactionColor CustomFactionColor;

		private Faction loadedFaction;

		private CanvasGroup canvasGroup;

		private ModalPanel modalPanel;

		private int savingModalPanelOpenId;

		[Space(10f)]
		public Image[] m_objectsToColor = new Image[0];

		public bool isOpen
		{
			get
			{
				if (canvasGroup != null)
				{
					return canvasGroup.interactable;
				}
				return false;
			}
		}

		public event Action SavingFactionStarted;

		public event Action SavingFactionCompleted;

		private void Start()
		{
			modalPanel = ServiceLocator.GetService<ModalPanel>();
		}

		public void UpdateAddGlyph(UnitButtonBase unitButton)
		{
			int index;
			bool flag = !UnitAlreadySelected(unitButton.GetUnit(), out index);
			addGlyph.SetActive(flag);
			removeGlyph.SetActive(!flag);
		}

		public void ToggleNameInput()
		{
			if (factionNameText.IsTextInputEnabled)
			{
				DisableNameInput();
			}
			else
			{
				EnableNameInput();
			}
		}

		private void EnableNameInput()
		{
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			previousSelection = currentSelectedGameObject;
			factionNameText.EnableTextInput();
		}

		private void DisableNameInput()
		{
			factionNameText.DisableTextInput();
			if (previousSelection != null)
			{
				EventSystem.current.SetSelectedGameObject(previousSelection);
			}
		}

		public void OnClickUnit(FactionCreatorUnitBrowserUnitButton unitButton)
		{
			UnitBlueprint unit = unitButton.GetUnit();
			if (UnitAlreadySelected(unit, out var index))
			{
				addGlyph.SetActive(value: true);
				removeGlyph.SetActive(value: false);
				RemoveUnit(index);
			}
			else
			{
				addGlyph.SetActive(value: false);
				removeGlyph.SetActive(value: true);
				AddNewUnit(unit);
			}
		}

		private void AddNewUnit(UnitBlueprint unit)
		{
			UnitButtonBase spawnedButton = UnityEngine.Object.Instantiate(UnitButtonPrefab, FactionListParent).GetComponent<UnitButtonBase>().Setup(unit);
			addedUnits.Add(new SpawnedWrapper(unit, spawnedButton));
			SetFactionBrowserState(unit, v: true);
		}

		private void RemoveUnit(int index)
		{
			SpawnedWrapper spawnedWrapper = addedUnits[index];
			UnityEngine.Object.Destroy(spawnedWrapper.spawnedButton.gameObject);
			addedUnits.RemoveAt(index);
			SetFactionBrowserState(spawnedWrapper.unit, v: false);
		}

		public void UnSelectUnit(UnitBlueprint unit)
		{
			int num = -1;
			for (int i = 0; i < addedUnits.Count; i++)
			{
				if (addedUnits[i].unit == unit)
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				RemoveUnit(num);
			}
		}

		private void SetFactionBrowserState(UnitBlueprint unit, bool v)
		{
			factionCreatorUnitBrowser.SetButtonState(unit, v);
		}

		public Color GetFactionColor()
		{
			return CustomFactionColor.m_Color;
		}

		public bool UnitAlreadySelected(UnitBlueprint unit, out int index)
		{
			for (int i = 0; i < addedUnits.Count; i++)
			{
				if (addedUnits[i].unit.Entity.GUID == unit.Entity.GUID)
				{
					index = i;
					return true;
				}
			}
			index = 0;
			return false;
		}

		public UnitBlueprint[] GetFactionUnits()
		{
			UnitBlueprint[] array = new UnitBlueprint[addedUnits.Count];
			for (int i = 0; i < addedUnits.Count; i++)
			{
				array[i] = addedUnits[i].unit;
			}
			return array;
		}

		public void SelectIcon(FactionIcon icon)
		{
			FactionIcon = icon;
			icon.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && iconImage != null)
				{
					iconImage.sprite = sprite;
				}
			});
		}

		public void SelectColor(CustomFactionColorDatabase.CustomFactionColor m_color)
		{
			CustomFactionColor = m_color;
			for (int i = 0; i < m_objectsToColor.Length; i++)
			{
				m_objectsToColor[i].color = m_color.m_Color;
			}
			factionCreatorUnitBrowser.ApplyNewColor();
		}

		public Faction GetFaction()
		{
			string text = factionNameText.text;
			UnitBlueprint[] factionUnits = GetFactionUnits();
			Faction faction = new Faction();
			faction.Init();
			faction.Entity.GenerateNewID();
			faction.Entity.Name = text;
			faction.FactionIcon = FactionIcon;
			faction.CustomFactionColor = CustomFactionColor;
			faction.Units = factionUnits;
			faction.IsCustom = true;
			return faction;
		}

		public void Init()
		{
			canvasGroup = GetComponentInParent<CanvasGroup>();
			loadedFaction = null;
			Clear();
		}

		public void LoadFaction(Faction faction)
		{
			Clear();
			for (int i = 0; i < faction.Units.Length; i++)
			{
				UnitBlueprint unit = faction.Units[i];
				AddNewUnit(unit);
			}
			factionNameText.SetTextNoNotify(faction.Entity.Name);
			SelectIcon(faction.FactionIcon);
			SelectColor(faction.CustomFactionColor);
			loadedFaction = faction;
		}

		private void Clear()
		{
			Debug.Log("Clearing faction!");
			UnityEngine.Object.FindObjectOfType<UnitCreatorFactionBrowser>().customContentSideBar.CloseFactionPreview();
			factionNameText.SetTextNoNotify("");
			SelectIcon(defaultFactionIcon);
			SelectColor(ContentDatabase.Instance().GetCustomFactionColorDatabase().CustomFacionColors[0]);
			for (int num = addedUnits.Count - 1; num >= 0; num--)
			{
				RemoveUnit(num);
			}
		}

		private bool ValidateFaction(Faction faction)
		{
			ModalPanel service = ServiceLocator.GetService<ModalPanel>();
			if (string.IsNullOrEmpty(faction.Entity.Name))
			{
				service.PopUp("POPUP_EMPTYNAME");
				return false;
			}
			if (faction.Units == null || faction.Units.Length == 0)
			{
				service.PopUp("POPUP_NOUNITSADDED");
				return false;
			}
			return true;
		}

		private void ShowSavingPopup()
		{
			savingModalPanelOpenId = modalPanel.WaitPopUpWithFocus("POPUP_SAVING", -1f, null, null, true);
		}

		private async Task WaitForFrames(int frames)
		{
			for (int i = 0; i < frames; i++)
			{
				await Task.Yield();
			}
		}

		private async void HideSavingPopup()
		{
			await WaitForFrames(5);
			if (savingModalPanelOpenId == modalPanel.OpenId)
			{
				modalPanel.CloseWaitPopup();
			}
		}

		public async void SaveNewFaction()
		{
			ShowSavingPopup();
			await WaitForFrames(5);
			Faction faction = GetFaction();
			if (ValidateFaction(faction))
			{
				this.SavingFactionStarted?.Invoke();
				if ((bool)loadedFaction)
				{
					CustomFactionHandler.SaveFaction(faction, loadedFaction.Entity.GUID, OnSavingFactionDone);
				}
				else
				{
					CustomFactionHandler.SaveFaction(faction, default(DatabaseID), OnSavingFactionDone);
				}
			}
			else
			{
				HideSavingPopup();
				interfaceManager.OpenUIComponent(GetComponentInParent<UIComponentMainMenu>());
			}
		}

		private void OnSavingFactionDone()
		{
			HideSavingPopup();
			RefreshFactionBrowser();
			this.SavingFactionCompleted?.Invoke();
		}

		private void RefreshFactionBrowser()
		{
			UnitCreatorFactionBrowser unitCreatorFactionBrowser = UnityEngine.Object.FindObjectOfType<UnitCreatorFactionBrowser>();
			if (unitCreatorFactionBrowser != null)
			{
				unitCreatorFactionBrowser.QuickRefresh(WorkshopContentType.Faction);
				unitCreatorFactionBrowser.FocusSelection();
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.PageUp))
			{
				Clear();
			}
			if (canvasGroup != null && canvasGroup.interactable && PlayerActions.Instance.m_editUnitName.WasPressed)
			{
				ToggleNameInput();
			}
		}

		public void GoToIconPage()
		{
			m_IconBrowser.SetupFactionBanner(FactionIcon, factionNameText.text, GetFactionColor());
			m_customContentManager.NavigateToPage("ICONSELECT");
		}

		public void GoToColorSelectPage()
		{
			m_ColorSelector.SetupFactionBanner(FactionIcon, factionNameText.text, GetFactionColor());
			m_customContentManager.NavigateToPage("COLORSELECT");
		}
	}
}
