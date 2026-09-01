using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DM;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.Workshop
{
	public class CustomUnitHandler
	{
		private static UnitBlueprint[] SortUnitsByWriteTime(UnitBlueprint[] units)
		{
			for (int i = 0; i < units.Length - 1; i++)
			{
				for (int j = 0; j < units.Length - 1; j++)
				{
					DateTime writeTime = units[j].GetWriteTime();
					DateTime writeTime2 = units[j + 1].GetWriteTime();
					if (writeTime.CompareTo(writeTime2) < 0)
					{
						UnitBlueprint unitBlueprint = units[j];
						units[j] = units[j + 1];
						units[j + 1] = unitBlueprint;
					}
				}
			}
			return units;
		}

		public static UnitBlueprint[] GetLastLoadedUnits(int max = 6)
		{
			List<UnitBlueprint> list = new List<UnitBlueprint>();
			UnitBlueprint[] units = ContentDatabase.Instance().GetUserUnitBlueprints().ToArray();
			units = SortUnitsByWriteTime(units);
			int num = Mathf.Min(units.Length, max);
			for (int i = 0; i < num; i++)
			{
				list.Add(units[i]);
			}
			return list.ToArray();
		}

		public static void GetLoadedUnitFromDisk(string fileName, Action<UnitBlueprint> doneCallback)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			try
			{
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
						fileIO.DeserializeWithBinaryFormatter<UnitBlueprint>(fileName, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(object deserializedObject, Exception exception)
						{
							if (deserializedObject != null)
							{
								doneCallback?.Invoke((UnitBlueprint)deserializedObject);
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
			catch (Exception ex)
			{
				Debug.LogError("Failed to Load Unit with exception " + ex.Message);
				doneCallback?.Invoke(null);
			}
		}

		public static void OverrideUnit(UnitBlueprint unitBlueprint, Action refreshDoneCallback = null)
		{
			SaveUnit(unitBlueprint, unitBlueprint.Entity.GUID, refreshDoneCallback);
		}

		public static void SaveUnit(UnitBlueprint unitBlueprint, DatabaseID id = default(DatabaseID), Action refreshDoneCallback = null)
		{
			try
			{
				if (id == default(DatabaseID))
				{
					unitBlueprint.Entity.GenerateNewID();
				}
				else
				{
					unitBlueprint.Entity.GUID = id;
				}
				ContentDatabase.Instance().AddUserUnitBlueprint(unitBlueprint);
				string text = unitBlueprint.Entity.GUID.ToString();
				string customUnitPath = UnitBlueprint.GetCustomUnitPath(text);
				string iconPath = CustomContentFilePaths.UnitDirectoryPath + "/" + text + "/icon.png";
				string contents = JsonUtility.ToJson(unitBlueprint.SerializedUnit(default(DatabaseID)), prettyPrint: true);
				ServiceLocator.GetService<FileIOWrapper>().WriteAllText(customUnitPath, contents, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate
				{
					SaveIcon(unitBlueprint.Entity.SpriteIcon.texture, iconPath, delegate
					{
						ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Unit, refreshDoneCallback);
					});
				});
			}
			catch (Exception ex)
			{
				Debug.LogError("Failed to Save Unit with exception " + ex.Message);
				refreshDoneCallback?.Invoke();
			}
		}

		private static void SaveIcon(Texture2D icon, string path, Action<Exception> doneCallBack)
		{
			try
			{
				byte[] bytes = icon.EncodeToPNG();
				ServiceLocator.GetService<FileIOWrapper>().WriteAllBytes(path, bytes, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
				{
					doneCallBack?.Invoke(exception);
				});
			}
			catch (Exception ex)
			{
				Debug.LogError("Failed to Save Icon with exception " + ex.Message);
				doneCallBack?.Invoke(null);
			}
		}
	}
}
