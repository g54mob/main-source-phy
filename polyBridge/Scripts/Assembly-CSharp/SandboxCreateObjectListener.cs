using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SandboxCreateObjectListener : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerExitHandler, IPointerEnterHandler
{
	public SandboxItemType m_Category;

	public GameObject m_Prefab;

	public string m_PrefabAddress;

	[NonSerialized]
	public string m_Id;

	[NonSerialized]
	public string m_ModId;

	private Action<bool> m_HoverCallback;

	public void SetHoverCallback(Action<bool> callback)
	{
		m_HoverCallback = callback;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_HoverCallback?.Invoke(obj: false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_HoverCallback?.Invoke(obj: true);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left || GameUI.IsEditingCustomShapeOrRamp())
		{
			return;
		}
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition());
		SandboxItem sandboxItem = null;
		switch (m_Category)
		{
		case SandboxItemType.ANCHOR:
			sandboxItem = CreateAnchor(worldPointFromScreenPos);
			break;
		case SandboxItemType.ZED_AXIS_VEHICLE:
			sandboxItem = CreateZedAxisVehicle(worldPointFromScreenPos);
			break;
		case SandboxItemType.VEHICLE:
			sandboxItem = CreateVehicle(worldPointFromScreenPos);
			break;
		case SandboxItemType.PLATFORM:
			sandboxItem = CreatePlatform(worldPointFromScreenPos);
			break;
		case SandboxItemType.RAMP:
			sandboxItem = CreateRamp(worldPointFromScreenPos);
			break;
		case SandboxItemType.TERRAIN:
			sandboxItem = CreateMiddleTerrainRandom(new Vector3(worldPointFromScreenPos.x, 0f, worldPointFromScreenPos.z));
			break;
		case SandboxItemType.FLYING_OBJECT:
			sandboxItem = CreateFlyingObject(worldPointFromScreenPos, m_Prefab);
			break;
		case SandboxItemType.ROCK:
			sandboxItem = CreateRock(new Vector3(worldPointFromScreenPos.x, 0f, worldPointFromScreenPos.z), m_Prefab);
			break;
		case SandboxItemType.CUSTOM_SHAPE:
			sandboxItem = CreateCustomShape(worldPointFromScreenPos);
			break;
		case SandboxItemType.BUILD_ZONE:
			sandboxItem = CreateBuildZone(worldPointFromScreenPos, BuildZones.DEFAULT_SIZE, m_Prefab);
			break;
		case SandboxItemType.PILLAR:
			sandboxItem = CreatePillar(worldPointFromScreenPos, m_Prefab);
			break;
		case SandboxItemType.DECOR:
			sandboxItem = CreateDecor(worldPointFromScreenPos);
			break;
		default:
			Debug.LogWarningFormat("Trying to create unsupported Sandbox Item type {0}", m_Category.ToString());
			break;
		}
		if ((bool)sandboxItem)
		{
			if (sandboxItem.m_Type == SandboxItemType.TERRAIN)
			{
				sandboxItem.GetComponent<TerrainIsland>().ShrinkForSandboxMode(shrink: true);
			}
			sandboxItem.SetOffsetFromPointer(GameInput.GetMousePosition());
			SandboxItems.SetNewUnPlacedItem(sandboxItem);
		}
	}

	private SandboxItem CreateAnchor(Vector3 pos)
	{
		BridgeJoint bridgeJoint = BridgeJoints.CreateAnchor(pos, Utils.GenerateUniqueId());
		if (!(bridgeJoint != null))
		{
			return null;
		}
		return bridgeJoint.GetComponent<SandboxItem>();
	}

	private SandboxItem CreatePlatform(Vector3 pos)
	{
		Platform platform = Platforms.CreatePlatform(pos, Quaternion.identity);
		if (!(platform != null))
		{
			return null;
		}
		return platform.GetComponent<SandboxItem>();
	}

	public static SandboxItem CreateRamp(Vector3 pos)
	{
		Ramp ramp = Ramps.CreateRamp(pos, Quaternion.identity);
		if (!ramp)
		{
			return null;
		}
		ramp.SetControlPoints(ramp.GetControlPointPositions());
		ramp.RefreshMesh();
		ramp.m_UpdateSplineNextFrame = true;
		return ramp.GetComponent<SandboxItem>();
	}

	private SandboxItem CreateMiddleTerrainRandom(Vector3 pos)
	{
		int numTerrainIslandPrefabs = Theme.m_Instance.GetNumTerrainIslandPrefabs(TerrainIslandType.Middle);
		if (numTerrainIslandPrefabs == 0)
		{
			return null;
		}
		int variantIndex = UnityEngine.Random.Range(0, numTerrainIslandPrefabs);
		TerrainIsland terrainIsland = TerrainIslands.CreateTerrain(Theme.m_Instance.GetTerrainIslandPrefab(TerrainIslandType.Middle, variantIndex), pos, Quaternion.identity);
		if (!terrainIsland)
		{
			return null;
		}
		terrainIsland.SetHeight(Theme.m_Instance.GetDefaultTerrainHeight());
		if (Theme.m_Instance != null)
		{
			terrainIsland.UpdateShaderProperties(buildMode: false, CuttingPlanes.m_Instance.m_Floor);
		}
		return terrainIsland.GetComponent<SandboxItem>();
	}

	private SandboxItem CreateFlyingObject(Vector3 pos, GameObject prefab)
	{
		FlyingObject flyingObject = FlyingObjects.CreateFlyingObject(prefab, pos, Quaternion.identity);
		if (!flyingObject)
		{
			return null;
		}
		return flyingObject.GetComponent<SandboxItem>();
	}

	private SandboxItem CreateRock(Vector3 pos, GameObject prefab)
	{
		Rock rock = Rocks.CreateRock(prefab, pos, Quaternion.identity);
		if (!rock)
		{
			return null;
		}
		return rock.GetComponent<SandboxItem>();
	}

	private SandboxItem CreatePillar(Vector3 pos, GameObject prefab)
	{
		Pillar pillar = Pillars.CreatePillar(prefab, pos, Quaternion.identity);
		if (!pillar)
		{
			return null;
		}
		return pillar.GetComponent<SandboxItem>();
	}

	private SandboxItem CreateBuildZone(Vector2 pos, Vector2 size, GameObject prefab)
	{
		BuildZone buildZone = BuildZones.Create(prefab, pos, size);
		if (buildZone == null)
		{
			return null;
		}
		return buildZone.GetComponent<SandboxItem>();
	}

	private SandboxItem CreateCustomShape(Vector3 pos)
	{
		List<CustomShape> list = CustomShapesLibrary.SpawnByFullPath(m_Id, pos);
		if (list == null || list.Count == 0)
		{
			return null;
		}
		if (list.Count == 1)
		{
			list[0].m_FullyQualifiedPath = m_Id;
			return list[0].GetComponent<SandboxItem>();
		}
		SandboxItem sandboxItem = CreateSandboxItemImposter(string.Empty, SandboxItemType.CUSTOM_SHAPE, string.Empty, string.Empty, null);
		sandboxItem.transform.position = pos;
		foreach (CustomShape item in list)
		{
			item.m_FullyQualifiedPath = m_Id;
			item.transform.SetParent(sandboxItem.transform);
		}
		return sandboxItem;
	}

	private SandboxItem CreateSandboxItemImposter(string addressable, SandboxItemType sandboxItemType, string id, string modId, Sprite icon)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_SandboxItemImposter);
		if (gameObject == null)
		{
			return null;
		}
		SandboxItem component = gameObject.GetComponent<SandboxItem>();
		if (component == null)
		{
			return null;
		}
		component.m_LoadingAddressable = addressable;
		component.m_LoadingAddressableId = id;
		component.m_LoadingAddressableModId = modId;
		component.m_LoadingAddressableType = sandboxItemType;
		if (component.m_LoadingAddressableIcon != null)
		{
			component.m_LoadingAddressableIcon.sprite = icon;
			component.m_LoadingAddressableIcon.gameObject.SetActive(value: false);
		}
		component.m_Type = SandboxItemType.IMPOSTER;
		SandboxItems.m_Imposters.Add(component);
		return component;
	}

	private SandboxItem CreateZedAxisVehicle(Vector3 pos)
	{
		SandboxItem result = null;
		if (Prefabs.AsyncPrefabExists(m_PrefabAddress))
		{
			m_Prefab = Prefabs.GetAsyncPrefab(m_PrefabAddress);
			result = SandboxItems.CreateZedAxisVehicle(pos, m_Prefab, m_ModId);
		}
		else
		{
			ZedAxisVehicleStub stubByAddressable = ZedAxisVehicleStubs.GetStubByAddressable(m_PrefabAddress);
			if (stubByAddressable != null)
			{
				result = CreateSandboxItemImposter(m_PrefabAddress, SandboxItemType.ZED_AXIS_VEHICLE, string.Empty, m_ModId, stubByAddressable.m_Icon);
				Prefabs.m_Instance.PreloadSingleAsset(m_PrefabAddress, string.Empty, null);
			}
		}
		return result;
	}

	private SandboxItem CreateVehicle(Vector3 pos)
	{
		SandboxItem result = null;
		if (Prefabs.AsyncPrefabExists(m_PrefabAddress))
		{
			m_Prefab = Prefabs.GetAsyncPrefab(m_PrefabAddress);
			result = SandboxItems.CreateVehicle(pos, m_Prefab, m_ModId);
		}
		else
		{
			VehicleStub stubByAddressable = VehicleStubs.GetStubByAddressable(m_PrefabAddress);
			if (stubByAddressable != null)
			{
				result = CreateSandboxItemImposter(m_PrefabAddress, SandboxItemType.VEHICLE, string.Empty, m_ModId, stubByAddressable.m_Icon);
				Prefabs.m_Instance.PreloadSingleAsset(m_PrefabAddress, string.Empty, null);
			}
		}
		return result;
	}

	private SandboxItem CreateDecor(Vector3 pos)
	{
		SandboxItem result = null;
		if (Prefabs.AsyncPrefabExists(m_PrefabAddress))
		{
			m_Prefab = Prefabs.GetAsyncPrefab(m_PrefabAddress);
			result = SandboxItems.CreateDecor(pos, m_Prefab, m_Id, m_ModId);
		}
		else if (DecorStubs.GetStubFromId(m_Id) != null)
		{
			result = CreateSandboxItemImposter(m_PrefabAddress, SandboxItemType.DECOR, m_Id, m_ModId, null);
			Prefabs.m_Instance.PreloadSingleAsset(m_PrefabAddress, string.Empty, null);
		}
		return result;
	}
}
