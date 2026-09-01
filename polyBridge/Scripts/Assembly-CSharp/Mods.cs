using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class Mods
{
	private static List<string> m_ActiveModDirectories = new List<string>();

	private static List<string> m_AutoAddedModList = new List<string>();

	private static List<string> m_AutoRemovedModList = new List<string>();

	public static bool m_IsUsingGameplayMod = false;

	public static readonly string MOD_LOAD_FILENAME = "OnModLoad.lua";

	public static readonly string MOD_UPDATE_FILENAME = "OnUpdate.lua";

	public static readonly string MOD_FIXED_UPDATE_FILENAME = "OnFixedUpdate.lua";

	public static readonly string EMBEDDED_MODS_FILENAME = "EmbeddedMods.txt";

	public static void Init()
	{
		Utils.CreateDirectory(GetLocalTestModsDirectoryPath());
	}

	public static void SetActiveModsFromProfile()
	{
		try
		{
			m_ActiveModDirectories.Clear();
			foreach (string activeModDirectory in Profiles.m_ActiveProfile.m_ActiveModDirectories)
			{
				if (!string.IsNullOrEmpty(activeModDirectory) && Workshop.m_SubscribedItems.ContainsKey(activeModDirectory))
				{
					ActivateMod(activeModDirectory);
				}
			}
			foreach (string activeLocalModDirectory in Profiles.m_ActiveProfile.m_ActiveLocalModDirectories)
			{
				if (Utils.DirectoryExists(Path.Combine(GetLocalTestModsDirectoryPath(), activeLocalModDirectory)))
				{
					ActivateMod(activeLocalModDirectory);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception in SetActiveModsFromProfile: " + ex.Message);
		}
	}

	public static List<string> GetActiveModDirectories()
	{
		return m_ActiveModDirectories;
	}

	public static List<string> GetActiveCheatModDirectories()
	{
		List<string> list = new List<string>();
		foreach (string activeModDirectory in m_ActiveModDirectories)
		{
			string pathToMod = GetPathToMod(activeModDirectory);
			if (ModDirectoryHasCheats(pathToMod))
			{
				list.Add(pathToMod);
			}
		}
		return list;
	}

	public static bool ModDirectoryHasCheats(string modDirectory)
	{
		FileInfo[] luaFilesInMod = GetLuaFilesInMod(modDirectory);
		if (luaFilesInMod != null && luaFilesInMod.Length != 0)
		{
			return ModApi.CheckForCheatFunctions(luaFilesInMod);
		}
		return false;
	}

	public static bool ModDirectoryHasUGC(string modDirectory)
	{
		FileInfo[] luaFilesInMod = GetLuaFilesInMod(modDirectory);
		if (luaFilesInMod != null && luaFilesInMod.Length != 0)
		{
			if (ModApi.CheckForVehicleUGCFunctions(luaFilesInMod))
			{
				return true;
			}
			if (ModApi.CheckForZVehicleUGCFunctions(luaFilesInMod))
			{
				return true;
			}
			if (ModApi.CheckForCustomShapeUGCFunctions(luaFilesInMod))
			{
				return true;
			}
			if (ModApi.CheckForDecorUGCFunctions(luaFilesInMod))
			{
				return true;
			}
			return false;
		}
		return false;
	}

	public static bool ModIsLocalTest(string modDirectory)
	{
		return modDirectory.StartsWith(GetLocalTestModsDirectoryPath());
	}

	public static bool IsUsingLocalUGC()
	{
		foreach (string activeModDirectory in m_ActiveModDirectories)
		{
			string pathToMod = GetPathToMod(activeModDirectory);
			if (ModIsLocalTest(pathToMod) && ModDirectoryHasUGC(pathToMod))
			{
				return true;
			}
		}
		return false;
	}

	public static bool ModIsActive(string itemID)
	{
		return m_ActiveModDirectories.Contains(itemID);
	}

	public static void ActivateMod(string itemID)
	{
		if (!m_ActiveModDirectories.Contains(itemID))
		{
			m_ActiveModDirectories.Add(itemID);
		}
	}

	public static void DeleteFromWorkshopPath(string itemID)
	{
		string pathToMod = GetPathToMod(itemID);
		if (!string.IsNullOrEmpty(pathToMod))
		{
			Utils.DeleteDirectoryAndContents(pathToMod);
		}
	}

	public static void DeactivateMod(string itemID)
	{
		if (m_ActiveModDirectories.Contains(itemID))
		{
			m_ActiveModDirectories.Remove(itemID);
		}
	}

	public static void ClearActiveMods()
	{
		m_ActiveModDirectories.Clear();
		Profiles.m_ActiveProfile.m_ActiveModDirectories.Clear();
		Profiles.SaveActiveProfile();
	}

	public static void ApplyActiveMods()
	{
		Profiles.m_ActiveProfile.m_ActiveModDirectories.Clear();
		foreach (string activeModDirectory in m_ActiveModDirectories)
		{
			Profiles.m_ActiveProfile.m_ActiveModDirectories.Add(activeModDirectory);
		}
		Profiles.SaveActiveProfile();
		RefreshAllMods(null);
	}

	public static void RefreshMod(string modPath)
	{
		if (string.IsNullOrEmpty(modPath))
		{
			Debug.LogWarning("Invalid mod path: " + modPath);
			return;
		}
		string path = Path.Combine(modPath, MOD_LOAD_FILENAME);
		if (File.Exists(path))
		{
			try
			{
				ModApi.RunScript(modPath, File.ReadAllText(path));
			}
			catch (Exception ex)
			{
				string fileName = Path.GetFileName(modPath);
				ModApi.AddErrorMessageToQueue(Localize.Get("UI_MODS_ERROR_ONMODLOAD", fileName) + " " + ex.Message);
			}
		}
		path = Path.Combine(modPath, MOD_UPDATE_FILENAME);
		if (File.Exists(path))
		{
			ModApi.AddOnUpdate(modPath, File.ReadAllText(path));
		}
		path = Path.Combine(modPath, MOD_FIXED_UPDATE_FILENAME);
		if (File.Exists(path))
		{
			ModApi.AddOnFixedUpdate(modPath, File.ReadAllText(path));
		}
	}

	public static async void RefreshAllMods(Action callback)
	{
		try
		{
			Profiles.m_ActiveProfile.m_DidCrashOnModLoad = true;
			Profiles.SaveActiveProfile();
			m_IsUsingGameplayMod = false;
			ModApi.ResetAllToDefault();
			foreach (string activeModDirectory in m_ActiveModDirectories)
			{
				RefreshMod(GetPathToMod(activeModDirectory));
			}
			if (!Localize.IsBuiltInLanguageCode(Profiles.m_ActiveProfile.m_LanguageCode) && !ModIsActive(Profiles.m_ActiveProfile.m_LanguageCode))
			{
				Profiles.m_ActiveProfile.m_LanguageCode = Localize.GetSystemLanguageCode();
				Localize.SwitchToLanguage(Profiles.m_ActiveProfile.m_LanguageCode);
			}
			await Task.Delay(250);
			Profiles.m_ActiveProfile.m_DidCrashOnModLoad = false;
			Profiles.SaveActiveProfile();
		}
		catch (Exception ex)
		{
			Debug.LogWarning("HANDLED: " + ex.Message);
		}
		callback?.Invoke();
	}

	public static List<string> GetAllModsInLayout(SandboxLayoutData layoutData)
	{
		List<string> list = new List<string>();
		if (layoutData == null)
		{
			return list;
		}
		foreach (ZedAxisVehicleProxy zedAxisVehicle in layoutData.m_ZedAxisVehicles)
		{
			if (!string.IsNullOrEmpty(zedAxisVehicle.m_ModId) && !list.Contains(zedAxisVehicle.m_ModId))
			{
				list.Add(zedAxisVehicle.m_ModId);
			}
		}
		foreach (VehicleProxy vehicle in layoutData.m_Vehicles)
		{
			if (!string.IsNullOrEmpty(vehicle.m_ModId) && !list.Contains(vehicle.m_ModId))
			{
				list.Add(vehicle.m_ModId);
			}
			string modIdPrefix = GetModIdPrefix(vehicle.m_SkinID);
			if (!string.IsNullOrEmpty(modIdPrefix) && !list.Contains(modIdPrefix))
			{
				list.Add(modIdPrefix);
			}
		}
		foreach (DecorProxy decor in layoutData.m_Decors)
		{
			if (!string.IsNullOrEmpty(decor.m_ModId) && !list.Contains(decor.m_ModId))
			{
				list.Add(decor.m_ModId);
			}
		}
		foreach (CustomShapeProxy customShape in layoutData.m_CustomShapes)
		{
			string modIdPrefix2 = GetModIdPrefix(customShape.m_TextureId);
			if (!string.IsNullOrEmpty(modIdPrefix2) && !list.Contains(modIdPrefix2))
			{
				list.Add(modIdPrefix2);
			}
		}
		list.Remove(string.Empty);
		return list;
	}

	public static void DeactivateAutoLoadedMods()
	{
		foreach (string autoRemovedMod in m_AutoRemovedModList)
		{
			if (!m_ActiveModDirectories.Contains(autoRemovedMod))
			{
				ActivateMod(autoRemovedMod);
			}
		}
		m_AutoRemovedModList.Clear();
		if (m_AutoAddedModList.Count <= 0)
		{
			return;
		}
		foreach (string autoAddedMod in m_AutoAddedModList)
		{
			DeactivateMod(autoAddedMod);
		}
		m_AutoAddedModList.Clear();
		LoadModsFromProfile(null);
	}

	public static void AddAutoLoadedMods(List<string> modsToActivate)
	{
		foreach (string item in modsToActivate)
		{
			string fileName = Path.GetFileName(item);
			if (m_ActiveModDirectories.Contains(fileName))
			{
				m_AutoRemovedModList.Add(fileName);
				DeactivateMod(fileName);
			}
		}
		m_AutoAddedModList.Clear();
		foreach (string item2 in modsToActivate)
		{
			m_AutoAddedModList.Add(item2);
		}
	}

	public static void ActivateAutoLoadedMods()
	{
		foreach (string autoAddedMod in m_AutoAddedModList)
		{
			ActivateMod(autoAddedMod);
		}
	}

	public static void LoadModsFromProfile(Action callback)
	{
		if (Profiles.m_ActiveProfile.m_DidCrashOnModLoad)
		{
			Profiles.m_ActiveProfile.m_ActiveModDirectories.Clear();
			Profiles.m_ActiveProfile.m_DidCrashOnModLoad = false;
			Profiles.SaveActiveProfile();
			ModApi.AddErrorMessageToQueue(Localize.Get("UI_MODS_DID_CRASH_ON_LAUNCH"));
			callback?.Invoke();
		}
		else
		{
			RefreshAllMods(callback);
		}
	}

	public static string GetPathToMod(string itemID)
	{
		if (itemID == null)
		{
			return string.Empty;
		}
		string text = Path.Combine(GetLocalTestModsDirectoryPath(), itemID);
		if (Directory.Exists(text))
		{
			return text;
		}
		if (!string.IsNullOrEmpty(itemID) && Workshop.m_SubscribedItems.ContainsKey(itemID))
		{
			return Workshop.m_SubscribedItems[itemID].m_InstallPath;
		}
		return string.Empty;
	}

	public static string GetLocalTestModsDirectoryPath()
	{
		return Path.Combine(Path.GetFullPath(Application.persistentDataPath), "CreatedMods");
	}

	public static int GetNumLocalMods()
	{
		string localTestModsDirectoryPath = GetLocalTestModsDirectoryPath();
		if (!Utils.DirectoryExists(localTestModsDirectoryPath))
		{
			return 0;
		}
		return Directory.GetDirectories(localTestModsDirectoryPath, "*", SearchOption.TopDirectoryOnly).Length;
	}

	public static List<string> GetInactiveModsInLayout(string layoutPath)
	{
		List<string> list = new List<string>();
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(layoutPath);
		if (sandboxLayoutData == null)
		{
			Debug.LogWarningFormat("Could not load: {0}", layoutPath);
			return list;
		}
		string text = Path.Combine(Path.GetDirectoryName(layoutPath), EMBEDDED_MODS_FILENAME);
		if (File.Exists(text))
		{
			list.Add(text);
		}
		foreach (string item in GetAllModsInLayout(sandboxLayoutData))
		{
			if (!Profiles.m_ActiveProfile.m_ActiveModDirectories.Contains(item) && !m_AutoAddedModList.Contains(item))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public static FileInfo[] GetLuaFilesInMod(string modDirPath)
	{
		try
		{
			if (!Directory.Exists(modDirPath))
			{
				return null;
			}
			return new DirectoryInfo(modDirPath)?.GetFiles("*.lua", SearchOption.AllDirectories);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in GetFilesInMod: " + ex.Message);
			return null;
		}
	}

	private static string GetModIdPrefix(string id)
	{
		if (!string.IsNullOrEmpty(id))
		{
			int num = id.IndexOf('_');
			if (num == 10)
			{
				string text = id.Substring(0, num);
				if (ulong.TryParse(text, out var _))
				{
					return text;
				}
			}
		}
		return string.Empty;
	}
}
