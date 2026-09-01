using System;
using System.IO;

namespace TFBGames
{
	public interface IFileIOPlatform
	{
		bool IsReady { get; }

		void CreateDirectory(string path, FileHandlingFileType fileType, Action<Exception> doneCallback);

		void DirectoryExists(string path, FileHandlingFileType fileType, Action<bool> doneCallback);

		void DeleteDirectory(string path, bool recursive, FileHandlingFileType fileType, Action<Exception> doneCallback);

		void GetDirectories(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback);

		void GetFiles(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback);

		void FileExists(string path, FileHandlingFileType fileType, Action<bool> doneCallback);

		void DeleteFile(string path, FileHandlingFileType fileType, Action<Exception> doneCallback);

		void OpenRead(string path, FileHandlingFileType fileType, Action<Stream, Exception> doneCallback);

		void CloseRead(string path, Stream stream);

		Stream OpenWrite(string path);

		void CloseWrite(string path, Stream stream, FileHandlingFileType fileType, Action<Exception> doneCallback);

		void ReadAllText(string path, FileHandlingFileType fileType, Action<string, Exception> doneCallback);

		void WriteAllText(string path, string contents, FileHandlingFileType fileType, Action<Exception> doneCallback);

		void WaitForAsyncsToFinish(Action doneCallback);

		bool IsBusyWithAsyncs();

		void MoveFile(string sourcePath, string destinationPath, FileHandlingFileType fileType, Action<Exception> doneCallback);

		void MoveDirectory(string sourcePath, string destinationPath, FileHandlingFileType fileType, Action<Exception> doneCallback);

		void GetFilesRecursive(string path, FileHandlingFileType fileType, Action<string[], Exception> doneCallback);
	}
}
