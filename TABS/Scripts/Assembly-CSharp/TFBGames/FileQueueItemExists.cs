using System;

namespace TFBGames
{
	public class FileQueueItemExists : IFileQueueItem
	{
		public Action<bool> DoneCallback;

		public bool IsDirectory;

		public string Path { get; set; }

		public Action<IFileQueueItem> Process { get; set; }

		public ulong? UserId { get; set; }

		public FileHandlingFileType FileType { get; set; }
	}
}
