using System;
using System.Collections.Generic;
using BesiegeDlc;
using Steamworks;
using UnityEngine;

public class FileBrowserPageViewTest : MonoBehaviour
{
	[SerializeField]
	private FileBrowserView view;

	[SerializeField]
	private FileBrowserPageView pageView;

	[SerializeField]
	private int amountOfFiles = 10;

	[SerializeField]
	private WorkshopType workshopType;

	private void Start()
	{
		Generate();
	}

	public static List<IVirtualObject> GenerateVirtualFiles(FileBrowserType fileBrowserType, int amountOfObjects)
	{
		List<IVirtualObject> list = new List<IVirtualObject>();
		VirtualFile item = GenerateTestFileWithLongName(fileBrowserType);
		list.Add(item);
		int num = 0;
		for (int i = 0; i < amountOfObjects - 1; i++)
		{
			int num2 = UnityEngine.Random.Range(0, 10);
			IVirtualObject item2 = ((num2 % 2 != 0 || num++ >= 5) ? ((IVirtualObject)GenerateTestFile(fileBrowserType)) : ((IVirtualObject)GenerateTestFolder()));
			list.Add(item2);
		}
		return list;
	}

	private void Generate()
	{
		string text = "SavedMachines";
		string text2 = FileSystemPath.DirectorySeparator + text + FileSystemPath.DirectorySeparator;
		List<IVirtualObject> list = GenerateVirtualFiles(FileBrowserType.LocalMachines, amountOfFiles);
		FileSystemPath path = FileSystemPath.Parse(text2);
		FileSystemPath thumbnailPath = FileSystemPath.Parse(text2 + text + ".png");
		VirtualFolder virtualFolder = new VirtualFolder(path, thumbnailPath);
		virtualFolder.AddRange(list);
		pageView.Initialize(view, list, workshopType);
	}

	private static VirtualFile GenerateTestFileWithLongName(FileBrowserType fileBrowserType)
	{
		string fileName = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
		string fileExtension = GetFileExtension(fileBrowserType);
		return GenerateTestFile(fileBrowserType, fileName, fileExtension);
	}

	private static VirtualFile GenerateTestFile(FileBrowserType fileBrowserType)
	{
		string randomName = ScoreboardTester.GetRandomName();
		string fileExtension = GetFileExtension(fileBrowserType);
		return GenerateTestFile(fileBrowserType, randomName, fileExtension);
	}

	private static string GetFileExtension(FileBrowserType fileBrowserType)
	{
		return (!Enum.GetName(typeof(FileBrowserType), fileBrowserType).Contains("Machine")) ? ".blv" : ".bsg";
	}

	private static VirtualFolder GenerateTestFolder()
	{
		string randomName = ScoreboardTester.GetRandomName();
		return GenerateTestFolder(randomName);
	}

	private static bool IsWorkshopType(FileBrowserType type)
	{
		return type == FileBrowserType.SteamLevels || type == FileBrowserType.SteamMachines || type == FileBrowserType.ModIOLevels || type == FileBrowserType.ModIOMachines;
	}

	private static VirtualFile GenerateTestFile(FileBrowserType type, string fileName, string extension)
	{
		FileSystemPath path = FileSystemPath.Parse(FileSystemPath.DirectorySeparator + fileName + extension);
		FileSystemPath thumbnailPath = FileSystemPath.Parse(FileSystemPath.DirectorySeparator + "Thumbnails" + FileSystemPath.DirectorySeparator + fileName + ".png");
		if (IsWorkshopType(type))
		{
			WorkshopFile workshopFile = new WorkshopFile(path, thumbnailPath);
			workshopFile.WorkshopItemId = (ulong)UnityEngine.Random.Range(1, 9999);
			workshopFile.IsInstalled = true;
			workshopFile.IsPublishedItem = false;
			workshopFile.IsOwner = true;
			if (SteamManager.Initialized)
			{
				workshopFile.Author = SteamUser.GetSteamID().m_SteamID;
			}
			workshopFile.DlcDependencyMask = GetRandomDependencyMask();
			List<uint> missingDlcs = DlcManager.Instance.GetMissingDlcs(workshopFile.DlcDependencyMask);
			workshopFile.AreDlcRequirementsMet = missingDlcs.Count == 0;
			workshopFile.Date = DateTime.Now.ToFileTimeUtc();
			workshopFile.IsUploadable = false;
			return workshopFile;
		}
		return new VirtualFile(path, thumbnailPath);
	}

	private static uint GetRandomDependencyMask()
	{
		uint num = 0u;
		if (UnityEngine.Random.Range(0, 1000) % 2 == 0)
		{
			num |= 1;
			if (UnityEngine.Random.Range(0, 1000) % 2 == 0)
			{
				num |= 2;
			}
			if (UnityEngine.Random.Range(0, 99999) % 2 == 0)
			{
				num |= 4;
			}
		}
		return num;
	}

	private static VirtualFolder GenerateTestFolder(string folderName)
	{
		FileSystemPath path = FileSystemPath.Parse(FileSystemPath.DirectorySeparator + folderName);
		FileSystemPath thumbnailPath = FileSystemPath.Parse(FileSystemPath.DirectorySeparator + "Thumbnails" + FileSystemPath.DirectorySeparator + folderName + ".png");
		return new VirtualFolder(path, thumbnailPath);
	}
}
