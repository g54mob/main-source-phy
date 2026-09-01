using System.IO;

namespace CloudinaryDotNet
{
	public class FileDescription
	{
		internal int BufferLength = int.MaxValue;

		internal long BytesSent;

		public Stream Stream { get; }

		public string FileName { get; set; }

		public string FilePath { get; }

		public bool IsRemote { get; }

		internal bool Eof => BytesSent == GetFileLength();

		public FileDescription(string name, Stream stream)
		{
			FileName = name;
			Stream = stream;
		}

		public FileDescription(string name, string filePath)
		{
			IsRemote = Utils.IsRemoteFile(filePath);
			FilePath = filePath;
			FileName = (IsRemote ? filePath : name);
		}

		public FileDescription(string filePath)
		{
			IsRemote = Utils.IsRemoteFile(filePath);
			FilePath = filePath;
			FileName = (IsRemote ? filePath : Path.GetFileName(filePath));
		}

		internal long GetFileLength()
		{
			return Stream?.Length ?? new FileInfo(FilePath).Length;
		}

		internal void Reset(int bufferSize = int.MaxValue)
		{
			BufferLength = bufferSize;
			BytesSent = 0L;
		}
	}
}
