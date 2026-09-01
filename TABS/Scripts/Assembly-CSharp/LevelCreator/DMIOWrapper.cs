using System;
using System.Collections.Generic;
using TFBGames;

namespace LevelCreator
{
	public static class DMIOWrapper
	{
		public static class File
		{
			public static void Exists(string path, FileHandlingFileType fileType, Action<bool> doneCallback)
			{
				FileIO.FileExists(path, fileType, doneCallback);
			}

			public static void Delete(string path, FileHandlingFileType fileType, Action<Exception> doneCallback)
			{
				FileIO.DeleteFile(path, fileType, doneCallback);
			}

			public static void ReadAllLines(string path, FileHandlingFileType fileType, Action<string[]> doneCallback)
			{
				FileIO.ReadAllText(path, fileType, delegate(string text, Exception e)
				{
					doneCallback?.Invoke(text?.Split(','));
				});
			}

			public static void WriteAllLines(string path, IEnumerable<string> contents, FileHandlingFileType fileType, Action<Exception> doneCallback)
			{
				FileIO.WriteAllText(path, string.Join(",", contents), fileType, doneCallback);
			}

			public static void ReadAllBytes(string path, FileHandlingFileType fileType, Action<byte[], Exception> doneCallback)
			{
				FileIO.ReadAllBytes(path, fileType, doneCallback);
			}

			public static void WriteAllBytes(string path, byte[] bytes, FileHandlingFileType fileType, Action<Exception> doneCallback)
			{
				FileIO.WriteAllBytes(path, bytes, fileType, doneCallback);
			}
		}

		public static class Directory
		{
			public static void Exists(string path, FileHandlingFileType fileType, Action<bool> doneCallback)
			{
				FileIO.DirectoryExists(path, fileType, doneCallback);
			}

			public static void CreateDirectory(string path, FileHandlingFileType fileType, Action<Exception> doneCallback)
			{
				FileIO.CreateDirectory(path, fileType, doneCallback);
			}

			public static void EnsureDirectoryExists(string path, FileHandlingFileType fileType, Action<Exception> doneCallback)
			{
				FileIO.DirectoryExists(path, fileType, delegate(bool exists)
				{
					if (!exists)
					{
						CreateDirectory(path, fileType, doneCallback);
					}
					else
					{
						doneCallback?.Invoke(null);
					}
				});
			}

			public static void GetDirectories(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback)
			{
				FileIO.GetDirectories(path, fileType, doneCallback);
			}

			public static void GetFiles(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback)
			{
				FileIO.GetFiles(path, fileType, doneCallback);
			}

			public static void GetFilesRecursive(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback)
			{
				List<string> files = new List<string>();
				int getDirectoriesAsyncCount = 0;
				int getFilesAsyncCount = 0;
				AddDirectories(path);
				void AddDirectories(string currentDir)
				{
					getDirectoriesAsyncCount++;
					getFilesAsyncCount++;
					GetFiles(currentDir, fileType, delegate(string[] result, Exception e)
					{
						if (result != null && result.Length != 0)
						{
							files.AddRange(result);
						}
						getFilesAsyncCount--;
						if (getDirectoriesAsyncCount <= 0 && getFilesAsyncCount <= 0)
						{
							doneCallback?.Invoke(files.ToArray(), null);
						}
					});
					GetDirectories(currentDir, fileType, delegate(string[] dirs, Exception e)
					{
						if (dirs != null && dirs.Length != 0)
						{
							foreach (string currentDir2 in dirs)
							{
								AddDirectories(currentDir2);
							}
						}
						getDirectoriesAsyncCount--;
						if (getDirectoriesAsyncCount <= 0 && getFilesAsyncCount <= 0)
						{
							doneCallback?.Invoke(files.ToArray(), null);
						}
					});
				}
			}
		}

		private static FileIOWrapper fileIOInstance;

		private static FileIOWrapper FileIO
		{
			get
			{
				if (fileIOInstance == null)
				{
					fileIOInstance = ServiceLocator.GetService<FileIOWrapper>();
				}
				return fileIOInstance;
			}
		}
	}
}
