using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using InternalModding.Blocks;
using Localisation;
using Selectors;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ReferenceMaster : MonoBehaviour
{
	public enum WorkshopItemType
	{
		Machine = 0,
		Skins = 1,
		Levels = 2,
		Mods = 3,
		Unset = 4
	}

	public static class Clipboard
	{
		public static string valueText;

		public static float value;

		public static Color color;

		public static Vector3 position;

		public static Vector3 euler;

		public static Vector3 scale;
	}

	public delegate void WorkshopUnsubscribe(ulong publishedFileId);

	public static ReferenceMaster Instance;

	public static Dictionary<string, ulong> FolderToWorkshop;

	public static Dictionary<ulong, WorkshopManager.WorkshopItem> FolderToWorkshopItem;

	public static Action<string, string, int> onJoin;

	public static Action<string, string> onJoinPF;

	public static Action<string, bool, string, int> onHost;

	public static Action<GenericEntity, EntityLogic, EntityEvent> onExecuteEvent;

	public static Action onRemoteFilesUpdated;

	public static Action<bool> onTogglePortForwarding;

	public static Action<UPNPStatus> UPNPStatusChanged;

	public static Action<ConnectionTesterStatus> ConnectionTesterStatusChanged;

	public static Action onLevelLoad;

	public static Action onBeforeLevelWon;

	public static Action onLevelWon;

	public static Action OnGameStateReceived;

	public static Action onSceneTransition;

	public static Action onSceneLoaded;

	public static Action<int> onLevelLoadComplete;

	public static Action onResolutionChanged;

	public static Action onFOVChanged;

	public static Action onCameraSensitivityChanged;

	public static Action onUIScaleChanged;

	public static Action onUIIntensityChanged;

	public static Action onDOFChanged;

	public static Action onBloomChanged;

	public static Action onVignetteChanged;

	public static Action onShadowsChanged;

	public static Action onBlockShadowsChanged;

	public static Action onAAChanged;

	public static Action onSaturationChanged;

	public static Action onAnalyticsToggled;

	public static Action onCloudSavingToggled;

	public static Action onAdvancedBuildingToggled;

	public static Action onMiddleClickVFXToggled;

	public static Action onUIBlurToggled;

	public static Action onShowNodeGridToggled;

	public static Action onSmoothCamToggled;

	public static Action onBloodToggled;

	public static Action onTooltipsToggled;

	public static Action<bool> onTutorialsToggled;

	public static Action onConquerToggled;

	public static Action onHotkeyHUDToggled;

	public static Action onFramerateChanged;

	public static Action onTextureQualityChanged;

	public static Action onReflectionQualityChanged;

	public static Action onAnisoChanged;

	public static Action onSSAOChanged;

	public static Action onAudioReverbToggled;

	public static Action onControlsChanged;

	public static Action<Machine> onMachineModified;

	public static Action<Machine> onMachineChanged;

	public static Action<Machine> onCalculateMiddle;

	public static Action<Machine> onMachinePostLoad;

	public static Action<BlockBehaviour> onBlockPlaced;

	public static Action<BlockBehaviour> onDraggedBlockPlacement;

	public static Action<BlockBehaviour> onDraggedBlockPlaced;

	public static Action<int> onBlockRemoved;

	public static Action onGhostTransformed;

	public static Action<bool> onBlockHover;

	public static Action<bool> onLevelSimulation;

	public static Action<bool> onLocalMachineSimulation;

	public static Action<Machine> onPreSimulateMachine;

	public static Action<Machine, bool> onMachineSimulation;

	public static Action<Machine> onMachinePostSim;

	public static Action onDestroyPhysicsGoal;

	public static Action<Region> RegionChanged;

	public static UPNPStatus UPNPStatus = UPNPStatus.Initializing;

	public static string UPNPError = string.Empty;

	public static Action onMachineDLCStateChanged;

	public static Action<LevelSettings.LevelEnvironment> onLevelEditorEnvironmentChanged;

	public static IConsoleController ConsoleController = new DummyConsoleController();

	public static IChatController ChatController = new DummyChatController();

	public static Dictionary<Region, FallbackHost> RegionServers = new Dictionary<Region, FallbackHost>
	{
		{
			Region.EUCentral,
			new FallbackHost("euwest.ms.spiderlinggames.co.uk", "35.158.70.225")
		},
		{
			Region.AsiaPacific,
			new FallbackHost("asia.ms.spiderlinggames.co.uk", "54.169.9.7")
		},
		{
			Region.USEast,
			new FallbackHost("useast.ms.spiderlinggames.co.uk", "3.12.37.241")
		}
	};

	public static Dictionary<uint, List<BlockBehaviour>> BuildingBlocks = new Dictionary<uint, List<BlockBehaviour>>();

	public static Dictionary<uint, List<BlockBehaviour>> SimulationBlocks = new Dictionary<uint, List<BlockBehaviour>>();

	public static Dictionary<uint, List<BlockBehaviour>> IntactBlocks = new Dictionary<uint, List<BlockBehaviour>>();

	public static Dictionary<uint, HashSet<BlockBehaviour>> ReloadableBlocks = new Dictionary<uint, HashSet<BlockBehaviour>>();

	public static Dictionary<uint, HashSet<BlockBehaviour>> RepairableBlocks = new Dictionary<uint, HashSet<BlockBehaviour>>();

	public static Dictionary<uint, Transform> SimulationMachines = new Dictionary<uint, Transform>();

	public static int blocksInSim = 0;

	public static List<ISelectable> Selectables = new List<ISelectable>();

	public static BlockSkinLoader.SkinPack.Skin ActiveSkin;

	public static WorkshopItemType UIActive = WorkshopItemType.Unset;

	public static Transform physicsGoalInstance;

	public static List<MonoBehaviour> ToolsEnabled = new List<MonoBehaviour>();

	public static Dictionary<int, Material> clusterMaterials = new Dictionary<int, Material>();

	public static Dictionary<Transform, bool> machineSimulationStates = new Dictionary<Transform, bool>();

	public static bool activeMachineSimulating = false;

	public static List<BasicInfo> ExternalForceObjects = new List<BasicInfo>();

	public static List<BasicInfo> ExternalForceTemp = new List<BasicInfo>();

	public static BasicInfo[] ExternalForceObjectsArray = new BasicInfo[0];

	public static HashSet<GameObject> IgnoreBreakCollisions = new HashSet<GameObject>();

	public Sprite[] blockTypeSprites;

	public Texture2D[] blockTypeTextures;

	public Material HighlightMaterial;

	public Material SelectedMaterial;

	public Material BMReferenceMaterial;

	public Material NodeBuildingGridMaterial;

	public Material aerodynamicMaterial;

	public Material aerodynamicMaterialSurface;

	public Shader clusterShader;

	public Shader ghostShader;

	public Shader bannedButtonShader;

	public Shader waterBoundingBox;

	public Shader waterAlphaBlend;

	public Texture2D aiShaddowDissolve;

	public Color mpGhostColor;

	public Texture missingPrefabThumbnail;

	public GameObject placementEffect;

	public OutlinePlacementEffect outlinePlacementEffect;

	public Texture2D pickerCursor;

	public LayerMask levelEditorMask;

	public LayerMask editorPickMask;

	public LayerMask hudMask;

	public string[] godPowers;

	public Color[] teamColors;

	public Color[] zoneColors;

	public Color goodColor;

	public Color badColor;

	public Color waterPlayerBoundColor;

	public static string versionFormat = "{0}-{1}";

	public Color ObjectSelectionColor = new Color32(byte.MaxValue, 100, 0, byte.MaxValue);

	public Color BlockHighlightColor = Color.green;

	public Material surfaceNodeGhost;

	public Material surfaceEdgeGhost;

	public Material surfaceMouseGhost;

	public Material explosionOverlay;

	public Material explosionOpaque;

	public Material[] highlightMaterials = new Material[0];

	public Material[] highlightAccentMaterials = new Material[0];

	private Color[] highlightColors = new Color[0];

	private Color[] highlightAccentColors = new Color[0];

	private float highlight = 1f;

	public AudioSource flipAudio;

	private static AudioMixer mixer;

	private static Dictionary<string, AudioMixerGroup> mixerGroups = new Dictionary<string, AudioMixerGroup>();

	public GameObject BuildSurfaceGo;

	public GameObject BuildSurfaceParticle;

	public bool colorBetweenBlockOutlines;

	public GameObject BuildSurfaceVis;

	public GameObject goreGib;

	private static BesiegeConfig oldBesiegeConfig;

	private static BoxCollider boxCollider;

	private static SphereCollider sphereCollider;

	private static CapsuleCollider capsuleCollider;

	private static MeshCollider meshCollider;

	public static GameObject UndoSystemGO { get; set; }

	public static VisualSelector VisualSelectorMapper { get; set; }

	public static event OnConnect OnConnect;

	public static event ToolDisable ToolDisable;

	public static event OnDisconnect OnDisconnect;

	public static event RefreshWorkshopDel RefreshWorkshopDel;

	public static event WorkshopUnsubscribe Unsubscribe;

	public static event ResetLEditor ResetEditor;

	public static event VoidDel SaveConfigDel;

	public static void InvokeResolutionChange()
	{
		OptionsMaster.SetResolution();
	}

	public static void PlayFlip()
	{
		Instance.flipAudio.Play();
	}

	public static AudioMixerGroup GetMixer(string s)
	{
		return mixerGroups[s];
	}

	public static AudioMixerGroup GetWaterMixerFrom(AudioMixerGroup m)
	{
		if (!WaterController.Exist)
		{
			return m;
		}
		string text = "SFX";
		switch (m.name)
		{
		case "Master":
		case "UI":
		case "Music":
		case "Ambience":
			return m;
		case "Physics":
			text = "Physics (Underwater)";
			break;
		case "Blocks":
			text = "Blocks (Underwater)";
			break;
		default:
			text = "Underwater";
			break;
		}
		if (mixerGroups.Count == 0)
		{
			return m;
		}
		return GetMixer(text);
	}

	public void Awake()
	{
		Mesh mesh = new Mesh();
		mesh.name = "Empty Mesh";
		BlockSkinnedVisualController.empty = mesh;
		InitMixer();
		SceneManager.sceneLoaded += OnSceneLoad;
		Instance = this;
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		if (boxCollider == null)
		{
			boxCollider = new GameObject("BoxColliderRef", typeof(BoxCollider)).GetComponent<BoxCollider>();
			sphereCollider = new GameObject("SphereColliderRef", typeof(SphereCollider)).GetComponent<SphereCollider>();
			capsuleCollider = new GameObject("CapsuleColliderRef", typeof(CapsuleCollider)).GetComponent<CapsuleCollider>();
			meshCollider = new GameObject("MeshColliderRef", typeof(MeshCollider)).GetComponent<MeshCollider>();
			GameObject obj = boxCollider.gameObject;
			int num = 8;
			meshCollider.gameObject.layer = num;
			num = num;
			capsuleCollider.gameObject.layer = num;
			num = num;
			sphereCollider.gameObject.layer = num;
			obj.layer = num;
			BoxCollider obj2 = boxCollider;
			bool isTrigger = capsuleCollider.isTrigger;
			sphereCollider.isTrigger = isTrigger;
			obj2.isTrigger = isTrigger;
			Transform obj3 = boxCollider.transform;
			Transform transform = base.transform;
			meshCollider.transform.parent = transform;
			transform = transform;
			capsuleCollider.transform.parent = transform;
			transform = transform;
			sphereCollider.transform.parent = transform;
			obj3.parent = transform;
			Transform obj4 = boxCollider.transform;
			Vector3 vector = Vector3.one * 10000f;
			meshCollider.transform.position = vector;
			vector = vector;
			capsuleCollider.transform.position = vector;
			vector = vector;
			sphereCollider.transform.position = vector;
			obj4.position = vector;
		}
		SetDefaultColour(highlightMaterials, ref highlightColors);
		SetDefaultColour(highlightAccentMaterials, ref highlightAccentColors);
		UpdateHighting();
		onUIIntensityChanged = (Action)Delegate.Combine(onUIIntensityChanged, new Action(UpdateHighting));
	}

	private void SetDefaultColour(Material[] m, ref Color[] c)
	{
		c = new Color[m.Length];
		for (int i = 0; i < m.Length; i++)
		{
			if (m[i].HasProperty("_TintColor"))
			{
				c[i] = m[i].GetColor("_TintColor");
			}
			else if (m[i].HasProperty("_Color"))
			{
				c[i] = m[i].GetColor("_Color");
			}
		}
	}

	private void ResetColour(Material[] m, Color[] c)
	{
		for (int i = 0; i < m.Length; i++)
		{
			if (m[i].HasProperty("_TintColor"))
			{
				m[i].SetColor("_TintColor", c[i]);
			}
			else if (m[i].HasProperty("_Color"))
			{
				m[i].SetColor("_Color", c[i]);
			}
		}
	}

	public void UpdateHighting()
	{
		SetHighlightingMaterials(OptionsMaster.BesiegeConfig.UIIntensity / 100f);
	}

	public void SetHighlightingMaterials(float i)
	{
		if (highlight != i)
		{
			highlight = i;
			i = Mathf.Sqrt(i);
			SetColour(highlightMaterials, highlightColors, i, true);
			SetColour(highlightAccentMaterials, highlightAccentColors, i, false);
		}
	}

	private void SetColour(Material[] m, Color[] c, float intensity, bool useBrightness)
	{
		for (int i = 0; i < m.Length; i++)
		{
			if (m[i].HasProperty("_TintColor"))
			{
				SetColour("_TintColor", m[i], c[i], intensity, useBrightness);
			}
			else if (m[i].HasProperty("_Color"))
			{
				SetColour("_Color", m[i], c[i], intensity, useBrightness);
			}
		}
	}

	private void SetColour(string field, Material m, Color c, float intensity, bool useBrightness)
	{
		float a = c.a;
		float H;
		float S;
		float V;
		Color.RGBToHSV(c, out H, out S, out V);
		if (useBrightness)
		{
			H *= 0.9f + intensity * 0.1f;
			S *= Mathf.Sqrt(intensity);
			V *= intensity;
		}
		else
		{
			H *= 0.95f + intensity * 0.05f;
			S *= Mathf.Pow(intensity, 0.25f);
			V -= (1f - intensity) * 0.1f;
		}
		c = Color.HSVToRGB(H, S, V);
		c.a = a;
		m.SetColor(field, c);
	}

	public static Bounds GetBoxBounds(BoxCollider source)
	{
		boxCollider.transform.position = source.transform.position;
		boxCollider.transform.rotation = source.transform.rotation;
		boxCollider.transform.localScale = source.transform.localScale;
		boxCollider.center = source.center;
		boxCollider.size = source.size;
		Bounds bounds = boxCollider.bounds;
		boxCollider.transform.position = Vector3.one * 10000f;
		return bounds;
	}

	public static Bounds GetSphereBounds(SphereCollider source)
	{
		sphereCollider.transform.position = source.transform.position;
		sphereCollider.transform.rotation = source.transform.rotation;
		sphereCollider.transform.localScale = source.transform.localScale;
		sphereCollider.center = source.center;
		sphereCollider.radius = source.radius;
		Bounds bounds = sphereCollider.bounds;
		sphereCollider.transform.position = Vector3.one * 10000f;
		return bounds;
	}

	public static Bounds GetCapsuleBounds(CapsuleCollider source)
	{
		capsuleCollider.transform.position = source.transform.position;
		capsuleCollider.transform.rotation = source.transform.rotation;
		capsuleCollider.transform.localScale = source.transform.localScale;
		capsuleCollider.center = source.center;
		capsuleCollider.radius = source.radius;
		capsuleCollider.direction = source.direction;
		capsuleCollider.height = source.height;
		Bounds bounds = capsuleCollider.bounds;
		capsuleCollider.transform.position = Vector3.one * 10000f;
		return bounds;
	}

	public static Bounds GetMeshColliderBounds(MeshCollider source)
	{
		meshCollider.transform.position = source.transform.position;
		meshCollider.transform.rotation = source.transform.rotation;
		meshCollider.transform.localScale = source.transform.localScale;
		if (source.sharedMesh.isReadable)
		{
			meshCollider.sharedMesh = source.sharedMesh;
		}
		else
		{
			Debug.LogError(string.Concat("Non readable mesh collider ", source, " ", source.sharedMesh));
		}
		meshCollider.convex = source.convex;
		Bounds bounds = meshCollider.bounds;
		meshCollider.sharedMesh = null;
		meshCollider.transform.position = Vector3.one * 10000f;
		return bounds;
	}

	public void InitMixer()
	{
		mixer = SingleInstance<MusicController>.Instance.mixer;
		mixerGroups.Add("UI", mixer.FindMatchingGroups("UI")[0]);
		mixerGroups.Add("SFX", mixer.FindMatchingGroups("SFX")[0]);
		mixerGroups.Add("Overwater", mixer.FindMatchingGroups("Overwater")[0]);
		mixerGroups.Add("Blocks", mixer.FindMatchingGroups("Blocks")[0]);
		mixerGroups.Add("Physics", mixer.FindMatchingGroups("Physics")[0]);
		mixerGroups.Add("Explosions", mixer.FindMatchingGroups("Explosions")[0]);
		mixerGroups.Add("Underwater", mixer.FindMatchingGroups("Underwater")[0]);
		mixerGroups.Add("Blocks (Underwater)", mixer.FindMatchingGroups("Blocks (Underwater)")[0]);
		mixerGroups.Add("Physics (Underwater)", mixer.FindMatchingGroups("Physics (Underwater)")[0]);
		mixerGroups.Add("Music", mixer.FindMatchingGroups("Music")[0]);
		mixerGroups.Add("Ambience", mixer.FindMatchingGroups("Ambience")[0]);
	}

	public static string GetBugReportHeader()
	{
		List<string> list = new List<string>();
		list.Add(OptionsMaster.BesiegeConfig.PlayerName);
		list.Add(VersionNumber.GetVersionString());
		list.Add((!StatMaster.isMP) ? "SP" : "MP");
		if (StatMaster.isMP)
		{
			list.Add((!StatMaster.isHosting) ? "Client" : "Server");
			list.Add("Level: " + StatMaster.lastLoadedLevel);
		}
		return string.Join("|", list.ToArray()) + "\n";
	}

	public static void SaveConfig()
	{
		if (ReferenceMaster.SaveConfigDel != null)
		{
			ReferenceMaster.SaveConfigDel();
		}
	}

	public static void SetInterpolationForAllRigidbodies(RigidbodyInterpolation en)
	{
	}

	public static string CamelCaseToSpaces(string strInput)
	{
		string text = string.Empty;
		int num = 0;
		int num2 = strInput.Length - 1;
		for (num = 0; num <= num2; num++)
		{
			char c = strInput[num];
			char c2 = c;
			if (num > 0)
			{
				c2 = strInput[num - 1];
			}
			if (char.IsUpper(c) && char.IsLower(c2))
			{
				text += " ";
			}
			text += c;
		}
		return text;
	}

	public static T[] GetInterfacesInType<T, Y>()
	{
		if (!typeof(T).IsInterface)
		{
			throw new SystemException("Specified type is not an interface!");
		}
		Y[] source = UnityEngine.Object.FindObjectsOfType(typeof(Y)) as Y[];
		return (from a in source
			where a.GetType().GetInterfaces().Any((Type k) => k == typeof(T))
			select (T)(object)a).ToArray();
	}

	public static List<BlockBehaviour> GetAllSimulationBlocks()
	{
		List<BlockBehaviour> list = new List<BlockBehaviour>();
		foreach (uint key in SimulationBlocks.Keys)
		{
			list.AddRange(SimulationBlocks[key]);
		}
		return list;
	}

	public static List<BlockBehaviour> GetAllBuildingBlocks()
	{
		List<BlockBehaviour> list = new List<BlockBehaviour>();
		foreach (uint key in BuildingBlocks.Keys)
		{
			list.AddRange(BuildingBlocks[key]);
		}
		return list;
	}

	public static List<BlockBehaviour> GetSimulationBlocks(uint index)
	{
		List<BlockBehaviour> value;
		if (SimulationBlocks.TryGetValue(index, out value))
		{
			return value;
		}
		value = new List<BlockBehaviour>();
		SimulationBlocks.Add(index, value);
		return value;
	}

	public static List<BlockBehaviour> GetBuildingBlocks(uint index)
	{
		List<BlockBehaviour> value;
		if (BuildingBlocks.TryGetValue(index, out value))
		{
			return value;
		}
		value = new List<BlockBehaviour>();
		BuildingBlocks.Add(index, value);
		return value;
	}

	public static List<uint> AllBuildingBlockIndices()
	{
		return new List<uint>(BuildingBlocks.Keys);
	}

	public static void ClearBuildingBlocks()
	{
		foreach (uint key in BuildingBlocks.Keys)
		{
			ClearBuildingBlocks(key);
		}
	}

	public static void ClearSimulationBlocks()
	{
		foreach (uint key in SimulationBlocks.Keys)
		{
			ClearSimulationBlocks(key);
		}
	}

	public static void ClearBuildingBlocks(uint index)
	{
		List<BlockBehaviour> value;
		if (BuildingBlocks.TryGetValue(index, out value))
		{
			value.Clear();
		}
	}

	public static void ClearSimulationBlocks(uint index)
	{
		List<BlockBehaviour> value;
		if (SimulationBlocks.TryGetValue(index, out value))
		{
			value.Clear();
		}
		if (IntactBlocks.TryGetValue(index, out value))
		{
			value.Clear();
		}
	}

	public static void OnMachineEndSimulation()
	{
		Instance.StartCoroutine(UpdateExtrenalForceArray());
	}

	public static void OnMachineBeginSimulation()
	{
		Instance.StartCoroutine(UpdateExtrenalForceArray());
	}

	public static IEnumerator UpdateExtrenalForceArray()
	{
		yield return null;
		ExternalForceObjectsArray = ExternalForceObjects.ToArray();
		ExternalForceTemp.Clear();
	}

	public static List<BlockBehaviour> GetIntactBlocks(uint index)
	{
		List<BlockBehaviour> value;
		if (IntactBlocks.TryGetValue(index, out value))
		{
			return value;
		}
		value = new List<BlockBehaviour>();
		IntactBlocks.Add(index, value);
		return value;
	}

	public static BlockBehaviour GetRandomIntactBlock(uint index)
	{
		List<BlockBehaviour> value;
		if (IntactBlocks.TryGetValue(index, out value) && value.Count != 0)
		{
			int index2 = UnityEngine.Random.Range(0, value.Count - 1);
			if (value[index2].gameObject.activeSelf)
			{
				return value[index2];
			}
		}
		else if (StatMaster.isMP)
		{
			ServerMachine machine;
			if (NetworkAddPiece.Instance.GetActiveMachine(index, out machine))
			{
				machine.hasIntactBlocks = false;
			}
		}
		else
		{
			Machine.Active().hasIntactBlocks = false;
		}
		return GetRandomBlock(index);
	}

	private static bool UntargetableBlock(BlockBehaviour block)
	{
		return block.Prefab.Type == BlockType.Pin || block.Prefab.Type == BlockType.CameraBlock || block.Prefab.Type == BlockType.BuildNode || block.Prefab.Type == BlockType.BuildEdge;
	}

	public static BlockBehaviour GetRandomBlock(uint index)
	{
		List<BlockBehaviour> list = SimulationBlocks[index];
		if (list.Count != 0)
		{
			BlockBehaviour blockBehaviour = null;
			int num = 20;
			int num2 = 0;
			while (blockBehaviour == null)
			{
				int index2 = UnityEngine.Random.Range(0, list.Count - 1);
				blockBehaviour = list[index2];
				if (blockBehaviour.gameObject.activeSelf && !UntargetableBlock(blockBehaviour))
				{
					return blockBehaviour;
				}
				blockBehaviour = null;
				num2++;
				if (num2 >= num)
				{
					break;
				}
			}
		}
		return null;
	}

	public static IEnumerable<T> GetInterfaces<T>(GameObject go)
	{
		if (!typeof(T).IsInterface)
		{
			throw new SystemException("Specified type is not an interface!");
		}
		MonoBehaviour[] components = go.GetComponents<MonoBehaviour>();
		T[] array = new T[components.Length];
		int num = 0;
		for (int i = 0; i < components.Length; i++)
		{
			Type[] interfaces = components[i].GetType().GetInterfaces();
			for (int j = 0; j < interfaces.Length; j++)
			{
				if (interfaces[j] == typeof(T))
				{
					array[num] = (T)(object)components[i];
					num++;
				}
			}
		}
		T[] array2 = new T[num];
		if (num != 0)
		{
			Array.Copy(array, array2, num);
		}
		return array2;
	}

	public static int EnumToInt(int _enum)
	{
		return _enum;
	}

	public static void Unsub(ulong id)
	{
		if (ReferenceMaster.Unsubscribe != null)
		{
			ReferenceMaster.Unsubscribe(id);
		}
	}

	public static void RefreshWorkshop()
	{
		if (ReferenceMaster.RefreshWorkshopDel != null)
		{
			ReferenceMaster.RefreshWorkshopDel();
		}
	}

	public static void InvokeOnConnect()
	{
		if (ReferenceMaster.OnConnect != null)
		{
			ReferenceMaster.OnConnect();
		}
	}

	public static void InvokeOnDisconnect()
	{
		if (ReferenceMaster.OnDisconnect != null)
		{
			ReferenceMaster.OnDisconnect();
		}
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		ToolsEnabled.Clear();
		StatMaster.GodTools.ResetGodTools();
		StatMaster.clusterCoded = false;
		if (activeMachineSimulating && !StatMaster.isMP)
		{
			activeMachineSimulating = false;
		}
	}

	public static bool DisableAllTools()
	{
		int count = ToolsEnabled.Count;
		if (ReferenceMaster.ToolDisable != null)
		{
			ReferenceMaster.ToolDisable();
		}
		return count > 0;
	}

	public static void ResetLevelEditor()
	{
		if (ReferenceMaster.ResetEditor != null)
		{
			ReferenceMaster.ResetEditor();
		}
	}

	public static bool CompareQuaternion(Quaternion q1, Quaternion q2)
	{
		return q1.x == q2.x && q1.y == q2.y && q1.z == q2.z && q1.w == q2.w;
	}

	public static void SetDynamicText(DynamicText dynamicText, string text)
	{
		if (dynamicText == null)
		{
			Debug.LogWarning("DynamicText is null");
			return;
		}
		dynamicText.serializedText = text;
		dynamicText.SetText(text);
	}

	public static string TranslateBlockName(BlockType blockType)
	{
		BlockLoader instance = SingleInstanceFindOnly<BlockLoader>.Instance;
		if (instance.IsModBlock((int)blockType))
		{
			return instance.GetBlockName((int)blockType);
		}
		int num = -1;
		BlockPrefab prefab;
		if (PrefabMaster.GetPrefab(blockType, out prefab))
		{
			num = prefab.locID;
		}
		if (num == -1)
		{
			num = 2004;
			return string.Format(LocalisationManager.GetTranslation(num), (int)blockType);
		}
		return LocalisationManager.GetTranslation(num);
	}

	public static string TranslateKeyCode(KeyCode enumValue)
	{
		switch (enumValue)
		{
		case KeyCode.A:
			return LocalisationManager.GetTranslation(2552);
		case KeyCode.Alpha0:
			return LocalisationManager.GetTranslation(2529);
		case KeyCode.Alpha1:
			return LocalisationManager.GetTranslation(2530);
		case KeyCode.Alpha2:
			return LocalisationManager.GetTranslation(2531);
		case KeyCode.Alpha3:
			return LocalisationManager.GetTranslation(2532);
		case KeyCode.Alpha4:
			return LocalisationManager.GetTranslation(2533);
		case KeyCode.Alpha5:
			return LocalisationManager.GetTranslation(2534);
		case KeyCode.Alpha6:
			return LocalisationManager.GetTranslation(2535);
		case KeyCode.Alpha7:
			return LocalisationManager.GetTranslation(2536);
		case KeyCode.Alpha8:
			return LocalisationManager.GetTranslation(2537);
		case KeyCode.Alpha9:
			return LocalisationManager.GetTranslation(2538);
		case KeyCode.AltGr:
			return LocalisationManager.GetTranslation(2631);
		case KeyCode.Ampersand:
			return LocalisationManager.GetTranslation(2519);
		case KeyCode.Asterisk:
			return LocalisationManager.GetTranslation(2523);
		case KeyCode.At:
			return LocalisationManager.GetTranslation(2545);
		case KeyCode.B:
			return LocalisationManager.GetTranslation(2553);
		case KeyCode.BackQuote:
			return LocalisationManager.GetTranslation(2551);
		case KeyCode.Backslash:
			return LocalisationManager.GetTranslation(2547);
		case KeyCode.Backspace:
			return LocalisationManager.GetTranslation(2508);
		case KeyCode.Break:
			return LocalisationManager.GetTranslation(2635);
		case KeyCode.C:
			return LocalisationManager.GetTranslation(2554);
		case KeyCode.CapsLock:
			return LocalisationManager.GetTranslation(2621);
		case KeyCode.Caret:
			return LocalisationManager.GetTranslation(2549);
		case KeyCode.Clear:
			return LocalisationManager.GetTranslation(2510);
		case KeyCode.Colon:
			return LocalisationManager.GetTranslation(2539);
		case KeyCode.Comma:
			return LocalisationManager.GetTranslation(2525);
		case KeyCode.D:
			return LocalisationManager.GetTranslation(2555);
		case KeyCode.Delete:
			return LocalisationManager.GetTranslation(2578);
		case KeyCode.Dollar:
			return LocalisationManager.GetTranslation(2518);
		case KeyCode.DoubleQuote:
			return LocalisationManager.GetTranslation(2516);
		case KeyCode.DownArrow:
			return LocalisationManager.GetTranslation(2597);
		case KeyCode.E:
			return LocalisationManager.GetTranslation(2556);
		case KeyCode.End:
			return LocalisationManager.GetTranslation(2602);
		case KeyCode.Equals:
			return LocalisationManager.GetTranslation(2542);
		case KeyCode.Escape:
			return LocalisationManager.GetTranslation(2513);
		case KeyCode.Exclaim:
			return LocalisationManager.GetTranslation(2515);
		case KeyCode.F:
			return LocalisationManager.GetTranslation(2557);
		case KeyCode.F1:
			return LocalisationManager.GetTranslation(2605);
		case KeyCode.F10:
			return LocalisationManager.GetTranslation(2614);
		case KeyCode.F11:
			return LocalisationManager.GetTranslation(2615);
		case KeyCode.F12:
			return LocalisationManager.GetTranslation(2616);
		case KeyCode.F13:
			return LocalisationManager.GetTranslation(2617);
		case KeyCode.F14:
			return LocalisationManager.GetTranslation(2618);
		case KeyCode.F15:
			return LocalisationManager.GetTranslation(2619);
		case KeyCode.F2:
			return LocalisationManager.GetTranslation(2606);
		case KeyCode.F3:
			return LocalisationManager.GetTranslation(2607);
		case KeyCode.F4:
			return LocalisationManager.GetTranslation(2608);
		case KeyCode.F5:
			return LocalisationManager.GetTranslation(2609);
		case KeyCode.F6:
			return LocalisationManager.GetTranslation(2610);
		case KeyCode.F7:
			return LocalisationManager.GetTranslation(2611);
		case KeyCode.F8:
			return LocalisationManager.GetTranslation(2612);
		case KeyCode.F9:
			return LocalisationManager.GetTranslation(2613);
		case KeyCode.G:
			return LocalisationManager.GetTranslation(2558);
		case KeyCode.Greater:
			return LocalisationManager.GetTranslation(2543);
		case KeyCode.H:
			return LocalisationManager.GetTranslation(2559);
		case KeyCode.Hash:
			return LocalisationManager.GetTranslation(2517);
		case KeyCode.Help:
			return LocalisationManager.GetTranslation(2632);
		case KeyCode.Home:
			return LocalisationManager.GetTranslation(2601);
		case KeyCode.I:
			return LocalisationManager.GetTranslation(2560);
		case KeyCode.Insert:
			return LocalisationManager.GetTranslation(2600);
		case KeyCode.J:
			return LocalisationManager.GetTranslation(2561);
		case KeyCode.Joystick1Button0:
			return LocalisationManager.GetTranslation(2664);
		case KeyCode.Joystick1Button1:
			return LocalisationManager.GetTranslation(2665);
		case KeyCode.Joystick1Button10:
			return LocalisationManager.GetTranslation(2674);
		case KeyCode.Joystick1Button11:
			return LocalisationManager.GetTranslation(2675);
		case KeyCode.Joystick1Button12:
			return LocalisationManager.GetTranslation(2676);
		case KeyCode.Joystick1Button13:
			return LocalisationManager.GetTranslation(2677);
		case KeyCode.Joystick1Button14:
			return LocalisationManager.GetTranslation(2678);
		case KeyCode.Joystick1Button15:
			return LocalisationManager.GetTranslation(2679);
		case KeyCode.Joystick1Button16:
			return LocalisationManager.GetTranslation(2680);
		case KeyCode.Joystick1Button17:
			return LocalisationManager.GetTranslation(2681);
		case KeyCode.Joystick1Button18:
			return LocalisationManager.GetTranslation(2682);
		case KeyCode.Joystick1Button19:
			return LocalisationManager.GetTranslation(2683);
		case KeyCode.Joystick1Button2:
			return LocalisationManager.GetTranslation(2666);
		case KeyCode.Joystick1Button3:
			return LocalisationManager.GetTranslation(2667);
		case KeyCode.Joystick1Button4:
			return LocalisationManager.GetTranslation(2668);
		case KeyCode.Joystick1Button5:
			return LocalisationManager.GetTranslation(2669);
		case KeyCode.Joystick1Button6:
			return LocalisationManager.GetTranslation(2670);
		case KeyCode.Joystick1Button7:
			return LocalisationManager.GetTranslation(2671);
		case KeyCode.Joystick1Button8:
			return LocalisationManager.GetTranslation(2672);
		case KeyCode.Joystick1Button9:
			return LocalisationManager.GetTranslation(2673);
		case KeyCode.Joystick2Button0:
			return LocalisationManager.GetTranslation(2684);
		case KeyCode.Joystick2Button1:
			return LocalisationManager.GetTranslation(2685);
		case KeyCode.Joystick2Button10:
			return LocalisationManager.GetTranslation(2694);
		case KeyCode.Joystick2Button11:
			return LocalisationManager.GetTranslation(2695);
		case KeyCode.Joystick2Button12:
			return LocalisationManager.GetTranslation(2696);
		case KeyCode.Joystick2Button13:
			return LocalisationManager.GetTranslation(2697);
		case KeyCode.Joystick2Button14:
			return LocalisationManager.GetTranslation(2698);
		case KeyCode.Joystick2Button15:
			return LocalisationManager.GetTranslation(2699);
		case KeyCode.Joystick2Button16:
			return LocalisationManager.GetTranslation(2700);
		case KeyCode.Joystick2Button17:
			return LocalisationManager.GetTranslation(2701);
		case KeyCode.Joystick2Button18:
			return LocalisationManager.GetTranslation(2702);
		case KeyCode.Joystick2Button19:
			return LocalisationManager.GetTranslation(2703);
		case KeyCode.Joystick2Button2:
			return LocalisationManager.GetTranslation(2686);
		case KeyCode.Joystick2Button3:
			return LocalisationManager.GetTranslation(2687);
		case KeyCode.Joystick2Button4:
			return LocalisationManager.GetTranslation(2688);
		case KeyCode.Joystick2Button5:
			return LocalisationManager.GetTranslation(2689);
		case KeyCode.Joystick2Button6:
			return LocalisationManager.GetTranslation(2690);
		case KeyCode.Joystick2Button7:
			return LocalisationManager.GetTranslation(2691);
		case KeyCode.Joystick2Button8:
			return LocalisationManager.GetTranslation(2692);
		case KeyCode.Joystick2Button9:
			return LocalisationManager.GetTranslation(2693);
		case KeyCode.Joystick3Button0:
			return LocalisationManager.GetTranslation(2704);
		case KeyCode.Joystick3Button1:
			return LocalisationManager.GetTranslation(2705);
		case KeyCode.Joystick3Button10:
			return LocalisationManager.GetTranslation(2714);
		case KeyCode.Joystick3Button11:
			return LocalisationManager.GetTranslation(2715);
		case KeyCode.Joystick3Button12:
			return LocalisationManager.GetTranslation(2716);
		case KeyCode.Joystick3Button13:
			return LocalisationManager.GetTranslation(2717);
		case KeyCode.Joystick3Button14:
			return LocalisationManager.GetTranslation(2718);
		case KeyCode.Joystick3Button15:
			return LocalisationManager.GetTranslation(2719);
		case KeyCode.Joystick3Button16:
			return LocalisationManager.GetTranslation(2720);
		case KeyCode.Joystick3Button17:
			return LocalisationManager.GetTranslation(2721);
		case KeyCode.Joystick3Button18:
			return LocalisationManager.GetTranslation(2722);
		case KeyCode.Joystick3Button19:
			return LocalisationManager.GetTranslation(2723);
		case KeyCode.Joystick3Button2:
			return LocalisationManager.GetTranslation(2706);
		case KeyCode.Joystick3Button3:
			return LocalisationManager.GetTranslation(2707);
		case KeyCode.Joystick3Button4:
			return LocalisationManager.GetTranslation(2708);
		case KeyCode.Joystick3Button5:
			return LocalisationManager.GetTranslation(2709);
		case KeyCode.Joystick3Button6:
			return LocalisationManager.GetTranslation(2710);
		case KeyCode.Joystick3Button7:
			return LocalisationManager.GetTranslation(2711);
		case KeyCode.Joystick3Button8:
			return LocalisationManager.GetTranslation(2712);
		case KeyCode.Joystick3Button9:
			return LocalisationManager.GetTranslation(2713);
		case KeyCode.Joystick4Button0:
			return LocalisationManager.GetTranslation(2724);
		case KeyCode.Joystick4Button1:
			return LocalisationManager.GetTranslation(2725);
		case KeyCode.Joystick4Button10:
			return LocalisationManager.GetTranslation(2734);
		case KeyCode.Joystick4Button11:
			return LocalisationManager.GetTranslation(2735);
		case KeyCode.Joystick4Button12:
			return LocalisationManager.GetTranslation(2736);
		case KeyCode.Joystick4Button13:
			return LocalisationManager.GetTranslation(2737);
		case KeyCode.Joystick4Button14:
			return LocalisationManager.GetTranslation(2738);
		case KeyCode.Joystick4Button15:
			return LocalisationManager.GetTranslation(2739);
		case KeyCode.Joystick4Button16:
			return LocalisationManager.GetTranslation(2740);
		case KeyCode.Joystick4Button17:
			return LocalisationManager.GetTranslation(2741);
		case KeyCode.Joystick4Button18:
			return LocalisationManager.GetTranslation(2742);
		case KeyCode.Joystick4Button19:
			return LocalisationManager.GetTranslation(2743);
		case KeyCode.Joystick4Button2:
			return LocalisationManager.GetTranslation(2726);
		case KeyCode.Joystick4Button3:
			return LocalisationManager.GetTranslation(2727);
		case KeyCode.Joystick4Button4:
			return LocalisationManager.GetTranslation(2728);
		case KeyCode.Joystick4Button5:
			return LocalisationManager.GetTranslation(2729);
		case KeyCode.Joystick4Button6:
			return LocalisationManager.GetTranslation(2730);
		case KeyCode.Joystick4Button7:
			return LocalisationManager.GetTranslation(2731);
		case KeyCode.Joystick4Button8:
			return LocalisationManager.GetTranslation(2732);
		case KeyCode.Joystick4Button9:
			return LocalisationManager.GetTranslation(2733);
		case KeyCode.Joystick5Button0:
			return LocalisationManager.GetTranslation(2744);
		case KeyCode.Joystick5Button1:
			return LocalisationManager.GetTranslation(2745);
		case KeyCode.Joystick5Button10:
			return LocalisationManager.GetTranslation(2754);
		case KeyCode.Joystick5Button11:
			return LocalisationManager.GetTranslation(2755);
		case KeyCode.Joystick5Button12:
			return LocalisationManager.GetTranslation(2756);
		case KeyCode.Joystick5Button13:
			return LocalisationManager.GetTranslation(2757);
		case KeyCode.Joystick5Button14:
			return LocalisationManager.GetTranslation(2758);
		case KeyCode.Joystick5Button15:
			return LocalisationManager.GetTranslation(2759);
		case KeyCode.Joystick5Button16:
			return LocalisationManager.GetTranslation(2760);
		case KeyCode.Joystick5Button17:
			return LocalisationManager.GetTranslation(2761);
		case KeyCode.Joystick5Button18:
			return LocalisationManager.GetTranslation(2762);
		case KeyCode.Joystick5Button19:
			return LocalisationManager.GetTranslation(2763);
		case KeyCode.Joystick5Button2:
			return LocalisationManager.GetTranslation(2746);
		case KeyCode.Joystick5Button3:
			return LocalisationManager.GetTranslation(2747);
		case KeyCode.Joystick5Button4:
			return LocalisationManager.GetTranslation(2748);
		case KeyCode.Joystick5Button5:
			return LocalisationManager.GetTranslation(2749);
		case KeyCode.Joystick5Button6:
			return LocalisationManager.GetTranslation(2750);
		case KeyCode.Joystick5Button7:
			return LocalisationManager.GetTranslation(2751);
		case KeyCode.Joystick5Button8:
			return LocalisationManager.GetTranslation(2752);
		case KeyCode.Joystick5Button9:
			return LocalisationManager.GetTranslation(2753);
		case KeyCode.Joystick6Button0:
			return LocalisationManager.GetTranslation(2764);
		case KeyCode.Joystick6Button1:
			return LocalisationManager.GetTranslation(2765);
		case KeyCode.Joystick6Button10:
			return LocalisationManager.GetTranslation(2774);
		case KeyCode.Joystick6Button11:
			return LocalisationManager.GetTranslation(2775);
		case KeyCode.Joystick6Button12:
			return LocalisationManager.GetTranslation(2776);
		case KeyCode.Joystick6Button13:
			return LocalisationManager.GetTranslation(2777);
		case KeyCode.Joystick6Button14:
			return LocalisationManager.GetTranslation(2778);
		case KeyCode.Joystick6Button15:
			return LocalisationManager.GetTranslation(2779);
		case KeyCode.Joystick6Button16:
			return LocalisationManager.GetTranslation(2780);
		case KeyCode.Joystick6Button17:
			return LocalisationManager.GetTranslation(2781);
		case KeyCode.Joystick6Button18:
			return LocalisationManager.GetTranslation(2782);
		case KeyCode.Joystick6Button19:
			return LocalisationManager.GetTranslation(2783);
		case KeyCode.Joystick6Button2:
			return LocalisationManager.GetTranslation(2766);
		case KeyCode.Joystick6Button3:
			return LocalisationManager.GetTranslation(2767);
		case KeyCode.Joystick6Button4:
			return LocalisationManager.GetTranslation(2768);
		case KeyCode.Joystick6Button5:
			return LocalisationManager.GetTranslation(2769);
		case KeyCode.Joystick6Button6:
			return LocalisationManager.GetTranslation(2770);
		case KeyCode.Joystick6Button7:
			return LocalisationManager.GetTranslation(2771);
		case KeyCode.Joystick6Button8:
			return LocalisationManager.GetTranslation(2772);
		case KeyCode.Joystick6Button9:
			return LocalisationManager.GetTranslation(2773);
		case KeyCode.Joystick7Button0:
			return LocalisationManager.GetTranslation(2784);
		case KeyCode.Joystick7Button1:
			return LocalisationManager.GetTranslation(2785);
		case KeyCode.Joystick7Button10:
			return LocalisationManager.GetTranslation(2794);
		case KeyCode.Joystick7Button11:
			return LocalisationManager.GetTranslation(2795);
		case KeyCode.Joystick7Button12:
			return LocalisationManager.GetTranslation(2796);
		case KeyCode.Joystick7Button13:
			return LocalisationManager.GetTranslation(2797);
		case KeyCode.Joystick7Button14:
			return LocalisationManager.GetTranslation(2798);
		case KeyCode.Joystick7Button15:
			return LocalisationManager.GetTranslation(2799);
		case KeyCode.Joystick7Button16:
			return LocalisationManager.GetTranslation(2800);
		case KeyCode.Joystick7Button17:
			return LocalisationManager.GetTranslation(2801);
		case KeyCode.Joystick7Button18:
			return LocalisationManager.GetTranslation(2802);
		case KeyCode.Joystick7Button19:
			return LocalisationManager.GetTranslation(2803);
		case KeyCode.Joystick7Button2:
			return LocalisationManager.GetTranslation(2786);
		case KeyCode.Joystick7Button3:
			return LocalisationManager.GetTranslation(2787);
		case KeyCode.Joystick7Button4:
			return LocalisationManager.GetTranslation(2788);
		case KeyCode.Joystick7Button5:
			return LocalisationManager.GetTranslation(2789);
		case KeyCode.Joystick7Button6:
			return LocalisationManager.GetTranslation(2790);
		case KeyCode.Joystick7Button7:
			return LocalisationManager.GetTranslation(2791);
		case KeyCode.Joystick7Button8:
			return LocalisationManager.GetTranslation(2792);
		case KeyCode.Joystick7Button9:
			return LocalisationManager.GetTranslation(2793);
		case KeyCode.Joystick8Button0:
			return LocalisationManager.GetTranslation(2804);
		case KeyCode.Joystick8Button1:
			return LocalisationManager.GetTranslation(2805);
		case KeyCode.Joystick8Button10:
			return LocalisationManager.GetTranslation(2814);
		case KeyCode.Joystick8Button11:
			return LocalisationManager.GetTranslation(2815);
		case KeyCode.Joystick8Button12:
			return LocalisationManager.GetTranslation(2816);
		case KeyCode.Joystick8Button13:
			return LocalisationManager.GetTranslation(2817);
		case KeyCode.Joystick8Button14:
			return LocalisationManager.GetTranslation(2818);
		case KeyCode.Joystick8Button15:
			return LocalisationManager.GetTranslation(2819);
		case KeyCode.Joystick8Button16:
			return LocalisationManager.GetTranslation(2820);
		case KeyCode.Joystick8Button17:
			return LocalisationManager.GetTranslation(2821);
		case KeyCode.Joystick8Button18:
			return LocalisationManager.GetTranslation(2822);
		case KeyCode.Joystick8Button19:
			return LocalisationManager.GetTranslation(2823);
		case KeyCode.Joystick8Button2:
			return LocalisationManager.GetTranslation(2806);
		case KeyCode.Joystick8Button3:
			return LocalisationManager.GetTranslation(2807);
		case KeyCode.Joystick8Button4:
			return LocalisationManager.GetTranslation(2808);
		case KeyCode.Joystick8Button5:
			return LocalisationManager.GetTranslation(2809);
		case KeyCode.Joystick8Button6:
			return LocalisationManager.GetTranslation(2810);
		case KeyCode.Joystick8Button7:
			return LocalisationManager.GetTranslation(2811);
		case KeyCode.Joystick8Button8:
			return LocalisationManager.GetTranslation(2812);
		case KeyCode.Joystick8Button9:
			return LocalisationManager.GetTranslation(2813);
		case KeyCode.JoystickButton0:
			return LocalisationManager.GetTranslation(2644);
		case KeyCode.JoystickButton1:
			return LocalisationManager.GetTranslation(2645);
		case KeyCode.JoystickButton10:
			return LocalisationManager.GetTranslation(2654);
		case KeyCode.JoystickButton11:
			return LocalisationManager.GetTranslation(2655);
		case KeyCode.JoystickButton12:
			return LocalisationManager.GetTranslation(2656);
		case KeyCode.JoystickButton13:
			return LocalisationManager.GetTranslation(2657);
		case KeyCode.JoystickButton14:
			return LocalisationManager.GetTranslation(2658);
		case KeyCode.JoystickButton15:
			return LocalisationManager.GetTranslation(2659);
		case KeyCode.JoystickButton16:
			return LocalisationManager.GetTranslation(2660);
		case KeyCode.JoystickButton17:
			return LocalisationManager.GetTranslation(2661);
		case KeyCode.JoystickButton18:
			return LocalisationManager.GetTranslation(2662);
		case KeyCode.JoystickButton19:
			return LocalisationManager.GetTranslation(2663);
		case KeyCode.JoystickButton2:
			return LocalisationManager.GetTranslation(2646);
		case KeyCode.JoystickButton3:
			return LocalisationManager.GetTranslation(2647);
		case KeyCode.JoystickButton4:
			return LocalisationManager.GetTranslation(2648);
		case KeyCode.JoystickButton5:
			return LocalisationManager.GetTranslation(2649);
		case KeyCode.JoystickButton6:
			return LocalisationManager.GetTranslation(2650);
		case KeyCode.JoystickButton7:
			return LocalisationManager.GetTranslation(2651);
		case KeyCode.JoystickButton8:
			return LocalisationManager.GetTranslation(2652);
		case KeyCode.JoystickButton9:
			return LocalisationManager.GetTranslation(2653);
		case KeyCode.K:
			return LocalisationManager.GetTranslation(2562);
		case KeyCode.Keypad0:
			return LocalisationManager.GetTranslation(2579);
		case KeyCode.Keypad1:
			return LocalisationManager.GetTranslation(2580);
		case KeyCode.Keypad2:
			return LocalisationManager.GetTranslation(2581);
		case KeyCode.Keypad3:
			return LocalisationManager.GetTranslation(2582);
		case KeyCode.Keypad4:
			return LocalisationManager.GetTranslation(2583);
		case KeyCode.Keypad5:
			return LocalisationManager.GetTranslation(2584);
		case KeyCode.Keypad6:
			return LocalisationManager.GetTranslation(2585);
		case KeyCode.Keypad7:
			return LocalisationManager.GetTranslation(2586);
		case KeyCode.Keypad8:
			return LocalisationManager.GetTranslation(2587);
		case KeyCode.Keypad9:
			return LocalisationManager.GetTranslation(2588);
		case KeyCode.KeypadDivide:
			return LocalisationManager.GetTranslation(2590);
		case KeyCode.KeypadEnter:
			return LocalisationManager.GetTranslation(2594);
		case KeyCode.KeypadEquals:
			return LocalisationManager.GetTranslation(2595);
		case KeyCode.KeypadMinus:
			return LocalisationManager.GetTranslation(2592);
		case KeyCode.KeypadMultiply:
			return LocalisationManager.GetTranslation(2591);
		case KeyCode.KeypadPeriod:
			return LocalisationManager.GetTranslation(2589);
		case KeyCode.KeypadPlus:
			return LocalisationManager.GetTranslation(2593);
		case KeyCode.L:
			return LocalisationManager.GetTranslation(2563);
		case KeyCode.LeftAlt:
			return LocalisationManager.GetTranslation(2628);
		case KeyCode.LeftArrow:
			return LocalisationManager.GetTranslation(2599);
		case KeyCode.LeftBracket:
			return LocalisationManager.GetTranslation(2546);
		case KeyCode.LeftControl:
			return LocalisationManager.GetTranslation(2626);
		case KeyCode.LeftParen:
			return LocalisationManager.GetTranslation(2521);
		case KeyCode.LeftShift:
			return LocalisationManager.GetTranslation(2624);
		case KeyCode.LeftWindows:
			return LocalisationManager.GetTranslation(2629);
		case KeyCode.Less:
			return LocalisationManager.GetTranslation(2541);
		case KeyCode.M:
			return LocalisationManager.GetTranslation(2564);
		case KeyCode.Menu:
			return LocalisationManager.GetTranslation(2636);
		case KeyCode.Minus:
			return LocalisationManager.GetTranslation(2526);
		case KeyCode.Mouse0:
			return LocalisationManager.GetTranslation(2637);
		case KeyCode.Mouse1:
			return LocalisationManager.GetTranslation(2638);
		case KeyCode.Mouse2:
			return LocalisationManager.GetTranslation(2639);
		case KeyCode.Mouse3:
			return LocalisationManager.GetTranslation(2640);
		case KeyCode.Mouse4:
			return LocalisationManager.GetTranslation(2641);
		case KeyCode.Mouse5:
			return LocalisationManager.GetTranslation(2642);
		case KeyCode.Mouse6:
			return LocalisationManager.GetTranslation(2643);
		case KeyCode.N:
			return LocalisationManager.GetTranslation(2565);
		case KeyCode.None:
			return LocalisationManager.GetTranslation(2507);
		case KeyCode.Numlock:
			return LocalisationManager.GetTranslation(2620);
		case KeyCode.O:
			return LocalisationManager.GetTranslation(2566);
		case KeyCode.P:
			return LocalisationManager.GetTranslation(2567);
		case KeyCode.PageDown:
			return LocalisationManager.GetTranslation(2604);
		case KeyCode.PageUp:
			return LocalisationManager.GetTranslation(2603);
		case KeyCode.Pause:
			return LocalisationManager.GetTranslation(2512);
		case KeyCode.Period:
			return LocalisationManager.GetTranslation(2527);
		case KeyCode.Plus:
			return LocalisationManager.GetTranslation(2524);
		case KeyCode.Print:
			return LocalisationManager.GetTranslation(2633);
		case KeyCode.Q:
			return LocalisationManager.GetTranslation(2568);
		case KeyCode.Question:
			return LocalisationManager.GetTranslation(2544);
		case KeyCode.Quote:
			return LocalisationManager.GetTranslation(2520);
		case KeyCode.R:
			return LocalisationManager.GetTranslation(2569);
		case KeyCode.Return:
			return LocalisationManager.GetTranslation(2511);
		case KeyCode.RightAlt:
			return LocalisationManager.GetTranslation(2627);
		case KeyCode.RightArrow:
			return LocalisationManager.GetTranslation(2598);
		case KeyCode.RightBracket:
			return LocalisationManager.GetTranslation(2548);
		case KeyCode.RightControl:
			return LocalisationManager.GetTranslation(2625);
		case KeyCode.RightParen:
			return LocalisationManager.GetTranslation(2522);
		case KeyCode.RightShift:
			return LocalisationManager.GetTranslation(2623);
		case KeyCode.RightWindows:
			return LocalisationManager.GetTranslation(2630);
		case KeyCode.S:
			return LocalisationManager.GetTranslation(2570);
		case KeyCode.ScrollLock:
			return LocalisationManager.GetTranslation(2622);
		case KeyCode.Semicolon:
			return LocalisationManager.GetTranslation(2540);
		case KeyCode.Slash:
			return LocalisationManager.GetTranslation(2528);
		case KeyCode.Space:
			return LocalisationManager.GetTranslation(2514);
		case KeyCode.SysReq:
			return LocalisationManager.GetTranslation(2634);
		case KeyCode.T:
			return LocalisationManager.GetTranslation(2571);
		case KeyCode.Tab:
			return LocalisationManager.GetTranslation(2509);
		case KeyCode.U:
			return LocalisationManager.GetTranslation(2572);
		case KeyCode.Underscore:
			return LocalisationManager.GetTranslation(2550);
		case KeyCode.UpArrow:
			return LocalisationManager.GetTranslation(2596);
		case KeyCode.V:
			return LocalisationManager.GetTranslation(2573);
		case KeyCode.W:
			return LocalisationManager.GetTranslation(2574);
		case KeyCode.X:
			return LocalisationManager.GetTranslation(2575);
		case KeyCode.Y:
			return LocalisationManager.GetTranslation(2576);
		case KeyCode.Z:
			return LocalisationManager.GetTranslation(2577);
		default:
			return string.Format(LocalisationManager.GetTranslation(2506), enumValue);
		}
	}

	public static string TranslateTriggerType(TriggerType enumValue)
	{
		switch (enumValue)
		{
		case TriggerType.Activate:
			return LocalisationManager.GetTranslation(2826);
		case TriggerType.Behaviour:
			return LocalisationManager.GetTranslation(2834);
		case TriggerType.Deactivate:
			return LocalisationManager.GetTranslation(2827);
		case TriggerType.Death:
			return LocalisationManager.GetTranslation(2831);
		case TriggerType.Destroy:
			return LocalisationManager.GetTranslation(2830);
		case TriggerType.End:
			return LocalisationManager.GetTranslation(2825);
		case TriggerType.Enter:
			return LocalisationManager.GetTranslation(2828);
		case TriggerType.Exit:
			return LocalisationManager.GetTranslation(2829);
		case TriggerType.Explode:
			return LocalisationManager.GetTranslation(2833);
		case TriggerType.Ignite:
			return LocalisationManager.GetTranslation(2832);
		case TriggerType.KeyPressed:
			return LocalisationManager.GetTranslation(2837);
		case TriggerType.KeyReleased:
			return LocalisationManager.GetTranslation(2838);
		case TriggerType.MachineDamage:
			return LocalisationManager.GetTranslation(2835);
		case TriggerType.Start:
			return LocalisationManager.GetTranslation(2824);
		case TriggerType.Variable:
			return LocalisationManager.GetTranslation(2836);
		case TriggerType.LevelStart:
			return LocalisationManager.GetTranslation(3278);
		case TriggerType.Modded:
			return "Modded";
		default:
			return string.Format(LocalisationManager.GetTranslation(2506), enumValue);
		}
	}

	public static string TranslateUPNPStatus(UPNPStatus status)
	{
		string result = string.Empty;
		switch (status)
		{
		case UPNPStatus.Initializing:
			result = LocalisationManager.GetTranslation(1913);
			break;
		case UPNPStatus.FailedToInitialize:
			result = LocalisationManager.GetTranslation(1912);
			break;
		case UPNPStatus.Initialized:
			result = LocalisationManager.GetTranslation(1914);
			break;
		case UPNPStatus.ForwardingPort:
			result = LocalisationManager.GetTranslation(1915);
			break;
		case UPNPStatus.PortforwardingFailed:
			result = UPNPError;
			break;
		case UPNPStatus.PortforwardingSucceeded:
			result = LocalisationManager.GetTranslation(1917);
			break;
		}
		return result;
	}

	public static string TranslateRegion(Region region)
	{
		string empty = string.Empty;
		switch (region)
		{
		case Region.AsiaPacific:
			return LocalisationManager.GetTranslation(3174);
		case Region.EUCentral:
			return LocalisationManager.GetTranslation(3173);
		case Region.USEast:
			return LocalisationManager.GetTranslation(3172);
		case Region.TestRegion:
		case Region.TestRegion2:
			return region.ToString();
		default:
			return LocalisationManager.GetTranslation(3175);
		}
	}

	public static bool IsPlatformReady()
	{
		return SteamManager.Initialized;
	}

	public static void PrepareThumbnailQualitySettings(bool isMachineThumbnail = false)
	{
		oldBesiegeConfig = OptionsMaster.BesiegeConfig;
		OptionsMaster.BesiegeConfig = OptionsMaster.DefaultConfig;
		InvokeConfigCallbacks(false, isMachineThumbnail);
	}

	public static void PrepareThumbnailQualitySettings(BesiegeConfig c)
	{
		oldBesiegeConfig = OptionsMaster.BesiegeConfig;
		OptionsMaster.BesiegeConfig = c;
		InvokeConfigCallbacks(false);
	}

	public static void PrepareBuildSettings()
	{
		oldBesiegeConfig = OptionsMaster.BesiegeConfig;
		OptionsMaster.BesiegeConfig = OptionsMaster.DefaultConfig;
		InvokeConfigCallbacks(true);
	}

	public static void RestorePreBuildSettings()
	{
		OptionsMaster.BesiegeConfig = oldBesiegeConfig;
		InvokeConfigCallbacks(false);
	}

	public static void RestoreQualitySettings(bool isMachineThumbnail = false)
	{
		OptionsMaster.BesiegeConfig = oldBesiegeConfig;
		InvokeConfigCallbacks(false, isMachineThumbnail);
	}

	public static Island LevelToIsland(int i)
	{
		if (i <= 0)
		{
			Debug.Log("This is any type of sandbox");
			if ((bool)UnityEngine.Object.FindObjectOfType<WaterController>())
			{
				return Island.WaterSandbox;
			}
			return Island.None;
		}
		if (i <= 15 || i == 55)
		{
			return Island.Ipsilon;
		}
		if (i <= 34)
		{
			return Island.Tolbrynd;
		}
		if (i <= 44)
		{
			return Island.Valfross;
		}
		if (i <= 54)
		{
			return Island.Krolmar;
		}
		if (i <= 70)
		{
			return Island.Water;
		}
		return Island.None;
	}

	public static char StripSurrogateFromTextField(string input, int charIndex, char addedChar)
	{
		if (char.IsSurrogate(addedChar))
		{
			addedChar = '?';
		}
		return addedChar;
	}

	private static void InvokeConfigCallbacks(bool updateTextures, bool isMachineThumbnail = false)
	{
		if (onShadowsChanged != null)
		{
			onShadowsChanged();
		}
		if (updateTextures && onTextureQualityChanged != null)
		{
			onTextureQualityChanged();
		}
		if (onReflectionQualityChanged != null)
		{
			onReflectionQualityChanged();
		}
		if (onFOVChanged != null)
		{
			onFOVChanged();
		}
		if (onDOFChanged != null)
		{
			onDOFChanged();
		}
		if (onBloomChanged != null)
		{
			onBloomChanged();
		}
		if (onVignetteChanged != null)
		{
			onVignetteChanged();
		}
		if (onAAChanged != null)
		{
			onAAChanged();
		}
		if (onUIBlurToggled != null)
		{
			onUIBlurToggled();
		}
		if (!isMachineThumbnail && onAnisoChanged != null)
		{
			onAnisoChanged();
		}
		if (!isMachineThumbnail && onSSAOChanged != null)
		{
			onSSAOChanged();
		}
	}

	public static BlockBehaviour GetBlockWithinProximity(Machine machine, Transform t, float maxDistance)
	{
		float num = maxDistance * maxDistance;
		BlockLinkManager linkManager = machine.LinkManager;
		for (int i = 0; i < linkManager.Clusters.Count; i++)
		{
			if ((linkManager.Clusters[i].Center - t.position).sqrMagnitude < num)
			{
				int index = UnityEngine.Random.Range(0, linkManager.Clusters[i].Blocks.Count - 1);
				return linkManager.Clusters[i].Blocks[index].Block;
			}
		}
		return null;
	}
}
