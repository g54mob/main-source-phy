using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DM;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEditorHandler : MonoBehaviour
{
	private static UnitEditorHandler _instance;

	[SerializeField]
	private GameObject m_unitRoot;

	[SerializeField]
	private UnitEditorMenuButton m_clothesButton;

	[SerializeField]
	private UnitEditorMenuButton m_weaponsButton;

	[SerializeField]
	private UnitEditorMenuButton m_statsButton;

	[SerializeField]
	private TMP_InputField m_unitNameInput;

	[SerializeField]
	private Button m_factionButton;

	[SerializeField]
	private Button m_saveButton;

	[SerializeField]
	private Button m_loadButton;

	[SerializeField]
	private Button m_testButton;

	[SerializeField]
	private Button m_backButton;

	[SerializeField]
	private UnitEditorTestScene m_testScene;

	[SerializeField]
	private Gradient m_highlightGradient;

	[SerializeField]
	private Camera m_mainCamera;

	[SerializeField]
	private Vector3 m_cameraPosDefault = new Vector3(-0.35f, 0f, -8.5f);

	[SerializeField]
	private Vector3 m_cameraPosHead;

	[SerializeField]
	private Vector3 m_cameraPosTorso;

	[SerializeField]
	private Vector3 m_cameraPosWaist;

	[SerializeField]
	private Vector3 m_cameraPosLegs;

	[SerializeField]
	private Vector3 m_cameraPosFeet;

	private Vector3 m_targetCameraPos;

	private Vector3 m_categoryCameraPos;

	private float m_scrollOffset;

	private Transform m_cameraSpinner;

	private UnitBlueprint m_currentBlueprint;

	private List<CharacterItem> m_equippedProps = new List<CharacterItem>();

	private UnitEditorContextMenu m_contextMenu;

	private UnitEditorEquippedList m_equippedList;

	private CharacterItem m_hoverProp;

	private CharacterItem m_temporaryProp;

	private Material m_highlightMaterial;

	private TeamColor m_unitTeamColor;

	public string LastLoadedUnitName;

	private float counter;

	private Color unEditedHoverColor;

	private Color editedHoverColor;

	public bool m_hasWeapon;

	public static UnitEditorHandler Instance => _instance;

	public GameObject UnitRoot => m_unitRoot;

	public List<CharacterItem> EquippedProps => m_equippedProps;

	public UnitEditorContextMenu ContextMenu => m_contextMenu;

	public UnitEditorEquippedList EquippedList => m_equippedList;

	public UnitBlueprint CurrentLoadedCustomUnit { get; private set; }

	private void Awake()
	{
		if (_instance != null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		_instance = this;
		Time.timeScale = 1f;
		m_targetCameraPos = m_cameraPosDefault;
		m_categoryCameraPos = m_cameraPosDefault;
		m_cameraSpinner = m_mainCamera.transform.parent;
		m_contextMenu = GetComponent<UnitEditorContextMenu>();
		m_equippedList = GetComponent<UnitEditorEquippedList>();
		ContentDatabase contentDatabase = ContentDatabase.Instance();
		IEnumerable<CharacterItem> heads = contentDatabase.GetEditorVisibleCharacterItemsOfType(UnitRig.GearType.HEAD);
		IEnumerable<CharacterItem> necks = contentDatabase.GetEditorVisibleCharacterItemsOfType(UnitRig.GearType.NECK);
		IEnumerable<CharacterItem> shoulders = contentDatabase.GetEditorVisibleCharacterItemsOfType(UnitRig.GearType.SHOULDER);
		IEnumerable<CharacterItem> torsos = contentDatabase.GetEditorVisibleCharacterItemsOfType(UnitRig.GearType.TORSO);
		IEnumerable<CharacterItem> arms = contentDatabase.GetEditorVisibleCharacterItemsOfType(UnitRig.GearType.ARMS);
		IEnumerable<CharacterItem> wrists = contentDatabase.GetEditorVisibleCharacterItemsOfType(UnitRig.GearType.WRISTS);
		IEnumerable<CharacterItem> hands = contentDatabase.GetEditorVisibleCharacterItemsOfType(UnitRig.GearType.HANDS);
		IEnumerable<CharacterItem> waist = contentDatabase.GetEditorVisibleCharacterItemsOfType(UnitRig.GearType.WAIST);
		IEnumerable<CharacterItem> legs = contentDatabase.GetEditorVisibleCharacterItemsOfType(UnitRig.GearType.LEGS);
		IEnumerable<CharacterItem> feet = contentDatabase.GetEditorVisibleCharacterItemsOfType(UnitRig.GearType.FEET);
		IEnumerable<WeaponItem> meleeWeapons = contentDatabase.GetEditorVisibleWeaponItemsOfType<MeleeWeapon>();
		IEnumerable<WeaponItem> rangeWeapons = contentDatabase.GetEditorVisibleWeaponItemsOfType<RangeWeapon>();
		m_clothesButton.buttonsNames = new string[10] { "Head", "Neck", "Shoulder", "Torso", "Arm", "Wrist", "Hands", "Waist", "Legs", "Feet" };
		m_clothesButton.m_getButtonsCallbackSmallE = new UnitEditorMenuButton.GetButtonsCallback[10]
		{
			delegate
			{
				SetCameraPosition(UnitRig.GearType.HEAD, setCategory: true);
				RemoveTemporary();
				return heads.ToList();
			},
			delegate
			{
				SetCameraPosition(UnitRig.GearType.NECK, setCategory: true);
				RemoveTemporary();
				return necks.ToList();
			},
			delegate
			{
				SetCameraPosition(UnitRig.GearType.SHOULDER, setCategory: true);
				RemoveTemporary();
				return shoulders.ToList();
			},
			delegate
			{
				SetCameraPosition(UnitRig.GearType.TORSO, setCategory: true);
				RemoveTemporary();
				return torsos.ToList();
			},
			delegate
			{
				SetCameraPosition(UnitRig.GearType.ARMS, setCategory: true);
				RemoveTemporary();
				return arms.ToList();
			},
			delegate
			{
				SetCameraPosition(UnitRig.GearType.WRISTS, setCategory: true);
				RemoveTemporary();
				return wrists.ToList();
			},
			delegate
			{
				SetCameraPosition(UnitRig.GearType.HANDS, setCategory: true);
				RemoveTemporary();
				return hands.ToList();
			},
			delegate
			{
				SetCameraPosition(UnitRig.GearType.WAIST, setCategory: true);
				RemoveTemporary();
				return waist.ToList();
			},
			delegate
			{
				SetCameraPosition(UnitRig.GearType.LEGS, setCategory: true);
				RemoveTemporary();
				return legs.ToList();
			},
			delegate
			{
				SetCameraPosition(UnitRig.GearType.FEET, setCategory: true);
				RemoveTemporary();
				return feet.ToList();
			}
		};
		m_weaponsButton.buttonsNames = new string[2] { "Melee", "Range" };
		m_weaponsButton.m_getButtonsCallbackSmallE = new UnitEditorMenuButton.GetButtonsCallback[2]
		{
			delegate
			{
				SetCameraPosition(UnitRig.GearType.WEAPON, setCategory: true);
				RemoveTemporary();
				return ((IEnumerable<CharacterItem>)meleeWeapons).ToList();
			},
			delegate
			{
				SetCameraPosition(UnitRig.GearType.WEAPON, setCategory: true);
				RemoveTemporary();
				return ((IEnumerable<CharacterItem>)rangeWeapons).ToList();
			}
		};
		m_statsButton.buttonsNames = new string[0];
		m_statsButton.m_onClick = delegate
		{
			Debug.Log("OPEN STATS");
		};
		m_factionButton.onClick.AddListener(delegate
		{
			UnitCreatorUIHandler.Instance.Toggle(UnitCreatorScreen.Faction);
		});
		m_saveButton.onClick.AddListener(delegate
		{
			UnitCreatorUIHandler.Instance.Toggle(UnitCreatorScreen.Save);
		});
		m_loadButton.onClick.AddListener(delegate
		{
			UnitCreatorUIHandler.Instance.Toggle(UnitCreatorScreen.Load);
		});
		m_testButton.onClick.AddListener(StartTestScene);
		m_backButton.onClick.AddListener(TABSSceneManager.LoadMainMenu);
		m_currentBlueprint = new UnitBlueprint(contentDatabase.GetUnitEditorBlueprint());
		m_unitRoot.GetComponent<Unit>().data.immunityForSeconds = float.PositiveInfinity;
		m_unitRoot.GetComponentInChildren<RigidbodyHolder>().randomizeRigidbodySizes = false;
		m_unitRoot.GetComponent<EyeSpawner>().randomizeEyes = false;
		m_unitTeamColor = m_unitRoot.GetComponentInChildren<TeamColor>();
		UnitEditorColorPalette unitEditorColorPalette = ContentDatabase.Instance().GetUnitEditorColorPalette();
		m_highlightMaterial = unitEditorColorPalette.HighlightMaterial;
	}

	private void Update()
	{
		if (m_highlightMaterial != null)
		{
			counter += Time.deltaTime;
			if (counter > 0.1f)
			{
				counter = 0f;
			}
			m_highlightMaterial.color = unEditedHoverColor * (Mathf.Cos(Time.time * 15f) + 4f) / 4f;
		}
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			m_scrollOffset -= Input.mouseScrollDelta.y * 0.2f;
			m_scrollOffset = Mathf.Clamp(m_scrollOffset, 0f, 1f);
		}
		Vector3 b = Vector3.Lerp(m_targetCameraPos, m_cameraPosDefault, m_scrollOffset);
		Vector3 localPosition = m_mainCamera.transform.localPosition;
		Vector3 localPosition2 = m_cameraSpinner.localPosition;
		Vector3 vector = Vector3.Lerp(new Vector3(localPosition.x, localPosition2.y, localPosition.z), b, Time.deltaTime * 5f);
		localPosition.x = vector.x;
		localPosition2.y = vector.y;
		localPosition.z = vector.z;
		m_mainCamera.transform.localPosition = localPosition;
		m_cameraSpinner.transform.localPosition = localPosition2;
	}

	public void SetHoverColor(Color c)
	{
		unEditedHoverColor = c;
		editedHoverColor = c * 1.2f;
	}

	private void OnDestroy()
	{
		if (m_highlightMaterial != null)
		{
			m_highlightMaterial.color = Color.white;
		}
	}

	public void SetCameraPosition(UnitRig.GearType gearType, bool setCategory = false)
	{
		switch (gearType)
		{
		case UnitRig.GearType.HEAD:
			m_targetCameraPos = m_cameraPosHead;
			break;
		case UnitRig.GearType.NECK:
			m_targetCameraPos = m_cameraPosHead;
			break;
		case UnitRig.GearType.SHOULDER:
			m_targetCameraPos = m_cameraPosTorso;
			break;
		case UnitRig.GearType.TORSO:
			m_targetCameraPos = m_cameraPosTorso;
			break;
		case UnitRig.GearType.ARMS:
			m_targetCameraPos = m_cameraPosTorso;
			break;
		case UnitRig.GearType.WRISTS:
			m_targetCameraPos = m_cameraPosTorso;
			break;
		case UnitRig.GearType.HANDS:
			m_targetCameraPos = m_cameraPosTorso;
			break;
		case UnitRig.GearType.WAIST:
			m_targetCameraPos = m_cameraPosWaist;
			break;
		case UnitRig.GearType.LEGS:
			m_targetCameraPos = m_cameraPosLegs;
			break;
		case UnitRig.GearType.FEET:
			m_targetCameraPos = m_cameraPosFeet;
			break;
		case UnitRig.GearType.WEAPON:
			m_targetCameraPos = m_cameraPosDefault;
			break;
		default:
			m_targetCameraPos = m_cameraPosDefault;
			break;
		}
		if (setCategory)
		{
			m_categoryCameraPos = m_targetCameraPos;
		}
	}

	public void ResetCameraPosition(bool forceCategory = false)
	{
		if (forceCategory)
		{
			m_targetCameraPos = m_categoryCameraPos;
			return;
		}
		m_targetCameraPos = m_cameraPosDefault;
		m_categoryCameraPos = m_cameraPosDefault;
		m_scrollOffset = 0f;
	}

	public void RemoveTemporary()
	{
		if (m_temporaryProp != null)
		{
			m_temporaryProp.Remove();
		}
		if (m_hoverProp != null)
		{
			m_hoverProp.Remove();
		}
		m_temporaryProp = null;
		m_hoverProp = null;
		UnitEditorItemButton.TurnOffCurrentPropButton();
	}

	public void SetHoverPropTemporary()
	{
		if (m_temporaryProp != null)
		{
			m_temporaryProp.Remove();
		}
		m_temporaryProp = m_hoverProp;
		m_hoverProp = null;
	}

	private CharacterItem SpawnProp(CharacterItem prop, PropItemData propData)
	{
		CharacterItem component;
		if (prop.GetComponent<Weapon>() == null)
		{
			component = m_unitRoot.GetComponent<UnitRig>().SpawnProp(prop, propData, Stitcher.TransformCatalog.RigType.Human, UnitEditorTeamButtons._CurrentTeam, null, isUnitEditor: true).GetComponent<CharacterItem>();
		}
		else
		{
			if (m_hasWeapon)
			{
				return null;
			}
			m_hasWeapon = true;
			Quaternion rotation = m_unitRoot.GetComponentInChildren<WeaponHandler>().transform.rotation;
			component = m_currentBlueprint.SetWeapon(m_unitRoot.GetComponent<Unit>(), UnitEditorTeamButtons._CurrentTeam, prop.gameObject, propData, HoldingHandler.HandType.Right, rotation, new List<GameObject>(), isUnitEditor: true).GetComponent<WeaponItem>();
		}
		component.RegisterTeamCallbacks();
		component.SetTeamColors(UnitEditorTeamButtons._CurrentTeam);
		return component;
	}

	public void AddHoverProp(CharacterItem prop)
	{
		RemoveHoverProp();
		if (!(m_temporaryProp != null) || !(prop.Entity.GUID == m_temporaryProp.Entity.GUID))
		{
			m_hoverProp = SpawnProp(prop, new PropItemData());
			SetTemporaryPropVisibility(visible: false);
		}
	}

	public void RemoveHoverProp()
	{
		if (m_hoverProp != null)
		{
			m_hoverProp.Remove();
		}
		m_hoverProp = null;
		SetTemporaryPropVisibility(visible: true);
	}

	public void EquipSpawnedProp(CharacterItem prop = null)
	{
		if (!(m_temporaryProp == null) || !(prop == null))
		{
			if (prop != null)
			{
				m_temporaryProp = prop;
			}
			m_equippedProps.Add(m_temporaryProp);
			m_equippedList.AddEquipped(m_temporaryProp);
			m_temporaryProp.IsReallyEquipped = true;
			m_temporaryProp = null;
		}
	}

	public void RemoveEquipedProp(CharacterItem prop)
	{
		if (!(prop == null))
		{
			m_equippedProps.Remove(prop);
			prop.Remove();
		}
	}

	private void SetTemporaryPropVisibility(bool visible)
	{
		if (m_temporaryProp != null)
		{
			m_temporaryProp.SetVisibility(visible);
		}
	}

	public void SaveUnit(string unitName, bool temp = false, DatabaseID useThisID = default(DatabaseID), Action doneCallback = null)
	{
		string tempFolderPath = CustomContentFilePaths.GetTempFolderPath();
		string path = UnitBlueprint.GetCustomUnitPath(unitName);
		if (temp)
		{
			path = Path.Combine(tempFolderPath, unitName + CustomContentFilePaths.FileEndingUnit);
		}
		FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
		fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
		{
			SaveUnitOnExists(unitName, temp, useThisID, fileIO, path, exists, doneCallback);
		});
	}

	private void SaveUnitOnExists(string unitName, bool temp, DatabaseID useThisID, FileIOWrapper fileIO, string path, bool exists, Action doneCallback)
	{
		if (exists && !temp)
		{
			fileIO.ReadAllText(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string jsonDataOld, Exception readException)
			{
				if (string.IsNullOrEmpty(jsonDataOld))
				{
					Debug.LogErrorFormat("Failed to load: {0}", path);
					doneCallback?.Invoke();
				}
				else
				{
					SerializedUnitBlueprint serializedUnitBlueprint = JsonUtility.FromJson<SerializedUnitBlueprint>(jsonDataOld);
					m_currentBlueprint.Entity.Name = unitName;
					m_currentBlueprint.Entity.GUID = serializedUnitBlueprint.m_id;
					SaveUnitOnGotNameAndId(unitName, temp, fileIO, path, exists, doneCallback);
				}
			});
		}
		else
		{
			m_currentBlueprint.Entity.Name = unitName;
			m_currentBlueprint.Entity.GenerateNewID();
			if (useThisID != default(DatabaseID))
			{
				m_currentBlueprint.Entity.GUID = useThisID;
			}
			SaveUnitOnGotNameAndId(unitName, temp, fileIO, path, exists, doneCallback);
		}
	}

	private void SaveUnitOnGotNameAndId(string unitName, bool temp, FileIOWrapper fileIO, string path, bool exists, Action doneCallback)
	{
		Debug.LogFormat("Saving Unit With ID: {0}   Current Loaded Unit: {1}", m_currentBlueprint.Entity.GUID, (CurrentLoadedCustomUnit != null) ? CurrentLoadedCustomUnit.Entity.GUID.ToString() : "null");
		SaveClothes();
		SaveWeapons();
		if (exists && !temp && LastLoadedUnitName != unitName)
		{
			Action tempDoneCallback = doneCallback;
			string question = $"Unit: {unitName} Does Already Exist, Do You Want To Overwrite?";
			ServiceLocator.GetService<ModalPanel>().Choice("POPUP_SAVEANDREPLACE_TITLE", question, delegate
			{
				SaveUnitOnOverwrite(unitName, fileIO, path, quickRefresh: false, doneCallback);
			}, delegate
			{
				tempDoneCallback?.Invoke();
			});
		}
		else
		{
			SaveUnitOnOverwrite(unitName, fileIO, path, quickRefresh: true, doneCallback);
		}
	}

	private void SaveUnitOnOverwrite(string unitName, FileIOWrapper fileIO, string path, bool quickRefresh, Action doneCallback)
	{
		Debug.LogFormat("Saving unit: {0}", path);
		try
		{
			string contents = JsonUtility.ToJson(m_currentBlueprint.SerializedUnit(default(DatabaseID)), prettyPrint: true);
			fileIO.WriteAllText(path, contents, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception writeException)
			{
				if (writeException != null)
				{
					Debug.LogErrorFormat("Failed to save: {0}", path);
					doneCallback?.Invoke();
				}
				else
				{
					LastLoadedUnitName = unitName;
					if (quickRefresh)
					{
						ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Unit, null);
					}
					doneCallback?.Invoke();
				}
			});
		}
		catch (Exception ex)
		{
			Debug.LogErrorFormat("Failed to save: {0}\n{1}", path, ex);
			doneCallback?.Invoke();
		}
	}

	public DatabaseID[] GetClothesIDs()
	{
		return (from item in GetClothes()
			select item.Entity.GUID).ToArray();
	}

	public DatabaseID GetWeaponID()
	{
		List<CharacterItem> weapons = GetWeapons();
		if (weapons != null && weapons.Count > 0)
		{
			return weapons[0].Entity.GUID;
		}
		return default(DatabaseID);
	}

	private List<CharacterItem> GetClothes()
	{
		return Instance.m_equippedProps.Where((CharacterItem item) => item.gameObject.GetComponent<PropItem>() != null).ToList();
	}

	private List<CharacterItem> GetWeapons()
	{
		return Instance.m_equippedProps.Where((CharacterItem item) => item.gameObject.GetComponent<Weapon>() != null).ToList();
	}

	private GameObject[] GetProps(List<CharacterItem> items)
	{
		return items.Select((CharacterItem item) => item.gameObject).ToArray();
	}

	public PropItemData[] GetClothesData()
	{
		List<CharacterItem> clothes = GetClothes();
		return GetPropData(clothes);
	}

	public PropItemData GetWeaponData()
	{
		List<CharacterItem> weapons = GetWeapons();
		if (weapons != null && weapons.Count > 0)
		{
			return weapons[0].PropData;
		}
		return null;
	}

	public void SetTeamColors(Team team)
	{
		m_unitTeamColor.SetTeamColor(team);
	}

	private PropItemData[] GetPropData(List<CharacterItem> items)
	{
		PropItemData[] array = new PropItemData[items.Count];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = items[i].PropData;
		}
		return array;
	}

	private void SaveClothes()
	{
		List<CharacterItem> clothes = GetClothes();
		m_currentBlueprint.m_props = GetProps(clothes);
		m_currentBlueprint.m_propData = GetPropData(clothes);
	}

	public void SaveClothes(UnitBlueprint blueprint)
	{
		List<CharacterItem> clothes = GetClothes();
		blueprint.m_props = GetProps(clothes);
		blueprint.m_propData = GetPropData(clothes);
	}

	private void SaveWeapons()
	{
		List<CharacterItem> weapons = GetWeapons();
		if (weapons == null || weapons.Count < 1)
		{
			m_currentBlueprint.RightWeapon = null;
			m_currentBlueprint.RightWeaponData = null;
		}
		else
		{
			m_currentBlueprint.RightWeapon = weapons[0].gameObject;
			m_currentBlueprint.RightWeaponData = weapons[0].PropData;
		}
	}

	public void LoadUnit(UnitBlueprint wrapper, Action doneCallback)
	{
		string filePath = wrapper.FilePath;
		CurrentLoadedCustomUnit = wrapper;
		ServiceLocator.GetService<FileIOWrapper>().FileExists(filePath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
		{
			if (!exists)
			{
				doneCallback?.Invoke();
			}
			else
			{
				m_currentBlueprint.SetValues(wrapper);
				m_currentBlueprint.Entity.GUID = wrapper.Entity.GUID;
				RemoveTemporary();
				ClearUnit();
				LoadClothes();
				LoadWeapons();
				LastLoadedUnitName = wrapper.Name;
				doneCallback?.Invoke();
			}
		});
	}

	public void ClearUnit()
	{
		int num;
		for (num = 0; num < m_equippedProps.Count; num++)
		{
			RemoveEquipedProp(m_equippedProps[num]);
			num--;
		}
	}

	public void LoadClothes()
	{
		for (int i = 0; i < m_currentBlueprint.m_props.Length; i++)
		{
			CharacterItem component = m_currentBlueprint.m_props[i].GetComponent<CharacterItem>();
			PropItemData propData = ((i < m_currentBlueprint.m_propData.Length) ? m_currentBlueprint.m_propData[i] : new PropItemData());
			CharacterItem prop = SpawnProp(component, propData);
			EquipSpawnedProp(prop);
		}
	}

	public void LoadClothes(UnitBlueprint blueprint)
	{
		m_currentBlueprint.m_props = blueprint.m_props;
		m_currentBlueprint.m_propData = blueprint.m_propData;
		LoadClothes();
	}

	public void LoadWeapons()
	{
		if (m_currentBlueprint.RightWeapon != null)
		{
			CharacterItem prop = SpawnProp(m_currentBlueprint.RightWeapon.GetComponent<WeaponItem>(), m_currentBlueprint.RightWeaponData);
			EquipSpawnedProp(prop);
		}
	}

	public void LoadWeapons(UnitBlueprint blueprint)
	{
		m_currentBlueprint.RightWeapon = blueprint.RightWeapon;
		m_currentBlueprint.RightWeaponData = blueprint.RightWeaponData;
		LoadWeapons();
	}

	public void StartTestScene()
	{
		SaveClothes();
		SaveWeapons();
		ContentDatabase contentDatabase = ContentDatabase.Instance();
		SerializedUnitBlueprint data = m_currentBlueprint.SerializedUnit(default(DatabaseID));
		m_currentBlueprint = UnitBlueprint.DeserializedUnit(data, contentDatabase.AssetLoader, contentDatabase.LandfallContentDatabase);
		m_testScene.StartTestScene(m_currentBlueprint);
	}
}
