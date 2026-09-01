using System;
using UnityEngine;

public class SaveAndDestroyOnClick : MonoBehaviour
{
	public GameObject objToSave;

	public string fileName;

	public static bool isLoading;

	public static string lastSavedMachineName;

	private void Start()
	{
		isLoading = false;
	}

	private void SaveName(string stringy)
	{
		throw new NotImplementedException();
	}

	private void LoadName(string stringy)
	{
		throw new NotImplementedException();
	}

	private void WorkshopLoadName(string stringy)
	{
		throw new NotImplementedException();
	}
}
