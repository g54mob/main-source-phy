using System;
using System.Collections.Generic;
using UnityEngine;

public class TagCheckboxManager : MonoBehaviour
{
	public enum ModTags
	{
		MachineBlocks = 0,
		LevelEditorObjects = 1,
		LevelEditorLogic = 2,
		LevelEditorExtensions = 3,
		BuildingUtility = 4,
		MachineControlUtility = 5,
		CheatsPowers = 6,
		Informative = 7,
		SupportsMultiplayer = 8
	}

	public bool useBlockTags;

	public bool useLevelTags;

	public bool useModTags;

	private bool[] tagStates;

	private TagCheckbox[] checkboxes;

	private string[] ignoredTags = new string[4] { "Machines", "Levels", "Mods", "Skin Packs" };

	private string[] machineTags = new string[21]
	{
		"Vehicle", "Cars", "Tanks", "Planes", "Boats & Ships", "Rotors", "Airships", "Submarines", "Walkers And Mechs", "Transformers",
		"Mechanisms And Systems", "Stationary", "Medieval", "Amphibious", "Other", "Fits In Bounding Box", "WIP", "Requires Mod", "Basic", "Advanced",
		"Built With Mods"
	};

	private string[] blockTags = new string[10] { "Medieval Packs", "Modern Packs", "Futuristic Packs", "Simplistic Packs", "Fantasy Packs", "Themed Packs", "Other Packs", "New Models", "New Textures", "High Resolution" };

	private string[] levelTags = new string[10] { "Single Player", "Co-op", "Versus", "Sandbox", "Race", "Combat", "Puzzle", "Transport", "Challenge", "Sport" };

	private string[] modTags = new string[10] { "Machine Blocks", "Building Utility", "Machine Control Utility", "Level Editor Objects", "Level Editor Logic", "Level Editor Extensions", "Cheats and Powers", "Informative", "Multiplayer Supported", "Miscellaneous" };

	private string[] tagNames;

	private bool init;

	private TagCheckbox basicTag;

	public void Awake()
	{
		Initialize();
	}

	private void Initialize()
	{
		if (init)
		{
			return;
		}
		bool flag = false;
		if (useBlockTags)
		{
			tagNames = blockTags;
		}
		else if (useLevelTags)
		{
			tagNames = levelTags;
		}
		else if (useModTags)
		{
			tagNames = modTags;
		}
		else
		{
			tagNames = machineTags;
			flag = true;
		}
		tagStates = new bool[tagNames.Length];
		checkboxes = new TagCheckbox[tagNames.Length];
		for (int i = 0; i < tagNames.Length; i++)
		{
			Transform transform = base.transform.Find("Button(Tag" + ((i >= 10) ? string.Empty : "0") + i + ")");
			TagCheckbox tagCheckbox = ((!(transform != null)) ? null : transform.GetComponent<TagCheckbox>());
			checkboxes[i] = tagCheckbox;
			if (tagCheckbox != null)
			{
				tagCheckbox.Initialize(this, tagNames[i]);
				if (flag && tagNames[i].Equals("Basic"))
				{
					basicTag = tagCheckbox;
				}
			}
			else
			{
				Debug.LogWarning("Failed to setup Tag toggle: " + tagNames[i]);
			}
		}
		init = true;
	}

	public void OnEnable()
	{
		if (!useBlockTags && !useLevelTags && !useModTags)
		{
			Initialize();
			if (basicTag != null)
			{
				basicTag.Toggle(true);
			}
		}
	}

	public void SetTags(List<string> tags)
	{
		Initialize();
		for (int i = 0; i < tags.Count; i++)
		{
			if (Array.IndexOf(ignoredTags, tags[i]) <= -1)
			{
				int num = Array.IndexOf(tagNames, tags[i].Replace(" box", " Box").Replace("and Sys", "And Sys").Replace("and Mechs", "And Mechs"));
				if (num >= 0)
				{
					checkboxes[num].Toggle(true);
				}
				else
				{
					Debug.LogError(tags[i] + " doesn't exist in tag list");
				}
			}
		}
	}

	public void SetTag(int tagNum, bool state)
	{
		if (tagNum < tagStates.Length || tagNum < 0)
		{
			checkboxes[tagNum].Toggle(state);
		}
		else
		{
			MonoBehaviour.print("Error: selected non existent tag");
		}
	}

	public void SetTagState(int tagNum, bool state)
	{
		if (tagNum < tagStates.Length || tagNum < 0)
		{
			tagStates[tagNum] = state;
		}
		else
		{
			MonoBehaviour.print("Error: selected non existent tag");
		}
	}

	public List<string> GetTagSelected()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < tagNames.Length; i++)
		{
			if (tagStates[i])
			{
				list.Add(tagNames[i]);
			}
		}
		return list;
	}
}
