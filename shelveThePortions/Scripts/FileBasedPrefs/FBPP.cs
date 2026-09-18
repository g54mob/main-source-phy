using System;
using System.IO;
using System.Text;
using UnityEngine;

public static class FBPP
{
	private class FBPPInitException : Exception
	{
		public FBPPInitException(string message)
			: base(message)
		{
		}
	}

	private const string INIT_EXCEPTION_MESSAGE = "Error, you must call FBPP.Start(FBPPConfig config) before trying to get or set saved data.";

	private static FBPPConfig _config;

	public static bool ShowInitWarning = true;

	private static FBPPFileModel _latestData;

	private static StringBuilder _sb = new StringBuilder();

	private const string String_Empty = "";

	public static void Start(FBPPConfig config)
	{
		_config = config;
		_latestData = null;
		_latestData = GetSaveFile();
	}

	private static void CheckForInit()
	{
		if (_config == null)
		{
			throw new FBPPInitException("Error, you must call FBPP.Start(FBPPConfig config) before trying to get or set saved data.");
		}
	}

	public static void SetString(string key, string value = "")
	{
		AddDataToSaveFile(key, value);
	}

	public static string GetString(string key, string defaultValue = "")
	{
		return (string)GetDataFromSaveFile(key, defaultValue);
	}

	public static void SetInt(string key, int value = 0)
	{
		AddDataToSaveFile(key, value);
	}

	public static int GetInt(string key, int defaultValue = 0)
	{
		return (int)GetDataFromSaveFile(key, defaultValue);
	}

	public static void SetFloat(string key, float value = 0f)
	{
		AddDataToSaveFile(key, value);
	}

	public static float GetFloat(string key, float defaultValue = 0f)
	{
		return (float)GetDataFromSaveFile(key, defaultValue);
	}

	public static void SetBool(string key, bool value = false)
	{
		AddDataToSaveFile(key, value);
	}

	public static bool GetBool(string key, bool defaultValue = false)
	{
		return (bool)GetDataFromSaveFile(key, defaultValue);
	}

	public static bool HasKey(string key)
	{
		return GetSaveFile().HasKey(key);
	}

	public static bool HasKeyForString(string key)
	{
		return GetSaveFile().HasKeyFromObject(key, string.Empty);
	}

	public static bool HasKeyForInt(string key)
	{
		return GetSaveFile().HasKeyFromObject(key, 0);
	}

	public static bool HasKeyForFloat(string key)
	{
		return GetSaveFile().HasKeyFromObject(key, 0f);
	}

	public static bool HasKeyForBool(string key)
	{
		return GetSaveFile().HasKeyFromObject(key, false);
	}

	public static void DeleteKey(string key)
	{
		GetSaveFile().DeleteKey(key);
		SaveSaveFile();
	}

	public static void DeleteString(string key)
	{
		GetSaveFile().DeleteString(key);
		SaveSaveFile();
	}

	public static void DeleteInt(string key)
	{
		GetSaveFile().DeleteInt(key);
		SaveSaveFile();
	}

	public static void DeleteFloat(string key)
	{
		GetSaveFile().DeleteFloat(key);
		SaveSaveFile();
	}

	public static void DeleteBool(string key)
	{
		GetSaveFile().DeleteBool(key);
		SaveSaveFile();
	}

	public static void DeleteAll()
	{
		WriteToSaveFile(JsonUtility.ToJson(new FBPPFileModel()));
		_latestData = new FBPPFileModel();
	}

	public static void OverwriteLocalSaveFile(string data)
	{
		WriteToSaveFile(data);
		_latestData = null;
		_latestData = GetSaveFile();
	}

	private static FBPPFileModel GetSaveFile()
	{
		CheckForInit();
		CheckSaveFileExists();
		if (_latestData == null)
		{
			string text = File.ReadAllText(GetSaveFilePath());
			if (_config.ScrambleSaveData)
			{
				text = DataScrambler(text);
			}
			try
			{
				_latestData = JsonUtility.FromJson<FBPPFileModel>(text);
			}
			catch (ArgumentException ex)
			{
				Debug.LogException(new Exception("FBPP Error loading save file: " + ex.Message));
				if (_config.OnLoadError != null)
				{
					_config.OnLoadError();
				}
				else
				{
					DeleteAll();
				}
			}
		}
		return _latestData;
	}

	public static string GetSaveFilePath()
	{
		CheckForInit();
		return Path.Combine(_config.GetSaveFilePath(), _config.SaveFileName);
	}

	public static string GetSaveFileAsJson()
	{
		CheckForInit();
		CheckSaveFileExists();
		return JsonUtility.ToJson(GetSaveFile());
	}

	private static object GetDataFromSaveFile(string key, object defaultValue)
	{
		return GetSaveFile().GetValueForKey(key, defaultValue);
	}

	private static void AddDataToSaveFile(string key, object value)
	{
		CheckForInit();
		GetSaveFile().UpdateOrAddData(key, value);
		SaveSaveFile();
	}

	public static void Save()
	{
		CheckForInit();
		SaveSaveFile(manualSave: true);
	}

	private static void SaveSaveFile(bool manualSave = false)
	{
		if (_config.AutoSaveData || manualSave)
		{
			WriteToSaveFile(JsonUtility.ToJson(GetSaveFile()));
		}
	}

	private static void WriteToSaveFile(string data)
	{
		StreamWriter streamWriter = new StreamWriter(GetSaveFilePath());
		if (_config.ScrambleSaveData)
		{
			data = DataScrambler(data);
		}
		streamWriter.Write(data);
		streamWriter.Close();
	}

	private static void CheckSaveFileExists()
	{
		if (!DoesSaveFileExist())
		{
			CreateNewSaveFile();
		}
	}

	private static bool DoesSaveFileExist()
	{
		return File.Exists(GetSaveFilePath());
	}

	private static void CreateNewSaveFile()
	{
		WriteToSaveFile(JsonUtility.ToJson(new FBPPFileModel()));
	}

	private static string DataScrambler(string data)
	{
		_sb.Clear();
		for (int i = 0; i < data.Length; i++)
		{
			_sb.Append((char)(data[i] ^ _config.EncryptionSecret[i % _config.EncryptionSecret.Length]));
		}
		return _sb.ToString();
	}
}
