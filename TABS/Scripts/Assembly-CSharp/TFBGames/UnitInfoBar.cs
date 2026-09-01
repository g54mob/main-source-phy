using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.UnitPlacement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class UnitInfoBar : MonoBehaviour
	{
		[SerializeField]
		protected Image unitIcon;

		[SerializeField]
		protected LocalizeText unitText;

		[SerializeField]
		protected TMP_Text unitCost;

		[SerializeField]
		protected TMP_Text unitFoodCost;

		[SerializeField]
		protected Image unitFoodImage;

		[SerializeField]
		protected Sprite defaultIcon;

		[SerializeField]
		protected Sprite unitRedIcon;

		[SerializeField]
		protected Sprite unitBlueIcon;

		[SerializeField]
		protected SimpleStateAnimation animation;

		private UnitBlueprint currentlySelectedUnit;

		private UnitBlueprint currentValidUnit;

		private UnitPlacementBrush brush;

		private ISaveLoaderService saveLoader;

		private Texture lastTextureLoadedFromDisk;

		private RadialMenuShouldDestroyIconTextureCallback shouldDestroyIconTexture;

		public UnitBlueprint CurrentlySelectedUnit => currentlySelectedUnit;

		public UnitBlueprint CurrentValidUnit => currentValidUnit;

		private void Awake()
		{
			animation = GetComponent<SimpleStateAnimation>();
			PopulateSaveLoader();
		}

		private void Start()
		{
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			brush = currentGameMode.Brush;
			SetUnitInfo(brush.UnitToSpawn);
			unitFoodCost.gameObject.SetActive(value: false);
		}

		private void OnDestroy()
		{
			if (brush != null)
			{
				brush.CursorStateChange -= UpdateFoodIconColor;
			}
		}

		public void SetShouldDestroyIconFunction(RadialMenuShouldDestroyIconTextureCallback shouldDestroyFunc)
		{
			shouldDestroyIconTexture = shouldDestroyFunc;
		}

		public void SetUpdateTextForFaction(Faction faction)
		{
			if (!(faction == null))
			{
				if (unitCost != null)
				{
					unitCost.text = "-";
				}
				if (unitIcon != null)
				{
					LoadEntityIcon(faction.Entity, isCustomUnit: false);
				}
				if (unitText != null)
				{
					unitText.LocaleID = faction.Entity.Name;
				}
			}
		}

		public void SetUnitInfo(UnitBlueprint blueprint)
		{
			if (saveLoader == null)
			{
				return;
			}
			if (blueprint == null)
			{
				SetUnitIconSprite(defaultIcon);
				unitText.LocaleID = "LABEL_NONE_SELECTED";
				unitCost.text = "-";
				return;
			}
			PopulateSaveLoader();
			bool flag = false;
			if (!blueprint.IsSecret)
			{
				flag = true;
				currentValidUnit = blueprint;
			}
			else if (saveLoader.HasUnlockedSecret(blueprint.Entity.UnlockKey))
			{
				flag = true;
				currentValidUnit = blueprint;
			}
			if (unitIcon != null)
			{
				LoadEntityIcon(blueprint.Entity, blueprint.IsCustomUnit);
			}
			if (unitText != null)
			{
				unitText.LocaleID = (flag ? blueprint.Name : string.Empty);
			}
			if (unitCost != null)
			{
				unitCost.text = (flag ? blueprint.GetUnitCost().ToString() : string.Empty);
			}
			if (unitFoodCost != null)
			{
				unitFoodCost.text = (flag ? blueprint.FoodCost.ToString() : string.Empty);
			}
		}

		public void Show()
		{
			if (animation != null)
			{
				animation.SetState(SimpleStateAnimation.State.State01);
			}
		}

		public void Hide()
		{
			if (animation != null)
			{
				animation.SetState(SimpleStateAnimation.State.State02);
			}
		}

		public void SetCurrentlySelectedUnit(UnitBlueprint blueprint)
		{
			currentlySelectedUnit = blueprint;
			SetUnitInfo(currentlySelectedUnit);
		}

		private void PopulateSaveLoader()
		{
			if (saveLoader == null)
			{
				saveLoader = ServiceLocator.GetService<ISaveLoaderService>();
			}
		}

		private void UpdateFoodIconColor(PlacementCursorState newCursorState)
		{
			if (unitFoodImage != null)
			{
				switch (newCursorState)
				{
				case PlacementCursorState.AllowRedPlacement:
					unitFoodImage.sprite = unitRedIcon;
					break;
				case PlacementCursorState.AllowBluePlacement:
					unitFoodImage.sprite = unitBlueIcon;
					break;
				}
			}
		}

		private void SetUnitIconSprite(Sprite sprite)
		{
			if (unitIcon != null)
			{
				DestroyLastTextureLoadedFromDisk();
				unitIcon.sprite = sprite;
			}
		}

		private void LoadEntityIcon(DatabaseEntity entity, bool isCustomUnit)
		{
			entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (!(sprite == null) && !(unitIcon == null))
				{
					SetUnitIconSprite(sprite);
					if (shouldDestroyIconTexture(isCustomUnit))
					{
						lastTextureLoadedFromDisk = sprite.texture;
					}
				}
			});
		}

		private void DestroyLastTextureLoadedFromDisk()
		{
			if (lastTextureLoadedFromDisk != null)
			{
				Object.Destroy(lastTextureLoadedFromDisk);
				lastTextureLoadedFromDisk = null;
			}
		}
	}
}
