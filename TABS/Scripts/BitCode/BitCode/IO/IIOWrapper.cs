using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BitCode.IO
{
	public interface IIOWrapper
	{
		void WriteToFile(string path, byte[] buffer);

		void WriteToFile(string path, [NotNull] byte[] buffer, int offset, int length);

		Task WriteToFileAsync(string path, [NotNull] byte[] buffer);

		Task WriteToFileAsync(string path, [NotNull] byte[] buffer, int offset, int length);

		void ReadFromFile(string path, [CanBeNull] ref byte[] buffer, out long numReadBytes);

		void ReadFromFile(string path, [CanBeNull] ref byte[] buffer, int offset, out long numReadBytes);

		Task<(long bytesRead, byte[] readBuffer)> ReadFromFileAsync(string path, [CanBeNull] byte[] buffer);

		Task<(long bytesRead, byte[] readBuffer)> ReadFromFileAsync(string path, [CanBeNull] byte[] buffer, int offset);

		bool FileExists(string path);

		Task<bool> FileExistsAsync(string path);

		void DeleteFile(string path);

		Task DeleteFileAsync(string path);
	}
}
