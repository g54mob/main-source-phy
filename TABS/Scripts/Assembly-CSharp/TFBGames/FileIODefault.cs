using System;
using System.IO;

namespace TFBGames
{
	public class FileIODefault : IFileIOPlatform
	{
		public bool IsReady => true;

		public void CreateDirectory(string path, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			try
			{
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				doneCallback?.Invoke(null);
			}
			catch (Exception obj)
			{
				doneCallback?.Invoke(obj);
			}
		}

		public void DirectoryExists(string path, FileHandlingFileType fileType, Action<bool> doneCallback)
		{
			doneCallback?.Invoke(Directory.Exists(path));
		}

		public void DeleteDirectory(string path, bool recursive, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			try
			{
				Directory.Delete(path, recursive);
				doneCallback?.Invoke(null);
			}
			catch (Exception obj)
			{
				doneCallback?.Invoke(obj);
			}
		}

		public void GetDirectories(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback)
		{
			try
			{
				string[] directories = Directory.GetDirectories(path);
				doneCallback?.Invoke(directories, null);
			}
			catch (Exception arg)
			{
				doneCallback?.Invoke(null, arg);
			}
		}

		public void GetFiles(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback)
		{
			try
			{
				string[] files = Directory.GetFiles(path);
				doneCallback?.Invoke(files, null);
			}
			catch (Exception arg)
			{
				doneCallback?.Invoke(null, arg);
			}
		}

		public void FileExists(string path, FileHandlingFileType fileType, Action<bool> doneCallback)
		{
			doneCallback?.Invoke(File.Exists(path));
		}

		public void DeleteFile(string path, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			try
			{
				File.Delete(path);
				doneCallback?.Invoke(null);
			}
			catch (Exception obj)
			{
				doneCallback?.Invoke(obj);
			}
		}

		public void OpenRead(string path, FileHandlingFileType fileType, Action<Stream, Exception> doneCallback)
		{
			try
			{
				Stream arg = File.Open(path, FileMode.Open);
				doneCallback?.Invoke(arg, null);
			}
			catch (Exception arg2)
			{
				doneCallback?.Invoke(null, arg2);
			}
		}

		public void CloseRead(string path, Stream stream)
		{
			stream?.Dispose();
		}

		public Stream OpenWrite(string path)
		{
			return File.Open(path, FileMode.OpenOrCreate);
		}

		public void CloseWrite(string path, Stream stream, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			stream?.Dispose();
			doneCallback?.Invoke(null);
		}

		public void ReadAllText(string path, FileHandlingFileType fileType, Action<string, Exception> doneCallback)
		{
			try
			{
				doneCallback?.Invoke(File.ReadAllText(path), null);
			}
			catch (Exception arg)
			{
				doneCallback?.Invoke(null, arg);
			}
		}

		public void WriteAllText(string path, string contents, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			try
			{
				File.WriteAllText(path, contents);
				doneCallback?.Invoke(null);
			}
			catch (Exception obj)
			{
				doneCallback?.Invoke(obj);
			}
		}

		public void WaitForAsyncsToFinish(Action doneCallback)
		{
			doneCallback?.Invoke();
		}

		public bool IsBusyWithAsyncs()
		{
			return false;
		}

		public void MoveFile(string sourcePath, string destinationPath, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			try
			{
				File.Move(sourcePath, destinationPath);
				doneCallback?.Invoke(null);
			}
			catch (Exception obj)
			{
				doneCallback?.Invoke(obj);
			}
		}

		public void MoveDirectory(string sourcePath, string destinationPath, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			try
			{
				Directory.Move(sourcePath, destinationPath);
				doneCallback?.Invoke(null);
			}
			catch (Exception obj)
			{
				doneCallback?.Invoke(obj);
			}
		}

		public void GetFilesRecursive(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback)
		{
			try
			{
				string[] files = Directory.GetFiles(path, "", SearchOption.AllDirectories);
				doneCallback?.Invoke(files, null);
			}
			catch (Exception arg)
			{
				doneCallback?.Invoke(null, arg);
			}
		}
	}
}
