using System;
using System.IO;
using UnityEngine;

namespace Dreamteck
{
	public static class ResourceUtility
	{
		public static string FindFolder(string dir, string folderPattern)
		{
			if (folderPattern.StartsWith("/"))
			{
				folderPattern = folderPattern.Substring(1);
			}
			if (!dir.EndsWith("/"))
			{
				dir += "/";
			}
			if (folderPattern == "")
			{
				return "";
			}
			string[] array = folderPattern.Split('/');
			if (array.Length == 0)
			{
				return "";
			}
			string text = "";
			try
			{
				string[] directories = Directory.GetDirectories(dir);
				foreach (string text2 in directories)
				{
					if (new DirectoryInfo(text2).Name == array[0])
					{
						text = text2;
						string text3 = FindFolder(text2, string.Join("/", array, 1, array.Length - 1));
						if (text3 != "")
						{
							text = text3;
							break;
						}
					}
				}
				if (text == "")
				{
					directories = Directory.GetDirectories(dir);
					for (int i = 0; i < directories.Length; i++)
					{
						text = FindFolder(directories[i], string.Join("/", array));
						if (text != "")
						{
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
				return "";
			}
			return text;
		}

		public static Texture2D LoadTexture(string dreamteckPath, string textureFileName)
		{
			string text = Application.dataPath + "/Dreamteck/" + dreamteckPath;
			if (!Directory.Exists(text))
			{
				text = FindFolder(Application.dataPath, "Dreamteck/" + dreamteckPath);
				if (!Directory.Exists(text))
				{
					return null;
				}
			}
			if (!File.Exists(text + "/" + textureFileName))
			{
				return null;
			}
			byte[] data = File.ReadAllBytes(text + "/" + textureFileName);
			Texture2D obj = new Texture2D(1, 1)
			{
				name = textureFileName
			};
			obj.LoadImage(data);
			return obj;
		}

		public static Texture2D LoadTexture(string path)
		{
			if (!File.Exists(path))
			{
				return null;
			}
			byte[] data = File.ReadAllBytes(path);
			Texture2D texture2D = new Texture2D(1, 1);
			FileInfo fileInfo = new FileInfo(path);
			texture2D.name = fileInfo.Name;
			texture2D.LoadImage(data);
			return texture2D;
		}
	}
}
