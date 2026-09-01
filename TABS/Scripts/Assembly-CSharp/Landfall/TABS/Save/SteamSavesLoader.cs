using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DM;
using Landfall.TABS.Workshop;
using Steamworks;
using TABS.Security;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.Save
{
	public class SteamSavesLoader : ISaveLoaderService, IService
	{
		private const int MaxLoadCorruptCloudFailCount = 10;

		private const string LoadCorruptCloudFailCountKey = "LdCrptCldFlCnt";

		private const string MESSAGE_FILE_NAME = "TABSSave.TABSSAVE";

		private readonly string TABS_LOCAL_SAVE_FILE = "TABSSAVE.TABSProgress";

		private SteamAPICall_t m_FileReadAsyncHandle;

		private CallResult<RemoteStorageFileWriteAsyncComplete_t> OnRemoteStorageFileWriteAsyncCompleteCallResult;

		private CallResult<RemoteStorageFileReadAsyncComplete_t> OnRemoteStorageFileReadAsyncCompleteCallResult;

		private int m_LoadCorruptCloudFailCount;

		private bool m_DidIncrementLoadCorruptCloudFailCount;

		private bool m_hasPendingSave;

		private IPlayerPrefsPlatform m_PlayerPrefs;

		private TABSSave m_currentSave;

		private bool m_DidLoadCloudsave;

		private bool m_DidLoadLocal;

		private SecretUnlockConditions m_secretUnlockConditions;

		private readonly IPlatformManager m_platformManager;

		private string SAVE_DIRECTORY => GamePaths.PersistentDataPath + "/Saved/";

		public TABSSave CurrentSave => m_currentSave;

		public bool IsReady { get; }

		public SteamSavesLoader()
		{
			m_platformManager = (SteamManager)ServiceLocator.GetService<IPlatformManager>();
			if (m_platformManager.Initialized)
			{
				string text = SteamUser.GetSteamID().ToString() + "_";
				TABS_LOCAL_SAVE_FILE = text + TABS_LOCAL_SAVE_FILE;
			}
			OnRemoteStorageFileWriteAsyncCompleteCallResult = CallResult<RemoteStorageFileWriteAsyncComplete_t>.Create(OnRemoteStorageFileWriteAsyncComplete);
			OnRemoteStorageFileReadAsyncCompleteCallResult = CallResult<RemoteStorageFileReadAsyncComplete_t>.Create(OnRemoteStorageFileReadAsyncComplete);
			m_currentSave = new TABSSave();
			m_secretUnlockConditions = Resources.Load<SecretUnlockConditions>("SecretUnlockConditions");
		}

		public bool HasBeatenLevel(DatabaseID lvl, DatabaseID campaignID)
		{
			return m_currentSave.HasBeatenLevel(lvl, campaignID);
		}

		public void AddBeatenLevel(DatabaseID lvl, DatabaseID campaignID)
		{
			if (!(lvl == default(DatabaseID)))
			{
				Debug.Log("Adding Saved Level: " + lvl);
				m_currentSave.AddLevel(lvl, campaignID);
				Save();
			}
		}

		public void ClearLevels(List<TABSCampaignAsset> availableCampaigns)
		{
			IPlayerPrefsPlatform service = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			service.SetInt("CompletedTutorial", 0);
			service.SetInt("CompletedPossessionTutorial", 0);
			if (availableCampaigns != null)
			{
				foreach (TABSCampaignAsset availableCampaign in availableCampaigns)
				{
					m_currentSave.ClearWinStreak(availableCampaign.Entity.GUID);
				}
			}
			m_currentSave.ClearLevels();
			Save();
		}

		public void ClearSecrets()
		{
			m_currentSave.ClearSecrets();
			Save();
		}

		public bool HasUnlockedSecret(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				return true;
			}
			return m_currentSave.HasUnlockedSecret(key);
		}

		public List<SecretUnlockCondition> UnlockSecret(string key)
		{
			m_currentSave.UnlockSecret(key);
			List<SecretUnlockCondition> list = new List<SecretUnlockCondition>();
			if (m_secretUnlockConditions != null)
			{
				list = m_secretUnlockConditions.CheckUnlockCondition(m_currentSave.UnlockedSecrets);
				foreach (SecretUnlockCondition item in list)
				{
					m_currentSave.UnlockSecret(item.m_unlock);
				}
			}
			Save();
			return list;
		}

		public void AddWinStreakLevel(DatabaseID lvl, DatabaseID campaignID)
		{
			m_currentSave.AddWinStreakLevel(lvl, campaignID);
			Save();
		}

		public void ClearWinStreak(DatabaseID campaignID)
		{
			m_currentSave.ClearWinStreak(campaignID);
			Save();
		}

		public bool HasBeatenCampaignWithoutLosing(DatabaseID campaignID)
		{
			TABSCampaignLevelAsset[] levelsInCampaign = ContentDatabase.Instance().GetCampaign(campaignID).LevelsInCampaign;
			foreach (TABSCampaignLevelAsset tABSCampaignLevelAsset in levelsInCampaign)
			{
				if (!m_currentSave.HasLevelInWinStreak(tABSCampaignLevelAsset.Entity.GUID, campaignID))
				{
					return false;
				}
			}
			return true;
		}

		public bool LevelIsInWinStreak(DatabaseID lvl, DatabaseID campaignID)
		{
			return m_currentSave.HasLevelInWinStreak(lvl, campaignID);
		}

		public void AddNonPossessedLevel(DatabaseID lvl, DatabaseID campaignID)
		{
			m_currentSave.AddNonPossessedLevel(lvl, campaignID);
			Save();
		}

		public void ClearNonPossessedCampaign(DatabaseID campaignID)
		{
			m_currentSave.ClearNonPossessedCampaign(campaignID);
			Save();
		}

		public bool HasBeatenNonPossessedCamapaign(DatabaseID campaignID)
		{
			TABSCampaignLevelAsset[] levelsInCampaign = ContentDatabase.Instance().GetCampaign(campaignID).LevelsInCampaign;
			foreach (TABSCampaignLevelAsset tABSCampaignLevelAsset in levelsInCampaign)
			{
				if (!m_currentSave.HasLevelInNonPossessedCampaign(tABSCampaignLevelAsset.Entity.GUID, campaignID))
				{
					return false;
				}
			}
			return true;
		}

		private void Save()
		{
			m_hasPendingSave = false;
			if (m_platformManager.Initialized && IsCloudEnabled())
			{
				SaveSteam();
			}
			else
			{
				SaveLocal();
			}
		}

		public void Load()
		{
			Debug.Log("(TABS SAVE)--- Loading Saved ---");
			if (m_platformManager.Initialized && IsCloudEnabled())
			{
				ReadSteam();
			}
			else
			{
				ReadLocal();
			}
		}

		private byte[] GetSavedData()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, m_currentSave);
			byte[] result = memoryStream.ToArray();
			memoryStream.Close();
			return result;
		}

		private void SaveSteam()
		{
			byte[] savedData = GetSavedData();
			SaveLocalTemp(savedData);
			Debug.Log("Saving Cloud Save...");
			if (!m_DidLoadCloudsave && DoesCloudsaveExist())
			{
				Debug.LogError("Hm, Could not load Cloudsave but it exists, trying to load again to prevent overwriting data...");
				Load();
			}
			else
			{
				SteamAPICall_t hAPICall = SteamRemoteStorage.FileWriteAsync("TABSSave.TABSSAVE", savedData, (uint)savedData.Length);
				OnRemoteStorageFileWriteAsyncCompleteCallResult.Set(hAPICall);
			}
		}

		private void ReadSteam()
		{
			if (!DoesCloudsaveExist() || !IsCloudEnabled())
			{
				ReadLocal();
				Debug.LogError("Has no cloud save: First time?");
				return;
			}
			int fileSize = SteamRemoteStorage.GetFileSize("TABSSave.TABSSAVE");
			byte[] array = new byte[fileSize];
			if (SteamRemoteStorage.FileRead("TABSSave.TABSSAVE", array, fileSize) == array.Length)
			{
				LoadSaveFromBytes(array);
			}
			else
			{
				ReadLocal();
			}
		}

		private bool ReadLocal()
		{
			string path = SAVE_DIRECTORY + TABS_LOCAL_SAVE_FILE;
			if (!File.Exists(path))
			{
				m_DidLoadLocal = false;
				return false;
			}
			byte[] array = File.ReadAllBytes(path);
			try
			{
				BinaryFormatter obj = new BinaryFormatter
				{
					Binder = new TargetTypeSerializationBinder<TABSSave>()
				};
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(array, 0, array.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				TABSSave tABSSave = obj.Deserialize(memoryStream) as TABSSave;
				tABSSave.Upgrade();
				memoryStream.Close();
				Debug.Log("Reading Local Save...");
				m_currentSave = tABSSave;
			}
			catch (Exception ex)
			{
				Debug.LogError("Error: " + ex.Message);
				Debug.LogError("Stack: " + ex.StackTrace);
			}
			m_DidLoadLocal = m_currentSave != null;
			return m_DidLoadLocal;
		}

		private void SaveLocal()
		{
			byte[] savedData = GetSavedData();
			SaveLocalTemp(savedData);
		}

		private void SaveLocalTemp(byte[] data)
		{
			Debug.Log("Saving local save");
			try
			{
				if (!Directory.Exists(SAVE_DIRECTORY))
				{
					Directory.CreateDirectory(SAVE_DIRECTORY);
				}
				File.WriteAllBytes(SAVE_DIRECTORY + TABS_LOCAL_SAVE_FILE, data);
			}
			catch
			{
				Debug.Log("Could not save local save");
			}
		}

		private bool DoesCloudsaveExist()
		{
			return SteamRemoteStorage.FileExists("TABSSave.TABSSAVE");
		}

		private bool IsCloudEnabled()
		{
			bool num = SteamRemoteStorage.IsCloudEnabledForApp();
			bool flag = SteamRemoteStorage.IsCloudEnabledForAccount();
			return num && flag;
		}

		private void OnRemoteStorageFileWriteAsyncComplete(RemoteStorageFileWriteAsyncComplete_t pCallback, bool bIOFailure)
		{
			if (pCallback.m_eResult != EResult.k_EResultOK)
			{
				Debug.LogError("Error During Cloud save: " + pCallback.m_eResult);
			}
		}

		private void OnRemoteStorageFileReadAsyncComplete(RemoteStorageFileReadAsyncComplete_t pCallback, bool bIOFailure)
		{
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				byte[] array = new byte[pCallback.m_cubRead];
				if (SteamRemoteStorage.FileReadAsyncComplete(pCallback.m_hFileReadAsync, array, pCallback.m_cubRead))
				{
					LoadSaveFromBytes(array);
				}
				else
				{
					Debug.LogError("Failed To Read CloudSave, Got Data but failed to read");
				}
			}
			else
			{
				Debug.LogError("Failed To Read CloudSave..." + pCallback.m_eResult);
			}
		}

		private void LoadSaveFromBytes(byte[] data)
		{
			TABSSave tABSSave = null;
			try
			{
				BinaryFormatter obj = new BinaryFormatter
				{
					Binder = new TargetTypeSerializationBinder<TABSSave>()
				};
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(data, 0, data.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				tABSSave = (TABSSave)obj.Deserialize(memoryStream);
				tABSSave.Upgrade();
				memoryStream.Close();
			}
			catch (Exception ex)
			{
				Debug.LogError("Error: " + ex.Message);
				Debug.LogError("Stack: " + ex.StackTrace);
			}
			if (tABSSave == null)
			{
				if (!m_DidIncrementLoadCorruptCloudFailCount)
				{
					m_DidIncrementLoadCorruptCloudFailCount = true;
					int num = m_LoadCorruptCloudFailCount + 1;
					if (num >= 10)
					{
						num = 0;
						m_hasPendingSave = true;
						m_DidLoadCloudsave = true;
					}
					SetLoadCorruptCloudFailCount(num);
				}
				if (!m_DidLoadLocal)
				{
					ReadLocal();
				}
			}
			else
			{
				SetLoadCorruptCloudFailCount(0);
				if (ReadLocal())
				{
					tABSSave.Merge(m_currentSave);
					m_currentSave = tABSSave;
				}
				else
				{
					m_currentSave = tABSSave;
				}
				m_DidLoadCloudsave = true;
			}
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
			m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			m_LoadCorruptCloudFailCount = m_PlayerPrefs.GetInt("LdCrptCldFlCnt");
			Load();
		}

		public void OnUpdate()
		{
			if (m_hasPendingSave)
			{
				Save();
			}
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}

		public Faction[] GetFactionBar()
		{
			return m_currentSave.GetFactionBar();
		}

		public void SetFactionBar(Faction[] factions)
		{
			m_currentSave.SetFactionBar(factions);
			Save();
		}

		private void SetLoadCorruptCloudFailCount(int count)
		{
			m_LoadCorruptCloudFailCount = count;
			m_PlayerPrefs.SetInt("LdCrptCldFlCnt", m_LoadCorruptCloudFailCount);
			m_PlayerPrefs.Save();
		}
	}
}
