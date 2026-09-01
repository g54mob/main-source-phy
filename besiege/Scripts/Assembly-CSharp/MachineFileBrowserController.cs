using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoSave;
using BesiegeDlc;
using GameGrind;
using Localisation;
using UnityEngine;

public class MachineFileBrowserController : FileBrowserController
{
	private const string NO_STARTING_BLOCK_KEY = "SavedWithoutStartingBlock";

	private bool isLoading;

	private readonly string LoadErrorMessage;

	private readonly string SaveErrorMessage;

	private readonly string FormatErrorMessage;

	private readonly string SelectionNotValidMessage;

	private readonly string BackupFailedMessage;

	public MachineFileBrowserController(FileBrowserView browserView)
		: base(browserView)
	{
		LoadErrorMessage = LocalisationManager.GetTranslation(5021);
		SaveErrorMessage = LocalisationManager.GetTranslation(5022);
		FormatErrorMessage = LocalisationManager.GetTranslation(5023);
		SelectionNotValidMessage = LocalisationManager.GetTranslation(5024);
		BackupFailedMessage = LocalisationManager.GetTranslation(5026);
	}

	protected override void LoadFile(IVirtualObject virtualObject, OpenMode mode)
	{
		if (FileBrowserView.saveMenuUpload)
		{
			UpdateWorkshopFile(virtualObject);
			return;
		}
		string path;
		string name;
		MachineInfo machineInfo = GetMachineInfo(virtualObject, out path, out name);
		if (machineInfo == null)
		{
			OnFileLoaded(FileLoadResult.Failed);
			return;
		}
		try
		{
			switch (mode)
			{
			case OpenMode.Normal:
				Load(machineInfo, path, name);
				OnFileLoaded(FileLoadResult.Success);
				break;
			case OpenMode.AdditiveOrSelectionOnly:
				LoadAdditive(machineInfo);
				OnFileLoaded(FileLoadResult.SuccessAdditive);
				break;
			default:
				throw new NotImplementedException(mode.ToString());
			}
		}
		catch (Exception exception)
		{
			SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(LoadErrorMessage, 5f);
			Debug.LogException(exception);
			OnFileLoaded(FileLoadResult.Failed);
			return;
		}
		IWorkshopItem workshopItem = virtualObject as IWorkshopItem;
		if (workshopItem != null && !workshopItem.IsOwner)
		{
			Journal.Increment(5, 1);
		}
	}

	private void Load(MachineInfo machineInfo, string path, string name)
	{
		Machine machine = Machine.Active();
		bool flag = false;
		if (machine != null)
		{
			if (machine.CanModify)
			{
				LoadMachine(machine, machineInfo, path);
				flag = true;
			}
		}
		else if (!StatMaster.isMP)
		{
			machine = SingleInstance<MachineObjectTracker>.Instance.CreateNewMachine();
			LoadMachine(machine, machineInfo, path);
			flag = true;
		}
		if (flag)
		{
			FileBrowserView.AddLastEntry(FileBrowserType.LocalMachines, name);
		}
	}

	private void LoadMachine(Machine machine, MachineInfo machineInfo, string machinePath)
	{
		if (StatMaster.isMP)
		{
			NetworkAuxAddPiece.Instance.LoadLocalMachine(machineInfo);
			return;
		}
		ReplaceMachineUndoAction action = new ReplaceMachineUndoAction(machine, machineInfo);
		machine.UndoSystem.AddAction(action);
		machine.LoadMachineInfo(machineInfo, machinePath);
	}

	private void LoadAdditive(MachineInfo machineInfo)
	{
		Machine machine = Machine.Active();
		if (!(machine == null) && machine.CanModify)
		{
			machine.isLoadingInfo = true;
			if (StatMaster.isMP)
			{
				StatMaster.cachingTransformActions = true;
			}
			bool mergeSurfaceTypesOnDeselect = StatMaster.mergeSurfaceTypesOnDeselect;
			StatMaster.mergeSurfaceTypesOnDeselect = false;
			BlockSelectionTool.Duplicating = true;
			List<UndoAction> undoActions = new List<UndoAction>();
			if (machineInfo.MachineData.HasKey("SavedWithoutStartingBlock") && machineInfo.MachineData.ReadBool("SavedWithoutStartingBlock"))
			{
				machineInfo.Blocks.RemoveAt(0);
			}
			Dictionary<Guid, BlockBehaviour> addedBlocks;
			machine.AddBlocksFromInfo(machineInfo.Blocks, out addedBlocks, ref undoActions);
			if (StatMaster.isMP)
			{
				(machine as ServerMachine).FlushAndBan();
			}
			if (undoActions.Count > 0)
			{
				AdvancedBlockEditor.Instance.selectionController.DeselectAll(true);
				AdvancedBlockEditor.Instance.SetActiveTool(StatMaster.Tool.Translate);
				machine.UndoSystem.AddActions(undoActions);
				AdvancedBlockEditor.Instance.selectionController.Select(addedBlocks.Values.ToList(), true, true);
				SingleInstanceFindOnly<AddPiece>.Instance.SingleHammerAnimate(AdvancedBlockEditor.Instance.selectionController.LastBlock.transform.position, AdvancedBlockEditor.Instance.selectionController.LastBlock.transform.position, AdvancedBlockEditor.Instance.selectionController.LastBlock.transform.forward);
				machine.onBatchOperationComplete();
			}
			BlockSelectionTool.Duplicating = false;
			machine.isLoadingInfo = false;
			StatMaster.mergeSurfaceTypesOnDeselect = mergeSurfaceTypesOnDeselect;
			SingleInstanceFindOnly<AddPiece>.Instance.UpdateMiddleOfObject(true);
		}
	}

	private MachineInfo GetMachineInfo(IVirtualObject virtualObject, out string path, out string name)
	{
		try
		{
			path = virtualObject.ObjectPath.Path;
			FileInfo fileInfo = new FileInfo(path);
			name = StaticSettings.SanatizeFileName(fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length));
			string auth = string.Empty;
			if (virtualObject is WorkshopFile)
			{
				WorkshopFile workshopFile = virtualObject as WorkshopFile;
				auth = workshopFile.Author.ToString();
			}
			if (!fileInfo.Exists)
			{
				if (!StatMaster.isHosting)
				{
					Debug.LogWarning("File does not exist: '" + fileInfo.FullName + "'!");
				}
				OnFileLoaded(FileLoadResult.FileNotFound);
				return null;
			}
			fileInfo.MoveTo(Path.Combine(fileInfo.DirectoryName, name + ".bsg"));
			if (isLoading)
			{
				return null;
			}
			isLoading = true;
			MachineInfo machineInfo = null;
			if (XmlSaver.IsXmlFormat(path))
			{
				if (!StatMaster.isHosting)
				{
					Debug.Log(string.Format("Loading machine in XML format: {0}.bsg", name));
				}
				machineInfo = XmlLoader.LoadFromFullPath(path, auth);
			}
			else if (XmlSaver.IsBsgFormat(path))
			{
				if (!StatMaster.isHosting)
				{
					Debug.Log(string.Format("Loading machine in old format: {0}.bsg", name));
				}
				machineInfo = MachineFormatConverter.ConvertBsgToMachineInfo(name, path);
			}
			else
			{
				SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(FormatErrorMessage, 5f);
				if (!StatMaster.isHosting)
				{
					Debug.LogWarning("Machine format not recognized: " + path);
				}
			}
			isLoading = false;
			if (machineInfo == null)
			{
				OnFileLoaded(FileLoadResult.Failed);
				return null;
			}
			List<DlcManager.DlcStatus> dlcIssues;
			if (!DlcManager.Instance.GetInfoDlcStatus(machineInfo, out dlcIssues))
			{
				view.OpenDlcsMissingPopup(dlcIssues, 4441);
				OnFileLoaded(FileLoadResult.Failed);
				return null;
			}
			return machineInfo;
		}
		catch (Exception exception)
		{
			SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(LoadErrorMessage, 5f);
			Debug.LogException(exception);
			OnFileLoaded(FileLoadResult.Failed);
			path = null;
			name = null;
			return null;
		}
	}

	protected void UpdateWorkshopFile(IVirtualObject virtualObject)
	{
		UploadData localFileUploadData = GenerateUploadData(virtualObject);
		view.UpdateWorkshopFileFromLocal(localFileUploadData);
	}

	protected override void SaveFile(IVirtualObject virtualObject, OpenMode mode)
	{
		FileInfo fileInfo = new FileInfo(virtualObject.ObjectPath.Path);
		string text = StaticSettings.SanatizeFileName(fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length));
		Machine.Active().Name = text;
		if (fileInfo.Exists)
		{
			try
			{
				SingleInstanceFindOnly<MachineAutosaveController>.Instance.VersionMachine(fileInfo.Directory.FullName, text);
			}
			catch (Exception exception)
			{
				SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(BackupFailedMessage, 5f);
				Debug.LogException(exception);
			}
		}
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
			OnFileSaved(mode, FileSaveResult.Success);
		}
		catch (Exception exception2)
		{
			SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(SaveErrorMessage, 5f);
			Debug.LogException(exception2);
			OnFileSaved(mode, FileSaveResult.Failed);
		}
	}

	private void Save(FileInfo fileInfo)
	{
		Machine machine = Machine.Active();
		MachineInfo machine2 = machine.CreateMachineInfo();
		XmlSaver.Save(machine2, fileInfo.Directory.FullName);
		machine.LoadedMachinePath = fileInfo.FullName;
		CreateThumbnail(fileInfo, false);
	}

	private void SaveSelection(FileInfo fileInfo)
	{
		Machine machine = Machine.Active();
		MachineInfo machineInfo = machine.CreateMachineInfo(false);
		BlockInfo blockInfo = null;
		List<BuildNodeBlock> list = new List<BuildNodeBlock>();
		List<BuildEdgeBlock> list2 = new List<BuildEdgeBlock>();
		List<BlockInfo> list3 = new List<BlockInfo>();
		foreach (BlockBehaviour item4 in AdvancedBlockEditor.Instance.selectionController.MachineSelection)
		{
			BlockInfo blockInfo2 = BlockInfo.FromBlockBehaviour(item4);
			switch (blockInfo2.ID)
			{
			case BlockType.StartingBlock:
				if (blockInfo == null)
				{
					blockInfo = blockInfo2;
				}
				else
				{
					list3.Add(blockInfo2);
				}
				break;
			case BlockType.BuildSurface:
			{
				BuildSurface buildSurface = item4 as BuildSurface;
				list.AddRange(buildSurface.nodes);
				list2.AddRange(buildSurface.edges);
				list3.Add(blockInfo2);
				break;
			}
			default:
				list3.Add(blockInfo2);
				break;
			case BlockType.BuildNode:
			case BlockType.BuildEdge:
				break;
			}
		}
		if (blockInfo == null && list3.Count == 0)
		{
			SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(SelectionNotValidMessage, 5f);
			return;
		}
		if (blockInfo == null)
		{
			BlockInfo blockInfo3 = new BlockInfo();
			blockInfo3.ID = BlockType.StartingBlock;
			BlockInfo item = blockInfo3;
			machineInfo.Blocks.Add(item);
			machineInfo.MachineData.Write("SavedWithoutStartingBlock", true);
		}
		else
		{
			machineInfo.Blocks.Add(blockInfo);
		}
		foreach (BuildNodeBlock item5 in list)
		{
			BlockInfo item2 = BlockInfo.FromBlockBehaviour(item5);
			machineInfo.Blocks.Add(item2);
		}
		foreach (BuildEdgeBlock item6 in list2)
		{
			BlockInfo item3 = BlockInfo.FromBlockBehaviour(item6);
			machineInfo.Blocks.Add(item3);
		}
		machineInfo.Blocks.AddRange(list3);
		CreateThumbnail(fileInfo, false, true);
		XmlSaver.Save(machineInfo, fileInfo.Directory.FullName);
	}

	protected override void UploadFile(IVirtualObject virtualObject)
	{
		string path = virtualObject.ObjectPath.Path;
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
		string path2 = virtualObject.ThumbnailPath.Path;
		view.OpenUploadDialog(WorkshopManager.ItemTypes.Machines, false, path, fileNameWithoutExtension, path2);
	}

	public override bool ShowAdditiveOrSelectionOnlyButton(bool isSaveMenu)
	{
		return OptionsMaster.BesiegeConfig.AdvancedBuilding && !FileBrowserView.saveMenuUpload && (!isSaveMenu || (AdvancedBlockEditor.Instance.SelectionCount > 0 && StatMaster.Mode.selectedTool != StatMaster.Tool.Modify));
	}

	protected override void UploadFolder(IVirtualObject virtualObject)
	{
		throw new NotImplementedException();
	}
}
