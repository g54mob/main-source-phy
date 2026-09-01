using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using DM;
using Landfall.TABS.Budget;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.UnitPlacement;
using ModIO;
using ModIO.UI;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.Workshop
{
	public class CampaignHandler : InstancedHandler<CampaignHandler>
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

			public static Func<UnitBlueprint, bool> _003C_003E9__23_0;

			public static Func<AllowedUnitWrapper, bool> _003C_003E9__24_0;

			public static Action _003C_003E9__27_0;

			internal bool _003CUpgradeUnitWhitelist_003Eb__23_0(UnitBlueprint p)
			{
				if (p != null && p.Entity != null)
				{
					_ = p.Entity.GUID;
					return true;
				}
				return false;
			}

			internal bool _003CUpgradeUnitWhitelist_003Eb__24_0(AllowedUnitWrapper p)
			{
				if (p != null)
				{
					_ = p.ID;
					return true;
				}
				return false;
			}

			internal void _003CApplySceneData_003Eb__27_0()
			{
				Debug.LogError("On done with placing units, time to set budget!");
			}
		}

		public static TABSCampaignLevelAsset LastLoadedLevel { get; private set; }

		public static void ResetLoadedLevel()
		{
			LastLoadedLevel = null;
		}

		public static void SaveTempCampaign(CampaignSequence sequence, Action<string> doneCallback)
		{
			string tempFolderPath = CustomContentFilePaths.GetTempFolderPath();
			string fileName = sequence.ID.ToString();
			string fileFolder = Path.Combine(tempFolderPath, fileName);
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			fileIO.DirectoryExists(fileFolder, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (exists)
				{
					SaveTempCampaignOnDirectoryExists(sequence, fileFolder, fileName, fileIO, doneCallback);
				}
				else
				{
					fileIO.CreateDirectory(fileFolder, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
					SaveTempCampaignOnDirectoryExists(sequence, fileFolder, fileName, fileIO, doneCallback);
				}
			});
		}

		private static void SaveTempCampaignOnDirectoryExists(CampaignSequence sequence, string fileFolder, string fileName, FileIOWrapper fileIO, Action<string> doneCallback)
		{
			string fullPath = Path.Combine(fileFolder, fileName + CustomContentFilePaths.FileEndingCampaign);
			Debug.LogFormat("Saving Temp Campaign: {0}", fullPath);
			fileIO.SerializeWithBinaryFormatter(fullPath, sequence, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
			{
				if (exception == null)
				{
					doneCallback?.Invoke(fullPath);
				}
				else
				{
					Debug.LogFormat("Failed to save: {0}\n{1}", fullPath, exception);
					doneCallback?.Invoke(string.Empty);
				}
			});
		}

		public static void SaveTempBattle(CampaignLevel level, string fileName, Action<string> doneCallback)
		{
			string tempFolderPath = CustomContentFilePaths.GetTempFolderPath();
			string fileFolder = Path.Combine(tempFolderPath, fileName);
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			fileIO.DirectoryExists(fileFolder, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (exists)
				{
					SaveTempBattleOnDirectoryExists(level, fileName, fileFolder, fileIO, doneCallback);
				}
				else
				{
					fileIO.CreateDirectory(fileFolder, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
					SaveTempBattleOnDirectoryExists(level, fileName, fileFolder, fileIO, doneCallback);
				}
			});
		}

		private static void SaveTempBattleOnDirectoryExists(CampaignLevel level, string fileName, string fileFolder, FileIOWrapper fileIO, Action<string> doneCallback)
		{
			string fullFilePath = Path.Combine(fileFolder, fileName + CustomContentFilePaths.FileEndingBattle);
			Debug.LogFormat("Saving Temp Battle: {0}", fullFilePath);
			fileIO.SerializeWithBinaryFormatter(fullFilePath, level, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
			{
				if (exception == null)
				{
					doneCallback?.Invoke(fullFilePath);
				}
				else
				{
					Debug.LogFormat("Failed to save file: {0}\n{1}", fullFilePath, exception);
					doneCallback?.Invoke(string.Empty);
				}
			});
		}

		public static void SaveCampaign(CampaignSequence sequence, TABSCampaignAsset alreadySelectedCampaign = null, Action<bool> doneCallback = null)
		{
			CustomContentFilePaths.GetTempFolderPath();
			string filePathCampaign = CustomContentFilePaths.FilePathCampaign;
			string fileEndingCampaign = CustomContentFilePaths.FileEndingCampaign;
			string text = sequence.ID.ToString();
			FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
			string text2 = Path.Combine(filePathCampaign, text);
			service.CreateDirectory(filePathCampaign, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
			service.CreateDirectory(text2, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
			BattleCreatorScreenshotHandler.CaptureScreenshot(Path.Combine(text2, "Picture.png"), campaign: true);
			string fullPath = Path.Combine(text2, text + fileEndingCampaign);
			Debug.Log("Saving Campaign: " + fullPath);
			service.SerializeWithBinaryFormatter(fullPath, sequence, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
			{
				if (exception == null)
				{
					ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Campaign, delegate
					{
						doneCallback?.Invoke(obj: true);
					});
				}
				else
				{
					Debug.LogFormat("Failed to save campaign: {0}\n{1}", fullPath, exception);
					doneCallback?.Invoke(obj: false);
				}
			});
		}

		public static TABSCampaignLevelAsset CreateCampaignLevelAsset(string fileName, SaveCampaignSettings settings, DatabaseID newId = default(DatabaseID))
		{
			TABSCampaignLevelAsset tABSCampaignLevelAsset = ScriptableObject.CreateInstance<TABSCampaignLevelAsset>();
			tABSCampaignLevelAsset.InitEntity();
			if (newId != default(DatabaseID))
			{
				tABSCampaignLevelAsset.Entity.GUID = newId;
			}
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (settings.SaveRed)
			{
				tABSCampaignLevelAsset.RedUnits = currentGameMode.TeamLayouts.GetTeamLayout(Team.Red).ToArray();
			}
			else
			{
				tABSCampaignLevelAsset.RedUnits = new TABSCampaignLevelAsset.TABSLayoutUnit[0];
			}
			if (settings.SaveBlue)
			{
				tABSCampaignLevelAsset.BlueUnits = currentGameMode.TeamLayouts.GetTeamLayout(Team.Blue).ToArray();
			}
			else
			{
				tABSCampaignLevelAsset.BlueUnits = new TABSCampaignLevelAsset.TABSLayoutUnit[0];
			}
			tABSCampaignLevelAsset.m_budget = settings.Budget;
			tABSCampaignLevelAsset.Entity.Name = fileName;
			tABSCampaignLevelAsset.CampaignInfo = new CampaignInfo
			{
				Description = settings.BattleDescription
			};
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			tABSCampaignLevelAsset.AllowedFactions = contentDatabase.GetFactionsByIds(settings.AllowedFactions).ToArray();
			tABSCampaignLevelAsset.AllowedUnits = new UnitBlueprint[settings.AllowedUnits.Length];
			tABSCampaignLevelAsset.MapAsset = contentDatabase.GetMapAsset(settings.SelectedMap);
			for (int i = 0; i < tABSCampaignLevelAsset.AllowedUnits.Length; i++)
			{
				tABSCampaignLevelAsset.AllowedUnits[i] = contentDatabase.GetUnitBlueprint(settings.AllowedUnits[i].ID);
			}
			tABSCampaignLevelAsset.SceneSettings = settings.SceneSettings;
			tABSCampaignLevelAsset.CustomMap = settings.CustomMap;
			return tABSCampaignLevelAsset;
		}

		public static TABSCampaignLevelAsset EditCampaignLevelAsset(string fileName, SaveCampaignSettings settings, TABSCampaignLevelAsset OG)
		{
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (settings.SaveRed)
			{
				OG.RedUnits = currentGameMode.TeamLayouts.GetTeamLayout(Team.Red).ToArray();
			}
			if (settings.SaveBlue)
			{
				OG.BlueUnits = currentGameMode.TeamLayouts.GetTeamLayout(Team.Blue).ToArray();
			}
			OG.m_budget = settings.Budget;
			OG.Entity.Name = fileName;
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			OG.AllowedFactions = contentDatabase.GetFactionsByIds(settings.AllowedFactions).ToArray();
			OG.AllowedUnits = new UnitBlueprint[settings.AllowedUnits.Length];
			OG.MapAsset = contentDatabase.GetMapAsset(settings.SelectedMap);
			for (int i = 0; i < OG.AllowedUnits.Length; i++)
			{
				OG.AllowedUnits[i] = contentDatabase.GetUnitBlueprint(settings.AllowedUnits[i].ID);
			}
			OG.SceneSettings = settings.SceneSettings;
			return OG;
		}

		public static void OverwriteCampaign(CampaignSequence sequence, string filePath, Action<bool> doneCallback)
		{
			Debug.Log("Saving Campaign: " + filePath);
			ServiceLocator.GetService<FileIOWrapper>().SerializeWithBinaryFormatter(filePath, sequence, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
			{
				if (exception == null)
				{
					ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Campaign, delegate
					{
						doneCallback?.Invoke(obj: true);
					});
				}
				else
				{
					Debug.LogFormat("Failed to save campaign: {0}\n{1}", filePath, exception);
					doneCallback?.Invoke(obj: false);
				}
			});
		}

		public static void OverwriteCampaign(TABSCampaignAsset campaignAsset, Action<bool> doneCallback)
		{
			CampaignSequence objectToSerialize = campaignAsset.SerializeCampaign();
			ContentDatabase.Instance().AddUserCampaign(campaignAsset);
			string path = campaignAsset.FilePath;
			Debug.LogFormat("Saving Campaign: {0}", path);
			ServiceLocator.GetService<FileIOWrapper>().SerializeWithBinaryFormatter(path, objectToSerialize, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
			{
				if (exception == null)
				{
					ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Campaign, delegate
					{
						doneCallback?.Invoke(obj: true);
					});
				}
				else
				{
					Debug.LogFormat("Failed to save campaign: {0}\n{1}", path, exception);
					doneCallback?.Invoke(obj: false);
				}
			});
		}

		public static void OverwriteLayout(TABSCampaignLevelAsset campaignLevelAsset, Action<bool> doneCallback)
		{
			CampaignLevel objectToSerialize = campaignLevelAsset.SerializeCampaignLevel();
			ContentDatabase.Instance().AddUserCampaignLevel(campaignLevelAsset);
			Debug.Log("Saving layout: " + campaignLevelAsset.FilePath);
			FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
			string path = campaignLevelAsset.FilePath;
			service.SerializeWithBinaryFormatter(path, objectToSerialize, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
			{
				if (exception == null)
				{
					ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Battle, delegate
					{
						doneCallback?.Invoke(obj: true);
					});
				}
				else
				{
					Debug.LogFormat("Failed to save layout: {0}\n{1}", path, exception);
					doneCallback?.Invoke(obj: false);
				}
			});
		}

		public static void SaveLayout(string fileName, SaveCampaignSettings settings, bool temp, DatabaseID useThisGuid = default(DatabaseID), TABSCampaignLevelAsset loadedAsset = null, Action<bool> doneCallback = null)
		{
			string tempFolderPath = CustomContentFilePaths.GetTempFolderPath();
			string text = CustomContentFilePaths.FilePathLayout;
			if (temp)
			{
				text = tempFolderPath;
			}
			string fileEndingBattle = CustomContentFilePaths.FileEndingBattle;
			string fileEndingLayout = CustomContentFilePaths.FileEndingLayout;
			DatabaseID newId = ((loadedAsset == null) ? useThisGuid : loadedAsset.Entity.GUID);
			TABSCampaignLevelAsset campaignLevel = CreateCampaignLevelAsset(fileName, settings, newId);
			string text2 = campaignLevel.Entity.GUID.ToString();
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			string text3 = Path.Combine(text, text2);
			fileIO.CreateDirectory(text, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
			if (!temp)
			{
				fileIO.CreateDirectory(text3, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
			}
			if (temp)
			{
				text3 = tempFolderPath;
			}
			string text4 = ((settings.LevelType == LevelType.Battle) ? fileEndingBattle : fileEndingLayout);
			BattleCreatorScreenshotHandler.CaptureScreenshot(Path.Combine(text3, "Picture.png"), campaign: false);
			string fullFilePath = Path.Combine(text3, text2 + text4);
			SaveLayoutGetLevelGUID(fullFilePath, campaignLevel, useThisGuid, temp, delegate(DatabaseID? id)
			{
				if (id.HasValue)
				{
					campaignLevel.Entity.GUID = id.Value;
				}
				CampaignLevel level = campaignLevel.SerializeCampaignLevel();
				ContentDatabase contentDatabase = ContentDatabase.Instance();
				contentDatabase.AddUserCampaignLevel(campaignLevel);
				Debug.Log("Saving to: " + fullFilePath);
				fileIO.SerializeWithBinaryFormatter(fullFilePath, level, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
				{
					if (exception == null)
					{
						LastLoadedLevel = TABSCampaignLevelAsset.DeserializeCampaignLevel(level, fullFilePath, contentDatabase.LandfallContentDatabase, contentDatabase.UserContentDatabase);
						ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Battle, delegate
						{
							ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Campaign, delegate
							{
								doneCallback?.Invoke(obj: true);
							});
						});
					}
					else
					{
						Debug.LogFormat("Failed to save to: {0}\n{1}", fullFilePath, exception);
						doneCallback?.Invoke(obj: false);
					}
				});
			});
		}

		private static void SaveLayoutGetLevelGUID(string path, TABSCampaignLevelAsset campaignLevel, DatabaseID useThisGuid, bool temp, Action<DatabaseID?> doneCallback)
		{
			if (useThisGuid != default(DatabaseID))
			{
				doneCallback?.Invoke(useThisGuid);
				return;
			}
			if (temp)
			{
				doneCallback?.Invoke(null);
				return;
			}
			ServiceLocator.GetService<FileIOWrapper>().FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool success)
			{
				if (!success)
				{
					doneCallback?.Invoke(null);
				}
				else
				{
					GetLoadedLayoutFromDisk(path, delegate(CampaignLevel level)
					{
						if (level == null)
						{
							doneCallback?.Invoke(null);
						}
						else
						{
							doneCallback?.Invoke(level.ID);
						}
					});
				}
			});
		}

		public static void GetLoadedCampaignFromDisk(string campaignFullPath, Action<CampaignSequence> doneCallback)
		{
			GetLoadedCampaignFromDisk(new FileInfo(campaignFullPath), doneCallback);
		}

		public static void GetLoadedCampaignFromDisk(FileInfo file, Action<CampaignSequence> doneCallback)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			string fileName = file.FullName;
			fileIO.FileExists(fileName, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					Debug.LogError("Could not find file: " + fileName);
					doneCallback?.Invoke(null);
				}
				else if (string.Equals(Path.GetExtension(fileName), ".png", StringComparison.InvariantCultureIgnoreCase))
				{
					doneCallback?.Invoke(null);
				}
				else
				{
					fileIO.DeserializeWithBinaryFormatter<CampaignSequence>(fileName, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(object deserializedObject, Exception exception)
					{
						if (deserializedObject != null)
						{
							doneCallback?.Invoke((CampaignSequence)deserializedObject);
						}
						else
						{
							Debug.LogErrorFormat("Failed to load: {0}\n{1}", fileName, exception);
							doneCallback?.Invoke(null);
						}
					});
				}
			});
		}

		public static void GetLoadedLayoutFromDisk(string fileName, Action<CampaignLevel> doneCallback)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			fileIO.FileExists(fileName, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					Debug.LogErrorFormat("Could not find file: {0}", fileName);
					doneCallback?.Invoke(null);
				}
				else if (string.Equals(Path.GetExtension(fileName), ".png", StringComparison.InvariantCultureIgnoreCase))
				{
					doneCallback?.Invoke(null);
				}
				else
				{
					fileIO.DeserializeWithBinaryFormatter<CampaignLevel>(fileName, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(object deserializedObject, Exception exception)
					{
						if (deserializedObject != null)
						{
							CampaignLevel obj = (CampaignLevel)deserializedObject;
							doneCallback?.Invoke(obj);
						}
						else
						{
							Debug.LogErrorFormat("Failed to load: {0}\n{1}", fileName, exception);
							doneCallback?.Invoke(null);
						}
					});
				}
			});
		}

		public static void LoadLayoutFromDisk(string filePath, Action<bool> doneCallback)
		{
			GetLoadedLayoutFromDisk(filePath, delegate(CampaignLevel level)
			{
				if (level == null)
				{
					Debug.LogErrorFormat("Failed to load level: {0}", filePath);
					doneCallback?.Invoke(obj: false);
				}
				else
				{
					LoadLevel(TABSCampaignLevelAsset.DeserializeCampaignLevel(level, filePath, ContentDatabase.Instance().LandfallContentDatabase, ContentDatabase.Instance().UserContentDatabase));
					Debug.Log("--- Loaded Level: " + filePath + " ---");
					doneCallback?.Invoke(obj: true);
				}
			});
		}

		public static bool CheckVersion(CampaignLevel lvl, string file)
		{
			double versionNumber = lvl.VersionNumber;
			if (versionNumber != CustomContentData.Version_Number)
			{
				return UpdateCampaignToNextVersion(versionNumber, lvl, file);
			}
			return true;
		}

		private static bool UpdateCampaignToNextVersion(double version, CampaignLevel lvl, string file)
		{
			int indexOfVersion = CustomContentData.GetIndexOfVersion(version);
			int num = indexOfVersion + 1;
			int indexOfVersion2 = CustomContentData.GetIndexOfVersion(num);
			if (indexOfVersion < 2)
			{
				for (int i = 0; i < lvl.RedUnits.Units.Length; i++)
				{
					lvl.RedUnits.Units[i].Rotation = 90f;
				}
				for (int j = 0; j < lvl.BlueUnits.Units.Length; j++)
				{
					lvl.BlueUnits.Units[j].Rotation = 270f;
				}
			}
			if (indexOfVersion < 3)
			{
				UpgradeUnitWhitelist(lvl.AllowedUnits, out lvl.AllowedFactions);
			}
			if (num == CustomContentData.GetCurrentVersionIndex())
			{
				return true;
			}
			return UpdateCampaignToNextVersion(indexOfVersion2, lvl, file);
		}

		public static bool UpgradeUnitWhitelist(UnitBlueprint[] allowedUnits, out DatabaseID[] AllowedFactions)
		{
			int num = allowedUnits.Length;
			allowedUnits = allowedUnits.Where(delegate(UnitBlueprint p)
			{
				if (p != null && p.Entity != null)
				{
					_ = p.Entity.GUID;
					return true;
				}
				return false;
			}).ToArray();
			AllowedUnitWrapper[] array = new AllowedUnitWrapper[allowedUnits.Length];
			for (int num2 = 0; num2 < allowedUnits.Length; num2++)
			{
				array[num2] = new AllowedUnitWrapper();
				array[num2].ID = allowedUnits[num2].Entity.GUID;
			}
			UpgradeUnitWhitelist(array, out AllowedFactions);
			if (num != allowedUnits.Length)
			{
				return false;
			}
			return true;
		}

		public static void UpgradeUnitWhitelist(AllowedUnitWrapper[] allowedUnits, out DatabaseID[] AllowedFactions)
		{
			allowedUnits = allowedUnits.Where(delegate(AllowedUnitWrapper p)
			{
				if (p != null)
				{
					_ = p.ID;
					return true;
				}
				return false;
			}).ToArray();
			List<Faction> list = new List<Faction>();
			for (int num = 0; num < allowedUnits.Length; num++)
			{
				Faction[] array = ContentDatabase.Instance().GetFactionsByUnit(allowedUnits[num].ID).ToArray();
				for (int num2 = 0; num2 < array.Length; num2++)
				{
					if (!list.Contains(array[num2]))
					{
						list.Add(array[num2]);
					}
				}
			}
			DatabaseID[] array2 = new DatabaseID[list.Count];
			for (int num3 = 0; num3 < list.Count; num3++)
			{
				array2[num3] = list[num3].Entity.GUID;
			}
			AllowedFactions = array2;
		}

		private static bool DoesUnitsInclude(AllowedUnitWrapper[] allowedUnits, DatabaseID unit)
		{
			for (int i = 0; i < allowedUnits.Length; i++)
			{
				if (allowedUnits[i].ID == unit)
				{
					return true;
				}
			}
			return false;
		}

		public static void LoadLevel(TABSCampaignLevelAsset level, bool campaign = false)
		{
			Debug.Log($"Loading Level: {level}");
			if (CampaignPlayerDataHolder.CurrentGameModeState != GameModeState.Campaign && level.MapAsset.MapName != TABSSceneManager.GetCurrentMap())
			{
				LastLoadedLevel = level;
				TABSSceneManager.LoadMap(level.MapAsset);
				return;
			}
			ApplySceneData(level, campaign);
			GameStateManager service = ServiceLocator.GetService<GameStateManager>();
			service.EnterNoneState();
			service.EnterPlacementState();
			LastLoadedLevel = level;
		}

		public static void ApplySceneData(TABSCampaignLevelAsset level, bool campaign)
		{
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			Debug.Log("Current Game Mode: " + currentGameMode.GetType().Name);
			TeamLayouts teamLayouts = currentGameMode.TeamLayouts;
			_ = currentGameMode.Brush;
			BattleBudget battleBudget = currentGameMode.BattleBudget;
			Debug.Log($"Level: {level}");
			battleBudget.SetBudget(level.m_budget);
			battleBudget.ResetBudgetAll();
			teamLayouts.ClearTeamLayout(Team.Red);
			teamLayouts.ClearTeamLayout(Team.Blue);
			TABSCampaignLevelAsset.TABSLayoutUnit[] redUnits = level.RedUnits;
			foreach (TABSCampaignLevelAsset.TABSLayoutUnit obj in redUnits)
			{
				obj.CampaignUnit = true;
				obj.DeserializeRuntimeReference();
			}
			redUnits = level.BlueUnits;
			foreach (TABSCampaignLevelAsset.TABSLayoutUnit obj2 in redUnits)
			{
				obj2.CampaignUnit = true;
				obj2.DeserializeRuntimeReference();
			}
			teamLayouts.SetTeamLayout(Team.Red, level.RedUnits);
			teamLayouts.SetTeamLayout(Team.Blue, level.BlueUnits);
			if (level.RedUnits.Length != 0 && campaign && _003C_003Ec._003C_003E9__27_0 == null)
			{
				_003C_003Ec._003C_003E9__27_0 = delegate
				{
					Debug.LogError("On done with placing units, time to set budget!");
				};
			}
			_ = level.BlueUnits.LongLength;
			SceneSettings.SerializedSettings = level.SceneSettings;
		}

		public static void GetSavedLevels(LevelType type, Action<FileInfo[]> doneCallback)
		{
			string rootPath = CustomContentFilePaths.FilePathLayout;
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			fileIO.DirectoryExists(rootPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					doneCallback?.Invoke(new FileInfo[0]);
				}
				else
				{
					GetSavedLevelsOnDirectoryExists(type, rootPath, fileIO, doneCallback);
				}
			});
		}

		private static void GetSavedLevelsOnDirectoryExists(LevelType type, string rootPath, FileIOWrapper fileIO, Action<FileInfo[]> doneCallback)
		{
			fileIO.GetDirectories(rootPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] directories, Exception exception)
			{
				if (directories == null || directories.Length == 0)
				{
					doneCallback?.Invoke(new FileInfo[0]);
				}
				else
				{
					GetSavedLevelsOnFoundSubFolders(type, fileIO, directories, doneCallback);
				}
			});
		}

		private static void GetSavedLevelsOnFoundSubFolders(LevelType type, FileIOWrapper fileIO, string[] directories, Action<FileInfo[]> doneCallback)
		{
			List<FileInfo> levelPaths = new List<FileInfo>();
			string fileEnding = CustomContentFilePaths.GetFileExtension(type);
			int num = directories.Length;
			AsyncCounter counter = new AsyncCounter(num);
			for (int i = 0; i < num; i++)
			{
				string path = directories[i];
				fileIO.GetFiles(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] files, Exception exception)
				{
					if (files != null && files.Length != 0)
					{
						int j = 0;
						for (int num2 = files.Length; j < num2; j++)
						{
							string text = files[j];
							if (!string.IsNullOrEmpty(text) && Path.GetExtension(text) == fileEnding)
							{
								levelPaths.Add(new FileInfo(text));
							}
						}
					}
					if (counter.OnAsyncDone())
					{
						doneCallback?.Invoke(levelPaths.ToArray());
					}
				});
			}
		}

		public static void GetBattleSprite(TABSCampaignLevelAsset battle, Action<Sprite> onSuccess)
		{
			if (battle == null)
			{
				return;
			}
			string path = battle.FilePath;
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (exists)
				{
					string directoryName = Path.GetDirectoryName(battle.FilePath);
					path = Path.Combine(directoryName, "Picture.png");
				}
				if (battle.IsCustomCampaignLevel && !string.IsNullOrEmpty(path))
				{
					fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool pictureExists)
					{
						if (pictureExists)
						{
							CacheUtil.GetSpriteIconAsync(null, path, onSuccess);
						}
						else if (battle.IsModIOLevel)
						{
							CreateModSprite(battle.ModProfile, onSuccess);
						}
					});
				}
				else if (battle.IsModIOLevel)
				{
					CreateModSprite(battle.ModProfile, onSuccess);
				}
			});
		}

		public static Sprite GetBattleSprite(TABSCampaignLevelAsset battle)
		{
			if (battle == null)
			{
				return null;
			}
			string text = "";
			if (File.Exists(battle.FilePath))
			{
				text = Path.GetDirectoryName(battle.FilePath) + "/Picture.png";
			}
			if (battle.IsCustomCampaignLevel && !string.IsNullOrEmpty(text) && File.Exists(text))
			{
				return CacheUtil.GetSpriteIcon(null, text);
			}
			if (battle.IsModIOLevel)
			{
				return CreateModSprite(battle.ModProfile);
			}
			return null;
		}

		public static void GetCampaignSprite(TABSCampaignAsset campaign, Action<Sprite> onSuccess)
		{
			if (campaign == null)
			{
				return;
			}
			string path = campaign.FilePath;
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (exists)
				{
					string directoryName = Path.GetDirectoryName(path);
					path = Path.Combine(directoryName, "Picture.png");
				}
				if (campaign.IsCustomCampaign && !string.IsNullOrEmpty(path))
				{
					fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool campaignExists)
					{
						if (campaignExists)
						{
							CacheUtil.GetSpriteIconAsync(null, path, delegate(Sprite sprite)
							{
								onSuccess(sprite);
							});
						}
						else if (campaign.IsModCampaign)
						{
							CreateModSprite(campaign.ModProfile, onSuccess);
						}
					});
				}
				else if (campaign.IsModCampaign)
				{
					CreateModSprite(campaign.ModProfile, onSuccess);
				}
			});
		}

		public static Sprite GetCampaignSprite(TABSCampaignAsset campaign)
		{
			if (campaign == null)
			{
				return null;
			}
			string text = "";
			if (File.Exists(campaign.FilePath))
			{
				text = Path.GetDirectoryName(campaign.FilePath) + "/Picture.png";
			}
			if (campaign.IsCustomCampaign && !string.IsNullOrEmpty(text) && File.Exists(text))
			{
				return CacheUtil.GetSpriteIcon(null, text);
			}
			if (campaign.IsModCampaign)
			{
				return CreateModSprite(campaign.ModProfile);
			}
			return null;
		}

		private static void CreateModSprite(ModProfile modProfile, Action<Sprite> onSuccess)
		{
			if (modProfile == null)
			{
				onSuccess?.Invoke(null);
				return;
			}
			ImageRequestManager.instance.RequestModLogo(modProfile.id, modProfile.logoLocator, LogoSize.Thumbnail_640x360, delegate(Texture2D tex)
			{
				Sprite obj = UIUtilities.CreateSpriteFromTexture(tex);
				onSuccess?.Invoke(obj);
			}, delegate(Texture2D fallbackTex)
			{
				Sprite obj = UIUtilities.CreateSpriteFromTexture(fallbackTex);
				onSuccess?.Invoke(obj);
			}, WebRequestError.LogAsWarning);
		}

		private static Sprite CreateModSprite(ModProfile modProfile)
		{
			DateTime now = DateTime.Now;
			Sprite returnSprite = null;
			ImageRequestManager.instance.RequestModLogo(modProfile.id, modProfile.logoLocator, LogoSize.Thumbnail_640x360, delegate(Texture2D tex)
			{
				returnSprite = UIUtilities.CreateSpriteFromTexture(tex);
			}, delegate(Texture2D fallbackTex)
			{
				returnSprite = UIUtilities.CreateSpriteFromTexture(fallbackTex);
			}, WebRequestError.LogAsWarning);
			while (returnSprite == null || (DateTime.Now - now).TotalSeconds > 10.0)
			{
			}
			return returnSprite;
		}
	}
}
