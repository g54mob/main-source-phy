using System;

namespace TFBGames
{
	public class FileQueueItemMove : IFileQueueItem
	{
		public Action<Exception> DoneCallback;

		public string Path { get; set; }

		public string DestinationPath { get; set; }

		public Action<IFileQueueItem> Process { get; set; }

		public ulong? UserId { get; set; }

		public FileHandlingFileType FileType { get; set; }

		public bool IsFile { get; set; }
	}
}
