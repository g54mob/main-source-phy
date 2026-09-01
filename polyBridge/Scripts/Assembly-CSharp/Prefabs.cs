using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Prefabs : MonoBehaviour
{
	public static Prefabs m_Instance;

	public static Dictionary<string, GameObject> m_PrefabsDict = new Dictionary<string, GameObject>();

	private static List<AsyncOperationHandle> m_AsyncHandlesInProgress = new List<AsyncOperationHandle>();

	private static Dictionary<string, AsyncOperationHandle> m_AsyncPrefabHandlesDict = new Dictionary<string, AsyncOperationHandle>();

	[Header("Effects")]
	public GameObject[] m_Effects;

	[Header("UI")]
	public GameObject m_Grid;

	public GameObject m_SelectionBox;

	public GameObject m_SelectionCircleDot;

	public GameObject m_MovementCircleDot;

	public GameObject m_EventTimeline;

	public GameObject m_EventStage;

	public GameObject m_EventIcon;

	public GameObject m_CheckpointPanel;

	public GameObject m_SplineControlPoint;

	public GameObject m_TimelineDivider;

	public GameObject m_TimelineDividerGrey;

	public GameObject m_ToolTip;

	public GameObject m_ArcTracer;

	public GameObject m_FillError;

	public GameObject m_PlacementDot;

	public GameObject m_SnapDot;

	public GameObject m_WorkshopItemSlotPrefab;

	public GameObject m_SandboxThumbnail;

	public GameObject m_SandboxItemImposter;

	public GameObject m_SandboxItemLabel;

	public GameObject m_VehicleCenterOfMass;

	public Material m_SandboxOutlineMaterial;

	[Header("Phases")]
	public GameObject m_HydraulicsPhase;

	public GameObject m_VehicleRestartPhase;

	[Header("Bridge")]
	public GameObject m_Joint;

	public GameObject m_Road;

	public GameObject m_ReinforcedRoad;

	public GameObject m_WoodTruss;

	public GameObject m_SteelTruss;

	public GameObject m_RopeTruss;

	public GameObject m_CableTruss;

	public GameObject m_HydraulicsTruss;

	public GameObject m_BungieRopeTruss;

	public GameObject m_SpringTruss;

	public GameObject m_Piston;

	public GameObject m_Spring;

	public GameObject m_JointSelector;

	public GameObject m_PreviewJoint;

	public GameObject m_BridgePillar;

	[Header("Clipboard")]
	public GameObject m_ClipboardJoint;

	public GameObject m_RoadClipboard;

	public GameObject m_ReinforcedRoadClipboard;

	public GameObject m_WoodTrussClipboard;

	public GameObject m_SteelTrussClipboard;

	public GameObject m_RopeTrussClipboard;

	public GameObject m_CableTrussClipboard;

	public GameObject m_HydraulicsTrussClipboard;

	public GameObject m_SpringTrussClipboard;

	public GameObject m_BridgePillarClipboard;

	[Header("Shadow")]
	public GameObject m_ShadowJoint;

	public GameObject m_ShadowRoad;

	public GameObject m_ShadowReinforcedRoad;

	public GameObject m_ShadowWood;

	public GameObject m_ShadowSteel;

	public GameObject m_ShadowRope;

	public GameObject m_ShadowCable;

	public GameObject m_ShadowHydraulics;

	public GameObject m_ShadowSpring;

	public GameObject m_ShadowBridgePillar;

	[Header("Links")]
	public GameObject m_ErrorLink;

	public GameObject m_RopeLink;

	public GameObject m_CableLink;

	public GameObject m_BungieRopeLink;

	public GameObject m_SpringCoilLink;

	[Header("Materials")]
	public GameObject m_RoadMaterial;

	public GameObject m_ReinforcedRoadMaterial;

	public GameObject m_WoodMaterial;

	public GameObject m_SteelMaterial;

	public GameObject m_HydraulicsMaterial;

	public GameObject m_CableMaterial;

	public GameObject m_RopeMaterial;

	public GameObject m_SpringMaterial;

	public GameObject m_BungieRopeMaterial;

	public GameObject m_PillarMaterial;

	[Header("Spline")]
	public GameObject m_Platform;

	public GameObject m_Ramp;

	[Header("Checkpoints")]
	public GameObject m_CheckpointStar;

	[Header("Flags")]
	public GameObject m_VictoryFlag;

	[Header("Placeable Objects")]
	public GameObject[] m_FlyingObjects;

	public GameObject[] m_Rocks;

	public GameObject m_Pillar;

	[Header("Build Zones")]
	public GameObject m_BuildZoneRect;

	public GameObject m_BuildZoneRectControlPoint;

	public GameObject m_BuildZoneTriangle;

	public GameObject m_BuildZoneTriangleControlPoint;

	[Header("Physics")]
	public GameObject m_PhysicsNode;

	public GameObject m_PhysicsEdge;

	public GameObject m_PhysicsRope;

	[Header("Water")]
	public GameObject m_WaterDash;

	public GameObject m_WaterRuler;

	public GameObject m_SplashBig;

	public GameObject m_SplashSmall;

	[Header("Custom Shapeps")]
	public GameObject m_CustomShape;

	public GameObject m_CustomShapeAnchor;

	public GameObject m_CustomShapePin;

	public GameObject m_CustomShapeVert;

	[Header("Debug")]
	public GameObject m_Graphy;

	private void Awake()
	{
		m_Instance = this;
		BuildDictionary();
	}

	public static bool AsyncLoadInProgress()
	{
		return m_AsyncHandlesInProgress.Count > 0;
	}

	public static bool AsyncPrefabExists(string prefabName)
	{
		return m_AsyncPrefabHandlesDict.ContainsKey(prefabName);
	}

	public bool IsLevelPreloaded(string layoutPath)
	{
		if (string.IsNullOrEmpty(layoutPath))
		{
			return false;
		}
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(layoutPath);
		if (sandboxLayoutData == null)
		{
			Debug.LogWarningFormat("Could not load: {0}", layoutPath);
			return false;
		}
		string addressableNameForId = ThemeStubs.m_Instance.GetAddressableNameForId(sandboxLayoutData.m_ThemeStubId);
		if (!m_AsyncPrefabHandlesDict.ContainsKey(addressableNameForId))
		{
			return false;
		}
		foreach (string item in GetAddressablePrefabNamesInLayout(sandboxLayoutData))
		{
			if (!m_AsyncPrefabHandlesDict.ContainsKey(item))
			{
				return false;
			}
		}
		return true;
	}

	public static ThemeStub GetAsyncTheme(string themeAddressableName)
	{
		if (m_AsyncPrefabHandlesDict.ContainsKey(themeAddressableName))
		{
			return (ThemeStub)m_AsyncPrefabHandlesDict[themeAddressableName].Result;
		}
		return null;
	}

	public static GameObject GetAsyncPrefab(string prefabName)
	{
		if (m_AsyncPrefabHandlesDict.ContainsKey(prefabName))
		{
			return (GameObject)m_AsyncPrefabHandlesDict[prefabName].Result;
		}
		return null;
	}

	public static void ReleaseAsset(string prefabName)
	{
		if (m_AsyncPrefabHandlesDict.ContainsKey(prefabName))
		{
			Addressables.Release(m_AsyncPrefabHandlesDict[prefabName]);
			m_AsyncPrefabHandlesDict.Remove(prefabName);
		}
	}

	public void PreloadSingleAsset(string assetName, string instanceID, Action<string, string, bool> callback)
	{
		StartCoroutine(LoadSingleAssetRoutine<GameObject>(assetName, instanceID, callback));
	}

	public void PreloadSingleShader(string assetName, string instanceID, Action<string, string, bool> callback)
	{
		StartCoroutine(LoadSingleAssetRoutine<Shader>(assetName, instanceID, callback));
	}

	public void PreloadSingleTexture(string assetName, string instanceID, Action<string, string, bool> callback)
	{
		StartCoroutine(LoadSingleAssetRoutine<Texture>(assetName, instanceID, callback));
	}

	public void PreloadSingleTheme(string themeAddressableName, string instanceID, Action<string, string, bool> callback)
	{
		StartCoroutine(LoadSingleAssetRoutine<ThemeStub>(themeAddressableName, instanceID, callback));
	}

	private IEnumerator LoadSingleAssetRoutine<T>(string assetName, string instanceID, Action<string, string, bool> callback)
	{
		AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(assetName);
		m_AsyncHandlesInProgress.Add(handle);
		yield return handle;
		m_AsyncHandlesInProgress.Remove(handle);
		if (handle.Status == AsyncOperationStatus.Succeeded)
		{
			if (!m_AsyncPrefabHandlesDict.ContainsKey(assetName))
			{
				m_AsyncPrefabHandlesDict.Add(assetName, handle);
			}
			callback?.Invoke(assetName, instanceID, arg3: true);
		}
		else
		{
			callback?.Invoke(assetName, instanceID, arg3: false);
		}
	}

	public void UnloadAssetsNotInLayout(string layoutPath)
	{
		if (string.IsNullOrEmpty(layoutPath))
		{
			return;
		}
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(layoutPath);
		if (sandboxLayoutData == null)
		{
			Debug.LogWarningFormat("Could not load: {0}", layoutPath);
			return;
		}
		List<string> list = new List<string>();
		HashSet<string> addressablePrefabNamesInLayout = GetAddressablePrefabNamesInLayout(sandboxLayoutData);
		string addressableNameForId = ThemeStubs.m_Instance.GetAddressableNameForId(sandboxLayoutData.m_ThemeStubId);
		addressablePrefabNamesInLayout.Add(addressableNameForId);
		foreach (KeyValuePair<string, AsyncOperationHandle> item in m_AsyncPrefabHandlesDict)
		{
			if (!addressablePrefabNamesInLayout.Contains(item.Key))
			{
				list.Add(item.Key);
			}
		}
		foreach (string item2 in list)
		{
			Addressables.Release(m_AsyncPrefabHandlesDict[item2]);
			if (m_AsyncPrefabHandlesDict.ContainsKey(item2))
			{
				m_AsyncPrefabHandlesDict.Remove(item2);
			}
		}
	}

	public void PreloadAssets(string layoutPath, SandboxLayoutData layoutData)
	{
		string addressableNameForId = ThemeStubs.m_Instance.GetAddressableNameForId(layoutData.m_ThemeStubId);
		if (string.IsNullOrEmpty(addressableNameForId))
		{
			Debug.LogError("Cannot find theme addressable for theme ID: '" + layoutData.m_ThemeStubId + "'");
		}
		else if (!m_AsyncPrefabHandlesDict.ContainsKey(addressableNameForId))
		{
			StartCoroutine(LoadAssetAsyncRoutine<ThemeStub>(addressableNameForId));
		}
		HashSet<string> addressablePrefabNamesInLayout = GetAddressablePrefabNamesInLayout(layoutData);
		StartCoroutine(PreloadAssetsRoutine(addressablePrefabNamesInLayout, layoutPath));
	}

	private IEnumerator PreloadAssetsRoutine(HashSet<string> assetList, string layoutPath)
	{
		foreach (string asset in assetList)
		{
			if (!m_AsyncPrefabHandlesDict.ContainsKey(asset))
			{
				StartCoroutine(LoadAssetAsyncRoutine<GameObject>(asset));
			}
		}
		while (m_AsyncHandlesInProgress.Count > 0)
		{
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator LoadAssetAsyncRoutine<T>(string prefabName)
	{
		AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(prefabName);
		m_AsyncHandlesInProgress.Add(handle);
		yield return handle;
		if (handle.Status == AsyncOperationStatus.Succeeded && !m_AsyncPrefabHandlesDict.ContainsKey(prefabName))
		{
			m_AsyncPrefabHandlesDict.Add(prefabName, handle);
		}
		m_AsyncHandlesInProgress.Remove(handle);
	}

	private HashSet<string> GetAddressablePrefabNamesInLayout(SandboxLayoutData layoutData)
	{
		HashSet<string> hashSet = new HashSet<string>();
		if (layoutData != null)
		{
			foreach (ZedAxisVehicleProxy zedAxisVehicle in layoutData.m_ZedAxisVehicles)
			{
				hashSet.Add(zedAxisVehicle.m_PrefabName);
			}
			foreach (VehicleProxy vehicle in layoutData.m_Vehicles)
			{
				hashSet.Add(vehicle.m_PrefabName);
			}
			foreach (DecorProxy decor in layoutData.m_Decors)
			{
				DecorStub stubFromId = DecorStubs.GetStubFromId(decor.m_ID);
				if (stubFromId == null)
				{
					Debug.LogWarningFormat("Could not find decor stub to preload layout, id = {0}", decor.m_ID);
				}
				else
				{
					hashSet.Add(stubFromId.m_PrefabAddress);
				}
			}
			foreach (CustomShapeProxy customShape in layoutData.m_CustomShapes)
			{
				if (customShape.m_MeshId != CustomShapes.AUTO_GENERATED_MESH_ID)
				{
					hashSet.Add(customShape.m_MeshId);
				}
			}
		}
		return hashSet;
	}

	private void BuildDictionary()
	{
		AddPickups();
		AddFlyingObjects();
		AddRocks();
		AddPillars();
		AddBuildZones();
	}

	private void AddPickups()
	{
		if (m_VictoryFlag != null)
		{
			m_PrefabsDict.Add(m_VictoryFlag.name, m_VictoryFlag);
		}
		if (m_CheckpointStar != null)
		{
			m_PrefabsDict.Add(m_CheckpointStar.name, m_CheckpointStar);
		}
	}

	private void AddFlyingObjects()
	{
		GameObject[] flyingObjects = m_FlyingObjects;
		foreach (GameObject gameObject in flyingObjects)
		{
			m_PrefabsDict.Add(gameObject.name, gameObject);
		}
	}

	private void AddRocks()
	{
		GameObject[] rocks = m_Rocks;
		foreach (GameObject gameObject in rocks)
		{
			m_PrefabsDict.Add(gameObject.name, gameObject);
		}
	}

	private void AddPillars()
	{
		if (m_Pillar != null)
		{
			m_PrefabsDict.Add(m_Pillar.name, m_Pillar);
		}
		if (m_BridgePillar != null)
		{
			m_PrefabsDict.Add(m_BridgePillar.name, m_BridgePillar);
		}
	}

	private void AddBuildZones()
	{
		m_PrefabsDict.Add(m_BuildZoneRect.name, m_BuildZoneRect);
		m_PrefabsDict.Add(m_BuildZoneTriangle.name, m_BuildZoneTriangle);
	}
}
