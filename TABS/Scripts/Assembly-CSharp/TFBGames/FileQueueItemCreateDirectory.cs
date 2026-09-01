using System;

namespace TFBGames
{
	public class FileQueueItemCreateDirectory : IFileQueueItem
	{
		public Action<Exception> DoneCallback;

		public string Path { get; set; }

		public Action<IFileQueueItem> Process { get; set; }

		public ulong? UserId { get; set; }

		public FileHandlingFileType FileType { get; set; }
	}
}
