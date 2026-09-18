using System;
using UnityEngine;

public class FBPPConfig
{
	private const string DEFAULT_SAVE_FILE_NAME = "saveData.txt";

	private const string DEFAULT_ENCRYPTION_SECRET = "encryption-secret-default";

	public string SaveFileName = "saveData.txt";

	public bool AutoSaveData = true;

	public bool ScrambleSaveData = true;

	public string EncryptionSecret = "encryption-secret-default";

	public string SaveFilePath;

	public Action OnLoadError;

	internal string GetSaveFilePath()
	{
		if (!string.IsNullOrEmpty(SaveFilePath))
		{
			return SaveFilePath;
		}
		return Application.persistentDataPath;
	}
}
