using System;
using System.Collections.Generic;
using System.IO;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using ModIO;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

public class CampaignLevelSelector : Paginator
{
	public Transform m_grid;

	public GameObject m_cell;

	[SerializeField]
	private UIMapSelector uiMapSelector;

	[SerializeField]
	private PageCounter m_PageCounter;

	private List<GameObject> spawnedObjects = new List<GameObject>();

	protected override void Awake()
	{
		base.Awake();
		if (uiMapSelector != null)
		{
			uiMapSelector.MapSelectorOpened += EnableNavigation;
			uiMapSelector.MapSelectorClosed += DisableNavigation;
		}
	}

	protected override void Populate(int newPage = 0)
	{
		base.Populate(newPage);
		Clear();
		TABSCampaignLevelAsset[] levelsInCurrentCampaign = CampaignPlayerDataHolder.GetLevelsInCurrentCampaign();
		bool flag = false;
		TABSCampaignLevelAsset currentLevel = CampaignPlayerDataHolder.GetCurrentLevel();
		totalPages = Mathf.CeilToInt((float)levelsInCurrentCampaign.Length / 12f);
		int num = levelsInCurrentCampaign.Length - 12 * currentPage;
		if (m_PageCounter != null)
		{
			m_PageCounter.Set(currentPage + 1, totalPages);
		}
		for (int i = 0; i < Mathf.Min(12, num); i++)
		{
			int num2 = levelsInCurrentCampaign.Length - (num - i);
			bool selected = currentLevel.Entity.GUID == levelsInCurrentCampaign[num2].Entity.GUID;
			bool flag2 = ServiceLocator.GetService<ISaveLoaderService>().HasBeatenLevel(levelsInCurrentCampaign[num2].Entity.GUID, CampaignPlayerDataHolder.GetCurrentCampaignID);
			bool flag3 = num2 == 0;
			bool flag4 = flag2 || flag3 || flag;
			bool newLevel = flag4 && !flag2;
			SpawnLevelCell(num2, levelsInCurrentCampaign[num2], flag4, newLevel, selected);
			flag = flag2;
		}
		SelectFirstButton();
	}

	protected override void Update()
	{
		base.Update();
		if (base.IsOpen)
		{
			if (PlayerActions.Instance.m_cycleUnitsUp.WasPressed)
			{
				NextPage();
			}
			if (PlayerActions.Instance.m_cycleUnitsDown.WasPressed)
			{
				PreviousPage();
			}
		}
	}

	private void SpawnLevelCell(int index, TABSCampaignLevelAsset reference, bool unlocked, bool newLevel, bool selected)
	{
		GameObject cell = UnityEngine.Object.Instantiate(m_cell, m_grid, worldPositionStays: false);
		MapGrid mapGrid = cell.GetComponent<MapGrid>();
		reference.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
		{
			if (mapGrid != null)
			{
				mapGrid.Setup(sprite, reference.Entity.Name, !reference.IsCustomCampaignLevel, reference.CampaignInfo.Description);
			}
		});
		mapGrid.CheckLevelWinStreak(reference.Entity.GUID, CampaignPlayerDataHolder.GetCurrentCampaignID);
		if (selected)
		{
			mapGrid.Selected();
		}
		cell.FetchComponent<CampaignSelectedCellUI>().Init(index, reference, delegate
		{
			OnLevelClicked(index);
		}, unlocked, newLevel);
		cell.transform.localScale = Vector3.one;
		cell.SetActive(value: true);
		spawnedObjects.Add(cell);
		if (reference.IsModIOLevel)
		{
			ModManager.GetModLogo(reference.ModProfile, LogoSize.Thumbnail_320x180, delegate(Texture2D texture2D)
			{
				cell.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.zero);
			}, delegate(WebRequestError error)
			{
				Debug.LogError("Could not get mod image: " + error.errorMessage);
			});
			return;
		}
		if (reference.IsCustomCampaignLevel)
		{
			FileInfo fileInfo = new FileInfo(Path.Combine(new FileInfo(reference.FilePath).Directory.FullName, "Picture.png"));
			string path = fileInfo.FullName;
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (exists)
				{
					fileIO.ReadAllBytes(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(byte[] bytes, Exception exception)
					{
						Texture2D texture2D = new Texture2D(2, 2);
						if (exception == null && bytes != null)
						{
							texture2D.LoadImage(bytes);
						}
						cell.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.zero);
					});
				}
			});
			return;
		}
		reference.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
		{
			if (cell != null && sprite != null)
			{
				cell.GetComponent<Image>().sprite = sprite;
			}
		});
	}

	private void OnLevelClicked(int index)
	{
		if (uiMapSelector != null)
		{
			uiMapSelector.CloseMapSelector();
		}
		else
		{
			ServiceLocator.GetService<InputService>().ChangeToGameplay();
		}
		CampaignPlayerDataHolder.LoadCampaignLevelWithIndex(index);
	}

	public override void OpenPage()
	{
		base.OpenPage();
	}

	protected void SelectFirstButton()
	{
		if (spawnedObjects != null && spawnedObjects.Count > 0)
		{
			Button component = spawnedObjects[0].GetComponent<Button>();
			if (component != null)
			{
				component.Select();
			}
		}
	}

	private void EnableNaviagtionOnUnpause()
	{
		if (base.IsOpen)
		{
			EnableNavigation();
		}
	}

	private void DisableNavigationOnPause()
	{
		if (base.IsOpen)
		{
			DisableNavigation();
		}
	}

	protected override void Clear()
	{
		base.Clear();
		for (int i = 0; i < spawnedObjects.Count; i++)
		{
			UnityEngine.Object.Destroy(spawnedObjects[i]);
		}
		spawnedObjects.Clear();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (uiMapSelector != null)
		{
			uiMapSelector.MapSelectorOpened += EnableNavigation;
			uiMapSelector.MapSelectorClosed += DisableNavigation;
		}
	}
}
