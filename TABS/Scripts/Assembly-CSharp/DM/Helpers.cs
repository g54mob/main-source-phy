using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ModIO;
using TFBGames;
using UnityEngine;

namespace DM
{
	public class Helpers
	{
		public static void ReportExceptionIfAny(string operation, string path, Exception maybeException)
		{
			if (maybeException != null)
			{
				Debug.LogErrorFormat("DM: Operation {0} on \"{1}\" failed! Reason: {2}", operation, path, maybeException);
			}
		}

		public static string TrimmedFileContent(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return "*empty*";
			}
			if (data.Length < 100)
			{
				return "(" + data.Length + "B, \"" + data.ToString() + "\")";
			}
			return "(" + data.Length + "B, \"" + Encoding.UTF8.GetString(data.Take(100).ToArray()) + "..\")";
		}

		public static string TryGetHash(byte[] data)
		{
			if (data != null)
			{
				try
				{
					using (MD5 mD = MD5.Create())
					{
						return BitConverter.ToString(mD.ComputeHash(data)).Replace("-", "").ToLowerInvariant();
					}
				}
				catch (Exception e)
				{
					Debug.LogWarning("DM: [mod.io] Failed to calculate hash.\n" + Utility.GenerateExceptionDebugString(e));
				}
			}
			return null;
		}

		public static void WithFilepathDirectory(FileIOWrapper fileIOWrapper, FileHandlingFileType fileHandlingFileType, string filepath, Action<Exception> callback)
		{
			string directory = Path.GetDirectoryName(filepath);
			if (string.IsNullOrEmpty(directory))
			{
				callback?.Invoke(null);
				return;
			}
			fileIOWrapper.DirectoryExists(directory, fileHandlingFileType, delegate(bool exists)
			{
				if (exists)
				{
					callback?.Invoke(null);
				}
				else
				{
					fileIOWrapper.CreateDirectory(directory, fileHandlingFileType, callback);
				}
			});
		}

		public static void ReadFileIfExists(FileIOWrapper fileIOWrapper, FileHandlingFileType fileHandlingFileType, string path, Action<byte[], Exception> callback)
		{
			fileIOWrapper.FileExists(path, fileHandlingFileType, delegate(bool exists)
			{
				if (exists)
				{
					fileIOWrapper.ReadAllBytes(path, fileHandlingFileType, callback);
				}
				else
				{
					callback?.Invoke(null, null);
				}
			});
		}

		public static void WriteFile(FileIOWrapper fileIOWrapper, FileHandlingFileType fileHandlingFileType, string filepath, byte[] data, Action<Exception> callback)
		{
			WithFilepathDirectory(fileIOWrapper, fileHandlingFileType, filepath, delegate(Exception maybeException)
			{
				if (maybeException != null)
				{
					callback?.Invoke(maybeException);
				}
				else
				{
					fileIOWrapper.WriteAllBytes(filepath, data, fileHandlingFileType, callback);
				}
			});
		}

		public static void DeleteFile(FileIOWrapper fileIOWrapper, FileHandlingFileType fileHandlingFileType, string path, Action<Exception> callback)
		{
			fileIOWrapper.FileExists(path, fileHandlingFileType, delegate(bool exists)
			{
				if (exists)
				{
					fileIOWrapper.DeleteFile(path, fileHandlingFileType, callback);
				}
				else
				{
					callback?.Invoke(null);
				}
			});
		}

		public static void MoveFile(FileIOWrapper fileIOWrapper, FileHandlingFileType fileHandlingFileType, string sourcePath, string destinationPath, Action<Exception> callback)
		{
			fileIOWrapper.MoveFile(sourcePath, destinationPath, overwrite: true, fileHandlingFileType, callback);
		}

		public static void DeleteDirectory(FileIOWrapper fileIOWrapper, FileHandlingFileType fileHandlingFileType, string path, Action<Exception> callback)
		{
			fileIOWrapper.DirectoryExists(path, fileHandlingFileType, delegate(bool exists)
			{
				if (exists)
				{
					fileIOWrapper.DeleteDirectory(path, recursive: true, fileHandlingFileType, callback);
				}
				else
				{
					callback?.Invoke(null);
				}
			});
		}
	}
}
