using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BesiegeDlc;
using UnityEngine;

internal class LevelObjectiveFileManager : SingleInstance<LevelObjectiveFileManager>
{
	public const string saveFile = "LevelObjectives.txt";

	private BesiegeFileManager.FileLocation saveLocation;

	public static bool hasLoadedProgress = false;

	private static Dictionary<int, int> storedObjectives = new Dictionary<int, int>();

	public static bool loadedProgress = false;

	private static CompleteAllSecondaryObjectives completeAllSecondaryObjectives;

	private static CompleteAllSecondaryObjectivesSS completeAllSecondaryObjectivesSS;

	internal static readonly string key = "54389fdshjfdshka";

	internal static readonly string iv = "231dsa2134rd56hf";

	public override string Name
	{
		get
		{
			return "LevelObjectiveFileManager";
		}
	}

	private void Awake()
	{
		loadedProgress = true;
		completeAllSecondaryObjectives = UnityEngine.Object.FindObjectOfType<CompleteAllSecondaryObjectives>();
		completeAllSecondaryObjectivesSS = UnityEngine.Object.FindObjectOfType<CompleteAllSecondaryObjectivesSS>();
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
		WorkshopManager workshopManager = SingleInstance<WorkshopManager>.Instance;
		if (workshopManager != null)
		{
			if (BesiegeFileManager.Exists("LevelObjectives.txt", saveLocation))
			{
				loadedProgress = true;
				if (!workshopManager.IsRemoteFile("LevelObjectives.txt"))
				{
					string fileName = Path.Combine(StaticSettings.DataPath, "LevelObjectives.txt");
					FileInfo fileInfo = new FileInfo(fileName);
					workshopManager.WriteRemoteFileAsync(fileInfo, false);
				}
			}
			else if (workshopManager.IsRemoteFile("LevelObjectives.txt"))
			{
				workshopManager.ReadRemoteFileAsync("LevelObjectives.txt", delegate(string p, bool s, byte[] c)
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
		string text = DecryptString(Encoding.ASCII.GetString(content));
		bool flag = false;
		string[] array = new string[2];
		string[] array2 = text.Split('-');
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].Length < 1)
			{
				continue;
			}
			array = array2[i].Split('/');
			int num = int.Parse(array[0]);
			int num2 = int.Parse(array[1]);
			int value;
			if (storedObjectives.TryGetValue(num, out value))
			{
				if (value != num2)
				{
					flag = true;
					storedObjectives[num] = value | num2;
				}
			}
			else
			{
				storedObjectives.Add(num, num2);
				flag = true;
			}
		}
		if (flag)
		{
			SaveDataToFile(syncOnChange);
		}
		InvokeProgressLoad();
	}

	private IEnumerator Start()
	{
		ReadFromFile();
		for (int i = 0; i < 5; i++)
		{
			yield return null;
		}
		UpdateCompletedObjectives();
	}

	public static void SetObjectiveState(int levelId, bool isOwn, bool isIntact)
	{
		int num = (isOwn ? 1 : 0);
		num += (isIntact ? 2 : 0);
		if (storedObjectives.ContainsKey(levelId))
		{
			int num2 = storedObjectives[levelId];
			if (num2 == 3 || num2 == num)
			{
				return;
			}
			num = num2 | num;
			storedObjectives[levelId] = num;
		}
		else
		{
			storedObjectives.Add(levelId, num);
		}
		SaveDataToFile();
	}

	public static int GetObjectiveState(int levelID)
	{
		int value;
		if (storedObjectives.TryGetValue(levelID, out value))
		{
			return value;
		}
		return 0;
	}

	internal static void UpdateCompletedObjectives()
	{
		int num = 0;
		int num2 = 0;
		foreach (KeyValuePair<int, int> storedObjective in storedObjectives)
		{
			int num3 = 0;
			if ((storedObjective.Value & 1) != 0)
			{
				num3++;
			}
			if ((storedObjective.Value & 2) != 0)
			{
				num3++;
			}
			switch (DlcManager.GetLevelDLCTypeFromLevelIndex(storedObjective.Key))
			{
			case 0:
				num += num3;
				break;
			case 1:
				num2 += num3;
				break;
			}
		}
		foreach (KeyValuePair<int, AchievementTrigger> levelAchievement in LevelAchievementTrigger.levelAchievements)
		{
			if (levelAchievement.Value.Completed())
			{
				switch (DlcManager.GetLevelDLCTypeFromLevelIndex(levelAchievement.Key))
				{
				case 0:
					num++;
					break;
				case 1:
					num2++;
					break;
				}
			}
		}
		if (completeAllSecondaryObjectives != null && completeAllSecondaryObjectives.GetProgress() < num)
		{
			completeAllSecondaryObjectives.SetValueAchievement(num);
		}
		if (completeAllSecondaryObjectivesSS != null && completeAllSecondaryObjectivesSS.GetProgress() < num2)
		{
			completeAllSecondaryObjectivesSS.SetValueAchievement(num2);
		}
	}

	private static void SaveDataToFile(bool syncOnChange = true)
	{
		UpdateCompletedObjectives();
		string text = string.Empty;
		foreach (KeyValuePair<int, int> storedObjective in storedObjectives)
		{
			string text2 = text;
			text = text2 + storedObjective.Key + "/" + storedObjective.Value + "-";
		}
		byte[] bytes = Encoding.ASCII.GetBytes(EncryptString(text));
		BesiegeFileManager.Save("LevelObjectives.txt", SingleInstance<LevelObjectiveFileManager>.Instance.saveLocation, bytes);
		if (syncOnChange && SteamManager.Initialized)
		{
			WorkshopManager workshopManager = SingleInstance<WorkshopManager>.Instance;
			if (workshopManager != null)
			{
				string fileName = Path.Combine(StaticSettings.DataPath, "LevelObjectives.txt");
				FileInfo fileInfo = new FileInfo(fileName);
				workshopManager.WriteRemoteFileAsync(fileInfo, false);
			}
		}
	}

	private static void OnSaveComplete(string path)
	{
	}

	private static void LoadDataFromString(string data)
	{
		storedObjectives.Clear();
		string[] array = new string[2];
		string[] array2 = data.Split('-');
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].Length >= 1)
			{
				array = array2[i].Split('/');
				storedObjectives.Add(int.Parse(array[0]), int.Parse(array[1]));
			}
		}
	}

	public void ReadFromFile()
	{
		byte[] data = new byte[0];
		if (BesiegeFileManager.Load("LevelObjectives.txt", saveLocation, out data))
		{
			string data2 = DecryptString(Encoding.ASCII.GetString(data));
			LoadDataFromString(data2);
		}
		else
		{
			WorkshopManager workshopManager = SingleInstance<WorkshopManager>.Instance;
			if (workshopManager != null)
			{
				if (workshopManager.IsRemoteFile("LevelObjectives.txt"))
				{
					workshopManager.ReadRemoteFileAsync("LevelObjectives.txt", delegate(string p, bool s, byte[] c)
					{
						OnSyncProgress(s, c, false);
					});
				}
			}
			else
			{
				SaveDataToFile();
			}
		}
		InvokeProgressLoad();
	}

	private void InvokeProgressLoad()
	{
		hasLoadedProgress = true;
	}

	internal static string EncryptString(string unencryptedData)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(unencryptedData);
		using (Aes aes = Aes.Create())
		{
			aes.Key = Encoding.UTF8.GetBytes(key);
			aes.IV = Encoding.UTF8.GetBytes(iv);
			using (ICryptoTransform cryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
					{
						cryptoStream.Write(bytes, 0, bytes.Length);
					}
					return Convert.ToBase64String(memoryStream.ToArray());
				}
			}
		}
	}

	internal static string DecryptString(string encryptedText)
	{
		using (Aes aes = Aes.Create())
		{
			aes.Key = Encoding.UTF8.GetBytes(key);
			aes.IV = Encoding.UTF8.GetBytes(iv);
			ICryptoTransform cryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV);
			byte[] buffer = Convert.FromBase64String(encryptedText);
			using (MemoryStream stream = new MemoryStream(buffer))
			{
				using (CryptoStream stream2 = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Read))
				{
					using (StreamReader streamReader = new StreamReader(stream2))
					{
						return streamReader.ReadToEnd();
					}
				}
			}
		}
	}
}
