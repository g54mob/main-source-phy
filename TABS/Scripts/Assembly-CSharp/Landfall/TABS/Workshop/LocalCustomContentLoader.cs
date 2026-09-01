using System;
using System.Collections.Generic;
using System.IO;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.Workshop
{
	public class LocalCustomContentLoader
	{
		private readonly List<string> layoutExtensions = new List<string>(new string[2]
		{
			CustomContentFilePaths.FileEndingBattle,
			CustomContentFilePaths.FileEndingLayout
		});

		private readonly List<string> campaignExtensions = new List<string>(new string[1] { CustomContentFilePaths.FileEndingCampaign });

		private readonly List<string> unitExtensions = new List<string>(new string[1] { CustomContentFilePaths.FileEndingUnit });

		private readonly List<string> factionExtensions = new List<string>(new string[1] { CustomContentFilePaths.FileEndingFaction });

		private readonly List<string> customMapExtensions = new List<string>(new string[1] { CustomContentFilePaths.FileEndingCustomMap });

		public Dictionary<WorkshopContentType, List<FileInfo>> CustomContentFiles { get; private set; }

		private static void MoveOldFiles(string oldPath, string newPath)
		{
			try
			{
				if (!Directory.Exists(oldPath))
				{
					return;
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(oldPath);
				if (directoryInfo.Exists)
				{
					DirectoryInfo[] directories = directoryInfo.GetDirectories();
					foreach (DirectoryInfo directoryInfo2 in directories)
					{
						string text = Path.Combine(newPath, directoryInfo2.Name);
						Directory.CreateDirectory(newPath);
						if (!Directory.Exists(text))
						{
							Directory.Move(directoryInfo2.FullName, text);
						}
					}
				}
				try
				{
					Directory.Delete(oldPath, recursive: true);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.Message);
				}
			}
			catch (Exception ex2)
			{
				Debug.LogError(ex2.Message);
			}
		}

		public LocalCustomContentLoader()
		{
			CustomContentFiles = new Dictionary<WorkshopContentType, List<FileInfo>>();
		}

		public void SearchForLocalCustomContent(bool expectingNewContent = false, WorkshopContentType type = WorkshopContentType.Any, Action doneCallback = null)
		{
			List<Action<Action>> list = new List<Action<Action>>(4);
			if (type == WorkshopContentType.Any || type == WorkshopContentType.Unit)
			{
				list.Add(SearchForCustomLocalUnits);
			}
			if (type == WorkshopContentType.Any || type == WorkshopContentType.Faction)
			{
				list.Add(SearchForCustomLocalFactions);
			}
			if (type == WorkshopContentType.Any || type == WorkshopContentType.Battle || type == WorkshopContentType.Layout)
			{
				list.Add(SearchForCustomLocalLayouts);
			}
			if (type == WorkshopContentType.Any || type == WorkshopContentType.Campaign)
			{
				list.Add(SearchForCustomLocalCampaigns);
			}
			if (type == WorkshopContentType.Any || type == WorkshopContentType.Map)
			{
				list.Add(SearchForCustomLocalMaps);
			}
			int count = list.Count;
			if (count <= 0)
			{
				doneCallback?.Invoke();
			}
			AsyncCounter asyncCounter = new AsyncCounter(count);
			for (int i = 0; i < count; i++)
			{
				AsyncCounter tempCounter = asyncCounter;
				list[i](delegate
				{
					if (tempCounter.OnAsyncDone())
					{
						doneCallback?.Invoke();
					}
				});
			}
		}

		private void SearchForCustomLocalCampaigns(Action doneCallback)
		{
			MoveOldFiles(CustomContentFilePaths.OldFilePathCampaign, CustomContentFilePaths.FilePathCampaign);
			string filePathCampaign = CustomContentFilePaths.FilePathCampaign;
			FindCustomFiles(filePathCampaign, campaignExtensions, delegate(List<FileInfo> files)
			{
				if (files == null)
				{
					doneCallback?.Invoke();
				}
				else
				{
					if (!CustomContentFiles.ContainsKey(WorkshopContentType.Campaign))
					{
						CustomContentFiles.Add(WorkshopContentType.Campaign, files);
					}
					else
					{
						CustomContentFiles[WorkshopContentType.Campaign] = files;
					}
					doneCallback?.Invoke();
				}
			});
		}

		private void SearchForCustomLocalLayouts(Action doneCallback)
		{
			MoveOldFiles(CustomContentFilePaths.OldFilePathLayout, CustomContentFilePaths.FilePathLayout);
			string filePathLayout = CustomContentFilePaths.FilePathLayout;
			FindCustomFiles(filePathLayout, layoutExtensions, delegate(List<FileInfo> files)
			{
				if (files == null)
				{
					doneCallback?.Invoke();
				}
				else
				{
					if (!CustomContentFiles.ContainsKey(WorkshopContentType.Battle))
					{
						CustomContentFiles.Add(WorkshopContentType.Battle, files);
					}
					else
					{
						CustomContentFiles[WorkshopContentType.Battle] = files;
					}
					doneCallback?.Invoke();
				}
			});
		}

		private void SearchForCustomLocalUnits(Action doneCallback)
		{
			string unitDirectoryPath = CustomContentFilePaths.UnitDirectoryPath;
			FindCustomFiles(unitDirectoryPath, unitExtensions, delegate(List<FileInfo> files)
			{
				if (files == null)
				{
					doneCallback?.Invoke();
				}
				else
				{
					if (!CustomContentFiles.ContainsKey(WorkshopContentType.Unit))
					{
						CustomContentFiles.Add(WorkshopContentType.Unit, files);
					}
					else
					{
						CustomContentFiles[WorkshopContentType.Unit] = files;
					}
					doneCallback?.Invoke();
				}
			});
		}

		private void SearchForCustomLocalFactions(Action doneCallback)
		{
			string filePathFaction = CustomContentFilePaths.FilePathFaction;
			FindCustomFiles(filePathFaction, factionExtensions, delegate(List<FileInfo> files)
			{
				if (files == null)
				{
					doneCallback?.Invoke();
				}
				else
				{
					if (!CustomContentFiles.ContainsKey(WorkshopContentType.Faction))
					{
						CustomContentFiles.Add(WorkshopContentType.Faction, files);
					}
					else
					{
						CustomContentFiles[WorkshopContentType.Faction] = files;
					}
					doneCallback?.Invoke();
				}
			});
		}

		private void SearchForCustomLocalMaps(Action doneCallback)
		{
			string filePathCustomMap = CustomContentFilePaths.FilePathCustomMap;
			FindCustomFiles(filePathCustomMap, customMapExtensions, delegate(List<FileInfo> files)
			{
				if (files == null)
				{
					doneCallback?.Invoke();
				}
				else
				{
					if (!CustomContentFiles.ContainsKey(WorkshopContentType.Map))
					{
						CustomContentFiles.Add(WorkshopContentType.Map, files);
					}
					else
					{
						CustomContentFiles[WorkshopContentType.Map] = files;
					}
					doneCallback?.Invoke();
				}
			});
		}

		private void FindCustomFiles(string rootPath, List<string> fileExtensions, Action<List<FileInfo>> doneCallback)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			fileIO.DirectoryExists(rootPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					doneCallback?.Invoke(null);
				}
				else
				{
					FindCustomFilesOnDirectoryExists(rootPath, fileExtensions, fileIO, doneCallback);
				}
			});
		}

		private void FindCustomFilesOnDirectoryExists(string rootPath, List<string> fileExtensions, FileIOWrapper fileIO, Action<List<FileInfo>> doneCallback)
		{
			List<FileInfo> filesInside = new List<FileInfo>();
			fileIO.GetDirectories(rootPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] directories, Exception exception)
			{
				if (directories == null || directories.Length == 0)
				{
					doneCallback?.Invoke(filesInside);
				}
				else
				{
					FindCustomFilesOnFoundSubFolders(fileExtensions, fileIO, filesInside, directories, doneCallback);
				}
			});
		}

		private void FindCustomFilesOnFoundSubFolders(List<string> fileExtensions, FileIOWrapper fileIO, List<FileInfo> filesInside, string[] directories, Action<List<FileInfo>> doneCallback)
		{
			int num = directories.Length;
			AsyncCounter counter = new AsyncCounter(num);
			for (int i = 0; i < num; i++)
			{
				string path = directories[i];
				fileIO.GetFiles(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] files, Exception exception)
				{
					if (files != null && files.Length != 0)
					{
						int j = 0;
						for (int num2 = files.Length; j < num2; j++)
						{
							string text = files[j];
							if (!string.IsNullOrEmpty(text))
							{
								string extension = Path.GetExtension(text);
								if (fileExtensions.Contains(extension))
								{
									filesInside.Add(new FileInfo(text));
								}
							}
						}
					}
					if (counter.OnAsyncDone())
					{
						doneCallback?.Invoke(filesInside);
					}
				});
			}
		}
	}
}
