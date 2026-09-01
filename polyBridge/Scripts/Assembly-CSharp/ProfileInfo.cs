using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProfileInfo
{
	private static string PROFILE_INFO_FILENAME = ".activeprofile";

	public static string GetActiveProfileName()
	{
		string infoFilePath = GetInfoFilePath();
		string text = string.Empty;
		if (Utils.FileExists(infoFilePath))
		{
			string[] array = File.ReadAllLines(infoFilePath);
			if (array.Length != 0)
			{
				text = array[0];
			}
		}
		List<string> profileNames = Profiles.GetProfileNames();
		if (profileNames != null)
		{
			foreach (string item in profileNames)
			{
				if (item == text)
				{
					return text;
				}
			}
		}
		if (profileNames != null && profileNames.Count > 0)
		{
			return profileNames[0];
		}
		return null;
	}

	public static void WriteActiveProfileName(string name)
	{
		Utils.WriteAllText(GetInfoFilePath(), name);
	}

	private static string GetInfoFilePath()
	{
		return Path.Combine(Application.persistentDataPath, Profiles.ROOT_DIRECTORY_NAME, PROFILE_INFO_FILENAME);
	}
}
