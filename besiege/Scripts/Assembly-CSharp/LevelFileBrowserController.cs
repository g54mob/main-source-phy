using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameGrind;
using Localisation;
using UnityEngine;

public class LevelFileBrowserController : FileBrowserController
{
	private const string NO_BUILD_ZONE_KEY = "SavedWithoutBuildZone";

	private readonly string LoadErrorMessage;

	private readonly string SaveErrorMessage;

	private readonly string SelectionNotValidMessage;

	public LevelFileBrowserController(FileBrowserView browserView)
		: base(browserView)
	{
		LoadErrorMessage = LocalisationManager.GetTranslation(5021);
		SaveErrorMessage = LocalisationManager.GetTranslation(5022);
		SelectionNotValidMessage = LocalisationManager.GetTranslation(5024);
	}

	protected override void LoadFile(IVirtualObject virtualObject, OpenMode mode)
	{
		if (FileBrowserView.saveMenuUpload)
		{
			UpdateWorkshopFile(virtualObject);
			return;
		}
		FileInfo fileInfo = new FileInfo(virtualObject.ObjectPath.Path);
		if (StatMaster.waitingForServerResponse)
		{
			view.Close();
			return;
		}
		if (StatMaster.Mode.LevelEditor.isSelectingLevel)
		{
			LevelPlaylistManager.Current.OnAdd(fileInfo.FullName);
			return;
		}
		if (!fileInfo.Exists)
		{
			Debug.LogWarning("Level doesn't exist: " + fileInfo.FullName);
			OnFileLoaded(FileLoadResult.FileNotFound);
			return;
		}
		try
		{
			switch (mode)
			{
			case OpenMode.Normal:
				Load(fileInfo);
				OnFileLoaded(FileLoadResult.Success);
				break;
			case OpenMode.AdditiveOrSelectionOnly:
				LoadAdditive(fileInfo);
				OnFileLoaded(FileLoadResult.SuccessAdditive);
				break;
			default:
				throw new NotImplementedException(mode.ToString());
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Error loading level: " + fileInfo.FullName + "\n" + ex);
			SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(LoadErrorMessage, 5f);
			OnFileLoaded(FileLoadResult.Failed);
		}
	}

	private void Load(FileInfo fileInfo)
	{
		StatMaster.lastLoadedLevel = fileInfo.Name;
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("Load level: " + StatMaster.lastLoadedLevel);
		}
		string levelData = File.ReadAllText(fileInfo.FullName);
		FileBrowserView.AddLastEntry(FileBrowserType.LocalLevels, Path.GetFileNameWithoutExtension(fileInfo.Name));
		NetworkAuxAddPiece instance = NetworkAuxAddPiece.Instance;
		instance.LoadLevel(levelData, Path.GetFileNameWithoutExtension(fileInfo.FullName));
	}

	private void LoadAdditive(FileInfo fileInfo)
	{
		XDataHolder customData;
		List<EntityController.PlaceEntry> entries;
		if (!LevelXMLLoader.ReadLevelInfoFromFile(fileInfo.FullName, out customData, out entries))
		{
			SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(LoadErrorMessage, 5f);
			return;
		}
		if (customData.HasKey("SavedWithoutBuildZone"))
		{
			entries.RemoveAt(0);
		}
		LevelEditor.Instance.selectionController.DeselectAll(true);
		LevelEditor.Instance.SetActiveTool(StatMaster.Tool.Translate);
		LevelEditor.Instance.entityController.Add(entries, false, true, true);
	}

	protected void UpdateWorkshopFile(IVirtualObject virtualObject)
	{
		UploadData localFileUploadData = GenerateUploadData(virtualObject);
		view.UpdateWorkshopFileFromLocal(localFileUploadData);
	}

	protected override void SaveFile(IVirtualObject virtualObject, OpenMode mode)
	{
		FileInfo fileInfo = new FileInfo(virtualObject.ObjectPath.Path);
		try
		{
			switch (mode)
			{
			case OpenMode.Normal:
				Save(fileInfo);
				break;
			case OpenMode.AdditiveOrSelectionOnly:
				SaveSelection(fileInfo);
				break;
			default:
				throw new NotImplementedException(mode.ToString());
			}
			if (OptionsMaster.BesiegeConfig.CloudSaving && SteamManager.Initialized)
			{
				WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
				if (instance != null)
				{
					instance.WriteRemoteFileAsync(fileInfo, true);
				}
			}
			ReferenceMaster.ConsoleController.AppendLogLine("Successfully saved level '" + fileInfo.Name + "'");
			if (SingleInstance<AchievementManager>.hasInstance())
			{
				Journal.Increment(3, 1);
			}
			OnFileSaved(mode, FileSaveResult.Success);
		}
		catch (Exception ex)
		{
			Debug.LogError("Error saving level: " + fileInfo.FullName + "\n" + ex);
			SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(SaveErrorMessage, 5f);
			OnFileSaved(mode, FileSaveResult.Failed);
		}
	}

	private void Save(FileInfo fileInfo)
	{
		LevelXMLSaver.Create(fileInfo.FullName, fileInfo.Name);
		CreateThumbnail(fileInfo, true);
	}

	private void SaveSelection(FileInfo fileInfo)
	{
		if (LevelEditor.Instance.SelectionCount == 0)
		{
			SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(SelectionNotValidMessage, 5f);
			return;
		}
		List<LevelEntity> selection = LevelEditor.Instance.Selection;
		XDataHolder xDataHolder = LevelEditor.Instance.CustomData.Clone();
		if (!selection.Any((LevelEntity x) => x.isBuildZone))
		{
			xDataHolder.Write("SavedWithoutBuildZone", true);
			selection.Insert(0, LevelEditor.Instance.Entities.First((LevelEntity x) => x.isBuildZone));
		}
		CreateThumbnail(fileInfo, true);
		LevelXMLSaver.Create(fileInfo.FullName, fileInfo.Name, xDataHolder, selection);
	}

	protected override void UploadFile(IVirtualObject virtualObject)
	{
		UploadLevelOrFolder(virtualObject);
	}

	protected override void UploadFolder(IVirtualObject virtualObject)
	{
		UploadLevelOrFolder(virtualObject);
	}

	public override bool ShowAdditiveOrSelectionOnlyButton(bool isSaveMenu)
	{
		return LevelEditor.Instance.isActive && !StatMaster.levelSimulating && !FileBrowserView.saveMenuUpload && !StatMaster.Mode.LevelEditor.isSelectingLevel && (!isSaveMenu || (LevelEditor.Instance.SelectionCount > 0 && StatMaster.Mode.selectedTool != StatMaster.Tool.Modify));
	}

	private void UploadLevelOrFolder(IVirtualObject virtualObject)
	{
		string path = virtualObject.ObjectPath.Path;
		string path2 = virtualObject.ThumbnailPath.Path;
		string fileName = Path.GetFileNameWithoutExtension(path);
		if (virtualObject.IsFolder)
		{
			fileName = virtualObject.ObjectPath.EntityName;
		}
		view.OpenUploadDialog(WorkshopManager.ItemTypes.Levels, virtualObject.IsFolder, path, fileName, path2);
	}
}
