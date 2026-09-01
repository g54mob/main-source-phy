using System;
using System.IO;

namespace TFBGames
{
	public class FileQueueItemSave : IFileQueueItem
	{
		public Stream Stream;

		public Action<Exception> DoneCallback;

		public string Path { get; set; }

		public Action<IFileQueueItem> Process { get; set; }

		public ulong? UserId { get; set; }

		public FileHandlingFileType FileType { get; set; }
	}
}
