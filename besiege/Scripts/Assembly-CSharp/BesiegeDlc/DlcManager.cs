using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BesiegeDlc
{
	internal class DlcManager
	{
		public enum DlcType
		{
			Water = 1
		}

		public class DlcStatus
		{
			public DlcType type;

			public DlcStatusType status;
		}

		public enum DlcStatusType
		{
			Allowed = 0,
			MissingDlc = 1,
			DisabledOnServer = 2
		}

		public Action DlcManagerInitialized;

		public Action DlcSettingsChanged;

		public static DlcManager Instance;

		private Dictionary<BlockType, DlcType> blocks;

		private Dictionary<int, DlcType> levelPrefabs;

		private Dictionary<LevelSettings.LevelEnvironment, DlcType> environments;

		private DlcProviderBase dlcProvider;

		private DlcInfo info;

		public DlcManager()
		{
			Instance = this;
		}

		public void Initialize()
		{
			info = Resources.Load<DlcInfo>("DlcInfo");
			if (info == null)
			{
				Debug.LogError("Failed to setup DLC Manager, couldn't load DlcInfo!");
				return;
			}
			Dictionary<DlcType, DlcInfo.Dlc> list = info.GetInfo();
			blocks = new Dictionary<BlockType, DlcType>();
			levelPrefabs = new Dictionary<int, DlcType>();
			environments = new Dictionary<LevelSettings.LevelEnvironment, DlcType>();
			if (SteamManager.Initialized)
			{
				dlcProvider = new SteamDlcProvider(list, OnProviderInitialized, OnDlcPackageInstalled);
			}
			else
			{
				Debug.LogWarning("Steam is not initialized");
				dlcProvider = new NullDlcProvider(list, OnProviderInitialized, OnDlcPackageInstalled);
			}
			dlcProvider.SetUp();
		}

		public void CleanUp()
		{
			if (dlcProvider != null)
			{
				dlcProvider.CleanUp();
			}
			dlcProvider = null;
			Instance = null;
		}

		private void OnDlcPackageInstalled(DlcType dlcType)
		{
			SingleInstanceFindOnly<DlcRestartNotice>.Instance.Open();
		}

		private void OnProviderInitialized()
		{
			if (DlcManagerInitialized != null)
			{
				DlcManagerInitialized();
			}
		}

		public void OnUpdate()
		{
			if (dlcProvider != null)
			{
				dlcProvider.OnUpdate();
			}
		}

		public bool GetBlockDLCStatus(BlockType type)
		{
			DlcType dlcType;
			GetBlockDlcType(type, out dlcType);
			if (dlcType == (DlcType)0)
			{
				return true;
			}
			return HasPurchasedDlc(dlcType);
		}

		public bool GetBlockDlcType(BlockType type, out DlcType dlcType)
		{
			return blocks.TryGetValue(type, out dlcType);
		}

		public List<uint> GetMissingDlcs(uint dlcDependencyMask)
		{
			List<uint> dlcTypesFromMask = GetDlcTypesFromMask(dlcDependencyMask);
			for (int num = dlcTypesFromMask.Count - 1; num >= 0; num--)
			{
				DlcType dlcType = (DlcType)dlcTypesFromMask[num];
				if (GetDlcStatus(dlcType) == DlcStatusType.Allowed)
				{
					dlcTypesFromMask.RemoveAt(num);
				}
			}
			return dlcTypesFromMask;
		}

		public void GetMissingDlcs(uint dlcDependencyMask, out List<uint> installedDlcTypes, out List<uint> missingDlcTypes)
		{
			installedDlcTypes = GetDlcTypesFromMask(dlcDependencyMask);
			missingDlcTypes = new List<uint>();
			for (int num = installedDlcTypes.Count - 1; num >= 0; num--)
			{
				uint num2 = installedDlcTypes[num];
				if (GetDlcStatus((DlcType)num2) != DlcStatusType.Allowed)
				{
					installedDlcTypes.RemoveAt(num);
					missingDlcTypes.Add(num2);
				}
			}
		}

		public bool GetDlcType(int prefabId, out DlcType dlcType)
		{
			return levelPrefabs.TryGetValue(prefabId, out dlcType);
		}

		public bool GetDlcType(StatMaster.Category category, int prefabId, out DlcType dlcType)
		{
			LevelPrefab prefab = PrefabMaster.GetPrefab(category, prefabId);
			if (prefab == null || !levelPrefabs.TryGetValue(prefab.ID, out dlcType))
			{
				dlcType = (DlcType)0;
				return false;
			}
			return true;
		}

		public bool GetDlcType(LevelSettings.LevelEnvironment env, out DlcType dlcType)
		{
			return environments.TryGetValue(env, out dlcType);
		}

		public bool AddEnv(LevelEnvironment environment)
		{
			DlcType value;
			if (!environments.TryGetValue(environment.env, out value))
			{
				value = GetDlcType(environment);
			}
			if (value == (DlcType)0)
			{
				return true;
			}
			if (!environments.ContainsKey(environment.env))
			{
				environments.Add(environment.env, value);
			}
			return TestDlcTypes(Convert(GetDlcTypesFromMask((uint)value)), new List<DlcStatus>());
		}

		public bool AddBlock(BlockPrefabContainer c)
		{
			DlcType value;
			if (!blocks.TryGetValue(c.Info.Type, out value))
			{
				value = GetDlcType(c);
			}
			if (value == (DlcType)0)
			{
				return true;
			}
			if (!blocks.ContainsKey(c.Info.Type))
			{
				blocks.Add(c.Info.Type, value);
			}
			return TestDlcTypes(Convert(GetDlcTypesFromMask((uint)value)), new List<DlcStatus>());
		}

		public bool AddLevelPrefab(LevelPrefab prefab)
		{
			DlcType value;
			if (!levelPrefabs.TryGetValue(prefab.ID, out value))
			{
				value = GetDlcType(prefab);
			}
			if (value == (DlcType)0)
			{
				return true;
			}
			if (!levelPrefabs.ContainsKey(prefab.ID))
			{
				levelPrefabs.Add(prefab.ID, value);
			}
			return TestDlcTypes(Convert(GetDlcTypesFromMask((uint)value)), new List<DlcStatus>());
		}

		public static List<DlcType> Convert(List<uint> dlcList)
		{
			List<DlcType> list = new List<DlcType>();
			for (int i = 0; i < dlcList.Count; i++)
			{
				list.Add((DlcType)dlcList[i]);
			}
			return list;
		}

		public bool GetMachineInfoDlc(MachineInfo machineInfo, out List<DlcType> dlcTypes)
		{
			dlcTypes = new List<DlcType>();
			for (int i = 0; i < machineInfo.Blocks.Count; i++)
			{
				BlockInfo blockInfo = machineInfo.Blocks[i];
				DlcType dlcType;
				if (GetBlockDlcType(blockInfo.ID, out dlcType) && dlcType != 0 && !dlcTypes.Contains(dlcType))
				{
					dlcTypes.Add(dlcType);
				}
			}
			return dlcTypes.Count > 0;
		}

		public bool GetMachineBlockDlc(List<BlockBehaviour> blocks, out List<DlcType> dlcTypes)
		{
			dlcTypes = new List<DlcType>();
			for (int i = 0; i < blocks.Count; i++)
			{
				DlcType dlcType;
				if (GetBlockDlcType(blocks[i].Prefab.Type, out dlcType) && dlcType != 0 && !dlcTypes.Contains(dlcType))
				{
					dlcTypes.Add(dlcType);
				}
			}
			return dlcTypes.Count > 0;
		}

		public bool GetInfoDlcStatus(MachineInfo machineInfo, out List<DlcStatus> dlcIssues)
		{
			List<DlcType> dlcTypes;
			GetMachineInfoDlc(machineInfo, out dlcTypes);
			dlcIssues = new List<DlcStatus>();
			for (int i = 0; i < dlcTypes.Count; i++)
			{
				DlcType dlc = dlcTypes[i];
				if (dlcIssues.FindIndex((DlcStatus x) => x.type == dlc) == -1)
				{
					DlcStatusType dlcStatus = GetDlcStatus(dlc);
					if (!HasPurchasedDlc(dlc))
					{
						dlcIssues.Add(new DlcStatus
						{
							type = dlc,
							status = dlcStatus
						});
					}
				}
			}
			return dlcIssues.Count == 0;
		}

		public bool TestDlcTypes(List<DlcType> dlcTypes, List<DlcStatus> dlcIssues)
		{
			for (int i = 0; i < dlcTypes.Count; i++)
			{
				DlcType dlc = dlcTypes[i];
				if (dlcIssues.FindIndex((DlcStatus x) => x.type == dlc) == -1)
				{
					DlcStatusType dlcStatus = GetDlcStatus(dlc);
					if (dlcStatus != DlcStatusType.Allowed)
					{
						dlcIssues.Add(new DlcStatus
						{
							type = dlc,
							status = dlcStatus
						});
					}
				}
			}
			return dlcIssues.Count == 0;
		}

		public uint GetMaskFromDlcTypes(List<DlcType> dlcTypes)
		{
			uint num = 0u;
			for (int i = 0; i < dlcTypes.Count; i++)
			{
				num |= (uint)dlcTypes[i];
			}
			return num;
		}

		public uint GetMaskFromDlcTypes(List<uint> dlcTypes)
		{
			uint num = 0u;
			for (int i = 0; i < dlcTypes.Count; i++)
			{
				num |= dlcTypes[i];
			}
			return num;
		}

		public List<uint> GetLocalDlcTypes(bool ignoreServerSettings)
		{
			int length = Enum.GetValues(typeof(DlcType)).Length;
			List<uint> list = new List<uint>();
			for (int i = 0; i < length; i++)
			{
				DlcType dlcType = (DlcType)(1 << i);
				DlcStatusType dlcStatus = GetDlcStatus(dlcType);
				bool flag = StatMaster.hostDisabledDLC && !StatMaster.IsLevelEditorOnly && StatMaster.isMP && (StatMaster.isHeadless || StatMaster.isHosting || StatMaster.initializingHostEnvironment);
				if (dlcStatus == DlcStatusType.Allowed || (ignoreServerSettings && dlcStatus == DlcStatusType.DisabledOnServer && !flag))
				{
					list.Add((uint)dlcType);
				}
			}
			return list;
		}

		public List<uint> GetDlcTypesFromMask(uint dlcTypeMask)
		{
			int length = Enum.GetValues(typeof(DlcType)).Length;
			List<uint> list = new List<uint>();
			if (dlcTypeMask == 0)
			{
				return list;
			}
			for (int i = 0; i < length; i++)
			{
				DlcType dlcType = (DlcType)(1 << i);
				if ((long)((ulong)dlcTypeMask & (ulong)dlcType) > 0L)
				{
					list.Add((uint)dlcType);
				}
			}
			return list;
		}

		private List<DlcType> ExtractDlcTypes(DlcType mask)
		{
			if (mask > (DlcType)0)
			{
				List<uint> dlcTypesFromMask = GetDlcTypesFromMask((uint)mask);
				return Convert(dlcTypesFromMask);
			}
			return new List<DlcType>();
		}

		public List<DlcType> GetPrefabDlcTypes(int prefabId)
		{
			DlcType dlcType;
			if (!GetDlcType(prefabId, out dlcType))
			{
				return new List<DlcType>();
			}
			return ExtractDlcTypes(dlcType);
		}

		public bool GetPrefabDlcStatus(int prefabId, List<DlcStatus> dlcIssues)
		{
			List<DlcType> prefabDlcTypes = GetPrefabDlcTypes(prefabId);
			return TestDlcTypes(prefabDlcTypes, dlcIssues);
		}

		public DlcStatusType GetDlcStatus(DlcType dlcType)
		{
			bool flag = StatMaster.hostDisabledDLC && !StatMaster.IsLevelEditorOnly && StatMaster.isMP && (StatMaster.isHeadless || StatMaster.isHosting || StatMaster.initializingHostEnvironment);
			if (!HasPurchasedDlc(dlcType))
			{
				return DlcStatusType.MissingDlc;
			}
			if ((StatMaster.HasGameState && (NetworkScene.ServerSettings.dlcMask & (uint)dlcType) == 0) || flag)
			{
				return DlcStatusType.DisabledOnServer;
			}
			return DlcStatusType.Allowed;
		}

		public bool CheckEnv(LevelSettings.LevelEnvironment environment, List<DlcStatus> dlcIssues)
		{
			DlcType dlcType;
			if (!GetDlcType(environment, out dlcType))
			{
				return true;
			}
			return TestDlcTypes(Convert(GetDlcTypesFromMask((uint)dlcType)), dlcIssues);
		}

		private DlcType GetDlcType(object o)
		{
			Type type = o.GetType();
			FieldInfo field = type.GetField("dlcType", BindingFlags.Instance | BindingFlags.NonPublic);
			return (DlcType)(int)field.GetValue(o);
		}

		public bool HasPurchasedDlc(DlcType dlcType)
		{
			return dlcProvider.HasPurchasedDlc(dlcType);
		}

		public bool IsSupporter()
		{
			return dlcProvider.IsDlcIdInstalled("2959970");
		}

		public bool HasPurchasedDlcMask(uint dlcDependencyMask)
		{
			if (dlcDependencyMask == 0)
			{
				return true;
			}
			List<uint> dlcTypesFromMask = GetDlcTypesFromMask(dlcDependencyMask);
			foreach (uint item in dlcTypesFromMask)
			{
				if (!HasPurchasedDlc((DlcType)item))
				{
					return false;
				}
			}
			return true;
		}

		public object GetDlcPlatformID(uint dlcType)
		{
			return dlcProvider.PlatformID((DlcType)dlcType);
		}

		public string GetDlcName(DlcType dlcType)
		{
			return dlcProvider.Name(dlcType);
		}

		public Texture GetDlcTexture(DlcType dlcType)
		{
			return GetDlcIcon(dlcType).texture;
		}

		public Sprite GetDlcSprite(DlcType dlcType)
		{
			return GetDlcIcon(dlcType);
		}

		public Sprite GetDlcIcon(DlcType dlcType)
		{
			Sprite sprite = dlcProvider.Icon(dlcType);
			if (sprite == null)
			{
				return info.DlcNotFoundIcon;
			}
			return sprite;
		}

		public void OnUserSignin()
		{
			dlcProvider.OnUserSignin();
		}

		public void OpenDlcStore(DlcType dlcType)
		{
			dlcProvider.OpenDlcStore(dlcType);
		}

		public bool IsLevelAllowed(string s)
		{
			int result;
			if ((!s.ToLower().Contains("water") && (!int.TryParse(s, out result) || (ReferenceMaster.LevelToIsland(result) != Island.Water && ReferenceMaster.LevelToIsland(result) != Island.WaterSandbox))) || HasPurchasedDlc(DlcType.Water))
			{
				return true;
			}
			return false;
		}

		public static int GetLevelDLCType(int levelName)
		{
			if (levelName < 56)
			{
				return 0;
			}
			if (levelName < 71)
			{
				return 1;
			}
			return 0;
		}

		public static int GetLevelDLCTypeFromLevelIndex(int levelIndex)
		{
			return GetLevelDLCType(levelIndex + 1);
		}
	}
}
