using System.IO;
using Poly.Base;
using Poly.Graphics;
using UnityEngine;

public class Sandbox
{
	public static bool m_AllowedToPanCamera;

	public static string m_CurrentLayoutName;

	public static string m_CurrentLayoutHash;

	public static SandboxLayoutData m_CurrentLayoutData;

	public static bool m_UnsavedChanges;

	public static bool m_SpawnRandomVehicle;

	public static bool m_ShowVehicleCenterOfMass;

	public static void Init()
	{
		SandboxUI.Init();
		EventEditor.Init();
	}

	public static void Clear()
	{
		SandboxSelectionSet.CancelSelection();
		EventEditor.Clear();
		GameUI.m_Instance.m_EventEditor.m_CollapsePanel.ForceUpdate();
		GameUI.m_Instance.m_EventEditor.SetDefaultEventEditorLocation();
		Bridge.Clear();
		BridgeJoints.DestroyAll();
		BuildZones.DestroyAll();
		TriggerCallbackManager.DestroyAll();
		Checkpoints.DestroyAll();
		CustomShapes.DestroyAll();
		Decors.DestroyAll();
		Vehicles.DestroyAll();
		VehicleStopTriggers.DestroyAll();
		VehicleRestartPhases.DestroyAll();
		ZedAxisVehicles.Disable();
		ZedAxisVehicles.DestroyAll();
		HydraulicsPhases.DestroyAll();
		HydraulicsController.DestroyAll();
		Pillars.DestroyAll();
		BridgePillars.DestroyAll();
		Platforms.DestroyAll();
		Ramps.DestroyAll();
		Rocks.DestroyAll();
		FlyingObjects.DestroyAll();
		TerrainIslands.DestroyAll();
		WaterBlocks.DestroyAll();
		WaterSplash.DestroyAll();
	}

	public static void UpdateManual()
	{
		SandboxInput.UpdateManual();
		SandboxItems.UpdateManual();
		CustomShapes.UpdateSpawnTransform();
		Vehicles.UpdateSpawnTransform();
		ZedAxisVehicles.SnapToWaterLine();
		ZedAxisVehicles.UpdateSpawnTransform();
		WaterBlocks.UpdateManual();
	}

	public static void LateUpdateManual()
	{
		SandboxItems.UpdateFloatingText();
		SandboxItems.UpdateFloatingTextFocus();
	}

	public static void Save(string name)
	{
		if (GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditCustomShapeTools.ExitCustomShapeEditToolsMode();
		}
		Profiles.m_ActiveProfile.m_LastLoadedSandbox = name;
		Profiles.SaveActiveProfile();
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Save(name);
		if (sandboxLayoutData == null)
		{
			GameUI.ShowMessage(ScreenMessageLocation.TOP_LEFT, string.Format(Localize.Get("WARN_SAVE_FAILED"), Path.GetFileNameWithoutExtension(name)), ScreenMessage.DEFAULT_DURATION_SECONDS);
			return;
		}
		m_CurrentLayoutData = sandboxLayoutData;
		m_CurrentLayoutName = name;
		m_UnsavedChanges = false;
		GameUI.ShowMessage(ScreenMessageLocation.TOP_LEFT, string.Format(Localize.Get("UI_SAVING"), Path.GetFileNameWithoutExtension(name)), ScreenMessage.DEFAULT_DURATION_SECONDS);
	}

	public static void StartNewSandbox(string themeId)
	{
		Clear();
		Budget.Init();
		SandboxSettings.Init();
		EventTimelines.CreateTimeline();
		Load(themeId, null, loadBridge: false);
		PointsOfView.OnLayoutLoaded(string.Empty);
		SandboxUndo.Clear();
		SandboxUndo.SnapShot();
		TerrainIslands.UpdateOutlines();
		WorkshopSubmit.Reset();
		GameUI.m_Instance.m_TopBar.m_MessageTopLeft.UnpinMessage();
		GameUI.m_Instance.m_SandboxModifiers.RefreshProperties();
		GameUI.m_Instance.m_SandboxResources.RefreshProperties();
		m_CurrentLayoutName = string.Empty;
	}

	public static string GetCurrentLayoutName()
	{
		return m_CurrentLayoutName;
	}

	public static string GetCurrentLayoutFilename()
	{
		return SandboxLayout.AddFileExtension(m_CurrentLayoutName);
	}

	public static bool LoadLayout(string layoutPath)
	{
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(layoutPath);
		if (sandboxLayoutData == null)
		{
			Debug.LogWarningFormat("Could not load Sandbox Layout: {0}", layoutPath);
			return false;
		}
		Profiles.m_ActiveProfile.m_LastLoadedSandbox = GetRelativePathToSaveDir(layoutPath);
		Profiles.SaveActiveProfile();
		string themeStubId = sandboxLayoutData.m_ThemeStubId;
		if (string.IsNullOrEmpty(themeStubId))
		{
			return false;
		}
		Clear();
		Load(themeStubId, sandboxLayoutData, loadBridge: true);
		PointsOfView.OnLayoutLoaded(string.Empty);
		SandboxUndo.Clear();
		SandboxUndo.SnapShot();
		return true;
	}

	public static void Load(string id, SandboxLayoutData layoutData, bool loadBridge)
	{
		AudioEmitters.Clear();
		Theme.m_Instance.m_ThemeStub = ThemeStubs.m_Instance.GetStubFromId(id);
		if (Theme.m_Instance.m_ThemeStub == null)
		{
			Debug.LogError("Cannot find ThemeStub with id " + id);
			return;
		}
		if (layoutData == null)
		{
			RegisterBookendTerrains();
			if (TerrainIslands.m_Terrains.Count == 0)
			{
				CreateRandomBookends();
				CreateDefaultAnchors();
				GameGrid.CenterOnTerrainEdge(TerrainIslands.GetLeftTerrain());
			}
			SandboxSettings.m_NoWater = Theme.m_Instance.m_ThemeStub.m_NoWaterDefault;
			SandboxSettings.m_ThreeWaySplitJointsEnabled = false;
			m_CurrentLayoutData = SandboxLayout.SerializeToProxies();
		}
		else
		{
			SandboxSettings.m_ThreeWaySplitJointsEnabled = false;
			SandboxLayout.DeserializeFromProxies(layoutData, loadBridge);
			if (GameUI.m_Instance.m_SandboxModifiers.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_SandboxModifiers.RefreshProperties();
			}
			if (GameUI.m_Instance.m_SandboxResources.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_SandboxResources.RefreshProperties();
			}
			if (GameUI.m_Instance.m_SandboxTitleAndDescription.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_SandboxTitleAndDescription.RefreshProperties();
			}
			m_CurrentLayoutData = layoutData;
		}
		if (m_CurrentLayoutData != null)
		{
			m_CurrentLayoutHash = Checksum.Generate(m_CurrentLayoutData.SerializeWithoutBridgeBinary());
		}
		else
		{
			m_CurrentLayoutHash = string.Empty;
		}
		GameUI.OnLayoutLoaded();
		GameStateManager.OnLayoutLoaded();
		GroupSelect.OnLayoutLoaded();
		Theme.m_Instance.OnLayoutLoaded();
		BridgeJointPlacement.OnLayoutLoaded();
		BridgeJointSelectors.OnLayoutLoaded();
		BridgeTrace.OnLayoutLoaded();
		BridgeShadow.OnLayoutLoaded();
		BridgeSaveSlots.ClearLastSlotSavedForFutureQuicksave();
		Pistons.DisablePinions();
		GameUI.m_Instance.m_BottomBar.OnLayoutLoaded();
		GameUI.m_Instance.m_Recenter.OnLayoutLoaded();
		PolyTwitch.OnLayoutLoaded();
		GameUI.m_Instance.m_TopBar.OnLayoutLoaded();
		HydraulicsController.OnLayoutLoaded();
		WorldBounds.Calculate(GameSettings.WorldWidth(), GameSettings.WorldMinY(), GameSettings.WorldMaxY());
		Cameras.AbortRecording();
		SandboxItems.UpdateFloatingText();
		SandboxItems.ResolveOverlappingFloatingText();
		SandboxSelectionSet.OnLayoutLoaded();
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			GameStateSandbox.ObjectsEnterSandboxMode();
		}
		WaterBlocks.UpdateManual();
		WaterBlocks.RebuildMesh();
		WaterBlocks.EnableMeshRenderers(GameStateManager.GetState() != GameState.SANDBOX && !SandboxSettings.m_NoWater);
		TerrainIslands.EnableCollisionMeshRenderer(GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.BUILD);
		TerrainIslands.UpdateShaderProperties(GameStateManager.GetState() == GameState.BUILD, CuttingPlanes.m_Instance.m_Floor);
		BuildZones.EnableSpriteRendering(GameStateManager.GetState() == GameState.SANDBOX);
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			GameStateBuild.Exit(GameState.INVALID);
			GameStateBuild.Enter(GameState.INVALID);
		}
		m_UnsavedChanges = false;
	}

	public static void Restore()
	{
		BridgePhysics.Reset();
		BridgeEdges.SetDefaultColors();
		BridgeEdges.UpdateTransforms();
		CustomShapes.Restore();
		Vehicles.Restore();
		ZedAxisVehicles.Restore();
		TriggerCallbackManager.Restore();
		VehicleStopTriggers.Restore();
		Checkpoints.Restore();
		EventTimelines.Restore();
		Bridge.m_Simulating = false;
		SingletonBehaviour<GpuInstancer>.instance?.Reset();
	}

	public static void CreateDefaultVehicle()
	{
		string randomVehiclePrefabAddress = VehicleStubs.GetRandomVehiclePrefabAddress();
		if (string.IsNullOrEmpty(randomVehiclePrefabAddress))
		{
			Debug.LogWarning("Failed to find a random vehicle prefab address");
		}
		else if (Prefabs.AsyncPrefabExists(randomVehiclePrefabAddress))
		{
			CreateVehicleAtDefaultSpawn(Prefabs.GetAsyncPrefab(randomVehiclePrefabAddress));
		}
		else
		{
			Prefabs.m_Instance.PreloadSingleAsset(randomVehiclePrefabAddress, string.Empty, PreloadVehiclePrefabCallback);
		}
	}

	public static Checkpoint CreateCheckpointForVehicle(Vehicle vehicle)
	{
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		if (!rightTerrain)
		{
			Debug.LogWarningFormat("Could not find right terrain for placing vehicle checkpoint");
			return null;
		}
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		if (!leftTerrain)
		{
			Debug.LogWarningFormat("Could not find left terrain for placing vehicle goal flag");
			return null;
		}
		TerrainIslandSpawnPoint spawnPoint = rightTerrain.m_SpawnPoint;
		if (!spawnPoint)
		{
			Debug.LogWarningFormat("Right terrain requires a TerrainIslandSpawnPoint for default vehicle goal location");
		}
		Vector3 worldPos = (rightTerrain.transform.position + leftTerrain.transform.position) / 2f;
		worldPos.y = spawnPoint.transform.position.y;
		Checkpoint checkpoint = Checkpoints.CreateCheckpoint(Prefabs.m_Instance.m_CheckpointStar, vehicle.GetFlagColor(), GameGrid.SnapPosToGrid(worldPos), Prefabs.m_Instance.m_CheckpointStar.transform.rotation, Utils.GenerateUniqueId());
		if ((bool)checkpoint)
		{
			checkpoint.m_VehicleGuid = vehicle.m_Guid;
			checkpoint.UpdateFloatingText();
			checkpoint.InstantiatePickupFX();
			checkpoint.transform.position += new Vector3(0f, checkpoint.GetMeshRenderer().bounds.size.y, 0f);
			checkpoint.EnterGameState(GameStateManager.GetState());
			checkpoint.RefreshMesh();
			checkpoint.ResolveOverlap();
			vehicle.m_Checkpoints.Add(checkpoint);
			SandboxItems.ResolveOverlappingFloatingText();
			SandboxUndo.SnapShot();
		}
		return checkpoint;
	}

	public static string GetRelativePathToSaveDir(string fullpath)
	{
		string text = string.Empty;
		string[] array = fullpath.Split(Path.DirectorySeparatorChar);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == SandboxLayout.SAVE_DIRECTORY)
			{
				for (int j = i + 1; j < array.Length; j++)
				{
					text = Path.Combine(text, array[j]);
				}
				break;
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			return text;
		}
		return fullpath;
	}

	private static void RegisterBookendTerrains()
	{
		TerrainIsland[] array = Object.FindObjectsOfType<TerrainIsland>();
		foreach (TerrainIsland terrainIsland in array)
		{
			if (terrainIsland.m_TerrainIslandType == TerrainIslandType.Bookend)
			{
				TerrainIslands.m_Terrains.Add(terrainIsland);
				terrainIsland.transform.SetParent(SandboxItems.GetSandboxContainerTransform());
			}
		}
	}

	private static void CreateRandomBookends()
	{
		if (Theme.m_Instance.GetNumTerrainIslandPrefabs(TerrainIslandType.Bookend) == 0)
		{
			RegisterBookendTerrains();
			return;
		}
		int num = Theme.m_Instance.GetRandomBookendVariant();
		TerrainIsland terrainIsland = TerrainIslands.CreateTerrain(Theme.m_Instance.GetTerrainIslandPrefab(TerrainIslandType.Bookend, num), Vector3.zero, Quaternion.identity);
		if (terrainIsland != null)
		{
			terrainIsland.ShrinkForSandboxMode(shrink: true);
			terrainIsland.SetHeight(Theme.m_Instance.GetDefaultTerrainHeight());
		}
		int randomBookendVariantWithExclusion = Theme.m_Instance.GetRandomBookendVariantWithExclusion(num);
		if (randomBookendVariantWithExclusion >= 0)
		{
			num = randomBookendVariantWithExclusion;
		}
		TerrainIsland terrainIsland2 = TerrainIslands.CreateTerrain(Theme.m_Instance.GetTerrainIslandPrefab(TerrainIslandType.Bookend, num), new Vector3(WaterBlocks.DEFAULT_WIDTH, 0f, 0f), Quaternion.identity);
		if ((bool)terrainIsland2)
		{
			terrainIsland2.ShrinkForSandboxMode(shrink: true);
			terrainIsland2.SetHeight(Theme.m_Instance.GetDefaultTerrainHeight());
			terrainIsland2.Flip();
		}
	}

	private static void CreateDefaultAnchors()
	{
		float y = Theme.m_Instance.GetDefaultTerrainHeight() - 0.1f;
		BridgeJoint bridgeJoint = BridgeJoints.CreateAnchor(new Vector3(0f, y, 0f), Utils.GenerateUniqueId());
		bridgeJoint.m_SandboxItem = SandboxItems.AddSandboxItemComponent(bridgeJoint.gameObject, SandboxItemType.ANCHOR);
		BridgeJoint bridgeJoint2 = BridgeJoints.CreateAnchor(new Vector3(WaterBlocks.DEFAULT_WIDTH, y, 0f), Utils.GenerateUniqueId());
		bridgeJoint2.m_SandboxItem = SandboxItems.AddSandboxItemComponent(bridgeJoint2.gameObject, SandboxItemType.ANCHOR);
	}

	private static void CreateVehicleAtDefaultSpawn(GameObject prefab)
	{
		SandboxItem sandboxItem = SandboxItems.CreateVehicle(Vector3.zero, prefab, string.Empty);
		if (sandboxItem != null)
		{
			EventEditor.UpdatePendingStage();
			EventEditor.DropIcon();
			SandboxItems.PlaceNewVehicle(sandboxItem.GetComponent<Vehicle>(), useDefaultStartPos: true);
			EventEditor.m_PendingStage = null;
			SandboxUndo.Clear();
			SandboxUndo.SnapShot();
		}
	}

	private static void PreloadVehiclePrefabCallback(string addressableName, string instanceID, bool success)
	{
		if (Prefabs.AsyncPrefabExists(addressableName))
		{
			CreateVehicleAtDefaultSpawn(Prefabs.GetAsyncPrefab(addressableName));
		}
	}
}
