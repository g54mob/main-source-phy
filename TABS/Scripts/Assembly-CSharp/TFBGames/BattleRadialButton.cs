using System;
using BitCode.UI;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class BattleRadialButton : Selectable, ISubmitHandler, IEventSystemHandler, IRadialMenuItem<IDatabaseEntity>
	{
		private class LoadIconsInfo
		{
			public RadialMenuCheckCanLoadAnotherIconCallback CheckCanLoadAnotherIconFunc;

			public RadialMenuShouldDestroyIconTextureCallback ShouldDestroyIconTexture;

			public int CancelLoadIconCount;

			public Texture LastTextureLoadedFromDisk;

			public DatabaseEntity BusyLoadingEntity { get; set; }

			public DatabaseEntity SuccessfullyLoadedEntity { get; set; }

			public DatabaseEntity NextEntityToLoad { get; private set; }

			public bool NextEntityToLoadIsCustomUnit { get; private set; }

			public void SetNextEntityToLoad(DatabaseEntity entity, bool isCustomUnit)
			{
				NextEntityToLoad = entity;
				NextEntityToLoadIsCustomUnit = isCustomUnit;
			}
		}

		protected CanvasGroup canvasGroup;

		[SerializeField]
		protected TMP_Text costLabel;

		[SerializeField]
		protected Image costImage;

		[SerializeField]
		protected TMP_Text unitFoodCost;

		[SerializeField]
		protected Image unitFoodImage;

		[SerializeField]
		protected Sprite unitRedIcon;

		[SerializeField]
		protected Sprite unitBlueIcon;

		[SerializeField]
		protected Image icon;

		[SerializeField]
		protected Image loadingIcon;

		[SerializeField]
		protected Color unlockedColor;

		[SerializeField]
		protected Color lockedColor;

		[Tooltip("Non custom unit icons will be forced to a local scale of Vector3.one")]
		[SerializeField]
		protected float overrideCustomUnitIconScale = 1.2f;

		private ISaveLoaderService saveLoader;

		private bool initialized;

		private BattleRadialMenuBackgroundHolder spriteBackgroundHolder;

		private Image background;

		private RadialMenuButtonInteractionType buttonInteractionType;

		private Vector3 customUnitIconScale;

		private GameModeService gameModeService;

		private readonly LoadIconsInfo loadIcons = new LoadIconsInfo();

		public IDatabaseEntity CurrentData { get; private set; }

		public bool Unlocked { get; set; }

		private bool IsBusyLoadingIcon => loadIcons.BusyLoadingEntity != null;

		Transform IRadialMenuItem<IDatabaseEntity>.transform => base.transform;

		public event Action<IDatabaseEntity> RadialButtonSelected;

		public event Action<IDatabaseEntity> RadialButtonPressed;

		public event Action StartedLoadingIcon;

		public event Action DoneLoadingIcon;

		protected override void Awake()
		{
			base.Awake();
			spriteBackgroundHolder = GetComponent<BattleRadialMenuBackgroundHolder>();
			background = GetComponent<Image>();
			customUnitIconScale = Vector3.one * overrideCustomUnitIconScale;
		}

		protected override void Start()
		{
			base.Start();
			Init();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			loadIcons.CancelLoadIconCount++;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			loadIcons.CancelLoadIconCount++;
			DestroyLastTextureLoadedFromDisk();
			loadIcons.CheckCanLoadAnotherIconFunc = null;
			loadIcons.ShouldDestroyIconTexture = null;
		}

		private void Update()
		{
			UpdateLoadIcon();
		}

		public void SetIconFunctions(RadialMenuCheckCanLoadAnotherIconCallback canLoadFunc, RadialMenuShouldDestroyIconTextureCallback shouldDestroyFunc)
		{
			loadIcons.CheckCanLoadAnotherIconFunc = canLoadFunc;
			loadIcons.ShouldDestroyIconTexture = shouldDestroyFunc;
		}

		public override void Select()
		{
			base.Select();
			this.RadialButtonSelected?.Invoke(CurrentData);
		}

		public void UpdateData(IDatabaseEntity data)
		{
			Init();
			CurrentData = data;
			TABSCampaignLevelAsset tABSCampaignLevelAsset = null;
			bool flag = CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Campaign;
			bool flag2 = true;
			if (flag)
			{
				tABSCampaignLevelAsset = CampaignPlayerDataHolder.GetCurrentLevel();
			}
			if (CurrentData is UnitBlueprint unitBlueprint)
			{
				TryAdjustCustomUnitIconSize(unitBlueprint);
				if (tABSCampaignLevelAsset != null)
				{
					flag2 = tABSCampaignLevelAsset.IsAllowed(unitBlueprint);
				}
				if (unitBlueprint.IsSecret)
				{
					PopulateSaveLoader();
					flag2 = saveLoader.HasUnlockedSecret(unitBlueprint.Entity.UnlockKey);
				}
				if (icon != null)
				{
					LoadIcon(unitBlueprint.Entity, unitBlueprint.IsCustomUnit);
				}
				if (flag2)
				{
					costLabel.text = unitBlueprint.GetUnitCost().ToString();
					costImage.enabled = true;
					unitFoodCost.text = unitBlueprint.FoodCost.ToString();
					unitFoodImage.enabled = true;
					if (gameModeService.CurrentGameMode.Brush.GetTeamAtCursorPosition() == Team.Blue)
					{
						unitFoodImage.sprite = unitBlueIcon;
					}
					else
					{
						unitFoodImage.sprite = unitRedIcon;
					}
				}
				else
				{
					HideCostUI();
				}
				SetUnlockedState(flag2);
			}
			else if (CurrentData is Faction faction)
			{
				bool flag3 = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_RESTRICT_UNITS").currentValue == 1;
				bool unlockedState = false;
				if (flag && !flag3)
				{
					for (int i = 0; i < faction.Units.Length; i++)
					{
						if (tABSCampaignLevelAsset.IsAllowed(faction.Units[i]))
						{
							unlockedState = true;
							break;
						}
					}
				}
				else
				{
					unlockedState = true;
				}
				if (icon != null)
				{
					LoadIcon(faction.Entity, isCustomUnit: false);
				}
				HideCostUI();
				SetUnlockedState(unlockedState);
				switch (buttonInteractionType)
				{
				case RadialMenuButtonInteractionType.Removing:
					if (CurrentData.Entity.GUID.m_ID == spriteBackgroundHolder.AddFactionFaction.Entity.GUID.m_ID)
					{
						SetGreen();
					}
					else
					{
						SetRed();
					}
					break;
				case RadialMenuButtonInteractionType.None:
					SetGreen();
					break;
				case RadialMenuButtonInteractionType.Adding:
					SetBlue();
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
			else
			{
				Debug.LogWarning("Cannot Update Data for Button. Data may be an in invalid type or null.");
			}
		}

		private void LoadIcon(DatabaseEntity entity, bool isCustomUnit)
		{
			if (!IsReadyToLoadIcon(showLoadingIcon: true, entity))
			{
				loadIcons.SetNextEntityToLoad(entity, isCustomUnit);
			}
			else
			{
				StartLoadingIcon(entity, isCustomUnit);
			}
		}

		private void StartLoadingIcon(DatabaseEntity entity, bool isCustomUnit)
		{
			if (entity == loadIcons.SuccessfullyLoadedEntity)
			{
				ShowLoadingIcon(show: false);
				return;
			}
			ShowLoadingIcon(show: true);
			loadIcons.BusyLoadingEntity = entity;
			this.StartedLoadingIcon?.Invoke();
			loadIcons.CancelLoadIconCount++;
			int tempCancelCount = loadIcons.CancelLoadIconCount;
			entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				loadIcons.BusyLoadingEntity = null;
				this.DoneLoadingIcon?.Invoke();
				bool flag = sprite != null && icon != null;
				bool flag2 = tempCancelCount != loadIcons.CancelLoadIconCount;
				if (flag2)
				{
					flag = false;
				}
				if (loadIcons.NextEntityToLoad != null)
				{
					if (flag && loadIcons.NextEntityToLoad == entity)
					{
						loadIcons.SetNextEntityToLoad(null, isCustomUnit: false);
					}
					else
					{
						flag = false;
					}
				}
				if (flag)
				{
					ShowLoadingIcon(show: false);
					DestroyLastTextureLoadedFromDisk();
					loadIcons.SuccessfullyLoadedEntity = entity;
					icon.sprite = sprite;
					if (loadIcons.ShouldDestroyIconTexture != null && loadIcons.ShouldDestroyIconTexture(isCustomUnit))
					{
						loadIcons.LastTextureLoadedFromDisk = sprite.texture;
					}
				}
				else if (flag2)
				{
					DestroyLastTextureLoadedFromDisk();
					if (sprite != null && loadIcons.ShouldDestroyIconTexture != null && loadIcons.ShouldDestroyIconTexture(isCustomUnit))
					{
						loadIcons.LastTextureLoadedFromDisk = sprite.texture;
					}
				}
			});
		}

		private bool IsReadyToLoadIcon(bool showLoadingIcon, DatabaseEntity entityToLoad)
		{
			bool flag = true;
			bool isBusyLoadingIcon = IsBusyLoadingIcon;
			float num = ((canvasGroup != null) ? canvasGroup.alpha : 1f);
			bool flag2 = num > 0f && !Mathf.Approximately(num, 0f);
			if (loadIcons.CheckCanLoadAnotherIconFunc == null || !loadIcons.CheckCanLoadAnotherIconFunc() || isBusyLoadingIcon || !flag2)
			{
				flag = false;
			}
			bool flag3 = entityToLoad != null && entityToLoad == loadIcons.SuccessfullyLoadedEntity;
			if (showLoadingIcon && !flag && !isBusyLoadingIcon && !flag3)
			{
				ShowLoadingIcon(show: true);
			}
			return flag;
		}

		private void UpdateLoadIcon()
		{
			if (loadIcons.NextEntityToLoad != null && IsReadyToLoadIcon(showLoadingIcon: true, loadIcons.NextEntityToLoad))
			{
				DatabaseEntity nextEntityToLoad = loadIcons.NextEntityToLoad;
				bool nextEntityToLoadIsCustomUnit = loadIcons.NextEntityToLoadIsCustomUnit;
				loadIcons.SetNextEntityToLoad(null, isCustomUnit: false);
				StartLoadingIcon(nextEntityToLoad, nextEntityToLoadIsCustomUnit);
			}
		}

		private void DestroyLastTextureLoadedFromDisk()
		{
			if (loadIcons.LastTextureLoadedFromDisk != null)
			{
				UnityEngine.Object.Destroy(loadIcons.LastTextureLoadedFromDisk);
				loadIcons.LastTextureLoadedFromDisk = null;
			}
		}

		private void ShowLoadingIcon(bool show)
		{
			if (loadingIcon != null)
			{
				loadingIcon.enabled = show;
			}
			if (icon != null)
			{
				icon.enabled = !show;
			}
		}

		private void HideCostUI()
		{
			costLabel.text = string.Empty;
			costImage.enabled = false;
			unitFoodCost.text = string.Empty;
			unitFoodImage.enabled = false;
		}

		public void OnSubmit(BaseEventData eventData)
		{
			if (Unlocked)
			{
				this.RadialButtonPressed?.Invoke(CurrentData);
			}
		}

		public void SetAlpha(float alpha)
		{
			if (canvasGroup != null)
			{
				canvasGroup.alpha = alpha;
			}
		}

		public void SetUnlockedState(bool unlocked)
		{
			Unlocked = unlocked;
			icon.color = (Unlocked ? unlockedColor : lockedColor);
			base.interactable = Unlocked;
		}

		private void PopulateSaveLoader()
		{
			if (saveLoader == null)
			{
				saveLoader = ServiceLocator.GetService<ISaveLoaderService>();
			}
		}

		public void SetButtonColorType(RadialMenuButtonInteractionType interactionType)
		{
			buttonInteractionType = interactionType;
		}

		public void SetButtonColor(RadialMenuButtonColor color)
		{
			switch (color)
			{
			case RadialMenuButtonColor.Red:
				SetRed();
				break;
			case RadialMenuButtonColor.Green:
				SetGreen();
				break;
			case RadialMenuButtonColor.Blue:
				SetBlue();
				break;
			default:
				throw new ArgumentOutOfRangeException("color", color, null);
			}
		}

		private void Init()
		{
			if (!initialized)
			{
				gameModeService = ServiceLocator.GetService<GameModeService>();
				canvasGroup = GetComponent<CanvasGroup>();
				PopulateSaveLoader();
				unitFoodCost.gameObject.SetActive(value: false);
				unitFoodImage.gameObject.SetActive(value: false);
				initialized = true;
			}
		}

		private void TryAdjustCustomUnitIconSize(UnitBlueprint blueprint)
		{
			if (blueprint.IsCustomUnit)
			{
				icon.rectTransform.localScale = customUnitIconScale;
			}
			else
			{
				icon.rectTransform.localScale = Vector3.one;
			}
		}

		private void SetRed()
		{
			if (!(background.sprite == null) && !(spriteBackgroundHolder == null) && !(spriteBackgroundHolder.RedBackground == null))
			{
				background.sprite = spriteBackgroundHolder.Red;
				SetSelectedSpriteState(spriteBackgroundHolder.RedBackground);
			}
		}

		private void SetGreen()
		{
			if (!(background.sprite == null) && !(spriteBackgroundHolder == null) && !(spriteBackgroundHolder.GreenBackground == null))
			{
				background.sprite = spriteBackgroundHolder.Green;
				SetSelectedSpriteState(spriteBackgroundHolder.GreenBackground);
			}
		}

		private void SetBlue()
		{
			if (!(background.sprite == null) && !(spriteBackgroundHolder == null) && !(spriteBackgroundHolder.BlueBackground == null))
			{
				background.sprite = spriteBackgroundHolder.Blue;
				SetSelectedSpriteState(spriteBackgroundHolder.BlueBackground);
			}
		}

		private void SetSelectedSpriteState(Sprite backgroundSprite)
		{
			Sprite disabledSprite = base.spriteState.disabledSprite;
			base.spriteState = new SpriteState
			{
				highlightedSprite = backgroundSprite,
				selectedSprite = backgroundSprite,
				pressedSprite = backgroundSprite,
				disabledSprite = disabledSprite
			};
		}
	}
}
