using ModIO.PlatformIOCallbacks;

namespace ModIO
{
	public interface IPlatformIO
	{
		string InstallationDirectory { get; }

		string CacheDirectory { get; }

		void ReadFile(string path, ReadFileCallback callback);

		void WriteFile(string path, byte[] data, WriteFileCallback callback);

		void DeleteFile(string path, DeleteFileCallback callback);

		void MoveFile(string source, string destination, MoveFileCallback callback);

		void GetFileExists(string path, GetFileExistsCallback callback);

		void GetFileSizeAndHash(string path, GetFileSizeAndHashCallback callback);

		void GetFiles(string path, string nameFilter, bool recurseSubdirectories, GetFilesCallback callback);

		void CreateDirectory(string path, CreateDirectoryCallback callback);

		void DeleteDirectory(string path, DeleteDirectoryCallback callback);

		void MoveDirectory(string source, string destination, MoveDirectoryCallback callback);

		void GetDirectoryExists(string path, GetDirectoryExistsCallback callback);

		void GetDirectories(string path, GetDirectoriesCallback callback);
	}
}
