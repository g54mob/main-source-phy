using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[AddComponentMenu("UI/Tutorial/Tutorial File Manager")]
public class TutorialFileManager : SingleInstance<TutorialFileManager>
{
	public const string saveFile = "TutorialProgress.txt";

	public static Action onProgressLoad;

	public static bool hasLoadedProgress = false;

	private static Dictionary<string, int> tutorialStates = new Dictionary<string, int>();

	private BesiegeFileManager.FileLocation saveLocation;

	public override string Name
	{
		get
		{
			return "TutorialFileManager";
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
		ReadFromFile();
	}

	private void OnDestroy()
	{
	}

	public static void ResetProgress()
	{
		tutorialStates.Clear();
		TutorialBaseContainer.indeces = new int[60];
		SaveDataToFile();
	}

	public static void SetTutorialState(string tutorialId, int state)
	{
		if (tutorialStates.ContainsKey(tutorialId))
		{
			tutorialStates[tutorialId] = state;
		}
		else
		{
			tutorialStates.Add(tutorialId, state);
		}
		SaveDataToFile();
		SaveDataToFile();
	}

	public static int GetTutorialState(string tutorialId)
	{
		int value;
		if (tutorialStates.TryGetValue(tutorialId, out value))
		{
			return value;
		}
		return -1;
	}

	private static void SaveDataToFile(bool syncOnChange = true)
	{
		string text = string.Empty;
		foreach (KeyValuePair<string, int> tutorialState in tutorialStates)
		{
			string text2 = text;
			text = text2 + tutorialState.Key + "@" + tutorialState.Value + ";";
		}
		byte[] bytes = Encoding.ASCII.GetBytes(text);
		BesiegeFileManager.Save("TutorialProgress.txt", SingleInstance<TutorialFileManager>.Instance.saveLocation, bytes);
	}

	private static void OnSaveComplete(string path)
	{
	}

	private static void LoadDataFromString(string data)
	{
		tutorialStates.Clear();
		string[] array = new string[2];
		string[] array2 = data.Split(';');
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].Length >= 1)
			{
				array = array2[i].Split('@');
				tutorialStates.Add(array[0], int.Parse(array[1]));
				Debug.Log("Adding dictionary entry: " + array[0] + "@" + int.Parse(array[1]));
			}
		}
	}

	public void ReadFromFile()
	{
		byte[] data = new byte[0];
		if (BesiegeFileManager.Load("TutorialProgress.txt", saveLocation, out data))
		{
			string data2 = Encoding.ASCII.GetString(data);
			LoadDataFromString(data2);
		}
		else
		{
			SaveDataToFile();
		}
		InvokeProgressLoad();
	}

	private void InvokeProgressLoad()
	{
		if (onProgressLoad != null)
		{
			onProgressLoad();
		}
		hasLoadedProgress = true;
	}
}
