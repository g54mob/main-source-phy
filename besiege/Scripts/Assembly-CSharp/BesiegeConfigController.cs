using System;
using System.IO;
using System.Text.RegularExpressions;
using Ordered;
using UnityEngine;

internal class BesiegeConfigController : MonoBehaviour
{
	private string legacyConfigPath;

	private string configFile = "Config.xml";

	private BesiegeFileManager.FileLocation configLocation;

	private string controlFile = "Controls.xml";

	private BesiegeFileManager.FileLocation controlLocation;

	public static bool GeneratedNewSaveFileThisSession;

	private void Awake()
	{
		legacyConfigPath = Path.Combine(StaticSettings.DataPath, "Config.txt");
		if (LoadLegacyConfig())
		{
			GeneratedNewSaveFileThisSession = true;
			OptionsMaster.BesiegeConfig.SetFirstTimerValues();
			SaveConfig();
			DeleteLegacyConfig();
		}
		else
		{
			LoadConfig();
			LoadControls();
			ReferenceMaster.SaveConfigDel += OnSaveConfig;
		}
	}

	private void OnSaveConfig()
	{
		SaveConfig();
	}

	private void OnApplicationQuit()
	{
		SaveConfig();
		SaveControls();
	}

	private void LoadConfig()
	{
		if (!BesiegeFileManager.Exists(configFile, configLocation))
		{
			GeneratedNewSaveFileThisSession = true;
			OptionsMaster.BesiegeConfig.SetFirstTimerValues();
			OptionsMaster.BesiegeConfig.Save(configFile, configLocation);
		}
		OptionsMaster.BesiegeConfig.Load(configFile, configLocation);
		if (SystemInfo.graphicsShaderLevel < 30)
		{
			OptionsMaster.BesiegeConfig.ReflectionQuality = 0;
		}
	}

	private void LoadControls()
	{
		OptionsMaster.CustomControls.Load(controlFile, controlLocation);
	}

	private bool LoadLegacyConfig()
	{
		if (!File.Exists(legacyConfigPath))
		{
			return false;
		}
		string fileContent = File.ReadAllText(legacyConfigPath).Replace("\r\n", "\n");
		ParseLegacyConfig(fileContent);
		return true;
	}

	private void DeleteLegacyConfig()
	{
		File.Delete(legacyConfigPath);
	}

	private bool ParseLegacyConfig(string fileContent)
	{
		MatchCollection matchCollection = Regex.Matches(fileContent, "(.*)\\n(.*)\\n\\n");
		if (matchCollection.Count == 0)
		{
			Debug.LogWarning("Failed to parse legacy config, creating a new one...");
			DeleteLegacyConfig();
			return false;
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (Match item in matchCollection)
		{
			dictionary.Add(item.Groups[1].Value.ToLower().Replace(" ", string.Empty), item.Groups[2].Value.ToLower());
		}
		try
		{
			ReadConfigDictionary(dictionary);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			return false;
		}
		return true;
	}

	private void ReadConfigDictionary(Dictionary<string, string> configDict)
	{
		Debug.Log("read besiege config controller");
		OptionsMaster.BesiegeConfig.ShadowsEnabled = bool.Parse(configDict["shadows"]);
		OptionsMaster.BesiegeConfig.ScreenSpaceAmbientOcclusion = bool.Parse(configDict["ssao"]);
		OptionsMaster.BesiegeConfig.DepthOfField = bool.Parse(configDict["dof"]);
		OptionsMaster.BesiegeConfig.Vignette = bool.Parse(configDict["vignette"]);
		OptionsMaster.BesiegeConfig.Bloom = bool.Parse(configDict["bloom"]);
		OptionsMaster.BesiegeConfig.WindowedMode = bool.Parse(configDict["windowedmode"]);
		OptionsMaster.BesiegeConfig.ScreenWidth = int.Parse(configDict["resolutionwidth"]);
		OptionsMaster.BesiegeConfig.ScreenHeight = int.Parse(configDict["resolutionheight"]);
		OptionsMaster.BesiegeConfig.FirstTimePlaying = bool.Parse(configDict["firsttimeplaying"]);
		OptionsMaster.BesiegeConfig.BloodEnabled = bool.Parse(configDict["bloodenabled"]);
		OptionsMaster.BesiegeConfig.SkinsEnabled = bool.Parse(configDict["skinsenabled"]);
		OptionsMaster.BesiegeConfig.MorePrecisePhysics = bool.Parse(configDict["moreprecisephysicsenabled"]);
		if (configDict.ContainsKey("leveleditorenabled"))
		{
			OptionsMaster.BesiegeConfig.LevelEditorEnabled = bool.Parse(configDict["leveleditorenabled"]);
		}
	}

	private void SaveConfig()
	{
		if (WinScreen.noErrorsDetected)
		{
			OptionsMaster.BesiegeConfig.Save(configFile, configLocation);
		}
	}

	private void SaveControls()
	{
		if (WinScreen.noErrorsDetected)
		{
			OptionsMaster.CustomControls.Save(controlFile, controlLocation);
		}
	}

	private void OnDestroy()
	{
		ReferenceMaster.SaveConfigDel -= OnSaveConfig;
	}
}
