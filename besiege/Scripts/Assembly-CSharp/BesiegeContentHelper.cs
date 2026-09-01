using System;
using System.Collections.Generic;
using System.IO;
using BesiegeDlc;
using UnityEngine;

public static class BesiegeContentHelper
{
	public static uint GetDlcDependencyMaskFromPath(string path, WorkshopManager.ItemTypes itemType, bool isFolder)
	{
		if (isFolder)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			return GetDlcMaskFromFolder(directoryInfo, itemType);
		}
		FileInfo fileInfo = new FileInfo(path);
		return GetDlcMaskFromFile(fileInfo, itemType);
	}

	private static uint GetDlcMaskFromFile(FileInfo fileInfo, WorkshopManager.ItemTypes itemType)
	{
		DlcManager instance = DlcManager.Instance;
		List<DlcManager.DlcType> dlcTypes;
		switch (itemType)
		{
		case WorkshopManager.ItemTypes.Machines:
		{
			MachineInfo machineInfo = LoadMachineInfoFromPath(fileInfo);
			if (instance.GetMachineInfoDlc(machineInfo, out dlcTypes))
			{
				return GetDlcMaskFromDlcTypes(dlcTypes);
			}
			break;
		}
		case WorkshopManager.ItemTypes.Levels:
			if (LevelXMLLoader.GetDlcTypesFromFile(fileInfo.FullName, out dlcTypes))
			{
				return GetDlcMaskFromDlcTypes(dlcTypes);
			}
			break;
		default:
			throw new NotImplementedException("Can't load any other file type than machines and levels");
		}
		return 0u;
	}

	private static uint GetDlcMaskFromDlcTypes(List<DlcManager.DlcType> dlcTypes)
	{
		uint num = 0u;
		foreach (DlcManager.DlcType dlcType in dlcTypes)
		{
			num |= (uint)dlcType;
		}
		return num;
	}

	private static MachineInfo LoadMachineInfoFromPath(FileInfo file)
	{
		MachineInfo result = null;
		string fullName = file.FullName;
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullName);
		if (!File.Exists(fullName))
		{
			Debug.LogError("Loading machine from missing path: " + fullName);
			return result;
		}
		if (XmlSaver.IsXmlFormat(fullName))
		{
			try
			{
				if (!StatMaster.isHosting)
				{
					Debug.Log("Loading machine in XML format: " + fileNameWithoutExtension);
				}
				result = XmlLoader.LoadFromFullPath(fullName, string.Empty);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
		else if (XmlSaver.IsBsgFormat(fullName))
		{
			if (!StatMaster.isHosting)
			{
				Debug.Log("Loading machine in old format: " + fileNameWithoutExtension);
			}
			string name = StaticSettings.SanatizeFileName(fileNameWithoutExtension);
			result = MachineFormatConverter.ConvertBsgToMachineInfo(name, fullName);
		}
		else
		{
			Debug.LogWarning("Unknown machine format!");
		}
		return result;
	}

	private static uint GetDlcMaskFromFolder(DirectoryInfo directoryInfo, WorkshopManager.ItemTypes itemType)
	{
		if (!directoryInfo.Exists)
		{
			return 0u;
		}
		switch (itemType)
		{
		case WorkshopManager.ItemTypes.Mods:
			return 0u;
		case WorkshopManager.ItemTypes.Skins:
			return 0u;
		default:
		{
			uint num = 0u;
			string contentExtensionFromType = GetContentExtensionFromType(itemType);
			FileInfo[] files = directoryInfo.GetFiles(contentExtensionFromType);
			foreach (FileInfo fileInfo in files)
			{
				num |= GetDlcMaskFromFile(fileInfo, itemType);
			}
			return num;
		}
		}
	}

	private static uint GetDlcMaskFromSkinFolder(DirectoryInfo skinDirectoryInfo)
	{
		uint num = 0u;
		DlcManager instance = DlcManager.Instance;
		DirectoryInfo[] directories = skinDirectoryInfo.GetDirectories();
		foreach (DirectoryInfo directoryInfo in directories)
		{
			string name = directoryInfo.Name;
			int num2 = BlockPrefab.TryParseBlockId(name);
			DlcManager.DlcType dlcType;
			if (num2 != -1 && instance.GetBlockDlcType((BlockType)num2, out dlcType))
			{
				num |= (uint)dlcType;
			}
		}
		return num;
	}

	private static string GetContentExtensionFromType(WorkshopManager.ItemTypes itemType)
	{
		switch (itemType)
		{
		case WorkshopManager.ItemTypes.Levels:
			return ".blv";
		case WorkshopManager.ItemTypes.Machines:
			return ".bsg";
		default:
			return string.Empty;
		}
	}
}
