using System;

namespace BitCode.AssetManagement
{
	public interface IContentLoadOperation
	{
		bool Async { get; }

		object LoadedAsset { get; }

		bool IsDone { get; }

		string ResourceName { get; }

		event Action<string> LoadStarted;

		event Action<string, object> LoadCompleted;

		void StartLoad();

		void CompleteLoad();
	}
}
