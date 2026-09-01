using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class Profiles
{
	public static Profile m_ActiveProfile;

	public static string ROOT_DIRECTORY_NAME = "Profiles";

	public static string ROOT_DIRECTORY_NAME_NO_SYNC = "ProfilesNoSync";

	public static string PROFILE_SETTINGS_FILENAME = "settings";

	public static string PROFILE_GAME_STATS_FILENAME = "stats";

	public static string PROFILE_LEADERBOARDS_PROGRESS_FILENAME = "leaderboards";

	public static string DEFAULT_AVATAR_ADDRESSABLE = "Van";

	public static string DEFAULT_AVATAR_SKIN = "Red";

	public static readonly int NAME_CHARACTER_LIMIT = 24;

	public static readonly int MAX_SLOTS = 8;

	public static int CURRENT_VERSION = 116;

	private static string PROFILE_SLOT_FILENAME = ".slot";

	public static void AssignSlots()
	{
		List<string> profileNames = GetProfileNames();
		if (profileNames == null || profileNames.Count == 0)
		{
			return;
		}
		HashSet<int> hashSet = new HashSet<int>();
		foreach (string item in profileNames)
		{
			string fullPath = Path.Combine(Application.persistentDataPath, ROOT_DIRECTORY_NAME, item, PROFILE_SLOT_FILENAME);
			bool flag = false;
			if (Utils.FileExists(fullPath))
			{
				string text = Utils.ReadAllText(fullPath);
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, out var result) && result >= 0 && result < MAX_SLOTS && !hashSet.Contains(result))
				{
					flag = true;
					hashSet.Add(result);
				}
			}
			if (flag)
			{
				continue;
			}
			for (int i = 0; i < MAX_SLOTS; i++)
			{
				if (!hashSet.Contains(i) && WriteSlotIndex(item, i))
				{
					hashSet.Add(i);
					break;
				}
			}
		}
	}

	public static bool WriteSlotIndex(string profileName, int slotIndex)
	{
		return Utils.WriteAllText(Path.Combine(Application.persistentDataPath, ROOT_DIRECTORY_NAME, profileName, PROFILE_SLOT_FILENAME), slotIndex.ToString());
	}

	public static int GetSlotIndex(string profileName)
	{
		string fullPath = Path.Combine(Application.persistentDataPath, ROOT_DIRECTORY_NAME, profileName, PROFILE_SLOT_FILENAME);
		if (Utils.FileExists(fullPath))
		{
			string text = Utils.ReadAllText(fullPath);
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, out var result) && result >= 0 && result < MAX_SLOTS)
			{
				return result;
			}
		}
		return -1;
	}

	public static void LoadActiveProfile()
	{
		string activeProfileName = ProfileInfo.GetActiveProfileName();
		if (string.IsNullOrEmpty(activeProfileName))
		{
			Profile profile = new Profile();
			profile.Init(Localize.Get("UI_DEFAULT_PROFILE_NAME"));
			profile.Write();
			SetActiveProfile(profile);
			profile.Apply();
			WriteSlotIndex(profile.m_Name, 0);
		}
		else
		{
			ProfileInfo.WriteActiveProfileName(activeProfileName);
			m_ActiveProfile = LoadProfile(activeProfileName);
			m_ActiveProfile.Apply();
			LoadActiveProfileProgress();
		}
	}

	public static Profile LoadProfile(string profileName)
	{
		Profile profile = new Profile();
		profile.Init(profileName);
		profile.Load();
		return profile;
	}

	public static void LoadActiveProfileProgress()
	{
		if (!CampaignProgress.Load())
		{
			CampaignWorlds.m_Instance.SetDefaultProgress();
		}
		WeeklyChallengesProgress.Load();
		WorkshopRecentlyPlayed.Load();
	}

	public static string GetActiveProfileName()
	{
		if (m_ActiveProfile == null)
		{
			Debug.LogWarning("No active profile when calling GetActiveProfileName(). Falling back to Default profile");
		}
		if (m_ActiveProfile != null)
		{
			return m_ActiveProfile.m_Name;
		}
		return Localize.Get("UI_DEFAULT_PROFILE_NAME");
	}

	public static void SetActiveProfile(Profile profile)
	{
		m_ActiveProfile = profile;
		ProfileInfo.WriteActiveProfileName(profile.m_Name);
	}

	public static void RenameActiveProfile(string newName)
	{
		m_ActiveProfile.m_Name = newName;
		ProfileInfo.WriteActiveProfileName(newName);
	}

	public static void SaveActiveProfile()
	{
		m_ActiveProfile.Write();
	}

	public static void ResetLastPlayedCampaignData()
	{
		m_ActiveProfile.m_LastPlayedLevelIDs.Clear();
		m_ActiveProfile.m_LastLoadedCampaignLevelId = string.Empty;
	}

	public static void Delete(string profileName)
	{
		Utils.DeleteDirectoryAndContents(GetProfileDirectory(profileName));
	}

	public static List<string> GetProfileNames()
	{
		string text = Path.Combine(Application.persistentDataPath, ROOT_DIRECTORY_NAME);
		if (!Utils.DirectoryExists(text))
		{
			return null;
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(text);
		if (directoryInfo == null)
		{
			return null;
		}
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		if (directories.Length == 0)
		{
			return null;
		}
		List<string> list = new List<string>();
		DirectoryInfo[] array = directories;
		foreach (DirectoryInfo directoryInfo2 in array)
		{
			if (Utils.FileExists(Path.Combine(text, directoryInfo2.Name, PROFILE_SETTINGS_FILENAME)))
			{
				list.Add(directoryInfo2.Name);
			}
		}
		return list;
	}

	public static bool Exists(string name)
	{
		List<string> profileNames = GetProfileNames();
		if (profileNames != null)
		{
			foreach (string item in profileNames)
			{
				if (name.ToLower() == item.ToLower())
				{
					return true;
				}
			}
		}
		return false;
	}

	public static string GetProfileRootDirectory()
	{
		return Path.Combine(Application.persistentDataPath, ROOT_DIRECTORY_NAME);
	}

	public static string GetProfileDirectory(string profileName)
	{
		return Path.Combine(Application.persistentDataPath, ROOT_DIRECTORY_NAME, profileName);
	}

	public static string GetProfileDirectoryNoSync(string profileName)
	{
		return Path.Combine(Application.persistentDataPath, ROOT_DIRECTORY_NAME_NO_SYNC, profileName);
	}

	public static ProfileProxy LoadProfileProxy(string profileName)
	{
		string profileDirectory = GetProfileDirectory(profileName);
		if (!Directory.Exists(profileDirectory))
		{
			return null;
		}
		try
		{
			string path = Path.Combine(profileDirectory, PROFILE_SETTINGS_FILENAME);
			if (File.Exists(path))
			{
				byte[] array = File.ReadAllBytes(path);
				if (array != null && array.Length != 0)
				{
					return SerializationUtility.DeserializeValue<ProfileProxy>(array, DataFormat.JSON);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Caught exception reading profile: {0}", ex.Message.ToString());
		}
		return null;
	}

	public static Sprite GetSpriteForVehicle(string vehicleAddressable, string skinName)
	{
		Sprite result = GameUI.m_Instance.m_DefaultAvatarSprite;
		VehicleStub stubByAddressable = VehicleStubs.GetStubByAddressable(vehicleAddressable);
		if (stubByAddressable != null)
		{
			if (stubByAddressable.m_Skins != null && stubByAddressable.m_Skins.Length != 0)
			{
				result = stubByAddressable.m_Skins[0].m_Icon;
				if (!string.IsNullOrEmpty(skinName))
				{
					VehicleSkin[] skins = stubByAddressable.m_Skins;
					foreach (VehicleSkin vehicleSkin in skins)
					{
						if (skinName == vehicleSkin.m_DisplayNameLocID)
						{
							result = vehicleSkin.m_Icon;
							break;
						}
					}
				}
			}
			else
			{
				result = stubByAddressable.m_Icon;
			}
		}
		return result;
	}

	public static bool Move(string sourceProfileName, string destProfileName)
	{
		try
		{
			string profileDirectory = GetProfileDirectory(sourceProfileName);
			string profileDirectory2 = GetProfileDirectory(destProfileName);
			Directory.Move(profileDirectory, profileDirectory2);
			string profileDirectoryNoSync = GetProfileDirectoryNoSync(sourceProfileName);
			string profileDirectoryNoSync2 = GetProfileDirectoryNoSync(destProfileName);
			Directory.Move(profileDirectoryNoSync, profileDirectoryNoSync2);
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Failed to rename profile from '" + sourceProfileName + "' to '" + destProfileName + "' due to exception '" + ex.Message + "'");
			return false;
		}
	}

	public static string GetProfileSandboxLocation()
	{
		return $"{GetActiveProfileName()}{Path.DirectorySeparatorChar}Sandbox{Path.DirectorySeparatorChar}";
	}
}
