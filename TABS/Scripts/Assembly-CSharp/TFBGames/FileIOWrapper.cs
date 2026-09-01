using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DM;
using TABS.Security;
using UnityEngine;

namespace TFBGames
{
	public class FileIOWrapper : ServicePrefab
	{
		private const string NullPlatformFileIOExceptionMessage = "Platform IO wrapper is null.";

		private IFileIOPlatform fileIOPlatform;

		private IFileIOPlatform dotNetFileIOPlatform;

		private IPlatformIO_To_IFileIOPlatform platformIOWrapper;

		private IUserDataIO_To_IFileIOPlatform userDataIOWrapper;

		public bool IsReady
		{
			get
			{
				if (fileIOPlatform != null)
				{
					return fileIOPlatform.IsReady;
				}
				return false;
			}
		}

		public override void OnAwake()
		{
			base.OnAwake();
			dotNetFileIOPlatform = new FileIODefault();
			fileIOPlatform = dotNetFileIOPlatform;
		}

		public void CreateDirectory(string path, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(new Exception("Platform IO wrapper is null."));
			}
			else
			{
				platform.CreateDirectory(path, fileType, doneCallback);
			}
		}

		public void DirectoryExists(string path, FileHandlingFileType fileType, Action<bool> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(obj: false);
				return;
			}
			platform.DirectoryExists(path, fileType, delegate(bool b)
			{
				doneCallback?.Invoke(b);
			});
		}

		public void DeleteDirectory(string path, bool recursive, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(new Exception("Platform IO wrapper is null."));
			}
			else
			{
				platform.DeleteDirectory(path, recursive, fileType, doneCallback);
			}
		}

		public void GetDirectories(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(null, new Exception("Platform IO wrapper is null."));
				return;
			}
			platform.GetDirectories(path, fileType, delegate(string[] s, Exception e)
			{
				doneCallback?.Invoke(s, e);
			});
		}

		public void GetFiles(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(null, new Exception("Platform IO wrapper is null."));
				return;
			}
			platform.GetFiles(path, fileType, delegate(string[] s, Exception e)
			{
				doneCallback?.Invoke(s, e);
			});
		}

		public void FileExists(string path, FileHandlingFileType fileType, Action<bool> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(obj: false);
				return;
			}
			platform.FileExists(path, fileType, delegate(bool b)
			{
				doneCallback?.Invoke(b);
			});
		}

		public void SerializeWithBinaryFormatter(string path, object objectToSerialize, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			try
			{
				Stream stream = OpenWrite(path, fileType);
				if (stream == null)
				{
					doneCallback?.Invoke(new IOException("The stream is null."));
					return;
				}
				new BinaryFormatter().Serialize(stream, objectToSerialize);
				CloseWrite(path, stream, fileType, delegate(Exception closeException)
				{
					if (closeException == null)
					{
						doneCallback?.Invoke(null);
					}
					else
					{
						doneCallback?.Invoke(closeException);
					}
				});
			}
			catch (Exception obj)
			{
				doneCallback?.Invoke(obj);
			}
		}

		public void DeserializeWithBinaryFormatter<T>(string path, FileHandlingFileType fileType, Action<object, Exception> doneCallback)
		{
			OpenRead(path, fileType, delegate(Stream stream, Exception openException)
			{
				if (stream == null)
				{
					doneCallback?.Invoke(null, openException ?? new IOException("Failed to open the stream."));
					return;
				}
				object arg = null;
				Exception arg2 = null;
				try
				{
					arg = new BinaryFormatter
					{
						Binder = new TargetTypeSerializationBinder<T>()
					}.Deserialize(stream);
				}
				catch (Exception ex)
				{
					arg2 = ex;
				}
				finally
				{
					CloseRead(path, fileType, stream);
					doneCallback?.Invoke(arg, arg2);
				}
			});
		}

		public void ReadAllBytes(string path, FileHandlingFileType fileType, Action<byte[], Exception> doneCallback)
		{
			OpenRead(path, fileType, delegate(Stream stream, Exception openException)
			{
				if (stream == null)
				{
					doneCallback?.Invoke(null, openException ?? new IOException("Failed to open the stream."));
				}
				else
				{
					byte[] arg = null;
					Exception arg2 = null;
					try
					{
						arg = ReadBytesFromStream(stream);
					}
					catch (Exception ex)
					{
						Debug.LogErrorFormat("Failed to read bytes from file [{0}]\n{1}", path, ex);
						arg2 = ex;
					}
					CloseRead(path, fileType, stream);
					doneCallback?.Invoke(arg, arg2);
				}
			});
		}

		public void WriteAllBytes(string path, byte[] bytes, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			try
			{
				Stream stream = OpenWrite(path, fileType);
				if (stream == null)
				{
					doneCallback?.Invoke(new IOException("Failed to open the stream."));
					return;
				}
				try
				{
					WriteBytesToStream(stream, bytes);
				}
				catch (Exception ex)
				{
					Debug.LogErrorFormat("Failed to write bytes to file [{0}]\n{1}", path, ex);
				}
				CloseWrite(path, stream, fileType, doneCallback);
			}
			catch (Exception obj)
			{
				doneCallback?.Invoke(obj);
				throw;
			}
		}

		public void ReadAllText(string path, FileHandlingFileType fileType, Action<string, Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(null, new Exception("Platform IO wrapper is null."));
			}
			else
			{
				platform.ReadAllText(path, fileType, doneCallback);
			}
		}

		public void WriteAllText(string path, string contents, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(new Exception("Platform IO wrapper is null."));
			}
			else
			{
				platform.WriteAllText(path, contents, fileType, doneCallback);
			}
		}

		public void CopyFile(string sourcePath, string destinationPath, bool overwrite, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			if (overwrite)
			{
				CopyFileInternal(sourcePath, destinationPath, fileType, doneCallback);
				return;
			}
			FileExists(destinationPath, fileType, delegate(bool exists)
			{
				if (exists)
				{
					doneCallback?.Invoke(new IOException("The destination file exists."));
				}
				else
				{
					CopyFileInternal(sourcePath, destinationPath, fileType, doneCallback);
				}
			});
		}

		public void DeleteFile(string path, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(new InvalidOperationException("Platform IO wrapper is null."));
			}
			else
			{
				platform.DeleteFile(path, fileType, doneCallback);
			}
		}

		public void WaitForAsyncsToFinish(Action doneCallback)
		{
			if (fileIOPlatform == null)
			{
				doneCallback?.Invoke();
			}
			else
			{
				fileIOPlatform.WaitForAsyncsToFinish(doneCallback);
			}
		}

		public bool IsBusyWithAsyncs()
		{
			if (fileIOPlatform == null)
			{
				return false;
			}
			return fileIOPlatform.IsBusyWithAsyncs();
		}

		public void MoveFile(string sourcePath, string destinationPath, bool overwrite, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			if (overwrite)
			{
				MoveFileInternal(sourcePath, destinationPath, fileType, doneCallback);
				return;
			}
			FileExists(destinationPath, fileType, delegate(bool exists)
			{
				if (exists)
				{
					doneCallback?.Invoke(new IOException("The destination file exists."));
				}
				else
				{
					MoveFileInternal(sourcePath, destinationPath, fileType, doneCallback);
				}
			});
		}

		public void MoveDirectory(string sourcePath, string destinationPath, bool overwrite, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			if (overwrite)
			{
				MoveDirectoryInternal(sourcePath, destinationPath, fileType, doneCallback);
				return;
			}
			DirectoryExists(destinationPath, fileType, delegate(bool exists)
			{
				if (exists)
				{
					doneCallback?.Invoke(new IOException("The destination directory exists."));
				}
				else
				{
					MoveDirectoryInternal(sourcePath, destinationPath, fileType, doneCallback);
				}
			});
		}

		public void GetFilesRecursive(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(null, new Exception("Platform IO wrapper is null."));
				return;
			}
			platform.GetFilesRecursive(path, fileType, delegate(string[] s, Exception e)
			{
				doneCallback?.Invoke(s, e);
			});
		}

		private void MoveFileInternal(string sourcePath, string destinationPath, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(new InvalidOperationException("Platform IO wrapper is null."));
			}
			else
			{
				platform.MoveFile(sourcePath, destinationPath, fileType, doneCallback);
			}
		}

		private void MoveDirectoryInternal(string sourcePath, string destinationPath, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(new InvalidOperationException("Platform IO wrapper is null."));
			}
			else
			{
				platform.MoveDirectory(sourcePath, destinationPath, fileType, doneCallback);
			}
		}

		private void CopyFileInternal(string sourcePath, string destinationPath, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			ReadAllBytes(sourcePath, fileType, delegate(byte[] bytes, Exception exception)
			{
				if (exception != null)
				{
					doneCallback?.Invoke(exception);
				}
				else
				{
					WriteAllBytes(destinationPath, bytes, fileType, delegate(Exception writeException)
					{
						doneCallback?.Invoke(writeException);
					});
				}
			});
		}

		private void OpenRead(string path, FileHandlingFileType fileType, Action<Stream, Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(null, new Exception("Platform IO wrapper is null."));
			}
			else
			{
				platform.OpenRead(path, fileType, doneCallback);
			}
		}

		private void CloseRead(string path, FileHandlingFileType fileType, Stream stream)
		{
			GetPlatform(fileType)?.CloseRead(path, stream);
		}

		private Stream OpenWrite(string path, FileHandlingFileType fileType)
		{
			return GetPlatform(fileType)?.OpenWrite(path);
		}

		private void CloseWrite(string path, Stream stream, FileHandlingFileType fileType, Action<Exception> doneCallback)
		{
			IFileIOPlatform platform = GetPlatform(fileType);
			if (platform == null)
			{
				doneCallback?.Invoke(new Exception("Platform IO wrapper is null."));
			}
			else
			{
				platform.CloseWrite(path, stream, fileType, doneCallback);
			}
		}

		private IFileIOPlatform GetPlatform(FileHandlingFileType fileType)
		{
			if (IsOnConsoleOrWindowsGameCore() && fileType != FileHandlingFileType.PlayerPrefsOrTABSSave)
			{
				return dotNetFileIOPlatform;
			}
			return fileIOPlatform;
		}

		private bool IsOnConsoleOrWindowsGameCore()
		{
			return false;
		}

		private static byte[] ReadBytesFromStream(Stream stream)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}

		private static void WriteBytesToStream(Stream stream, byte[] bytes)
		{
			new BinaryWriter(stream).Write(bytes);
		}
	}
}
