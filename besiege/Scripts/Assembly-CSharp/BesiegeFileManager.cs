using System.IO;
using UnityEngine;

public static class BesiegeFileManager
{
	public enum FileLocation
	{
		Data = 0,
		PersistentData = 1
	}

	public static bool Save(string fileName, FileLocation location, byte[] data)
	{
		if (OptionsMaster.isSandboxed)
		{
			return false;
		}
		try
		{
			string path = Path.Combine(GetPath(location), fileName);
			File.WriteAllBytes(path, data);
		}
		catch
		{
			Debug.LogError("Missing path permission");
			return false;
		}
		return true;
	}

	public static bool Exists(string fileName, FileLocation location)
	{
		if (OptionsMaster.isSandboxed)
		{
			return false;
		}
		return File.Exists(Path.Combine(GetPath(location), fileName));
	}

	public static bool GetFiles(FileLocation location, out string[] files)
	{
		files = null;
		try
		{
			string path = GetPath(location);
			if (OptionsMaster.isSandboxed || !Directory.Exists(path))
			{
				return false;
			}
			string[] files2 = Directory.GetFiles(path);
			files = new string[files2.Length];
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = Path.GetFileName(files2[i]);
			}
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static bool Load(string fileName, FileLocation location, out byte[] data)
	{
		data = null;
		try
		{
			string path = Path.Combine(GetPath(location), fileName);
			if (OptionsMaster.isSandboxed || !File.Exists(path))
			{
				return false;
			}
			data = File.ReadAllBytes(path);
			return true;
		}
		catch
		{
			return false;
		}
	}

	private static string GetPath(FileLocation location)
	{
		if (location == FileLocation.PersistentData)
		{
			return Application.persistentDataPath;
		}
		return StaticSettings.DataPath;
	}
}
