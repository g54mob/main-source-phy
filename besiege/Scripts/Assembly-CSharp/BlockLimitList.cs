using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Selectors;
using UnityEngine;

public class BlockLimitList : MonoBehaviour
{
	public BlockLimitEntry entryPrefab;

	public Transform contentContainer;

	public UIScrollbar scrollbar;

	public UIButton banAll;

	public ValueHolderDefaulting overallLimitField;

	public GameObject infiniteIcon;

	public uint minimumBlocksToAlwaysBeAllowed = 2u;

	protected List<BlockLimitEntry> entries = new List<BlockLimitEntry>();

	private LevelEditor levelEditor;

	private LevelSettingsScreen settingsScreen;

	protected bool banAllToggle;

	private bool wasSelected;

	public void Init(LevelSettingsScreen settings)
	{
		banAll.Down += ToggleAllBans;
		overallLimitField.ValueChanged += OnSetOverallBlockRestriction;
		overallLimitField.FocusChange += delegate(bool b)
		{
			if (!b)
			{
				OnSetOverallBlockRestriction(overallLimitField.ValueNumber);
			}
		};
		settingsScreen = settings;
		levelEditor = LevelEditor.Instance;
	}

	public void Refresh()
	{
		StartCoroutine(IEOnEnable());
	}

	public IEnumerator IEOnEnable()
	{
		yield return new WaitForEndOfFrame();
		LevelSettings settings = levelEditor.Settings;
		overallLimitField.SetText(settings.BlockCountLimiter);
		SetOverallBlockRestictions(settings.BlockCountLimiter);
		ClearList();
		GenerateList();
	}

	protected void GenerateList()
	{
		int num = 0;
		List<int> list = PrefabMaster.BlockPrefabs.Keys.ToList();
		for (int i = 1; i < list.Count; i++)
		{
			int num2 = list[i];
			BlockType blockType = (BlockType)num2;
			if (blockType != BlockType.BuildNode && blockType != BlockType.BuildEdge && blockType != BlockType.Unused && blockType != BlockType.Unused3 && blockType != BlockType.CameraBlock && blockType != BlockType.Magnet)
			{
				BlockPrefab blockPrefab = PrefabMaster.BlockPrefabs[num2];
				BlockLimitEntry blockLimitEntry = Object.Instantiate(entryPrefab, contentContainer) as BlockLimitEntry;
				blockLimitEntry.name = "Entry: " + blockPrefab.name;
				blockLimitEntry.transform.localPosition = new Vector3(0f, -0.375f - (float)num * 0.8f, 0f);
				blockLimitEntry.transform.localScale = Vector3.one;
				blockLimitEntry.SetupEntry(blockPrefab, settingsScreen, levelEditor, 3);
				entries.Add(blockLimitEntry);
				num++;
			}
		}
		scrollbar.UpdateBounds();
	}

	protected void ClearList()
	{
		for (int i = 0; i < entries.Count; i++)
		{
			Object.Destroy(entries[i].gameObject);
		}
		foreach (Transform item in contentContainer)
		{
			Object.Destroy(item.gameObject);
		}
		entries.Clear();
	}

	protected void OnSetOverallBlockRestriction(float value)
	{
		SetOverallBlockRestictions(value);
		settingsScreen.OnUpdateSettings();
	}

	protected void SetOverallBlockRestictions(float value)
	{
		if (value >= 0f && value < (float)minimumBlocksToAlwaysBeAllowed)
		{
			value = minimumBlocksToAlwaysBeAllowed;
			overallLimitField.SetText(minimumBlocksToAlwaysBeAllowed);
		}
		levelEditor.Settings.BlockCountLimiter = (int)value;
		infiniteIcon.SetActive(value < 0f);
	}

	protected void ToggleAllBans()
	{
		banAllToggle = !banAllToggle;
		for (int i = 0; i < entries.Count; i++)
		{
			entries[i].SetBan(banAllToggle);
		}
		settingsScreen.OnUpdateSettings();
	}

	protected void Update()
	{
		if (overallLimitField.IsFocused && !wasSelected)
		{
			infiniteIcon.SetActive(false);
			wasSelected = true;
		}
		else if (!overallLimitField.IsFocused && wasSelected)
		{
			wasSelected = false;
		}
	}
}
