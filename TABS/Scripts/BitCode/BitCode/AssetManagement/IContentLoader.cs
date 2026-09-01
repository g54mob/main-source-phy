using System;

namespace BitCode.AssetManagement
{
	public interface IContentLoader
	{
		int QueueCount { get; }

		bool AnyItemQueued { get; }

		bool Busy { get; }

		IContentLoadOperation LoadItem(string resourceName, bool async, Action<string> onLoadStart = null, Action<string, object> onLoadComplete = null, bool addToFront = false);

		bool CancelLoadItem(string resourceName);

		bool IsQueued(string resourceName);
	}
}
