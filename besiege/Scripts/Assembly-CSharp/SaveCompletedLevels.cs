using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCompletedLevels : MonoBehaviour
{
	public const int MAX_LEVELS = 71;

	public const string saveFile = "CompletedLevels.txt";

	public static SaveCompletedLevels Instance;

	private BesiegeFileManager.FileLocation saveLocation;

	public static bool loadedProgress;

	private void Awake()
	{
		Instance = this;
		LoadGame();
		SceneManager.sceneLoaded += OnSceneLoad;
		loadedProgress = true;
		if (SteamManager.Initialized && OptionsMaster.BesiegeConfig.CloudSaving)
		{
			loadedProgress = false;
			ReferenceMaster.onRemoteFilesUpdated = (Action)Delegate.Combine(ReferenceMaster.onRemoteFilesUpdated, new Action(OnRemoteFilesUpdated));
		}
	}

	private void OnRemoteFilesUpdated()
	{
		ReferenceMaster.onRemoteFilesUpdated = (Action)Delegate.Remove(ReferenceMaster.onRemoteFilesUpdated, new Action(OnRemoteFilesUpdated));
		if (!SteamManager.Initialized || !OptionsMaster.BesiegeConfig.CloudSaving)
		{
			return;
		}
		WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
		if (instance != null)
		{
			if (BesiegeFileManager.Exists("CompletedLevels.txt", saveLocation))
			{
				loadedProgress = true;
				if (!instance.IsRemoteFile("CompletedLevels.txt"))
				{
					string fileName = Path.Combine(StaticSettings.DataPath, "CompletedLevels.txt");
					FileInfo fileInfo = new FileInfo(fileName);
					instance.WriteRemoteFileAsync(fileInfo, false);
				}
			}
			else if (instance.IsRemoteFile("CompletedLevels.txt"))
			{
				instance.ReadRemoteFileAsync("CompletedLevels.txt", delegate(string p, bool s, byte[] c)
				{
					OnSyncProgress(s, c, false);
				});
			}
			else
			{
				loadedProgress = true;
			}
		}
		else
		{
			loadedProgress = true;
		}
	}

	private void OnSyncProgress(bool success, byte[] content, bool syncOnChange)
	{
		if (success)
		{
			SyncProgress(content, syncOnChange);
		}
		loadedProgress = true;
	}

	public void SyncProgress(byte[] content, bool syncOnChange = true)
	{
		string input = Encoding.UTF8.GetString(content);
		string text = Regex.Replace(input, "\\s+", string.Empty);
		string[] array = text.Split("|"[0]);
		bool flag = false;
		for (int i = 0; i < array.Length; i++)
		{
			int num = int.Parse(array[i]);
			if (i < LEVELLORD.levelsComplete.Length && num > LEVELLORD.levelsComplete[i])
			{
				LEVELLORD.levelsComplete[i] = num;
				flag = true;
			}
		}
		if (flag)
		{
			SaveGame(syncOnChange);
		}
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		LoadGame();
	}

	public void SaveGame(bool syncOnChange = true)
	{
		int[] levelsComplete = LEVELLORD.levelsComplete;
		byte[] array = new byte[levelsComplete.Length * 2 - 1];
		int num = 0;
		for (int i = 0; i < levelsComplete.Length; i++)
		{
			array[num++] = (byte)(levelsComplete[i] + 48);
			if (i < levelsComplete.Length - 1)
			{
				array[num++] = 124;
			}
		}
		if (!BesiegeFileManager.Save("CompletedLevels.txt", saveLocation, array))
		{
			Debug.Log("Couldn't save progress to 'CompletedLevels.txt'!");
		}
		if (syncOnChange && SteamManager.Initialized)
		{
			WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
			if (instance != null)
			{
				string fileName = Path.Combine(StaticSettings.DataPath, "CompletedLevels.txt");
				FileInfo fileInfo = new FileInfo(fileName);
				instance.WriteRemoteFileAsync(fileInfo, false);
			}
		}
	}

	public void LoadGame()
	{
		byte[] data;
		if (BesiegeFileManager.Load("CompletedLevels.txt", saveLocation, out data))
		{
			string liney = Encoding.UTF8.GetString(data);
			ReadInts(liney);
		}
	}

	private void ReadInts(string liney)
	{
		string text = Regex.Replace(liney, "\\s+", string.Empty);
		string[] array = text.Split("|"[0]);
		int[] array2 = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = int.Parse(array[i]);
		}
		int num = Mathf.Max(SceneManager.sceneCount, 71);
		if (array2.Length < num)
		{
			List<int> list = array2.ToList();
			bool flag = false;
			for (int j = 0; j < num - array2.Length; j++)
			{
				flag = true;
				list.Add(0);
			}
			array2 = list.ToArray();
			LEVELLORD.levelsComplete = array2;
			if (flag)
			{
				SaveGame();
			}
		}
		else
		{
			LEVELLORD.levelsComplete = array2;
		}
	}
}
