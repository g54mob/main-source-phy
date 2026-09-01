using System.Collections;
using System.Linq;
using DM;
using GamepadUI.StateManager.Core;
using UIStateManager;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorUIManager : InterfaceStateManager
	{
		[SerializeField]
		private CustomContentPageLoadingRefreshIcon _loadingIcon;

		public UnitEditorManager unitEditorManager;

		public UnitEditorUnitBaseGrid UnitBaseGrid;

		public UnitEditorClothingTypeGrid ClothingTypeGrid;

		public UnitEditorContentGrid contentGrid;

		public UnitEditorEquipedClothingGrid EquipedClothingGrid;

		public UnitEditorEquipedAbilityGrid EquipedAbilityGrid;

		public UnitEditorEquipedClothing EquipedClothing;

		public Image UnitBasePreviewIcon;

		public LocalizeText UnitBasePreviewName;

		public UnitEditorWeaponSlot MainSlot;

		public UnitEditorWeaponSlot OffhandSlot;

		public UnitEditorStatsPageUI StatsPage;

		public RawImage iconImage;

		public UnitEditorSaveScreen UnitEditorSaveScreen;

		public UnitEditorListSelectScreen listSelectScreen;

		public UnitEditorUnitPage UnitPage;

		public UnitEditorWeaponsPage WeaponsPage;

		public UnitEditorRiderUI RiderUI;

		public UnitEditorGamepadGlyphs GamepadGlyphs;

		public UIMovementAnimation UnitPreviewAnimation;

		public CodeAnimation[] MainUIAnimations;

		public PageCounter clothingCounter;

		public PageCounter abilityCounter;

		private Coroutine _showRiderRoutine;

		protected override void Start()
		{
			base.Start();
			UpdateEquipedWeapons();
			StatsPage.SpawnStats(unitEditorManager.Stats);
			UpdateItemCounts();
			if (WeaponsPage != null)
			{
				WeaponsPage.SetManager(unitEditorManager);
			}
			if (_loadingIcon != null)
			{
				_loadingIcon.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.HaveContent);
			}
		}

		public void GoToVoiceSelect()
		{
			listSelectScreen.Setup(ContentDatabase.Instance().GetAllVoiceBundles().ToArray());
			NavigateToPage("VOICESELECT");
		}

		public void NavigateToPage(string pageName)
		{
			UIComponent uIComponentFromName = GetUIComponentFromName(pageName);
			if (!(uIComponentFromName == null))
			{
				OpenUIComponent(uIComponentFromName);
			}
		}

		public void Lock(string pageName)
		{
			UIComponent uIComponentFromName = GetUIComponentFromName(pageName);
			if (!(uIComponentFromName == null))
			{
				uIComponentFromName.GetComponent<CanvasGroup>().interactable = false;
				uIComponentFromName.IsActive = false;
			}
		}

		public void Unlock(string pageName)
		{
			UIComponent uIComponentFromName = GetUIComponentFromName(pageName);
			if (!(uIComponentFromName == null))
			{
				uIComponentFromName.GetComponent<CanvasGroup>().interactable = true;
				uIComponentFromName.IsActive = true;
			}
		}

		public void InitlizeUnitBaseButtons(UnitEditorManager.UnitBaseWrapper[] unitBaseWrappers)
		{
			UnitBaseGrid.SpawnUnitBaseButtons(unitBaseWrappers, unitEditorManager);
		}

		public void InitlizeClothingTypeButtons(UnitEditorManager.ClothingTypeWrapper[] clothingTypeWrappers)
		{
			ClothingTypeGrid.SpawnUnitBaseButtons(clothingTypeWrappers, unitEditorManager);
		}

		private UIComponent GetUIComponentFromName(string pageName)
		{
			foreach (UIComponentContainer interfaceComponent in interfaceComponents)
			{
				if (interfaceComponent.UIComponentName == pageName)
				{
					return interfaceComponent.Component;
				}
			}
			return null;
		}

		public GameObject SpawnEquipedClothing(UnitEditorManager.EquipedClothingWrapper clothingWrapper)
		{
			return EquipedClothingGrid.SpawnEquipedClothes(clothingWrapper);
		}

		public GameObject SpawnEquipedAbility(UnitEditorManager.EquipedSpecialAbility wrapper)
		{
			return EquipedAbilityGrid.SpawnEquipedAbility(wrapper);
		}

		public void UpdateUnitBasePreview(UnitEditorManager.UnitBaseWrapper unitBase)
		{
			UnitBasePreviewIcon.sprite = unitBase.BaseIcon;
			UnitBasePreviewName.LocaleID = unitBase.BaseDisplayName;
		}

		public void ShowClothesByType(UnitEditorManager.ClothingTypeWrapper gearType)
		{
			contentGrid.PopulateCells(gearType.GetCharacterItems(unitEditorManager), UnitEditorContentGrid.ContentType.Clothes);
		}

		public void SetupEquipedClothing(UnitEditorManager.EquipedClothingWrapper clothingWrapper)
		{
			EquipedClothing.Setup(clothingWrapper);
		}

		public void SetupEquipedWeapon(UnitEditorManager.EquipedWeaponWrapper weapon)
		{
			if (!weapon.isRightHanded)
			{
				OnWeaponModeChanged(0);
			}
			EquipedClothing.Setup(unitEditorManager.GetEquipedWeapon(weapon.isRightHanded));
		}

		public void SetupEquipedAbility(UnitEditorManager.EquipedSpecialAbility ability)
		{
			EquipedClothing.Setup(ability);
		}

		public void ShowWeapons(bool right)
		{
			CharacterItem[] array = ContentDatabase.Instance().GetEditorVisibleWeaponItemsOfType<Weapon>().ToArray();
			CharacterItem[] characterItems = array;
			if (right)
			{
				contentGrid.PopulateCells(characterItems, UnitEditorContentGrid.ContentType.WeaponsRight);
			}
			else
			{
				contentGrid.PopulateCells(characterItems, UnitEditorContentGrid.ContentType.WeaponsLeft);
			}
		}

		public void ShowSpecialAbilities()
		{
			CharacterItem[] array = ContentDatabase.Instance().GetEditorVisibleSpecialAbilities().ToArray();
			CharacterItem[] characterItems = array;
			contentGrid.PopulateCells(characterItems, UnitEditorContentGrid.ContentType.SpecialAbility);
		}

		public void ShowProjectiles()
		{
			NavigateToPage("VOICESELECT");
			listSelectScreen.Setup((from p in ContentDatabase.Instance().GetAllProjectiles()
				select p.GetComponent<ProjectileEntity>()).ToArray());
		}

		public void UpdateEquipedWeapons()
		{
			MainSlot.UpdateUI(unitEditorManager.RightHandedWeapon);
			OffhandSlot.UpdateUI(unitEditorManager.LeftHandedWeapon);
		}

		public void ClickedUnitEditorWeaponSlot(bool isRight)
		{
			UnitEditorManager.EquipedWeaponWrapper equipedWeapon = unitEditorManager.GetEquipedWeapon(isRight);
			if (equipedWeapon == null)
			{
				NavigateToPage("CLOTHINGLIST");
				ShowWeapons(isRight);
			}
			else
			{
				NavigateToPage("EQUIPEDCLOTHING");
				SetupEquipedWeapon(equipedWeapon);
			}
		}

		public void ClearUnit()
		{
			if (_showRiderRoutine != null)
			{
				StopCoroutine(_showRiderRoutine);
				_showRiderRoutine = null;
			}
			if (_loadingIcon != null)
			{
				_loadingIcon.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.HaveContent);
			}
			SetPitch(1f);
			UpdateEquipedWeapons();
			RemoveAllClothingUI();
			RemoveAllAbilityUI();
			UnitEditorSaveScreen.Clear();
		}

		public void SetPitch(float f)
		{
			UnitPage.SetPitch(f);
		}

		public void OnWeaponModeChanged(int mode)
		{
			switch (mode)
			{
			case 0:
				unitEditorManager.ChangeWeaponMode(UnitEditorManager.WeaponMode.OneHanded);
				OffhandSlot.Enable();
				break;
			case 1:
				unitEditorManager.ChangeWeaponMode(UnitEditorManager.WeaponMode.TwoHanded);
				OffhandSlot.Disable();
				break;
			}
		}

		public void RemoveAllClothingUI()
		{
			EquipedClothingGrid.ClearAllButtons();
		}

		public void RemoveAllAbilityUI()
		{
			EquipedAbilityGrid.ClearAllButtons();
		}

		public void UpdateStatUI()
		{
			StatsPage.UpdateStat();
		}

		public void CenterUnit()
		{
			UnitPreviewAnimation.SetState(UIMovementAnimation.State.State02);
			for (int i = 0; i < MainUIAnimations.Length; i++)
			{
				MainUIAnimations[i].PlayOut();
			}
		}

		public void ResetUnitUI()
		{
			UnitPreviewAnimation.SetState(UIMovementAnimation.State.State01);
			for (int i = 0; i < MainUIAnimations.Length; i++)
			{
				MainUIAnimations[i].PlayIn();
			}
		}

		public void SetNewIcon(Texture2D tex2D)
		{
			if (tex2D != null)
			{
				iconImage.enabled = true;
			}
			else
			{
				iconImage.enabled = false;
			}
			iconImage.texture = tex2D;
			UnitEditorSaveScreen.OnSetIcon(tex2D != null);
		}

		public void OnUnitNameChanged(string newName)
		{
			UnitEditorSaveScreen.OnUnitNameChanged(newName);
		}

		public string GetUnitName()
		{
			return UnitEditorSaveScreen.GetUnitName();
		}

		public string GetUnitDescrption()
		{
			return UnitEditorSaveScreen.GetUnitDescrption();
		}

		public void SetName(string name)
		{
			UnitEditorSaveScreen.SetName(name);
		}

		public void SetDescription(string description)
		{
			UnitEditorSaveScreen.SetDescription(description);
		}

		public void SelectVoiceBundle(VoiceBundle currentVoiceBundle)
		{
			UnitPage.EquipVoiceBundle(currentVoiceBundle);
		}

		public float GetCurrentPitch()
		{
			return UnitPage.GetPitch();
		}

		public void UpdateMovementType(UnitEditorManager.MovementTypeWrapper movementTypeWrapper)
		{
			UnitPage.UpdateMovementType(movementTypeWrapper);
		}

		public void UpdateTargetingType(UnitEditorManager.TargetingTypeWrapper targetTypeWrapper)
		{
			UnitPage.UpdateTargetingType(targetTypeWrapper);
		}

		public void UpdateItemCounts()
		{
			int clothingCount = unitEditorManager.GetClothingCount();
			int maxClothingCount = unitEditorManager.GetMaxClothingCount();
			clothingCounter.Set(clothingCount, maxClothingCount);
			EquipedClothingGrid.SetNewButtonState(clothingCount != maxClothingCount);
			int abilityCount = unitEditorManager.GetAbilityCount();
			int maxAbilityCount = unitEditorManager.GetMaxAbilityCount();
			abilityCounter.Set(abilityCount, maxAbilityCount);
			EquipedAbilityGrid.SetNewButtonState(abilityCount != maxAbilityCount);
		}

		internal void ShowRiders()
		{
			if (_showRiderRoutine != null)
			{
				StopCoroutine(_showRiderRoutine);
			}
			_showRiderRoutine = StartCoroutine(ShowRiderRoutine());
			IEnumerator ShowRiderRoutine()
			{
				if (_loadingIcon != null)
				{
					_loadingIcon.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.Loading);
				}
				yield return listSelectScreen.Setup(ContentDatabase.Instance().GetUserUnitBlueprintsByIdExcluded(unitEditorManager.GetCurrentID()).ToArray());
				NavigateToPage("VOICESELECT");
				_showRiderRoutine = null;
				if (_loadingIcon != null)
				{
					_loadingIcon.UpdateLoadingScreenState(CustomContentPageLoadingRefreshIcon.LoadingIconState.HaveContent);
				}
			}
		}

		public void UpdateRider(UnitBlueprint unit)
		{
			RiderUI.UpdateUI(unit);
		}

		public void SetupUnitPage(bool subUnit)
		{
			UnitPage.SetSavePageVisable(!subUnit);
			UnitPage.Setup(subUnit);
		}
	}
}
