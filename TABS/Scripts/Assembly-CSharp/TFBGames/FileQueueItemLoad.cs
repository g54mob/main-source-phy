using System;
using System.IO;

namespace TFBGames
{
	public class FileQueueItemLoad : IFileQueueItem
	{
		public Action<Stream, Exception> DoneCallback;

		public string Path { get; set; }

		public Action<IFileQueueItem> Process { get; set; }

		public ulong? UserId { get; set; }

		public FileHandlingFileType FileType { get; set; }
	}
}
