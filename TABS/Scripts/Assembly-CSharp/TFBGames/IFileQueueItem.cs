using System;

namespace TFBGames
{
	public interface IFileQueueItem
	{
		string Path { get; }

		Action<IFileQueueItem> Process { get; }

		FileHandlingFileType FileType { get; set; }

		ulong? UserId { get; }
	}
}
