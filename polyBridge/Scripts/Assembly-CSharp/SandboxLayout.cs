using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SandboxLayout
{
	public static int CURRENT_VERSION = 75;

	public static int CURRENT_VERSION_TWITCH = 74;

	public static string SAVE_DIRECTORY = "Sandbox";

	public static string SAVE_EXTENSION = ".layout";

	private static Dictionary<string, SandboxLayoutData> m_SandboxLayoutCache = new Dictionary<string, SandboxLayoutData>();

	public static SandboxLayoutData Save(string name)
	{
		SandboxLayoutData sandboxLayoutData = SerializeToProxies();
		try
		{
			byte[] bytes = sandboxLayoutData.SerializeBinary();
			Write(AddFileExtension(name), bytes);
			return sandboxLayoutData;
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Caught exception in SandboxLayoutData::Save {0}", ex.Message);
			return null;
		}
	}

	public static SandboxLayoutData Load(string fullpath)
	{
		string fileName = Path.GetFileName(fullpath);
		if (m_SandboxLayoutCache.ContainsKey(fileName))
		{
			return m_SandboxLayoutCache[fileName];
		}
		SandboxLayoutData sandboxLayoutData = TryLoad(fullpath);
		if (sandboxLayoutData != null && fileName != Workshop.LEVEL_LAYOUT_FILENAME)
		{
			m_SandboxLayoutCache.Add(fileName, sandboxLayoutData);
		}
		if (sandboxLayoutData == null)
		{
			fullpath = Path.ChangeExtension(fullpath, ".restore");
			sandboxLayoutData = TryLoad(fullpath);
		}
		return sandboxLayoutData;
	}

	private static SandboxLayoutData TryLoad(string fullpath)
	{
		try
		{
			if (File.Exists(fullpath))
			{
				byte[] array = Utils.ReadAllBytes(fullpath);
				if (array == null || array.Length == 0 || array[0] == 0)
				{
					return null;
				}
				int offset = 0;
				SandboxLayoutData sandboxLayoutData = new SandboxLayoutData(array, ref offset);
				if (sandboxLayoutData != null)
				{
					MaybeFixUpThemeStubID(sandboxLayoutData, fullpath);
				}
				return sandboxLayoutData;
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Caught exception in SandboxLayoutData::Load {0}", ex.Message);
		}
		return null;
	}

	public static SandboxLayoutData Load(string path, string name)
	{
		return Load(Path.Combine(path, AddFileExtension(name)));
	}

	public static SandboxLayoutData SerializeToProxies()
	{
		return new SandboxLayoutData
		{
			m_Version = (PolyTwitch.m_IsSerializing ? CURRENT_VERSION_TWITCH : CURRENT_VERSION),
			m_BridgeVersion = BridgeSave.CURRENT_VERSION,
			m_ThemeStubId = Theme.m_Instance.m_ThemeStub.m_ID,
			m_Anchors = BridgeJoints.SerializeAnchorsForSandboxLayout(),
			m_HydraulicsPhases = HydraulicsPhases.Serialize(),
			m_Bridge = BridgeSave.Serialize(),
			m_Checkpoints = Checkpoints.Serialize(),
			m_ZedAxisVehicles = ZedAxisVehicles.Serialize(),
			m_Vehicles = Vehicles.Serialize(),
			m_VehicleRestartPhases = VehicleRestartPhases.Serialize(),
			m_VehicleStopTriggers = VehicleStopTriggers.Serialize(),
			m_EventTimelines = EventTimelines.Serialize(),
			m_TerrainStretches = TerrainIslands.Serialize(),
			m_Pillars = Pillars.Serialize(),
			m_Decors = Decors.Serialize(),
			m_Platforms = Platforms.Serialize(),
			m_Ramps = Ramps.Serialize(),
			m_FlyingObjects = FlyingObjects.Serialize(),
			m_Rocks = Rocks.Serialize(),
			m_WaterBlocks = WaterBlocks.Serialize(),
			m_BuildZones = BuildZones.Serialize(),
			m_CustomShapes = CustomShapes.Serialize(),
			m_Budget = Budget.Serialize(),
			m_Settings = SandboxSettings.Serialize(),
			m_Workshop = WorkshopSubmit.Serialize()
		};
	}

	public static void DeserializeFromProxies(SandboxLayoutData saveData, bool loadBridge)
	{
		BridgeJoints.Deserialize(saveData.m_Anchors);
		HydraulicsPhases.Deserialize(saveData.m_HydraulicsPhases);
		SandboxSettings.Deserialize(saveData.m_Settings);
		DeserializeBridge(saveData.m_Bridge);
		if (!loadBridge)
		{
			Bridge.DestroyAllExceptPrebuilt();
			if (!Bridge.HasPrebuilts())
			{
				BridgeJoints.UnSplitAllJoints();
			}
		}
		ZedAxisVehicles.Deserialize(saveData.m_ZedAxisVehicles, saveData.m_Version);
		Vehicles.Deserialize(saveData.m_Vehicles, saveData.m_Version);
		Checkpoints.Deserialize(saveData.m_Checkpoints);
		VehicleStopTriggers.Deserialize(saveData.m_VehicleStopTriggers);
		VehicleRestartPhases.Deserialize(saveData.m_VehicleRestartPhases);
		EventTimelines.Deserialize(saveData.m_EventTimelines);
		TerrainIslands.Deserialize(saveData.m_TerrainStretches);
		Pillars.Deserialize(saveData.m_Pillars);
		Decors.Deserialize(saveData.m_Decors);
		Platforms.Deserialize(saveData.m_Platforms);
		Ramps.Deserialize(saveData.m_Ramps);
		FlyingObjects.Deserialize(saveData.m_FlyingObjects);
		Rocks.Deserialize(saveData.m_Rocks);
		WaterBlocks.Deserialize(saveData.m_WaterBlocks);
		BuildZones.Deserialize(saveData.m_BuildZones);
		CustomShapes.Deserialize(saveData.m_CustomShapes);
		Budget.Deserialize(saveData.m_Budget);
		WorkshopSubmit.Deserialize(saveData.m_Workshop);
		Vehicles.ResolveCheckpointGuids();
		Checkpoints.UpdateFloatingText();
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			BridgeJoints.HideSplitUI();
			BridgeEdges.HideJointSelectorUI();
		}
	}

	public static string GetSavePath(string profileName)
	{
		return Path.Combine(Application.persistentDataPath, Profiles.ROOT_DIRECTORY_NAME, profileName, SAVE_DIRECTORY);
	}

	public static string AddFileExtension(string filename)
	{
		if (Path.GetExtension(filename) == SAVE_EXTENSION)
		{
			return filename;
		}
		return filename + SAVE_EXTENSION;
	}

	public static void DeserializeBridge(BridgeSaveData bridgeSaveData)
	{
		BridgeSave.Deserialize(bridgeSaveData);
		Bridge.Sanitize();
		if (!SandboxSettings.m_ThreeWaySplitJointsEnabled)
		{
			SandboxSettings.m_ThreeWaySplitJointsEnabled = BridgeJoints.GetNumThreeWaySplitJoints() > 0;
		}
	}

	private static void Write(string relativePath, byte[] bytes)
	{
		string savePath = GetSavePath(Profiles.GetActiveProfileName());
		string path = Path.Combine(savePath, relativePath);
		string directoryName = Path.GetDirectoryName(path);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
		if (bytes != null && bytes.Length != 0 && bytes[0] != 0)
		{
			Utils.WriteBytesWithBackup(savePath, relativePath, bytes);
			string fileName = Path.GetFileName(path);
			if (m_SandboxLayoutCache.ContainsKey(fileName))
			{
				m_SandboxLayoutCache.Remove(fileName);
			}
		}
	}

	private static void MaybeFixUpThemeStubID(SandboxLayoutData sandboxLayoutData, string fullpath)
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullpath);
		CampaignWorld worldForAnyLevel = CampaignWorlds.m_Instance.GetWorldForAnyLevel(fileNameWithoutExtension);
		if (worldForAnyLevel != null)
		{
			sandboxLayoutData.m_ThemeStubId = worldForAnyLevel.m_ThemePreloadStub.m_ID;
		}
	}
}
