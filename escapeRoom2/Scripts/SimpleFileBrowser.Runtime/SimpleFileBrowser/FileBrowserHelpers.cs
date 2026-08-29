using System;
using System.IO;
using UnityEngine;

namespace SimpleFileBrowser
{
	public static class FileBrowserHelpers
	{
		public static bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public static bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public static bool IsDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				return true;
			}
			if (File.Exists(path))
			{
				return false;
			}
			string extension = Path.GetExtension(path);
			if (extension != null)
			{
				return extension.Length <= 1;
			}
			return true;
		}

		public static bool IsPathDescendantOfAnother(string path, string parentFolderPath)
		{
			path = Path.GetFullPath(path).Replace('\\', '/');
			parentFolderPath = Path.GetFullPath(parentFolderPath).Replace('\\', '/');
			if (path == parentFolderPath)
			{
				return false;
			}
			if (parentFolderPath[parentFolderPath.Length - 1] != '/')
			{
				parentFolderPath += "/";
			}
			if (path != parentFolderPath)
			{
				return path.StartsWith(parentFolderPath, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		public static string GetDirectoryName(string path)
		{
			return Path.GetDirectoryName(path);
		}

		public static FileSystemEntry[] GetEntriesInDirectory(string path, bool extractOnlyLastSuffixFromExtensions)
		{
			try
			{
				string[] files = Directory.GetFiles(path);
				string[] directories = Directory.GetDirectories(path);
				FileSystemEntry[] array = new FileSystemEntry[files.Length + directories.Length];
				int num = 0;
				for (int i = 0; i < files.Length; i++)
				{
					try
					{
						FileInfo fileInfo = new FileInfo(files[i]);
						array[num] = new FileSystemEntry(fileInfo, FileBrowser.GetExtensionFromFilename(fileInfo.Name, extractOnlyLastSuffixFromExtensions));
						num++;
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
					}
				}
				for (int j = 0; j < directories.Length; j++)
				{
					try
					{
						array[num] = new FileSystemEntry(new DirectoryInfo(directories[j]), string.Empty);
						num++;
					}
					catch (Exception exception2)
					{
						Debug.LogException(exception2);
					}
				}
				if (array.Length != num)
				{
					Array.Resize(ref array, num);
				}
				return array;
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (Exception exception3)
			{
				Debug.LogException(exception3);
			}
			return null;
		}

		public static string CreateFileInDirectory(string directoryPath, string filename)
		{
			string text = Path.Combine(directoryPath, filename);
			using (File.Create(text))
			{
				return text;
			}
		}

		public static string CreateFolderInDirectory(string directoryPath, string folderName)
		{
			string text = Path.Combine(directoryPath, folderName);
			Directory.CreateDirectory(text);
			return text;
		}

		public static void WriteBytesToFile(string targetPath, byte[] bytes)
		{
			File.WriteAllBytes(targetPath, bytes);
		}

		public static void WriteTextToFile(string targetPath, string text)
		{
			File.WriteAllText(targetPath, text);
		}

		public static void AppendBytesToFile(string targetPath, byte[] bytes)
		{
			using FileStream fileStream = new FileStream(targetPath, FileMode.Append, FileAccess.Write);
			fileStream.Write(bytes, 0, bytes.Length);
		}

		public static void AppendTextToFile(string targetPath, string text)
		{
			File.AppendAllText(targetPath, text);
		}

		private static void AppendFileToFile(string targetPath, string sourceFileToAppend)
		{
			using Stream stream = File.OpenRead(sourceFileToAppend);
			using Stream stream2 = new FileStream(targetPath, FileMode.Append, FileAccess.Write);
			byte[] array = new byte[4096];
			int count;
			while ((count = stream.Read(array, 0, array.Length)) > 0)
			{
				stream2.Write(array, 0, count);
			}
		}

		public static byte[] ReadBytesFromFile(string sourcePath)
		{
			return File.ReadAllBytes(sourcePath);
		}

		public static string ReadTextFromFile(string sourcePath)
		{
			return File.ReadAllText(sourcePath);
		}

		public static void CopyFile(string sourcePath, string destinationPath)
		{
			File.Copy(sourcePath, destinationPath, overwrite: true);
		}

		public static void CopyDirectory(string sourcePath, string destinationPath)
		{
			CopyDirectoryRecursively(new DirectoryInfo(sourcePath), destinationPath);
		}

		private static void CopyDirectoryRecursively(DirectoryInfo sourceDirectory, string destinationPath)
		{
			Directory.CreateDirectory(destinationPath);
			FileInfo[] files = sourceDirectory.GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].CopyTo(Path.Combine(destinationPath, files[i].Name), overwrite: true);
			}
			DirectoryInfo[] directories = sourceDirectory.GetDirectories();
			for (int j = 0; j < directories.Length; j++)
			{
				CopyDirectoryRecursively(directories[j], Path.Combine(destinationPath, directories[j].Name));
			}
		}

		public static void MoveFile(string sourcePath, string destinationPath)
		{
			File.Move(sourcePath, destinationPath);
		}

		public static void MoveDirectory(string sourcePath, string destinationPath)
		{
			Directory.Move(sourcePath, destinationPath);
		}

		public static string RenameFile(string path, string newName)
		{
			string text = Path.Combine(Path.GetDirectoryName(path), newName);
			File.Move(path, text);
			return text;
		}

		public static string RenameDirectory(string path, string newName)
		{
			string text = Path.Combine(new DirectoryInfo(path).Parent.FullName, newName);
			Directory.Move(path, text);
			return text;
		}

		public static void DeleteFile(string path)
		{
			File.Delete(path);
		}

		public static void DeleteDirectory(string path)
		{
			Directory.Delete(path, recursive: true);
		}

		public static string GetFilename(string path)
		{
			return Path.GetFileName(path);
		}

		public static long GetFilesize(string path)
		{
			return new FileInfo(path).Length;
		}

		public static DateTime GetLastModifiedDate(string path)
		{
			return new FileInfo(path).LastWriteTime;
		}
	}
}
