using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefabContainer
{
	public Vector3 offset;

	public GameObject prefab;

	public string containingTab;

	public string path;

	public string name;

	public bool physicsGoal = true;

	private Dictionary<string, Action> xmlSaverDictionary;

	private Dictionary<string, Action> xmlReaderDictionary;

	public Dictionary<string, Action> XmlReaderDictionary
	{
		get
		{
			if (xmlReaderDictionary != null)
			{
				xmlReaderDictionary = new Dictionary<string, Action>();
			}
			return xmlReaderDictionary;
		}
	}

	public Dictionary<string, Action> XmlSaverDictionary
	{
		get
		{
			if (xmlSaverDictionary != null)
			{
				xmlSaverDictionary = new Dictionary<string, Action>();
			}
			return xmlSaverDictionary;
		}
	}

	public LevelPrefabContainer(string path, float yOffset, GameObject prefab)
	{
		offset = new Vector3(0f, yOffset, 0f);
		this.prefab = prefab;
		containingTab = "RememberToChangeThis!";
		name = prefab.name;
		this.path = path;
		containingTab = name;
	}

	private bool TestForErrors()
	{
		if (prefab == null)
		{
			Debug.LogError("Prefab is null!");
			return false;
		}
		if (containingTab.Equals("RememberToChangeThis"))
		{
			Debug.LogError("You forgot to change the ContainingFolder!");
			return false;
		}
		Debug.LogError(containingTab + " is not an existing folder");
		return false;
	}
}
