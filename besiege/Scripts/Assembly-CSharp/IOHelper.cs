using System;
using System.IO;
using UnityEngine;

public static class IOHelper
{
	public static void DeleteDirectory(string path)
	{
		if (!Directory.Exists(path))
		{
			return;
		}
		string[] directories = Directory.GetDirectories(path);
		foreach (string path2 in directories)
		{
			DeleteDirectory(path2);
		}
		try
		{
			Directory.Delete(path, true);
		}
		catch (IOException)
		{
			Directory.Delete(path, true);
		}
		catch (UnauthorizedAccessException)
		{
			Directory.Delete(path, true);
		}
		catch (Exception)
		{
		}
	}

	public static void CopyFolderOverwrite(DirectoryInfo source, DirectoryInfo destination)
	{
		if ((!(source.FullName.ToLower() == destination.FullName.ToLower()) || Application.platform == RuntimePlatform.LinuxPlayer) && (!(source.FullName == destination.FullName) || Application.platform != RuntimePlatform.LinuxPlayer))
		{
			if (Directory.Exists(destination.FullName))
			{
				DeleteDirectory(destination.FullName);
			}
			Directory.CreateDirectory(destination.FullName);
			FileInfo[] files = source.GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				fileInfo.CopyTo(Path.Combine(destination.ToString(), fileInfo.Name), true);
			}
			DirectoryInfo[] directories = source.GetDirectories();
			foreach (DirectoryInfo directoryInfo in directories)
			{
				DirectoryInfo destination2 = destination.CreateSubdirectory(directoryInfo.Name);
				CopyFolderOverwrite(directoryInfo, destination2);
			}
		}
	}

	public static void SaveTexture(Texture2D targetTexture, string filePath)
	{
		int width = targetTexture.width;
		int height = targetTexture.height;
		RenderTexture active = RenderTexture.active;
		RenderTexture renderTexture = new RenderTexture(width, height, 0);
		Graphics.Blit(targetTexture, renderTexture);
		RenderTexture.active = renderTexture;
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGBA32, false);
		texture2D.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		texture2D.Apply();
		RenderTexture.active = active;
		byte[] bytes = texture2D.EncodeToPNG();
		File.WriteAllBytes(filePath, bytes);
	}

	public static bool FileExists(string filePath)
	{
		if (string.IsNullOrEmpty(filePath))
		{
			return false;
		}
		return File.Exists(filePath);
	}

	public static bool FolderExists(string folderPath)
	{
		if (string.IsNullOrEmpty(folderPath))
		{
			return false;
		}
		return Directory.Exists(folderPath);
	}

	public static bool FileOrFolderExists(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return false;
		}
		return File.Exists(path) || Directory.Exists(path);
	}
}
