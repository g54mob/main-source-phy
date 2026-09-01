using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DM;
using Landfall.TABS;
using TFBGames;
using UnityEngine;

namespace LevelCreator
{
	public static class LevelUtility
	{
		public static void GetTemplateLevels(Action<string[], Exception> doneCallback)
		{
			DMIOWrapper.Directory.Exists(Paths.TemplateDirectory, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate(bool exists)
			{
				if (!exists)
				{
					doneCallback?.Invoke(new string[0], null);
				}
				else
				{
					DMIOWrapper.Directory.GetFilesRecursive(Paths.TemplateDirectory, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate(string[] files, Exception e)
					{
						doneCallback?.Invoke(files.Where((string s) => s.EndsWith(".tld")).ToArray(), e);
					});
				}
			});
		}

		public static void GetPlayerLevels(Action<string[], Exception> doneCallback)
		{
			DMIOWrapper.Directory.Exists(Paths.PlayerLevelDirectory, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					doneCallback?.Invoke(new string[0], null);
				}
				else
				{
					DMIOWrapper.Directory.GetFiles(Paths.PlayerLevelDirectory, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] files, Exception e)
					{
						doneCallback?.Invoke(files.Where((string s) => s.EndsWith(".tld")).ToArray(), e);
					});
				}
			});
		}

		public static string GetLevelThumbnail(string levelPath)
		{
			levelPath = levelPath.Remove(levelPath.Length - 3, 3);
			return levelPath + "png";
		}

		public static void WithRecentLevelPaths(Action<IEnumerable<string>> doneCallback)
		{
			DMIOWrapper.File.Exists(Paths.RecentLevelsFile, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					doneCallback?.Invoke(Enumerable.Empty<string>());
				}
				else
				{
					DMIOWrapper.File.ReadAllLines(Paths.RecentLevelsFile, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] lines)
					{
						Action<IEnumerable<string>> action = doneCallback;
						if (action != null)
						{
							action(lines ?? Enumerable.Empty<string>());
						}
					});
				}
			});
		}

		public static void WriteRecentLevelPaths(IEnumerable<string> recentLevelPaths)
		{
			IEnumerable<string> contents = recentLevelPaths.Where((string path) => path.Contains(Paths.PlayerLevelDirectoryName)).Take(100);
			DMIOWrapper.File.WriteAllLines(Paths.RecentLevelsFile, contents, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
		}

		public static void AddRecentLevel(string path)
		{
			WithRecentLevelPaths(delegate(IEnumerable<string> recentLevelPaths)
			{
				IEnumerable<string> second = recentLevelPaths.Where((string recentLevelPath) => recentLevelPath != path);
				WriteRecentLevelPaths(new string[1] { path }.Concat(second));
			});
		}

		public static void RemoveRecentLevel(string path)
		{
			WithRecentLevelPaths(delegate(IEnumerable<string> recentLevelPaths)
			{
				WriteRecentLevelPaths(recentLevelPaths.Where((string recentLevelPath) => recentLevelPath != path));
			});
		}

		public static string GetLevelName(string path)
		{
			FileInfo fileInfo = new FileInfo(path);
			string result = fileInfo.Name;
			string extension = fileInfo.Extension;
			if (!string.IsNullOrEmpty(extension))
			{
				result = fileInfo.Name.Replace(extension, "");
			}
			return result;
		}

		public static CustomMap GetCustomMapFromLevelPath(string path)
		{
			string levelName = GetLevelName(path);
			levelName = levelName.Substring(2, levelName.Length - 2);
			if (int.TryParse(levelName, out var result))
			{
				DatabaseID id = new DatabaseID(result);
				CustomMap userMap = ContentDatabase.Instance().GetUserMap(id);
				if (userMap == null)
				{
					userMap = ContentDatabase.Instance().GetUserMap(new DatabaseID(0, id.m_ID));
				}
				return userMap;
			}
			Debug.LogError("Could not parse: levelID: " + levelName + "\n" + path);
			return null;
		}
	}
}
