using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DM;
using Landfall.TABS.AI.Components.Modifiers;
using Landfall.TABS.AI.Components.Tags;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.RuntimeCleanup;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using Sirenix.OdinInspector;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorManager : SerializedMonoBehaviour
	{
		[Serializable]
		public class UnitBaseWrapper
		{
			public UnitBlueprint UnitBaseBlueprint;

			public string BaseDisplayName;

			public Sprite BaseIcon;

			public CharacterItem.UnitBaseRestrictions UnitBaseRestriction;
		}

		[Serializable]
		public struct MovementTypeWrapper
		{
			public string DisplayName;
		}

		[Serializable]
		public struct TargetingTypeWrapper
		{
			public string DisplayName;
		}

		[Serializable]
		public class ClothingTypeWrapper
		{
			public UnitRig.GearType[] GearType;

			public string BaseDisplayName;

			public Sprite BaseIcon;

			private CharacterItem[] allCharacterItems;

			public CharacterItem[] GetCharacterItems(UnitEditorManager manager)
			{
				List<CharacterItem> list = new List<CharacterItem>();
				for (int i = 0; i < allCharacterItems.Length; i++)
				{
					CharacterItem characterItem = allCharacterItems[i];
					if (characterItem.UnitbaseAllowed(manager.GetUnitBase().UnitBaseRestriction))
					{
						list.Add(characterItem);
					}
				}
				return list.ToArray();
			}

			public void BakeItemList()
			{
				List<CharacterItem> list = new List<CharacterItem>(ContentDatabase.Instance().GetEditorVisibleCharacterItemsOfType(GearType[0]));
				for (int i = 1; i < GearType.Length; i++)
				{
					list.AddRange(ContentDatabase.Instance().GetEditorVisibleCharacterItemsOfType(GearType[i]));
				}
				allCharacterItems = list.ToArray();
			}
		}

		[Serializable]
		public class StatsWrapper
		{
			public enum UnitStat
			{
				HP = 0,
				Size = 1,
				Weight = 2,
				MoveSpeed = 3,
				AttackSpeedMultiplier = 4,
				DamageMultiplier = 5
			}

			public string name;

			public UnitStat unitStat;

			[Multiline]
			public string description;

			public float defaultValue;

			public float minValue = float.NegativeInfinity;

			public float maxValue = float.PositiveInfinity;

			public StatMode statsMode;

			private float currentValue;

			public float CurrentValue
			{
				get
				{
					return currentValue;
				}
				set
				{
					currentValue = value;
					if (unitStat == UnitStat.Size)
					{
						UnityEngine.Object.FindObjectOfType<UnitEditorManager>().RespawnUnit();
						Debug.Log($"Stat: {name} changed to: {value}. Respawning Unit");
					}
				}
			}

			public void Initialize()
			{
				currentValue = defaultValue;
			}

			public void Reset()
			{
				Initialize();
			}
		}

		public enum StatMode
		{
			Value = 0,
			Multiplier = 1
		}

		public enum WeaponMode
		{
			OneHanded = 0,
			TwoHanded = 1
		}

		[Serializable]
		public abstract class EquipedWrapper
		{
			public enum ColorClothingDataType
			{
				ColorData = 0,
				TeamColorData = 1,
				DefaultColor = 2
			}

			public enum WrapperType
			{
				Clothing = 0,
				Weapon = 1,
				Ability = 2
			}

			public CharacterItem prop;

			public CharacterItem spawnedProp;

			public PropItemData propData;

			public ColorClothingDataType GetClothingColor(int subIndex, UnitEditorColorPalette ColorPalette, out object data)
			{
				int num = propData.m_colors[subIndex];
				if (num != -1 && !propData.m_isTeamColor[subIndex])
				{
					if (num >= 0 && num < ColorPalette.Colors.Length)
					{
						ColorPaletteData colorPaletteData = ColorPalette.Colors[num];
						data = colorPaletteData;
						return ColorClothingDataType.ColorData;
					}
					data = spawnedProp.DefaultColors[subIndex];
					return ColorClothingDataType.DefaultColor;
				}
				if (num != -1)
				{
					TeamColorPaletteData teamColorPaletteData = ColorPalette.TeamColors[num];
					data = teamColorPaletteData;
					return ColorClothingDataType.TeamColorData;
				}
				data = spawnedProp.DefaultColors[subIndex];
				return ColorClothingDataType.DefaultColor;
			}

			public abstract WrapperType GetWrapperType();

			internal Color GetColor(int subIndex, UnitEditorColorPalette colorPalette, Team team)
			{
				int num = propData.m_colors[subIndex];
				if (num != -1 && !propData.m_isTeamColor[subIndex])
				{
					if (num >= 0 && num < colorPalette.Colors.Length)
					{
						return colorPalette.Colors[num].m_color;
					}
					return spawnedProp.DefaultColors[subIndex].m_material.color;
				}
				if (num != -1)
				{
					TeamColorPaletteData teamColorPaletteData = colorPalette.TeamColors[num];
					return teamColorPaletteData.GetColor(team);
				}
				return spawnedProp.DefaultColors[subIndex].m_material.color;
			}
		}

		[Serializable]
		public class EquipedWeaponWrapper : EquipedWrapper
		{
			public bool isRightHanded;

			public bool isRangedWeapon;

			public ProjectileEntity projectile;

			public EquipedWeaponWrapper(CharacterItem prop, CharacterItem spawnedProp, PropItemData propData, bool isRightHanded)
			{
				base.spawnedProp = spawnedProp;
				base.propData = propData;
				base.prop = prop;
				this.isRightHanded = isRightHanded;
				isRangedWeapon = prop.GetComponent<RangeWeapon>() != null;
				if (isRangedWeapon)
				{
					projectile = prop.GetComponent<RangeWeapon>().ObjectToSpawn.GetComponent<ProjectileEntity>();
				}
			}

			public override WrapperType GetWrapperType()
			{
				return WrapperType.Weapon;
			}
		}

		[Serializable]
		public class EquipedClothingWrapper : EquipedWrapper
		{
			public GameObject spawnedButton;

			public EquipedClothingWrapper(CharacterItem prop, CharacterItem spawnedProp, PropItemData propData)
			{
				base.spawnedProp = spawnedProp;
				base.propData = propData;
				base.prop = prop;
			}

			public override WrapperType GetWrapperType()
			{
				return WrapperType.Clothing;
			}
		}

		[Serializable]
		public class EquipedSpecialAbility : EquipedWrapper
		{
			public GameObject spawnedButton;

			public EquipedSpecialAbility(CharacterItem prop, CharacterItem spawnedProp, PropItemData propData)
			{
				base.spawnedProp = spawnedProp;
				base.propData = propData;
				base.prop = prop;
			}

			public override WrapperType GetWrapperType()
			{
				return WrapperType.Ability;
			}
		}

		public CameraSpinner m_CameraSpinner;

		public UnitEditorFreeCamera m_FreeCamera;

		public Camera m_Camera;

		public UnitEditorColorPalette UnitEditorColorPalette;

		public UnitEditorUIManager UIManager;

		public NavigableTMPTextInput nameField;

		public UnitEditorTeamToggle teamToggle;

		private CustomContentLoaderModIO m_customContentLoader;

		public MovementTypeWrapper[] MovementTypes;

		public List<IMovementComponent> MovementTypesComponents;

		public TargetingTypeWrapper[] TargetingTypes;

		public List<ITargetingComponent> TargetingTypesComponents;

		private WeaponMode m_WeaponMode;

		public UnitBaseWrapper[] UnitBases;

		public ClothingTypeWrapper[] ClothingTypes;

		public Transform UnitSpawnPoint;

		public StatsWrapper[] Stats;

		public UnitEditorPhotoUI photoUI;

		public UnitEditorPopup editorPopup;

		public UnitEditorUIManager unitEditorUIManager;

		[SerializeField]
		[Tooltip("Empty UI shown while the unit is busy saving. (It ensures the user doesn't press buttons in the main UI via the gamepad/keyboard.)")]
		protected UIComponentMainMenu emptySavingPopup;

		[SerializeField]
		private GameObject ExitTestPrompts;

		private List<EquipedClothingWrapper> equipedClothes = new List<EquipedClothingWrapper>();

		private List<EquipedSpecialAbility> equipedAbilities = new List<EquipedSpecialAbility>();

		private GameObject[] spawnedBase;

		private CharacterItem tempProp;

		private int currentUnitBase;

		private int currentMovementType;

		private int currentTargetingType;

		private float normalFOV;

		private Texture2D currentIcon;

		private VoiceBundle currentVoiceBundle;

		private DatabaseID m_currentRider;

		private bool m_hasRider;

		private const int maxEquipedClothes = 15;

		private const int maxEquipedAbilties = 5;

		private bool autoCaluculateCost = true;

		private UnitBlueprint loadedUnit;

		private EquipedWeaponWrapper mainHandWeapon;

		private EquipedWeaponWrapper offhandWeapon;

		private ProjectileEntity projectileMainHand;

		private ProjectileEntity projectileOffhand;

		private InputService inputService;

		private PlayerActions playerActions;

		private ModalPanel modalPanel;

		private UnitBlueprint isSubUnitTo;

		private LodGroupMerger lodGroupMerger;

		private int savingModalPanelOpenId;

		private const string DiscardMessage = "<size=70%>Are you sure you want to discard your changes?<size=100%>";

		public StatsWrapper[] WeaponStats;

		public ShaderParamaterAnimation groundAnimation;

		public Team currentTeam;

		private Vector3 m_testUnitCameraCachedPos;

		private Quaternion m_testUnitCameraCachedRot;

		public static bool isTestingUnit;

		private GameObject[] spawnedTestUnit;

		private GameObject selectedObject;

		private bool canGoBack = true;

		public WeaponMode WeaponHandMode => m_WeaponMode;

		public bool AutoCost
		{
			get
			{
				return autoCaluculateCost;
			}
			set
			{
				autoCaluculateCost = value;
			}
		}

		public ushort CustomCost { get; internal set; }

		public EquipedWeaponWrapper RightHandedWeapon
		{
			get
			{
				return mainHandWeapon;
			}
			set
			{
				mainHandWeapon = value;
			}
		}

		public EquipedWeaponWrapper LeftHandedWeapon
		{
			get
			{
				return offhandWeapon;
			}
			set
			{
				offhandWeapon = value;
			}
		}

		private UnitBaseWrapper GetUnitBase()
		{
			return UnitBases[currentUnitBase];
		}

		public void DestroyTemporary()
		{
			if ((bool)tempProp)
			{
				tempProp.Remove();
			}
			tempProp = null;
		}

		private void Awake()
		{
			isTestingUnit = false;
			m_customContentLoader = ServiceLocator.GetService<CustomContentLoaderModIO>();
			Time.timeScale = 1f;
			for (int i = 0; i < Stats.Length; i++)
			{
				Stats[i].Initialize();
			}
			photoUI.Setup(this);
		}

		public IEnumerator Start()
		{
			inputService = ServiceLocator.GetService<InputService>();
			modalPanel = ServiceLocator.GetService<ModalPanel>();
			playerActions = PlayerActions.Instance;
			if (inputService != null)
			{
				inputService.InputChanged += OnInputSourceChanged;
			}
			for (int i = 0; i < ClothingTypes.Length; i++)
			{
				ClothingTypes[i].BakeItemList();
			}
			UIManager.InitlizeUnitBaseButtons(UnitBases);
			UIManager.InitlizeClothingTypeButtons(ClothingTypes);
			UIManager.SetupUnitPage(subUnit: false);
			SwitchUnitBase(0);
			normalFOV = m_Camera.fieldOfView;
			EquipedVoiceBundle(null);
			SetRider(null);
			ServiceLocator.GetService<GameModeService>().SetGameMode<UnitCreatorGameMode>();
			CampaignPlayerDataHolder.SetToNone();
			yield return null;
			UnitBlueprint currentlyLoadingUnit = TABSSceneManager.CurrentlyLoadingUnit;
			if (currentlyLoadingUnit != null)
			{
				LoadUnit(currentlyLoadingUnit);
			}
			TABSSceneManager.CurrentlyLoadingUnit = null;
			ServiceLocator.GetService<MusicHandler>().PlayUnitCreatorMusic();
			ExitTestPrompts.SetActive(value: false);
		}

		private void OnInputSourceChanged(InputType type)
		{
			switch (type)
			{
			case InputType.Controller:
				m_CameraSpinner.UseController = true;
				break;
			case InputType.Keyboard:
			case InputType.Any:
				m_CameraSpinner.UseController = false;
				break;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			}
		}

		private void OnDestroy()
		{
			if (inputService != null)
			{
				inputService.InputChanged -= OnInputSourceChanged;
			}
			isTestingUnit = false;
		}

		public void SwitchUnitBase(int unitBaseIndex)
		{
			UnitBaseWrapper unitBase = UnitBases[unitBaseIndex];
			currentUnitBase = unitBaseIndex;
			RespawnUnit();
			UIManager.UpdateUnitBasePreview(unitBase);
		}

		private void SetStats(UnitBlueprint blueprint)
		{
			blueprint.health = GetStatValue(StatsWrapper.UnitStat.HP);
			blueprint.massMultiplier = GetStatValue(StatsWrapper.UnitStat.Weight);
			blueprint.sizeMultiplier = GetStatValue(StatsWrapper.UnitStat.Size);
			blueprint.attackSpeedMultiplier = GetStatValue(StatsWrapper.UnitStat.AttackSpeedMultiplier);
			blueprint.damageMultiplier = GetStatValue(StatsWrapper.UnitStat.DamageMultiplier);
			blueprint.maxSizeRandom = 1.2f;
			blueprint.minSizeRandom = 0.8f;
			blueprint.movementSpeedMuiltiplier = GetStatValue(StatsWrapper.UnitStat.MoveSpeed);
			blueprint.animationMultiplier = Mathf.Lerp(blueprint.movementSpeedMuiltiplier, 1f, 0.8f);
			blueprint.stepMultiplier = 1f / Mathf.Lerp(blueprint.movementSpeedMuiltiplier, 1f, 0.8f);
		}

		private void RespawnUnit()
		{
			if (spawnedBase != null)
			{
				for (int i = 0; i < spawnedBase.Length; i++)
				{
					UnityEngine.Object.Destroy(spawnedBase[i]);
				}
			}
			SetStats(UnitBases[currentUnitBase].UnitBaseBlueprint);
			UnitBases[currentUnitBase].UnitBaseBlueprint.forceNoRandomSize = true;
			if (m_hasRider)
			{
				UnitBases[currentUnitBase].UnitBaseBlueprint.SetCustomRider(m_currentRider);
			}
			else
			{
				UnitBases[currentUnitBase].UnitBaseBlueprint.SetNoCustomRider();
			}
			spawnedBase = UnitBases[currentUnitBase].UnitBaseBlueprint.Spawn(UnitSpawnPoint.position, UnitSpawnPoint.rotation, currentTeam);
			spawnedBase[0].GetComponentInChildren<HealthHandler>().SetInvulnerable(invulnerable: true);
			lodGroupMerger = spawnedBase[0].GetComponent<LodGroupMerger>();
			Debug.Log("Successfuly spawned units");
			RigidbodyHolder[] componentsInChildren = spawnedBase[0].GetComponentsInChildren<RigidbodyHolder>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].randomizeRigidbodySizes = false;
			}
			EyeSpawner[] componentsInChildren2 = spawnedBase[0].GetComponentsInChildren<EyeSpawner>();
			for (int k = 0; k < componentsInChildren2.Length; k++)
			{
				componentsInChildren2[k].randomizeEyes = false;
			}
			spawnedBase[0].GetComponentInChildren<Torso>().GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)10;
			RespawnProps();
			m_CameraSpinner.SetRigidbodyHolder(spawnedBase[0].GetComponentInChildren<RigidbodyHolder>());
		}

		public void RemoveWeapon(bool rightWeapon)
		{
			EquipedWeaponWrapper weapon = (rightWeapon ? RightHandedWeapon : LeftHandedWeapon);
			RemoveWeapon(weapon);
			UIManager.UpdateEquipedWeapons();
			if (rightWeapon)
			{
				projectileMainHand = null;
			}
			else
			{
				projectileOffhand = null;
			}
		}

		public void RemoveProp(EquipedWrapper wrapper)
		{
			if (wrapper.GetType() == typeof(EquipedClothingWrapper))
			{
				EquipedClothingWrapper equipedClothingWrapper = (EquipedClothingWrapper)wrapper;
				equipedClothes.Remove(equipedClothingWrapper);
				wrapper.spawnedProp.Remove();
				UnityEngine.Object.Destroy(equipedClothingWrapper.spawnedButton);
			}
			else if (wrapper.GetType() == typeof(EquipedWeaponWrapper))
			{
				EquipedWeaponWrapper weapon = (EquipedWeaponWrapper)wrapper;
				RemoveWeapon(weapon);
				UIManager.UpdateEquipedWeapons();
			}
			else if (wrapper.GetType() == typeof(EquipedSpecialAbility))
			{
				RemoveAbility((EquipedSpecialAbility)wrapper);
			}
			Debug.Log("Removing: " + wrapper.spawnedProp.gameObject.name, wrapper.spawnedProp.gameObject);
			UIManager.UpdateItemCounts();
		}

		private void RemoveAbility(EquipedSpecialAbility equipedSpecialAbility)
		{
			equipedAbilities.Remove(equipedSpecialAbility);
			UnityEngine.Object.Destroy(equipedSpecialAbility.spawnedProp.gameObject);
			UnityEngine.Object.Destroy(equipedSpecialAbility.spawnedButton);
		}

		public void RespawnProps()
		{
			for (int i = 0; i < equipedClothes.Count; i++)
			{
				equipedClothes[i].spawnedProp = SpawnProp(equipedClothes[i].prop, equipedClothes[i].propData);
			}
			for (int j = 0; j < equipedAbilities.Count; j++)
			{
				equipedAbilities[j].spawnedProp = SpawnAbility(equipedAbilities[j].prop);
			}
			RespawnWeapons();
		}

		public void RespawnWeapons()
		{
			if (RightHandedWeapon != null)
			{
				SpawnWeapon(RightHandedWeapon.prop, isRightHand: true, RightHandedWeapon.propData);
			}
			if (LeftHandedWeapon != null)
			{
				SpawnWeapon(LeftHandedWeapon.prop, isRightHand: false, LeftHandedWeapon.propData);
			}
		}

		public void EquipNewAbility(CharacterItem ability)
		{
			PropItemData propData = new PropItemData();
			CharacterItem spawnedProp = SpawnAbility(ability);
			EquipedSpecialAbility equipedSpecialAbility = new EquipedSpecialAbility(ability, spawnedProp, propData);
			equipedSpecialAbility.spawnedButton = UIManager.SpawnEquipedAbility(equipedSpecialAbility);
			equipedAbilities.Add(equipedSpecialAbility);
			UIManager.UpdateItemCounts();
		}

		public SpecialAbility SpawnAbility(CharacterItem ability)
		{
			Transform transform = spawnedBase[0].transform;
			return UnityEngine.Object.Instantiate(ability.gameObject, transform.position, transform.rotation, transform).GetComponent<SpecialAbility>();
		}

		public void EquipNewProp(CharacterItem prop)
		{
			if (equipedClothes.Count != 15)
			{
				PropItemData propItemData = new PropItemData();
				CharacterItem characterItem = SpawnProp(prop, propItemData);
				propItemData.m_colors = new int[characterItem.DefaultColors.Length];
				propItemData.m_isTeamColor = new bool[characterItem.DefaultColors.Length];
				for (int i = 0; i < propItemData.m_colors.Length; i++)
				{
					propItemData.m_isTeamColor[i] = characterItem.DefaultColors[i].m_hasTeamColor;
					propItemData.m_colors[i] = characterItem.DefaultColors[i].m_paletteIndex;
				}
				EquipedClothingWrapper equipedClothingWrapper = new EquipedClothingWrapper(prop, characterItem, propItemData);
				equipedClothes.Add(equipedClothingWrapper);
				equipedClothingWrapper.spawnedButton = UIManager.SpawnEquipedClothing(equipedClothingWrapper);
				UIManager.UpdateItemCounts();
			}
		}

		public void EquipNewProp(CharacterItem prop, PropItemData propData)
		{
			CharacterItem spawnedProp = SpawnProp(prop, propData);
			EquipedClothingWrapper equipedClothingWrapper = new EquipedClothingWrapper(prop, spawnedProp, propData);
			equipedClothes.Add(equipedClothingWrapper);
			equipedClothingWrapper.spawnedButton = UIManager.SpawnEquipedClothing(equipedClothingWrapper);
			UIManager.UpdateItemCounts();
		}

		public EquipedWeaponWrapper GetEquipedWeapon(bool isRight)
		{
			if (isRight)
			{
				return RightHandedWeapon;
			}
			return LeftHandedWeapon;
		}

		public void FlipWeapons()
		{
			CharacterItem characterItem = null;
			PropItemData propData = null;
			if (LeftHandedWeapon != null)
			{
				characterItem = LeftHandedWeapon.prop;
				propData = LeftHandedWeapon.propData;
			}
			CharacterItem characterItem2 = null;
			PropItemData propData2 = null;
			if (RightHandedWeapon != null)
			{
				characterItem2 = RightHandedWeapon.prop;
				propData2 = RightHandedWeapon.propData;
			}
			RemoveWeapon(LeftHandedWeapon);
			RemoveWeapon(RightHandedWeapon);
			if ((bool)characterItem)
			{
				SpawnWeapon(characterItem, isRightHand: true, propData);
			}
			if ((bool)characterItem2)
			{
				SpawnWeapon(characterItem2, isRightHand: false, propData2);
			}
		}

		public void RemoveWeapon(EquipedWeaponWrapper weapon)
		{
			if (weapon != null)
			{
				if (weapon.spawnedProp != null)
				{
					weapon.spawnedProp.Remove();
				}
				if (weapon.isRightHanded)
				{
					RightHandedWeapon = null;
				}
				else
				{
					LeftHandedWeapon = null;
				}
			}
		}

		public void SpawnWeapon(CharacterItem prop, bool isRightHand)
		{
			if (isRightHand && RightHandedWeapon != null && RightHandedWeapon.spawnedProp != null)
			{
				RemoveWeapon(RightHandedWeapon);
			}
			if (!isRightHand && LeftHandedWeapon != null && LeftHandedWeapon.spawnedProp != null)
			{
				RemoveWeapon(LeftHandedWeapon);
			}
			Unit component = spawnedBase[0].GetComponent<Unit>();
			if (component.unitBlueprint == null)
			{
				component.unitBlueprint = UnitBases[currentUnitBase].UnitBaseBlueprint;
			}
			component.unitBlueprint.holdinigWithTwoHands = m_WeaponMode != WeaponMode.OneHanded;
			PropItemData propItemData = new PropItemData();
			if (m_WeaponMode == WeaponMode.OneHanded || isRightHand)
			{
				HoldingHandler.HandType hand = HoldingHandler.HandType.Right;
				if (!isRightHand)
				{
					hand = HoldingHandler.HandType.Left;
				}
				CharacterItem characterItem = SpawnProp(prop, propItemData, hand);
				propItemData.m_colors = new int[characterItem.DefaultColors.Length];
				propItemData.m_isTeamColor = new bool[characterItem.DefaultColors.Length];
				for (int i = 0; i < propItemData.m_colors.Length; i++)
				{
					propItemData.m_isTeamColor[i] = characterItem.DefaultColors[i].m_hasTeamColor;
					propItemData.m_colors[i] = characterItem.DefaultColors[i].m_paletteIndex;
				}
				if (isRightHand)
				{
					RightHandedWeapon = new EquipedWeaponWrapper(prop, characterItem, propItemData, isRightHanded: true);
				}
				if (!isRightHand)
				{
					LeftHandedWeapon = new EquipedWeaponWrapper(prop, characterItem, propItemData, isRightHanded: false);
				}
			}
			else if (m_WeaponMode == WeaponMode.TwoHanded)
			{
				LeftHandedWeapon = new EquipedWeaponWrapper(prop, null, propItemData, isRightHanded: false);
			}
			UIManager.UpdateEquipedWeapons();
		}

		public void SpawnWeapon(CharacterItem prop, bool isRightHand, PropItemData propData)
		{
			if (isRightHand && RightHandedWeapon != null && RightHandedWeapon.spawnedProp != null)
			{
				RemoveWeapon(RightHandedWeapon);
			}
			if (!isRightHand && LeftHandedWeapon != null && LeftHandedWeapon.spawnedProp != null)
			{
				RemoveWeapon(LeftHandedWeapon);
			}
			Unit component = spawnedBase[0].GetComponent<Unit>();
			if (component.unitBlueprint == null)
			{
				component.unitBlueprint = UnitBases[currentUnitBase].UnitBaseBlueprint;
			}
			component.unitBlueprint.holdinigWithTwoHands = m_WeaponMode != WeaponMode.OneHanded;
			if (m_WeaponMode == WeaponMode.OneHanded || isRightHand)
			{
				HoldingHandler.HandType hand = HoldingHandler.HandType.Right;
				if (!isRightHand)
				{
					hand = HoldingHandler.HandType.Left;
				}
				CharacterItem characterItem = SpawnProp(prop, propData, hand);
				LODGroup[] componentsInChildren = characterItem.GetComponentsInChildren<LODGroup>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].ForceLOD(0);
				}
				if (isRightHand)
				{
					RightHandedWeapon = new EquipedWeaponWrapper(prop, characterItem, propData, isRightHanded: true);
				}
				else
				{
					LeftHandedWeapon = new EquipedWeaponWrapper(prop, characterItem, propData, isRightHanded: false);
				}
			}
			else if (m_WeaponMode == WeaponMode.TwoHanded)
			{
				LeftHandedWeapon = new EquipedWeaponWrapper(prop, null, propData, isRightHanded: false);
			}
			ConsolidateLodGroups();
			UIManager.UpdateEquipedWeapons();
		}

		public void EquipTemporaryProp(CharacterItem prop)
		{
			if (!isTestingUnit)
			{
				PropItemData propData = new PropItemData();
				DestroyTemporary();
				tempProp = SpawnProp(prop, propData);
			}
		}

		public CharacterItem SpawnProp(CharacterItem prop, PropItemData propData, HoldingHandler.HandType hand = HoldingHandler.HandType.Right)
		{
			if (spawnedBase.Length == 0 || spawnedBase[0] == null)
			{
				return null;
			}
			CharacterItem component;
			if (prop.GetComponent<Weapon>() == null)
			{
				component = spawnedBase[0].GetComponent<UnitRig>().SpawnProp(prop, propData, Stitcher.TransformCatalog.RigType.Human, currentTeam, null, isUnitEditor: true).GetComponent<CharacterItem>();
			}
			else
			{
				Quaternion rotation = spawnedBase[0].GetComponentInChildren<WeaponHandler>().transform.rotation;
				component = UnitBases[currentUnitBase].UnitBaseBlueprint.SetWeapon(spawnedBase[0].GetComponent<Unit>(), currentTeam, prop.gameObject, propData, hand, rotation, new List<GameObject>(), isUnitEditor: true).GetComponent<WeaponItem>();
				Weapon component2 = component.GetComponent<Weapon>();
				if (component2.GetType() == typeof(RangeWeapon))
				{
					RangeWeapon rangeWeapon = (RangeWeapon)component2;
					if (hand == HoldingHandler.HandType.Right)
					{
						if (projectileMainHand != null)
						{
							rangeWeapon.ObjectToSpawn = projectileMainHand.gameObject;
						}
					}
					else if (projectileOffhand != null)
					{
						rangeWeapon.ObjectToSpawn = projectileOffhand.gameObject;
					}
				}
			}
			LODGroup[] componentsInChildren = component.GetComponentsInChildren<LODGroup>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ForceLOD(0);
			}
			component.RegisterTeamCallbacks();
			component.SetTeamColors(currentTeam);
			ConsolidateLodGroups();
			return component;
		}

		public void SwitchUnitBase(GameObject unitBase)
		{
			int unitBaseIndex = GetUnitBaseIndex(unitBase);
			SwitchUnitBase(unitBaseIndex);
		}

		private int GetUnitBaseIndex(GameObject unitBase)
		{
			for (int i = 0; i < UnitBases.Length; i++)
			{
				if (unitBase == UnitBases[i].UnitBaseBlueprint.UnitBase)
				{
					return i;
				}
			}
			return 0;
		}

		public void ColorProp(EquipedWrapper equipedClothingWrapper, int submeshIndex, ColorPaletteData colorData)
		{
			UnityEngine.Object.FindObjectOfType<UnitEditorHighlightingManager>().StopBlinking(equipedClothingWrapper, submeshIndex);
			equipedClothingWrapper.spawnedProp.SetMaterial(colorData, submeshIndex);
			equipedClothingWrapper.propData.m_colors[submeshIndex] = colorData.ColorIndex;
			equipedClothingWrapper.propData.m_isTeamColor[submeshIndex] = false;
		}

		public void TeamColorProp(EquipedWrapper equipedClothingWrapper, int submeshIndex, TeamColorPaletteData colorData, Team team)
		{
			UnityEngine.Object.FindObjectOfType<UnitEditorHighlightingManager>().StopBlinking(equipedClothingWrapper, submeshIndex);
			equipedClothingWrapper.spawnedProp.SetMaterial(colorData, submeshIndex, team);
			equipedClothingWrapper.propData.m_colors[submeshIndex] = colorData.ColorIndex;
			equipedClothingWrapper.propData.m_isTeamColor[submeshIndex] = true;
		}

		public void ChangeWeaponMode(WeaponMode mode)
		{
			if (m_WeaponMode != mode)
			{
				m_WeaponMode = mode;
				FlipWeapons();
				FlipWeapons();
			}
		}

		public void ToggleWeaponMode()
		{
			switch (m_WeaponMode)
			{
			case WeaponMode.OneHanded:
				ChangeWeaponMode(WeaponMode.TwoHanded);
				break;
			case WeaponMode.TwoHanded:
				ChangeWeaponMode(WeaponMode.OneHanded);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			UIManager.UpdateEquipedWeapons();
		}

		public void ClearUnit()
		{
			loadedUnit = null;
			SetNewIcon(null);
			SetRider(null);
			RemoveWeapon(LeftHandedWeapon);
			RemoveWeapon(RightHandedWeapon);
			ClearClothes();
			for (int num = equipedAbilities.Count - 1; num >= 0; num--)
			{
				RemoveAbility(equipedAbilities[num]);
			}
			equipedAbilities.Clear();
			projectileOffhand = null;
			projectileMainHand = null;
			UIManager.ClearUnit();
			nameField.text = nameField.DefaultText;
			for (int i = 0; i < Stats.Length; i++)
			{
				Stats[i].Reset();
			}
			UIManager.UpdateStatUI();
			AutoCost = true;
			SwitchUnitBase(0);
			currentVoiceBundle = UnitBases[0].UnitBaseBlueprint.voiceBundle;
			UIManager.SelectVoiceBundle(currentVoiceBundle);
			SelectMovementType(0);
			SelectTargetingType(0);
			UIManager.UpdateItemCounts();
		}

		public void ClearClothes()
		{
			for (int num = equipedClothes.Count - 1; num >= 0; num--)
			{
				RemoveProp(equipedClothes[num]);
			}
			equipedClothes.Clear();
		}

		public void ToggleMovmentType()
		{
			int num = currentMovementType;
			num++;
			if (num >= MovementTypesComponents.Count)
			{
				num = 0;
			}
			SelectMovementType(num);
		}

		public void ToggleTargetingType()
		{
			int num = currentTargetingType;
			num++;
			if (num >= TargetingTypesComponents.Count)
			{
				num = 0;
			}
			SelectTargetingType(num);
		}

		public void SelectMovementType(int index)
		{
			currentMovementType = index;
			UIManager.UpdateMovementType(MovementTypes[index]);
		}

		public void SelectTargetingType(int index)
		{
			currentTargetingType = index;
			UIManager.UpdateTargetingType(TargetingTypes[index]);
		}

		private void LoadIcon(UnitBlueprint blueprint)
		{
			currentIcon = null;
			if ((bool)blueprint.Entity.SpriteIcon)
			{
				currentIcon = blueprint.Entity.SpriteIcon.texture;
			}
			UIManager.SetNewIcon(currentIcon);
		}

		private int GetMovementIndex(IMovementComponent component)
		{
			for (int i = 0; i < MovementTypesComponents.Count; i++)
			{
				if (component.GetType() == MovementTypesComponents[i].GetType())
				{
					return i;
				}
			}
			return 0;
		}

		private int GetTargetIndex(ITargetingComponent component)
		{
			for (int i = 0; i < TargetingTypesComponents.Count; i++)
			{
				if (component.GetType() == TargetingTypesComponents[i].GetType())
				{
					return i;
				}
			}
			return 0;
		}

		public void LoadUnit(UnitBlueprint blueprint)
		{
			ClearUnit();
			loadedUnit = blueprint;
			SelectMovementType(GetMovementIndex(loadedUnit.MovementComponents[0]));
			SelectTargetingType(GetTargetIndex(loadedUnit.TargetingComponent));
			LoadIcon(blueprint);
			Unit component = spawnedBase[0].GetComponent<Unit>();
			if (component.unitBlueprint == null)
			{
				component.unitBlueprint = UnitBases[currentUnitBase].UnitBaseBlueprint;
			}
			if (blueprint.HasMissingComponents || !blueprint.AllUnitPropsAndWeaponsColorsAreValid())
			{
				ServiceLocator.GetService<ModalPanel>().PopUp("CUSTOM_CONTENT_VALIDATION_FAILED_UNIT_ADDITIONAL_INFO");
			}
			m_WeaponMode = WeaponMode.OneHanded;
			if (blueprint.holdinigWithTwoHands)
			{
				m_WeaponMode = WeaponMode.TwoHanded;
			}
			component.unitBlueprint.holdinigWithTwoHands = blueprint.holdinigWithTwoHands;
			if ((bool)blueprint.RightWeapon)
			{
				SpawnWeapon(blueprint.RightWeapon.GetComponent<CharacterItem>(), isRightHand: true, blueprint.RightWeaponData);
			}
			if ((bool)blueprint.LeftWeapon)
			{
				SpawnWeapon(blueprint.LeftWeapon.GetComponent<CharacterItem>(), isRightHand: false, blueprint.LeftWeaponData);
			}
			for (int i = 0; i < blueprint.m_props.Length; i++)
			{
				if (blueprint.m_propData.Length <= i)
				{
					EquipNewProp(blueprint.m_props[i].GetComponent<CharacterItem>(), new PropItemData());
				}
				else
				{
					EquipNewProp(blueprint.m_props[i].GetComponent<CharacterItem>(), blueprint.m_propData[i]);
				}
			}
			if (blueprint.objectsToSpawnAsChildren != null)
			{
				for (int j = 0; j < blueprint.objectsToSpawnAsChildren.Length; j++)
				{
					EquipNewAbility(blueprint.objectsToSpawnAsChildren[j].GetComponent<SpecialAbility>());
				}
			}
			SetStatValue(StatsWrapper.UnitStat.HP, blueprint.health);
			SetStatValue(StatsWrapper.UnitStat.Weight, blueprint.massMultiplier);
			SetStatValue(StatsWrapper.UnitStat.Size, blueprint.sizeMultiplier);
			SetStatValue(StatsWrapper.UnitStat.MoveSpeed, blueprint.movementSpeedMuiltiplier);
			SetStatValue(StatsWrapper.UnitStat.AttackSpeedMultiplier, blueprint.attackSpeedMultiplier);
			SetStatValue(StatsWrapper.UnitStat.DamageMultiplier, blueprint.damageMultiplier);
			UIManager.SetName(blueprint.Entity.Name);
			UIManager.SetDescription(blueprint.UnitDescription);
			UIManager.UpdateStatUI();
			UIManager.SetupUnitPage(isSubUnitTo != null);
			SwitchUnitBase(blueprint.UnitBase);
			EquipedVoiceBundle(blueprint.voiceBundle);
			UIManager.SetPitch(blueprint.VoicePitch);
			projectileOffhand = blueprint.leftProjectile;
			projectileMainHand = blueprint.rightProjectile;
			UnitBlueprint rider = null;
			UnitBlueprint[] unitRiders = blueprint.UnitRiders;
			if (unitRiders != null && unitRiders.Length != 0)
			{
				rider = unitRiders[0];
			}
			SetRider(rider);
		}

		public void SetStatValue(StatsWrapper.UnitStat statType, float value)
		{
			for (int i = 0; i < Stats.Length; i++)
			{
				if (Stats[i].unitStat == statType)
				{
					Stats[i].CurrentValue = value;
					Debug.Log(string.Concat("Loaded stat: ", statType, "... new value: ", value));
					break;
				}
			}
		}

		public float GetStatValue(StatsWrapper.UnitStat statType)
		{
			for (int i = 0; i < Stats.Length; i++)
			{
				if (Stats[i].unitStat == statType)
				{
					return Stats[i].CurrentValue;
				}
			}
			return 1f;
		}

		private UnitBlueprint GetBlueprint()
		{
			UnitBlueprint unitBlueprint = new UnitBlueprint(UnitBases[currentUnitBase].UnitBaseBlueprint);
			unitBlueprint.name = UIManager.GetUnitName();
			unitBlueprint.forceNoRandomSize = false;
			if (RightHandedWeapon != null)
			{
				unitBlueprint.RightWeapon = RightHandedWeapon.prop.gameObject;
				unitBlueprint.RightWeaponData = RightHandedWeapon.propData;
				if ((bool)projectileMainHand)
				{
					unitBlueprint.rightProjectile = projectileMainHand;
				}
			}
			if (LeftHandedWeapon != null && !unitBlueprint.holdinigWithTwoHands)
			{
				unitBlueprint.LeftWeapon = LeftHandedWeapon.prop.gameObject;
				unitBlueprint.LeftWeaponData = LeftHandedWeapon.propData;
				if ((bool)projectileOffhand)
				{
					unitBlueprint.leftProjectile = projectileOffhand;
				}
			}
			unitBlueprint.m_props = new GameObject[equipedClothes.Count];
			unitBlueprint.m_propData = new PropItemData[equipedClothes.Count];
			for (int i = 0; i < unitBlueprint.m_props.Length; i++)
			{
				unitBlueprint.m_props[i] = equipedClothes[i].prop.gameObject;
				unitBlueprint.m_propData[i] = equipedClothes[i].propData;
			}
			GameObject[] array = new GameObject[equipedAbilities.Count];
			for (int j = 0; j < equipedAbilities.Count; j++)
			{
				array[j] = equipedAbilities[j].prop.gameObject;
			}
			unitBlueprint.objectsToSpawnAsChildren = array;
			SetStats(unitBlueprint);
			if (!AutoCost)
			{
				unitBlueprint.useCustomCost = true;
				unitBlueprint.customCost = CustomCost;
			}
			unitBlueprint.voiceBundle = currentVoiceBundle;
			unitBlueprint.VoicePitch = UIManager.GetCurrentPitch();
			unitBlueprint.MovementComponents = new List<IMovementComponent> { MovementTypesComponents[currentMovementType] };
			unitBlueprint.TargetingComponent = TargetingTypesComponents[currentTargetingType];
			if (m_hasRider)
			{
				unitBlueprint.SetCustomRider(m_currentRider);
			}
			return unitBlueprint;
		}

		public void SaveUnit()
		{
			SaveUnit(false);
		}

		public void SaveUnit(bool subUnit = false)
		{
			Debug.Log("Saving Unit");
			UnitBlueprint newBlueprint = GetBlueprint();
			newBlueprint.Entity.Name = UIManager.GetUnitName();
			newBlueprint.UnitDescription = UIManager.GetUnitDescrption();
			newBlueprint.SetIconTexture(currentIcon);
			if (subUnit)
			{
				OnCompleteSaveOverwrite(newBlueprint, loadMenuPage: false);
			}
			else if (loadedUnit != null)
			{
				if (!isTestingUnit)
				{
					Debug.Log("OVERWRITING UNIT");
					UIManager.OpenUIComponent(editorPopup);
					editorPopup.AskOverwrite(newBlueprint, delegate
					{
						OnCompleteSaveOverwrite(newBlueprint);
					}, delegate
					{
						OnCompleteSave(newBlueprint);
					});
				}
			}
			else
			{
				OnCompleteSave(newBlueprint);
			}
		}

		public void OnCompleteSave(UnitBlueprint newUnit)
		{
			PrepareToSaveUnit();
			CustomUnitHandler.SaveUnit(newUnit, default(DatabaseID), delegate
			{
				HandleSavingDone(clearUnitAndLoadCustomContentPage: true);
			});
		}

		public void OnCompleteSaveOverwrite(UnitBlueprint newUnit, bool loadMenuPage = true)
		{
			loadedUnit.Entity.InvalidateSprite();
			PrepareToSaveUnit();
			CustomUnitHandler.SaveUnit(newUnit, loadedUnit.Entity.GUID, delegate
			{
				HandleSavingDone(loadMenuPage);
			});
		}

		private void PrepareToSaveUnit()
		{
			UIManager.OpenUIComponent(emptySavingPopup);
			savingModalPanelOpenId = modalPanel.WaitPopUpWithFocus("POPUP_SAVING", -1f, null, null, true);
		}

		private async void HandleSavingDone(bool clearUnitAndLoadCustomContentPage)
		{
			await Task.Yield();
			await Task.Yield();
			if (savingModalPanelOpenId == modalPanel.OpenId)
			{
				modalPanel.CloseWaitPopup(restorePreviouslySelectedObject: false);
			}
			UIManager.OpenUIComponent(UIManager.UnitPage);
			m_customContentLoader.Refresh();
			if (clearUnitAndLoadCustomContentPage)
			{
				ClearUnit();
				LoadCustomContentPage();
			}
		}

		public void LoadMainMenu()
		{
			TABSSceneManager.LoadMainMenu();
		}

		public void TestUnit()
		{
			StartCoroutine(TestUnitCorutine());
		}

		private IEnumerator TestUnitCorutine()
		{
			UIManager.CenterUnit();
			UnitEditorRenderer component = m_Camera.GetComponent<UnitEditorRenderer>();
			m_testUnitCameraCachedPos = m_Camera.transform.position;
			m_testUnitCameraCachedRot = m_Camera.transform.rotation;
			component.SetFullscreen();
			groundAnimation.Animate();
			m_FreeCamera.transform.position = m_Camera.transform.position;
			m_FreeCamera.transform.rotation = m_Camera.transform.rotation;
			m_FreeCamera.ParentCamera(m_Camera);
			m_FreeCamera.SetMouseLook();
			m_CameraSpinner.enabled = false;
			m_FreeCamera.enabled = true;
			isTestingUnit = true;
			UnitBlueprint blueprint = GetBlueprint();
			spawnedTestUnit = blueprint.Spawn(Vector3.zero, Quaternion.Euler(0f, 180f, 0f), currentTeam);
			DestroyUnit();
			UIManager.NavigateToPage("UNIT");
			UIManager.Lock("UNIT");
			ExitTestPrompts.SetActive(value: true);
			UnityEngine.Object.FindObjectOfType<UnitEditorSpawnTestEnemies>().Spawn(blueprint.GetUnitCost(), TeamUtlity.GetOtherTeam(currentTeam));
			ServiceLocator.GetService<GameStateManager>().EnterBattleState();
			yield return null;
		}

		private void DestroyUnit()
		{
			for (int i = 0; i < spawnedBase.Length; i++)
			{
				UnityEngine.Object.Destroy(spawnedBase[i]);
				spawnedBase[i] = null;
			}
		}

		public void StopTestingUnit()
		{
			StartCoroutine(Delay());
			IEnumerator Delay()
			{
				UIManager.ResetUnitUI();
				m_FreeCamera.enabled = false;
				groundAnimation.AnimateBackwards();
				m_CameraSpinner.enabled = true;
				m_Camera.transform.parent = m_CameraSpinner.transform;
				m_Camera.transform.position = m_testUnitCameraCachedPos;
				m_Camera.transform.rotation = m_testUnitCameraCachedRot;
				FieldOfViewLerp component = m_Camera.GetComponent<FieldOfViewLerp>();
				component.targetFOV = normalFOV;
				component.lerpFactor = 8f;
				isTestingUnit = false;
				ServiceLocator.GetService<RuntimeGarbageCollector>().ForceFlushGC();
				for (int i = 0; i < spawnedTestUnit.Length; i++)
				{
					UnityEngine.Object.Destroy(spawnedTestUnit[i]);
				}
				StartCoroutine(StopTestingDelay());
				UnityEngine.Object.FindObjectOfType<UnitEditorSpawnTestEnemies>().Clear();
				ServiceLocator.GetService<GameStateManager>().EnterPlacementState();
				yield return null;
				UIManager.Unlock("UNIT");
				ExitTestPrompts.SetActive(value: false);
			}
		}

		private IEnumerator StopTestingDelay()
		{
			yield return null;
			RespawnUnit();
			yield return new WaitForSeconds(1f);
			if (!isTestingUnit)
			{
				m_Camera.GetComponent<UnitEditorRenderer>().SetSquare();
			}
		}

		public void ChangeTeamColor(Team team)
		{
			if (currentTeam != team)
			{
				currentTeam = team;
				RespawnUnit();
			}
		}

		private void Update()
		{
			if (playerActions.m_enterExitBattle.WasPressed)
			{
				if (isTestingUnit)
				{
					StopTestingUnit();
				}
				else
				{
					TestUnit();
				}
			}
			if (isTestingUnit)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			if (playerActions.m_toggleTeamColours.WasPressed && !isTestingUnit && teamToggle != null)
			{
				switch (currentTeam)
				{
				case Team.Red:
					teamToggle.SetButtonSelected(Team.Blue);
					break;
				case Team.Blue:
					teamToggle.SetButtonSelected(Team.Red);
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		public void EnterPhotoMode()
		{
			UnitEditorRenderer component = m_Camera.GetComponent<UnitEditorRenderer>();
			UIManager.CenterUnit();
			component.SetFullscreen();
			photoUI.AnimateIn();
		}

		public void ExitPhotoMode()
		{
			UIManager.ResetUnitUI();
			StartCoroutine(StopTestingDelay());
			photoUI.AnimateOut();
			photoUI.Close?.Invoke();
		}

		public void TakePhoto()
		{
			Texture2D newIcon = photoUI.TakePhoto();
			SetNewIcon(newIcon);
			ExitPhotoMode();
		}

		public void SetNewIcon(Texture2D tex2D)
		{
			UIManager.SetNewIcon(tex2D);
			currentIcon = tex2D;
		}

		public void SaveIcon(string path)
		{
			if (currentIcon == null)
			{
				throw new Exception();
			}
			byte[] bytes = currentIcon.EncodeToPNG();
			ServiceLocator.GetService<FileIOWrapper>().WriteAllBytes(path, bytes, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
		}

		public uint GetAutoCost()
		{
			return GetBlueprint().GetAutoCost();
		}

		private IEnumerator GoBackCooldown()
		{
			canGoBack = false;
			for (int i = 0; i < 5; i++)
			{
				yield return null;
			}
			canGoBack = true;
		}

		public void DiscardUnit()
		{
			if (!canGoBack)
			{
				return;
			}
			StartCoroutine(GoBackCooldown());
			if (isTestingUnit)
			{
				return;
			}
			if (isSubUnitTo != null)
			{
				UIManager.OpenUIComponent(editorPopup);
				editorPopup.AskSaveSubunit(UIManager.GetUnitName(), delegate
				{
					LoadMainUnit(saveSubUnit: false);
				}, delegate
				{
					LoadMainUnit(saveSubUnit: true);
				}, delegate
				{
					UIManager.OpenUIComponent(UIManager.UnitPage);
				});
			}
			else
			{
				UIManager.OpenUIComponent(editorPopup);
				editorPopup.AskDiscard(DiscardComplete);
			}
		}

		private void DiscardComplete()
		{
			ClearUnit();
			LoadCustomContentPage();
		}

		public void EquipedVoiceBundle(VoiceBundle voiceBundle)
		{
			if (voiceBundle != null)
			{
				currentVoiceBundle = voiceBundle;
			}
			else
			{
				currentVoiceBundle = UnitBases[0].UnitBaseBlueprint.voiceBundle;
				Debug.Log("Equiping Standard Voice Bundle");
			}
			UIManager.SelectVoiceBundle(currentVoiceBundle);
		}

		public VoiceBundle GetVoiceBundle()
		{
			return currentVoiceBundle;
		}

		public float GetCurrentPitch()
		{
			return UIManager.GetCurrentPitch();
		}

		public void EquipProjectile(ProjectileEntity projectileEntity, bool isMainHand)
		{
			if (isMainHand)
			{
				projectileMainHand = projectileEntity;
			}
			else
			{
				projectileOffhand = projectileEntity;
			}
		}

		public ProjectileEntity GetProjectile(EquipedWeaponWrapper weapon)
		{
			if (weapon.isRightHanded)
			{
				if (projectileMainHand != null)
				{
					return projectileMainHand;
				}
				return weapon.projectile;
			}
			if (projectileOffhand != null)
			{
				return projectileOffhand;
			}
			return weapon.projectile;
		}

		public void LoadCustomContentPage()
		{
			TABSSceneManager.LoadCustomContentPage();
		}

		public int GetClothingCount()
		{
			return equipedClothes.Count;
		}

		public int GetAbilityCount()
		{
			return equipedAbilities.Count;
		}

		public int GetMaxClothingCount()
		{
			return 15;
		}

		public int GetMaxAbilityCount()
		{
			return 5;
		}

		public void SetRider(UnitBlueprint m_unit)
		{
			if (m_unit == null)
			{
				m_hasRider = false;
			}
			else
			{
				m_currentRider = m_unit.Entity.GUID;
				m_hasRider = true;
			}
			UIManager.UpdateRider(m_unit);
			RespawnUnit();
		}

		public void LoadRiderSubUnit()
		{
			if (m_hasRider)
			{
				isSubUnitTo = GetBlueprint();
				if ((bool)loadedUnit)
				{
					isSubUnitTo.Entity.GUID = loadedUnit.Entity.GUID;
				}
				else
				{
					isSubUnitTo.Entity.GenerateNewID();
				}
				isSubUnitTo.Entity.Name = UIManager.GetUnitName();
				isSubUnitTo.SetIconTexture(currentIcon);
				isSubUnitTo.UnitDescription = UIManager.GetUnitDescrption();
				Debug.Log("Loading Sub Unit!");
				LoadUnit(ContentDatabase.Instance().GetUnitBlueprint(m_currentRider));
			}
		}

		public void LoadMainUnit(bool saveSubUnit)
		{
			UIManager.OpenUIComponent(UIManager.UnitPage);
			if (isSubUnitTo != null)
			{
				if (saveSubUnit)
				{
					SaveUnit(subUnit: true);
				}
				UnitBlueprint blueprint = isSubUnitTo;
				isSubUnitTo = null;
				LoadUnit(blueprint);
			}
		}

		public DatabaseID GetCurrentID()
		{
			if (!loadedUnit)
			{
				return default(DatabaseID);
			}
			return loadedUnit.Entity.GUID;
		}

		private void ConsolidateLodGroups()
		{
			if (lodGroupMerger != null)
			{
				lodGroupMerger.ConsolidateChildLodGroups();
			}
		}
	}
}
