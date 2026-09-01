using System;
using System.Collections.Generic;
using System.Linq;
using BitCode.UI;
using DM;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.UnitPlacement;
using ModIO;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
	public class BattleRadialMenu : RadialMenu<BattleRadialButton, IDatabaseEntity>
	{
		private const float SoundPlayOffset = 25f;

		private Canvas canvas;

		private CanvasGroup canvasGroup;

		private SimpleStateAnimation animation;

		private List<IDatabaseEntity> factionCache = new List<IDatabaseEntity>();

		private List<IDatabaseEntity> addFactionsFactionCache = new List<IDatabaseEntity>();

		private Dictionary<Faction, List<IDatabaseEntity>> factionUnitDictionary = new Dictionary<Faction, List<IDatabaseEntity>>();

		private bool isSelectingUnits;

		private UnitBlueprint lastValidUnit;

		private UnitPlacementBrush placementBrush;

		private ISaveLoaderService saveLoader;

		private SoundPlayer soundPlayer;

		private Transform mainCamTransform;

		private GameModeService gameModeService;

		private Faction lastSelectedFaction;

		[SerializeField]
		[Header("The Index of the first selected item in the menu.")]
		protected int selectedIndex;

		[SerializeField]
		protected UnitInfoBar unitInfoBar;

		[SerializeField]
		protected LocalizeText factionNameText;

		[SerializeField]
		protected Image factionIcon;

		[SerializeField]
		protected Transform backgroundTransform;

		[SerializeField]
		protected Transform menuItemsTransform;

		[SerializeField]
		protected Transform arrowTransform;

		[SerializeField]
		[Tooltip("An extra offset amount to keep the radial menu on screen.")]
		protected float screenEdgePadding = 20f;

		[SerializeField]
		[Tooltip("Time to prevent unit placement after the menu closes.")]
		protected float blockPlacementTime = 0.5f;

		[SerializeField]
		protected GameObject editFactionsButtonPromptGameObject;

		[SerializeField]
		protected Faction addFactionEntity;

		[SerializeField]
		protected BattleRadialMenuEditFactionPromptSetUp radialMenuEditButtons;

		[SerializeField]
		protected string addFactionText = "BUTTON_ADD";

		[SerializeField]
		protected string removeFactionText = "BUTTON_REMOVE";

		[SerializeField]
		private Faction HalloweenFaction;

		[SerializeField]
		private UnitBlueprint[] HalloweenFactionUnits;

		private bool canUseCustomFactions;

		private RadialMenuButtonInteractionType editingState = RadialMenuButtonInteractionType.None;

		private readonly BattleRadialButtonIconManager buttonIconManager = new BattleRadialButtonIconManager();

		public bool IsOpen { get; private set; }

		public bool IsSelectingUnits => isSelectingUnits;

		public event Action RadialMenuOpened;

		public event Action RadialMenuClosed;

		public event Action<UnitBlueprint> UnitSelected;

		protected override void Awake()
		{
			base.Awake();
			canvas = GetComponent<Canvas>();
			canvasGroup = GetComponent<CanvasGroup>();
			animation = GetComponent<SimpleStateAnimation>();
			saveLoader = ServiceLocator.GetService<ISaveLoaderService>();
			soundPlayer = ServiceLocator.GetService<SoundPlayer>();
			if (radialMenuEditButtons == null)
			{
				radialMenuEditButtons = GetComponentInChildren<BattleRadialMenuEditFactionPromptSetUp>();
			}
			if (animation != null)
			{
				animation.Completed += OnAnimationComplete;
			}
			gameModeService = ServiceLocator.GetService<GameModeService>();
			if (gameModeService != null)
			{
				placementBrush = gameModeService.CurrentGameMode?.Brush;
				if (placementBrush != null)
				{
					placementBrush.SelectedUnitChanged += OnSelectedUnitChanged;
				}
			}
			canUseCustomFactions = CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Sandbox || CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.LocalMultiplayer;
			unitInfoBar.SetShouldDestroyIconFunction(ShouldDestroyIconTexture);
			BuildCaches();
			Close();
			ShowFactions();
			editFactionsButtonPromptGameObject.SetActive(canUseCustomFactions);
		}

		private new void Update()
		{
			if (IsOpen)
			{
				base.Update();
			}
		}

		private void RadialMenuClosedResetEditingState()
		{
			switch (editingState)
			{
			case RadialMenuButtonInteractionType.Removing:
			case RadialMenuButtonInteractionType.Adding:
				StopEditingFactions();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			case RadialMenuButtonInteractionType.None:
				break;
			}
		}

		private void Start()
		{
			BuildCaches();
			MainCam mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(Player.One);
			mainCamTransform = ((mainCam != null) ? mainCam.transform : null);
		}

		public void EditRadialMenuPressed()
		{
			if (canUseCustomFactions && IsOpen)
			{
				switch (editingState)
				{
				case RadialMenuButtonInteractionType.None:
				case RadialMenuButtonInteractionType.Adding:
					StartEditingFactions();
					break;
				case RadialMenuButtonInteractionType.Removing:
					StopEditingFactions();
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		private void StartEditingFactions()
		{
			editingState = RadialMenuButtonInteractionType.Removing;
			AddFactionEntityFromFactionCache(addFactionEntity);
			PopulateFactionsMenu(factionCache);
			radialMenuEditButtons.SetButtonForBack();
		}

		private void StopEditingFactions()
		{
			editingState = RadialMenuButtonInteractionType.None;
			RemoveFactionEntityFromFactionCache(addFactionEntity);
			if (factionCache.Count == 0)
			{
				AddFactionEntityFromFactionCache(addFactionEntity);
			}
			else
			{
				PopulateFactionsMenu(factionCache);
			}
			radialMenuEditButtons.SetButtonForEdit();
		}

		private void SetButtonBackgrounds()
		{
			switch (editingState)
			{
			case RadialMenuButtonInteractionType.Removing:
				SetAllSelectedHighlightBackgroundsForRemove();
				break;
			case RadialMenuButtonInteractionType.None:
				SetAllSelectedHighlightBackgroundsForNormal();
				break;
			case RadialMenuButtonInteractionType.Adding:
				SetSelectedHighlightBackgroundsForAdd();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private void SetAllSelectedHighlightBackgroundsForRemove()
		{
			BattleRadialButton[] graphicItems = base.GraphicItems;
			foreach (BattleRadialButton battleRadialButton in graphicItems)
			{
				battleRadialButton.SetButtonColorType(RadialMenuButtonInteractionType.Removing);
				if (battleRadialButton.CurrentData.Entity.GUID.m_ID == addFactionEntity.Entity.GUID.m_ID)
				{
					battleRadialButton.SetButtonColor(RadialMenuButtonColor.Green);
				}
				else
				{
					battleRadialButton.SetButtonColor(RadialMenuButtonColor.Red);
				}
			}
		}

		private void SetAllSelectedHighlightBackgroundsForNormal()
		{
			BattleRadialButton[] graphicItems = base.GraphicItems;
			foreach (BattleRadialButton obj in graphicItems)
			{
				obj.SetButtonColorType(RadialMenuButtonInteractionType.None);
				obj.SetButtonColor(RadialMenuButtonColor.Green);
			}
		}

		private void SetSelectedHighlightBackgroundsForAdd()
		{
			BattleRadialButton[] graphicItems = base.GraphicItems;
			foreach (BattleRadialButton obj in graphicItems)
			{
				obj.SetButtonColorType(RadialMenuButtonInteractionType.Adding);
				obj.SetButtonColor(RadialMenuButtonColor.Blue);
			}
		}

		public void OpenAtPosition(Vector2 position, bool keepOnScreen = false)
		{
			if (keepOnScreen)
			{
				float num = base.ElementPlacer.Radius + screenEdgePadding;
				float resolutionScaleFactor = base.ElementPlacer.ResolutionScaleFactor;
				float num2 = num * resolutionScaleFactor;
				float num3 = (float)Screen.width - num * resolutionScaleFactor;
				float num4 = num * resolutionScaleFactor;
				float num5 = (float)Screen.height - num * resolutionScaleFactor;
				if (position.x < num2)
				{
					position.x = num2;
				}
				if (position.x > num3)
				{
					position.x = num3;
				}
				if (position.y < num4)
				{
					position.y = num4;
				}
				if (position.y > num5)
				{
					position.y = num5;
				}
			}
			((RectTransform)base.transform).position = position;
			Open();
		}

		public void Open()
		{
			if (base.IsSpiral)
			{
				UpdateSpiralItems(selectionChanged: false, 0, 0, 0f);
			}
			else
			{
				UpdateRingItems();
			}
			IsOpen = true;
			canvas.enabled = true;
			canvasGroup.interactable = true;
			canvasGroup.alpha = 1f;
			if (animation != null)
			{
				animation.SetState(SimpleStateAnimation.State.State02);
			}
			this.RadialMenuOpened?.Invoke();
			editingState = RadialMenuButtonInteractionType.None;
			if (base.GraphicItems[0] != null)
			{
				base.GraphicItems[0].Select();
			}
			if (lastSelectedFaction != null)
			{
				ShowUnitsForFaction(lastSelectedFaction);
			}
			if (base.GraphicItems.Length == 1 && base.IsSpiral)
			{
				BattleRadialButton battleRadialButton = base.GraphicItems[0];
				OnRadialButtonPressed(battleRadialButton.CurrentData);
			}
		}

		private void OpenAtCentre()
		{
			OpenAtPosition(Vector2.zero);
		}

		public void Close()
		{
			if (!IsOpen)
			{
				return;
			}
			IsOpen = false;
			editingState = RadialMenuButtonInteractionType.None;
			placementBrush.PreventPlacementBuffer(blockPlacementTime);
			if (unitInfoBar != null)
			{
				unitInfoBar.SetUnitInfo(unitInfoBar.CurrentValidUnit);
				UnitBlueprint currentValidUnit = unitInfoBar.CurrentValidUnit;
				if (currentValidUnit != null)
				{
					placementBrush.SetBrushUnit(unitInfoBar.CurrentValidUnit.Entity.GUID);
					if (currentValidUnit != lastValidUnit && mainCamTransform != null)
					{
						soundPlayer.PlaySoundEffect(currentValidUnit.VocalRef, 1f, mainCamTransform.position + mainCamTransform.forward * 25f, SoundEffectVariations.MaterialType.Default, null, currentValidUnit.VoicePitch);
					}
					lastValidUnit = currentValidUnit;
				}
			}
			if (animation != null)
			{
				animation.SetState(SimpleStateAnimation.State.State01);
			}
			else
			{
				DisableCanvases();
			}
			SaveRadialMenu();
			this.RadialMenuClosed?.Invoke();
			StopEditingFactions();
		}

		private void SaveRadialMenu()
		{
			Faction[] array = new Faction[factionCache.Count];
			for (int i = 0; i < factionCache.Count; i++)
			{
				array[i] = (Faction)factionCache[i];
			}
			ServiceLocator.GetService<ISaveLoaderService>().SetFactionBar(array);
		}

		public void ClearLastCachedFaction()
		{
			lastSelectedFaction = null;
		}

		public void BackPressedFromPlacementUI()
		{
			switch (editingState)
			{
			case RadialMenuButtonInteractionType.Removing:
				StopEditingFactions();
				break;
			case RadialMenuButtonInteractionType.Adding:
				StartEditingFactions();
				break;
			case RadialMenuButtonInteractionType.None:
				if (isSelectingUnits)
				{
					ShowFactions();
				}
				else
				{
					Close();
				}
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public void ShowFactions()
		{
			lastSelectedFaction = null;
			PopulateFactionsMenu(factionCache);
		}

		public void ShowUnitsForFaction(Faction faction)
		{
			lastSelectedFaction = faction;
			PopulateUnitsMenu(faction);
		}

		public void SetSelectedUnit(UnitBlueprint blueprint)
		{
			if (unitInfoBar != null)
			{
				unitInfoBar.SetCurrentlySelectedUnit(blueprint);
			}
		}

		public void UpdateAvailableSandboxUnits()
		{
			factionCache.Clear();
			factionUnitDictionary.Clear();
			BuildCaches();
			Close();
			ShowFactions();
		}

		private void SetUpButtonEvents()
		{
			switch (editingState)
			{
			case RadialMenuButtonInteractionType.None:
				if (base.GraphicItems != null && base.GraphicItems.Length != 0)
				{
					BattleRadialButton[] graphicItems = base.GraphicItems;
					foreach (BattleRadialButton button2 in graphicItems)
					{
						SubscribeRadialMenuButtonEvents(button2, OnRadialButtonSelected, OnRadialButtonPressed);
					}
				}
				break;
			case RadialMenuButtonInteractionType.Adding:
				if (base.GraphicItems != null && base.GraphicItems.Length != 0)
				{
					BattleRadialButton[] graphicItems = base.GraphicItems;
					foreach (BattleRadialButton button3 in graphicItems)
					{
						SubscribeRadialMenuButtonEvents(button3, OnRadialButtonSelectedAdding, OnRadialButtonPressedAdding);
					}
				}
				break;
			case RadialMenuButtonInteractionType.Removing:
				if (base.GraphicItems != null && base.GraphicItems.Length != 0)
				{
					BattleRadialButton[] graphicItems = base.GraphicItems;
					foreach (BattleRadialButton button in graphicItems)
					{
						SubscribeRadialMenuButtonEvents(button, OnRadialButtonSelectedRemoving, OnRadialButtonPressedRemoving);
					}
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("editingState", editingState, null);
			}
		}

		private void SubscribeRadialMenuButtonEvents(BattleRadialButton button, Action<IDatabaseEntity> onSelectedCallback, Action<IDatabaseEntity> onPressedCallback)
		{
			button.RadialButtonSelected += onSelectedCallback;
			button.RadialButtonPressed += onPressedCallback;
		}

		private void CleanUpButtonEvents()
		{
			if (base.GraphicItems == null || base.GraphicItems.Length == 0)
			{
				return;
			}
			BattleRadialButton[] graphicItems = base.GraphicItems;
			foreach (BattleRadialButton battleRadialButton in graphicItems)
			{
				if (!(battleRadialButton == null))
				{
					battleRadialButton.RadialButtonSelected -= OnRadialButtonSelected;
					battleRadialButton.RadialButtonPressed -= OnRadialButtonPressed;
					battleRadialButton.RadialButtonSelected -= OnRadialButtonSelectedAdding;
					battleRadialButton.RadialButtonPressed -= OnRadialButtonPressedAdding;
					battleRadialButton.RadialButtonSelected -= OnRadialButtonSelectedRemoving;
					battleRadialButton.RadialButtonPressed -= OnRadialButtonPressedRemoving;
				}
			}
		}

		private void OnAnimationComplete(SimpleStateAnimation.State state)
		{
			if (state == SimpleStateAnimation.State.State01)
			{
				DisableCanvases();
				RadialMenuClosedResetEditingState();
			}
		}

		private void DisableCanvases()
		{
			canvas.enabled = false;
			canvasGroup.interactable = false;
			canvasGroup.alpha = 0f;
		}

		private void OnRadialButtonSelected(IDatabaseEntity entity)
		{
			if (IsOpen)
			{
				if (editingState == RadialMenuButtonInteractionType.None && entity.Entity.GUID.m_ID == addFactionEntity.Entity.GUID.m_ID)
				{
					editingState = RadialMenuButtonInteractionType.Adding;
					PopulateFactionsMenu(addFactionsFactionCache);
					radialMenuEditButtons.SetButtonForBack();
				}
				else if (entity is UnitBlueprint currentlySelectedUnit)
				{
					unitInfoBar.SetCurrentlySelectedUnit(currentlySelectedUnit);
				}
				else
				{
					factionNameText.LocaleID = entity.Entity.Name;
					unitInfoBar.SetUnitInfo(unitInfoBar.CurrentlySelectedUnit);
				}
			}
		}

		private void OnRadialButtonSelectedRemoving(IDatabaseEntity entity)
		{
			factionNameText.LocaleID = ((entity.Entity.GUID.m_ID == addFactionEntity.Entity.GUID.m_ID) ? addFactionText : removeFactionText);
			if (entity is Faction updateTextForFaction)
			{
				unitInfoBar.SetUpdateTextForFaction(updateTextForFaction);
			}
		}

		private bool FactionContainsUnitsWithMissingAssets(IDatabaseEntity entity)
		{
			return !((Faction)entity).ValidateFactionUnitPropsAndWeapons();
		}

		private void OnRadialButtonPressedRemoving(IDatabaseEntity entity)
		{
			if (entity.Entity.GUID.m_ID != addFactionEntity.Entity.GUID.m_ID)
			{
				RemoveFactionEntityFromFactionCache(entity);
				addFactionsFactionCache.Add(entity);
				AddFactionEntityFromFactionCache(addFactionEntity);
				PopulateFactionsMenu(factionCache);
			}
			else
			{
				editingState = RadialMenuButtonInteractionType.Adding;
				PopulateFactionsMenu(addFactionsFactionCache);
				radialMenuEditButtons.SetButtonForBack();
			}
		}

		private void OnRadialButtonPressedAdding(IDatabaseEntity entity)
		{
			if (CharacterItemExtensions.ShouldCustomContentBeValidated() && FactionContainsUnitsWithMissingAssets(entity))
			{
				ServiceLocator.GetService<ModalPanel>().PopUp("CUSTOM_CONTENT_VALIDATION_FAILED_ADDITIONAL_INFO");
			}
			if (addFactionsFactionCache.Contains(entity))
			{
				addFactionsFactionCache.Remove(entity);
				factionCache.Add(entity);
			}
			if (base.GraphicItems.Length <= 1)
			{
				StopEditingFactions();
			}
			else
			{
				PopulateFactionsMenu(addFactionsFactionCache);
			}
		}

		private void OnRadialButtonSelectedAdding(IDatabaseEntity entity)
		{
			factionNameText.LocaleID = addFactionText;
			if (entity is Faction updateTextForFaction)
			{
				unitInfoBar.SetUpdateTextForFaction(updateTextForFaction);
			}
		}

		private void AddFactionEntityFromFactionCache(IDatabaseEntity entity)
		{
			if (!factionCache.Contains(entity) && addFactionsFactionCache.Count > 0)
			{
				factionCache.Add(entity);
			}
		}

		private void RemoveFactionEntityFromFactionCache(IDatabaseEntity entity)
		{
			if (factionCache.Contains(entity))
			{
				factionCache.Remove(entity);
			}
		}

		private void PopulateFactionsMenu()
		{
			buttonIconManager.Clear();
			if (factionUnitDictionary != null && factionUnitDictionary.Count > 0 && factionUnitDictionary.Count > 0 && factionCache != null && factionCache.Count > 0)
			{
				if (base.Initialized)
				{
					CleanUpButtonEvents();
					Clear();
				}
				Initialize(factionCache);
				isSelectingUnits = false;
				factionNameText.gameObject.SetActive(value: true);
				factionIcon.gameObject.SetActive(value: false);
				Select(selectedIndex);
				SetUpButtonEvents();
				SetButtonBackgrounds();
				switch (editingState)
				{
				case RadialMenuButtonInteractionType.Removing:
				case RadialMenuButtonInteractionType.Adding:
					radialMenuEditButtons.SetButtonForBack();
					break;
				case RadialMenuButtonInteractionType.None:
					radialMenuEditButtons.SetButtonForEdit();
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				if (base.GraphicItems[0] != null)
				{
					base.GraphicItems[0].Select();
				}
				buttonIconManager.Initialize(base.GraphicItems, ShouldDestroyIconTexture);
			}
		}

		private bool ShouldDestroyIconTexture(bool isCustomUnit)
		{
			if (isCustomUnit)
			{
				return DoesPlatformSupportCustomIcons();
			}
			return false;
		}

		private bool DoesPlatformSupportCustomIcons()
		{
			return true;
		}

		private void PopulateFactionsMenu(List<IDatabaseEntity> factionCache)
		{
			buttonIconManager.Clear();
			if (factionUnitDictionary == null || factionUnitDictionary.Count <= 0 || factionUnitDictionary.Count <= 0 || factionCache == null || factionCache.Count <= 0)
			{
				return;
			}
			if (base.Initialized)
			{
				CleanUpButtonEvents();
				Clear();
			}
			if (gameModeService != null && gameModeService.CurrentGameMode is OnlineMultiplayerGameMode)
			{
				Initialize(factionCache.Where((IDatabaseEntity entity) => !((Faction)entity).IsCustom).ToList());
			}
			else
			{
				Initialize(factionCache);
			}
			isSelectingUnits = false;
			factionNameText.gameObject.SetActive(value: true);
			factionIcon.gameObject.SetActive(value: false);
			SetUpButtonEvents();
			SetButtonBackgrounds();
			Select(selectedIndex);
			switch (editingState)
			{
			case RadialMenuButtonInteractionType.Removing:
			case RadialMenuButtonInteractionType.Adding:
				radialMenuEditButtons.SetButtonForBack();
				break;
			case RadialMenuButtonInteractionType.None:
				radialMenuEditButtons.SetButtonForEdit();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (base.GraphicItems[0] != null)
			{
				base.GraphicItems[0].Select();
			}
			buttonIconManager.Initialize(base.GraphicItems, ShouldDestroyIconTexture);
		}

		private void PopulateUnitsMenu(Faction faction)
		{
			buttonIconManager.Clear();
			if (factionUnitDictionary == null || factionUnitDictionary.Count <= 0 || !factionUnitDictionary.TryGetValue(faction, out var value) || value == null || value.Count <= 0)
			{
				return;
			}
			if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.OnlineMultiplayer)
			{
				value.RemoveAll((IDatabaseEntity item) => ((UnitBlueprint)item).ExcludeFromOnlineMultiplayer);
			}
			if (base.Initialized)
			{
				CleanUpButtonEvents();
				Clear();
			}
			UnitBlueprint[] rearrangedUnits = faction.GetRearrangedUnits();
			Initialize(rearrangedUnits);
			isSelectingUnits = true;
			factionNameText.gameObject.SetActive(value: false);
			factionIcon.gameObject.SetActive(value: true);
			editingState = RadialMenuButtonInteractionType.None;
			faction.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && factionIcon != null)
				{
					factionIcon.sprite = sprite;
				}
			});
			SetUpButtonEvents();
			buttonIconManager.Initialize(base.GraphicItems, ShouldDestroyIconTexture);
		}

		private void OnRadialButtonPressed(IDatabaseEntity entity)
		{
			if (!IsOpen)
			{
				return;
			}
			if (entity == null)
			{
				return;
			}
			if (!(entity is Faction faction))
			{
				if (entity is UnitBlueprint unitBlueprint)
				{
					UnitBlueprint unitBlueprint2 = unitBlueprint;
					GameModeService service = ServiceLocator.GetService<GameModeService>();
					if (service != null)
					{
						this.UnitSelected?.Invoke(unitBlueprint2);
						service.CurrentGameMode?.Brush?.SetBrushUnit(unitBlueprint2.Entity.GUID);
					}
					Close();
				}
			}
			else
			{
				Faction faction2 = faction;
				ShowUnitsForFaction(faction2);
				Select(selectedIndex);
			}
		}

		private void BuildCaches()
		{
			factionCache.Clear();
			addFactionsFactionCache.Clear();
			if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Campaign)
			{
				SetUpFactionsForCampaign();
				return;
			}
			SetUpDefaultFactionsForSandBox();
			if (!(gameModeService != null) || !(gameModeService.CurrentGameMode is OnlineMultiplayerGameMode))
			{
				SetUpDefaultAddFactionsCache();
			}
		}

		public void ResetEditedFactions()
		{
			if (canUseCustomFactions && IsOpen)
			{
				IEnumerable<Faction> defaultHotbarFactions = ContentDatabase.Instance().GetDefaultHotbarFactions();
				ServiceLocator.GetService<ISaveLoaderService>().SetFactionBar(defaultHotbarFactions.ToArray());
				BuildCaches();
				PopulateFactionsMenu();
				if (editingState != RadialMenuButtonInteractionType.None)
				{
					StopEditingFactions();
				}
			}
		}

		private void SetUpDefaultAddFactionsCache()
		{
			foreach (Faction allFaction in ContentDatabase.Instance().GetAllFactions())
			{
				Faction faction;
				if ((object)(faction = allFaction) != null && faction.m_displayFaction && !factionCache.Contains(allFaction))
				{
					addFactionsFactionCache.Add(faction);
					List<IDatabaseEntity> list = new List<IDatabaseEntity>();
					UnitBlueprint[] units = faction.Units;
					foreach (UnitBlueprint item in units)
					{
						list.Add(item);
					}
					if (!factionUnitDictionary.ContainsKey(faction))
					{
						factionUnitDictionary.Add(faction, list);
					}
				}
			}
		}

		private void SetUpDefaultFactionsForSandBox()
		{
			Faction[] factionBar = ServiceLocator.GetService<ISaveLoaderService>().GetFactionBar();
			if (factionBar == null)
			{
				return;
			}
			List<int> enabledModIds = LocalUser.EnabledModIds;
			Faction[] array = factionBar;
			foreach (Faction faction in array)
			{
				if (faction.modID == 0 || enabledModIds.Contains(faction.modID))
				{
					UpdateFactionCacheAndUnitDictionary(faction);
				}
			}
			foreach (Faction defaultHotbarFaction in ContentDatabase.Instance().GetDefaultHotbarFactions())
			{
				UpdateFactionCacheAndUnitDictionary(defaultHotbarFaction);
			}
		}

		private void SetUpFactionsForCampaign()
		{
			IEnumerable<Faction> enumerable;
			if (ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_RESTRICT_UNITS").currentValue != 1)
			{
				IEnumerable<Faction> allowedFactions = CampaignPlayerDataHolder.GetCurrentLevel().AllowedFactions;
				enumerable = allowedFactions;
			}
			else
			{
				enumerable = ContentDatabase.Instance().GetDefaultHotbarFactions();
			}
			IEnumerable<Faction> enumerable2 = enumerable;
			Faction[] array = ((enumerable2 != null && enumerable2.Count() > 0) ? enumerable2.Where((Faction faction2) => faction2 != null && ContentDatabase.Instance().GetFaction(faction2.Entity.GUID) != null).ToArray() : GetFactions());
			Faction[] array2 = array;
			foreach (Faction faction in array2)
			{
				UpdateFactionCacheAndUnitDictionary(faction);
			}
		}

		private Faction[] GetFactions()
		{
			IEnumerable<Faction> defaultHotbarFactions = ContentDatabase.Instance().GetDefaultHotbarFactions();
			List<int> enabledMods = LocalUser.EnabledModIds;
			return defaultHotbarFactions.Where((Faction faction) => faction != null && (faction.modID == 0 || enabledMods.Contains(faction.modID))).ToArray();
		}

		private void UpdateFactionCacheAndUnitDictionary(Faction faction)
		{
			if (!factionCache.Contains(faction))
			{
				factionCache.Add(faction);
			}
			List<IDatabaseEntity> list = new List<IDatabaseEntity>();
			UnitBlueprint[] units = faction.Units;
			foreach (UnitBlueprint item in units)
			{
				list.Add(item);
			}
			if (!factionUnitDictionary.ContainsKey(faction))
			{
				factionUnitDictionary.Add(faction, list);
			}
		}

		private void OnSelectedUnitChanged(UnitBlueprint unitBlueprint)
		{
			if (unitInfoBar != null)
			{
				unitInfoBar.SetCurrentlySelectedUnit(unitBlueprint);
			}
		}

		private void OnDestroy()
		{
			buttonIconManager.Clear();
			GameModeService service = ServiceLocator.GetService<GameModeService>();
			if (service != null)
			{
				placementBrush = service.CurrentGameMode?.Brush;
				if (placementBrush != null)
				{
					placementBrush.SelectedUnitChanged -= OnSelectedUnitChanged;
				}
			}
			CleanUpButtonEvents();
			if (animation != null)
			{
				animation.Completed -= OnAnimationComplete;
			}
		}

		public void SetPlacerScaleFactor(float scaleFactor)
		{
			base.ElementPlacer.SetScaleFactor(scaleFactor, Mathf.Clamp01(scaleFactor));
			Vector3 localScale = Vector3.one * base.ElementPlacer.LayoutScaleFactor;
			if (backgroundTransform != null)
			{
				backgroundTransform.localScale = localScale;
			}
			if (menuItemsTransform != null)
			{
				menuItemsTransform.localScale = localScale;
			}
			base.ElementPlacer.UpdateArrow((RectTransform)arrowTransform, base.InputProvider.GetAbsoluteInput());
		}
	}
}
