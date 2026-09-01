using System.Collections.Generic;
using System.Text;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class MemoryTest : MonoBehaviour
{
	public Text m_Text;

	private ProfilerRecorder m_TextureMemoryRecorder;

	private ProfilerRecorder m_MeshMemoryRecorder;

	private Dictionary<string, GameObject> m_LoadedAssets = new Dictionary<string, GameObject>();

	private List<string> m_AddressableVehicles = new List<string>
	{
		"Ambulance", "ArticulatedBus", "Bulldozer", "CactusRally", "Caravan", "Chopper", "Delorean", "DumpTruck", "CompactCar", "DuneBuggy",
		"FarmerTruck", "FireTruck", "Jeep", "Limo", "MailVan", "MiniVan", "ModelT", "MonsterTruck", "PickupTruck", "PoliceCar",
		"SchoolBus", "SchoolBusNew", "SportsCar", "SteamCar", "Taxi", "TowTruck", "Truck", "TruckNew", "TruckWithContainer", "TruckWithFlatbed",
		"TruckWithLiquid", "Van", "Vespa", "TowTruckNew", "MiniMover", "DuneBuggyNew", "BigWheelBuggy", "DirtBike", "Forklift"
	};

	private List<string> m_AddressableZedAxis = new List<string>
	{
		"Steamboat", "ShowJet", "Blimp", "SpeedBoat", "Sailboat", "Submarine", "CruiseShip", "SeaPlane", "PirateShip", "BiPlane",
		"Hydrofoil", "Barrier", "RedChonker", "Shuttle", "HelicopterStubby", "WidePlane", "LongShip", "SwampHover", "JawBreaker"
	};

	private void Start()
	{
		uConsole.RegisterCommand("load_asset", load_asset);
		uConsole.RegisterCommand("load_theme", load_theme);
		uConsole.RegisterCommand("unload_asset", unload_asset);
		uConsole.RegisterCommand("reset", reset);
		uConsole.RegisterCommand("load_all_vehicles", load_all_vehicles);
		uConsole.RegisterCommand("load_all_zed", load_all_zed);
	}

	private void OnEnable()
	{
		m_TextureMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Texture Memory");
		m_MeshMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Mesh Memory");
	}

	private void OnDisable()
	{
		m_TextureMemoryRecorder.Dispose();
	}

	private void Update()
	{
		StringBuilder stringBuilder = new StringBuilder(500);
		if (m_TextureMemoryRecorder.Valid)
		{
			float num = m_TextureMemoryRecorder.LastValue;
			num /= 1024f;
			string text = (num / 1024f).ToString("0.00");
			stringBuilder.AppendLine("Texture Memory: " + text + " MB");
			float num2 = m_MeshMemoryRecorder.LastValue;
			num2 /= 1024f;
			string text2 = (num2 / 1024f).ToString("0.00");
			stringBuilder.AppendLine("Mesh Memory (Includes Console UI Mesh): " + text2 + " MB");
		}
		m_Text.text = stringBuilder.ToString();
	}

	private void load_all_vehicles()
	{
		foreach (string addressableVehicle in m_AddressableVehicles)
		{
			LoadAndCreate(addressableVehicle);
		}
	}

	private void load_all_zed()
	{
		foreach (string item in m_AddressableZedAxis)
		{
			LoadAndCreate(item);
		}
	}

	private void load_asset()
	{
		if (uConsole.GetNumParameters() == 0)
		{
			uConsole.Log("Need asset name");
			return;
		}
		string assetName = uConsole.GetString();
		LoadAndCreate(assetName);
	}

	private void load_theme()
	{
		if (uConsole.GetNumParameters() == 0)
		{
			uConsole.Log("Need theme name");
			return;
		}
		string themeName = uConsole.GetString();
		LoadAndCreateTheme(themeName);
	}

	private void unload_asset()
	{
		if (uConsole.GetNumParameters() == 0)
		{
			uConsole.Log("Need asset name");
			return;
		}
		string assetName = uConsole.GetString();
		UnloadAndDestroy(assetName);
	}

	private void reset()
	{
		List<string> list = new List<string>();
		list.AddRange(m_LoadedAssets.Keys);
		foreach (string item in list)
		{
			UnloadAndDestroy(item);
		}
	}

	private void LoadAndCreate(string assetName)
	{
		if (m_LoadedAssets.ContainsKey(assetName))
		{
			uConsole.Log("Already loaded " + assetName);
		}
		else
		{
			Prefabs.m_Instance.PreloadSingleAsset(assetName, string.Empty, LoadAssetCallback);
		}
	}

	private void LoadAssetCallback(string assetName, string instanceID, bool success)
	{
		if (success)
		{
			uConsole.Log("Asset loaded: " + assetName);
			GameObject value = Object.Instantiate(Prefabs.GetAsyncPrefab(assetName));
			m_LoadedAssets.Add(assetName, value);
		}
	}

	private void LoadAndCreateTheme(string themeName)
	{
		if (m_LoadedAssets.ContainsKey(themeName))
		{
			uConsole.Log("Already loaded " + themeName);
		}
		else
		{
			Prefabs.m_Instance.PreloadSingleTheme(themeName, string.Empty, LoadThemeCallback);
		}
	}

	private void LoadThemeCallback(string themeName, string instanceID, bool success)
	{
		if (success)
		{
			uConsole.Log("Theme loaded: " + themeName);
			m_LoadedAssets.Add(themeName, null);
		}
	}

	private void UnloadAndDestroy(string assetName)
	{
		if (m_LoadedAssets.ContainsKey(assetName))
		{
			uConsole.Log("Asset unloaded: " + assetName);
			if (m_LoadedAssets[assetName] != null)
			{
				Object.Destroy(m_LoadedAssets[assetName]);
			}
			Prefabs.ReleaseAsset(assetName);
			m_LoadedAssets.Remove(assetName);
		}
	}
}
