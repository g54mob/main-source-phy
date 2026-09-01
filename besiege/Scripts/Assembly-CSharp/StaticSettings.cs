using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class StaticSettings
{
	public const int ThumbnailSize = 512;

	private static List<string> existingDirectories = new List<string>();

	private static Regex tagRegex = new Regex("<[^>]*>([^<]+)<\\/[^>]*>", RegexOptions.Compiled);

	private static Regex richRegex = new Regex("\\[[^\\]]*\\]([^\\[]+)\\[\\/[^\\]]*\\]", RegexOptions.Compiled);

	public static string DataPath
	{
		get
		{
			return Application.dataPath.Replace("/", "\\");
		}
	}

	public static string MachinePath
	{
		get
		{
			return GetPath(Path.Combine(DataPath, "SavedMachines"));
		}
	}

	public static string LevelPath
	{
		get
		{
			return GetPath(Path.Combine(DataPath, "CustomLevels"));
		}
	}

	public static string LevelAutosavePath
	{
		get
		{
			return GetPath(Path.Combine(LevelPath, "Autosave"));
		}
	}

	public static string MachineAutosavePath
	{
		get
		{
			return GetPath(Path.Combine(MachinePath, "AutoSave"));
		}
	}

	public static string LevelThumbnailPath
	{
		get
		{
			return GetPath(Path.Combine(LevelPath, "Thumbnails"));
		}
	}

	public static string MachineThumbnailPath
	{
		get
		{
			return GetPath(Path.Combine(MachinePath, "Thumbnails"));
		}
	}

	public static string BsgBackupPath
	{
		get
		{
			return GetPath(Path.Combine(MachinePath, "Conversion Backups"));
		}
	}

	public static string LocalisationPath
	{
		get
		{
			string dataPath = DataPath;
			return GetPath(Path.Combine(dataPath, "Localisation Files"));
		}
	}

	public static CultureInfo Culture
	{
		get
		{
			return new CultureInfo("en-GB");
		}
	}

	public static int BlockSolverIterationCount
	{
		get
		{
			return 30;
		}
	}

	private static string GetPath(string path)
	{
		CreateDir(path);
		return path;
	}

	private static void CreateDir(string path)
	{
		if (!existingDirectories.Contains(path))
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			existingDirectories.Add(path);
		}
	}

	public static string SanatizeFileName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return name;
		}
		string text = Path.GetInvalidFileNameChars().Aggregate(SanatizeString(name), (string current, char c) => current.Replace(c.ToString(), string.Empty));
		text = text.Replace("#", "_");
		text = text.Replace("$", "_");
		text = text.Replace("\\", "_");
		text = text.Replace("/", "_");
		text = text.Replace("<", "_");
		text = text.Replace(">", "_");
		text = text.Replace(":", "_");
		text = text.Replace("\"", "_");
		text = text.Replace("?", "_");
		text = text.Replace("|", "_");
		return text.Replace("*", "_");
	}

	public static string SanatizeString(string str)
	{
		return richRegex.Replace(tagRegex.Replace(str, "$1"), "$1");
	}

	public static double GetTimestamp(DateTime dateTime)
	{
		return (dateTime - GetRefDateTime()).TotalSeconds;
	}

	public static double GetTimestamp(double utcTime)
	{
		return (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(utcTime) - GetRefDateTime()).TotalSeconds;
	}

	private static DateTime GetRefDateTime()
	{
		return new DateTime(2014, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}

	public static string GetThumbnailPath(FileInfo fileInfo)
	{
		string path = Path.Combine(fileInfo.Directory.FullName, "Thumbnails");
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		if (!directoryInfo.Exists)
		{
			try
			{
				directoryInfo.Create();
			}
			catch (IOException)
			{
				return null;
			}
		}
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
		string path2 = fileNameWithoutExtension + ".png";
		return Path.Combine(directoryInfo.FullName, path2);
	}
}
